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

using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        const int countDot = 200;

        List<double[]> dataList = new List<double[]>();
        int width = 20;
        RectangleGeometry rectGeometryBG = new RectangleGeometry();
        DrawingGroup drawingGroup = new DrawingGroup();
        GeometryDrawing geometryDrawingBG = new GeometryDrawing();
        GeometryDrawing geometryDrawingFun = new GeometryDrawing();
        GeometryDrawing geometryDrawingCaption = new GeometryDrawing();
        double R = 3;
        double h = 3;
        double[] x = new double[countDot + 1];
        double[] y = new double[countDot + 1];


        public Window1()
        {
            InitializeComponent();

            DataFill();// Заполнение списка данными
            Execute();// Заполнение слоев

            // Отображение на экране
            image1.Source = new DrawingImage(drawingGroup);
        }

        // Генерация точек графиков
        void DataFill()
        {

            for (int i = 0; i < x.Length; i++)
            {
                double angle = Math.PI * 2 / countDot*4 * i;
                x[i] = 2 * R * Math.Cos(angle) - h * Math.Cos(2 * angle);
                y[i] = 2 * R * Math.Sin(angle) - h * Math.Sin(2 * angle);
            }

            dataList.Add(x);
            dataList.Add(y);
        }

        // Послойное формирование рисунка в Z-последовательности
        void Execute()
        {
            BackgroundFun();
            GridFun();
            Fun();
            MarkerFun();
        }

        // Фон
        private void BackgroundFun()
        {
            //GeometryDrawing geometryDrawing = new GeometryDrawing();
            
            //width = (int)dataList[0][countDot]+1;
            rectGeometryBG.Rect = new Rect(0, 0, width, width);
            geometryDrawingBG.Geometry = rectGeometryBG;
            
            geometryDrawingBG.Pen = new Pen(Brushes.Gray, 0.01);
            geometryDrawingBG.Brush = Brushes.White;

            drawingGroup.Children.Add(geometryDrawingBG);
        }

        // Горизонтальная сетка
        private void GridFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            
            for (int i = 1; i < width; i++)
            {
                LineGeometry line = new LineGeometry(new Point(0, i),
                    new Point(width, i));
                geometryGroup.Children.Add(line);
            }
            
            GeometryDrawing geometryDrawing = new GeometryDrawing();
            geometryDrawing.Geometry = geometryGroup;
            
            geometryDrawing.Pen = new Pen(Brushes.Gray, 0.01);
            double[] dashes = { 1, 1, 1, 1, 1 };
            geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);
            
            geometryDrawing.Brush = Brushes.Beige;
            
            drawingGroup.Children.Add(geometryDrawing);
        }

        private void Fun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            //dataList[0].Max();
            for (int i = 0; i < dataList[0].Length-1; i++)
            {
                if (dataList[0][i] >= width || 10-dataList[1][i] <= 0)
                {
                    break;
                }
                LineGeometry line = new LineGeometry(
                    new Point(dataList[0][i]+10, 10-dataList[1][i]),
                    new Point(dataList[0][i + 1]+10, 10-dataList[1][i + 1])
                    );
                geometryGroup.Children.Add(line);

            }
            
            geometryDrawingFun.Geometry = geometryGroup;
            
            geometryDrawingFun.Pen = new Pen(Brushes.Red, 0.05);
            
            drawingGroup.Children.Add(geometryDrawingFun);
        }

        // Надписи
        private void MarkerFun()
        {
            GeometryGroup geometryGroup = new GeometryGroup();
            for (int i = 0; i <= 20; i++)
            {
                FormattedText formattedText = new FormattedText(
                String.Format("{0,7:F}", -(i-10)),
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                0.4,
                Brushes.Black);

                formattedText.SetFontWeight(FontWeights.Bold);

                Geometry geometry = formattedText.BuildGeometry(new Point(-1.75, i  - 0.03));
                geometryGroup.Children.Add(geometry);
            }

            geometryDrawingCaption.Geometry = geometryGroup;

            geometryDrawingCaption.Brush = Brushes.LightGray;
            geometryDrawingCaption.Pen = new Pen(Brushes.Gray, 0.005);

            drawingGroup.Children.Add(geometryDrawingCaption);
        }


        private void ChangeGraficColor(object sender, RoutedEventArgs e)
        {
            Rectangle rect = e.Source as Rectangle;
            geometryDrawingFun.Pen = new Pen(rect.Fill, 0.05);
        }



        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                geometryDrawingBG.Brush = new ImageBrush(new BitmapImage(new Uri(@openFileDialog.FileName)));
            }
        }

        bool error = false;
        private void TextBox_Change(object sender, RoutedEventArgs e)
        {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    String text = textBox.Text;

                if (Regex.IsMatch(text, @"^\d+$"))
                {
                    h = Int32.Parse(text);
                    textBox.Foreground = Brushes.Black;
                    error = false;
                }
                else
                {
                    textBox.Foreground = Brushes.Red;
                    error = true;
                }
            }
        }

        private void TextBox_Change1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                String text = textBox.Text;

                if (Regex.IsMatch(text, @"^\d+$"))
                {
                    R = Int32.Parse(text);
                    textBox.Foreground = Brushes.Black;
                    error = false;
                }
                else
                {
                    textBox.Foreground = Brushes.Red;
                    error = true;
                }
            }
        }

        private void RedrawGrafic(object sender, RoutedEventArgs e)
        {
            if (error == false)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    double angle = Math.PI * 2 / countDot * 2 * i;
                    x[i] = 2 * R * Math.Cos(angle) - h * Math.Cos(2 * angle);
                    y[i] = 2 * R * Math.Sin(angle) - h * Math.Sin(2 * angle);
                }
                Fun();
            }
        }

        private void SaveGrafic(object sender, RoutedEventArgs e)
        {
            image1.UpdateLayout();
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)image1.ActualWidth + 100, (int)image1.ActualHeight + 100, 96, 96, PixelFormats.Pbgra32);

            renderTargetBitmap.Render(image1);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                encoder.Save(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
