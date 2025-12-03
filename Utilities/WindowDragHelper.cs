using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using StreamerTycoon.ViewModels;

namespace StreamerTycoon.Utilities
{
    public static class WindowDragHelper
    {
        // XAML-də istifadə edəcəyimiz "EnableDrag" xüsusiyyəti
        public static readonly DependencyProperty EnableDragProperty =
            DependencyProperty.RegisterAttached("EnableDrag", typeof(bool), typeof(WindowDragHelper), new PropertyMetadata(false, OnEnableDragChanged));

        public static bool GetEnableDrag(DependencyObject obj) => (bool)obj.GetValue(EnableDragProperty);
        public static void SetEnableDrag(DependencyObject obj, bool value) => obj.SetValue(EnableDragProperty, value);

        // Xüsusiyyət dəyişəndə işə düşür
        private static void OnEnableDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
                element.MouseLeftButtonUp -= Element_MouseLeftButtonUp;
                element.MouseMove -= Element_MouseMove;

                if ((bool)e.NewValue)
                {
                    element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                    element.MouseLeftButtonUp += Element_MouseLeftButtonUp;
                    element.MouseMove += Element_MouseMove;
                }
            }
        }

        private static Point _startMousePoint;
        private static bool _isDragging;

        private static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element != null)
            {
                _isDragging = true;
                _startMousePoint = e.GetPosition(null); // Siçanın ekran koordinatlarını götürürük
                element.CaptureMouse(); // Siçanı "tuturuq" ki, sürətli hərəkət edəndə itməsin

                // Bonus: Pəncərəyə klikləyəndə onu önə gətirmək (ZIndex)
                if (element.DataContext is AppWindowViewModel vm)
                {
                    // Bu hissə sadəcə vizual klik effekti üçündür, 
                    // ZIndex məntiqi MainViewModel-də idarə olunsa daha yaxşıdır,
                    // amma burda da sadə bir toxunuş edə bilərik.
                }
            }
        }

        private static void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && sender is FrameworkElement element && element.DataContext is AppWindowViewModel vm)
            {
                var currentPoint = e.GetPosition(null);
                var offset = currentPoint - _startMousePoint;

                // ViewModel-dəki koordinatları yeniləyirik
                vm.X += offset.X;
                vm.Y += offset.Y;

                _startMousePoint = currentPoint;
            }
        }

        private static void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                (sender as FrameworkElement)?.ReleaseMouseCapture();
            }
        }
    }
}
