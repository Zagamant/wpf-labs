using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page4 : Page
    {
        private Page Page5 { get; set; }

        public Page4()
        {
            WindowTitle = "Page4: GradientSpreadMethod.Reflect";

            var btn = new Button
            {
                Content = "Next Page5"
            };
            btn.Click += btn4_Click;
            Content = btn;

            var brush = new LinearGradientBrush(
                Colors.Red, Colors.Blue, new Point(0, 0), new Point(0.25, 0.25))
            {
                SpreadMethod = GradientSpreadMethod.Reflect
            };

            btn.Background = brush;
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
                Page5 = new Page5();
            NavigationService.Navigate(Page5);
        }
    }
}
