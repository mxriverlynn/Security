namespace Security
{
	public interface ISecurityService
	{
		bool IsAllowed(IUser user, string activity);
		Permission AddPermission(IUser user, Activity activity, bool isAllowed);
		Permission AddPermission(Role role, Activity activity, bool isAllowed);
	}
}