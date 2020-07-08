using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public int ToUserID { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }


}
