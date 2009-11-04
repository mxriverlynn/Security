namespace Security
{
	public interface ISecurityService
	{
		bool IsAllowed(User user, string activity);
	}
}