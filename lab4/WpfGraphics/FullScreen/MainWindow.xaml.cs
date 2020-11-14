using System.Windows;
using System.Windows.Input;

namespace Task2
{
	public partial class MainWindow : Window
	{
		// Объявляем как поля для видимости в функциях
		private bool _fullScreen; // Состояние экрана
		private ResizeMode _resizeMode;
		private WindowState _windowState;
		private WindowStyle _windowStyle;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Запоминаем начальные параметры окна
			_windowState = WindowState;
			_windowStyle = WindowStyle;
			_resizeMode = ResizeMode.CanResizeWithGrip;
			ResizeMode = _resizeMode;
			ChangeScreen.Header = "FullScreen";
		}

		private void MenuItem_Click(object sender, RoutedEventArgs e)
		{
			FullScreen();
		}

		private void FullScreen()
		{
			if (!_fullScreen) // Переходим в полноэкранный режим 
			{
				if (WindowState == WindowState.Maximized)
				{
					// Чтобы скрыть панель задач и не было мерцания
					Hide();
					WindowState = WindowState.Normal;
				}

				Application.Current.MainWindow.WindowStyle = WindowStyle.None; // Без заголовка
				Application.Current.MainWindow.Topmost = true; // На передний план
				Application.Current.MainWindow.WindowState = WindowState.Maximized; // Развернуть
				Application.Current.MainWindow.ResizeMode = ResizeMode.NoResize; // Неизменяемое

				Menu.Visibility = Visibility.Collapsed; // Скрываем меню
				HookSystemKeys.FunHook(); // Запрещаем системные клавиши
				Visibility = Visibility.Visible;

				_fullScreen = true;
				ChangeScreen.Header = "WindowScreen";
			}
			else // Восстанавливаем оконный режим
			{
				WindowStyle = _windowStyle;
				Topmost = false;
				WindowState = _windowState;
				ResizeMode = _resizeMode;

				Menu.Visibility = Visibility.Visible; // Показываем меню
				HookSystemKeys.FunUnHook(); // Освобождаем системные клавиши

				_fullScreen = false;
				ChangeScreen.Header = "FullScreen";
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Close();
		}
	}
}