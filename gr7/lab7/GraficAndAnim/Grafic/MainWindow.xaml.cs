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

namespace Grafic
{
	public partial class Window1 : Window
	{
		private const int CountDot = 200;

		public readonly List<double[]> DataList = new List<double[]>();
		private readonly DrawingGroup _drawingGroup = new DrawingGroup();

		private bool _error;
		private readonly GeometryDrawing _geometryDrawingBg = new GeometryDrawing();
		private readonly GeometryDrawing _geometryDrawingCaption = new GeometryDrawing();
		private readonly GeometryDrawing _geometryDrawingFun = new GeometryDrawing();
		private double _h = 3;
		private double _r = 3;
		private readonly RectangleGeometry _rectGeometryBg = new RectangleGeometry();
		private readonly int _width = 20;
		private readonly double[] _x = new double[CountDot + 1];
		private readonly double[] _y = new double[CountDot + 1];


		public Window1()
		{
			InitializeComponent();

			DataFill(); // Заполнение списка данными
			Execute(); // Заполнение слоев

			// Отображение на экране
			image1.Source = new DrawingImage(_drawingGroup);
		}

		// Генерация точек графиков
		private void DataFill()
		{
			for (var i = 0; i < _x.Length; i++)
			{
				var angle = Math.PI * 2 / CountDot * 4 * i;
				_x[i] = 2 * _r * Math.Cos(angle) - _h * Math.Cos(2 * angle);
				_y[i] = 2 * _r * Math.Sin(angle) - _h * Math.Sin(2 * angle);
			}

			DataList.Add(_x);
			DataList.Add(_y);
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
			_rectGeometryBg.Rect = new Rect(0, 0, _width, _width);
			_geometryDrawingBg.Geometry = _rectGeometryBg;

			_geometryDrawingBg.Pen = new Pen(Brushes.Gray, 0.01);
			_geometryDrawingBg.Brush = Brushes.White;

			_drawingGroup.Children.Add(_geometryDrawingBg);
		}

		// Горизонтальная сетка
		private void GridFun()
		{
			var geometryGroup = new GeometryGroup();

			for (var i = 1; i < _width; i++)
			{
				var line = new LineGeometry(new Point(0, i),
					new Point(_width, i));
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup, Pen = new Pen(Brushes.Gray, 0.01)
			};

			double[] dashes = {1, 1, 1, 1, 1};
			geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);

			geometryDrawing.Brush = Brushes.Beige;

			_drawingGroup.Children.Add(geometryDrawing);
		}

		private void Fun()
		{
			var geometryGroup = new GeometryGroup();
			//dataList[0].Max();
			for (var i = 0; i < DataList[0].Length - 1; i++)
			{
				if (DataList[0][i] >= _width || 10 - DataList[1][i] <= 0) break;
				var line = new LineGeometry(
					new Point(DataList[0][i] + 10, 10 - DataList[1][i]),
					new Point(DataList[0][i + 1] + 10, 10 - DataList[1][i + 1])
				);
				geometryGroup.Children.Add(line);
			}
		
			_geometryDrawingFun.Geometry = geometryGroup;

			_geometryDrawingFun.Pen = new Pen(Brushes.Red, 0.05);
		
			_drawingGroup.Children.Add(_geometryDrawingFun);
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

			_geometryDrawingCaption.Geometry = geometryGroup;

			_geometryDrawingCaption.Brush = Brushes.LightGray;
			_geometryDrawingCaption.Pen = new Pen(Brushes.Gray, 0.005);

			_drawingGroup.Children.Add(_geometryDrawingCaption);
		}


		private void ChangeGraficColor(object sender, RoutedEventArgs e)
		{
			var rect = e.Source as Rectangle;
			_geometryDrawingFun.Pen = new Pen(rect.Fill, 0.05);
		}


		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
				_geometryDrawingBg.Brush = new ImageBrush(new BitmapImage(new Uri(openFileDialog.FileName)));
		}

		private void TextBox_Change(object sender, RoutedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				var text = textBox.Text;

				if (Regex.IsMatch(text, @"^\d+$"))
				{
					_h = int.Parse(text);
					textBox.Foreground = Brushes.Black;
					_error = false;
				}
				else
				{
					textBox.Foreground = Brushes.Red;
					_error = true;
				}
			}
		}

		private void TextBox_Change1(object sender, RoutedEventArgs e)
		{
			if (!(sender is TextBox textBox)) return;
			var text = textBox.Text;

			if (Regex.IsMatch(text, @"^\d+$"))
			{
				_r = int.Parse(text);
				textBox.Foreground = Brushes.Black;
				_error = false;
			}
			else
			{
				textBox.Foreground = Brushes.Red;
				_error = true;
			}
		}

		private void RedrawGrafic(object sender, RoutedEventArgs e)
		{
			if (_error == false)
			{
				for (var i = 0; i < _x.Length; i++)
				{
					var angle = Math.PI * 2 / CountDot * 2 * i;
					_x[i] = 2 * _r * Math.Cos(angle) - _h * Math.Cos(2 * angle);
					_y[i] = 2 * _r * Math.Sin(angle) - _h * Math.Sin(2 * angle);
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
			var saveFileDialog1 = new SaveFileDialog
			{
				Filter = "PNG Image|*.png",
				Title = "Save an Image File"
			};
			saveFileDialog1.ShowDialog();

			if (saveFileDialog1.FileName == "") return;

			var fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
			encoder.Save(fileStream);
			fileStream.Flush();
			fileStream.Close();
		}
	}
}