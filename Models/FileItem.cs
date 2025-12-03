using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerTycoon.Models
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Type { get; set; }     // "File", "Folder", "Image", "Exe"
        public string IconKey { get; set; }  // App.xaml-dakı ikonun adı (Icon_Folder, Icon_File ve s.)
        public string Size { get; set; }
        public string Date { get; set; }
    }
}
