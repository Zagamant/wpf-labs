using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Works.xaml
    /// </summary>
    public partial class Works : Window
    {
        public Works() => InitializeComponent();
        private void WorkClick(object sender, RoutedEventArgs e) => this.Content = new Works().Content;
        private void ContactClick(object sender, RoutedEventArgs e) => this.Content = new Contact().Content;
        private void MainClick(object sender, RoutedEventArgs e) => this.Content = new MainWindow().Content;
    }
}
