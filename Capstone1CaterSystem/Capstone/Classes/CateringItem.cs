using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item
        public string ItemNumber { set; get; }
        public string ItemName { set;  get; }
        public double ItemPrice { set;  get; }
        public string ItemType { set;  get; }
        public int ItemAmount { set;  get; } = 50;

        public string TimeChange { get; set; }

    }
}
