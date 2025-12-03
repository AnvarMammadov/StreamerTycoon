using System.Collections.ObjectModel;
using StreamerTycoon.Models;

namespace StreamerTycoon.ViewModels
{
    // Kontakt siyahısı üçün sadə bir model (Hələlik daxildə saxlayırıq)
    public class ChatContact
    {
        public string Name { get; set; }
        public string StatusColor { get; set; } = "#747F8D"; // Offline boz
        public bool IsSelected { get; set; }
    }

    public class ChatViewModel : AppWindowViewModel
    {
        public ObservableCollection<ChatMessage> Messages { get; set; }
        public ObservableCollection<ChatContact> Contacts { get; set; }

        public ChatViewModel() : base("ChatGrid", "Icon_Discord")
        {
            Contacts = new ObservableCollection<ChatContact>
            {
                new ChatContact { Name = "Scammer1337", StatusColor = "#43B581", IsSelected = true }, // Online Yaşıl
                new ChatContact { Name = "DarkSupplier", StatusColor = "#FAA61A" }, // Idle Sarı
                new ChatContact { Name = "Mom", StatusColor = "#747F8D" }, // Offline
            };

            Messages = new ObservableCollection<ChatMessage>
            {
                new ChatMessage { Username = "Scammer1337", Message = "Salam, hesabın məlumatlarını atdım.", ColorHex = "#FFFFFF" },
                new ChatMessage { Username = "Ghost_User", Message = "Yoxlayıram, bir dəqiqə.", ColorHex = "#00FF00" }, // Bizim mesaj
                new ChatMessage { Username = "Scammer1337", Message = "Tez ol, başqa müştəri var.", ColorHex = "#FFFFFF" }
            };
        }
    }
}
