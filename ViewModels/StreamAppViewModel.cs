using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;

namespace StreamerTycoon.ViewModels
{
    public partial class StreamAppViewModel : AppWindowViewModel
    {
        // Statistika
        [ObservableProperty] private int _viewerCount = 0;
        [ObservableProperty] private string _streamDuration = "00:00:00";
        [ObservableProperty] private bool _isLive = false;
        [ObservableProperty] private string _streamTitle = "🔥 ROAD TO RADIANT | RANKED | NO TILT 🚫";

        // Kolleksiyalar
        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();
        public ObservableCollection<StreamEvent> ActivityFeed { get; set; } = new ObservableCollection<StreamEvent>();

        private DispatcherTimer _gameTimer;
        private Random _rnd = new Random();
        private DateTime _startTime;

        public StreamAppViewModel() : base("LivePulse Studio", "Icon_Kick")
        {
            // Simulyasiya üçün Timer
            _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(800) };
            _gameTimer.Tick += GameLoop;
        }

        [RelayCommand]
        public void ToggleStream()
        {
            IsLive = !IsLive;

            if (IsLive)
            {
                _startTime = DateTime.Now;
                _gameTimer.Start();
                AddLog("System", "Stream Started! Connecting to servers...", "#00E676");
                ViewerCount = 1;
            }
            else
            {
                _gameTimer.Stop();
                AddLog("System", "Stream Ended.", "#FF453A");
                ViewerCount = 0;
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            // 1. Vaxtı yenilə
            var span = DateTime.Now - _startTime;
            StreamDuration = span.ToString(@"hh\:mm\:ss");

            // 2. İzləyici sayı (Random artma/azalma)
            if (_rnd.Next(0, 10) > 6)
            {
                int change = _rnd.Next(-5, 15);
                ViewerCount += change;
                if (ViewerCount < 0) ViewerCount = 0;
            }

            // 3. Random Çat Mesajları
            if (_rnd.Next(0, 10) > 5)
            {
                string[] users = { "NoobSlayer", "ValoGod", "Simp123", "HackerMan", "Viewer99", "Bot_X" };
                string[] msgs = { "POG", "LUL", "Nice shot!", "Bu oyun nədir?", "Salam!", "Donat gəlmir?", "FPS neçədir?", "fakeos op", "scam?" };

                AddChat(users[_rnd.Next(users.Length)], msgs[_rnd.Next(msgs.Length)], GetRandomColor());
            }

            // 4. Random Donat/Sub (Nadir hallarda)
            if (_rnd.Next(0, 100) > 95)
            {
                TriggerEvent();
            }
        }

        private void TriggerEvent()
        {
            string[] names = { "RichGuy", "FanBoy", "CryptoKing" };
            int amount = _rnd.Next(5, 100);

            var evt = new StreamEvent
            {
                Message = $"{names[_rnd.Next(names.Length)]} donated ${amount}!",
                Type = "Donation",
                Color = "#00E676"
            };

            // Ən başa əlavə edirik (Yeni gələn yuxarıda)
            ActivityFeed.Insert(0, evt);

            // Çatda da göstər
            AddChat("Streamlabs", $"Woah! ${amount} donation!", "#00E676");
        }

        private void AddChat(string user, string msg, string color)
        {
            ChatMessages.Add(new ChatMessage { Username = user, Message = msg, ColorHex = color });

            // Çat çox dolmasın (optimallaşdırma)
            if (ChatMessages.Count > 50) ChatMessages.RemoveAt(0);
        }

        private void AddLog(string user, string msg, string color)
        {
            ChatMessages.Add(new ChatMessage { Username = user, Message = msg, ColorHex = color });
        }

        private string GetRandomColor()
        {
            string[] colors = { "#FF453A", "#30D158", "#00A8FF", "#FFD60A", "#BF5AF2", "#FFFFFF" };
            return colors[_rnd.Next(colors.Length)];
        }
    }
}