using CrimeCaseManagement.Data;
using CrimeCaseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrimeCaseManagement.Controllers;

public class CasesController : Controller
{
    private readonly AppDbContext _context;
    public CasesController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? search)
    {
        var cases = _context.CrimeCases.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            cases = cases.Where(c => c.CaseNumber.Contains(search) || c.Title.Contains(search) || c.CrimeType.Contains(search));
        ViewData["Search"] = search;
        return View(await cases.OrderByDescending(c => c.IncidentDate).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.CrimeCases.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return item == null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new CrimeCase());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CaseNumber,Title,CrimeType,Location,IncidentDate,OfficerName,Status,Description")] CrimeCase item)
    {
        if (await _context.CrimeCases.AnyAsync(c => c.CaseNumber == item.CaseNumber))
            ModelState.AddModelError(nameof(item.CaseNumber), "This case number already exists.");
        if (!ModelState.IsValid) return View(item);
        _context.Add(item); await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.CrimeCases.FindAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,CaseNumber,Title,CrimeType,Location,IncidentDate,OfficerName,Status,Description")] CrimeCase item)
    {
        if (id != item.Id) return NotFound();
        if (await _context.CrimeCases.AnyAsync(c => c.CaseNumber == item.CaseNumber && c.Id != item.Id))
            ModelState.AddModelError(nameof(item.CaseNumber), "This case number already exists.");
        if (!ModelState.IsValid) return View(item);
        try { _context.Update(item); await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) { if (!await _context.CrimeCases.AnyAsync(c => c.Id == id)) return NotFound(); else throw; }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.CrimeCases.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.CrimeCases.FindAsync(id);
        if (item != null) { _context.CrimeCases.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}
