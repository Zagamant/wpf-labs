using System;
using System.Windows;
using System.Windows.Input;

using Microsoft.Win32;  // Для стандартных диалогов Win32
using System.IO;        // Работа с файлами и каталогами

namespace Notepad1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Создание жестов
            this.CreateGestures();

            // Дополнительные обработчики в файле EnabledControls.cs
            AdditionalHandlers();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Отключаем в TextBox встроенный жест Ctrl+X для команды Cut
            KeyBinding keyBinding = new KeyBinding(
                ApplicationCommands.NotACommand, Key.X, ModifierKeys.Control);
            txtBox1.InputBindings.Add(keyBinding);
            // Отключаем в TextBox встроенный жест Ctrl+C для команды Copy
            keyBinding = new KeyBinding(
                ApplicationCommands.NotACommand, Key.C, ModifierKeys.Control);
            txtBox1.InputBindings.Add(keyBinding);
            // Отключаем в TextBox встроенный жест Ctrl+V для команды Paste
            keyBinding = new KeyBinding(
                ApplicationCommands.NotACommand, Key.V, ModifierKeys.Control);
            txtBox1.InputBindings.Add(keyBinding);
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            this.Height = this.ActualHeight - 1;
            this.Height = this.ActualHeight + 1;
            this.txtBox1.Focus();
        }

        private void txtBox1_TextChanged(object sender,
            System.Windows.Controls.TextChangedEventArgs e)
        {
            if (IsModified)
                return;
            else
                IsModified = true;
        }


        #region private Fields - локальные поля

        //------------------------------------------------------
        //
        //  private Fields - локальные поля
        //
        //------------------------------------------------------

        bool modified = false;// Флаг изменений содержимого
        string strLoadedFile;   // Полное имя загруженного документа

        #endregion private Fields

        #region Auxiliary Methods - вспомогательные методы

        //------------------------------------------------------
        //
        //  Auxiliary Methods - вспомогательные методы
        //
        //------------------------------------------------------

        // Метод возвращает true, если содержимое 
        // TextBox не требует сохранения 
        bool flag;
        bool CheckModifiedAndSaveIt()
        {
            if (!IsModified)
                return true;

            MessageBoxResult result =
                MessageBox.Show(
                "Сохранить изменения?", "",     // Контекст и заголовок
                MessageBoxButton.YesNoCancel,   // Кнопки диалога
                MessageBoxImage.Question,       // Иконка вопроса
                MessageBoxResult.Yes            // Кнопка с фокусом
                );

            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (String.IsNullOrEmpty(strLoadedFile))
                        flag = DisplaySaveDialog("");   // Запись с диалогом 
                    else
                        flag = SaveFile(strLoadedFile); // Просто запись
                    break;
                case MessageBoxResult.No:
                    flag = true;
                    break;
                case MessageBoxResult.Cancel:
                    flag = false;
                    break;
            }
            return flag;
        }

        // Вызывает диалоговое окно записи файла
        // и возвращает true, если файл был сохранен
        bool DisplaySaveDialog(string strFileName)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text Documents(*.txt)|*.txt|All Files(*.*)|*.*";
            dlg.FileName = strFileName;
            bool result = (bool)dlg.ShowDialog(this);   // Желание пользователя
            if (result)
                result = SaveFile(dlg.FileName);        // Возможность компьютера
            return result;
        }

        // Сохраняет документ и возвращает true при успехе 
        // Аргумент - полное имя файла 
        bool SaveFile(string strFileName)
        {
            try
            {
                File.WriteAllText(strFileName, txtBox1.Text,
                    System.Text.Encoding.GetEncoding(1251));
            }
            catch (Exception e)
            {
                // Ловим все исключения и выводим диалог
                MessageBox.Show(
                    "Ошибка записи файла:\n" + e.Message, "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Asterisk
                    );
                return false;
            }

            strLoadedFile = strFileName;
            UpdateTitle();      // Меняем заголовок окна
            IsModified = false; // Нет изменений текста
            return true;
        }

        // Диалог открытия файла возвращает true при успехе
        bool DisplayOpenDialog()
        {
            flag = CheckModifiedAndSaveIt(); // Проверяем и сохраняем изменения
            if (!flag)
                return flag;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Documents(*.txt)|*.txt|All Files(*.*)|*.*";
            bool result = (bool)dlg.ShowDialog(this);   // Желание пользователя
            if (result)
                result = OpenFile(dlg.FileName);        // Возможность компьютера
            return result;
        }

        // Открывает файл и при успехе возвращает true
        bool OpenFile(string strFileName)
        {
            try
            {
                txtBox1.Text = File.ReadAllText(strFileName,
                    System.Text.Encoding.GetEncoding(1251));
            }
            catch (Exception e)
            {
                // Ловим все исключения и выводим диалог
                MessageBox.Show(
                    "Ошибка чтения файла:\n" + e.Message, "",
                    MessageBoxButton.OK,
                    MessageBoxImage.Asterisk
                    );
                return false;
            }

            strLoadedFile = strFileName;
            UpdateTitle();      // Меняем заголовок окна
            IsModified = false; // Нет изменений текста
            // Сбрасываем границы выделенного текста поля редактирования
            txtBox1.SelectionStart = 0;
            txtBox1.SelectionLength = 0;
            return true;
        }

        // Коррекция заголовка окна
        void UpdateTitle()
        {
            // Извлекаем заголовок окна из словаря ресурсов
            String title = Application.Current.
                    Resources["ApplicationTitle1"].ToString();

            //if (strLoadedFile == null || strLoadedFile.Trim() == String.Empty)
            if (String.IsNullOrEmpty(strLoadedFile))    // Проще!
            {
                this.Title = "Untitled - " + title;
                return;
            }

            // Извлекаем имя файла из полного пути
            int startIndex = strLoadedFile.LastIndexOf('\\') + 1;
            int endIndex;
            // Проверяем в системном реестре настройки системы по скрытию расширения файлов
            using (RegistryKey filekey = Registry.CurrentUser.CreateSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced"))
            {
                if ((filekey != null) && (filekey.GetValue("HideFileExt", 0).ToString() == "0"))
                {
                    endIndex = strLoadedFile.Length;            // Нет расширения
                }
                else
                {
                    endIndex = strLoadedFile.LastIndexOf('.');  // Отсекаем расширение
                }
            }

            if (endIndex > startIndex)
            {
                this.Title = strLoadedFile.Substring(startIndex) +
                    " - " + title;
            }
            else
            {
                this.Title = strLoadedFile.Substring(startIndex, endIndex - startIndex) +
                    " - " + title;
            }
        }

        #endregion  Auxiliary Methods
    }
}
