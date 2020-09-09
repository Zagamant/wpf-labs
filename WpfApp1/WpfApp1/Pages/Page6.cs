using System.Windows.Controls;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page6 : Page
    {
        public Page6()
        {
            WindowTitle = "Page6: Радиальный градиент";

            // Создание и присоединение градиента
            var brush = new RadialGradientBrush();
            Background = brush;

            // Цвета радуги
            brush.GradientStops.Add(new GradientStop(Colors.Red, 0));
            brush.GradientStops.Add(new GradientStop(Colors.Orange, .17));
            brush.GradientStops.Add(new GradientStop(Colors.Yellow, .33));
            brush.GradientStops.Add(new GradientStop(Colors.Green, .5));
            brush.GradientStops.Add(new GradientStop(Colors.CornflowerBlue, .67));
            brush.GradientStops.Add(new GradientStop(Colors.Blue, .84));
            brush.GradientStops.Add(new GradientStop(Colors.BlueViolet, 1));
        }
    }
}
