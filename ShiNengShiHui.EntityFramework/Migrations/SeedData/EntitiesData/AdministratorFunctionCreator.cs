using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.Entities.Function;
using ShiNengShiHui.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Migrations.SeedData.EntitiesData
{
    public class AdministratorFunctionCreator
    {
        private readonly ShiNengShiHuiDbContext _context;

        public AdministratorFunctionCreator(ShiNengShiHuiDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditons();
        }

        private void CreateEditons()
        {
            var adminRole = _context.Roles.FirstOrDefault(m => m.Name.Equals(StaticRoleNames.Tenants.Admin) && m.TenantId == 1);

            var index = _context.Function.FirstOrDefault(m => m.Name.Equals("主页") && m.RoleId==adminRole.Id);
            if (index == null)
            {
                index = new Function()
                {
                    PID = 0,
                    Name = "主页",
                    Action = "Index",
                    Controller = "Home",
                    ICon = "lnr lnr-home",
                    Order = 0,
                    RoleId = adminRole.Id
                };

                _context.Function.Add(index);
                _context.SaveChanges();
            }

            var userManage = _context.Function.FirstOrDefault(m => m.Name.Equals("用户管理") && m.RoleId==adminRole.Id);
            if (userManage == null)
            {
                userManage = new Function()
                {
                    PID = 0,
                    Name = "用户管理",
                    Action = "UserIndex",
                    Controller = "Administrator",
                    ICon = "lnr lnr-dice",
                    Order = 100,
                    RoleId = adminRole.Id
                };

                _context.Function.Add(userManage);
                _context.SaveChanges();
            }

            var classManage = _context.Function.FirstOrDefault(m => m.Name.Equals("班级管理") && m.RoleId==adminRole.Id);
            if (classManage==null)
            {
                classManage = new Function()
                {
                    PID = 0,
                    Name = "班级管理",
                    Action = "ClassIndex",
                    Controller = "Administrator",
                    ICon = "lnr lnr-text-format",
                    Order = 200,
                    RoleId = adminRole.Id
                };

                _context.Function.Add(classManage);
                _context.SaveChanges();
            }

            var teacherManage = _context.Function.FirstOrDefault(m => m.Name.Equals("教师管理") && m.RoleId==adminRole.Id);
            if (teacherManage==null)
            {
                teacherManage = new Function()
                {
                    PID = 0,
                    Name = "教师管理",
                    Action = "TeacherIndex",
                    Controller="Administrator",
                    ICon = "lnr lnr-chart-bars",
                    Order = 300,
                    RoleId = adminRole.Id
                };

                _context.Function.Add(teacherManage);
                _context.SaveChanges();
            }
        }
    }
}
