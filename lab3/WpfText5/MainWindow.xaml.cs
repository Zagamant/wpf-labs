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

// Включаем дополнительные пространства имен
using System.IO;
using Microsoft.Win32;
using HTMLConverter;
using System.Windows.Markup;

namespace WpfText5
{
    public partial class MainWindow : Window
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        // При конвертации создается подкаталог FilesXAML относительно текущего
        private void convertToXaml_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Title = "Select HTML-files";
            openFileDialog.Filter = "HTML Documents (*.htm; *.html)|*.htm;*.html";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == false || openFileDialog.FileName == String.Empty)
                return;

            if (!Directory.Exists("FilesXAML"))
                Directory.CreateDirectory("FilesXAML");
            string[] fileNames = openFileDialog.FileNames;
            foreach (String fileName in fileNames)
            {
                FileStream htmlStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader myStreamReader = new StreamReader(htmlStream,
                    Encoding.GetEncoding(1251));

                // Убираем полный путь и расширение
                String name = fileName.Substring(0, fileName.LastIndexOf('.'));
                int start = name.LastIndexOf('\\');
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
            openFileDialog.Title = "Select XAML-file";
            openFileDialog.Filter = "XAML Documents (*.xaml)|*.xaml";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == false || openFileDialog.FileName == String.Empty)
                return;

            String fileName = openFileDialog.FileName;
            try
            {
                FileStream htmlStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader myStreamReader = new StreamReader(htmlStream,
                    Encoding.GetEncoding(1251));
                FlowDocument content = XamlReader.Parse(myStreamReader.ReadToEnd()) as FlowDocument;
                flowDocumentScrollViewer.Document = content;
            }
            catch { }
            // Поток закрывать нельзя, разорвется связь с отображением документа
        }

        private void editXaml_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Title = "Choose a folder with a XamlPad.exe";
            openFileDialog.Filter = "XamlPad.exe|*.exe";
            openFileDialog.RestoreDirectory = false;

            if (openFileDialog.ShowDialog() == false || !File.Exists("XamlpadX.exe"))
                return;

            String pathXamlPad = System.IO.Path.Combine(
                Directory.GetCurrentDirectory(), "XamlpadX.exe");

            openFileDialog.Title = "Select XAML-file";
            openFileDialog.Filter = "XAML Documents (*.xaml)|*.xaml";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == false || openFileDialog.FileName == String.Empty)
                return;

            System.Diagnostics.Process exe = new System.Diagnostics.Process();
            exe.StartInfo.FileName = pathXamlPad; //Имя файла для запуска
            exe.StartInfo.Arguments = openFileDialog.FileName; //Аргументы

            try
            {
                exe.Start();
            }
            catch { }
        }
    }
}
