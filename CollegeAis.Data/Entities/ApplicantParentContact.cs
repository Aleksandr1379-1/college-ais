using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ApplicantParentContact
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;

    [Display(Name = "ФИО родителя/представителя")]
    [MaxLength(200)]
    public string? FullName { get; set; }

    [Display(Name = "Телефон родителя")]
    [MaxLength(30)]
    public string? Phone { get; set; }

    [Display(Name = "Статус родителя/представителя")]
    [MaxLength(50)]
    public string? Status { get; set; } // мать/отец/опекун и т.п.
}