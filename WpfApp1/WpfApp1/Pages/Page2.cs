using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Task1.Pages
{
    internal class Page2 : Page
    {
        private int ColorIndex { get; set; }
        public Page Page3 { get; set; }
        private readonly PropertyInfo[] _props; 

        public Page2()
        {
            _props = typeof(Brushes).GetProperties(
                BindingFlags.Public | BindingFlags.Static);

            var stackPanel = new StackPanel();
            Content = stackPanel;

            var btn = new Button {Name = "ButtonNextColor", Content = "NextColor >"};
            btn.Click += btn_Click;
            stackPanel.Children.Add(btn);

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
            ColorIndex += ((Button) sender).Name == "ButtonNextColor" ? 1 : _props.Length - 1;

            ColorIndex %= _props.Length;
            SetTitleAndBackground();
        }

        private void SetTitleAndBackground()
        {
            WindowTitle = "Page2: Имя цвета кисти - " + _props[ColorIndex].Name;
            Background = (Brush)_props[ColorIndex].GetValue(null, null);
        }

        private void btnPage2_Click(object sender, RoutedEventArgs e)
        {
            if(!NavigationService.CanGoForward)
            {
                Page3 = new Page3();
            }

            NavigationService.Navigate(Page3);
        }
    }

}
