using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LibraryResource;// Подключаем пространство имен библиотеки с ресурсами

namespace UseLibraryResource
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Инициализация интерфейсных элементов
            InitializeComponent();

            // Создаем и добавляем программно объекты ресурсов в коллекцию окна
            //
            // ... alternateColor
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Colors.Blue;
            this.Resources.Add("alternateColor", solidColorBrush);
            //
            // ... alternateImage
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(
                new Uri(@"Images\dog.png", UriKind.Relative)
                );
            imageBrush.TileMode = TileMode.Tile;
            imageBrush.ViewportUnits = BrushMappingMode.Absolute;// Не путать с ViewboxUnits
            imageBrush.Viewport = new Rect(0, 0, 32, 32);
            imageBrush.Opacity = 0.5D;// Можно и без D, преобразует неявно из float в double
            this.Resources.Add("alternateImage", imageBrush);

            // Регистрируем обработчики программно
            btn1.Click += btn1_Click;
            btn2.Click += btn2_Click;
        }

        // Используем внешние ресурсы
        void btn1_Click(object sender, RoutedEventArgs e)
        {
            CustomResources.changeResourceKeyFlag =
                !CustomResources.changeResourceKeyFlag;
            btn1.Foreground = btn1.TryFindResource(
                CustomResources.ForegroundBrushKey) as SolidColorBrush;
            btn1.Background = (ImageBrush)btn1.TryFindResource(
                CustomResources.BackgroundBrushKey);
        }

        // Используем внешние ресурсы
        void btn2_Click(object sender, RoutedEventArgs e)
        {
            CustomResources.changeResourceKeyFlag =
                !CustomResources.changeResourceKeyFlag;
            try
            {
                btn2.Foreground = btn2.FindResource(
                    CustomResources.ForegroundBrushKey) as SolidColorBrush;
                btn2.Background = (ImageBrush)btn2.FindResource(
                    CustomResources.BackgroundBrushKey);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        bool changeResourceFlag2 = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Приводим ссылку к типу Button
            Button btn = sender as Button;
            if (btn == null)
                return;

            if (changeResourceFlag2)
            {
                // Используем внутренние ресурсы окна
                btn.Foreground = (SolidColorBrush)this.Resources["alternateColor"];
                btn.Background = (ImageBrush)this.Resources["alternateImage"];
            }
            else
            {
                // Используем внешние ресурсы
                btn.Foreground = btn.TryFindResource(
                    CustomResources.ForegroundBrushKey) as SolidColorBrush;
                btn.Background = (ImageBrush)btn.TryFindResource(
                    CustomResources.BackgroundBrushKey);
            }
            changeResourceFlag2 = !changeResourceFlag2;// Готовим другой вариант
        }
    }
}
