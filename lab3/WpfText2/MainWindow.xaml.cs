using System;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Task2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FlowDocumentScrollViewer_Initialized(object sender, EventArgs e)
        {
            documentReader.Document = (FlowDocument)XamlReader.Load(File.OpenRead("XMLFile1.xaml"));

            // Альтернативный вариант
            //LoadFlowDocumentScrollViewerWithXAMLFile("XMLFile1.xaml");
        }

        // Альтернативный вариант
        private void LoadFlowDocumentScrollViewerWithXamlFile(string fileName)
        {
            var xamlFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var content = XamlReader.Load(xamlFile) as FlowDocument;
            documentReader.Document = content;

            xamlFile.Close();
        }
    }
}
