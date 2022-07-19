using System;
using Microsoft.EntityFrameworkCore;

namespace CSVQueries.Models
{
    public class ModelsContext: DbContext
    {
        public ModelsContext(DbContextOptions<ModelsContext> options) : base(options)
        { 
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
