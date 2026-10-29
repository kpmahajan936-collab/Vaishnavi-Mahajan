using CrimeCaseManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CrimeCaseManagement.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<CrimeCase> CrimeCases => Set<CrimeCase>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CrimeCase>().HasIndex(c => c.CaseNumber).IsUnique();
    }
}
