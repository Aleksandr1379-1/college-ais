using CollegeAis.Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants.Countries;

public class DeleteModel : PageModel
{
    private readonly CollegeDbContext _context;

    public DeleteModel(CollegeDbContext context) => _context = context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public string CountryName { get; private set; } = "";
    public int UsedCount { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        if (country is null) return NotFound();

        CountryName = country.Name;
        UsedCount = await _context.ApplicantPassports.CountAsync(p => p.CitizenshipCountryId == Id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == Id);
        if (country is null) return NotFound();

        var used = await _context.ApplicantPassports.AnyAsync(p => p.CitizenshipCountryId == Id);
        if (used)
        {
            // Нельзя удалить — покажем понятное сообщение
            CountryName = country.Name;
            UsedCount = await _context.ApplicantPassports.CountAsync(p => p.CitizenshipCountryId == Id);
            ModelState.AddModelError("", "Нельзя удалить: страна уже используется в паспортах.");
            return Page();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}