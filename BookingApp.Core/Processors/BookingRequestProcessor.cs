using BookingApp.Core.DataServices;
using BookingApp.Core.DataServices.Domain;
using BookingApp.Core.Enums;
using BookingApp.Core.Models;

namespace BookingApp.Core.Processors
{
    public class BookingRequestProcessor : IBookingRequestProcessor
    {
        private readonly IBookingService _bookingService;

        public BookingRequestProcessor(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public BookingResult BookRoom(BookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            //_bookingService.Save(new Booking
            //{
            //    FullName = bookingRequest.FullName,
            //    Date = bookingRequest.Date,
            //    Email = bookingRequest.Email,
            //});

            var availableRooms = _bookingService.GetAvailableRooms(bookingRequest.Date);
            var result = CreateBookingObject<BookingResult>(bookingRequest);

            if(availableRooms.Any()) // availableRooms.Count() > 0
            {
                var room = availableRooms.First();
                var roomBooking = CreateBookingObject<Booking>(bookingRequest);
                roomBooking.RoomId = room.Id;
                _bookingService.Save(roomBooking);

                result.RoomBookingId = roomBooking.Id;

                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }

            //return new BookingResult
            //{
            //    FullName = bookingRequest.FullName,
            //    Date = bookingRequest.Date,
            //    Email = bookingRequest.Email,
            //};
            return result;
        }

        private TBooking CreateBookingObject<TBooking>(BookingRequest bookingRequest) where TBooking : BookingBase, new()
        {
            return new TBooking
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email,
            };
        }


    }
    
}