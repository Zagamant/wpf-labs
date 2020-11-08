using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Task3
{
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}

		private void loadButton_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				Filter = "HTML Documents (*.htm; *.html)|*.htm;*.html",
				InitialDirectory = Directory.GetCurrentDirectory(),
				Multiselect = false
			};
			if (openFileDialog.ShowDialog() == false || openFileDialog.FileName == string.Empty)
				return;

			var uri = new Uri(openFileDialog.FileName);
			WebBrowser1.Source = uri;
		}

		private void backButton_Click(object sender, RoutedEventArgs e)
		{
			if (WebBrowser1.CanGoBack) WebBrowser1.GoBack();
		}

		private void forwardButton_Click(object sender, RoutedEventArgs e)
		{
			if (WebBrowser1.CanGoForward) WebBrowser1.GoForward();
		}

		private void webBrowser2_Initialized(object sender, EventArgs e)
		{
			var uri = new Uri(Directory.GetCurrentDirectory() + "\\HtmlDoc.html", UriKind.Absolute);

			WebBrowser2.Navigate(uri);
		}

		private void webBrowser3_Initialized(object sender, EventArgs e)
		{
			var uri = new Uri("HtmlDoc.html", UriKind.Relative);
			var source = Application.GetRemoteStream(uri).Stream;

			WebBrowser3.NavigateToStream(source);
		}
	}
}