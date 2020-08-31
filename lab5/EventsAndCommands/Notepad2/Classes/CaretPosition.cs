using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace Notepad2
{
    // Часть класса главного окна
    partial class MainWindow
    {
        // Создаем экземпляр и регистрируем обработчики
        CaretPosition caretPosition;
        private void CreateCaretPosition()
        {
            // Отображение в StatusBar номера строки и столбца
            caretPosition = new CaretPosition();// Создаем объект
            caretPosition.TxtBox = txtBox1;     // Присоединяем TextBox

            // Дополняем StatusBar
            this.statusBar.Items.Add(
                new System.Windows.Controls.Separator());
            this.statusBar.Items.Add(caretPosition.StrLineCol);
            // Увековечиваем себя!
            this.statusBar.Items.Insert(0, new System.Windows.Controls.Separator());
            this.statusBar.Items.Insert(0, "Снетков В.М.");

            // Регистрируем обработчик события перемещения каретки
            txtBox1.SelectionChanged +=
                new RoutedEventHandler(txtBox1_SelectionChanged);
        }

        // Обработчик инициирует вычисление и отрисовку нового положения
        void txtBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            caretPosition.CaretChanged();
        }
    }

    class CaretPosition
    {
        // Закрытые поля
        StatusBarItem strLineCol = new StatusBarItem();
        TextBox txtBox = new TextBox();

        // Открытые свойства
        // Для добавления в строку состояния клиента
        public StatusBarItem StrLineCol // Папа '-->'
        {
            // Только для чтения
            get { return strLineCol; }
        }
        // Для присоединения к TextBox клиента
        public TextBox TxtBox // Мама '>--'
        {
            // Только для записи
            set { txtBox = value; }
        }

        // Вычисляет номер строки
        int GetLine()
        {
            int count = 0;
            int pos = 0;
            int caretPos = txtBox.SelectionStart + 1; //txtBox1.CaretIndex
            while (pos < caretPos)
            {
                count++;        // Счетчик строк
                pos = txtBox.Text.IndexOf("\r\n", pos);// \n - перевод строки
                if (pos != -1)  // Нашли очередную пару
                    pos += 2;   // Сдвигаемся правее найденных
                else
                    break;// Больше нет
            }
            return count;
        }

        public void CaretChanged()
        {
            if (!txtBox.IsFocused)
                return;
            int posChar = txtBox.CaretIndex;
            int line = GetLine();
            int column = posChar - txtBox.GetCharacterIndexFromLineIndex(line - 1) + 1;

            // Обновляем в строке состояния
            strLineCol.Content = String.Format(" Ln {0} \t Col {1}", line, column);
        }
    }
}
