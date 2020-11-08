using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Task6
{
	public partial class Window1 : Window
	{
		private string[] arrayText;

		// Вынесли как поля для видимости в методах
		private FileStream myStream;
		private StreamReader myStreamReader;

		private string[] textSplit;

		public Window1()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Background = Brushes.LightGray;
			// Можно вместо разделителя '\\' использовать '/' 
			var fileName = "Documents/TextDocument.txt";

			try
			{
				myStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				myStreamReader = new StreamReader(myStream, Encoding.GetEncoding(1251));

				// Не знаем, какой массив задать, поэтому читаем в коллекцию
				// Но можно было и сразу в коллекцию StringBuilder.Append()
				var list = new List<string>();
				while (myStreamReader.EndOfStream != true)
					list.Add(myStreamReader.ReadLine()); // Читаем построчно

				// Копируем ссылки на элементы List в массив
				arrayText = new string[list.Count];
				for (var i = 0; i < list.Count; i++)
					arrayText[i] = list[i];

				// Заполняем ListBox
				foreach (var t in arrayText)
				{
					var pos = t.IndexOf('.');
					listBox.Items.Add(t.Substring(0, pos + 1));
				}

				listBox.SelectedIndex = 0; // На первый элемент списка
			}
			catch
			{
			} // Один синтаксис!!!
			finally
			{
				// Системные ресурсы надо закрывать
				myStream.Close();
				myStreamReader.Close();
			}
		}

		private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			text.Inlines.Clear(); // Очищаем предыдущее содержимое
			text.Inlines.Add(arrayText[listBox.SelectedIndex]); // Добавляем новое
		}

		private void fullDocument_Loaded(object sender, RoutedEventArgs e)
		{
			fullDocument.Background = Brushes.White; // Фон документа программно

			foreach (var t in arrayText)
			{
				var para = new Paragraph {FontFamily = new FontFamily("Arial")};
				para.Inlines.Add(t);
				fullDocument.Blocks.Add(para);
			}
		}

		private void splitDocument_Loaded(object sender, RoutedEventArgs e)
		{
			// Можно вместо разделителя '\\' использовать '/' 
			var fileName = "Documents/TextDocument.txt";

			try
			{
				myStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				myStreamReader = new StreamReader(myStream, Encoding.GetEncoding(1251));

				var str = myStreamReader.ReadToEnd(); // Читаем весь файл
				textSplit = str.Split('_'); // Расщепляем строку по метке '_'
				// Элемент textSplit[0] получился пустой
			}
			catch (Exception)
			{
				// ignored
			}
			finally
			{
				// Системные ресурсы надо закрывать
				myStream.Close();
				myStreamReader.Close();
			}

			// Формируем заголовок программно
			var head = new Paragraph {TextAlignment = TextAlignment.Center};
			var run = new Run("Документ получен расщеплением строки") {FontSize = 21};
			var bold = new Bold(run);
			head.Inlines.Add(bold);
			splitDocument.Blocks.Add(head);

			// Заполняем FlowDocument
			splitDocument.FontFamily = new FontFamily("Arial");
			for (var i = 1; i < textSplit.Length; i++)
			{
				var para = new Paragraph();
				para.Inlines.Add(textSplit[i]);
				splitDocument.Blocks.Add(para);
			}
		}

		private void listDocument_Initialized(object sender, EventArgs e)
		{
			// Формируем заголовок
			var head = new Paragraph {TextAlignment = TextAlignment.Center};
			var run = new Run("Нумерованный список OrderList") {FontSize = 21};
			var bold = new Bold(run);
			head.Inlines.Add(bold);
			listDocument.Blocks.Add(head);

			// Создаем и настраиваем список
			var list = new List
			{
				MarkerOffset = 25, 
				MarkerStyle = TextMarkerStyle.Decimal, 
				StartIndex = 25
			};
			// Отступ от маркеров
			// Нумерация с 10

			// Заполняем список дочерними элементами
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 1"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 2"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 3"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 4"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 5"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 6"))));
			list.ListItems.Add(new ListItem(new Paragraph(new Run("ListItem 7"))));

			// Присоединяем список к документу
			listDocument.FontFamily = new FontFamily("Arial");
			listDocument.Blocks.Add(list);
		}
	}
}