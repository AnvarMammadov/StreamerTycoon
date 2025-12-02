using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace StreamerTycoon.Models
{
    public partial class DesktopItem : ObservableObject
    {
        public string Title { get; set; }        // İkonun adı (məs: Recycle Bin)
        public string IconKey { get; set; }      // App.xaml-dakı ikonun adı
        public string Type { get; set; }         // App, Folder, Link

        // Koordinatlar (Dəyişdikdə UI yenilənməlidir)
        [ObservableProperty] private double _x;
        [ObservableProperty] private double _y;

        public DesktopItem(string title, string iconKey, double x, double y)
        {
            Title = title;
            IconKey = iconKey;
            X = x;
            Y = y;
        }
    }
}
