using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Security.TestModel;
using SpecUnit;

namespace Security.Specs
{
	public class CheckPermissionsSpecs
	{

		public class SecuritySpecsContext : ContextSpecification
		{
			protected User _user;
			protected string _action = "some action";
			protected ISecurityRepository _securityRepository;

			protected override void SharedContext()
			{
				_user = new User();
				_securityRepository = MockRepository.GenerateMock<ISecurityRepository>();
			}

			protected ISecurityService GetSecurityService()
			{
				SecurityService service = new SecurityService(_securityRepository);
				return service;
			}

			protected Permission GetPermission(bool isAllowed, User user, string actionName)
			{
				return new Permission(user, new Action {Name = actionName}, isAllowed);
			}
		}

		[TestFixture]
		[Concern("Checking Permissions")]
		public class When_checking_a_users_permission_for_an_action_that_they_are_allowed : SecuritySpecsContext
		{
			bool isAllowed;

			protected override void Context()
			{
				Permission permission = GetPermission(true, _user, "some action");

				_securityRepository.Stub(r => r.GetActionPermissionsByUserAndRole(null, null)).IgnoreArguments()
					.Return(new List<Permission> {permission});

				ISecurityService securityService = GetSecurityService();
				isAllowed = securityService.IsAllowed(_user, _action);
			}

			[Test]
			[Observation]
			public void Should_load_any_available_permission_for_the_user_and_action()
			{
				_securityRepository.AssertWasCalled(r => r.GetActionPermissionsByUserAndRole(_user, _action));
			}

			[Test]
			[Observation]
			public void Should_allow_access()
			{
				isAllowed.ShouldBeTrue();
			}

		}

		[TestFixture]
		[Concern("Checking Permissions")]
		public class When_checking_a_users_permission_for_an_action_that_they_are_not_allowed : SecuritySpecsContext
		{
			bool isAllowed;

			protected override void Context()
			{
				Permission permission = GetPermission(false, _user, "some action");

				_securityRepository.Stub(r => r.GetActionPermissionsByUserAndRole(null, null)).IgnoreArguments()
					.Return(new List<Permission> { permission });

				ISecurityService securityService = GetSecurityService();
				isAllowed = securityService.IsAllowed(_user, _action);
			}

			[Test]
			[Observation]
			public void Should_load_any_available_permission_for_the_user_and_action()
			{
				_securityRepository.AssertWasCalled(r => r.GetActionPermissionsByUserAndRole(_user, _action));
			}

			[Test]
			[Observation]
			public void Should_deny_access()
			{
				isAllowed.ShouldBeFalse();
			}

		}

		[TestFixture]
		[Concern("Checking Permissions")]
		public class When_checking_a_users_permission_for_an_action_that_they_have_no_assigned_permission_for : SecuritySpecsContext
		{
			bool isAllowed;

			protected override void Context()
			{
				_securityRepository.Stub(r => r.GetActionPermissionsByUserAndRole(null, null)).IgnoreArguments().Return(null);

				ISecurityService securityService = GetSecurityService();
				isAllowed = securityService.IsAllowed(_user, _action);
			}

			[Test]
			[Observation]
			public void Should_deny_access()
			{
				isAllowed.ShouldBeFalse();
			}

		}

		[TestFixture]
		[Concern("Checking Permissions")]
		public class When_checking_a_users_permission_for_an_action_and_they_have_at_least_one_allowed_and_at_least_one_denied_permissions : SecuritySpecsContext
		{
			bool isAllowed;

			protected override void Context()
			{
				Permission allowedPermission = GetPermission(true, _user, "some action");
				Permission deniedPermission = GetPermission(false, _user, "some action");

				_securityRepository.Stub(r => r.GetActionPermissionsByUserAndRole(null, null)).IgnoreArguments()
					.Return(new List<Permission> { allowedPermission, deniedPermission });

				ISecurityService securityService = GetSecurityService();
				isAllowed = securityService.IsAllowed(_user, _action);
			}

			[Test]
			[Observation]
			public void Should_deny_access()
			{
				isAllowed.ShouldBeFalse();
			}

		}


	}
}
