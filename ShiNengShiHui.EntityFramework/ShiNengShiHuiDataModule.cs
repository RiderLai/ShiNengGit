using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using ShiNengShiHui.EntityFramework;

namespace ShiNengShiHui
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(ShiNengShiHuiCoreModule))]
    public class ShiNengShiHuiDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ShiNengShiHuiDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
