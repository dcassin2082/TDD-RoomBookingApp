using BookingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core.DataServices.Domain
{
    public class Booking : BookingBase
    {
        public int RoomId { get; set; }
        public int? Id { get; set; }
    }
}
