using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTycoon.Models
{
    public class ChatMessage
    {
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ColorHex { get; set; } = "#FFFFFF"; // Mesaj rəngi
        public bool IsDonation { get; set; }
        public double DonationAmount { get; set; }
    }
}
