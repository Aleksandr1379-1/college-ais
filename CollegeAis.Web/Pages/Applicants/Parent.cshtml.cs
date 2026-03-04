using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeAis.Web.Pages.Applicants;

public class ParentModel : PageModel
{

    public List<SelectListItem> CitizenshipOptions { get; private set; } = new();
    
    private readonly CollegeDbContext _context;

    public ParentModel(CollegeDbContext context) => _context = context;

    public Applicant Applicant { get; private set; } = null!;

    [BindProperty(SupportsGet = true)]
    public Guid ApplicantId { get; set; }

    [BindProperty]
    public ApplicantParentContact Parent { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        var entity = await _context.ApplicantParentContacts.FirstOrDefaultAsync(x => x.ApplicantId == id);
        Parent = entity ?? new ApplicantParentContact { ApplicantId = id };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        ModelState.Remove("Parent.Applicant");

        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.ApplicantParentContacts.FirstOrDefaultAsync(x => x.ApplicantId == ApplicantId);

        if (existing is null)
        {
            Parent.ApplicantId = ApplicantId;
            _context.ApplicantParentContacts.Add(Parent);
        }
        else
        {
            existing.FullName = Parent.FullName;
            existing.Phone = Parent.Phone;
            existing.Status = Parent.Status;
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }
}