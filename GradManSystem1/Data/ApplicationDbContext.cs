using GradManSystem1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GradManSystem1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Student> Student { get; set; }
        public DbSet<Proffesor> Proffesor { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SendMailDto> SendMailDto { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<GradManSystem1.Models.Proffesor> Proffesor { get; set; }
        //public DbSet<GradManSystem1.Models.Student> Student { get; set; }
    }
}