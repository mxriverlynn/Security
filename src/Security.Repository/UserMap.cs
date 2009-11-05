using FluentNHibernate.Mapping;
using Security.TestModel;

namespace Security.TestRepository
{
	public class UserMap: SubclassMap<User>
	{
		public UserMap()
		{
			KeyColumn("Id");
			Map(u => u.Name);
			Map(u => u.Password);
		}
	}
}