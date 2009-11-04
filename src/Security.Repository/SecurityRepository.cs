using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Security.Model;
using UoW.NHibernate;

namespace Security.Repository
{
	public class SecurityRepository: NHibernateRepository, ISecurityRepository
	{
		public IList<Permission> GetPermissionsForUserActivity(IUser user, string activity)
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

	}
}