using System.Threading.Tasks;
using Abp.Application.Services;
using ShiNengShiHui.Roles.Dto;

namespace ShiNengShiHui.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
