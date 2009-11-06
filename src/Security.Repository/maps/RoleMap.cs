using FluentNHibernate.Mapping;
using Security.TestModel;

namespace Security.TestRepository.maps
{
	public class RoleMap: SubclassMap<Role>
	{
		public RoleMap()
		{
			KeyColumn("Id");

			Map(r => r.Name);
			HasManyToMany(r => r.Users)
				.Access.Property()
				.AsBag()
				.Cascade.None()
				.Table("User_Role")
				.ParentKeyColumn("RoleId")
				.ChildKeyColumn("UserId");
		}
	}
}