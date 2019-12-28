using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Controls.Primitives;

namespace WpfApp3
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            // Возвращаем Thumb в исходное состояние
            Canvas.SetLeft(thumb1, 5);
            Canvas.SetTop(thumb1, 5);
        }

        double originalLeft, originalTop;
        private void thumb1_DragStarted(object sender, DragStartedEventArgs e)
        {
            originalLeft = Canvas.GetLeft(thumb1);
            originalTop = Canvas.GetTop(thumb1);
        }

        private void thumb1_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double left = originalLeft + e.HorizontalChange;
            double top = originalTop + e.VerticalChange;
            Canvas.SetLeft(thumb1, left);
            Canvas.SetTop(thumb1, top);
            originalLeft = left;
            originalTop = top;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }
    }
}
