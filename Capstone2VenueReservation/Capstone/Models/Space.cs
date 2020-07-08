using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public decimal DailyRate { get; set; }
        public int MaxOccupancy { get; set; }
        public string Accessible { get; set; }
    }
}
