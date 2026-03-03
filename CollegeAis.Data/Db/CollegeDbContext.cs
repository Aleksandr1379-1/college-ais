using CollegeAis.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Data.Db;

public class CollegeDbContext : DbContext
{
    public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) { }

    public DbSet<Applicant> Applicants => Set<Applicant>();
    public DbSet<ProgramEntity> Programs => Set<ProgramEntity>();
    public DbSet<ApplicantPassport> ApplicantPassports => Set<ApplicantPassport>();
    public DbSet<ApplicantAddress> ApplicantAddresses => Set<ApplicantAddress>();
    public DbSet<ApplicantEducationDocument> ApplicantEducationDocuments => Set<ApplicantEducationDocument>();
    public DbSet<ApplicantParentContact> ApplicantParentContacts => Set<ApplicantParentContact>();
    public DbSet<ApplicantApplication> ApplicantApplications => Set<ApplicantApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>()
            .HasIndex(x => new { x.LastName, x.FirstName, x.BirthDate });
        modelBuilder.Entity<ProgramEntity>()
            .HasIndex(x => x.Code)
            .IsUnique();    
            modelBuilder.Entity<Applicant>()
    .HasOne(a => a.Passport)
    .WithOne(p => p.Applicant)
    .HasForeignKey<ApplicantPassport>(p => p.ApplicantId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<Applicant>()
    .HasOne(a => a.AddressInfo)
    .WithOne(ad => ad.Applicant)
    .HasForeignKey<ApplicantAddress>(ad => ad.ApplicantId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<Applicant>()
    .HasOne(a => a.EducationDocument)
    .WithOne(ed => ed.Applicant)
    .HasForeignKey<ApplicantEducationDocument>(ed => ed.ApplicantId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<Applicant>()
    .HasOne(a => a.ParentContact)
    .WithOne(pc => pc.Applicant)
    .HasForeignKey<ApplicantParentContact>(pc => pc.ApplicantId)
    .OnDelete(DeleteBehavior.Cascade);

modelBuilder.Entity<ApplicantApplication>()
    .HasIndex(x => new { x.ApplicantId, x.Priority })
    .IsUnique();
    }
}