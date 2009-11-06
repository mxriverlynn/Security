using FluentNHibernate.Mapping;
using Security.TestModel;

namespace Security.TestRepository.maps
{
	public class ActionMap: SubclassMap<Action>
	{
		public ActionMap()
		{
			KeyColumn("Id");
			Map(a => a.Name);
			Map(a => a.Description);
		}
	}
}