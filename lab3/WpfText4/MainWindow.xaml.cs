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
using System.Xml;
using System.IO;
using HTMLConverter;

namespace WpfText4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void flowDocumentReader_Initialized(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\HtmlDoc.html";
            StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("windows-1251"));
            string text = streamReader.ReadToEnd();

            flowDocumentReader.Document = ConvertTextToFlowDocument(text);
        }

        public static FlowDocument ConvertTextToFlowDocument(string text)
        {
            try
            {
                string xaml = HTMLConverter.HtmlToXamlConverter.ConvertHtmlToXaml(text, true);
                return XamlReader.Load(new XmlTextReader(new StringReader(xaml))) as FlowDocument;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
