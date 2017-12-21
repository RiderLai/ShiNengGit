using Abp.Authorization.Users;
using Microsoft.AspNet.Identity;
using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.EntityFramework;
using ShiNengShiHui.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.Migrations.SeedData.EntitiesData
{
    public class UserCreator
    {
        private readonly ShiNengShiHuiDbContext _context;
        private readonly int _tenantId;

        public UserCreator(ShiNengShiHuiDbContext context,int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreatrEditions();
        }

        private void CreatrEditions()
        {
            //Add default teacher user
            var testUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName.Equals("test"));
            if (testUser==null)
            {
                testUser = new User()
                {
                    TenantId = _tenantId,
                    UserName = "test",
                    Name = "三",
                    Surname = "张",
                    EmailAddress = "lyf670354671@gmail.com",
                    Password = new PasswordHasher().HashPassword("123qwe"),
                    IsEmailConfirmed = true,
                    IsActive = true,
                    TeacherId=_context.Teacher.FirstOrDefault(u=>u.Name.Equals(DefaultEntitiesDataCreator.DefalutTeacherName)).Id
                };

                testUser = _context.Users.Add(testUser);
                
                var teacherRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(StaticRoleNames.Tenants.Teacher));
                if (teacherRole!=null)
                {
                    _context.UserRoles.Add(new UserRole(_tenantId, testUser.Id, teacherRole.Id));
                }

                _context.SaveChanges();
            }

            //Add default headmaster user
            var headmasterUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName.Equals("headmaster"));
            if (headmasterUser == null)
            {
                headmasterUser = new User()
                {
                    TenantId = _tenantId,
                    UserName = "headmaster",
                    Name = "长",
                    Surname = "校",
                    EmailAddress = "lyf670354671@gmail.com",
                    Password = new PasswordHasher().HashPassword("123qwe"),
                    IsEmailConfirmed = true,
                    IsActive = true,
                };

                headmasterUser = _context.Users.Add(headmasterUser);

                var headmasterRole = _context.Roles.FirstOrDefault(r => r.Name.Equals(StaticRoleNames.Tenants.Headmaster));
                if (headmasterRole != null)
                {
                    _context.UserRoles.Add(new UserRole(_tenantId, headmasterUser.Id, headmasterRole.Id));
                }

                _context.SaveChanges();
            }
        }
    }
}
