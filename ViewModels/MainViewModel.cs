using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StreamerTycoon.Models;


namespace StreamerTycoon.ViewModels
{
    // Oyunun əsas mərhələləri
    public enum GameState
    {
        LoginScreen,
        Desktop
    }

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private GameState _currentState;

        [ObservableProperty]
        private string _currentTime;

        [ObservableProperty]
        private string _currentDate;

        private DispatcherTimer _timer;

        // Login üçün
        [ObservableProperty] private string _userName = "Scammer_X";
        [ObservableProperty] private string _password = "";
        [ObservableProperty] private string _loginError = "";

        public MainViewModel()
        {
            CurrentState = GameState.LoginScreen; // Oyun buradan başlayır

            // Saat sistemi
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => {
                CurrentTime = DateTime.Now.ToString("HH:mm");
                CurrentDate = DateTime.Now.ToString("dd MMMM, dddd");
            };
            _timer.Start();
        }

        [RelayCommand]
        public void Login()
        {
            // Sadə login yoxlaması
            if (!string.IsNullOrEmpty(Password))
            {
                CurrentState = GameState.Desktop;
            }
            else
            {
                LoginError = "Şifrəni daxil edin";
            }
        }

        [RelayCommand]
        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
