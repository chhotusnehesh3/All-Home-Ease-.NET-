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
    public class OrdersTbsController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public OrdersTbsController(AllHomeEaseContext context)
        {
            _context = context;
        }

        // GET: OrdersTbs
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
            var allHomeEaseContext = _context.OrdersTbs.Include(o => o.Employee).Include(o => o.Service).Include(o => o.User);
            return View(await allHomeEaseContext.ToListAsync());
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
                return RedirectToAction("Index");
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

        // GET: OrdersTbs/Details/5
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

            var ordersTb = await _context.OrdersTbs
                .Include(o => o.Employee)
                .Include(o => o.Service)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordersTb == null)
            {
                return NotFound();
            }

            return View(ordersTb);
        }

        // GET: OrdersTbs/Create
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTbs, "Id", "Id");
            ViewData["ServiceId"] = new SelectList(_context.ServiceTbs, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.UserTbs, "Id", "Id");
            return View();
        }

        // POST: OrdersTbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bookingtime,Status,EmployeeId,ServiceId,UserId")] OrdersTb ordersTb)
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
                _context.Add(ordersTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTbs, "Id", "Id", ordersTb.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.ServiceTbs, "Id", "Id", ordersTb.ServiceId);
            ViewData["UserId"] = new SelectList(_context.UserTbs, "Id", "Id", ordersTb.UserId);
            return View(ordersTb);
        }

        // GET: OrdersTbs/Edit/5
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

            var ordersTb = await _context.OrdersTbs.FindAsync(id);
            if (ordersTb == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTbs, "Id", "Id", ordersTb.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.ServiceTbs, "Id", "Id", ordersTb.ServiceId);
            ViewData["UserId"] = new SelectList(_context.UserTbs, "Id", "Id", ordersTb.UserId);
            return View(ordersTb);
        }

        // POST: OrdersTbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Bookingtime,Status,EmployeeId,ServiceId,UserId")] OrdersTb ordersTb)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id != ordersTb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordersTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersTbExists(ordersTb.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeeTbs, "Id", "Id", ordersTb.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.ServiceTbs, "Id", "Id", ordersTb.ServiceId);
            ViewData["UserId"] = new SelectList(_context.UserTbs, "Id", "Id", ordersTb.UserId);
            return View(ordersTb);
        }

        // GET: OrdersTbs/Delete/5
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

            var ordersTb = await _context.OrdersTbs
                .Include(o => o.Employee)
                .Include(o => o.Service)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordersTb == null)
            {
                return NotFound();
            }

            return View(ordersTb);
        }

        // POST: OrdersTbs/Delete/5
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
            var ordersTb = await _context.OrdersTbs.FindAsync(id);
            if (ordersTb != null)
            {
                _context.OrdersTbs.Remove(ordersTb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersTbExists(long id)
        {
            return _context.OrdersTbs.Any(e => e.Id == id);
        }
    }
}
