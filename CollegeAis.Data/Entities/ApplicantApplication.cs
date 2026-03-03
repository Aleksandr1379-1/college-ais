using System.ComponentModel.DataAnnotations;

namespace CollegeAis.Data.Entities;

public enum StudyForm
{
    FullTime,        // очная
    PartTime         // очно-заочная
}

public enum FundingBasis
{
    Budget,
    Paid
}

public class ApplicantApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;

    [Required]
    public Guid ProgramId { get; set; }
    public ProgramEntity Program { get; set; } = null!;

    [Range(1, 3)]
    public int Priority { get; set; } = 1;

    public StudyForm StudyForm { get; set; } = StudyForm.FullTime;
    public FundingBasis FundingBasis { get; set; } = FundingBasis.Budget;
}