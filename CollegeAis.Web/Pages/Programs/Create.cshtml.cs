using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CollegeAis.Web.Pages.Programs;

public class CreateModel : PageModel
{
    private readonly CollegeDbContext _context;

    public CreateModel(CollegeDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ProgramEntity Program { get; set; } = new();

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Небольшая нормализация
        Program.Code = Program.Code.Trim();
        Program.Name = Program.Name.Trim();

        _context.Programs.Add(Program);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            // Если включили уникальный индекс по Code, при дубле будет ошибка.
            ModelState.AddModelError(string.Empty, "Не удалось сохранить. Возможно, такой код уже существует.");
            return Page();
        }

        return RedirectToPage("Index");
    }
}