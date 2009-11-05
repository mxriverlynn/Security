using FluentNHibernate.Mapping;

namespace Security.TestRepository
{
	public class ActivityMap: ClassMap<Activity>
	{
		public ActivityMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Id(a => a.Id)
				.Column("Id")
				.GeneratedBy.Native();

			Map(a => a.Name);
			Map(a => a.Description);
		}
	}
}