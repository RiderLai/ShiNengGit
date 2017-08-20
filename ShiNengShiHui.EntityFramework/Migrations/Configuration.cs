using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using ShiNengShiHui.Migrations.SeedData;
using EntityFramework.DynamicFilters;
using ShiNengShiHui.Migrations.SeedData.EntitiesData;

namespace ShiNengShiHui.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ShiNengShiHui.EntityFramework.ShiNengShiHuiDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ShiNengShiHui";
        }

        protected override void Seed(ShiNengShiHui.EntityFramework.ShiNengShiHuiDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            new DefaultEntitiesDataCreator(context).Create();
            new RoleCreator(context, 1).Create();
            new TeacherUserCreator(context, 1).Create();
            new TeacherFunctionCreator(context).Create();

            context.SaveChanges();
        }
    }
}
