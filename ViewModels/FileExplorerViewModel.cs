using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using StreamerTycoon.Models;

namespace StreamerTycoon.ViewModels
{
    public partial class FileExplorerViewModel : AppWindowViewModel
    {
        [ObservableProperty] private string _currentPath;
        public ObservableCollection<FileItem> Files { get; set; }

        // Konstruktor: folderType "Junkyard" və ya "Data" ola bilər
        public FileExplorerViewModel(string folderType) : base("File Explorer", "Icon_Folder")
        {
            Files = new ObservableCollection<FileItem>();

            if (folderType == "Junkyard")
            {
                Title = "Recycle Bin";
                CurrentPath = "Desktop > Recycle Bin";
                IconKey = "Icon_Trash";

                // Zibil qutusu faylları (Lore üçün)
                Files.Add(new FileItem { Name = "failed_virus.exe", Type = "Application", Size = "4.2 MB", Date = "Yesterday", IconKey = "Icon_File" });
                Files.Add(new FileItem { Name = "evidence.png", Type = "Image", Size = "2.1 MB", Date = "Yesterday", IconKey = "Icon_File" });
                Files.Add(new FileItem { Name = "New Text Document.txt", Type = "Text", Size = "0 KB", Date = "Last Week", IconKey = "Icon_File" });
            }
            else if (folderType == "Data")
            {
                Title = "Data Dumps";
                CurrentPath = "This PC > Data Dumps";
                IconKey = "Icon_Folder";

                // Haker faylları
                Files.Add(new FileItem { Name = "passwords_dump.txt", Type = "Text", Size = "12 KB", Date = "Today", IconKey = "Icon_File" });
                Files.Add(new FileItem { Name = "target_list.xlsx", Type = "Sheet", Size = "45 KB", Date = "Today", IconKey = "Icon_File" });
                Files.Add(new FileItem { Name = "btc_wallet.dat", Type = "DAT File", Size = "128 KB", Date = "2 days ago", IconKey = "Icon_File" });
                Files.Add(new FileItem { Name = "Tools", Type = "Folder", Size = "", Date = "2023", IconKey = "Icon_Folder" });
            }
        }
    }
}
