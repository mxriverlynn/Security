namespace Security
{
	public interface IAuthorizationService
	{
		bool IsAllowed(IUser user, string action);
		Permission SetPermission(IUser user, IAction action, bool isAllowed);
		Permission SetPermission(IRole role, IAction action, bool isAllowed);
		void RemovePermission(IUser user, IAction action);
		void RemovePermission(IRole role, IAction action);
	}
}