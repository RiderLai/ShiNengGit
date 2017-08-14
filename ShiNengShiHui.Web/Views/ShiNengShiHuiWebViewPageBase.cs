using Abp.Web.Mvc.Views;

namespace ShiNengShiHui.Web.Views
{
    public abstract class ShiNengShiHuiWebViewPageBase : ShiNengShiHuiWebViewPageBase<dynamic>
    {

    }

    public abstract class ShiNengShiHuiWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ShiNengShiHuiWebViewPageBase()
        {
            LocalizationSourceName = ShiNengShiHuiConsts.LocalizationSourceName;
        }
    }
}