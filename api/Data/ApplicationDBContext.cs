using System.Collections.Generic;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Servicee> Servicees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SalaryService> SalaryServices { get; set; }
        public DbSet<ProjectService> ProjectServices { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure composite keys and relationships
            _ = builder.Entity<SalaryService>()
                .HasKey(ss => new { ss.AppUserId, ss.ServiceeId });

            _ = builder.Entity<SalaryService>()
                .HasOne(ss => ss.AppUser)
                .WithMany(au => au.SalaryServices)
                .HasForeignKey(ss => ss.AppUserId);

            _ = builder.Entity<SalaryService>()
                .HasOne(ss => ss.Servicee)
                .WithMany(se => se.SalaryServices)
                .HasForeignKey(ss => ss.ServiceeId);

            _ = builder.Entity<ProjectService>()
                .HasKey(ps => new { ps.ProjectId, ps.ServiceeId });

            _ = builder.Entity<ProjectService>()
                .HasOne(ps => ps.Project)
                .WithMany(p => p.ProjectServices)
                .HasForeignKey(ps => ps.ProjectId);

            _ = builder.Entity<ProjectService>()
                .HasOne(ps => ps.Servicee)
                .WithMany(s => s.ProjectServices)
                .HasForeignKey(ps => ps.ServiceeId);

            // Seed roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Salesperson", NormalizedName = "SALESPERSON" },
                new IdentityRole { Name = "Employee", NormalizedName = "EMPLOYEE" }
            };
            _ = builder.Entity<IdentityRole>().HasData(roles);
        }
    }

    // Design-time factory for ApplicationDBContext
    public class ApplicationDBContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            _ = optionsBuilder.UseSqlServer("Server=sqlserver,1433;Database=crm;User Id=sa;Password=NewPassword!2024;");

            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }
}
