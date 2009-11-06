using System.Collections.Generic;

namespace Security
{
	public interface ISecurityRepository
	{
		IList<Permission> GetActionPermissionsByUserAndRole(IUser user, string action);
		Permission GetActionPermissionsByUser(IUser user, IAction action);
		Permission GetActionPermissionsByRole(IRole role, IAction action);
		void SavePermission(Permission permission);
		void DeletePermission(Permission permission);
	}
}