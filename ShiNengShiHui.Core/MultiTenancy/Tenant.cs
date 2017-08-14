using Abp.MultiTenancy;
using ShiNengShiHui.Users;

namespace ShiNengShiHui.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}