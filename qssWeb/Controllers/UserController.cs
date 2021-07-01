using System;
using System.Linq;
using System.Web.Mvc;
using qssWeb.Models;
using qssWeb.Utils;

namespace qssWeb.Controllers
{
    public class UserController : Controller
    {
        private QssDBContext db = new QssDBContext();
        // GET
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                Response.Redirect("/User/Login");
            }
            ViewData["userName"] = Session["userName"];
            ViewData["wenjuans"] = Wenjuan.GetWenjuanInfosByUserId(int.Parse(Session["userId"].ToString()));
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var userName = form["userName"];
            var password = form["password"];

            Console.WriteLine(Crypt.SHA_256(password).Substring(32));

            var user = (db.Users.OrderBy(u => u.userId).Where(u => u.userName == userName)).SingleOrDefault();

            if (user == null)
                ViewData["loginMsg"] = "<font color='red'>用户不存在!</font>";
            else if (user.password != Crypt.SHA_256(password).ToLower().Substring(32))
            {
                ViewData["loginMsg"] = "<font color='red'>用户名或密码错误!</font>";
                ViewData["userName"] = userName;
            }
            else
            {
                Session["userId"] = user.userId;
                Session["userName"] = user.userName;
                Response.Redirect("/User");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Response.Redirect("/User/Login");
            return new ContentResult();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            var userName = form["userName"];
            var password = form["password"];
            var password2 = form["password2"];
            var user = (db.Users.OrderBy(u => u.userId).Where(u => u.userName == userName)).SingleOrDefault();
            if (user != null)
                ViewData["loginMsg"] = "<font color='red'>该用户已存在!</font>";
            else
            {
                if (password != password2)
                {
                    ViewData["loginMsg"] = "<font color='red'>两次密码输入不一致!</font>";
                    ViewData["userName"] = userName;
                }
                else
                {
                    if (password == null || userName == null)
                        ViewData["loginMsg"] = "<font color='red'>用户名和密码不能为空!</font>";
                    else
                    {
                        var script = "<script type=\"text/javascript\">" +
                                     "alert('注册成功!');" +
                                     "window.location.href=\"Login\";" +
                                     "</script>";
                        var cryptedPass = Crypt.SHA_256(password).ToLower().Substring(32);
                        user = new User();
                        user.userName = userName;
                        user.password = cryptedPass;
                        db.Users.Add(user);
                        db.SaveChanges();
                        ViewData["userName"] = userName;
                        ViewData["script"] = script;
                        var content=new ContentResult();
                        content.Content = script;
                    }
                }
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (Session["userName"] != null)
            {
                ViewData["userName"] = Session["userName"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            var userName = form["userName"];
            var oldPassword = form["oldPassword"];
            var password = form["password"];
            var password2 = form["password2"];
            var user = (db.Users.OrderBy(u => u.userId).Where(u => u.userName == userName)).SingleOrDefault();

            if (user == null)
            {
                ViewData["loginMsg"] = "<font color='red'>用户不存在!</font>";
                return View();
            }
                
            
            if (!user.password.Equals(Crypt.SHA_256(oldPassword).ToLower().Substring(32)))
            {
                ViewData["loginMsg"] = "<font color='red'>用户名或密码错误!</font>";
                ViewData["userName"] = userName;
                return View();
            }
            if (password != password2)
            {
                ViewData["loginMsg"] = "<font color='red'>两次密码输入不一致!</font>";
                ViewData["userName"] = userName;
            }
            else
            {
                if (password == null || userName == null)
                    ViewData["loginMsg"] = "<font color='red'>用户名和密码不能为空!</font>";
                else
                {
                    var script = "<script type=\"text/javascript\">" +
                                 "alert('密码修改成功');" +
                                 "window.location.href=\"Login\";" +
                                 "</script>";
                    var cryptedPass = Crypt.SHA_256(password).ToLower().Substring(32);
                    user.password = cryptedPass;
                    db.SaveChanges();
                    ViewData["userName"] = userName;
                    ViewData["script"] = script;
                    var content=new ContentResult();
                    content.Content = script;
                }
            }
            return View();
        }   
    }
}