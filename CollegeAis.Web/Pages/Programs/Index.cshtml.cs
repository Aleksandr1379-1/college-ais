using CollegeAis.Data.Db;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Programs;

public class IndexModel : PageModel
{
    private readonly CollegeDbContext _context;

    public IndexModel(CollegeDbContext context) => _context = context;

    public List<ProgramRow> Programs { get; private set; } = new();

    public class ProgramRow
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Qualification { get; set; }
        public string? BaseEducation { get; set; }
        public string? StudyDuration { get; set; }
        public int BudgetSeats { get; set; }
        public int PaidSeats { get; set; }
        public int UsedCount { get; set; }
    }

    public async Task OnGetAsync()
    {
        Programs = await _context.Programs
            .AsNoTracking()
            .OrderBy(x => x.Code)
            .Select(x => new ProgramRow
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Qualification = x.Qualification,
                BaseEducation = x.BaseEducation,
                StudyDuration = x.StudyDuration,
                BudgetSeats = x.BudgetSeats,
                PaidSeats = x.PaidSeats,
                UsedCount = _context.ApplicantApplications.Count(a => a.ProgramId == x.Id)
            })
            .ToListAsync();
    }
}