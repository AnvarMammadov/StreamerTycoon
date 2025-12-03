namespace StreamerTycoon.Models
{
    public class MarketItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Məs: "Valorant Account"
        public string Type { get; set; } = "Account"; // Account, Skin, Service
        public double Price { get; set; }
        public string SellerName { get; set; } = string.Empty;
        public string IconPath { get; set; } = ""; // SVG Path data olacaq
        public string RiskLevel { get; set; } = "";
    }
}
