using System.Windows;
using System.Windows.Media.Media3D;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StreamerTycoon.ViewModels
{
    public partial class AppWindowViewModel : ObservableObject
    {
        [ObservableProperty] private string _title;
        [ObservableProperty] private string _iconKey;

        // Pəncərənin Vəziyyəti (Görünürlüyü)
        [ObservableProperty] private Visibility _isVisible = Visibility.Visible;

        // Koordinatlar (Default: Ekranın Ortası)
        // Ekran 1280x800, Pəncərə 850x550. 
        // X = (1280-850)/2 = 215
        // Y = (800-550)/2 = 125
        [ObservableProperty] private double _x = 215;
        [ObservableProperty] private double _y = 125;
        [ObservableProperty] private int _zIndex = 0;

        // Ölçülər (Maximize üçün lazımdır)
        [ObservableProperty] private double _width = 850;
        [ObservableProperty] private double _height = 550;

        // Yaddaş (Maximize edəndə köhnə yerini yadda saxlamaq üçün)
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
            // Pəncərəni Desktop-dan gizlət (Taskbar-da qalacaq)
            IsVisible = Visibility.Collapsed;
        }

        [RelayCommand]
        public void Maximize()
        {
            if (_isMaximized)
            {
                // Restore (Köhnə vəziyyətə qayıt)
                X = _oldX;
                Y = _oldY;
                Width = _oldWidth;
                Height = _oldHeight;
                _isMaximized = false;
            }
            else
            {
                // Maximize (Tam ekran, Taskbar çıxmaq şərtilə)
                // Yadda saxla
                _oldX = X;
                _oldY = Y;
                _oldWidth = Width;
                _oldHeight = Height;

                // Tam ekran et (FakeOS ölçüləri: 1280x800, Taskbar 50px)
                X = 0;
                Y = 0;
                Width = 1280;
                Height = 750; // Taskbar üçün yer saxlayırıq
                _isMaximized = true;
            }
        }

        // Taskbar ikonuna basanda pəncərəni geri gətirmək üçün
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
                ZIndex = 999; // Önə gətir
            }
        }
    }
}
