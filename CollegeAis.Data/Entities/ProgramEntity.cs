using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ProgramEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(20)]
    public string Code { get; set; } = ""; // 09.02.07

    [Required, MaxLength(250)]
    public string Name { get; set; } = ""; // Информационные системы и программирование

    [MaxLength(50)]
    public string? EducationLevel { get; set; } // СПО / и т.п.

    public bool IsActive { get; set; } = true;
}