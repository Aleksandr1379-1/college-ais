using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class IndexModel : PageModel
{
    private readonly CollegeDbContext _context;

    public IndexModel(CollegeDbContext context)
    {
        _context = context;
    }

    public IList<Applicant> Applicants { get; set; } = new List<Applicant>();

    public async Task OnGetAsync()
    {
        Applicants = await _context.Applicants
            .OrderBy(a => a.LastName)
            .ToListAsync();
    }
}