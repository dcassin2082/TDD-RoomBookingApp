using BookingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.Processors
{
    public interface IBookingRequestProcessor
    {
        BookingResult BookRoom(BookingRequest bookingRequest);
    }
}
