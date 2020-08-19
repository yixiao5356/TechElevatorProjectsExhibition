using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class responseItem
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public responseItem(string text)
        {
            Sender = "Bot";
            Text = text;
        }
    }
}
