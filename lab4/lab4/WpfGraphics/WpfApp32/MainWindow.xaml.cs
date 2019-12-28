using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp32
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Инициализация разметочной части
            InitializeComponent();

            // Корректировка заголовка окна
            this.Title += "=\"Так голодают буржуины!\"";

            // Всплывающая подсказка
            this.ToolTip = "Вызывайте дочернее окно\n"
                + "двойным щелчком мыши\n"
                + "или контекстным меню...";

            // Сделать главным окном приложения
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.MainWindow = this;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Размеры всех рисунков одинаковы
            const int WIDTH = 348, HEIGHT = 232;

            // Создаем накопитель рисунков DrawingGroup 
            DrawingGroup drawingGroup = new DrawingGroup();

            // Левый верхний 
            ImageDrawing pict1 = new ImageDrawing();
            pict1.Rect = new Rect(0, 0, WIDTH, HEIGHT);
            pict1.ImageSource = new BitmapImage(new Uri(@"Images\market 040.jpg", UriKind.Relative));
            drawingGroup.Children.Add(pict1);

            // Правый верхний 
            ImageDrawing pict2 = new ImageDrawing();
            pict2.Rect = new Rect(350, 0, WIDTH, HEIGHT);
            pict2.ImageSource = new BitmapImage(new Uri(@"Images\market 039.jpg", UriKind.Relative));
            drawingGroup.Children.Add(pict2);

            // Левый нижний
            ImageDrawing pict3 = new ImageDrawing();
            pict3.Rect = new Rect(0, 234, WIDTH, HEIGHT);
            pict3.ImageSource = new BitmapImage(new Uri(@"Images\market 034.jpg", UriKind.Relative));
            drawingGroup.Children.Add(pict3);

            // Правый нижний
            ImageDrawing pict4 = new ImageDrawing();
            pict4.Rect = new Rect(350, 234, WIDTH, HEIGHT);
            pict4.ImageSource = new BitmapImage(new Uri(@"Images\market 032.jpg", UriKind.Relative));
            drawingGroup.Children.Add(pict4);

            // Передать рисовальщику
            DrawingImage drawingImageSource = new DrawingImage(drawingGroup);

            // Заморозить DrawingImage для лучшей производительности
            drawingImageSource.Freeze();

            // Передать элементу отображения
            Image image = new Image();
            image.Stretch = Stretch.None;
            image.Source = drawingImageSource;

            // Контейнер Border для присоединения к содержимому окна
            Border border = new Border();
            border.Background = Brushes.White;
            border.BorderBrush = Brushes.White;
            border.BorderThickness = new Thickness(2);  // Толщина рамки
            border.Margin = new Thickness(10);          // Внешний отступ-поле
            border.Child = image;                       // Отдать родителю

            this.Background = Brushes.Blue;
            this.Content = border;
        }
        Window wnd2;
        private void Show_Window2(object sender, MouseButtonEventArgs e)
        {
            bool windowExists = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "Window_2")
                {
                    windowExists = true;
                    window.Activate();// Сдвинуть на передний план
                    break;
                }
            }

            if (!windowExists)
            {
                wnd2 = new Window2();
                wnd2.Show();
            }
        }

        // Предотвращение повторного открытия окна: Способ 2
        // Обработчик контекстного меню
        private void Create_Window2(object sender, RoutedEventArgs e)
        {
            bool windowExists = false;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name == "Window_2")
                {
                    windowExists = true;
                    window.Activate();// Сдвинуть на передний план
                    break;
                }
            } 

            if (!windowExists)
            {
                wnd2 = new Window2();
                wnd2.Show();
            }
        }
    }
}
