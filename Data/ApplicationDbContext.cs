using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies().UseSqlServer("Server =(localdb)\\MSSQLLocalDB; Database = Vezeeta; Trusted_Connection = true;" +
                                 " Integrated Security = True;Trust Server Certificate=False;");
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentHour> AppointmentHours { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }
    }
}