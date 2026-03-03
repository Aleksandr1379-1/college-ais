using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public class ProgramEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(20)]
    public string Code { get; set; } = ""; // 09.02.07

    [Required, MaxLength(300)]
    public string Name { get; set; } = ""; // Название специальности

    // Базовое образование (9/11) — у тебя это сейчас "Уровень"
    [Required, MaxLength(20)]
    public string BaseEducation { get; set; } = "9 классов";

    // ✅ Новые поля
    [Required, MaxLength(200)]
    public string Qualification { get; set; } = ""; // квалификация

    [Required, MaxLength(50)]
    public string StudyDuration { get; set; } = ""; // срок обучения (например "3 г. 10 мес.")

    public int BudgetPlaces { get; set; } // бюджетных мест

    public int PaidPlaces { get; set; } // коммерческих мест (если нужно)

    public bool IsActive { get; set; } = true;
}