using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using StreamerTycoon.Models;

namespace StreamerTycoon.Services
{
    public partial class GameManager : ObservableObject
    {
        // Singleton Pattern (Hər yerdən əlçatan olması üçün)
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();

        // Oyunçunun Pulu
        [ObservableProperty] private double _money = 1500; // Başlanğıc pul

        // Oyunçunun Reputasiyası (Level üçün)
        [ObservableProperty] private int _reputation = 10;

        // İnventar (Aldığımız hesablar bura düşəcək)
        public ObservableCollection<MarketItem> Inventory { get; set; } = new ObservableCollection<MarketItem>();

        private GameManager() { }

        // Məhsul almaq funksiyası
        public bool TryBuyItem(MarketItem item)
        {
            if (Money >= item.Price)
            {
                Money -= item.Price; // Pulu çıx
                Inventory.Add(item); // İnventara əlavə et
                return true; // Uğurlu
            }
            return false; // Pul çatmır
        }

        // Hesab satmaq funksiyası (Gələcəkdə lazım olacaq)
        public void SellItem(MarketItem item, double sellPrice)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Money += sellPrice;
            }
        }
    }
}
