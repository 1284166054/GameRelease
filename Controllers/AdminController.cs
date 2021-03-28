using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 游戏发布站.Models;
using System.Web.UI;

namespace 游戏发布站.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            if (Session["uid"] == null)
            {
                return View("Login");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User User)
        {

            var GameDB = new gameEntities();
            var UserInfo = GameDB.User.Where(s => s.username == User.username).ToList();

            if(UserInfo.Count == 0)
            {
                ModelState.AddModelError("", "用户名不存在");
                return View();
            }
            else
            {
                if(UserInfo[0].password == User.password)
                {
                    Session["uid"] = UserInfo[0].id;
                    //return RedirectToAction("Index");
                    return Redirect("dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或者密码错误");
                    return View();
                }
                
            }

            
        }
        public ActionResult Reg()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reg(User User)
        {
            var GameDB = new gameEntities();
            var UserNum = GameDB.User.Where(s => s.username == User.username).Count();
            if(UserNum != 0)
            {

                ModelState.AddModelError("", "用户名已经存在");
                // return Json(new { code = 0, msg = "用户名已经存在" });
                return View();
            }
            else
            {
                long Ctimg = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                User.balance = 0;
                User.Creation_time = Convert.ToString(Ctimg);
                User.Login_nums = 0;
                User.Login_time = "0";
                GameDB.User.Add(User);
                if (GameDB.SaveChanges() != 0)
                {
                    // return Json(new { code = 1, msg = "账号注册成功" });
                    //return View("Login");
                    //Response.Cookies.Add(new HttpCookie("username"){ Value = User.username,Expires = DateTime.Now.AddDays(1) });
                    //Response.Cookies.Add(new HttpCookie("password") { Value = User.password,Expires = DateTime.Now.AddDays(1) });
                    //return RedirectToAction("Login");
                    ViewBag.regok = "ok";
                    return View();
                }
                else
                {
                    //return Json(new { code = 0, msg = "账号注册失败" });
                   ModelState.AddModelError("", "账号注册失败");
                   return View();
                }

            }
        }

    }
}
