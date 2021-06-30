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
                    UserInfo[0].Login_time = DateTime.Now.ToLocalTime().ToString();
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

                User.password = Md5String(User.password);
                User.balance = 0;
                User.Creation_time = DateTime.Now.ToLocalTime().ToString();
                User.Login_nums = 0;
                User.Login_time = "0";
                GameDB.User.Add(User);
                if (GameDB.SaveChanges() != 0)
                {
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
            foreach (var item in GameDB.Config.ToList())
            {
                ViewData[item.name] = item.value;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Seting(FormCollection Form)
        {

            var GameDB = new gameEntities();
            var Title = new List<Config>();
            foreach (string item in Form)
            {
                Title = GameDB.Config.Where(s => s.name == item).ToList();
                Title[0].value = Form[item];
            }
            GameDB.SaveChanges();


            foreach (var item in GameDB.Config.ToList())
            {
                ViewData[item.name] = item.value;
            }

            ViewBag.regok = "ok";
            return View();
        }

        public ActionResult UserList()
        {

            var GameDB = new gameEntities();
            var user_list = GameDB.User.ToList();
            ViewBag.user_list = user_list;
            return View();
        }

        [HttpPost]
        public ActionResult pay(FormCollection Form)
        {

            var GameDB = new gameEntities();
            int id = int.Parse(Form["id"]);
            var user_info = GameDB.User.Find(id);
            user_info.balance = user_info.balance + int.Parse(Form["mun"]);

            var PayLog = new PayLog();
            PayLog.uid = id;
            PayLog.amount = int.Parse(Form["mun"]);
            PayLog.username = user_info.username;
            PayLog.total = user_info.balance;
            PayLog.type = "管理员操作";
            PayLog.Creation_time = DateTime.Now.ToLocalTime().ToString();
            GameDB.PayLog.Add(PayLog);
            GameDB.SaveChanges();

            return Json(new { balance = user_info.balance,msg = 0 }, JsonRequestBehavior.AllowGet);;;
        }

        [HttpPost]
        public ActionResult changepass(FormCollection Form)
        {
            var GameDB = new gameEntities();
            int id = int.Parse(Form["id"]);
            var user_info = GameDB.User.Find(id);
            user_info.password = Md5String(Form["pass"]);
            GameDB.SaveChanges();
            return Json(new { msg = 0 }, JsonRequestBehavior.AllowGet); ; ;
        }


        public ActionResult userlog()
        {
            var GameDB = new gameEntities();
            var Userlog = GameDB.Userlog.ToList();
            ViewBag.Userlog = Userlog;
            return View();
        }


        public ActionResult paylog()
        {

            var GameDB = new gameEntities();
            var paylog = GameDB.PayLog.ToList();
            ViewBag.paylog = paylog;
            return View();
        }


    }
}
