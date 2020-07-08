using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    class API_TransferRequest
    {
        public int ToUserID { get; set; }
        public decimal Amount { get; set; }
    }
}
