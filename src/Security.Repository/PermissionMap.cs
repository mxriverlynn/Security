using FluentNHibernate.Mapping;

namespace Security.Repository
{
	public class PermissionMap: ClassMap<Permission>
	{
		public PermissionMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Table("Permissions");

			Id(p => p.Id)
				.Column("Id")
				.GeneratedBy.Native();

			References(p => p.User).Column("UserId");
			References(p => p.Role).Column("RoleId");
			References(p => p.Activity).Column("ActivityId");

			Map(p => p.IsAllowed);
		}
	}
}