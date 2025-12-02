using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamerTycoon.Models;
using StreamerTycoon.ViewModels;

namespace StreamerTycoon.ViewModels
{
    public class ChatViewModel : AppWindowViewModel
    {
        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ChatViewModel() : base("ChatGrid")
        {
            Messages = new ObservableCollection<ChatMessage>
            {
                new ChatMessage
                {
                    Username = "Scammer1337",
                    Message = "Salam, yeni Valorant hesabı var?",
                    ColorHex = "#FFFFFF" // Ağ rəng (Qarşı tərəf)
                },
                new ChatMessage
                {
                    Username = "Ghost_User",
                    Message = "Bəli, indi stokda var. 50$",
                    ColorHex = "#00FF00" // Yaşıl rəng (Biz)
                },
                new ChatMessage
                {
                    Username = "Scammer1337",
                    Message = "Əla, endirim edərsən?",
                    ColorHex = "#FFFFFF"
                }
            };
        }
    }
}
