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
    public class UserTbsController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public UserTbsController(AllHomeEaseContext context)
        {
            _context = context;
        }

        // GET: UserTbs
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

            return View(await _context.UserTbs.ToListAsync());
        }

        // GET: UserTbs/Details/5
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

            var userTb = await _context.UserTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTb == null)
            {
                return NotFound();
            }

            return View(userTb);
        }

        // GET: UserTbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserTbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,HouseNo,Pincode,State,Street,Dob,Email,FirstName,LastName,Password,Phone,Role")] UserTb userTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTb);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index","Home");
            }
            return View(userTb);
        }

        // GET: UserTbs/Edit/5
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

            var userTb = await _context.UserTbs.FindAsync(id);
            if (userTb == null)
            {
                return NotFound();
            }
            return View(userTb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,City,HouseNo,Pincode,State,Street,Dob,Email,FirstName,LastName,Password,Phone,Role")] UserTb userTb)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            if (id != userTb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTbExists(userTb.Id))
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
            return View(userTb);
        }

        // GET: UserTbs/Delete/5
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

            var userTb = await _context.UserTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTb == null)
            {
                return NotFound();
            }

            return View(userTb);
        }

        // POST: UserTbs/Delete/5
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
            var userTb = await _context.UserTbs.FindAsync(id);
            if (userTb != null)
            {
                _context.UserTbs.Remove(userTb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTbExists(long id)
        {
            return _context.UserTbs.Any(e => e.Id == id);
        }
    }
}
