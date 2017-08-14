using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using ShiNengShiHui.EntityFramework;

namespace ShiNengShiHui.Migrator
{
    [DependsOn(typeof(ShiNengShiHuiDataModule))]
    public class ShiNengShiHuiMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ShiNengShiHuiDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}