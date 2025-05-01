using CozyHavenStay.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace CozyHavenStay.Contexts
{
    public class CozyHavenStayContext : DbContext
    {

        public CozyHavenStayContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<HotelManager> HotelManagers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Admin - User (One-to-One)
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithOne(u => u.Admin)
                .HasForeignKey<Admin>(a => a.Email)
                .HasPrincipalKey<User>(u => u.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer - User (One-to-One)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.Email)
                .HasPrincipalKey<User>(u => u.Email)
                .OnDelete(DeleteBehavior.Cascade);

            // HotelManager - User (One-to-One)
            modelBuilder.Entity<HotelManager>()
                .HasOne(h => h.User)
                .WithOne(u => u.HotelManager)
                .HasForeignKey<HotelManager>(h => h.Email)
                .HasPrincipalKey<User>(u => u.Email)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
               .HasOne(h => h.HotelManager)
               .WithOne(m => m.Hotel)
               .HasForeignKey<Hotel>(h => h.HotelManagerId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Room>()
               .HasOne(r => r.Hotel)
               .WithMany(h => h.Rooms)
               .HasForeignKey(r => r.HotelId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
               .HasOne(r => r.Hotel)
               .WithMany(h => h.Reviews)
               .HasForeignKey(r => r.HotelId)
               .OnDelete(DeleteBehavior.Restrict);

            // Booking - Room
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // changed from Cascade to Restrict

            // Booking - Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // keep cascade for customer

            // Payment - Booking
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade path

            // Refund - Payment
            modelBuilder.Entity<Refund>()
                .HasOne(r => r.Payment)
                .WithMany()
                .HasForeignKey(r => r.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade path

            modelBuilder.Entity<Document>()
            .HasOne(d => d.Customer)
            .WithMany()  // A customer can have many documents
            .HasForeignKey(d => d.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete if a customer is deleted
        }
    }
}
