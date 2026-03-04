using CollegeAis.Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Web.Pages.Programs;

public class EditModel : PageModel
{
    private readonly CollegeDbContext _context;

    public EditModel(CollegeDbContext context) => _context = context;

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Укажи код")]
        [MaxLength(20, ErrorMessage = "Слишком длинный код")]
        public string Code { get; set; } = "";

        [Required(ErrorMessage = "Укажи название")]
        [MaxLength(250, ErrorMessage = "Слишком длинное название")]
        public string Name { get; set; } = "";

        [MaxLength(200, ErrorMessage = "Слишком длинное значение")]
        public string? Qualification { get; set; }

        [MaxLength(200, ErrorMessage = "Слишком длинное значение")]
        public string? BaseEducation { get; set; }

        [MaxLength(50, ErrorMessage = "Слишком длинное значение")]
        public string? StudyDuration { get; set; }

        [Range(0, 9999, ErrorMessage = "Введите число от 0 до 9999")]
        public int BudgetSeats { get; set; }

        [Range(0, 9999, ErrorMessage = "Введите число от 0 до 9999")]
        public int PaidSeats { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var p = await _context.Programs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (p is null) return NotFound();

        Input = new InputModel
        {
            Code = p.Code,
            Name = p.Name,
            Qualification = p.Qualification,
            BaseEducation = p.BaseEducation,
            StudyDuration = p.StudyDuration,
            BudgetSeats = p.BudgetSeats,
            PaidSeats = p.PaidSeats
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var p = await _context.Programs.FirstOrDefaultAsync(x => x.Id == Id);
        if (p is null) return NotFound();

        var code = Input.Code.Trim();
        var name = Input.Name.Trim();

        // уникальность кода
        var exists = await _context.Programs
            .AnyAsync(x => x.Id != Id && x.Code.ToLower() == code.ToLower());

        if (exists)
        {
            ModelState.AddModelError("Input.Code", "Специальность с таким кодом уже есть");
            return Page();
        }

        p.Code = code;
        p.Name = name;
        p.Qualification = string.IsNullOrWhiteSpace(Input.Qualification) ? null : Input.Qualification.Trim();
        p.BaseEducation = string.IsNullOrWhiteSpace(Input.BaseEducation) ? null : Input.BaseEducation.Trim();
        p.StudyDuration = string.IsNullOrWhiteSpace(Input.StudyDuration) ? null : Input.StudyDuration.Trim();
        p.BudgetSeats = Input.BudgetSeats;
        p.PaidSeats = Input.PaidSeats;

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}