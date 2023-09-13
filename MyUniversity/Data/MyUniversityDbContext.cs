using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyUniversity.Models;
using System.Reflection.Metadata;

namespace MyUniversity.Data
{
    public class MyUniversityDbContext : DbContext
    {
        public MyUniversityDbContext()
        {
            Database.EnsureCreated();
        }
        public MyUniversityDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Groups)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Students)
                .WithOne(e => e.Group)
                .HasForeignKey(e => e.GroupId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
