﻿using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.Entities.Function;
using ShiNengShiHui.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Migrations.SeedData.EntitiesData
{
    public class HeadmasterFunctionCreator
    {
        private readonly ShiNengShiHuiDbContext _context;

        public HeadmasterFunctionCreator(ShiNengShiHuiDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var headmasterRole = _context.Roles.FirstOrDefault(m => m.Name.Equals(StaticRoleNames.Tenants.Headmaster));

            var index = _context.Function.FirstOrDefault(m => m.Name.Equals("主页") && m.RoleId == headmasterRole.Id);
            if (index==null)
            {
                index = new Function()
                {
                    PID = 0,
                    Name = "主页",
                    Action = "Index",
                    Controller = "Home",
                    ICon = "lnr lnr-home",
                    Order = 0,
                    RoleId = headmasterRole.Id
                };

                _context.Function.Add(index);
                _context.SaveChanges();
            }

            var teacherShow = _context.Function.FirstOrDefault(m => m.Name.Equals("教师查看") && m.RoleId == headmasterRole.Id);
            if (teacherShow==null)
            {
                teacherShow = new Function()
                {
                    PID = 0,
                    Name = "教师查看",
                    Action = "TeacherIndex",
                    Controller = "Headmaster",
                    ICon = "lnr lnr-dice",
                    Order = 100,
                    RoleId = headmasterRole.Id
                };

                _context.Function.Add(teacherShow);
                _context.SaveChanges();
            }

            var classShow = _context.Function.FirstOrDefault(m => m.Name.Equals("班级查看") && m.RoleId == headmasterRole.Id);
            if (classShow==null)
            {
                classShow = new Function()
                {
                    PID = 0,
                    Name = "班级查看",
                    Action = "ClassIndex",
                    Controller = "Headmaster",
                    ICon = "lnr lnr-chart-bars",
                    Order = 200,
                    RoleId = headmasterRole.Id
                };

                _context.Function.Add(classShow);
                _context.SaveChanges();
            }
        }
    }
}
