using Abp.Authorization;
using ShiNengShiHui.Authorization.Roles;
using ShiNengShiHui.MultiTenancy;
using ShiNengShiHui.Users;

namespace ShiNengShiHui.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
