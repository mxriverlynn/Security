using FluentNHibernate.Mapping;

namespace Security.Repository
{
	public class UserMap: ClassMap<User>
	{
		public UserMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Id(u => u.Id)
				.Column("Id")
				.GeneratedBy.Native();

			Map(u => u.Name);
			Map(u => u.Password);
		}
	}
}