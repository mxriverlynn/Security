using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Security.TestModel;
using SpecUnit;

namespace Security.Specs
{
	public class ManagePermissionsSpecs
	{

		protected static bool allow = true;
		protected static bool deny = false;

		public class ManagePermissionsSpecsContext : ContextSpecification
		{
			protected ISecurityRepository securityRepository;
			
			protected override void SharedContext()
			{
				securityRepository = MockRepository.GenerateMock<ISecurityRepository>();
			}

			protected Activity GetActivity()
			{
				return new Activity();
			}

			protected IUser GetUser()
			{
				return new User();
			}

			protected ISecurityService GetSecurityService()
			{
				return new SecurityService(securityRepository);
			}

			protected Role GetRole()
			{
				return new Role();
			}

			protected void SetExistingPermissionForUser(Permission permission)
			{
				securityRepository.Stub(r => r.GetActivityPermissionsByUser(null, null))
					.IgnoreArguments()
					.Return(permission);
			}

			protected void SetExistingPermissionForRole(Permission permission)
			{
				securityRepository.Stub(r => r.GetActivityPermissionsByRole(null, null))
					.IgnoreArguments()
					.Return(permission);				
			}
		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_setting_a_new_user_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			IUser user;
			Activity activity;

			protected override void Context()
			{
				user = GetUser();
				activity = GetActivity();

				ISecurityService service = GetSecurityService();
				service.SetPermission(user, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_check_for_an_existing_user_to_activity_permission()
			{
				securityRepository.AssertWasCalled(r => r.GetActivityPermissionsByUser(user, activity));
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_updating_a_user_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			IUser user;
			Activity activity;
			Permission permission;

			protected override void Context()
			{
				user = GetUser();
				activity = GetActivity();
				permission = new Permission(user, activity, deny);
				SetExistingPermissionForUser(permission);

				ISecurityService service = GetSecurityService();
				service.SetPermission(user, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
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
		public class When_removing_an_existing_user_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			IUser user;
			Activity activity;
			Permission permission;

			protected override void Context()
			{
				user = GetUser();
				activity = GetActivity();
				permission = new Permission(user, activity, deny);
				SetExistingPermissionForUser(permission);

				ISecurityService service = GetSecurityService();
				service.RemovePermission(user, activity);
			}

			[Test]
			[Observation]
			public void Should_remove_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.DeletePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_a_nonexistent_user_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			IUser user;
			Activity activity;

			protected override void Context()
			{
				user = GetUser();
				activity = GetActivity();
				ISecurityService service = GetSecurityService();
				service.RemovePermission(user, activity);
			}

			[Test]
			[Observation]
			public void Should_attempt_to_load_the_existing_permission()
			{
				securityRepository.AssertWasCalled(r => r.GetActivityPermissionsByUser(user, activity));
			}

			[Test]
			[Observation]
			public void Should_not_remove_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasNotCalled(r => r.DeletePermission(null), mo => mo.IgnoreArguments());
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_setting_a_role_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			Role role;
			Activity activity;

			protected override void Context()
			{
				role = GetRole();
				activity = GetActivity();

				ISecurityService service = GetSecurityService();
				service.SetPermission(role, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_check_for_an_existing_role_to_activity_permission()
			{
				securityRepository.AssertWasCalled(r => r.GetActivityPermissionsByRole(role, activity));
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_updating_a_role_permission_for_an_activity : ManagePermissionsSpecsContext
		{
			private Role role;
			Activity activity;
			Permission permission;

			protected override void Context()
			{
				role = GetRole();
				activity = GetActivity();
				permission = new Permission(role, activity, deny);
				SetExistingPermissionForRole(permission);

				ISecurityService service = GetSecurityService();
				service.SetPermission(role, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.SavePermission(null), mo => mo
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
		public class When_removing_an_existing_role_permission_for_an_activity : ManagePermissionsSpecsContext
		{
			Role role;
			Activity activity;
			Permission permission;

			protected override void Context()
			{
				role = GetRole();
				activity = GetActivity();
				permission = new Permission(role, activity, deny);
				SetExistingPermissionForRole(permission);

				ISecurityService service = GetSecurityService();
				service.RemovePermission(role, activity);
			}

			[Test]
			[Observation]
			public void Should_remove_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.DeletePermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_removing_a_nonexistent_role_permission_for_an_activity : ManagePermissionsSpecsContext
		{
			Role role;
			Activity activity;

			protected override void Context()
			{
				role = GetRole();
				activity = GetActivity();
				ISecurityService service = GetSecurityService();
				service.RemovePermission(role, activity);
			}

			[Test]
			[Observation]
			public void Should_attempt_to_load_the_existing_permission()
			{
				securityRepository.AssertWasCalled(r => r.GetActivityPermissionsByRole(role, activity));
			}

			[Test]
			[Observation]
			public void Should_not_remove_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasNotCalled(r => r.DeletePermission(null), mo => mo.IgnoreArguments());
			}

		}

	}
}
