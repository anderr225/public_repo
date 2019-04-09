using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace WebApiCFT.Models
{
    public class CftContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public CftContext(DbContextOptions<CftContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set foreign key for Task.ProjectId
            modelBuilder.Entity<Task>().HasOne<Project>()
                .WithMany()
                .HasForeignKey(c => c.ProjectId);
            
            //unique name of task for every project
            modelBuilder.Entity<Task>()
                .HasIndex(p => new {p.Name, p.ProjectId}).IsUnique();

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Name).IsUnique();
        }   
    }
}