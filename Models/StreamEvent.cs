using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTycoon.Models
{
    public class StreamEvent
    {
        public string Message { get; set; } // Məs: "Anvar $50 atdı!"
        public string Type { get; set; }    // "Donation", "Sub", "Follow"
        public string Color { get; set; }   // Hadisənin rəngi
    }
}
