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
                    Action = "NULL",
                    Controller = "NULL",
                    ICon = "lnr lnr-text-format",
                    Order=200,
                    RoleId = teacherRole.Id
                };

                gradeManage = _context.Function.Add(gradeManage);
                _context.SaveChanges();
            }

            var weekGradeManage = _context.Function.FirstOrDefault(m => m.Name.Equals("周成绩管理") && m.RoleId == teacherRole.Id);
            if (weekGradeManage == null)
            {
                weekGradeManage = new Function()
                {
                    PID = gradeManage.Id,
                    Name = "周成绩管理",
                    Action = "GradeIndex",
                    Controller = "Teacher",
                    ICon = "NULL",
                    Order = 201,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(weekGradeManage);
                _context.SaveChanges();
            }

            var weekGradeShow = _context.Function.FirstOrDefault(m => m.Name.Equals("周成绩查看") && m.RoleId == teacherRole.Id);
            if (weekGradeShow == null)
            {
                weekGradeShow = new Function()
                {
                    PID = gradeManage.Id,
                    Name = "周成绩查看",
                    Action = "WeekGradeShow",
                    Controller = "Teacher",
                    ICon = "NULL",
                    Order = 202,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(weekGradeShow);
                _context.SaveChanges();
            }

            var groupWeekGradeManage = _context.Function.FirstOrDefault(m => m.Name.Equals("优胜组管理") && m.RoleId == teacherRole.Id);
            if (groupWeekGradeManage == null)
            {
                groupWeekGradeManage = new Function()
                {
                    PID = gradeManage.Id,
                    Name = "优胜组管理",
                    Action = "GroupWeekGradeIndex",
                    Controller = "Teacher",
                    ICon = "NULL",
                    Order = 203,
                    RoleId = teacherRole.Id
                };

                _context.Function.Add(groupWeekGradeManage);
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
