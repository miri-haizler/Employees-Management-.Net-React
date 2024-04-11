using Employee.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data
{
    public class DataContext : DbContext
    {
        public DbSet<EmployeeDetails> Employees { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<RoleEmployee> RolesEmployees { get; set; } = default!;

        //DATA SEED
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDetails>().HasIndex(e => e.Tz).IsUnique();
            modelBuilder.Entity<RoleEmployee>().HasIndex(e => new {e.RoleId, e.EmployeeDetailsId}).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<RoleEmployee>().HasIndex(r => new {r.RoleId, r.EmployeeDetailsId}).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=my_db");
        }
    }
}
