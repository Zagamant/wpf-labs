using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Reflection;

namespace WpfApp1
{
    class CreateBrushes : NavigationWindow
    {
        // Точка входа
        [STAThread] // Атрибут для однопоточного приложения
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CreateBrushes());
        }

        // Конструктор
        public CreateBrushes()
        {
            this.Width = 300;
            this.Height = 300;
            this.Title = "Каркас приложения Упражнения 1";
            //this.ShowsNavigationUI = false;// Скрыть навигационный интерфейс

            // Создали начальную страницу
            Page1 page1 = new Page1();
            this.Content = page1;   // Поместили страницу в каркас
        }
    }

   
        // Класс-расширение страницы Page1
        class Page1 : Page
        {
            // Создаем и инициализируем поле кисти белым цветом
            SolidColorBrush brush = new SolidColorBrush(Colors.White);
            Button btnPage1 = new Button();

            public Page1()
            {
                this.WindowTitle = "Page1: Кисть SolidColorBrush";

                // Создали кнопку
                btnPage1.Content = "Кнопка";
                btnPage1.Background = brush;
                // Зарегистрировали обработчики
                btnPage1.Click += new RoutedEventHandler(btnPage1_Click);
                btnPage1.MouseMove += new MouseEventHandler(btnPage1_MouseMove);
                this.Content = btnPage1;    // Поместили кнопку в страницу
            }

            // Меняет цвет фона кнопки на Page1
            void btnPage1_MouseMove(object sender, MouseEventArgs e)
            {
                // Чистая ширина клиентской области без рамки окна
                double width = btnPage1.ActualWidth;
                // Чистая высота клиентской области без рамки и заголовка окна
                double height = btnPage1.ActualHeight;

                Point ptMouse = e.GetPosition(btnPage1);// Точка курсора на клиентской области окна
                Point ptCenter = new Point(width / 2, height / 2);// Центр клиентской области
                Vector vectMouse = ptMouse - ptCenter;// Отклонение курсора от центра
                double angle = Math.Atan2(vectMouse.Y, vectMouse.X);// Угол нахождения курсора
                Vector vectEllipse = new Vector(width / 2 * Math.Cos(angle),
                    height / 2 * Math.Sin(angle));// Вписанный эллипс

                // Изоклина (в центре кнопки черная), округляется до одного байта 
                Byte byLevel = (byte)(255 * (Math.Min(1, vectMouse.Length / vectEllipse.Length)));
                Color color = brush.Color;// Привязываем к полю
                color.R = color.G = color.B = byLevel;// Цвета, пропорциональные изоклине
                brush.Color = color;// Меняем цвет фона окна изоклиной от равномерного черного цвета
            }

            // Переход на следующую страницу
            Page2 page2;
            void btnPage1_Click(object sender, RoutedEventArgs e)
            {
                if (!this.NavigationService.CanGoForward)
                    page2 = new Page2();// Создаем только один раз 
                this.NavigationService.Navigate(page2);

            }
        }

        // Класс-расширение страницы Page2
        class Page2 : Page
        {
            int index = 0;// Номер цвета
            PropertyInfo[] props;// Массив свойств

            public Page2()
            {
                // Применяем рефлексию для чтения свойств класса Brushes
                props = typeof(Brushes).GetProperties(
                    BindingFlags.Public | BindingFlags.Static);

                // Компоновочная панель
                StackPanel stackPanel = new StackPanel();// Создаем
                this.Content = stackPanel;// Присоединяем к странице

                Button btn = new Button();
                btn.Name = "ButtonNextColor";
                btn.Content = "NextColor >";
                btn.Click += new RoutedEventHandler(btn_Click);
                stackPanel.Children.Add(btn);// Добавляем в панель

                btn = new Button();
                btn.Content = "< PreviousColor";
                btn.Click += new RoutedEventHandler(btn_Click);
                stackPanel.Children.Add(btn);

                btn = new Button();
                btn.Content = "Next Page3";
                btn.Click += new RoutedEventHandler(btnPage2_Click);
                stackPanel.Children.Add(btn);

                // Возбуждается при каждом отображении страницы
                this.Loaded += new RoutedEventHandler(Page2_Loaded);
            }

            void Page2_Loaded(object sender, RoutedEventArgs e)
            {
                // Вариант
                //this.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
                SetTitleAndBackground();
            }

            void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
            {
                // Вариант
                //SetTitleAndBackground();
            }

            // Обработчики кнопок смены заголовка и цвета страницы
            void btn_Click(object sender, RoutedEventArgs e)
            {
                // Распознаем кнопку по имени и корректируем индекс
                if (((Button)sender).Name == "ButtonNextColor")
                    index += 1;
                else
                    index += props.Length - 1;

                index %= props.Length;// Деление по модулю
                SetTitleAndBackground();
            }

            // Установка заголовка и цвета фона страницы
            void SetTitleAndBackground()
            {
                this.WindowTitle = "Page2: Имя цвета кисти - " + props[index].Name;
                this.Background = (Brush)props[index].GetValue(null, null);
            }

            // Переход на следующую страницу
            Page3 page3;
            void btnPage2_Click(object sender, RoutedEventArgs e)
            {
                if (!this.NavigationService.CanGoForward)
                    page3 = new Page3();// Создаем только один раз 
                this.NavigationService.Navigate(page3);
            }

        }

        class Page3 : Page
        {
            public Page3()
            {
                this.WindowTitle = "Page3: Кисть LinearGradientBrush";

                Button btn = new Button();
                btn.Content = "Next Page4";
                btn.Click += new RoutedEventHandler(btn3_Click);
                this.Content = btn;

                // Создание и присоединение градиента
                LinearGradientBrush brush = new LinearGradientBrush(
                    Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
                btn.Background = brush;
            }

            // Переход на следующую страницу
            Page4 page4;
            void btn3_Click(object sender, RoutedEventArgs e)
            {
                if (!this.NavigationService.CanGoForward)
                    page4 = new Page4();// Создаем только один раз 
                this.NavigationService.Navigate(page4);
            }

        }


        class Page4 : Page
        {
            public Page4()
            {
                this.WindowTitle = "Page4: GradientSpreadMethod.Reflect";

                Button btn = new Button();
                btn.Content = "Next Page5";
                btn.Click += new RoutedEventHandler(btn4_Click);
                this.Content = btn;

                // Создание и присоединение градиента
                LinearGradientBrush brush = new LinearGradientBrush(
                    Colors.Red, Colors.Blue, new Point(0, 0), new Point(0.25, 0.25));
                brush.SpreadMethod = GradientSpreadMethod.Reflect;

                btn.Background = brush;
            }

            // Переход на следующую страницу
            Page5 page5;
            void btn4_Click(object sender, RoutedEventArgs e)
            {
                if (!this.NavigationService.CanGoForward)
                    page5 = new Page5();// Создаем только один раз 
                this.NavigationService.Navigate(page5);
            }

        }

        class Page5 : Page
        {
            public Page5()
            {
                this.WindowTitle = "Page5: Закраска цветами радуги";

                Button btn = new Button();
                this.Content = btn;
                btn.Content = "Next Page6";
                btn.Click += new RoutedEventHandler(btn5_Click);

                // Создание и присоединение градиента
                LinearGradientBrush brush = new LinearGradientBrush();
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

            // Переход на следующую страницу
            Page6 page6;
            void btn5_Click(object sender, RoutedEventArgs e)
            {
                if (!this.NavigationService.CanGoForward)
                    page6 = new Page6();   // Создаем только один раз 
                this.NavigationService.Navigate(page6);
            }

        }

        class Page6 : Page
        {
            public Page6()
            {
                this.WindowTitle = "Page6: Радиальный градиент";

                // Создание и присоединение градиента
                RadialGradientBrush brush = new RadialGradientBrush();
                this.Background = brush;

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

