using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ShiNengShiHui.Authorization
{
    public class ShiNengShiHuiAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            //Tenant permissions
            users.CreateChildPermission(PermissionNames.Pages_Users_Headmaster, multiTenancySides: MultiTenancySides.Tenant);
            users.CreateChildPermission(PermissionNames.Pages_Users_Teacher, multiTenancySides: MultiTenancySides.Tenant);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ShiNengShiHuiConsts.LocalizationSourceName);
        }
    }
}
