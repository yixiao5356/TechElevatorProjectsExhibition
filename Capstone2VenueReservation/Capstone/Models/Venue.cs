using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string Location { get; set; }
        public string category { get; set; } = "";
    }
}
