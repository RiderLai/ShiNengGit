using Abp.AutoMapper;
using ShiNengShiHui.AppServices.AdministratorDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiNengShiHui.Web.Models.Administrator.User
{
    [AutoMapFrom(typeof(UserShowOutput))]
    [AutoMapTo(typeof(UserUpdateInput))]
    public class UserEditModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public int? TeacherId { get; set; }
    }
}