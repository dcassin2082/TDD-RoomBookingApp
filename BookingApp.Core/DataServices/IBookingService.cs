using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Core.DataServices.Domain;

namespace BookingApp.Core.DataServices
{
    public interface IBookingService
    {
        void Save(Booking booking);
        IEnumerable<Room> GetAvailableRooms(DateTime date); 
    }
}
