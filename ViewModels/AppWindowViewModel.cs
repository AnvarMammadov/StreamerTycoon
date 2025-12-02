using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StreamerTycoon.ViewModels
{
    public partial class AppWindowViewModel : ObservableObject
    {
        [ObservableProperty] private string _title;
        [ObservableProperty] private bool _isVisible = true;

        // Pəncərəni bağlamaq üçün Action
        public Action<AppWindowViewModel> CloseAction { get; set; }

        public AppWindowViewModel(string title)
        {
            Title = title;
        }

        [RelayCommand]
        public void Close()
        {
            CloseAction?.Invoke(this);
        }
    }
}
