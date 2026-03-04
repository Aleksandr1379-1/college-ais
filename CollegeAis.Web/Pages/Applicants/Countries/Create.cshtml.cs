using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Web.Pages.Applicants.Countries;

public class CreateModel : PageModel
{
    private readonly CollegeDbContext _context;

    public CreateModel(CollegeDbContext context) => _context = context;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Укажи название")]
        [MaxLength(150, ErrorMessage = "Слишком длинное название")]
        public string Name { get; set; } = "";
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var name = Input.Name.Trim();

        var exists = await _context.Countries
            .AnyAsync(x => x.Name.ToLower() == name.ToLower());

        if (exists)
        {
            ModelState.AddModelError("Input.Name", "Такая страна уже есть");
            return Page();
        }

        _context.Countries.Add(new Country { Name = name });
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}