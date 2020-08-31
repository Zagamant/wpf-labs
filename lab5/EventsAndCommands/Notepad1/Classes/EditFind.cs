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

namespace Notepad1
{
    partial class MainWindow
    {
        internal FindAndReplaceDialog _dlg = null; // Для видимости в обработчиках
        string _findString, _replaceString; // Для видимости в обработчиках

        void CreateDialog()
        {
            if (_dlg != null)// Уже существует
            {
                // Если есть выделенное, обновляем прежнее
                if (!String.IsNullOrEmpty(txtBox1.SelectedText))
                    _findString = _dlg._findWhat.SelectedText = txtBox1.SelectedText;
                _dlg._findWhat.Focus();
                return;
            }

            // Создать заново
            _dlg = new FindAndReplaceDialog();

            _dlg.Owner = this;  // Привязываем диалог к владельцу
            _dlg.Show();// Немодальное, поэтому не перехватывает управление

            // Продолжаем настраивать
            _dlg._findWhat.Focus();

            // Если есть выделенное, обновляем прежнее
            if (!String.IsNullOrEmpty(txtBox1.SelectedText))
                _findString = _dlg._findWhat.SelectedText = txtBox1.SelectedText;

            _dlg.ReplaceWith = _replaceString;

            // Анонимные обработчики
            _dlg.FindNext += delegate (object sender, EventArgs args)
            {
                FindNextExec();
            }; //!!!
            _dlg.Replace += delegate (object sender, EventArgs args)
            {
                ReplaceExec();
            }; //!!!
            _dlg.ReplaceAll += delegate (object sender, EventArgs args)
            {   // У Петцольда (WPF с.466) есть иной вариант этого обработчика
                _replaceString = _dlg.ReplaceWith;
                txtBox1.SelectionStart = 0;
                txtBox1.SelectionLength = 0;

                while (FindNextExec())
                {
                    using (txtBox1.DeclareChangeBlock())
                    {
                        txtBox1.SelectedText = _replaceString;
                        txtBox1.SelectionLength = _replaceString.Length;
                    }
                }

                txtBox1.SelectionStart = 0;
                txtBox1.SelectionLength = 0;
            }; //!!!
            _dlg.Closed += delegate (object sender, EventArgs args)
            {
                _dlg = null;
            }; //!!! Точка с запятой обязательна - заканчивает строку
        }

        bool FindNextExec()
        {
            int indexStart, indexFind;// Откуда начать и начало следующего
            _findString = _dlg.FindWhat; // Извлекаем текст поиска

            // Учет регистра при поиске, однострочный условный оператор
            StringComparison strComp = (bool)_dlg.MatchCase ? StringComparison.Ordinal :
                    StringComparison.OrdinalIgnoreCase;

            if ((bool)_dlg.SearchUp)// Ищем вверх
            {
                indexStart = txtBox1.SelectionStart - 1;
                indexFind = txtBox1.Text.LastIndexOf(_findString, indexStart, strComp);
            }
            else                    // Ищем вниз
            {
                indexStart = txtBox1.SelectionStart + txtBox1.SelectionLength;
                indexFind = txtBox1.Text.IndexOf(_findString, indexStart, strComp);
            }

            // Анализируем и принимаем решение
            if (indexFind != -1)
            {
                txtBox1.Select(indexFind, _findString.Length);// Выделяем найденное 
                txtBox1.Focus();
                return true;
            }
            else
            {
                MessageBox.Show("Текст \"" + _findString + "\" не найден!",
                    this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
                txtBox1.Focus();
                return false;
            }
        }

        private void ReplaceExec()
        {
            // Извлекаем тексты поиска и замены
            _findString = _dlg.FindWhat;
            _replaceString = _dlg.ReplaceWith;

            // Учет регистра при поиске, однострочный условный оператор
            StringComparison strComp = (bool)_dlg.MatchCase ? StringComparison.Ordinal :
                    StringComparison.OrdinalIgnoreCase;

            if (_findString.Equals(txtBox1.SelectedText, strComp))
                txtBox1.SelectedText = _replaceString;

            FindNextExec();
        }
    }
}
