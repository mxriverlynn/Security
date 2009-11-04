namespace Security
{
	public interface ISecurityService
	{
		bool IsAllowed(IUser user, string activity);
	}
}