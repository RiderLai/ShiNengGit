using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace ShiNengShiHui.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class ShiNengShiHuiControllerBase : AbpController
    {
        protected ShiNengShiHuiControllerBase()
        {
            LocalizationSourceName = ShiNengShiHuiConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        /// <summary>
        /// 当弹出DIV弹窗时，需要刷新浏览器整个页面
        /// </summary>
        /// <returns></returns>
        public ContentResult RefreshParent(string alert = null)
        {
            var script = string.Format("<script>{0}; parent.location.reload(1)</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
            return this.Content(script);
        }

        //public new ContentResult RefreshParentTab(string alert = null)
        //{
        //    var script = string.Format("<script>{0}; if (window.opener != null) {{ window.opener.location.reload(); window.opener = null;window.open('', '_self', '');  window.close()}} else {{parent.location.reload(1)}}</script>", string.IsNullOrEmpty(alert) ? string.Empty : "alert('" + alert + "')");
        //    return this.Content(script);
        //}

        /// <summary>
        /// 用JS关闭弹窗
        /// </summary>
        /// <returns></returns>
        public ContentResult CloseThickbox()
        {
            return this.Content("<script>top.tb_remove()</script>");
        }

        /// <summary>
        /// 生成 Json 格式的返回值
        /// </summary>
        /// <param name="statu">状态</param>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <param name="backurl">跳转URL</param>
        /// <returns></returns>
        public ActionResult RedirectAjax(string statu, string msg, object data, string backurl)
        {
            //JsonResult res = new JsonResult();
            //res.Data = new { Statu = statu, Msg = msg, Data = data, BackUrl = backurl };
            //return res;
            return Json(new { Statu = statu, Msg = msg, Data = data, BackUrl = backurl }, JsonRequestBehavior.AllowGet);
        }
    }
}