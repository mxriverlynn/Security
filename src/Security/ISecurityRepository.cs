using System.Collections.Generic;

namespace Security
{
	public interface ISecurityRepository
	{
		IList<Permission> GetPermissionsForUserActivity(User user, string activity);
		User GetUser(string name);
	}
}