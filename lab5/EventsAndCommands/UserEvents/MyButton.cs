using System.Windows;
using System.Windows.Controls;

namespace UserEvents
{
	// Класс определения пользовательского события Tap,
	// возбуждаемого по щелчку мыши на расширенной кнопке
	public class MyButton : Button
	{
		// Объявляем базовое поле события
		public static readonly RoutedEvent TapEvent;

		public static int count; // Счетчик перехвата события

		// Инициализируем в статическом конструкторе базовое поле события
		// (можно было инициализировать сразу при объявлении поля, без конструктора)
		static MyButton()
		{
			TapEvent = EventManager.RegisterRoutedEvent(
				"Tap", // Зарегистрированное имя
				RoutingStrategy.Bubble, // Стратегия перенаправления
				typeof(RoutedEventHandler), // Тип делегата обработчиков
				typeof(MyButton) // Тип владельца события
			);
		}

		// Контейнер для создания обработчиков в XAML
		public event RoutedEventHandler Tap
		{
			add => AddHandler(TapEvent, value);
			remove => RemoveHandler(TapEvent, value);
		}

		// Перекроем метод щелчка базовой кнопки для возбуждения события
		protected override void OnClick()
		{
			count = 0; // Сбрасываем счетчик
			//Console.Clear();    // Очищаем консоль
			//base.RaiseEvent(new RoutedEventArgs(MyButton.TapEvent));// Возбуждаем событие

			// Добавляем информацию и отсылаем
			var myEventArgs = new MyEventArgs(TapEvent);
			myEventArgs.Message = "Привет студентам от Снеткова!";
			RaiseEvent(myEventArgs); // Возбуждаем событие
		}
	}
}

namespace UserEvents
{
	// Расширяем класс аргументов
	internal class MyEventArgs : RoutedEventArgs
	{
		// Соблюдаем сигнатуру родителя
		public MyEventArgs(RoutedEvent routedEvent)
			: base(routedEvent)
		{
		}

		public string Message // Свойство чтения/записи
		{
			get;
			set;
		}
	}
}