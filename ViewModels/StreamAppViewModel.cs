using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;

namespace StreamerTycoon.ViewModels
{
    public partial class StreamAppViewModel : AppWindowViewModel
    {
        // Statistika (OBS Style)
        [ObservableProperty] private int _viewerCount = 0;
        [ObservableProperty] private string _streamDuration = "00:00:00";
        [ObservableProperty] private bool _isLive = false;
        [ObservableProperty] private string _streamTitle = "🔥 ROAD TO RADIANT | RANKED | NO TILT 🚫";

        // Texniki Stats (Fake)
        [ObservableProperty] private int _fps = 60;
        [ObservableProperty] private string _cpuUsage = "4.2%";
        [ObservableProperty] private string _bitrate = "0 kb/s";
        [ObservableProperty] private string _droppedFrames = "0 (0.0%)";

        // Kolleksiyalar
        public ObservableCollection<ChatMessage> ChatMessages { get; set; } = new ObservableCollection<ChatMessage>();
        public ObservableCollection<StreamEvent> ActivityFeed { get; set; } = new ObservableCollection<StreamEvent>();

        // OBS Scenes (Sadəcə vizual siyahı)
        public ObservableCollection<string> Scenes { get; set; } = new ObservableCollection<string> { "Just Chatting", "Gaming [Main]", "BRB Screen", "Ending" };

        // OBS Sources
        public ObservableCollection<string> Sources { get; set; } = new ObservableCollection<string> { "Game Capture", "Webcam (C920)", "Mic (HyperX)", "Alert Box" };

        private DispatcherTimer _gameTimer;
        private Random _rnd = new Random();
        private DateTime _startTime;

        public StreamAppViewModel() : base("LivePulse Studio", "Icon_Kick")
        {
            // Simulyasiya üçün Timer
            _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            _gameTimer.Tick += GameLoop;

            // Default statik timer (FPS update üçün həmişə işləsin)
            var statsTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            statsTimer.Tick += (s, e) => UpdateTechnicalStats();
            statsTimer.Start();
        }

        [RelayCommand]
        public void ToggleStream()
        {
            IsLive = !IsLive;

            if (IsLive)
            {
                _startTime = DateTime.Now;
                _gameTimer.Start();
                AddLog("OBS", "Stream Output Started.", "#00E676");
                ViewerCount = 1;
                Bitrate = "6000 kb/s";
            }
            else
            {
                _gameTimer.Stop();
                AddLog("OBS", "Stream Output Stopped.", "#FF453A");
                ViewerCount = 0;
                StreamDuration = "00:00:00";
                Bitrate = "0 kb/s";
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

            // 4. Random Donat/Sub
            if (_rnd.Next(0, 100) > 95) TriggerEvent();
        }

        private void UpdateTechnicalStats()
        {
            // FPS 58-60 arası dəyişsin
            Fps = _rnd.Next(58, 61);
            // CPU 3-15% arası
            CpuUsage = $"{_rnd.Next(3, 15)}.{_rnd.Next(0, 9)}%";

            if (IsLive)
            {
                // Bitrate dalğalansın (5800 - 6200)
                Bitrate = $"{_rnd.Next(5800, 6200)} kb/s";
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

            ActivityFeed.Insert(0, evt);
            AddChat("Streamlabs", $"Woah! ${amount} donation!", "#00E676");
        }

        private void AddChat(string user, string msg, string color)
        {
            ChatMessages.Add(new ChatMessage { Username = user, Message = msg, ColorHex = color });
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