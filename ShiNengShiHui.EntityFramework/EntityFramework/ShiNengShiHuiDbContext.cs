using System.Data.Common;
using Abp.Zero.EntityFramework;
using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.MultiTenancy;
using ShiNengShiHui.Users;
using ShiNengShiHui.Entities.Teachers;
using ShiNengShiHui.Entities.Classes;
using ShiNengShiHui.Entities.Students;
using ShiNengShiHui.Entities.Grades;
using ShiNengShiHui.Entities.Prizes;
using System.Data.Entity;
using ShiNengShiHui.Entities.Function;

namespace ShiNengShiHui.EntityFramework
{
    public class ShiNengShiHuiDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public virtual IDbSet<Teacher> Teacher { get; set; }
        public virtual IDbSet<Class> Class { get; set; }
        public virtual IDbSet<Student> Student { get; set; }
        public virtual IDbSet<Grade> Grade { get; set; }
        public virtual IDbSet<Prize> Prize { get; set; }
        public virtual IDbSet<PrizeItem> PrizeItem { get; set; }
        public virtual IDbSet<Function> Function { get; set; }
        public virtual IDbSet<GroupWeekGrade> GroupWeekGrade { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ShiNengShiHuiDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ShiNengShiHuiDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ShiNengShiHuiDbContext since ABP automatically handles it.
         */
        public ShiNengShiHuiDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ShiNengShiHuiDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ShiNengShiHuiDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
