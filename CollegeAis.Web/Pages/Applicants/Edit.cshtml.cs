using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class EditModel : PageModel
{
    private readonly CollegeDbContext _context;

    public EditModel(CollegeDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Applicant Applicant { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null)
            return NotFound();

        Applicant = applicant;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // EF будет отслеживать сущность после загрузки; но тут форма присылает объект,
        // поэтому явно помечаем как изменённый.
        Applicant.UpdatedAt = DateTime.UtcNow;

        _context.Attach(Applicant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Applicants.AnyAsync(a => a.Id == Applicant.Id))
                return NotFound();

            throw;
        }

        return RedirectToPage("Index");
    }
}
