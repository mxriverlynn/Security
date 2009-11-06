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

		public bool IsAllowed(IUser user, string action)
		{
			bool isAllowed = false;
			IList<Permission> permissions = SecurityRepository.GetActionPermissionsByUserAndRole(user, action);
			
			if (permissions != null)
				isAllowed = CheckPermissionsForAllowedAccess(permissions);
			
			return isAllowed;
		}

		public Permission SetPermission(IUser user, IAction action, bool isAllowed)
		{
			Permission permission = SecurityRepository.GetActionPermissionsByUser(user, action);
			
			if (permission == null)
				permission = new Permission(user, action, isAllowed);
			else
				permission.IsAllowed = isAllowed;

			SecurityRepository.SavePermission(permission);
			
			return permission;
		}

		public Permission SetPermission(IRole role, IAction action, bool isAllowed)
		{
			Permission permission = SecurityRepository.GetActionPermissionsByRole(role, action);

			if (permission == null)
				permission = new Permission(role, action, isAllowed);
			else
				permission.IsAllowed = isAllowed;
			
			SecurityRepository.SavePermission(permission);
			return permission;			
		}

		public void RemovePermission(IUser user, IAction action)
		{
			Permission permission = SecurityRepository.GetActionPermissionsByUser(user, action);
			DeletePermission(permission);
		}

		public void RemovePermission(IRole role, IAction action)
		{
			Permission permission = SecurityRepository.GetActionPermissionsByRole(role, action);
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