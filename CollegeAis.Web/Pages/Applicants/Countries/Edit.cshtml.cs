using CollegeAis.Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Web.Pages.Applicants.Countries;

public class EditModel : PageModel
{
    private readonly CollegeDbContext _context;

    public EditModel(CollegeDbContext context) => _context = context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Укажи название")]
        [MaxLength(150, ErrorMessage = "Слишком длинное название")]
        public string Name { get; set; } = "";
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var country = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        if (country is null) return NotFound();

        Input = new InputModel { Name = country.Name };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == Id);
        if (country is null) return NotFound();

        var name = Input.Name.Trim();

        var exists = await _context.Countries
            .AnyAsync(x => x.Id != Id && x.Name.ToLower() == name.ToLower());

        if (exists)
        {
            ModelState.AddModelError("Input.Name", "Такая страна уже есть");
            return Page();
        }

        country.Name = name;
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}