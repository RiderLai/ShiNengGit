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
    public class TeacherFunctionCreator
    {
        private readonly ShiNengShiHuiDbContext _context;

        public TeacherFunctionCreator(ShiNengShiHuiDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var teacherRole = _context.Roles.FirstOrDefault(m => m.Name.Equals(StaticRoleNames.Tenants.Teacher) && m.TenantId == 1);

            var index = _context.Function.FirstOrDefault(m => m.Name.Equals("主页") && m.RoleId==teacherRole.Id);
            if (index == null)
            {
                index = new Function()
                {
                    PID = 0,
                    Name = "主页",
                    Action = "Index",
                    Controller = "Home",
                    ICon = "lnr lnr-home",
                    Order=0,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(index);
                _context.SaveChanges();
            }

            var studentManage = _context.Function.FirstOrDefault(m => m.Name.Equals("学生管理") && m.RoleId==teacherRole.Id);
            if (studentManage==null)
            {
                studentManage = new Function()
                {
                    PID = 0,
                    Name = "学生管理",
                    Action = "StudentIndex",
                    Controller = "Teacher",
                    ICon = "lnr lnr-dice",
                    Order=100,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(studentManage);
                _context.SaveChanges();
            }

            var gradeManage = _context.Function.FirstOrDefault(m => m.Name.Equals("成绩管理") && m.RoleId==teacherRole.Id);
            if (gradeManage==null)
            {
                gradeManage = new Function()
                {
                    PID = 0,
                    Name = "成绩管理",
                    Action = "GradeIndex",
                    Controller = "Teacher",
                    ICon = "lnr lnr-text-format",
                    Order=200,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(gradeManage);
                _context.SaveChanges();
            }

            var prizeManage = _context.Function.FirstOrDefault(m => m.Name.Equals("获奖查看")&& m.RoleId==teacherRole.Id);
            if (prizeManage == null)
            {
                prizeManage = new Function()
                {
                    PID = 0,
                    Name = "获奖查看",
                    Action = "PrizeIndex",
                    Controller = "Teacher",
                    ICon = "lnr lnr-chart-bars",
                    Order=300,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(prizeManage);
                _context.SaveChanges();
            }
        }
    }
}
