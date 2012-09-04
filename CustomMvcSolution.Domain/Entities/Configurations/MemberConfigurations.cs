using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace CustomMvcSolution.Domain.Entities.Configurations
{
    class MemberConfigurations : EntityTypeConfiguration<Member>
    {
        public MemberConfigurations()
        {
            // Add Here Your Fluent API Configurations...
            HasKey(m => m.MemberId);
            Property(m => m.UserName).IsRequired().HasMaxLength(32);
            Property(m => m.Email).IsRequired().HasMaxLength(256);
        }
    }
}
