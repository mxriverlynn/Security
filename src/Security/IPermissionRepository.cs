using System.Collections.Generic;

namespace Security
{
	public interface IPermissionRepository
	{
		IList<Permission> GetPermissionsByUserWithRoles(IUser user, string action);
		Permission GetPermissionByUser(IUser user, IAction action);
		Permission GetPermissionByRole(IRole role, IAction action);
		void SavePermission(Permission permission);
		void DeletePermission(Permission permission);
	}
}