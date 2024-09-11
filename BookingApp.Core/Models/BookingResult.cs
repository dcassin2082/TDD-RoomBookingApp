using BookingApp.Core.Enums;

namespace BookingApp.Core.Models
{
    public class BookingResult : BookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }     
    }
}