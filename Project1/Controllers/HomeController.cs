using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
/*        private readonly ILogger<HomeController> _logger;*/
        private readonly AllHomeEaseContext context;

        public HomeController(AllHomeEaseContext context)
        {
            this.context = context;
        }
/*        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserTb user)
        {
            var myUser = context.UserTbs.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                if (myUser.Email == "chhotusnehesh1@gmail.com")
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);
                    return RedirectToAction("Admin");
                }
                //storing sessionin Email
                HttpContext.Session.SetString("UserSession", myUser.Email); 
                return RedirectToAction("Index");
            }
            else
            {
                if (context.UserTbs.Any(x => x.Email != user.Email))
                {
                    ViewBag.Message = "Invalid Email";
                }
                else if (context.UserTbs.Any(y => y.Password != user.Password))
                {
                    ViewBag.Message = "Invalid Password";
                }
                else
                {
                    ViewBag.Message = "Login Failed";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
