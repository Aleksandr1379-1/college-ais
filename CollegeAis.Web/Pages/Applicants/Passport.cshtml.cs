using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class PassportModel : PageModel
{
    private readonly CollegeDbContext _context;

    public PassportModel(CollegeDbContext context)
    {
        _context = context;
    }

    public Applicant Applicant { get; private set; } = null!;

    [BindProperty(SupportsGet = true)]
    public Guid ApplicantId { get; set; }

    [BindProperty]
    public ApplicantPassport Passport { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        var passport = await _context.ApplicantPassports.FirstOrDefaultAsync(p => p.ApplicantId == id);
        Passport = passport ?? new ApplicantPassport { ApplicantId = id };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        // ✅ Не валидируем навигацию EF (она не приходит из формы)
        ModelState.Remove("Passport.Applicant");

        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.ApplicantPassports.FirstOrDefaultAsync(p => p.ApplicantId == ApplicantId);

        if (existing is null)
        {
            Passport.ApplicantId = ApplicantId;
            _context.ApplicantPassports.Add(Passport);
        }
        else
        {
            existing.Series = Passport.Series;
            existing.Number = Passport.Number;
            existing.IssuedBy = Passport.IssuedBy;
            existing.IssueDate = Passport.IssueDate;
            existing.DivisionCode = Passport.DivisionCode;
            existing.PlaceOfBirth = Passport.PlaceOfBirth;
            existing.Citizenship = Passport.Citizenship;
            existing.Inn = Passport.Inn;
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }
}