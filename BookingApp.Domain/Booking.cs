using BookingApp.Domain.BaseModels;

namespace BookingApp.Domain
{
    public class Booking : BookingBase
    {
        public int? Id { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
