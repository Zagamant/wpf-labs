using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using HTMLConverter;
using Microsoft.Win32; // Включаем дополнительные пространства имен

namespace Task5
{
	public partial class MainWindow : Window
	{
		private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

		public MainWindow()
		{
			InitializeComponent();
		}

		// При конвертации создается подкаталог FilesXAML относительно текущего
		private void convertToXaml_Click(object sender, RoutedEventArgs e)
		{
			_openFileDialog.Title = "Select HTML-files";
			_openFileDialog.Filter = "HTML Documents (*.htm; *.html)|*.htm;*.html";
			_openFileDialog.Multiselect = true;

			if (_openFileDialog.ShowDialog() == false || _openFileDialog.FileName == string.Empty)
				return;

			if (!Directory.Exists("FilesXAML"))
				Directory.CreateDirectory("FilesXAML");
			var fileNames = _openFileDialog.FileNames;
			foreach (var fileName in fileNames)
			{
				var htmlStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				var myStreamReader = new StreamReader(htmlStream,
					Encoding.GetEncoding(1251));

				// Убираем полный путь и расширение
				var name = fileName.Substring(0, fileName.LastIndexOf('.'));
				var start = name.LastIndexOf('\\');
				start = start != -1 ? start + 1 : 0;
				name = name.Substring(start);

				// Конвертируем
				try
				{
					File.WriteAllText("D:\\" + name + ".xaml",
						HtmlToXamlConverter.ConvertHtmlToXaml(myStreamReader.ReadToEnd(), true),
						Encoding.GetEncoding(1251));
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message);
				}
				finally
				{
					// Сохранили информацию и закрыли потоки
					htmlStream.Close();
					myStreamReader.Close();
				}
			}
		}

		private void loadXaml_Click(object sender, RoutedEventArgs e)
		{
			_openFileDialog.Title = "Select XAML-file";
			_openFileDialog.Filter = "XAML Documents (*.xaml)|*.xaml";
			_openFileDialog.Multiselect = false;
			if (_openFileDialog.ShowDialog() == false || _openFileDialog.FileName == string.Empty)
				return;

			var fileName = _openFileDialog.FileName;
			try
			{
				var htmlStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				var myStreamReader = new StreamReader(htmlStream,
					Encoding.GetEncoding(1251));
				var content = XamlReader.Parse(myStreamReader.ReadToEnd()) as FlowDocument;
				flowDocumentScrollViewer.Document = content;
			}
			catch
			{
			}

			// Поток закрывать нельзя, разорвется связь с отображением документа
		}

		private void editXaml_Click(object sender, RoutedEventArgs e)
		{
			_openFileDialog.Title = "Choose a folder with a XamlPad.exe";
			_openFileDialog.Filter = "XamlPad.exe|*.exe";
			_openFileDialog.RestoreDirectory = false;

			if (_openFileDialog.ShowDialog() == false || !File.Exists("XamlpadX.exe"))
				return;

			var pathXamlPad = Path.Combine(
				Directory.GetCurrentDirectory(), "XamlpadX.exe");

			_openFileDialog.Title = "Select XAML-file";
			_openFileDialog.Filter = "XAML Documents (*.xaml)|*.xaml";
			_openFileDialog.Multiselect = false;

			if (_openFileDialog.ShowDialog() == false || _openFileDialog.FileName == string.Empty)
				return;

			var exe = new Process();
			exe.StartInfo.FileName = pathXamlPad; //Имя файла для запуска
			exe.StartInfo.Arguments = _openFileDialog.FileName; //Аргументы

			try
			{
				exe.Start();
			}
			catch
			{
			}
		}
	}
}