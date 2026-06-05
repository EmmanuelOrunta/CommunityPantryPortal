using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityPantryPortal.Data;
using CommunityPantryPortal.Models;
using Microsoft.AspNetCore.Authorization;


namespace CommunityPantryPortal.Controllers
{
    [Authorize]
    public class HelpRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HelpRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HelpRequests.Include(h => h.Pantry);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelpRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests
                .Include(h => h.Pantry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            return View(helpRequest);
        }

        // GET: HelpRequests/Create
    public IActionResult Create(int? pantryId)
    {
        ViewData["PantryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Pantries, "Id", "Name", pantryId);
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PantryId,RequestType,Notes,ContactEmail")] HelpRequest helpRequest)
    {
        if (ModelState.IsValid)
        {
            helpRequest.Status = "Pending";
            helpRequest.CreatedAt = DateTime.UtcNow;
            _context.Add(helpRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["PantryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Pantries, "Id", "Name", helpRequest.PantryId);
        return View(helpRequest);
    }


        // GET: HelpRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests.FindAsync(id);
            if (helpRequest == null)
            {
                return NotFound();
            }
            ViewData["PantryId"] = new SelectList(_context.Pantries, "Id", "Address", helpRequest.PantryId);
            return View(helpRequest);
        }

        // POST: HelpRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PantryId,RequestType,Notes,ContactEmail,Status,CreatedAt")] HelpRequest helpRequest)
        {
            if (id != helpRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(helpRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HelpRequestExists(helpRequest.Id))
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
            ViewData["PantryId"] = new SelectList(_context.Pantries, "Id", "Address", helpRequest.PantryId);
            return View(helpRequest);
        }

        // GET: HelpRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var helpRequest = await _context.HelpRequests
                .Include(h => h.Pantry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (helpRequest == null)
            {
                return NotFound();
            }

            return View(helpRequest);
        }

        // POST: HelpRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var helpRequest = await _context.HelpRequests.FindAsync(id);
            if (helpRequest != null)
            {
                _context.HelpRequests.Remove(helpRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HelpRequestExists(int id)
        {
            return _context.HelpRequests.Any(e => e.Id == id);
        }
    }
}
