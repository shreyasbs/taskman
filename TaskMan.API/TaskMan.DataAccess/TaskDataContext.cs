using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using TaskMan.BusinessObjects;

namespace TaskMan.DataAccess
{
    public class TaskDataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TaskDataContext() : base() { }

        public TaskDataContext(DbContextOptions<TaskDataContext> options) : base(options) { }
        public DbSet<BusinessObjects.Task> Tasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SHREYASPC\\SQLEXPRESS01;Database=TaskManDb;Integrated Security=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BusinessObjects.Task>()
                .Property(p => p.Status).HasConversion(v => v.ToString(), v => (BusinessObjects.TaskStatus)Enum.Parse(typeof(BusinessObjects.TaskStatus), v));
            modelBuilder.Entity<BusinessObjects.Task>().Property(p => p.Id).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
