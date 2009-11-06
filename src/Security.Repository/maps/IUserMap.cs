using FluentNHibernate.Mapping;

namespace Security.TestRepository.maps
{
	public class IUserMap: ClassMap<IUser>
	{
		public IUserMap()
		{
			Table("User");

			Id(u => u.Id)
				.Column("Id")
				.GeneratedBy.Native();
		}
	}
}