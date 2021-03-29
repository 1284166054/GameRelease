using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace 游戏发布站.Filter
{
    public class GlobalAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //1、获取请求的类名和方法名
            string Controller_name = filterContext.RouteData.Values["controller"].ToString();
            string Action_name = filterContext.RouteData.Values["action"].ToString();
            string Server_Name = filterContext.HttpContext.Request.Url.Host;

            //var request = filterContext.HttpContext.Request;
            //string url = request.Url.Authority;
            //string functionurl = request.RawUrl;
            //base.OnActionExecuting(filterContext);


            if (Action_name != "Verification")
            {
                //filterContext.HttpContext.Response.Write("控制器：" + Controller_name + "<br/>");
                if (Server_Name != "localhost")
                {
                    filterContext.Result = new RedirectResult("/Verification");
                }
            }
            //filterContext.HttpContext.Response.Write("控制器：" + Controller_name + "<br/>");
            //filterContext.HttpContext.Response.Write("Action：" + Action_name + "<br/>");
            //filterContext.HttpContext.Response.Write(HttpContext.Current.Session["uid"]);
            //filterContext.Result = new RedirectResult("/Login");
            //base.OnAuthorization(filterContext);

        }


    }
}