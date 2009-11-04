using System.Collections.Generic;

namespace Security
{
	public class UserGroup
	{
		public virtual int Id { get; set; }

		public virtual string Name { get; set; }

		public virtual IList<User> Users { get; protected set; }
	}
}
