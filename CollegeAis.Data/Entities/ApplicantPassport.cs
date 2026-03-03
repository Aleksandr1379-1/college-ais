using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ApplicantPassport
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }

    public Applicant Applicant { get; set; } = null!;

    [Display(Name = "Серия")]
    [MaxLength(10)]
    public string? Series { get; set; }

    [Display(Name = "Номер")]
    [MaxLength(10)]
    public string? Number { get; set; }

    [Display(Name = "Кем выдан")]
    [MaxLength(250)]
    public string? IssuedBy { get; set; }

    [Display(Name = "Дата выдачи")]
    public DateOnly? IssueDate { get; set; }

    [Display(Name = "Код подразделения")]
    [MaxLength(20)]
    public string? DivisionCode { get; set; }

    [Display(Name = "Гражданство")]
    [MaxLength(100)]
    public string? Citizenship { get; set; }

    [Display(Name = "ИНН")]
    [MaxLength(20)]
    public string? Inn { get; set; }

    [Display(Name = "Место рождения")]
    [MaxLength(250)]
    public string? PlaceOfBirth { get; set; }
}