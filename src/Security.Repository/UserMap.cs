using FluentNHibernate.Mapping;
using Security.Model;

namespace Security.Repository
{
	public class UserMap: SubclassMap<User>
	{
		public UserMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Table("Users");

			Map(u => u.Name);
			Map(u => u.Password);
		}
	}
}