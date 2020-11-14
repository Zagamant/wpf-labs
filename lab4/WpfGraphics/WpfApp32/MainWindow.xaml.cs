using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Task3
{
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Window _wnd2;

		public MainWindow()
		{
			// Инициализация разметочной части
			InitializeComponent();

			// Корректировка заголовка окна
			Title += "=\"Так голодают буржуины!\"";

			// Всплывающая подсказка
			ToolTip = "Вызывайте дочернее окно\n"
			          + "двойным щелчком мыши\n"
			          + "или контекстным меню...";

			// Сделать главным окном приложения
			Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			Application.Current.MainWindow = this;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Размеры всех рисунков одинаковы
			const int width = 348, 
				height = 232;

			// Создаем накопитель рисунков DrawingGroup 
			var drawingGroup = new DrawingGroup();

			// Левый верхний 
			var pict1 = new ImageDrawing
			{
				Rect = new Rect(0, 0, width, height),
				ImageSource = new BitmapImage(new Uri(@"Images\market 040.jpg", UriKind.Relative))
			};
			drawingGroup.Children.Add(pict1);

			// Правый верхний 
			var pict2 = new ImageDrawing
			{
				Rect = new Rect(350, 0, width, height),
				ImageSource = new BitmapImage(new Uri(@"Images\market 039.jpg", UriKind.Relative))
			};
			drawingGroup.Children.Add(pict2);

			// Левый нижний
			var pict3 = new ImageDrawing
			{
				Rect = new Rect(0, 234, width, height),
				ImageSource = new BitmapImage(new Uri(@"Images\market 034.jpg", UriKind.Relative))
			};
			drawingGroup.Children.Add(pict3);

			// Правый нижний
			var pict4 = new ImageDrawing
			{
				Rect = new Rect(350, 234, width, height),
				ImageSource = new BitmapImage(new Uri(@"Images\market 032.jpg", UriKind.Relative))
			};
			drawingGroup.Children.Add(pict4);

			// Передать рисовальщику
			var drawingImageSource = new DrawingImage(drawingGroup);

			// Заморозить DrawingImage для лучшей производительности
			drawingImageSource.Freeze();

			// Передать элементу отображения
			var image = new Image
			{
				Stretch = Stretch.None, 
				Source = drawingImageSource
			};

			// Контейнер Border для присоединения к содержимому окна
			var border = new Border
			{
				Background = Brushes.White,
				BorderBrush = Brushes.White,
				BorderThickness = new Thickness(2),
				Margin = new Thickness(10),
				Child = image
			};
			// Толщина рамки
			// Внешний отступ-поле
			// Отдать родителю

			Background = Brushes.Blue;
			Content = border;
		}

		private void Show_Window2(object sender, MouseButtonEventArgs e)
		{
			var windowExists = false;
			foreach (Window window in Application.Current.Windows)
				if (window.Name == "Window_2")
				{
					windowExists = true;
					window.Activate(); // Сдвинуть на передний план
					break;
				}

			if (windowExists) return;
			_wnd2 = new Window2();
			_wnd2.Show();
		}

		// Предотвращение повторного открытия окна: Способ 2
		// Обработчик контекстного меню
		private void Create_Window2(object sender, RoutedEventArgs e)
		{
			var windowExists = false;
			foreach (Window window in Application.Current.Windows)
				if (window.Name == "Window_2")
				{
					windowExists = true;
					window.Activate(); // Сдвинуть на передний план
					break;
				}

			if (windowExists) return;
			_wnd2 = new Window2();
			_wnd2.Show();
		}
	}
}