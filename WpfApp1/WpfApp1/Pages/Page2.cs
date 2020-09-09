using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Task1.Pages
{
    internal class Page2 : Page
    {
        private int _index; // Номер цвета

        // Переход на следующую страницу
        public Page3 Page3;
        private readonly PropertyInfo[] _props; // Массив свойств

        public Page2()
        {
            // Применяем рефлексию для чтения свойств класса Brushes
            _props = typeof(Brushes).GetProperties(
                BindingFlags.Public | BindingFlags.Static);

            // Компоновочная панель
            var stackPanel = new StackPanel(); // Создаем
            Content = stackPanel; // Присоединяем к странице

            var btn = new Button {Name = "ButtonNextColor", Content = "NextColor >"};
            btn.Click += btn_Click;
            stackPanel.Children.Add(btn); // Добавляем в панель

            btn = new Button
            {
                Content = "< PreviousColor"
            };
            btn.Click += btn_Click;
            stackPanel.Children.Add(btn);

            btn = new Button
            {
                Content = "Next Page3"
            };
            btn.Click += btnPage2_Click;
            stackPanel.Children.Add(btn);

            Loaded += Page2_Loaded;
        }

        private void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            // Вариант
            //this.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
            SetTitleAndBackground();
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // Вариант
            //SetTitleAndBackground();
        }

        // Обработчики кнопок смены заголовка и цвета страницы
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            // Распознаем кнопку по имени и корректируем индекс
            if(((Button)sender).Name == "ButtonNextColor")
                _index += 1;
            else
                _index += _props.Length - 1;

            _index %= _props.Length; // Деление по модулю
            SetTitleAndBackground();
        }

        // Установка заголовка и цвета фона страницы
        private void SetTitleAndBackground()
        {
            WindowTitle = "Page2: Имя цвета кисти - " + _props[_index].Name;
            Background = (Brush)_props[_index].GetValue(null, null);
        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
                Page3 = new Page3(); // Создаем только один раз 
            NavigationService.Navigate(Page3);
        }
    }

}
