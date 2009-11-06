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
		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_adding_a_user_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			protected override void Context()
			{
				IUser user = GetUser();
				Activity activity = GetActivity();

				ISecurityService service = GetSecurityService();
				service.AddPermission(user, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.AddPermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}

		[TestFixture]
		[Concern("Manage Permissions")]
		public class When_adding_a_role_permission_for_an_activity : ManagePermissionsSpecsContext
		{

			protected override void Context()
			{
				Role role = GetRole();
				Activity activity = GetActivity();

				ISecurityService service = GetSecurityService();
				service.AddPermission(role, activity, allow);
			}

			[Test]
			[Observation]
			public void Should_add_the_permission_for_the_specified_user()
			{
				securityRepository.AssertWasCalled(r => r.AddPermission(null), mo => mo
					.IgnoreArguments()
					.Constraints(Is.TypeOf<Permission>())
				);
			}

		}
	}
}
