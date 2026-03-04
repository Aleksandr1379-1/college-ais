using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ApplicantPassport
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }

    public Applicant Applicant { get; set; } = null!;

    [Display(Name = "Серия")]
    [MaxLength(4, ErrorMessage = "Серия должна быть не длиннее 4 символов")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Серия должна содержать ровно 4 цифры")]
    public string? Series { get; set; }

    [Display(Name = "Номер")]
    [MaxLength(6, ErrorMessage = "Номер должен быть не длиннее 6 символов")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Номер должен содержать ровно 6 цифр")]
    public string? Number { get; set; }

    [Display(Name = "Кем выдан")]
    [MaxLength(250)]
    public string? IssuedBy { get; set; }

    [Display(Name = "Дата выдачи")]
    public DateOnly? IssueDate { get; set; }

    [Display(Name = "Код подразделения")]
    [MaxLength(7, ErrorMessage = "Код подразделения должен быть в формате 000-000")]
    [RegularExpression(@"^\d{3}-\d{3}$", ErrorMessage = "Код подразделения должен быть в формате 000-000")]
    public string? DivisionCode { get; set; }

    // ✅ Гражданство через справочник
    [Display(Name = "Гражданство")]
    public int? CitizenshipCountryId { get; set; }

    public Country? CitizenshipCountry { get; set; }

    [Display(Name = "ИНН")]
    [MaxLength(20)]
    public string? Inn { get; set; }

    [Display(Name = "Место рождения")]
    [MaxLength(250)]
    public string? PlaceOfBirth { get; set; }
}