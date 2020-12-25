using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Contact.xaml
    /// </summary>
    public partial class Contact : Window
    {
        public Contact() => InitializeComponent();
        private void WorkClick(object sender, RoutedEventArgs e) => this.Content = new Works().Content;
        private void ContactClick(object sender, RoutedEventArgs e) => this.Content = new Contact().Content;
        private void MainClick(object sender, RoutedEventArgs e) => this.Content = new MainWindow().Content;
    }
}
