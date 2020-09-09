using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page5 : Page
    {
        // Переход на следующую страницу
        private Page6 _page6;

        public Page5()
        {
            WindowTitle = "Page5: Закраска цветами радуги";

            var btn = new Button();
            Content = btn;
            btn.Content = "Next Page6";
            btn.Click += btn5_Click;

            // Создание и присоединение градиента
            var brush = new LinearGradientBrush();
            btn.Background = brush;

            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(1, 0);

            // Цвета радуги
            brush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Orange, .17));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, .33));
            brush.GradientStops.Add(new GradientStop(Colors.Green, .5));
            brush.GradientStops.Add(new GradientStop(Colors.CornflowerBlue, .67));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, .84));
            brush.GradientStops.Add(new GradientStop(Colors.BlueViolet, 1));
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
                _page6 = new Page6(); // Создаем только один раз 
            NavigationService.Navigate(_page6);
        }
    }
}
