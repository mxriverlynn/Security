using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using UoW.NHibernate;

namespace Security.TestRepository
{
	public class PermissionRepository: NHibernateRepository, IPermissionRepository
	{
		public IList<Permission> GetActionPermissionsByUserAndRole(IUser user, string action)
		{
			ICriterion userIdMatches = Restrictions.Eq("Id", user.Id);
			ICriterion actionNameMatches = Restrictions.Eq("Name", action);
			ICriterion userIdAliasMatches = Restrictions.Eq("u.Id", user.Id);

			DetachedCriteria groupPermissionCriteria = DetachedCriteria.For<Permission>()
				.SetProjection(Projections.Property("Role"))
				.CreateCriteria("Role").CreateCriteria("Users").Add(userIdMatches);
			ICriterion groupSubquery = Subqueries.PropertyIn("Role", groupPermissionCriteria);
            
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.CreateAlias("User", "u", JoinType.LeftOuterJoin)
				.Add(Restrictions.Or(userIdAliasMatches, groupSubquery));
			permissionCriteria.CreateCriteria("Action").Add(actionNameMatches);

			ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			return permissions;
		}

		public Permission GetActionPermissionsByUser(IUser user, IAction action)
		{
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.Add(Restrictions.Eq("User", user))
				.Add(Restrictions.Eq("Action", action));

			ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			Permission permission = null;
			if (permissions.Count > 0)
				permission = permissions[0];
			
			return permission;
		}

		public Permission GetActionPermissionsByRole(IRole role, IAction action)
		{
			DetachedCriteria permissionCriteria = DetachedCriteria.For<Permission>()
				.Add(Restrictions.Eq("Role", role))
				.Add(Restrictions.Eq("Action", action));

			ICriteria executableCriteria = permissionCriteria.GetExecutableCriteria(Session);
			IList<Permission> permissions = executableCriteria.List<Permission>();

			Permission permission = null;
			if (permissions.Count > 0)
				permission = permissions[0];

			return permission;
		}

		public void SavePermission(Permission permission)
		{
			Session.SaveOrUpdate(permission);
		}

		public void DeletePermission(Permission permission)
		{
			Session.Delete(permission);
		}
	}
}