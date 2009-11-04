namespace Security.Model
{
	public interface IUserRepository
	{
		User GetUser(string name);
	}
}
