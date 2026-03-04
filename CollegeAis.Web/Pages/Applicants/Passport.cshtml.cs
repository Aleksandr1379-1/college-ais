using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public List<SelectListItem> CitizenshipOptions { get; private set; } = new();

    private async Task BuildCitizenshipOptionsAsync()
    {
        CitizenshipOptions = await _context.Countries
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToListAsync();

        CitizenshipOptions.Insert(0, new SelectListItem("— выбери —", ""));
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        var passport = await _context.ApplicantPassports.FirstOrDefaultAsync(p => p.ApplicantId == id);
        Passport = passport ?? new ApplicantPassport { ApplicantId = id };

        await BuildCitizenshipOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        // ✅ Навигации EF не приходят из формы — убираем из проверки
        ModelState.Remove("Passport.Applicant");
        ModelState.Remove("Passport.CitizenshipCountry");

        if (!ModelState.IsValid)
        {
            await BuildCitizenshipOptionsAsync();
            return Page();
        }

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

            // ✅ сохраняем FK страны
            existing.CitizenshipCountryId = Passport.CitizenshipCountryId;

            existing.Inn = Passport.Inn;
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }
}