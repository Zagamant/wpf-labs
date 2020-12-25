using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WpfApp1
{
	public partial class Window1 : Window
	{
		private const int countDot = 200;
		private double a = 1;

		private readonly List<double[]> dataList = new List<double[]>();
		private readonly DrawingGroup drawingGroup = new DrawingGroup();

		private bool error;
		private readonly GeometryDrawing geometryDrawingBG = new GeometryDrawing();
		private readonly GeometryDrawing geometryDrawingCaption = new GeometryDrawing();
		private readonly GeometryDrawing geometryDrawingFun = new GeometryDrawing();
		private readonly RectangleGeometry rectGeometryBG = new RectangleGeometry();
		private readonly int width = 20;
		private readonly double[] x = new double[countDot + 1];
		private readonly double[] y = new double[countDot + 1];


		public Window1()
		{
			InitializeComponent();

			DataFill(); // Заполнение списка данными
			Execute(); // Заполнение слоев

			// Отображение на экране
			image1.Source = new DrawingImage(drawingGroup);
		}

		// Генерация точек графиков
		private void DataFill()
		{
			for (var i = 0; i < x.Length; i++)
			{
				var angle = Math.PI * 2 / countDot * 4 * i;
				x[i] = a * (Math.Cos(angle) + angle * Math.Sin(angle));
				y[i] = a * (Math.Sin(angle) - angle * Math.Cos(angle));
			}

			dataList.Add(x);
			dataList.Add(y);
		}

		// Послойное формирование рисунка в Z-последовательности
		private void Execute()
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
			var geometryGroup = new GeometryGroup();

			for (var i = 1; i < width; i++)
			{
				var line = new LineGeometry(new Point(0, i),
					new Point(width, i));
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			geometryDrawing.Pen = new Pen(Brushes.Gray, 0.01);
			double[] dashes = {1, 1, 1, 1, 1};
			geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);

			geometryDrawing.Brush = Brushes.Beige;

			drawingGroup.Children.Add(geometryDrawing);
		}

		private void Fun()
		{
			var geometryGroup = new GeometryGroup();
			//dataList[0].Max();
			for (var i = 0; i < dataList[0].Length - 1; i++)
			{
				if (dataList[0][i] >= width || 10 - dataList[1][i] <= 0) break;
				var line = new LineGeometry(
					new Point(dataList[0][i], 10 - dataList[1][i]),
					new Point(dataList[0][i + 1], 10 - dataList[1][i + 1])
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
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i <= 20; i++)
			{
				var formattedText = new FormattedText(
					$"{-(i - 10),7:F}",
					CultureInfo.InvariantCulture,
					FlowDirection.LeftToRight,
					new Typeface("Verdana"),
					0.4,
					Brushes.Black);

				formattedText.SetFontWeight(FontWeights.Bold);

				var geometry = formattedText.BuildGeometry(new Point(-1.75, i - 0.03));
				geometryGroup.Children.Add(geometry);
			}

			geometryDrawingCaption.Geometry = geometryGroup;

			geometryDrawingCaption.Brush = Brushes.LightGray;
			geometryDrawingCaption.Pen = new Pen(Brushes.Gray, 0.005);

			drawingGroup.Children.Add(geometryDrawingCaption);
		}


		private void ChangeGraficColor(object sender, RoutedEventArgs e)
		{
			var rect = e.Source as Rectangle;
			geometryDrawingFun.Pen = new Pen(rect.Fill, 0.05);
		}

		private void ChangeCaptionColor(object sender, RoutedEventArgs e)
		{
			var rect = e.Source as Rectangle;
			Name.Foreground = rect.Fill;
		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
				geometryDrawingBG.Brush = new ImageBrush(new BitmapImage(new Uri(openFileDialog.FileName)));
		}

		private void TextBox_Change(object sender, RoutedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox != null)
			{
				var text = textBox.Text;

				if (Regex.IsMatch(text, @"^\d+$"))
				{
					a = int.Parse(text);
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
				for (var i = 0; i < x.Length; i++)
				{
					var angle = Math.PI * 2 / countDot * 2 * i;
					x[i] = a * (Math.Cos(angle) + angle * Math.Sin(angle));
					y[i] = a * (Math.Sin(angle) - angle * Math.Cos(angle));
				}

				Fun();
			}
		}

		private void RedrawGrafic1()
		{
			if (error == false)
			{
				for (var i = 0; i < x.Length; i++)
				{
					var angle = Math.PI * 2 / countDot * 2 * i;
					x[i] = a * (Math.Cos(angle) + angle * Math.Sin(angle));
					y[i] = a * (Math.Sin(angle) - angle * Math.Cos(angle));
				}

				Fun();
			}
		}

		private void SaveGrafic(object sender, RoutedEventArgs e)
		{
			image1.UpdateLayout();
			var renderTargetBitmap = new RenderTargetBitmap((int) image1.ActualWidth + 100,
				(int) image1.ActualHeight + 100, 96, 96, PixelFormats.Pbgra32);

			renderTargetBitmap.Render(image1);

			var encoder = new PngBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			var saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "PNG Image|*.png";
			saveFileDialog1.Title = "Save an Image File";
			saveFileDialog1.ShowDialog();

			if (saveFileDialog1.FileName != "")
			{
				var fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
				encoder.Save(fileStream);
				fileStream.Flush();
				fileStream.Close();
			}
		}
	}
}