using System;

namespace Security
{
	public class Permission
	{
		public int Id{ get; set; }

		public bool IsAllowed { get; set; }

		public IAction Action { get; set; }

		public IUser User { get; set; }

		public IRole Role { get; set; }

		private Permission() {/*for nhibernate*/}

		public Permission(IUser user, IAction action, bool isAllowed)
		{
			User = user;
			Init(action, isAllowed);
		}

		public Permission(IRole role, IAction action, bool isAllowed)
		{
			Role = role;
			Init(action, isAllowed);
		}

		private void Init(IAction action, bool isAllowed)
		{
			Action = action;
			IsAllowed = isAllowed;
		}
	}
}
