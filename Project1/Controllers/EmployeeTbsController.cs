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
    public class EmployeeTbsController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public EmployeeTbsController(AllHomeEaseContext context)
        {
            _context = context;
        }

        // GET: EmployeeTbs
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
            var allHomeEaseContext = _context.EmployeeTbs.Include(e => e.Service);
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


        // GET: EmployeeTbs/Details/5
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

            var employeeTb = await _context.EmployeeTbs
                .Include(e => e.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeTb == null)
            {
                return NotFound();
            }

            return View(employeeTb);
        }

        // GET: EmployeeTbs/Create
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
            ViewData["Serviceid"] = new SelectList(_context.ServiceTbs, "Id", "Id");
            return View();
        }

        // POST: EmployeeTbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DeptName,EmpStatus,FirstName,HireDate,LastName,PhoneNum,Salary,Serviceid")] EmployeeTb employeeTb)
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
                _context.Add(employeeTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Serviceid"] = new SelectList(_context.ServiceTbs, "Id", "Id", employeeTb.Serviceid);
            return View(employeeTb);
        }

        // GET: EmployeeTbs/Edit/5
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

            var employeeTb = await _context.EmployeeTbs.FindAsync(id);
            if (employeeTb == null)
            {
                return NotFound();
            }
            ViewData["Serviceid"] = new SelectList(_context.ServiceTbs, "Id", "Id", employeeTb.Serviceid);
            return View(employeeTb);
        }

        // POST: EmployeeTbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DeptName,EmpStatus,FirstName,HireDate,LastName,PhoneNum,Salary,Serviceid")] EmployeeTb employeeTb)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id != employeeTb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTbExists(employeeTb.Id))
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
            ViewData["Serviceid"] = new SelectList(_context.ServiceTbs, "Id", "Id", employeeTb.Serviceid);
            return View(employeeTb);
        }

        // GET: EmployeeTbs/Delete/5
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

            var employeeTb = await _context.EmployeeTbs
                .Include(e => e.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeTb == null)
            {
                return NotFound();
            }

            return View(employeeTb);
        }

        // POST: EmployeeTbs/Delete/5
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
            var employeeTb = await _context.EmployeeTbs.FindAsync(id);
            if (employeeTb != null)
            {
                _context.EmployeeTbs.Remove(employeeTb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTbExists(long id)
        {
            return _context.EmployeeTbs.Any(e => e.Id == id);
        }
    }
}
