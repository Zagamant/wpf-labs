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

namespace FullScreen
{
    public partial class MainWindow : Window
    {
        // Объявляем как поля для видимости в функциях
        bool fullScreen = false;// Состояние экрана
        WindowStyle windowStyle;
        WindowState windowState;
        ResizeMode resizeMode;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Запоминаем начальные параметры окна
            windowState = this.WindowState;
            windowStyle = this.WindowStyle;
            resizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
            this.ResizeMode = resizeMode;
            changeScreen.Header = "FullScreen";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FullScreen();
        }

        private void FullScreen()
        {
            if (!fullScreen)// Переходим в полноэкранный режим 
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    // Чтобы скрыть панель задач и не было мерцания
                    this.Hide();
                    this.WindowState = WindowState.Normal;
                }

                App.Current.MainWindow.WindowStyle = WindowStyle.None;// Без заголовка
                App.Current.MainWindow.Topmost = true;      // На передний план
                App.Current.MainWindow.WindowState = WindowState.Maximized;// Развернуть
                App.Current.MainWindow.ResizeMode = ResizeMode.NoResize;// Неизменяемое

                menu.Visibility = Visibility.Collapsed;// Скрываем меню
                HookSystemKeys.FunHook();// Запрещаем системные клавиши
                this.Visibility = Visibility.Visible;

                fullScreen = true;
                changeScreen.Header = "WindowScreen";
            }
            else // Восстанавливаем оконный режим
            {
                this.WindowStyle = windowStyle;
                this.Topmost = false;
                this.WindowState = windowState;
                this.ResizeMode = resizeMode;

                menu.Visibility = Visibility.Visible;// Показываем меню
                HookSystemKeys.FunUnHook();// Освобождаем системные клавиши

                fullScreen = false;
                changeScreen.Header = "FullScreen";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
