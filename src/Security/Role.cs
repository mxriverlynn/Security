using System.Collections.Generic;

namespace Security
{
	public class Role
	{
		public virtual int Id { get; set; }

		public virtual string Name { get; set; }

		public virtual IList<IUser> Users { get; protected set; }
	}
}
