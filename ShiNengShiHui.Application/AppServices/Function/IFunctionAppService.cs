using Abp.Application.Services;
using ShiNengShiHui.AppServices.FunctionDTO;

namespace ShiNengShiHui.AppServices
{
    public interface IFunctionAppService:IApplicationService
    {
        FunctionGetOfRoleOutput FunctionOfRoleGetAll();
    }
}
