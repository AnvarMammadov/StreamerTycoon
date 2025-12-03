using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTycoon.Models
{
    public class DarkMarketItem
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty; // Məs: "BruteForce tool to crack weak passwords."
    }
}
