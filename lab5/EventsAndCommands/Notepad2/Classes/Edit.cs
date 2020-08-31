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

namespace Notepad2
{
    partial class MainWindow
    {
        //------------------------------------------------------
        //
        //  Обработчики источников задач Edit
        //
        //------------------------------------------------------

        private void UndoOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.Undo();
            if (!txtBox1.CanUndo)
                IsModified = false;
        }


        private void RedoOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.Redo();
        }

        private void CutOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.Cut();

            // Вариант
            //Clipboard.SetText(txtBox1.SelectedText);
            //txtBox1.SelectedText = "";
        }

        private void CopyOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.Copy();

            // Вариант
            //Clipboard.SetText(txtBox1.SelectedText);
        }

        private void PasteOnExecute(object sender, RoutedEventArgs e)
        {
            // Если в буфере содержатся данные текстового формата
            if (Clipboard.ContainsText())
                txtBox1.Paste();
        }

        private void DeleteOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.SelectedText = String.Empty;
        }

        private void FindOnExecute(object sender, RoutedEventArgs e)
        {
            CreateDialog();
        }

        private void FindNextOnExecute(object sender, RoutedEventArgs e)
        {
            if (_dlg == null)
                CreateDialog();
            else
                FindNextExec();
        }

        private void ReplaceOnExecute(object sender, RoutedEventArgs e)
        {
            if (_dlg == null)
            {
                CreateDialog();
                _dlg.ShowReplace = true;
            }
            else if (_dlg.ShowReplace == false)
                _dlg.ShowReplace = true;
            else
                ReplaceExec();
        }

        #region Задача Go To
        private void GoToOnExecute(object sender, RoutedEventArgs e)
        {
            GoToDialog dlg = new GoToDialog();
            dlg.Owner = this;
            dlg.LineNumber = CaretLineNumber;
            /***** Врет для завернутых строк Word Wrap! ******************
            dlg.MaxLineNumber = txtBox1.LineCount; 
            /*************************************************************/
            dlg.MaxLineNumber = GetMaxNumber();

            // Параметры анонимного обработчика "от фонаря", 
            // все равно не используем
            dlg.GotoActivate += delegate (object sender2, EventArgs e2)
            {
                // Заполняем текстовое поле диалога текущей строкой
                dlg.LineNumber = CaretLineNumber;
            };

            // Запускаем в модальном режиме. Ждет и закрывается
            if (dlg.ShowDialog() == true)
            {
                // Устанавливаем каретку (курсор) в начало строки
                CaretLineNumber = dlg.LineNumber;
            }

            txtBox1.Focus();
        }

        int GetMaxNumber()
        {
            int count = 0;
            int pos = 0;
            int caretPos = txtBox1.Text.Length + 1;
            while (pos < caretPos)
            {
                count++;        // Счетчик строк
                pos = txtBox1.Text.IndexOf("\r\n", pos);
                if (pos != -1)  // Нашли очередную пару
                    pos += 2;   // Сдвигаемся правее найденных
                else
                    break;// Больше нет
            }
            return count;
        }

        int CaretLineNumber
        {
            get
            {
                /***** Врет для завернутых строк Word Wrap! ******************
                int caretPos = txtBox1.SelectionStart;
                return (txtBox1.GetLineIndexFromCharacterIndex(caretPos) + 1);
                /*************************************************************/

                int count = 0;
                int pos = 0;
                int caretPos = txtBox1.SelectionStart + 1; //txtBox1.CaretIndex
                while (pos < caretPos)
                {
                    count++;        // Счетчик строк
                    pos = txtBox1.Text.IndexOf("\r\n", pos);// \n - перевод строки
                    if (pos != -1)  // Нашли очередную пару
                        pos += 2;   // Сдвигаемся правее найденных
                    else
                        break;// Больше нет
                }
                return count;
            }
            set
            {
                value = value - 1;
                int count = 0;
                int pos = 0;
                while (count < value)   // Пока не превышает заданную
                {
                    pos = txtBox1.Text.IndexOf("\r\n", pos);// \r - возврат каретки
                    if (pos != -1)  // Нашли очередную пару
                    {
                        count++;    // Счетчик строк
                        pos += 2;   // Сдвигаемся правее найденных
                    }
                    else
                    {
                        pos = txtBox1.Text.Length;
                        break;
                    }
                }
                // Позиционируем курсор
                txtBox1.SelectionStart = pos;
                txtBox1.SelectionLength = 0;
            }
        }
        #endregion Задача Go To

        private void SelectAllOnExecute(object sender, RoutedEventArgs e)
        {
            txtBox1.SelectAll();
        }
    }
}
