using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ApplicantAddress
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;

    [Display(Name = "Адрес прописки")]
    [MaxLength(400)]
    public string? RegistrationAddress { get; set; }

    [Display(Name = "Фактический адрес")]
    [MaxLength(400)]
    public string? ActualAddress { get; set; }

    [Display(Name = "Нужно общежитие")]
    public bool NeedsDormitory { get; set; }
}