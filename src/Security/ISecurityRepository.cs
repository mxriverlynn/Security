using System.Collections.Generic;

namespace Security
{
	public interface ISecurityRepository
	{
		IList<Permission> GetPermissionsForUserActivity(IUser user, string activity);
		void AddPermission(Permission permission);
	}
}