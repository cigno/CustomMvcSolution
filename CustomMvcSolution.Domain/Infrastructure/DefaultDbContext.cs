using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection;
using CustomMvcSolution.Domain.Entities;
using CustomMvcSolution.Domain.Entities.Attributes;
using CustomMvcSolution.Domain.Infrastructure;
using CustomMvcSolution.Domain.Initializers.Database;

namespace CustomMvcSolution.Domain.Infrastructure
{
    public class DefaultDbContext : DbContext
    {
        // MEMBERS DB SET
        public DbSet<Member> Members { get; set; }
        public DbSet<Password> Passwords { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<RuleForRole> RulesForRoles { get; set; }
        public DbSet<Rule> Rules { get; set; }

        // SETTINGS DB SET

        // 
        //  On Model Creating                                              
        // -----------------------------------------------------------------
        //                                                              
        //  Adds Entity specific Table settings                           
        //                                                                
        // -----------------------------------------------------------------
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Add here your Custom Entities Configurations...
            // modelBuilder.Configurations.Add(new MemberConfigurations());
        }
    }
}

//
//  DB INITIALIZER
// -----------------------------------------------------------
//
//  Custom Database Initializer who manages to create
//  Tables based on Entity's definitions and configurations.
//
// -----------------------------------------------------------
public class DbInitializer : IDatabaseInitializer<DefaultDbContext>
{
    //
    // Initialize Database
    // ------------------------------------------------
    //
    // Delete and Initialize the database and adds unique
    // keys when a custom [Unique] Data Attribute it's 
    // specified above an Entity Attribute
    //
    // ------------------------------------------------

    #region IDatabaseInitializer<RSDbContext> Members

    public void InitializeDatabase(DefaultDbContext context)
    {
        if (context == null) throw new ArgumentNullException("context");

        if (Debugger.IsAttached && context.Database.Exists() && !context.Database.CompatibleWithModel(false))
        {
            context.Database.Delete();
        }

        if (context.Database.Exists())
        {
            context.Database.Delete();
        }

        if (!context.Database.Exists())
        {
            context.Database.Create();

            var contextObject = context as Object;
            Type contextType = contextObject.GetType();
            PropertyInfo[] properties = contextType.GetProperties();

            foreach (PropertyInfo pi in properties)
            {
                if (pi.PropertyType.IsGenericType && pi.PropertyType.Name.Contains("DbSet"))
                {
                    Type t = pi.PropertyType.GetGenericArguments()[0];

                    object[] mytableName = t.GetCustomAttributes(typeof(TableAttribute), true);
                    string tableName;
                    if (mytableName.Length > 0)
                    {
                        var mytable = mytableName[0] as TableAttribute;
                        Debug.Assert(mytable != null, "mytable != null");
                        tableName = mytable.Name;
                    }
                    else
                    {
                        tableName = pi.Name;
                    }

                    foreach (PropertyInfo piEntity in t.GetProperties())
                    {
                        // If [Unique] Attribute it's specified over an Entity Attribute
                        // Adds a unique key to the table for the specified field
                        if (piEntity.GetCustomAttributes(typeof(UniqueAttribute), true).Length > 0)
                        {
                            string fieldName = piEntity.Name;
                            context.Database.ExecuteSqlCommand(
                                string.Format("ALTER TABLE {0} ADD CONSTRAINT with_Unique_{0}_{1} UNIQUE ({1})", tableName,
                                          fieldName)
                                );
                        }

                        // IF [Indexed] Attribute it's specified over an Entity Attribute
                        // Creates an index for the specified field
                        if (piEntity.GetCustomAttributes(typeof(IndexedAttribute), true).Length > 0)
                        {
                            string fieldName = piEntity.Name;
                            
                            context.Database.ExecuteSqlCommand(
                                string.Format("CREATE INDEX IX_{0}_{1} ON {0} ({1})", tableName, fieldName)
                                );
                        }
                    }
                }
            }

            // Populate the database
            
            // DEVELOPMENT
            DbInitDevelopment.Setup();

            // PRODUCTION
        }
    }

    #endregion
}