using CollegeAis.Data.Db;
using CollegeAis.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Web.Pages.Applicants;

public class ApplicationsModel : PageModel
{
    private readonly CollegeDbContext _context;

    public ApplicationsModel(CollegeDbContext context) => _context = context;

    public Applicant Applicant { get; private set; } = null!;

    [BindProperty(SupportsGet = true)]
    public Guid ApplicantId { get; set; }

    public List<SelectListItem> ProgramOptions { get; private set; } = new();

    [BindProperty]
    public List<ApplicationRow> Rows { get; set; } = new();

    public class ApplicationRow
    {
        public Guid? ProgramId { get; set; }
        public StudyForm StudyForm { get; set; } = StudyForm.FullTime;
        public FundingBasis FundingBasis { get; set; } = FundingBasis.Budget;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        ApplicantId = id;

        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        await LoadProgramsAsync();

        // Готовим 3 строки
        Rows = new List<ApplicationRow>
        {
            new(), new(), new()
        };

        // Подгружаем существующие приоритеты
        var existing = await _context.ApplicantApplications
            .Where(x => x.ApplicantId == id)
            .OrderBy(x => x.Priority)
            .ToListAsync();

        foreach (var app in existing)
        {
            if (app.Priority is < 1 or > 3) continue;
            Rows[app.Priority - 1] = new ApplicationRow
            {
                ProgramId = app.ProgramId,
                StudyForm = app.StudyForm,
                FundingBasis = app.FundingBasis
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Id == ApplicantId);
        if (applicant is null) return NotFound();
        Applicant = applicant;

        await LoadProgramsAsync();

        // Базовая проверка: список должен быть 3 строки
        if (Rows.Count != 3)
        {
            ModelState.AddModelError("", "Некорректные данные формы.");
            return Page();
        }

        // Проверка на дубликаты специальностей (игнорируем пустые)
        var selected = Rows
            .Where(r => r.ProgramId.HasValue)
            .Select(r => r.ProgramId!.Value)
            .ToList();

        if (selected.Count != selected.Distinct().Count())
        {
            ModelState.AddModelError("", "Нельзя выбирать одну и ту же специальность в нескольких приоритетах.");
            return Page();
        }

        if (!ModelState.IsValid)
            return Page();

        // Существующие записи из БД (приоритеты 1..3)
        var existing = await _context.ApplicantApplications
            .Where(x => x.ApplicantId == ApplicantId && x.Priority >= 1 && x.Priority <= 3)
            .ToListAsync();

        for (var priority = 1; priority <= 3; priority++)
        {
            var row = Rows[priority - 1];
            var dbRow = existing.FirstOrDefault(x => x.Priority == priority);

            if (!row.ProgramId.HasValue)
            {
                // Если пользователь очистил строку — удаляем запись, если была
                if (dbRow is not null)
                    _context.ApplicantApplications.Remove(dbRow);

                continue;
            }

            if (dbRow is null)
            {
                _context.ApplicantApplications.Add(new ApplicantApplication
                {
                    ApplicantId = ApplicantId,
                    ProgramId = row.ProgramId.Value,
                    Priority = priority,
                    StudyForm = row.StudyForm,
                    FundingBasis = row.FundingBasis
                });
            }
            else
            {
                dbRow.ProgramId = row.ProgramId.Value;
                dbRow.StudyForm = row.StudyForm;
                dbRow.FundingBasis = row.FundingBasis;
            }
        }

        applicant.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return RedirectToPage("Card", new { id = ApplicantId });
    }

    private async Task LoadProgramsAsync()
    {
        var programs = await _context.Programs
            .OrderBy(p => p.Code)
            .ThenBy(p => p.Name)
            .ToListAsync();

        ProgramOptions = programs
            .Select(p => new SelectListItem($"{p.Code} — {p.Name}", p.Id.ToString()))
            .ToList();
    }
}