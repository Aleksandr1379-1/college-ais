using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class EducationModel : PageModel
{
    private readonly CollegeDbContext _context;

    public EducationModel(CollegeDbContext context) => _context = context;

    public Applicant Applicant { get; private set; } = null!;

    [BindProperty(SupportsGet = true)]
    public Guid ApplicantId { get; set; }

    [BindProperty]
    public ApplicantEducationDocument Edu { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        var entity = await _context.ApplicantEducationDocuments.FirstOrDefaultAsync(x => x.ApplicantId == id);
        Edu = entity ?? new ApplicantEducationDocument { ApplicantId = id };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        ModelState.Remove("Edu.Applicant");

        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.ApplicantEducationDocuments.FirstOrDefaultAsync(x => x.ApplicantId == ApplicantId);

        if (existing is null)
        {
            Edu.ApplicantId = ApplicantId;
            _context.ApplicantEducationDocuments.Add(Edu);
        }
        else
        {
            existing.DocType = Edu.DocType;
            existing.Kind = Edu.Kind;
            existing.FinishedClasses = Edu.FinishedClasses;
            existing.Series = Edu.Series;
            existing.Number = Edu.Number;
            existing.IssueDate = Edu.IssueDate;
            existing.IssuedBy = Edu.IssuedBy;
            existing.AverageScore = Edu.AverageScore;
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }
}