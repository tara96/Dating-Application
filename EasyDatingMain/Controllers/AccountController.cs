using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyDatingMain.Models;
using System.Web.Security;
using System.Security.Principal;
using System.Security.Claims;

namespace EasyDatingMain.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                List<String> userNames = new List<String>();

                using (var db = new OurDbContext())
                {
                    var result = from u in db.userAccount
                                 select u.Username;

                    foreach (var item in result)
                    {
                        userNames.Add(item);
                    }
                }
                foreach (var item in userNames)
                {
                    if (item == account.Username)
                    {

                        ModelState.AddModelError("Username", "Användarnamnet är redan upptaget");
                        return View(account);

                    }
                }
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Name + " successfully registered.";
            }
            return View(account);
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            try
            {
                using (OurDbContext db = new OurDbContext())
                {

                    var usr = db.userAccount.Single(u => u.Username == user.Username && u.Password == user.Password);
                    if (usr != null)
                    {
                        Session["UserID"] = usr.ID.ToString();
                        Session["Username"] = usr.Username.ToString();
                        Session["Mail"] = usr.Mail.ToString();
                        Session["Name"] = usr.Name.ToString();
                        Session["Description"] = usr.Description.ToString();
                        Session["age"] = usr.age.ToString();


                        var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, Session["Username"].ToString()),
                        new Claim(ClaimTypes.Email, Session["Mail"].ToString()),
                        }, "ApplicationCookie");

                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;
                        authenticationManager.SignIn(identity);
                        return RedirectToAction("LoggedIn", usr);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is wrong.");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Username and password do not match.");
            }

            return View(user);

        }

        public ActionResult LoggedIn(UserAccount user)
        {

            if (Session["UserId"] != null)
            {
                using(var db = new OurDbContext())
                {
                    var id = Convert.ToInt32(Session["UserID"]);
                    var name = db.userAccount.Single(u => u.ID == id);
                    var thisUser = name.Username.ToString();
                    var currentUser = System.Web.HttpContext.Current.User.Identity.Name.ToString();
                    if (thisUser == currentUser)
                    {
                        return View(name);
                    }
                    return RedirectToAction("Login");
                }
      
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Account");
        }


        public ActionResult EditProfile()
        {
            try
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;

                using (var db = new OurDbContext())
                {

                    UserAccount user = new UserAccount();

                    var result = from u in db.userAccount
                                 where u.Username == username
                                 select u;

                    foreach (var u in result)
                    {
                        user.ProfilePicture = u.ProfilePicture;
                        user.Username = u.Username;
                        user.Description = u.Description;
                        user.age = u.age;
                        user.Visible = u.Visible;
                    }

                    return View(user);
                }

            }
            catch
            {
                return RedirectToAction("Login", "Account");

            }
        }

        [HttpPost]
        public ActionResult EditProfile(UserAccount UserProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var username = System.Web.HttpContext.Current.User.Identity.Name;

                    if (UsernameAvailable(UserProfile.Username) || UserProfile.Username == username)
                    {

                        using (var db = new OurDbContext())
                        {
                            UserAccount u = db.userAccount.FirstOrDefault(x => x.Username == username);

                            u.Username = UserProfile.Username;
                            u.Description = UserProfile.Description;
                            u.Visible = UserProfile.Visible;
                            u.age = UserProfile.age;

                            if (!string.IsNullOrWhiteSpace(UserProfile.Password))
                            {
                                u.Password = UserProfile.Password;
                                u.Confirm_Password = UserProfile.Password;
                            }

                            db.SaveChanges();
                        }

                        //Loggar ut och loggar in användaren på nytt för att hantera eventuellt byte av användarnamn,
                        //då användarnamnet som användes vid inlogg är det som finns sparat i cookien. 

                        FormsAuthentication.SignOut();
                        FormsAuthentication.SetAuthCookie(UserProfile.Username, false);


                        return RedirectToAction("LoggedIn", "Account", UserProfile);


                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Användarnamnet är redan upptaget.");
                        return View(UserProfile);
                    }
                }
                else
                {
                    return View(UserProfile);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool UsernameAvailable(string username)
        {
            var usernames = new List<String>();
            bool available = false;
            using (var db = new OurDbContext())
            {
                var result = from u in db.userAccount
                             select u.Username;

                foreach (var u in result)
                {
                    usernames.Add(u);
                }

            }

            foreach (var u in usernames)
            {
                if (u == username)
                {
                    available = false;
                    break;
                }
                else
                {
                    available = true;
                }
            }

            return available;

        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file, UserAccount usr)
        {
            if (file != null && file.ContentLength > 0)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/picture"), pic);

                //Filen laddas upp
                file.SaveAs(path);
                UserAccount user;
                using (OurDbContext db = new OurDbContext())
                {
                    var CurrentUser = User.Identity.Name;

                    user = db.userAccount.FirstOrDefault(x => x.Username == CurrentUser);
                    user.ProfilePicture = pic;
                    db.SaveChanges();
                }
                return RedirectToAction("LoggedIn", "Account", user);
            }
            return RedirectToAction("EditProfile", "Account", usr);
        }

        public ActionResult ShowUser(string username)
        {
            try
            {
                if (username != System.Web.HttpContext.Current.User.Identity.Name)
                {
                    using (var db = new OurDbContext())
                    {

                        UserAccount user = new UserAccount();

                        var result = from u in db.userAccount
                                     where u.Username == username
                                     select u;

                        foreach (var u in result)
                        {
                            user.Username = u.Username;
                            user.Name = u.Name;
                            user.age = u.age;
                            user.Description = u.Description;
                            user.ProfilePicture = u.ProfilePicture;
                        }
                        return View(user);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {

                return RedirectToAction("Index", "Home");

            }
        }

    }
}