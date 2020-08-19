using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Company : IItemsForAdmin
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string NumberOfEmployees { get; set; }
        public int NumberOfGrads { get; set; }
        public string NamesOfGrads { get; set; }
        public decimal Rating { get; set; }
    }
}
