using System.Collections.Generic;

namespace Security.TestModel
{
	public class Role: IRole
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IList<IUser> Users { get; set; }
	}
}