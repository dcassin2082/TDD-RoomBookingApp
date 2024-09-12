using BookingApp.Core.Enums;
using BookingApp.Domain.BaseModels;

namespace BookingApp.Core.Models
{
    public class BookingResult : BookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }     
    }
}