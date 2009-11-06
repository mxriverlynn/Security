using Security.TestModel;
using Security.TestRepository;
using StructureMap.Configuration.DSL;

namespace Security.TestApp
{
	public class DefaultRegistry: Registry
	{
		public DefaultRegistry()
		{
			ForRequestedType<IPermissionRepository>()
				.TheDefaultIsConcreteType<PermissionRepository>();
			ForRequestedType<IUserRepository>()
				.TheDefaultIsConcreteType<UserRepository>();

			ForRequestedType<IAuthorizationService>()
				.TheDefaultIsConcreteType<AuthorizationService>();
		}
	}
}