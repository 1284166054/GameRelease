using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 游戏发布站.Models;
namespace 游戏发布站.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var GameDB = new gameEntities();
            foreach (var item in GameDB.Config.ToList())
            {
                ViewData[item.name] = item.value;
            }
            ViewBag.list = GameDB.AdList;
            if (ViewData["type"].ToString() == "0")
            {
                //return new HttpNotFoundResult("此网站暂时关闭");
                return Content("此网站暂时关闭");
            }

            return View();
        }
        public ActionResult Verification()
        {
            return View();
        }

    }
}