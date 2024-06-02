using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HospitalSystem_FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace HospitalSystem_FinalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<Doctor> Doctors { get; set; } = default!;
        public DbSet<Hospital> Hospitals { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .ToTable(tb=>tb.UseSqlOutputClause(false))
                .HasOne(a => a.Hospital)
                .WithMany(h => h.Appointments)
                .HasForeignKey(a => a.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
