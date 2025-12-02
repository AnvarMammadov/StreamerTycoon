using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamerTycoon.ViewModels;

namespace StreamerTycoon.ViewModels
{
    public class OnionViewModel : AppWindowViewModel
    {
        public ObservableCollection<string> HackingTools { get; set; }

        public OnionViewModel() : base("OnionCore (Encrypted)")
        {
            HackingTools = new ObservableCollection<string>
            {
                "Combo List (10k Emails) - $200",
                "BruteForce Tool v5.0 - $500",
                "Fake ID Generator - $150"
            };
        }
    }
}
