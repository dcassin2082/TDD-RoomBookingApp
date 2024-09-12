using BookingApp.Domain;

namespace BookingApp.Core.DataServices
{
    public interface IBookingService
    {
        void Save(Booking booking);
        IEnumerable<Room> GetAvailableRooms(DateTime date); 
    }
}
