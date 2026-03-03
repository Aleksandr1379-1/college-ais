using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class AddressModel : PageModel
{
    private readonly CollegeDbContext _context;

    public AddressModel(CollegeDbContext context) => _context = context;

    public Applicant Applicant { get; private set; } = null!;

    [BindProperty(SupportsGet = true)]
    public Guid ApplicantId { get; set; }

    [BindProperty]
    public ApplicantAddress Address { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        var entity = await _context.ApplicantAddresses.FirstOrDefaultAsync(x => x.ApplicantId == id);
        Address = entity ?? new ApplicantAddress { ApplicantId = id };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        // Не валидируем навигацию EF
        ModelState.Remove("Address.Applicant");

        if (!ModelState.IsValid)
            return Page();

        var existing = await _context.ApplicantAddresses.FirstOrDefaultAsync(x => x.ApplicantId == ApplicantId);

        if (existing is null)
        {
            Address.ApplicantId = ApplicantId;
            _context.ApplicantAddresses.Add(Address);
        }
        else
        {
            existing.RegistrationAddress = Address.RegistrationAddress;
            existing.ActualAddress = Address.ActualAddress;
            existing.NeedsDormitory = Address.NeedsDormitory;
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }
}