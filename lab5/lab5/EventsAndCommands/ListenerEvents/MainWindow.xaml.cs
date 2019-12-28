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

namespace ListenerEvents
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int count;
        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            count = 0;
            Console.Clear();
            if (e.ChangedButton == MouseButton.Left)
                Console.WriteLine("{0}) Window: Наблюдаю туннельное событие PreviewMouseDown", ++count);
            else
                e.Handled = true;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) Window: Наблюдаю пузырьковое событие MouseDown", ++count);
        }

        private void Window_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}) Window: Наблюдаю пузырьковое событие Click (как вложенное)", ++count);
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) Grid: Наблюдаю туннельное событие PreviewMouseDown", ++count);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) Grid: Наблюдаю пузырьковое событие MouseDown", ++count);
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}) Grid: Наблюдаю пузырьковое событие Click (как вложенное)", ++count);
        }

        private void UniformGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) UniformGrid: Наблюдаю туннельное событие PreviewMouseDown", ++count);
        }

        private void UniformGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) UniformGrid: Наблюдаю пузырьковое событие MouseDown", ++count);
        }

        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}) UniformGrid: Наблюдаю пузырьковое событие Click (как вложенное)", ++count);
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) TextBlock: Наблюдаю туннельное событие PreviewMouseDown", ++count);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) TextBlock: Наблюдаю пузырьковое событие MouseDown", ++count);
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}) TextBlock: Наблюдаю пузырьковое событие Click (как вложенное)", ++count);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("{0}) TextBlock: Возбуждаю прямое событие MouseEnter", ++count);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("{0}) TextBlock: Возбуждаю прямое событие MouseLeave", ++count);
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) Button: Наблюдаю туннельное событие PreviewMouseDown", ++count);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("{0}) Button: Наблюдаю пузырьковое событие MouseDown", ++count);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("{0}) Button: Возбуждаю пузырьковое событие Click", ++count);
        }
    }
}
