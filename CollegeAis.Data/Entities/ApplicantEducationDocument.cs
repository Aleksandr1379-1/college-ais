using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public enum EducationDocType
{
    Basic9,          // Аттестат 9
    Secondary11,     // Аттестат 11
    SpoProfession,   // Диплом СПО (профессия)
    SpoSpecialty,    // Диплом СПО (специальность)
    Npo              // Диплом НПО
}

public enum DocumentKind
{
    Original,
    Copy
}

public class ApplicantEducationDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;

    [Display(Name = "Тип документа")]
    public EducationDocType DocType { get; set; }

    [Display(Name = "Окончено классов")]
    public int? FinishedClasses { get; set; } // 9 или 11

    [Display(Name = "Серия документа")]
    [MaxLength(50)]
    public string? Series { get; set; }

    [Display(Name = "Номер документа")]
    [MaxLength(50)]
    public string? Number { get; set; }

    [Display(Name = "Дата выдачи")]
    public DateOnly? IssueDate { get; set; }

    [Display(Name = "Кем выдан")]
    [MaxLength(250)]
    public string? IssuedBy { get; set; }

    [Display(Name = "Вид (оригинал/копия)")]
    public DocumentKind Kind { get; set; }

    [Display(Name = "Средний балл")]
    [Range(0, 5, ErrorMessage = "Поле «{0}» должно быть от {1} до {2}.")]
    public decimal? AverageScore { get; set; } // 0.00–5.00 (если нужно 5-балльная)
}