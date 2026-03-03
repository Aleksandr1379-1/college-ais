using CollegeAis.Data.Enums;

namespace CollegeAis.Web.Helpers;

public static class ApplicantStatusExtensions
{
    public static string ToRu(this ApplicantStatus status) => status switch
    {
        ApplicantStatus.Draft => "Черновик",
        ApplicantStatus.Submitted => "Документы приняты",
        ApplicantStatus.InRating => "В рейтинге",
        ApplicantStatus.Enrolled => "Зачислен",
        ApplicantStatus.Withdrawn => "Забрал документы",
        ApplicantStatus.ExcludedFromRating => "Не включать в рейтинг",
        _ => status.ToString()
    };
}