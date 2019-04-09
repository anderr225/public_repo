using System;
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

//            modelBuilder.Entity<Project>().HasData(
//                new Project()
//                {
//                    Name = "Default_project_name",
//                    Description = "Default_project_description",
//                    CreationTime = DateTime.Now,
//                    LastModificationTime = DateTime.Now,
//                    Id = 1
//                });
//            modelBuilder.Entity<Task>().HasData(
//                new Task()
//                {
//                    Name = "Default_task_name",
//                    Description = "Default_task_description",
//                    CreationTime = DateTime.Now,
//                    LastModificationTime = DateTime.Now,
//                    Priority = 1,
//                    ProjectId = 1,
//                    Status = Status.New,
//                    Id = 1
//                });
        }
    }
}