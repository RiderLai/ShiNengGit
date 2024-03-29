﻿using Abp.AutoMapper;
using ShiNengShiHui.Users;

namespace ShiNengShiHui.AppServices.AdministratorDTO
{
    [AutoMapTo(typeof(User))]
    public class UserUpdateInput
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public int? TeacherId { get; set; }
    }
}