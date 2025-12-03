using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;
using StreamerTycoon.Services;


namespace StreamerTycoon.ViewModels
{
    // Qısayol üçün sadə model
    public class BrowserShortcut
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string IconText { get; set; } // Məs: "G", "Y", "T"
        public string Color { get; set; }    // İkonun arxa fon rəngi
    }

    public partial class BrowserViewModel : AppWindowViewModel
    {
        [ObservableProperty] private string _addressBarText = "google.com";

        // Bu property View tərəfindən izlənəcək: "Home" və ya "Market"
        [ObservableProperty] private bool _isMarketVisible = false;

        public ObservableCollection<MarketItem> MarketItems { get; set; }
        public ObservableCollection<BrowserShortcut> Shortcuts { get; set; }

        public BrowserViewModel() : base("Chrome", "Icon_Web")
        {
            // Ana səhifədəki qısayollar (Screenshot-dakı kimi)
            Shortcuts = new ObservableCollection<BrowserShortcut>
            {
                new BrowserShortcut { Title = "GitHub", Url = "github.com", IconText = "Gh", Color = "#24292e" },
                new BrowserShortcut { Title = "Gemini", Url = "gemini.google.com", IconText = "Ge", Color = "#4285F4" },
                new BrowserShortcut { Title = "ChatGPT", Url = "chatgpt.com", IconText = "Ai", Color = "#10A37F" },
                new BrowserShortcut { Title = "G2 Market", Url = "https://g2g.market", IconText = "G2", Color = "#FF0000" }, // Bizim Market
                new BrowserShortcut { Title = "YouTube", Url = "youtube.com", IconText = "Yt", Color = "#FF0000" },
                new BrowserShortcut { Title = "Twitch", Url = "twitch.tv", IconText = "Tw", Color = "#9146FF" },
            };

            // Market data (Dəyişməyib)
            MarketItems = new ObservableCollection<MarketItem>
            {
                new MarketItem { Title = "Valorant | Ascendant 3", Price = 50, Type = "Account", SellerName = "LegitSeller_99", RiskLevel = "Low" },
                new MarketItem { Title = "CS2 Prime | 10 Year Coin", Price = 120, Type = "Steam", SellerName = "FastTrader", RiskLevel = "Medium" },
                new MarketItem { Title = "Fortnite OG Skull", Price = 350, Type = "Account", SellerName = "ScamWarning", RiskLevel = "High" },
                new MarketItem { Title = "GTA V Modded", Price = 20, Type = "SocialClub", SellerName = "HackerX", RiskLevel = "Medium" },
            };

            // Başlanğıcda Home səhifəsi olsun
            Navigate();
        }

        [RelayCommand]
        public void Navigate(string url = null)
        {
            if (url != null) AddressBarText = url;

            // Əgər URL-də "g2g" varsa, Marketi göstər, yoxsa Ana səhifəni (Google)
            if (AddressBarText.Contains("g2g"))
            {
                IsMarketVisible = true;
            }
            else
            {
                IsMarketVisible = false;
                AddressBarText = "google.com"; // Reset to home
            }
        }

        [RelayCommand]
        public void GoHome()
        {
            AddressBarText = "google.com";
            IsMarketVisible = false;
        }

        [RelayCommand]
        public void BuyItem(MarketItem item)
        {
            if (GameManager.Instance.TryBuyItem(item))
            {
                MarketItems.Remove(item);
            }
        }
    }
}
