namespace Security
{
	public class Permission
	{
		public virtual int Id{ get; set; }

		public virtual bool IsAllowed { get; set; }

		public virtual Activity Activity { get; set; }

		public virtual User User { get; set; }

		public virtual UserGroup Group { get; set; }
	}
}
