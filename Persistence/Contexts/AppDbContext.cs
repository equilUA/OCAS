using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using ActivityAcme.API.Domain.Models;

namespace ActivityAcme.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Domain.Models.Activity> Activitys { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Activity>().ToTable("Activitys");
            builder.Entity<Activity>().HasKey(p => p.Id);
            builder.Entity<Activity>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
            builder.Entity<Activity>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Activity>().HasMany(p => p.Employees).WithOne(p => p.Activity).HasForeignKey(p => p.ActivityId);

            builder.Entity<Activity>().HasData
            (
                new Activity { Id = 100, Name = "Soccer" }, // Id set manually due to in-memory provider
                new Activity { Id = 101, Name = "Poker" }
            );

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Employee>().Property(p => p.Email).IsRequired();
            builder.Entity<Employee>().Property(p => p.Comments);

            builder.Entity<Employee>().HasData
            (
                new Employee
                {
                    Id = 100,
                    FirstName = "Kirill",
                    LastName = "Golovan",
                    Email = "Kirill.Golovan@gmail.com",
                    Comments = "I love soccer",
                    ActivityId = 100
                },
                new Employee
                {
                    Id = 101,
                    FirstName = "Test",
                    LastName = "Lastname",
                    Email = "test@gmail.com",
                    Comments = "I love poker",
                    ActivityId = 101,
                }
            );
        }
    }
}