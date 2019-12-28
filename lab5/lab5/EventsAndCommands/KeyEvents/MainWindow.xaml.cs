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

namespace KeyEvents
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBox.Focus();
        }

        private void textBox_KeyEvent(object sender, KeyEventArgs e)
        {
            if ((bool)checkIgnoreRepeat.IsChecked && e.IsRepeat)
                return;// Игнорировать повторные события

            // Запретить действие в TextBox некоторых клавиш
            if (checkIgnoreOther.IsChecked.Value)
                switch (e.Key)
                { 
                    case Key.Space: // Пробел
                    case Key.Left:  // Стрелка влево
                    case Key.Right: // Стрелка вправо
                    case Key.Home:  // В начало поля
                    case Key.End:   // В конец поля
                        e.Handled = true;
                        break;
                }

            string key = e.Key.ToString();
            // Конвертируем вывод цифровых клавиш основной клавиатуры
            if (checkConvertNumber.IsChecked.Value)
            {
                KeyConverter converter = new KeyConverter();
                key = converter.ConvertToString(e.Key);
            }

            string message = e.RoutedEvent.ToString();
            message = message.Substring(message.IndexOf('.') + 1);
            message = String.Format("Event: {0,-25}", message) + "\t Key: " + key;
            listBox.Items.Add(message);
            listBox.ScrollIntoView(message);// Видеть последний
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем поле и список
            textBox.Clear();
            listBox.Items.Clear();

            // Возвращаем фокус 
            textBox.Focus();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            // Распознаем и синхронизируем взаимосвязанные CheckBox
            FrameworkElement checkBox = e.Source as FrameworkElement;
            switch (checkBox.Name)
            {
                case "checkIgnoreSymbol":
                    if (checkIgnoreSymbol.IsChecked.Value)
                        checkIgnorePreviewTextInput.IsChecked = false;
                    break;
                case "checkIgnorePreviewTextInput":
                    if (checkIgnorePreviewTextInput.IsChecked.Value)
                        checkIgnoreSymbol.IsChecked = false;
                    break;
            }

            // Возвращаем фокус 
            textBox.Focus();
        }

        // Для отображаемых символов текстового поля
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (checkIgnorePreviewTextInput.IsChecked.Value)
                return;

            // Запрещаем в TextBox нечисловые символы 
            short val;
            // Попытка преобразовать в число без генерации исключения
            bool success = Int16.TryParse(e.Text, out val);
            if (!success)
            {
                e.Handled = true;// Останавливаем событие
            }

            string message = e.RoutedEvent.ToString();
            message = message.Substring(message.IndexOf('.') + 1);
            message = String.Format("Event: {0,-25}", message) +
                "\t Text: " + e.Text;
            listBox.Items.Add(message);
        }
    }
}
