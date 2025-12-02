using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamerTycoon.Models;


namespace StreamerTycoon.ViewModels
{
    public class BrowserViewModel : AppWindowViewModel
    {
        public ObservableCollection<MarketItem> MarketItems { get; set; }

        public BrowserViewModel() : base("Web Portal")
        {
            MarketItems = new ObservableCollection<MarketItem>
            {
                new MarketItem
                {
                    Id = 1,
                    Title = "Valorant (Ascendant)",
                    Price = 50,
                    Type = "Game Account",
                    SellerName = "Risk: Low" // Risk leveli bura yazırıq
                },
                new MarketItem
                {
                    Id = 2,
                    Title = "Steam (CS2 Prime)",
                    Price = 120,
                    Type = "Steam Account",
                    SellerName = "Risk: Medium"
                },
                new MarketItem
                {
                    Id = 3,
                    Title = "Fortnite (OG Skins)",
                    Price = 300,
                    Type = "Game Account",
                    SellerName = "Risk: High"
                }
            };
        }
    }
}
