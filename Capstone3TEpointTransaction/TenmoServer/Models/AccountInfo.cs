using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class AccountInfo
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
