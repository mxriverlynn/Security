namespace Security.Model
{
	public class User : IUser
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
	}
}