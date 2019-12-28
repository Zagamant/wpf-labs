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

// Для сборок: System.Windows.Forms.dll, System.Drawing.dll
using sdp = System.Drawing.Printing;// Псевдоним пространства имен 
using swf = System.Windows.Forms;   // Псевдоним пространства имен
using PageSetupDialog = System.Windows.Forms.PageSetupDialog;// Псевдоним класса

namespace Notepad1
{
    partial class MainWindow
    {
        //------------------------------------------------------
        //
        //  Обработчики источников задач File
        //
        //------------------------------------------------------

        private void NewOnExecute(object sender, RoutedEventArgs e)
        {
            // Пользователь передумал или была ошибка записи изменений
            if (!CheckModifiedAndSaveIt())
                return;

            // Изменений нет или они успешно сохранены
            //txtBox1.Text = String.Empty;  // Вариант I
            //txtBox1.Text = "";            // Вариант II
            txtBox1.Clear();                // Вариант III
            strLoadedFile = null;
            IsModified = false;
            UpdateTitle();
            txtBox1.Focus();
        }

        private void OpenOnExecute(object sender, RoutedEventArgs e)
        {
            if (DisplayOpenDialog())
                txtBox1.CaretIndex = txtBox1.Text.Length;// Курсор в конец
            txtBox1.Focus();// Передача фокуса
        }

        private void SaveOnExecute(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(strLoadedFile))
                DisplaySaveDialog(String.Empty);
            else
                SaveFile(strLoadedFile);
            txtBox1.Focus();
        }

        private void SaveAsOnExecute(object sender, RoutedEventArgs e)
        {
            DisplaySaveDialog(strLoadedFile);
            txtBox1.Focus();
        }

        private void PageSetupOnExecute(object sender, RoutedEventArgs e)
        {
            // Ограничемся только показом окна Windows Forms
            PageSetupDialog dlg = new PageSetupDialog();

            // Без настроек не работает. Зададим хотя бы по умолчанию
            dlg.PageSettings = new sdp.PageSettings();
            dlg.PrinterSettings = new sdp.PrinterSettings();

            dlg.ShowDialog();
            txtBox1.Focus();
        }

        private void PrintPreviewOnExecute(object sender, RoutedEventArgs e)
        {
            sdp.PrintDocument document = new sdp.PrintDocument();
            document.DocumentName = strLoadedFile;

            swf.PrintPreviewDialog dlg = new swf.PrintPreviewDialog();
            dlg.Document = document;
            dlg.UseAntiAlias = true;// Включить сглаживание

            dlg.ShowDialog();
            txtBox1.Focus();
        }

        private void PrintOnExecute(object sender, RoutedEventArgs e)
        {
            sdp.PrintDocument document = new sdp.PrintDocument();
            document.DocumentName = strLoadedFile;

            swf.PrintDialog dlg = new swf.PrintDialog();
            dlg.Document = document;
            dlg.ShowDialog();
            txtBox1.Focus();
        }
        bool _IsExitItem = false;
        private void ExitOnExecute(object sender, RoutedEventArgs e)
        {
            if (!CheckModifiedAndSaveIt())
                return; // Пользователь передумал выходить

            _IsExitItem = true;
            Close();
        }
        private void Window_Closing(object sender,
                    System.ComponentModel.CancelEventArgs e)
        {
            /*
            // Эта проверка была бы надежнее
            if(_IsExitItem)
                return;
            */
            // !_IsExitItem должен в условии стоять первым
            if (!_IsExitItem && !CheckModifiedAndSaveIt())
            {
                e.Cancel = true;
                _IsExitItem = false;
                return; // Пользователь передумал выходить
            }
        }

    }
}
