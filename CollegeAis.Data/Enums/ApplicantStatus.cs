namespace CollegeAis.Data.Enums;

public enum ApplicantStatus
{
    Draft = 0,              // Черновик
    Submitted = 1,          // Подал документы
    InRating = 2,           // В рейтинге
    Enrolled = 3,           // Зачислен (студент)
    Withdrawn = 4,          // Забрал документы
    ExcludedFromRating = 5  // Не включать в рейтинг
}