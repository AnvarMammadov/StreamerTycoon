using System.Collections.ObjectModel;

namespace StreamerTycoon.ViewModels
{
    public class OnionViewModel : AppWindowViewModel
    {
        public ObservableCollection<string> HackingTools { get; set; }

        public OnionViewModel() : base("OnionCore", "Icon_Thor")
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
