namespace Security
{
	public interface ISecurityService
	{
		bool IsAllowed(IUser user, string activity);
		Permission SetPermission(IUser user, Activity activity, bool isAllowed);
		Permission SetPermission(Role role, Activity activity, bool isAllowed);
	}
}