using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input; // Mouse üçün lazımdır
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;
using StreamerTycoon.Services;

namespace StreamerTycoon.ViewModels
{
    public enum GameState
    {
        Bedroom,
        LoginScreen,
        Desktop
    }

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private GameState _currentState;
        [ObservableProperty] private string _currentTime;
        [ObservableProperty] private string _currentDate;

        public GameManager Game => GameManager.Instance;
        public ObservableCollection<AppWindowViewModel> OpenWindows { get; set; } = new ObservableCollection<AppWindowViewModel>();

        private DispatcherTimer _timer;

        public MainViewModel()
        {
            CurrentState = GameState.Bedroom;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                CurrentTime = DateTime.Now.ToString("HH:mm");
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            };
            _timer.Start();
        }

        [RelayCommand]
        public void OpenApp(string appName)
        {
            AppWindowViewModel existingWindow = null;

            switch (appName)
            {
                case "Browser": existingWindow = OpenWindows.OfType<BrowserViewModel>().FirstOrDefault(); break;
                case "Chat": existingWindow = OpenWindows.OfType<ChatViewModel>().FirstOrDefault(); break;
                case "Onion": existingWindow = OpenWindows.OfType<OnionViewModel>().FirstOrDefault(); break;
                case "Stream": existingWindow = OpenWindows.OfType<StreamAppViewModel>().FirstOrDefault(); break;
                case "Junkyard": existingWindow = OpenWindows.FirstOrDefault(w => w.Title == "Recycle Bin"); break;
                case "DataDumps": existingWindow = OpenWindows.FirstOrDefault(w => w.Title == "Data Dumps"); break;
                case "PC": existingWindow = OpenWindows.FirstOrDefault(w => w.Title == "Data Dumps"); break;
            }

            if (existingWindow != null)
            {
                if (existingWindow.IsVisible != Visibility.Visible) existingWindow.IsVisible = Visibility.Visible;
                foreach (var win in OpenWindows) win.ZIndex = 0;
                existingWindow.ZIndex = 999;
                return;
            }

            AppWindowViewModel newWindow = null;
            switch (appName)
            {
                case "Browser": newWindow = new BrowserViewModel(); break;
                case "Chat": newWindow = new ChatViewModel(); break;
                case "Onion": newWindow = new OnionViewModel(); break;
                case "Stream": newWindow = new StreamAppViewModel(); break;
                case "PC":
                case "DataDumps": newWindow = new FileExplorerViewModel("Data"); break;
                case "Junkyard": newWindow = new FileExplorerViewModel("Junkyard"); break;
            }

            if (newWindow != null)
            {
                foreach (var win in OpenWindows) win.ZIndex = 0;
                newWindow.ZIndex = 999;
                newWindow.CloseAction = (win) => OpenWindows.Remove(win);
                OpenWindows.Add(newWindow);
            }
        }

        [RelayCommand]
        public void EnterComputer() => CurrentState = GameState.LoginScreen;

        [RelayCommand]
        public void Login() => CurrentState = GameState.Desktop;

        [RelayCommand]
        public void Shutdown() => System.Windows.Application.Current.Shutdown();

        // --- YENİ: REFRESH COMMAND ---
        [RelayCommand]
        public void RefreshDesktop()
        {
            // Vizual olaraq refresh effekti (Kursor fırlanır)
            Mouse.OverrideCursor = Cursors.Wait;

            // Yarım saniyə sonra kursoru qaytar
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            timer.Tick += (s, e) =>
            {
                Mouse.OverrideCursor = null;
                timer.Stop();
            };
            timer.Start();
        }
    }
}