using FluentNHibernate.Mapping;

namespace Security.TestRepository
{
	public class PermissionMap: ClassMap<Permission>
	{
		public PermissionMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Id(p => p.Id)
				.Column("Id")
				.GeneratedBy.Native();

			References(p => p.User).Column("UserId");
			References(p => p.Role).Column("RoleId");
			References(p => p.Action).Column("ActionId");

			Map(p => p.IsAllowed);
		}
	}
}