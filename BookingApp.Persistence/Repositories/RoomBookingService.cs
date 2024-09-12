using BookingApp.Core.DataServices;
using BookingApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Repositories
{
    public class RoomBookingService : IBookingService
    {
        private readonly BookingAppDbContext _dbContext;

        public RoomBookingService(BookingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            return _dbContext.Rooms.Where(q => !q.RoomBookings.Any(x => x.Date == date)).ToList();

        }

        public void Save(Booking booking)
        {
            _dbContext.Add(booking);
            _dbContext.SaveChanges();
        }
    }
}
