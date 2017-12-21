using Abp.Authorization.Roles;
using ShiNengShiHui.Authorization;
using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.EntityFramework;
using System.Linq;

namespace ShiNengShiHui.Migrations.SeedData.EntitiesData
{
    public class RoleCreator
    {
        private readonly ShiNengShiHuiDbContext _context;
        private readonly int _tenantId;

        public RoleCreator(ShiNengShiHuiDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var headmaster = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Headmaster);
            if (headmaster == null)
            {
                headmaster = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Headmaster, StaticRoleNames.Tenants.Headmaster) { IsStatic = true });
                _context.SaveChanges();

                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.Pages_Users,
                        IsGranted = true,
                        RoleId = headmaster.Id
                    });
                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.Pages_Users_Headmaster,
                        IsGranted = true,
                        RoleId = headmaster.Id
                    });
                _context.SaveChanges();
            }

            var teacher = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Teacher);
            if (teacher == null)
            {
                teacher = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Teacher, StaticRoleNames.Tenants.Teacher) { IsStatic = true });
                _context.SaveChanges();

                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.Pages_Users,
                        IsGranted = true,
                        RoleId = teacher.Id
                    });
                _context.Permissions.Add(
                    new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = PermissionNames.Pages_Users_Teacher,
                        IsGranted = true,
                        RoleId = teacher.Id
                    });
                _context.SaveChanges();
            }
        }
    }
}
