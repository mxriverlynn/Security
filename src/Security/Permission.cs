namespace Security
{
	public class Permission
	{
		public int Id{ get; set; }

		public bool IsAllowed { get; set; }

		public Activity Activity { get; set; }

		public IUser User { get; set; }

		public Role Role { get; set; }
	}
}
