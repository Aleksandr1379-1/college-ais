using System.ComponentModel.DataAnnotations;
using CollegeAis.Data.Enums;

namespace CollegeAis.Data.Entities;

public class Applicant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string LastName { get; set; } = "";

    [Required, MaxLength(12)]
    public string SNILS { get; set; } = "";

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = "";

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }

    [Required, MaxLength(1)]
    public string Gender { get; set; } = "М"; // "М"/"Ж"

    [MaxLength(30)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(400)]
    public string? Address { get; set; }

    public ApplicantStatus Status { get; set; } = ApplicantStatus.Draft;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}