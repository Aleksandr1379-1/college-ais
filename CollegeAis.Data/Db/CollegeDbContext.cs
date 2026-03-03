using CollegeAis.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeAis.Data.Db;

public class CollegeDbContext : DbContext
{
    public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options) { }

    public DbSet<Applicant> Applicants => Set<Applicant>();
    public DbSet<ProgramEntity> Programs => Set<ProgramEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>()
            .HasIndex(x => new { x.LastName, x.FirstName, x.BirthDate });
        modelBuilder.Entity<ProgramEntity>()
            .HasIndex(x => x.Code)
            .IsUnique();    
    }
}