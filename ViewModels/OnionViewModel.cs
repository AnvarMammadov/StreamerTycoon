using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;
using StreamerTycoon.Services;

namespace StreamerTycoon.ViewModels
{
    public partial class OnionViewModel : AppWindowViewModel
    {
        // DarkMarketItem modelindən istifadə edirik
        public ObservableCollection<DarkMarketItem> HackingTools { get; set; }

        // Konsol çıxışı üçün log
        public ObservableCollection<string> ConsoleLog { get; set; } = new ObservableCollection<string>();

        public OnionViewModel() : base("OnionCore", "Icon_Thor")
        {
            HackingTools = new ObservableCollection<DarkMarketItem>
            {
                new DarkMarketItem { Name = "Combo List (10k Emails)", Price = 200, Description = "10.000 unikal email:şifrə cütü." },
                new DarkMarketItem { Name = "BruteForce Tool v5.0", Price = 500, Description = "Hesabları sındırmaq üçün avtomatlaşdırılmış proqram." },
                new DarkMarketItem { Name = "Fake ID Generator", Price = 150, Description = "Saxta sənədlər yaratmaq üçün şablonlar." }
            };

            ConsoleLog.Add($"[{DateTime.Now.ToString("HH:mm:ss")}] TOR circuit established. Ready for transaction.");
        }

        [RelayCommand]
        public void BuyTool(DarkMarketItem item)
        {
            // Bütün əşyalar MarketItem kimi inventara əlavə olunur
            if (GameManager.Instance.TryBuyItem(new MarketItem
            {
                Title = item.Name,
                Price = item.Price,
                Type = "Tool",
                SellerName = "DarkWeb Vendor",
                RiskLevel = "Extreme"
            }))
            {
                ConsoleLog.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] SUCCESS: {item.Name} purchased for ${item.Price:N0}. Check Inventory.");
                HackingTools.Remove(item);
            }
            else
            {
                ConsoleLog.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}] ERROR: Insufficient funds. Need ${item.Price:N0}.");
            }
            if (ConsoleLog.Count > 15) ConsoleLog.RemoveAt(ConsoleLog.Count - 1);
        }
    }
}
