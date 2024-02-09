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
    public class ImgTbsController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public ImgTbsController(AllHomeEaseContext context)
        {
            _context = context;
        }

        // GET: ImgTbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImgTbs.ToListAsync());
        }

        // GET: ImgTbs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgTb = await _context.ImgTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imgTb == null)
            {
                return NotFound();
            }

            return View(imgTb);
        }

        // GET: ImgTbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImgTbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImpSize,Name,Type")] ImgTb imgTb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imgTb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imgTb);
        }

        // GET: ImgTbs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgTb = await _context.ImgTbs.FindAsync(id);
            if (imgTb == null)
            {
                return NotFound();
            }
            return View(imgTb);
        }

        // POST: ImgTbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ImpSize,Name,Type")] ImgTb imgTb)
        {
            if (id != imgTb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imgTb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImgTbExists(imgTb.Id))
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
            return View(imgTb);
        }

        // GET: ImgTbs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imgTb = await _context.ImgTbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imgTb == null)
            {
                return NotFound();
            }

            return View(imgTb);
        }

        // POST: ImgTbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var imgTb = await _context.ImgTbs.FindAsync(id);
            if (imgTb != null)
            {
                _context.ImgTbs.Remove(imgTb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImgTbExists(long id)
        {
            return _context.ImgTbs.Any(e => e.Id == id);
        }
    }
}
