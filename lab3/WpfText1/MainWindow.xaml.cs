using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Task1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Заполняем коллекции и подключаем их к спискам ListBox
            this.FontFamilyListBox.ItemsSource = FontPropertyLists.FontFaces;
            this.fontWeightListBox.ItemsSource = FontPropertyLists.FontWeights;
            this.fontSizeListBox.ItemsSource = FontPropertyLists.FontSizes;

            // Выполняется после XAML
            this.fontSizeListBox.SelectedIndex = this.fontSizeListBox.Items.Count - 1;
            this.fontSizeListBox.ScrollIntoView(this.fontSizeListBox.Items.Count - 1);
        }

        // Сокрытие унаследованных от Window одноименных свойств
        public new FontFamily FontFamily
        {
            get => (FontFamily)FontFamilyListBox.SelectedItem;
            set
            {
                FontFamilyListBox.SelectedItem = value;    // Выделить
                FontFamilyListBox.ScrollIntoView(value);   // Прокрутить до видимого
            }
        }
        public new FontWeight FontWeight
        {
            get => (FontWeight)this.fontWeightListBox.SelectedItem;
            set
            {
                this.fontWeightListBox.SelectedItem = value;    // Выделить
                this.fontWeightListBox.ScrollIntoView(value);   // Прокрутить до видимого
            }
        }
        public new double FontSize
        {
            get => (double)this.fontSizeListBox.SelectedItem;
            set
            {
                this.fontSizeListBox.SelectedItem = value;      // Выделить
                this.fontSizeListBox.ScrollIntoView(value);     // Прокрутить до видимого
            }
        }

        private void fontFamilyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            this.FontFamily = new FontFamily(this.FontFamilyTextBox.Text);
        }

        private void fontWeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            if (FontPropertyLists.CanParseFontWeight(this.FontWeightTextBox.Text))
            {
                this.FontWeight = FontPropertyLists.ParseFontWeight(this.FontWeightTextBox.Text);
            }
        }

        private void fontSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            if (double.TryParse(this.fontSizeTextBox.Text, out var fontSize))
            {
                this.FontSize = fontSize;   // В поле действительно число
            }
        }

        private void text_Initialized(object sender, EventArgs e)
        {
            // Добавляем в TextBlock строку
            var run = new Run("\nDynamic")
            {
	            FontFamily = new FontFamily("Curlz MT")
            };
            
            Text.Inlines.Add(run);

            run = new Run("Text")
            {
	            FontFamily = new FontFamily("Comic Sans MS"), Foreground = Brushes.Aqua
            };

            Text.Inlines.Add(new Bold(run));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = passwordBox.Password;
        }

        private bool _flagState = true;
        private Brush _color;
        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Сохраняем первоначальный цвет кнопки 
            if (_flagState)
            {
                _color = toggleButton.Background;
                _flagState = false;
            }

            if (toggleButton.IsChecked == true)
            {
                passwordBox.IsEnabled = false;
                toggleButton.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                passwordBox.IsEnabled = true;
                toggleButton.Background = _color;
            }
        }

    }
}