using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Security.TestModel;
using SpecUnit;
using Action=Security.TestModel.Action;

namespace Security.Specs
{
	public class ManagePermissionsSpecs
	{

		protected static bool allow = true;
		protected static bool deny = false;

		public class ManagePermissionsSpecsContext : ContextSpecification
		{
			protected IPermissionRepository permissionRepository;
			
			protected override void SharedContext()
			{
				permissionRepository = MockRepository.GenerateMock<IPermissionRepository>();
			}

			protected IAction GetAction()
			{
				return new Action();
			}

			protected IUser GetUser()
			{
				return new User();
			}

			protected IAuthorizationService GetSecurityService()
			{
				return new AuthorizationService(permissionRepository);
			}

			protected IRole GetRole()
			{
				return new Role();
			}

			protected void SetExistingPermissionForUser(Permission permission)
			{
				permissionRepository.Stub(r => r.GetActionPermissionsByUser(null, null))
					.IgnoreArguments()
					.Return(permission);
			}

			protected void SetExistingPermissionForRole(Permission permission)
			{
				permissionRepository.Stub(r => r.GetActionPermissionsByRole(null, null))
					.IgnoreArguments()
					.Return(permission);				
			}
		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_setting_a_new_user_permission_for_an_action : ManagePermissionsSpecsContext
		{

			IUser user;
			IAction action;

			protected override void Context()
			{
				user = GetUser();
				action = GetAction();

				IAuthorizationService service = GetSecurityService();
				service.SetPermission(user, action, allow);
			}

			[Test]
			[Observation]
			public void Should_check_for_an_existing_user_to_action_permission()
			{
				permissionRepository.AssertWasCalled(r => r.GetActionPermissionsByUser(user, action));
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_updating_a_user_permission_for_an_action : ManagePermissionsSpecsContext
		{

			IUser user;
			IAction action;
			Permission permission;

			protected override void Context()
			{
				user = GetUser();
				action = GetAction();
				permission = new Permission(user, action, deny);
				SetExistingPermissionForUser(permission);

				IAuthorizationService service = GetSecurityService();
				service.SetPermission(user, action, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

			[Test]
			[Observation]
			public void Should_update_the_permission()
			{
				permission.IsAllowed.ShouldBeTrue();
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_an_existing_user_permission_for_an_action : ManagePermissionsSpecsContext
		{

			IUser user;
			IAction action;
			Permission permission;

			protected override void Context()
			{
				user = GetUser();
				action = GetAction();
				permission = new Permission(user, action, deny);
				SetExistingPermissionForUser(permission);

				IAuthorizationService service = GetSecurityService();
				service.RemovePermission(user, action);
			}

			[Test]
			[Observation]
			public void Should_remove_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.DeletePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_a_nonexistent_user_permission_for_an_action : ManagePermissionsSpecsContext
		{

			IUser user;
			IAction action;

			protected override void Context()
			{
				user = GetUser();
				action = GetAction();
				IAuthorizationService service = GetSecurityService();
				service.RemovePermission(user, action);
			}

			[Test]
			[Observation]
			public void Should_attempt_to_load_the_existing_permission()
			{
				permissionRepository.AssertWasCalled(r => r.GetActionPermissionsByUser(user, action));
			}

			[Test]
			[Observation]
			public void Should_not_remove_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasNotCalled(r => r.DeletePermission(null), mo => mo.IgnoreArguments());
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_setting_a_role_permission_for_an_action : ManagePermissionsSpecsContext
		{

			IRole role;
			IAction action;

			protected override void Context()
			{
				role = GetRole();
				action = GetAction();

				IAuthorizationService service = GetSecurityService();
				service.SetPermission(role, action, allow);
			}

			[Test]
			[Observation]
			public void Should_check_for_an_existing_role_to_action_permission()
			{
				permissionRepository.AssertWasCalled(r => r.GetActionPermissionsByRole(role, action));
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_updating_a_role_permission_for_an_action : ManagePermissionsSpecsContext
		{
			private IRole role;
			IAction action;
			Permission permission;

			protected override void Context()
			{
				role = GetRole();
				action = GetAction();
				permission = new Permission(role, action, deny);
				SetExistingPermissionForRole(permission);

				IAuthorizationService service = GetSecurityService();
				service.SetPermission(role, action, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

			[Test]
			[Observation]
			public void Should_update_the_permission()
			{
				permission.IsAllowed.ShouldBeTrue();
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_an_existing_role_permission_for_an_action : ManagePermissionsSpecsContext
		{
			IRole role;
			IAction action;
			Permission permission;

			protected override void Context()
			{
				role = GetRole();
				action = GetAction();
				permission = new Permission(role, action, deny);
				SetExistingPermissionForRole(permission);

				IAuthorizationService service = GetSecurityService();
				service.RemovePermission(role, action);
			}

			[Test]
			[Observation]
			public void Should_remove_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasCalled(r => r.DeletePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_a_nonexistent_role_permission_for_an_action : ManagePermissionsSpecsContext
		{
			IRole role;
			IAction action;

			protected override void Context()
			{
				role = GetRole();
				action = GetAction();
				IAuthorizationService service = GetSecurityService();
				service.RemovePermission(role, action);
			}

			[Test]
			[Observation]
			public void Should_attempt_to_load_the_existing_permission()
			{
				permissionRepository.AssertWasCalled(r => r.GetActionPermissionsByRole(role, action));
			}

			[Test]
			[Observation]
			public void Should_not_remove_the_permission_for_the_specified_user()
			{
				permissionRepository.AssertWasNotCalled(r => r.DeletePermission(null), mo => mo.IgnoreArguments());
			}

		}

	}
}
