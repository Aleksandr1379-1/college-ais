using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Web.Pages.Programs;

public class CreateModel : PageModel
{
    private readonly CollegeDbContext _context;

    public CreateModel(CollegeDbContext context) => _context = context;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Укажи код")]
        [MaxLength(20)]
        public string Code { get; set; } = "";

        [Required(ErrorMessage = "Укажи название")]
        [MaxLength(250)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string? Qualification { get; set; }

        [MaxLength(200)]
        public string? BaseEducation { get; set; }

        [MaxLength(50)]
        public string? StudyDuration { get; set; }

        [Range(0, 9999)]
        public int BudgetSeats { get; set; }

        [Range(0, 9999)]
        public int PaidSeats { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var code = Input.Code.Trim();
        var name = Input.Name.Trim();

        var exists = await _context.Programs.AnyAsync(x => x.Code.ToLower() == code.ToLower());
        if (exists)
        {
            ModelState.AddModelError("Input.Code", "Специальность с таким кодом уже есть");
            return Page();
        }

        _context.Programs.Add(new ProgramEntity
        {
            Code = code,
            Name = name,
            Qualification = string.IsNullOrWhiteSpace(Input.Qualification) ? null : Input.Qualification.Trim(),
            BaseEducation = string.IsNullOrWhiteSpace(Input.BaseEducation) ? null : Input.BaseEducation.Trim(),
            StudyDuration = string.IsNullOrWhiteSpace(Input.StudyDuration) ? null : Input.StudyDuration.Trim(),
            BudgetSeats = Input.BudgetSeats,
            PaidSeats = Input.PaidSeats
        });

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}