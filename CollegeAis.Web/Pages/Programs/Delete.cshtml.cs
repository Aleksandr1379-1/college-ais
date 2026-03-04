using CollegeAis.Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Programs;

public class DeleteModel : PageModel
{
    private readonly CollegeDbContext _context;

    public DeleteModel(CollegeDbContext context) => _context = context;

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public string Code { get; private set; } = "";
    public string Name { get; private set; } = "";
    public int UsedCount { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var p = await _context.Programs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (p is null) return NotFound();

        Code = p.Code;
        Name = p.Name;
        UsedCount = await _context.ApplicantApplications.CountAsync(a => a.ProgramId == Id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var p = await _context.Programs.FirstOrDefaultAsync(x => x.Id == Id);
        if (p is null) return NotFound();

        var used = await _context.ApplicantApplications.AnyAsync(a => a.ProgramId == Id);
        if (used)
        {
            Code = p.Code;
            Name = p.Name;
            UsedCount = await _context.ApplicantApplications.CountAsync(a => a.ProgramId == Id);

            ModelState.AddModelError("", "Нельзя удалить: специальность уже используется в заявлениях.");
            return Page();
        }

        _context.Programs.Remove(p);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}