using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using UoW.NHibernate;

namespace Security.TestRepository
{
	public class SecurityRepository: NHibernateRepository, ISecurityRepository
	{
		public IList<Permission> GetActivityPermissionsByUserAndRole(IUser user, string activity)
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

		public void SavePermission(Permission permission)
		{
			Session.SaveOrUpdate(permission);
		}

		public Permission GetActivityPermissionsByUser(IUser user, Activity activity)
		{
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.Add(Restrictions.Eq("User", user))
				.Add(Restrictions.Eq("Activity", activity));

			ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			Permission permission = null;
			if (permissions.Count > 0)
				permission = permissions[0];
			
			return permission;
		}

		public Permission GetActivityPermissionsByRole(Role role, Activity activity)
		{
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.Add(Restrictions.Eq("Role", role))
				.Add(Restrictions.Eq("Activity", activity));

			ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			Permission permission = null;
			if (permissions.Count > 0)
				permission = permissions[0];

			return permission;
		}

		public void DeletePermission(Permission permission)
		{
			Session.Delete(permission);
		}
	}
}