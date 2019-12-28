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

using System.Windows.Markup;
using System.IO;

namespace WpfText2
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
        void LoadFlowDocumentScrollViewerWithXAMLFile(string fileName)
        {
            FileStream xamlFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            FlowDocument content = XamlReader.Load(xamlFile) as FlowDocument;
            documentReader.Document = content;

            xamlFile.Close();
        }
    }
}
