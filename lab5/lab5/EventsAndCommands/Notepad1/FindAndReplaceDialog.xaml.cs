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

namespace Notepad1
{
    public partial class FindAndReplaceDialog : Window
    {
        public FindAndReplaceDialog()
        {
            InitializeComponent();

            // Начальная доступность кнопок, хотя кнопки Replece  
            _findNext.IsEnabled = _replace.IsEnabled = _replaceAll.IsEnabled
                = !String.IsNullOrEmpty(_findWhat.Text);
        }

        #region Открытые свойства - обертки закрытых полей
        //------------------------------------------------------
        //
        //  Открытые свойства - обертки закрытых полей
        //
        //------------------------------------------------------

        // Содержимое текстового поля _findWhat
        public string FindWhat
        {
            get
            {
                return _findWhat.Text;
            }
            set
            {
                _findWhat.Text = value;
            }
        }

        // Содержимое текстового поля _replaceWith
        public string ReplaceWith
        {
            get
            {
                return _replaceWith.Text;
            }
            set
            {
                _replaceWith.Text = value;
            }
        }

        // Определение состояния флага учета регистра
        public bool? MatchCase { get { return _matchCase.IsChecked; } }

        // Определение состояния радиокнопки направления поиска назад
        public bool? SearchUp { get { return _findUp.IsChecked; } }

        // Управление видимостью интерфейса замены
        // Hidden - элемент скрыт (не отображается, но занимает место)
        // Collapsed - элемент свернут (не отображается и не занимает место)
        public bool ShowReplace
        {
            get { return _replaceWith.Visibility == Visibility.Visible; }
            set
            {
                Visibility show;
                if (value)
                {
                    // Отобразить
                    show = Visibility.Visible;
                    _directionGroupBox.Visibility = Visibility.Collapsed;
                    _findDown.IsChecked = true;
                }
                else
                {
                    // Свернуть
                    show = Visibility.Collapsed;
                    _directionGroupBox.Visibility = Visibility.Visible;
                }

                _replaceLabel.Visibility = _replaceWith.Visibility =
                    _replace.Visibility = _replaceAll.Visibility = show;
            }
        }
        #endregion Общедоступные свойства - обертки полей

        #region Открытые события для обработки в основном классе
        //------------------------------------------------------
        //
        // Открытые события для обработки в основном классе Window1
        // Обеспечивают взаимодействие диалогового окна с владельцем
        //
        //------------------------------------------------------

        // Объявляем немаршрутизованные события
        public event EventHandler FindNext;
        public event EventHandler Replace;
        public event EventHandler ReplaceAll;

        #endregion Открытые события для обработки в основном классе Window1

        #region Закрытые обработчики
        //------------------------------------------------------
        //
        // Закрытые методы
        // При возбуждении событий первый параметр - ссылка на диалог,
        // которую в основном классе приведем к самому диалоговому окну
        //
        //------------------------------------------------------

        void OnActivated(object sender, EventArgs e)
        {
            _findWhat.Focus();
        }

        private void FindNextClicked(object sender, RoutedEventArgs e)
        {
            // Если на событие подписались, возбуждаем его
            if (FindNext != null)
            {
                FindNext(this, EventArgs.Empty);
            }
        }

        private void ReplaceClicked(object sender, RoutedEventArgs e)
        {
            // Возбуждаем событие, если для него существует обработчик
            if (Replace != null)
            {
                Replace(this, EventArgs.Empty);
            }
        }

        private void ReplaceAllClicked(object sender, RoutedEventArgs e)
        {
            // Проверяем наличие обработчика и возбуждаем событие
            if (ReplaceAll != null)
            {
                ReplaceAll(this, EventArgs.Empty);
            }
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindTextChanged(object sender, TextChangedEventArgs e)
        {
            // Управление доступность кнопок в зависимости от текстового поля
            _findNext.IsEnabled = _replace.IsEnabled = _replaceAll.IsEnabled
                = !String.IsNullOrEmpty(_findWhat.Text);
        }
        #endregion Закрытые обработчики
    }
}
