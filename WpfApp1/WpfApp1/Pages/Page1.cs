using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Task1.Pages
{
    internal class Page1 : Page
    {
        // Создаем и инициализируем поле кисти белым цветом
        private readonly SolidColorBrush _brush = new SolidColorBrush(Colors.White);
        private readonly Button _btnPage1 = new Button();

        // Переход на следующую страницу
        private Page2 _page2;

        public Page1()
        {
            WindowTitle = "Page1: Кисть SolidColorBrush";

            // Создали кнопку
            _btnPage1.Content = "Кнопка";
            _btnPage1.Background = _brush;
            // Зарегистрировали обработчики
            _btnPage1.Click += btnPage1_Click;
            _btnPage1.MouseMove += btnPage1_MouseMove;
            Content = _btnPage1; // Поместили кнопку в страницу
        }

        // Меняет цвет фона кнопки на Page1
        private void btnPage1_MouseMove(object sender, MouseEventArgs e)
        {
            // Чистая ширина клиентской области без рамки окна
            var width = _btnPage1.ActualWidth;
            // Чистая высота клиентской области без рамки и заголовка окна
            var height = _btnPage1.ActualHeight;

            var ptMouse = e.GetPosition(_btnPage1); // Точка курсора на клиентской области окна
            var ptCenter = new Point(width / 2, height / 2); // Центр клиентской области
            var vectMouse = ptMouse - ptCenter; // Отклонение курсора от центра
            var angle = Math.Atan2(vectMouse.Y, vectMouse.X); // Угол нахождения курсора
            var vectEllipse = new Vector(width / 2 * Math.Cos(angle),
                height / 2 * Math.Sin(angle)); // Вписанный эллипс

            // Изоклина (в центре кнопки черная), округляется до одного байта 
            var byLevel = (byte)(255 * Math.Min(1, vectMouse.Length / vectEllipse.Length));
            var color = _brush.Color; // Привязываем к полю
            color.R = color.G = color.B = byLevel; // Цвета, пропорциональные изоклине
            _brush.Color = color; // Меняем цвет фона окна изоклиной от равномерного черного цвета
        }

        private void btnPage1_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
                _page2 = new Page2(); // Создаем только один раз 
            NavigationService.Navigate(_page2);
        }
    }
}