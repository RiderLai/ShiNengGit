using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.User
{
    [AutoMapTo(typeof(UserCreateInput))]
    public class UserCreateModel
    {

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public int? TeacherId { get; set; }
    }
}