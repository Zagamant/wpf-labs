using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace Task4
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void flowDocumentReader_Initialized(object sender, EventArgs e)
		{
			var path = Directory.GetCurrentDirectory() + "\\HtmlDoc.html";
			var streamReader = new StreamReader(path, Encoding.GetEncoding("windows-1251"));
			var text = streamReader.ReadToEnd();

			FlowDocumentReader.Document = ConvertTextToFlowDocument(text);
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