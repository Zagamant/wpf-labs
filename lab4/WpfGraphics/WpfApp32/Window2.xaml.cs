using System.Windows;

namespace Task3
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            // Инициализация разметочной части
            InitializeComponent();

            // Корректировка заголовка окна
            this.Title += "=\"Так голодают буржуины!\"";

            // Дочернее окно не отображать в панели задач ОС 
            this.ShowInTaskbar = false;
        }

    }
}
