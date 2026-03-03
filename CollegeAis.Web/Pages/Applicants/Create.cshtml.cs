using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeAis.Web.Pages.Applicants;

public class CreateModel : PageModel
{
    private readonly CollegeDbContext _context;

    public CreateModel(CollegeDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Applicant Applicant { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Applicant.CreatedAt = DateTime.UtcNow;
        Applicant.UpdatedAt = DateTime.UtcNow;

        _context.Applicants.Add(Applicant);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}