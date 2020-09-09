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
    
namespace WpfText3
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
    
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "HTML Documents (*.htm; *.html)|*.htm;*.html";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialog.Multiselect=false;
            if (openFileDialog.ShowDialog() == false || openFileDialog.FileName == String.Empty)
                return;
    
            Uri uri = new Uri(openFileDialog.FileName);
            this.webBrowser1.Source = uri;
        }
    
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.webBrowser1.CanGoBack)
            {
                this.webBrowser1.GoBack();
            }
        }
    
        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.webBrowser1.CanGoForward)
            {
                this.webBrowser1.GoForward();
            }
        }

        private void webBrowser2_Initialized(object sender, EventArgs e)
        {
            Uri uri = new Uri(System.IO.Directory.GetCurrentDirectory() + "\\HtmlDoc.html", UriKind.Absolute);
    
            this.webBrowser2.Navigate(uri);
        }

        private void webBrowser3_Initialized(object sender, EventArgs e)
        {
            Uri uri = new Uri("HtmlDoc.html", UriKind.Relative);
            System.IO.Stream source = Application.GetRemoteStream(uri).Stream;

            this.webBrowser3.NavigateToStream(source);
        }
    }
}
