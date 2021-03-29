using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 游戏发布站.Models;
using System.Web.UI;
using System.Security.Cryptography;
namespace 游戏发布站.Controllers
{

[Filter.AdminAuthorizeAttribute]

    public class AdminController : Controller
    {
        public static string Md5String(string str)
        {

            string pwd = String.Empty;

            MD5 md5 =MD5.Create();

            // 编码UTF8/Unicode　
            byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));

            // 转换成字符串
            for (int i = 0; i < s.Length; i++)
            {
                //格式后的字符是小写的字母
                //如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");

            }

            return pwd;
        }
        // GET: Admin
        public ActionResult Index()
        {

            if (Session["uid"] == null)
            {
                return Redirect("/Login");
            }
            else
            {
                //var GameDB = new gameEntities();
                //ViewBag.UserInfo = GameDB.User.Find(Session["uid"]);
                return View();
            }

        }

        public ActionResult Login()
        {
            if (Session["uid"] == null)
            {
                return View();
            }
            else
            {
                return Redirect("Dashboard");
            }
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
                User.password = Md5String(User.password);
                if (UserInfo[0].password == User.password)
                {
                 
                    Session["uid"] = UserInfo[0].id;
                    //return RedirectToAction("Index");
                    UserInfo[0].Login_nums += 1;
                    UserInfo[0].Login_time = Convert.ToString((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
                    GameDB.SaveChanges();
                    return Redirect("Dashboard");
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
                User.password = Md5String(User.password);
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

        public ActionResult Logout()
        {
            Session.Clear(); 
            return Redirect("Login");
        }

        public ActionResult Seting()
        {

            var GameDB = new gameEntities();
            ViewBag.Title = GameDB.Config.Where(s => s.name == "title").ToList()[0].value;
            ViewBag.Keywords = GameDB.Config.Where(s => s.name == "Keywords").ToList()[0].value;
            ViewBag.Description = GameDB.Config.Where(s => s.name == "Description").ToList()[0].value;
            return View();
        }

        [HttpPost]
        public ActionResult Seting(Config Config)
        {

            //var GameDB = new gameEntities();
            //ViewBag.Title = GameDB.Config.Where(s => s.name == "title").ToList()[0].value;
            //ViewBag.Keywords = GameDB.Config.Where(s => s.name == "Keywords").ToList()[0].value;
            //ViewBag.Description = GameDB.Config.Where(s => s.name == "Description").ToList()[0].value;
            return View();
        }

    }
}
