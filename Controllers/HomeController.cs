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
            ViewBag.Title = GameDB.Config.Where(s => s.name == "title").ToList()[0].value;
            ViewBag.Keywords = GameDB.Config.Where(s => s.name == "Keywords").ToList()[0].value;
            ViewBag.Description = GameDB.Config.Where(s => s.name == "Description").ToList()[0].value;
            ViewBag.list = GameDB.AdList;
            return View();
        }

    }
}