using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AzureGitHubDemo
{
    public static class ConfManager
    {
        public static string AdoNetConnStr { get; set; }
    }
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AzureGitHubDemoDbContext : DbContext
    {
        public AzureGitHubDemoDbContext()
        {

        }

        public AzureGitHubDemoDbContext(DbContextOptions<AzureGitHubDemoDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>(x=>x.ToTable(nameof(Skill)));

            modelBuilder.Entity<Employee>(x => x.HasData(new Employee[]{
                new Employee{ Id = 1,Name = "Name-101"},
                new Employee{ Id = 2,Name = "Name-102"},
                new Employee{ Id = 3,Name = "Name-103"},
            }));

            modelBuilder.Entity<Department>(x => x.HasData(new Department[]{
                new Department{ Id = 1,Name = "Department-101"},
                new Department{ Id = 2,Name = "Department-102"},
                new Department{ Id = 3,Name = "Department-103"},
            }));

            modelBuilder.Entity<Skill>(x => x.HasData(new Skill[]{
                new Skill{ Id = 1,Name = "Skill-1"},
                new Skill{ Id = 2,Name = "Skill-2"},
                new Skill{ Id = 3,Name = "Skill-3"},
            }));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
