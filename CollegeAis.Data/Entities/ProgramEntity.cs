using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ProgramEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Укажи код")]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажи название")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;

    // ✅ Вернули поля
    [Display(Name = "Квалификация")]
    [MaxLength(200)]
    public string? Qualification { get; set; }

    [Display(Name = "Базовое образование")]
    [MaxLength(200)]
    public string? BaseEducation { get; set; }

    [Display(Name = "Срок обучения")]
    [MaxLength(50)]
    public string? StudyDuration { get; set; }

    [Display(Name = "Количество бюджетных мест")]
    [Range(0, 9999, ErrorMessage = "Введите число от 0 до 9999")]
    public int BudgetSeats { get; set; }

    [Display(Name = "Количество платных мест")]
    [Range(0, 9999, ErrorMessage = "Введите число от 0 до 9999")]
    public int PaidSeats { get; set; }
}