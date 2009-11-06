using System.Collections.Generic;

namespace Security
{
	public interface ISecurityRepository
	{
		IList<Permission> GetActivityPermissionsByUserAndRole(IUser user, string activity);
		void SavePermission(Permission permission);
		Permission GetActivityPermissionsByUser(IUser user, Activity activity);
		Permission GetActivityPermissionsByRole(Role role, Activity activity);
		void DeletePermission(Permission permission);
	}
}