using ShiNengShiHui.EntityFramework;
using EntityFramework.DynamicFilters;

namespace ShiNengShiHui.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly ShiNengShiHuiDbContext _context;

        public InitialHostDbBuilder(ShiNengShiHuiDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
