using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class ServiceTbsController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public ServiceTbsController(AllHomeEaseContext context)
        {
            _context = context;
        }

        // GET: ServiceTbs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View(await _context.ServiceTbs.ToListAsync());
        }

        // GET: ServiceTbs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var serviceTb = await _context.ServiceTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTb == null)
            {
                return NotFound();
            }

            return View(serviceTb);
        }

        // GET: ServiceTbs/Create
        public IActionResult Create()
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

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserTb user)
        {
            var myUser = _context.UserTbs.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                if (myUser.Email == "chhotusnehesh1@gmail.com")
                {
                    HttpContext.Session.SetString("UserSession", myUser.Email);
                    return RedirectToAction("Admin");
                }
                //storing sessionin Email
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (_context.UserTbs.Any(x => x.Email != user.Email))
                {
                    ViewBag.Message = "Invalid Email";
                }
                else if (_context.UserTbs.Any(y => y.Password != user.Password))
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

        // POST: ServiceTbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LongDesc,ServiceCharge,ServiceName,ServiceTax,ShortDesc")] ServiceTb serviceTb)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (ModelState.IsValid)
            {
                _context.Add(serviceTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceTb);
        }

        // GET: ServiceTbs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var serviceTb = await _context.ServiceTbs.FindAsync(id);
            if (serviceTb == null)
            {
                return NotFound();
            }
            return View(serviceTb);
        }

        // POST: ServiceTbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,LongDesc,ServiceCharge,ServiceName,ServiceTax,ShortDesc")] ServiceTb serviceTb)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id != serviceTb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTbExists(serviceTb.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(serviceTb);
        }

        // GET: ServiceTbs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var serviceTb = await _context.ServiceTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (serviceTb == null)
            {
                return NotFound();
            }

            return View(serviceTb);
        }

        // POST: ServiceTbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            var serviceTb = await _context.ServiceTbs.FindAsync(id);
            if (serviceTb != null)
            {
                _context.ServiceTbs.Remove(serviceTb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTbExists(long id)
        {

            return _context.ServiceTbs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AvailableServices()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View(await _context.ServiceTbs.ToListAsync());
        }
        public IActionResult DownloadImage(long id)
        {
            var img = _context.ImgTbs.Find(id);

            if (img != null && img.ImpSize != null && img.ImpSize.Length > 0)
            {
                string contentType = img.Type ?? "application/octet-stream";

                return File(img.ImpSize, contentType);
            }

            return File("~/images/placeholder.jpg", "image/jpeg"); 
           

        }

    }
}
