using System.Collections.Generic;

namespace CustomMvcSolution.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<RuleForRole> Rules { get; set; } 
    }

    public class Rule
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }

    public class RuleForRole
    {
        public Role Role { get; set; }
        public Rule Rule { get; set; }
        public bool IsActive { get; set; }
    }

}
