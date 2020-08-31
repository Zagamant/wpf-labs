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

namespace UserEvents
{
    public partial class MainWindow : Window
    {
        // Статический конструктор (без аргументов и перегрузок)
        static MainWindow()
        {
            // Зарегистрировать обработчик уровня любого класса дерева, который 
            // сработает перед всеми обработчиками экземпляра этого класса
            EventManager.RegisterClassHandler(
                typeof(MainWindow),
                MyButton.TapEvent,
                new RoutedEventHandler(SuperMethod1));

            EventManager.RegisterClassHandler(
                typeof(System.Windows.Controls.Primitives.UniformGrid),
                MyButton.TapEvent,
                new RoutedEventHandler(SuperMethod2));
        }

        // Обработчик уровня класса
        static void SuperMethod1(object sender, RoutedEventArgs e)
        {
            String typeName = sender.GetType().Name;
            Console.WriteLine(String.Format("{0})  {1}: Суперобработчик события Tab", ++MyButton.count, typeName));
        }

        // Обработчик уровня класса
        static void SuperMethod2(object sender, RoutedEventArgs e)
        {
            String typeName = sender.GetType().Name;
            Console.WriteLine(String.Format("{0})  {1}: Суперобработчик события Tab", ++MyButton.count, typeName));
        }

        // Конструктор экземпляра
        public MainWindow()
        {
            InitializeComponent();

            // Динамический способ присоединения обработчиков
            nWindow1.AddHandler(MyButton.TapEvent, new RoutedEventHandler(this.nWindow1_Tap));
            nDockPanel.AddHandler(MyButton.TapEvent, new RoutedEventHandler(this.nDockPanel_Tap));
        }

        private void MyButton_Tap(object sender, RoutedEventArgs e)
        {
            this.ShowTap(sender, e);
        }

        private void UniformGrid_Tap(object sender, RoutedEventArgs e)
        {
            this.ShowTap(sender, e);
        }

        private void Grid_Tap(object sender, RoutedEventArgs e)
        {
            this.ShowTap(sender, e);
        }

        private void nDockPanel_Tap(object sender, RoutedEventArgs e)
        {
            this.ShowTap(sender, e);
        }

        private void nWindow1_Tap(object sender, RoutedEventArgs e)
        {
            this.ShowTap(sender, e);
        }

        void ShowTap(object obj, RoutedEventArgs args)
        {
            if (MyButton.count == 0)
            {
                Console.WriteLine(
                    String.Format("\n\t Стратегия маршрутизации: {0}",
                    args.RoutedEvent.RoutingStrategy));
            }

            String typeName = obj.GetType().Name;

            // Повышаем полномочия ссылки и извлекаем сообщение
            MyEventArgs e = args as MyEventArgs;
            string message = e.Message;

            // Выводим информацию
            Console.WriteLine(
                String.Format("{0})  {1}: Наблюдаю событие Tap.\n"
                + "\tПолучил сообщение: {2}",
                ++MyButton.count, typeName, message));

            // Изменяем информацию в объекте-аргументе 
            e.Message = String.Format("Привет студентам от {0} и {1}!",
                typeName, args.RoutedEvent.OwnerType.Name);
        }
    }
}
