using FluentNHibernate.Mapping;

namespace Security.Repository
{
	public class IUserMap: ClassMap<IUser>
	{
		public IUserMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Table("Users");
			
			Id(u => u.Id)
				.Column("Id")
				.GeneratedBy.Native();

			DiscriminateSubClassesOnColumn("Id");
		
		}
	}
}