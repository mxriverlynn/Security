using Security.TestModel;
using Security.TestRepository;
using StructureMap.Configuration.DSL;

namespace Security.TestApp
{
	public class DefaultRegistry: Registry
	{
		public DefaultRegistry()
		{
			ForRequestedType<ISecurityRepository>()
				.TheDefaultIsConcreteType<SecurityRepository>();
			ForRequestedType<IUserRepository>()
				.TheDefaultIsConcreteType<UserRepository>();

			ForRequestedType<ISecurityService>()
				.TheDefaultIsConcreteType<SecurityService>();
		}
	}
}