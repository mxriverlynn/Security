using System.Collections.Generic;
using NHibernate.Criterion;
using Security.Model;
using UoW.NHibernate;

namespace Security.Repository
{
	public class UserRepository: NHibernateRepository, IUserRepository
	{
		public User GetUser(string name)
		{
			IList<User> users = Session.CreateCriteria<User>()
				.Add(Restrictions.Eq("Name", name))
				.List<User>();
			return users[0];
		}
	}
}
