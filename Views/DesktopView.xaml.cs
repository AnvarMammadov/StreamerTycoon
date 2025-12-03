using System.Windows.Controls;
using System.Windows.Input;

namespace StreamerTycoon.Views
{
    public partial class DesktopView : UserControl
    {
        public DesktopView()
        {
            InitializeComponent();
        }

        // YENİ: Siçan hərəkət etdikdə işə düşən metod
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            // Siçanın koordinatlarını əsas Grid-ə nəzərən alırıq
            var position = e.GetPosition(this);

            // Bizim xüsusi kursorumuzun (Path elementi) mövqeyini yeniləyirik
            // Kursorun ucu (0,0 nöqtəsi) tam siçanın olduğu yerdə olacaq
            Canvas.SetLeft(CustomCursor, position.X);
            Canvas.SetTop(CustomCursor, position.Y);
        }
    }
}
