using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 游戏发布站.Filter
{
    public class AdminAuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

          

            //1、获取请求的类名和方法名
            string Controller_name = filterContext.RouteData.Values["controller"].ToString();
            string Action_name = filterContext.RouteData.Values["action"].ToString();
            if (Controller_name == "Admin")
            {
                if(Action_name != "Reg" && Action_name != "Login")
                {
                    if (HttpContext.Current.Session["uid"] == null)
                    {
                        //filterContext.HttpContext.Response.Write("控制器：" + Controller_name + "<br/>");
                        //filterContext.HttpContext.Response.Write("Action：" + Action_name + "<br/>");
                        //filterContext.HttpContext.Response.Write(HttpContext.Current.Session["uid"]);
                        filterContext.Result = new RedirectResult("/Login");
                        //base.OnAuthorization(filterContext);
                    }
                    else
                    {
                        var GameDB = new Models.gameEntities();
                        filterContext.Controller.ViewBag.UserInfo = GameDB.User.Find(HttpContext.Current.Session["uid"]);
                    }

                }
            }



        }
    }
}