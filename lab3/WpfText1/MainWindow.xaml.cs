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

namespace WpfText1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Заполняем коллекции и подключаем их к спискам ListBox
            this.fontFamilyListBox.ItemsSource = FontPropertyLists.FontFaces;
            this.fontWeightListBox.ItemsSource = FontPropertyLists.FontWeights;
            this.fontSizeListBox.ItemsSource = FontPropertyLists.FontSizes;

            // Выполняется после XAML
            this.fontSizeListBox.SelectedIndex = this.fontSizeListBox.Items.Count - 1;
            this.fontSizeListBox.ScrollIntoView(this.fontSizeListBox.Items.Count - 1);
        }

        // Сокрытие унаследованных от Window одноименных свойств
        new public FontFamily FontFamily
        {
            get { return (FontFamily)this.fontFamilyListBox.SelectedItem; }
            set
            {
                this.fontFamilyListBox.SelectedItem = value;    // Выделить
                this.fontFamilyListBox.ScrollIntoView(value);   // Прокрутить до видимого
            }
        }
        new public FontWeight FontWeight
        {
            get { return (FontWeight)this.fontWeightListBox.SelectedItem; }
            set
            {
                this.fontWeightListBox.SelectedItem = value;    // Выделить
                this.fontWeightListBox.ScrollIntoView(value);   // Прокрутить до видимого
            }
        }
        new public double FontSize
        {
            get { return (double)this.fontSizeListBox.SelectedItem; }
            set
            {
                this.fontSizeListBox.SelectedItem = value;      // Выделить
                this.fontSizeListBox.ScrollIntoView(value);     // Прокрутить до видимого
            }
        }

        private void fontFamilyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            this.FontFamily = new FontFamily(this.fontFamilyTextBox.Text);
        }

        private void fontWeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            if (FontPropertyLists.CanParseFontWeight(this.fontWeightTextBox.Text))
            {
                this.FontWeight = FontPropertyLists.ParseFontWeight(this.fontWeightTextBox.Text);
            }
        }

        private void fontSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Привести ListBox в соответствие с содержимым TextBox
            double fontSize;
            if (double.TryParse(this.fontSizeTextBox.Text, out fontSize))
            {
                this.FontSize = fontSize;   // В поле действительно число
            }
        }

        private void text_Initialized(object sender, EventArgs e)
        {
            // Добавляем в TextBlock строку
            Run run = new Run("\nDynamic");
            run.FontFamily = new FontFamily("Curlz MT");
            text.Inlines.Add(run);
            run = new Run("Text");
            run.FontFamily = new FontFamily("Comic Sans MS");
            run.Foreground = Brushes.Aqua;
            text.Inlines.Add(new Bold(run));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = passwordBox.Password;
        }

        bool flagState = true;
        Brush color;
        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Сохраняем первоначальный цвет кнопки 
            if (flagState)
            {
                color = toggleButton.Background;
                flagState = false;
            }

            if (toggleButton.IsChecked == true)
            {
                passwordBox.IsEnabled = false;
                toggleButton.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                passwordBox.IsEnabled = true;
                toggleButton.Background = color;
            }
        }

    }
}