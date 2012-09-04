using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using CustomMvcSolution.Domain.Entities;
using CustomMvcSolution.Domain.Infrastructure;

namespace CustomMvcSolution.Domain.Initializers.Database
{
    public class DbInitDevelopment
    {
        private static DefaultDbContext context;
        private static Rule[] rules;
        private static Role[] roles;

        public static void Setup(DefaultDbContext dbContext)
        {
            context = dbContext;

            SetupRules();
            SetupRoles();
            SetupMembers();

            context.SaveChanges();
        }

        private static void SetupMembers()
        {
            Member defaultMember = new Member
                                       {
                                           UserName = "root", 
                                           Email = "root@example.com", 
                                           Role = roles[0],
                                           Profile = new Profile
                                                         {
                                                             FirstName = "Administrator",
                                                             LastName = "Application",
                                                             CompleteName = "Application Administrator",
                                                             Address = "",
                                                             Building = "",
                                                             Floor = "",
                                                             Phone = ""
                                                         }
                                       };
            Password defaultPassword = new Password
                                           {
                                               Salt = "IG9mMcHcoWAx6hM2W0aDkfUEf+Y/fjmNk6v1MTXy1B0=",
                                               Hash = "CC9622A7EAA4E34F88CCE34CA5D95420284332F3", // Password: Torino12
                                               Member = defaultMember
                                           };

            context.Members.Add(defaultMember);
            context.Passwords.Add(defaultPassword);
        }
        
        private static void SetupRules()
        {
            rules = new[]
                {
                    new Rule { Name = "Can_Access_Application" },
                    new Rule { Name = "Ca_Access_Admin_Area"}
                };

            foreach (var rule in rules)
            {
                context.Rules.Add(rule);
            }
        }

        private static void SetupRoles()
        {
            roles = new[]
                        {
                            new Role { Name = "Administrators" },
                            new Role { Name = "Power Users"},
                            new Role { Name = "Users" }
                        };

            List<RuleForRole> ruleForRoles = rules.Select(rule => new RuleForRole {Rule = rule, Role = roles[0], IsActive = true}).ToList();
            rules.Select(rule => new RuleForRole { Rule = rule, Role = roles[1], IsActive = true }).ToList();
            rules.Select(rule => new RuleForRole { Rule = rule, Role = roles[2], IsActive = false }).ToList();

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            foreach (var r in ruleForRoles)
            {
                context.RulesForRoles.Add(r);
            }

        }
    }
}
