using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
}