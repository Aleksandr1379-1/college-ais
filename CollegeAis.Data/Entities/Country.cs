using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class Country
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;
}