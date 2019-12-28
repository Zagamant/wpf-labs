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
    // Класс определения пользовательского события Tap,
    // возбуждаемого по щелчку мыши на расширенной кнопке
    public class MyButton : Button
    {
        // Объявляем базовое поле события
        public static readonly RoutedEvent TapEvent;

        // Инициализируем в статическом конструкторе базовое поле события
        // (можно было инициализировать сразу при объявлении поля, без конструктора)
        static MyButton()
        {
            TapEvent = EventManager.RegisterRoutedEvent(
                "Tap",                          // Зарегистрированное имя
                RoutingStrategy.Bubble,         // Стратегия перенаправления
                typeof(RoutedEventHandler),     // Тип делегата обработчиков
                typeof(MyButton)                // Тип владельца события
                );
        }

        // Контейнер для создания обработчиков в XAML
        public event RoutedEventHandler Tap
        {
            add { base.AddHandler(TapEvent, value); }
            remove { base.RemoveHandler(TapEvent, value); }
        }

        public static int count;        // Счетчик перехвата события

        // Перекроем метод щелчка базовой кнопки для возбуждения события
        protected override void OnClick()
        {
            count = 0;          // Сбрасываем счетчик
                                //Console.Clear();    // Очищаем консоль
                                //base.RaiseEvent(new RoutedEventArgs(MyButton.TapEvent));// Возбуждаем событие

            // Добавляем информацию и отсылаем
            MyEventArgs myEventArgs = new MyEventArgs(MyButton.TapEvent);
            myEventArgs.Message = "Привет студентам от Снеткова!";
            base.RaiseEvent(myEventArgs);// Возбуждаем событие
        }
    }
}

namespace UserEvents
{
    // Расширяем класс аргументов
    class MyEventArgs : RoutedEventArgs
    {
        // Соблюдаем сигнатуру родителя
        public MyEventArgs(RoutedEvent routedEvent)
            : base(routedEvent)
        {
        }

        string message;// Базовое поле свойства

        public string Message // Свойство чтения/записи
        {
            get { return message; }
            set { message = value; }
        }
    }
}
