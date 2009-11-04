using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using UoW;
using UoW.NHibernate;

namespace Security.Repository
{
	public static class RepoConfig
	{
		public static IUnitOfWorkConfiguration GetConfiguration()
		{
			Configuration config = Fluently.Configure()
				.Database(SQLiteConfiguration.Standard.UsingFile("security.s3db"))
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<SecurityRepository>()).BuildConfiguration();

			return new NHibernateConfig(() => config, new StructureMapRepositoryFactory(), new ThreadStaticUnitOfWorkStorage());
		}
	}
}