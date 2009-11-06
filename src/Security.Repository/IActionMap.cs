using FluentNHibernate.Mapping;

namespace Security.TestRepository
{
	public class IActionMap: ClassMap<IAction>
	{
		public IActionMap()
		{
			CreateMap();
		}

		private void CreateMap()
		{
			Table("Action");

			Id(a => a.Id)
				.Column("Id")
				.GeneratedBy.Native();
		}
	}
}