using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants.Countries;

public class IndexModel : PageModel
{
    private readonly CollegeDbContext _context;

    public IndexModel(CollegeDbContext context) => _context = context;

    public List<CountryRow> Countries { get; private set; } = new();

    public class CountryRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int UsedCount { get; set; }
    }

    public async Task OnGetAsync()
    {
        // Сколько раз страна используется в паспортах
        Countries = await _context.Countries
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CountryRow
            {
                Id = x.Id,
                Name = x.Name,
                UsedCount = _context.ApplicantPassports.Count(p => p.CitizenshipCountryId == x.Id)
            })
            .ToListAsync();
    }
}