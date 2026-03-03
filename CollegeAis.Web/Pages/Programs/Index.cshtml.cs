using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Programs;

public class IndexModel : PageModel
{
    private readonly CollegeDbContext _context;

    public IndexModel(CollegeDbContext context)
    {
        _context = context;
    }

    public IList<ProgramEntity> Programs { get; set; } = new List<ProgramEntity>();

    public async Task OnGetAsync()
    {
        Programs = await _context.Programs
            .OrderBy(x => x.Code)
            .ToListAsync();
    }
}