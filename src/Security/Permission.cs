using System;

namespace Security
{
	public class Permission
	{
		public int Id{ get; set; }

		public bool IsAllowed { get; set; }

		public Activity Activity { get; set; }

		public IUser User { get; set; }

		public IRole Role { get; set; }

		private Permission() {/*for nhibernate*/}

		public Permission(IUser user, Activity activity, bool isAllowed)
		{
			User = user;
			Init(activity, isAllowed);
		}

		public Permission(IRole role, Activity activity, bool isAllowed)
		{
			Role = role;
			Init(activity, isAllowed);
		}

		private void Init(Activity activity, bool isAllowed)
		{
			Activity = activity;
			IsAllowed = isAllowed;
		}
	}
}
