using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CollegeAis.Data.Enums;
using Microsoft.AspNetCore.Mvc;


namespace CollegeAis.Web.Pages.Applicants;

public class IndexModel : PageModel
{
    private readonly CollegeDbContext _context;

    public IndexModel(CollegeDbContext context) => _context = context;

    public IList<Applicant> Applicants { get; private set; } = new List<Applicant>();

    // ✅ Берём из query string ?q=...&status=...
    public string? Q { get; set; }
    public ApplicantStatus? Status { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AdmissionYearFilter { get; set; }

    public List<int> AdmissionYears { get; set; } = new();

    public async Task OnGetAsync(string? q, ApplicantStatus? status)
    {
        Q = q;
        Status = status;

        IQueryable<Applicant> query = _context.Applicants;

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();

            query = query.Where(a =>
                (a.LastName + " " + a.FirstName + " " + (a.MiddleName ?? "")).ToLower().Contains(q.ToLower()) ||
                (a.Phone ?? "").Contains(q) ||
                (a.Email ?? "").ToLower().Contains(q.ToLower())
            );
        }

        if (status.HasValue)
            query = query.Where(a => a.Status == status.Value);

        Applicants = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
        
        if (AdmissionYearFilter.HasValue)
{
    query = query.Where(a => a.AdmissionYear == AdmissionYearFilter.Value);
}

AdmissionYears = await _context.Applicants
    .Select(a => a.AdmissionYear)
    .Distinct()
    .OrderByDescending(y => y)
    .ToListAsync();
    }
    
    
}