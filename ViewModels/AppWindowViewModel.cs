using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StreamerTycoon.ViewModels
{
    public partial class AppWindowViewModel : ObservableObject
    {
        [ObservableProperty] private string _title;
        [ObservableProperty] private string _iconKey;
        [ObservableProperty] private Visibility _isVisible = Visibility.Visible;

        // --- YENİLƏNDİ: Default ölçülər böyüdüldü ---
        // Əvvəl: 850x550 -> İndi: 1100x720
        [ObservableProperty] private double _width = 1100;
        [ObservableProperty] private double _height = 720;

        // --- YENİLƏNDİ: Başlanğıc Koordinatları ---
        // Ekranın ortasına düşməsi üçün təxmini hesab (1920x1080 ekran üçün)
        // X = (1920 - 1100) / 2 = 410
        // Y = (1080 - 720) / 2 = 180
        [ObservableProperty] private double _x = 180;
        [ObservableProperty] private double _y = 50;

        [ObservableProperty] private int _zIndex = 0;

        // Yaddaş (Restore üçün)
        private double _oldX, _oldY, _oldWidth, _oldHeight;
        private bool _isMaximized = false;

        public Action<AppWindowViewModel> CloseAction { get; set; }

        public AppWindowViewModel(string title, string iconKey)
        {
            Title = title;
            IconKey = iconKey;
        }

        [RelayCommand]
        public void Close()
        {
            CloseAction?.Invoke(this);
        }

        [RelayCommand]
        public void Minimize()
        {
            IsVisible = Visibility.Collapsed;
        }

        [RelayCommand]
        public void Maximize()
        {
            if (_isMaximized)
            {
                // Restore (Köhnə vəziyyətə və ölçüyə qayıt)
                X = _oldX;
                Y = _oldY;
                Width = _oldWidth;
                Height = _oldHeight;
                _isMaximized = false;
            }
            else
            {
                // Maximize (Tam Ekran)
                // 1. İndiki vəziyyəti yadda saxla
                _oldX = X;
                _oldY = Y;
                _oldWidth = Width;
                _oldHeight = Height;

                // 2. Real Ekran ölçülərini tətbiq et
                X = 0;
                Y = 0;

                // Monitorun tam eni
                Width = SystemParameters.PrimaryScreenWidth;

                // Monitorun hündürlüyü ÇIXILSIN Taskbar hündürlüyü (50px)
                // FakeOS taskbarı DesktopView-da 50px hündürlüyündədir.
                Height = SystemParameters.PrimaryScreenHeight - 50;

                _isMaximized = true;
            }
        }

        [RelayCommand]
        public void ToggleMinimize()
        {
            if (IsVisible == Visibility.Visible)
            {
                Minimize();
            }
            else
            {
                IsVisible = Visibility.Visible;
                ZIndex = 999; // Pəncərəni ən önə gətir
            }
        }
    }
}