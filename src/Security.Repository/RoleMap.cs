using FluentNHibernate.Mapping;

namespace Security.Repository
{
	public class RoleMap: ClassMap<Role>
	{
		public RoleMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Id(r => r.Id)
				.Column("Id")
				.GeneratedBy.Native();

			Map(r => r.Name);
			HasManyToMany(r => r.Users)
				.Access.Property()
				.AsBag()
				.Cascade.None()
				.Table("User_Roles")
				.ParentKeyColumn("RoleId")
				.ChildKeyColumn("UserId");
		}
	}
}