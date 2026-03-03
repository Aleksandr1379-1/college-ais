using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class ViewModel : PageModel
{
    private readonly CollegeDbContext _context;

    public ViewModel(CollegeDbContext context)
    {
        _context = context;
    }

    public Applicant Applicant { get; private set; } = null!;
    public ApplicantPassport? Passport { get; private set; }
    public ApplicantAddress? Address { get; private set; }
    public ApplicantEducationDocument? Education { get; private set; }
    public ApplicantParentContact? Parent { get; private set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {

        Applications = await _context.ApplicantApplications
    .Where(x => x.ApplicantId == id)
    .Include(x => x.Program)
    .OrderBy(x => x.Priority)
    .ToListAsync();
    
        Applicant = await _context.Applicants
            .FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception();

        Passport = await _context.ApplicantPassports
            .FirstOrDefaultAsync(x => x.ApplicantId == id);

        Address = await _context.ApplicantAddresses
            .FirstOrDefaultAsync(x => x.ApplicantId == id);

        Education = await _context.ApplicantEducationDocuments
            .FirstOrDefaultAsync(x => x.ApplicantId == id);

        Parent = await _context.ApplicantParentContacts
            .FirstOrDefaultAsync(x => x.ApplicantId == id);

        return Page();
    }
    public List<ApplicantApplication> Applications { get; private set; } = new();
}