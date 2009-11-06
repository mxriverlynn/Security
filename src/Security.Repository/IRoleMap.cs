using FluentNHibernate.Mapping;

namespace Security.TestRepository
{
	public class IRoleMap: ClassMap<IRole>
	{
		public IRoleMap()
		{
			Table("Role");

			Id(r => r.Id)
				.Column("Id")
				.GeneratedBy.Native();
		}
	}
}