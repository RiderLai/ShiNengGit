using Abp.AutoMapper;
using ShiNengShiHui.Users;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapTo(typeof(User))]
    public class UserCreateInput
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public int? TeacherId { get; set; }
    }
}