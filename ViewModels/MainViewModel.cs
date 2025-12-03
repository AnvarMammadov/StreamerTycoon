using System;
using System.Collections.ObjectModel;
using System.Linq; // LINQ lazımdır (OfType, FirstOrDefault üçün)
using System.Windows; // Visibility üçün lazımdır
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

        // Service
        public GameManager Game => GameManager.Instance;

        // Açıq Pəncərələr Kolleksiyası
        public ObservableCollection<AppWindowViewModel> OpenWindows { get; set; } = new ObservableCollection<AppWindowViewModel>();

        private DispatcherTimer _timer;

        public MainViewModel()
        {
            CurrentState = GameState.Bedroom;

            // Saat sistemi
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                CurrentTime = DateTime.Now.ToString("HH:mm");
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            };
            _timer.Start();
        }

        // Tətbiqi Açmaq (Və ya Önə Gətirmək)
        [RelayCommand]
        public void OpenApp(string appName)
        {
            // 1. Yoxlayırıq: Bu pəncərə artıq açıqdırmı?
            AppWindowViewModel existingWindow = null;

            switch (appName)
            {
                case "Browser":
                    existingWindow = OpenWindows.OfType<BrowserViewModel>().FirstOrDefault();
                    break;
                case "Chat":
                    existingWindow = OpenWindows.OfType<ChatViewModel>().FirstOrDefault();
                    break;
                case "Onion":
                    existingWindow = OpenWindows.OfType<OnionViewModel>().FirstOrDefault();
                    break;
                case "Stream":
                    existingWindow = OpenWindows.OfType<StreamAppViewModel>().FirstOrDefault();
                    break;
                case "PC":
                    // PC (System Files) base klass olduğu üçün Title ilə yoxlayırıq
                    existingWindow = OpenWindows.FirstOrDefault(w => w.Title == "System Files");
                    break;
            }

            // 2. Əgər tapıldısa, yenisini YARATMA, mövcud olanı önə gətir
            if (existingWindow != null)
            {
                // Əgər minimize olubsa, görünən et
                if (existingWindow.IsVisible != Visibility.Visible)
                {
                    existingWindow.IsVisible = Visibility.Visible;
                }

                // Digər pəncərələri arxaya at, bunu önə çək
                foreach (var win in OpenWindows) win.ZIndex = 0;
                existingWindow.ZIndex = 999;

                return; // Metoddan çıxırıq ki, yeni pəncərə yaranmasın
            }

            // 3. Əgər tapılmadısa, yeni pəncərə yarat
            AppWindowViewModel newWindow = null;

            switch (appName)
            {
                case "Browser":
                    newWindow = new BrowserViewModel();
                    break;
                case "Chat":
                    newWindow = new ChatViewModel();
                    break;
                case "Onion":
                    newWindow = new OnionViewModel();
                    break;
                case "Stream":
                    newWindow = new StreamAppViewModel();
                    break;
                case "PC":
                    newWindow = new AppWindowViewModel("System Files", "Icon_MyPC");
                    break;
            }

            if (newWindow != null)
            {
                // Yeni pəncərəni önə qoyuruq
                foreach (var win in OpenWindows) win.ZIndex = 0;
                newWindow.ZIndex = 999;

                // Bağlananda siyahıdan silinməsi üçün
                newWindow.CloseAction = (win) => OpenWindows.Remove(win);

                OpenWindows.Add(newWindow);
            }
        }

        [RelayCommand]
        public void EnterComputer()
        {
            CurrentState = GameState.LoginScreen;
        }

        [RelayCommand]
        public void Login()
        {
            CurrentState = GameState.Desktop;
        }

        [RelayCommand]
        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}