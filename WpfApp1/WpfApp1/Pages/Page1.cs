using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page1 : Page
    {
        private readonly SolidColorBrush _brush = new SolidColorBrush(Colors.White);
        private readonly Button _btnPage1 = new Button();

        private Page Page2 { get; set; }

        public Page1()
        {
            WindowTitle = "Page1: Кисть SolidColorBrush";

            _btnPage1.Content = "Кнопка";
            _btnPage1.Background = _brush;

            _btnPage1.Click += btnPage1_Click;
            _btnPage1.MouseMove += btnPage1_MouseMove;
            Content = _btnPage1;
        }

        private void btnPage1_MouseMove(object sender, MouseEventArgs e)
        {
           
            var width = _btnPage1.ActualWidth;

            var height = _btnPage1.ActualHeight;

            var ptMouse = e.GetPosition(_btnPage1); 
            var ptCenter = new Point(width / 2, height / 2); 
            var vectorMouse = ptMouse - ptCenter; 
            var angle = Math.Atan2(vectorMouse.Y, vectorMouse.X);
            var vectorEllipse = new Vector(width / 2 * Math.Cos(angle),
                height / 2 * Math.Sin(angle)); 

            var byLevel = (byte)(255 * Math.Min(1, vectorMouse.Length / vectorEllipse.Length));

            var color = _brush.Color; 
            color.R = color.G = color.B = byLevel;
            _brush.Color = color; 
        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
            {
                Page2 = new Page2();
            } 
            NavigationService.Navigate(Page2);
        }
    }
}