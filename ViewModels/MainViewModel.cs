using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;

namespace StreamerTycoon.ViewModels
{
    // Oyunun mərhələləri
    public enum GameState
    {
        Bedroom,    // Otaq
        LoginScreen,// Giriş Ekranı
        Desktop     // Masaüstü
    }

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private GameState _currentState;

        [ObservableProperty]
        private string _currentTime;

        [ObservableProperty]
        private string _currentDate;

        [ObservableProperty] private string _userName = "Ghost_User";
        [ObservableProperty] private string _password = "";
        [ObservableProperty] private string _loginError = "";

        private DispatcherTimer _timer;

        public ObservableCollection<AppWindowViewModel> OpenWindows { get; set; } = new ObservableCollection<AppWindowViewModel>();

        // Tətbiqi Açmaq Əmri
        [RelayCommand]
        public void OpenApp(string appName)
        {
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
                    // PC qovluğu üçün sadə browser aça bilərik hələlik
                    newWindow = new AppWindowViewModel("System Files");
                    break;
            }

            if (newWindow != null)
            {
                // Pəncərə bağlananda kolleksiyadan silinməlidir
                newWindow.CloseAction = (win) => OpenWindows.Remove(win);
                OpenWindows.Add(newWindow);
            }
        }



        public MainViewModel()
        {
            // OYUN BURADAN BAŞLAYIR:
            CurrentState = GameState.Bedroom;

            // Saat sistemi
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                CurrentTime = DateTime.Now.ToString("HH:mm");
                CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            };
            _timer.Start();
        }

        // BU HİSSƏ ÇATIŞMIRDI: Otaqdan Login ekranına keçid
        [RelayCommand]
        public void EnterComputer()
        {
            CurrentState = GameState.LoginScreen;
        }

        // Login ekranından Masaüstünə keçid
        [RelayCommand]
        public void Login()
        {
            // Şifrə yoxlaması (sadəlik üçün hələlik birbaşa keçir)
            if (!string.IsNullOrEmpty(Password))
            {
                CurrentState = GameState.Desktop;
            }
            else
            {
                // İstəsəniz boş şifrə ilə buraxmayın:
                // LoginError = "Şifrəni daxil edin";
                CurrentState = GameState.Desktop; // Test üçün birbaşa buraxırıq
            }
        }

        // Oyunu bağlamaq
        [RelayCommand]
        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}