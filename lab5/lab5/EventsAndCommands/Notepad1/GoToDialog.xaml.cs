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
using System.Windows.Shapes;

// Дополнительные подключения пространств имен
using System.Resources;
using System.Reflection;

namespace Notepad1
{
    public partial class GoToDialog : Window
    {
        public GoToDialog()
        {
            InitializeComponent();

            // Сохраняем первое значение Label
            _rangeOrigin = _rangeNumber.Content.ToString();
        }

        // Объявляем событие для прослушивания в основном окне
        public event EventHandler GotoActivate;

        // Закрытые поля класса
        int _lineNumber = 0;
        int _maxLineNumber;
        ResourceManager res =
            new ResourceManager("Notepad2.StringTable",
                Assembly.GetExecutingAssembly());
        String _rangeOrigin;

        // Свойства доступа
        public int LineNumber
        {
            get { return _lineNumber; }
            set
            {
                _lineNumber = value;
                _lineNumberTextBox.Text = _lineNumber.ToString();
            }
        }
        public int MaxLineNumber
        {
            get { return _maxLineNumber; }
            set { _maxLineNumber = value; }
        }

        private void OnActivated(object sender, EventArgs e)
        {
            // Если существуют обработчики, инициируем событие
            if (GotoActivate != null)
                GotoActivate(this, EventArgs.Empty);

            _rangeNumber.Content = _rangeOrigin +
                String.Format(" (1 - {0}):", _maxLineNumber);
            _lineNumberTextBox.Focus();
            _lineNumberTextBox.SelectAll();// Выделяем содержимое
        }

        private void OkClicked(object sender, RoutedEventArgs e)
        {
            // Не закрывать диалог, пока есть ошибки
            if (string.IsNullOrEmpty(_lineNumberTextBox.Text))
            {
                MessageBox.Show(res.GetString("GotoErrorMsgEmpty"),
                    res.GetString("GotoErrorDialogTitle"));
                return;
            }
            if (!int.TryParse(_lineNumberTextBox.Text, out _lineNumber))
            {
                MessageBox.Show(res.GetString("GotoErrorMsgFormat"),
                    res.GetString("GotoErrorDialogTitle"));
                return;
            }
            if (LineNumber > _maxLineNumber || LineNumber <= 0)
            {
                MessageBox.Show(res.GetString("GotoErrorMsgRange"),
                    res.GetString("GotoErrorDialogTitle"));
                return;
            }

            this.DialogResult = true;
            this.Close();
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
