using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommunityPantryPortal.Data;
using CommunityPantryPortal.Models;

namespace CommunityPantryPortal.Controllers;

public class PantriesController : Controller
{
    private readonly ApplicationDbContext _context;
    public PantriesController(ApplicationDbContext context) => _context = context;

    [AllowAnonymous]
    public async Task<IActionResult> Index(string? q, string? state, string? suburb)
    {
        var query = _context.Pantries.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p =>
                p.Name.Contains(q) ||
                p.Suburb.Contains(q) ||
                p.Address.Contains(q) ||
                p.Description.Contains(q));
        }

        if (!string.IsNullOrWhiteSpace(state))
            query = query.Where(p => p.State == state);

        if (!string.IsNullOrWhiteSpace(suburb))
            query = query.Where(p => p.Suburb == suburb);

        ViewBag.States = await _context.Pantries.Select(p => p.State).Distinct().OrderBy(x => x).ToListAsync();
        ViewBag.Suburbs = await _context.Pantries.Select(p => p.Suburb).Distinct().OrderBy(x => x).ToListAsync();
        ViewBag.Q = q;
        ViewBag.State = state;
        ViewBag.Suburb = suburb;

        return View(await query.OrderBy(p => p.Name).ToListAsync());
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var pantry = await _context.Pantries.FirstOrDefaultAsync(p => p.Id == id);
        if (pantry == null) return NotFound();
        return View(pantry);
    }

    [Authorize]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create([Bind("Name,State,Suburb,Address,PantryType,OpenHours,Phone,Description,ImageUrl")] Pantry pantry)
    {
        if (!ModelState.IsValid) return View(pantry);

        _context.Pantries.Add(pantry);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Pantry registered successfully.";
        return RedirectToAction(nameof(Index));
    }
}