using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }
        public DbSet<Lead>Leads { get; set; }
        public DbSet<Campaign>Campaigns { get; set; }
        public DbSet<Activity> Activities { get; set; }  
        public DbSet<Opportunity> Opportunities { get; set; }  
        public DbSet<Contact> Contacts { get; set; }  
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }  
        public DbSet<Servicee> Servicees { get; set; }
        public DbSet<Project> Projects { get; set; }  
        public DbSet<SalaryService> SalaryServices { get; set; }  


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}