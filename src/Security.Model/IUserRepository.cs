namespace Security.TestModel
{
	public interface IUserRepository
	{
		User GetUser(string name);
	}
}