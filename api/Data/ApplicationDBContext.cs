using System.Collections.Generic;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                .HasKey(ss => new { ss.ProjectId, ss.ServiceeId });
                
            _ = builder.Entity<ProjectService>()
                .HasOne(ss => ss.Project)
                .WithMany(au => au.ProjectServices)
                .HasForeignKey(ss => ss.ProjectId);

            _ = builder.Entity<ProjectService>()
                .HasOne(ss => ss.Servicee) 
                .WithMany(se => se.ProjectServices)
                .HasForeignKey(ss => ss.ServiceeId);

            // Seed roles
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Salesperson",
                    NormalizedName = "SALESPERSON"
                },
                new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
            };
            _ = builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
