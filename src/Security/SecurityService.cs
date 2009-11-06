using System.Collections.Generic;

namespace Security
{
	public class SecurityService : ISecurityService
	{
		private ISecurityRepository SecurityRepository { get; set; }

		public SecurityService(ISecurityRepository securityRepository)
		{
			SecurityRepository = securityRepository;
		}

		public bool IsAllowed(IUser user, string activity)
		{
			bool isAllowed = false;
			IList<Permission> permissions = SecurityRepository.GetPermissionsForUserActivity(user, activity);
			
			if (permissions != null)
				isAllowed = CheckPermissionsForAllowedAccess(permissions);
			
			return isAllowed;
		}

		public Permission AddPermission(IUser user, Activity activity, bool isAllowed)
		{
			Permission permission = new Permission(user, activity, isAllowed);
			SecurityRepository.AddPermission(permission);
			return permission;
		}

		public Permission AddPermission(Role role, Activity activity, bool isAllowed)
		{
			Permission permission = new Permission(role, activity, isAllowed);
			SecurityRepository.AddPermission(permission);
			return permission;			
		}

		private bool CheckPermissionsForAllowedAccess(IEnumerable<Permission> permissions)
		{
			bool isAllowed = false;
			foreach(Permission permission in permissions)
			{
				if (permission.IsAllowed)
					isAllowed = true;
				else
				{
					isAllowed = false;
					break;
				}
			}
			return isAllowed;
		}
	}
}