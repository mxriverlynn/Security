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
			IList<Permission> permissions = SecurityRepository.GetActivityPermissionsByUserAndRole(user, activity);
			
			if (permissions != null)
				isAllowed = CheckPermissionsForAllowedAccess(permissions);
			
			return isAllowed;
		}

		public Permission SetPermission(IUser user, Activity activity, bool isAllowed)
		{
			Permission permission = SecurityRepository.GetActivityPermissionsByUser(user, activity);
			
			if (permission == null)
				permission = new Permission(user, activity, isAllowed);
			else
				permission.IsAllowed = isAllowed;

			SecurityRepository.SavePermission(permission);
			
			return permission;
		}

		public Permission SetPermission(Role role, Activity activity, bool isAllowed)
		{
			Permission permission = SecurityRepository.GetActivityPermissionsByRole(role, activity);

			if (permission == null)
				permission = new Permission(role, activity, isAllowed);
			else
				permission.IsAllowed = isAllowed;
			
			SecurityRepository.SavePermission(permission);
			return permission;			
		}

		public void RemovePermission(IUser user, Activity activity)
		{
			Permission permission = SecurityRepository.GetActivityPermissionsByUser(user, activity);
			DeletePermission(permission);
		}

		public void RemovePermission(Role role, Activity activity)
		{
			Permission permission = SecurityRepository.GetActivityPermissionsByRole(role, activity);
			DeletePermission(permission);
		}

		private void DeletePermission(Permission permission)
		{
			if (permission != null)
				SecurityRepository.DeletePermission(permission);
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