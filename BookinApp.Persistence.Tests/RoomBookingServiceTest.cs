using BookingApp.Domain;
using BookingApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            // Arrange
            var date = new DateTime(2024, 09, 11);

            var dbOptions = new DbContextOptionsBuilder<BookingAppDbContext>().EnableSensitiveDataLogging()
                .UseInMemoryDatabase("AvailableRoomTest", b => b.EnableNullChecks(false)).Options;
            using var context = new BookingAppDbContext(dbOptions);
            context.Add(new Room { Id = 1, Name = "Room 1" });
            context.Add(new Room { Id = 2, Name = "Room 2" });
            context.Add(new Room { Id = 3, Name = "Room 3" });
            context.Add(new Booking { RoomId = 1, Date = date });
            context.Add(new Booking { RoomId = 2, Date = date.AddDays(-1) });
            context.SaveChanges();
            //context.Dispose();

            var roomBookingService = new RoomBookingService(context);

            // Act
            var availableRooms = roomBookingService.GetAvailableRooms(date);

            // Assert
            Assert.Equal(2, availableRooms.Count());
            Assert.Contains(availableRooms, q => q.Id == 2);
            Assert.Contains(availableRooms, q => q.Id == 3);
            Assert.DoesNotContain(availableRooms, q => q.Id == 1);
        }

        [Fact]
        public void ShouldSaveRoomBooking()
        {
            var dbOptions = new DbContextOptionsBuilder<BookingAppDbContext>().EnableSensitiveDataLogging()
               .UseInMemoryDatabase("AvailableRoomTest", b => b.EnableNullChecks(false)).Options;

            var roomBooking = new Booking { RoomId = 1, Date = new DateTime(2024, 09, 11) };

            using var context = new BookingAppDbContext(dbOptions);
            var roomBookingService = new RoomBookingService(context);
            roomBookingService.Save(roomBooking);

            var bookings = context.Bookings.ToList();
            var booking = Assert.Single(bookings);

            Assert.Equal(roomBooking.Date, booking.Date);
            Assert.Equal(roomBooking.RoomId, booking.RoomId);

        }

    }
}
