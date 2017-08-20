using Abp.Application.Services;
using ShiNengShiHui.AppServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiNengShiHui.AppServices
{
    public interface IFunctionAppService:IApplicationService
    {
        FunctionGetOfRoleOutput FunctionOfRoleGetAll();
    }
}
