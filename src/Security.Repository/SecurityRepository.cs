using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using UoW.NHibernate;

namespace Security.Repository
{
	public class SecurityRepository: NHibernateRepository, ISecurityRepository
	{
		public IList<Permission> GetPermissionsForUserActivity(User user, string activity)
		{
			ICriterion userIdMatches = Restrictions.Eq("Id", user.Id);
			ICriterion activityNameMatches = Restrictions.Eq("Name", activity);
			ICriterion userIdAliasMatches = Restrictions.Eq("u.Id", user.Id);

			DetachedCriteria groupPermissionCriteria = DetachedCriteria.For<Permission>()
				.SetProjection(Projections.Property("Role"))
				.CreateCriteria("Role").CreateCriteria("Users").Add(userIdMatches);
			ICriterion groupSubquery = Subqueries.PropertyIn("Role", groupPermissionCriteria);
            
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.CreateAlias("User", "u", JoinType.LeftOuterJoin)
				.Add(Restrictions.Or(userIdAliasMatches, groupSubquery));
			permissionCriteria.CreateCriteria("Activity").Add(activityNameMatches);

            ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			return permissions;
		}

		public User GetUser(string name)
		{
			IList<User> users = Session.CreateCriteria<User>()
				.Add(Restrictions.Eq("Name", name))
				.List<User>();
			return users[0];
		}
	}
}