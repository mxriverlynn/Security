using System;
using System.Security.Principal;

namespace Security.TestModel
{
	public class User : IUser, IPrincipal
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		
		public bool IsInRole(string role)
		{
			return false;
		}

		public IIdentity Identity
		{
			get { throw new NotImplementedException(); }
		}
	}
}