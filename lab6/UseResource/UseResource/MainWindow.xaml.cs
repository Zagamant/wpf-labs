
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
    
namespace UseResource
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Инициализация интерфейсных элементов
            InitializeComponent();
    
            // Создаем и добавляем программно объекты ресурсов 
            // для будущей замены декларативных ресурсов целиком
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
    
            // Регистрируем часть обработчиков программно
            btn1.Click += btn1_Click;
            btn2.Click += btn2_Click;
    
            /*
            // Эти ресурсы не будем сохранять, потому что мы их будем только модифицировать
            resourceDictionary.Add("ForegroundBrush1", this.Resources["ForegroundBrush1"]);
            resourceDictionary.Add("BackgroundBrush1", this.Resources["BackgroundBrush1"]);
            //*/
    
            // Заполняем словарь ресурсов: дублируем ссылки на ресурсы 
            resourceDictionary.Add("originColor", this.Resources["ForegroundBrush2"]);
            resourceDictionary.Add("originImage", this.Resources["BackgroundBrush2"]);
        }
        // Поле-ссылка на объект, может объявляться в любом месте как член класса
        ResourceDictionary resourceDictionary = new ResourceDictionary();
    
        // Просто модифицируем объект ресурса
        bool changeResourceFlag1 = true;
        void btn1_Click(object sender, RoutedEventArgs e)
        {
            // Извлекаем ресурсы из словаря окна и приводим к типу двумя вариантами
            SolidColorBrush colorRes = (SolidColorBrush)this.Resources["ForegroundBrush1"];
            ImageBrush imageRes = this.Resources["BackgroundBrush1"] as ImageBrush; 
    
            if (changeResourceFlag1)// Редактируем существующий объект ресурса
            {
                colorRes.Color = Colors.Red;
                imageRes.Viewport = new Rect(0, 0, 16, 16);
            }
            else                    // Возвращаем к прежнему
            {
                colorRes.Color = Colors.Blue;
                imageRes.Viewport = new Rect(0, 0, 10, 10);
                /*
                // Восстанавливать из словаря нельзя - ссылаются на один и тот же объект
                colorRes = resourceDictionary["ForegroundBrush1"];
                imageRes = resourceDictionary["BackgroundBrush1"];
                //*/
            }
            changeResourceFlag1 = !changeResourceFlag1;// Готовим другой вариант
        }
    
        // Просто модифицируем объект ресурса
        void btn2_Click(object sender, RoutedEventArgs e)
        {
            // Ищем ресурс по ключу, начиная с текущего элемента к корню дерева
            // Метод TryFindResource защищать не нужно, потому что при неудаче он 
            // не генерирует исключение, а просто возвращает нулевую ссылку
            SolidColorBrush colorRes = ((FrameworkElement)sender).
                TryFindResource("ForegroundBrush1") as SolidColorBrush;
            ImageBrush imageRes = (ImageBrush)((FrameworkElement)sender).
                TryFindResource("BackgroundBrush1");
    
            if (colorRes == null || imageRes == null)// Ресурс не найден
                return;
    
            if (changeResourceFlag1)// Редактируем существующий объект ресурса
            {
                colorRes.Color = Colors.Red;
                imageRes.Viewport = new Rect(0, 0, 16, 16);
            }
            else                    // Возвращаем к прежнему
            {
                colorRes.Color = Colors.Blue;
                imageRes.Viewport = new Rect(0, 0, 10, 10);
            }
            changeResourceFlag1 = !changeResourceFlag1;// Готовим другой вариант
        }
    
        // Заменяем ресурс новым объектом
        bool changeResourceFlag2 = true;
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (changeResourceFlag2)// Полностью заменяем объекты ресурсов на новые
            {
                this.Resources["ForegroundBrush2"] = this.Resources["alternateColor"];
                this.Resources["BackgroundBrush2"] = this.Resources["alternateImage"];
            }
            else                    // Восстанавливаем объекты из словаря ресурсов
            {
                this.Resources["ForegroundBrush2"] = resourceDictionary["originColor"];
                this.Resources["BackgroundBrush2"] = resourceDictionary["originImage"];
            }
            changeResourceFlag2 = !changeResourceFlag2;// Готовим другой вариант
        }
    
        // Заменяем ресурс новым объектом
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            // Повышаем полномочия ссылки, чтобы применить метод FindResource. Можно повысить
            // только до FrameworkElement, там уже есть методы FindResource и TryFindResource 
            Button btn = (Button)sender;
    
            // Объявляем локальные ссылки
            Object colorRes, imageRes;
    
            if (changeResourceFlag2)// Полностью заменяем объекты ресурсов на новые
            {
                // Защищенно ищем ресурс методом FindResource, который 
                // при отсутствии ресурса генерирует исключение
                try
                {
                    colorRes = btn.FindResource("alternateColor");
                    imageRes = btn.FindResource("alternateImage");
                }
                catch { return; }   // Если не нашли, все оставляем как есть
    
                this.Resources["ForegroundBrush2"] = colorRes;
                this.Resources["BackgroundBrush2"] = imageRes;
            }
            else                    // Восстанавливаем объекты из словаря ресурсов
            {
                this.Resources["ForegroundBrush2"] = resourceDictionary["originColor"];
                this.Resources["BackgroundBrush2"] = resourceDictionary["originImage"];
            }
            changeResourceFlag2 = !changeResourceFlag2;// Готовим другой вариант
        }
    }
}

