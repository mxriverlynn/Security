using FluentNHibernate.Mapping;

namespace Security.TestRepository.maps
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