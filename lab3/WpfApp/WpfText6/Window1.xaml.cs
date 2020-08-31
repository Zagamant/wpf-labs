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
using System.Windows.Markup;
using System.Collections;
    
namespace WpfText6
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
    
        // Вынесли как поля для видимости в методах
        FileStream myStream;
        StreamReader myStreamReader;
        String[] arrayText;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Background = Brushes.LightGray;
            // Можно вместо разделителя '\\' использовать '/' 
            String fileName = "Documents/TextDocument.txt";
    
            try
            {
                myStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                myStreamReader = new StreamReader(myStream, Encoding.GetEncoding(1251));
    
                // Не знаем, какой массив задать, поэтому читаем в коллекцию
                // Но можно было и сразу в коллекцию StringBuilder.Append()
                ArrayList arrayList = new ArrayList();
                while (myStreamReader.EndOfStream != true)
                    arrayList.Add(myStreamReader.ReadLine());// Читаем построчно
    
                // Копируем ссылки на элементы ArrayList в массив
                arrayText = new String[arrayList.Count];
                for (int i = 0; i < arrayList.Count; i++)
                    arrayText[i] = (String)arrayList[i];
    
                // Заполняем ListBox
                for (int i = 0; i < arrayText.Length; i++)
                {
                    int pos = arrayText[i].IndexOf('.');
                    listBox.Items.Add(arrayText[i].Substring(0, pos + 1));
                }
                listBox.SelectedIndex = 0; // На первый элемент списка
            }
            catch { } // Один синтаксис!!!
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
            text.Inlines.Add(arrayText[listBox.SelectedIndex]);// Добавляем новое
        }
    
        private void fullDocument_Loaded(object sender, RoutedEventArgs e)
        {
            fullDocument.Background = Brushes.White;// Фон документа программно
    
            for (int i = 0; i < arrayText.Length; i++)
            {
                Paragraph para = new Paragraph();
                para.FontFamily = new FontFamily("Arial");
                para.Inlines.Add(arrayText[i]);
                fullDocument.Blocks.Add(para);
            }
        }
    
        String[] textSplit;
        private void splitDocument_Loaded(object sender, RoutedEventArgs e)
        {
            // Можно вместо разделителя '\\' использовать '/' 
            String fileName = "Documents/TextDocument.txt";
    
            try
            {
                myStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                myStreamReader = new StreamReader(myStream, Encoding.GetEncoding(1251));

                String str = myStreamReader.ReadToEnd();// Читаем весь файл
                textSplit = str.Split('_');// Расщепляем строку по метке '_'
                // Элемент textSplit[0] получился пустой
            }
            catch (Exception) { } // Другой синтаксис!!!
            finally
            {
                // Системные ресурсы надо закрывать
                myStream.Close();
                myStreamReader.Close();
            }
    
            // Формируем заголовок программно
            Paragraph head = new Paragraph();
            head.TextAlignment = TextAlignment.Center;
            Run run = new Run("Документ получен расщеплением строки");
            run.FontSize = 21;
            Bold bold = new Bold(run);
            head.Inlines.Add(bold);
            splitDocument.Blocks.Add(head);
    
            // Заполняем FlowDocument
            splitDocument.FontFamily = new FontFamily("Arial");
            for (int i = 1; i < textSplit.Length; i++)
            {
                Paragraph para = new Paragraph();
                para.Inlines.Add(textSplit[i]);
                splitDocument.Blocks.Add(para);
            }
        }
    
        private void listDocument_Initialized(object sender, EventArgs e)
        {
            // Формируем заголовок
            Paragraph head = new Paragraph();
            head.TextAlignment = TextAlignment.Center;
            Run run = new Run("Нумерованный список OrderList");
            run.FontSize = 21;
            Bold bold = new Bold(run);
            head.Inlines.Add(bold);
            listDocument.Blocks.Add(head);
    
            // Создаем и настраиваем список
            List list = new List();
            list.MarkerOffset = 25;// Отступ от маркеров
            list.MarkerStyle = TextMarkerStyle.Decimal;
            list.StartIndex = 25; // Нумерация с 10
    
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
