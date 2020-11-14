using System;
using System.Windows;

namespace Task3
{
	/// <summary>
	///     Логика взаимодействия для Window2.xaml
	/// </summary>
	public partial class Window2 : Window
	{
		public Window2()
		{
			// Инициализация разметочной части
			InitializeComponent();

			// Корректировка заголовка окна
			Title += "=\"Так голодают буржуины!\"";

			// Дочернее окно не отображать в панели задач ОС 
			ShowInTaskbar = false;

			// Регистрация обработчика
			Closed += Window2_Closed;
		}

		private void Window2_Closed(object sender, EventArgs e)
		{
			MainWindow.Wnd2 = null; // Для предотвращения повторного запуска
			GC.WaitForFullGCComplete(); // Ждать завершения сборки мусора
			GC.Collect(); // Начать сборку мусора
		}
	}
}