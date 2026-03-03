using System.ComponentModel.DataAnnotations;
using CollegeAis.Data.Enums;

namespace CollegeAis.Data.Entities;

public class Applicant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Поле «{0}» обязательно для заполнения.")]
    [MaxLength(100, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    public string LastName { get; set; } = "";

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Поле «{0}» обязательно для заполнения.")]
    [MaxLength(100, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    public string FirstName { get; set; } = "";

    [Display(Name = "Отчество")]
    [MaxLength(100, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    public string? MiddleName { get; set; }

    [Display(Name = "СНИЛС")]
    [MaxLength(50, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    [RegularExpression(@"^[0-9\-\s]{11,14}$", ErrorMessage = "Поле «{0}» заполнено неверно. Пример: 123-456-789 00")]
    public string? Snils { get; set; }

    [Display(Name = "Дата рождения")]
    [Required(ErrorMessage = "Поле «{0}» обязательно для заполнения.")]
    public DateOnly BirthDate { get; set; }

    [Display(Name = "Пол")]
    [Required(ErrorMessage = "Поле «{0}» обязательно для заполнения.")]
    [MaxLength(1, ErrorMessage = "Поле «{0}» заполнено неверно.")]
    public string Gender { get; set; } = "М"; // "М" / "Ж"

    [Display(Name = "Телефон")]
    [MaxLength(30, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    public string? Phone { get; set; }

    [Display(Name = "Email")]
    [MaxLength(200, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    [EmailAddress(ErrorMessage = "Поле «{0}» заполнено неверно.")]
    public string? Email { get; set; }

    [Display(Name = "Адрес")]
    [MaxLength(400, ErrorMessage = "Поле «{0}» не должно превышать {1} символов.")]
    public string? Address { get; set; }

    [Display(Name = "Статус")]
    public ApplicantStatus Status { get; set; } = ApplicantStatus.Draft;

    [Display(Name = "Создано")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Обновлено")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}