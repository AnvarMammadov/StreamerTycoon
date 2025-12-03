using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;
using StreamerTycoon.Services;


namespace StreamerTycoon.ViewModels
{
    public partial class BrowserViewModel : AppWindowViewModel
    {
        public ObservableCollection<MarketItem> MarketItems { get; set; }

        public BrowserViewModel() : base("G2-Marketplace", "Icon_Web")
        {
            MarketItems = new ObservableCollection<MarketItem>
            {
                // Məhsullar... (Köhnə siyahı qalır)
                new MarketItem { Title = "Valorant (Ascendant)", Price = 50, Type = "Account", SellerName = "LegitSeller_99", RiskLevel = "Low" },
                new MarketItem { Title = "Steam (CS2 Prime)", Price = 120, Type = "Steam", SellerName = "FastTrader", RiskLevel = "Medium" },
                new MarketItem { Title = "Fortnite (OG Skull)", Price = 350, Type = "Account", SellerName = "ScamWarning", RiskLevel = "High" },
                new MarketItem { Title = "GTA V (Modded)", Price = 20, Type = "SocialClub", SellerName = "HackerX", RiskLevel = "Medium" },
            };
        }

        // "Buy" düyməsi basılanda bu işləyəcək
        [RelayCommand]
        public void BuyItem(MarketItem item)
        {
            if (item == null) return;

            // GameManager-dən soruşuruq: Pul var?
            bool success = GameManager.Instance.TryBuyItem(item);

            if (success)
            {
                MessageBox.Show($"Successfully purchased: {item.Title}!", "Purchase Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                // Real oyunda məhsul marketdən silinməli və ya "Sold" yazılmalıdır
                MarketItems.Remove(item);
            }
            else
            {
                MessageBox.Show("Insufficient funds! Go stream or flip accounts to earn money.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
