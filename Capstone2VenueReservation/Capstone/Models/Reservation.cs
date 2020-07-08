using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Space { get; set; }
        public string Venue { get; set; }
        public int NumberOfAttendee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReserveFor { get; set; }
        public decimal Total { get; set; }

    }
}
