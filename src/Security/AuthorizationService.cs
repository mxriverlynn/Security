using System.Collections.Generic;

namespace Security
{
	public class AuthorizationService : IAuthorizationService
	{
		private IPermissionRepository PermissionRepository { get; set; }

		public AuthorizationService(IPermissionRepository permissionRepository)
		{
			PermissionRepository = permissionRepository;
		}

		public bool IsAllowed(IUser user, string action)
		{
			bool isAllowed = false;
			IList<Permission> permissions = PermissionRepository.GetActionPermissionsByUserAndRole(user, action);
			
			if (permissions != null)
				isAllowed = CheckPermissionsForAllowedAccess(permissions);
			
			return isAllowed;
		}

		public Permission SetPermission(IUser user, IAction action, bool isAllowed)
		{
			Permission permission = PermissionRepository.GetActionPermissionsByUser(user, action);
			
			if (permission == null)
				permission = new Permission(user, action, isAllowed);
			else
				permission.IsAllowed = isAllowed;

			PermissionRepository.SavePermission(permission);
			
			return permission;
		}

		public Permission SetPermission(IRole role, IAction action, bool isAllowed)
		{
			Permission permission = PermissionRepository.GetActionPermissionsByRole(role, action);

			if (permission == null)
				permission = new Permission(role, action, isAllowed);
			else
				permission.IsAllowed = isAllowed;
			
			PermissionRepository.SavePermission(permission);
			return permission;			
		}

		public void RemovePermission(IUser user, IAction action)
		{
			Permission permission = PermissionRepository.GetActionPermissionsByUser(user, action);
			DeletePermission(permission);
		}

		public void RemovePermission(IRole role, IAction action)
		{
			Permission permission = PermissionRepository.GetActionPermissionsByRole(role, action);
			DeletePermission(permission);
		}

		private void DeletePermission(Permission permission)
		{
			if (permission != null)
				PermissionRepository.DeletePermission(permission);
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