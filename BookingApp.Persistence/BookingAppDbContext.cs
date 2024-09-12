using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain;

namespace BookingApp.Persistence
{
    public class BookingAppDbContext : DbContext
    {
        public BookingAppDbContext(DbContextOptions<BookingAppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Conference Room A" },
                new Room { Id = 2, Name = "Conference Room B" },
                new Room { Id = 3, Name = "Conference Room C" }
            );
        }

    }
}
