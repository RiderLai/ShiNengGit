using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.User
{
    [AutoMapFrom(typeof(UserShowOutput))]
    [AutoMapTo(typeof(UserPasswordUpdateInput))]
    public class UserPasswordEditModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}