using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page3 : Page
    {
        private Page Page4 { get; set; }

        public Page3()
        {
            WindowTitle = "Page3: Кисть LinearGradientBrush";

            var btn = new Button
            {
                Content = "Next Page4"
            };
            btn.Click += btn3_Click;
            Content = btn;

            var brush = new LinearGradientBrush(
                Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
            btn.Background = brush;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
                Page4 = new Page4();
            NavigationService.Navigate(Page4);
        }
    }

}
