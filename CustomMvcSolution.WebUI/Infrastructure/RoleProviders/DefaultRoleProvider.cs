using System;
using System.Linq;
using System.Web.Security;
using CustomMvcSolution.Domain.Entities;
using CustomMvcSolution.Domain.Infrastructure;

namespace CustomMvcSolution.WebUI.Infrastructure.RoleProviders
{
    public class DefaultRoleProvider : RoleProvider
    {
        private readonly DefaultDbContext _context = new DefaultDbContext();

        #region Overrides of RoleProvider

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            Member member = _context.Members.FirstOrDefault(m => m.UserName == username);

            if (member != null)
            {
                var result = member.Role.Rules.Where(r => r.IsActive).Select(r => r.Rule.Name).ToList();
                return result.ToArray<string>();
            }

            return new string[]{};
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}