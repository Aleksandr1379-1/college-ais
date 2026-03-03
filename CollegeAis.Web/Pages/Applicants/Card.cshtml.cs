using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CollegeAis.Data.Enums;

namespace CollegeAis.Web.Pages.Applicants;

public class CardModel : PageModel
{
    private readonly CollegeDbContext _context;

    public CardModel(CollegeDbContext context)
    {
        _context = context;
    }

    public Applicant Applicant { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();

        Applicant = applicant;
        return Page();
    }

    public async Task<IActionResult> OnPostChangeStatusAsync(Guid id, ApplicantStatus status)
{
    var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
    if (applicant == null) return NotFound();

    applicant.Status = status;
    applicant.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToPage(new { id });
}
}