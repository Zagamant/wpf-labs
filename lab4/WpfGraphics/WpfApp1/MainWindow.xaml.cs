using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Task1
{
	public partial class MainWindow : Window
	{
		private const int CountDot = 20;

		private readonly List<double[]> _dataList = new List<double[]>();

		private readonly DrawingGroup _drawingGroup = new DrawingGroup();

		[Obsolete]
		public MainWindow()
		{
			InitializeComponent();

			DataFill(); // Заполнение списка данными
			Execute(); // Заполнение слоев

			// Отображение на экране
			Image1.Source = new DrawingImage(_drawingGroup);
		}

		// Генерация точек графиков
		private void DataFill()
		{
			var sin = new double[CountDot + 1];
			var cos = new double[CountDot + 1];
			var x3 = new double[CountDot + 1];
			var x4 = new double[CountDot + 1];

			for (var i = 0; i < sin.Length; i++)
			{
				var angle = Math.PI * 2 / CountDot * i;
				sin[i] = Math.Sin(angle);
				cos[i] = Math.Cos(angle);
				x3[i] = i - 1;
				x4[i] = i + 1;
			}

			_dataList.Add(sin);
			_dataList.Add(cos);
			_dataList.Add(x3);
			_dataList.Add(x4);
		}

		[Obsolete]
		private void Execute()
		{
			BackgroundFun();
			GridFun();
			SinFun();
			CosFun();
			X3Fun();
			X4Fun();
			//PointsFun();
			MarkerFun();
		}

		// Фон
		private void BackgroundFun()
		{
			var geometryDrawing = new GeometryDrawing();

			var rectGeometry = new RectangleGeometry();
			rectGeometry.Rect = new Rect(0, 0, 1, 1);
			geometryDrawing.Geometry = rectGeometry;

			geometryDrawing.Pen = new Pen(Brushes.BurlyWood, 0.01);
			geometryDrawing.Brush = Brushes.SeaShell;

			_drawingGroup.Children.Add(geometryDrawing);
		}

		// Горизонтальная сетка
		private void GridFun()
		{
			var geometryGroup = new GeometryGroup();

			for (var i = 1; i < 10; i++)
			{
				var line = new LineGeometry(new Point(1.0, i * 0.1), new Point(-0.1, i * 0.1));
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup,
				Pen = new Pen(Brushes.Tan, 0.003)
			};

			double[] dashes = {1, 1, 1, 1, 1};
			geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);

			geometryDrawing.Brush = Brushes.Beige;

			_drawingGroup.Children.Add(geometryDrawing);
		}

		// Строим синус линией
		private void SinFun()
		{
			// Описание синусоиды
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < _dataList[0].Length - 1; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double) CountDot, 0.5 - _dataList[0][i] / 2.0),
					new Point((i + 1) / (double) CountDot, 0.5 - _dataList[0][i + 1] / 2.0)
				);
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			geometryDrawing.Pen = new Pen(Brushes.Purple, 0.005);

			_drawingGroup.Children.Add(geometryDrawing);
		}


		private void PointsFun()
		{
			// Описание синусоиды
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < 4; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double) CountDot, 0.45 - _dataList[2][i] / 2.0),
					new Point((i + 1) / (double) CountDot, 0.45 - _dataList[2][i + 1] / 2.0)
				);
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup,
				Pen = new Pen(Brushes.Blue, 0.005)
			};


			_drawingGroup.Children.Add(geometryDrawing);
		}

		// Строим косинус точками
		private void CosFun()
		{
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < _dataList[1].Length; i++)
			{
				var ellipsis = new EllipseGeometry(
					new Point(
						i / (double) CountDot, 
						0.5 - _dataList[1][i] / 2.0), 0.01,
					0.01);
				geometryGroup.Children.Add(ellipsis);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup,
				Pen = new Pen(Brushes.Teal, 0.005)
			};


			_drawingGroup.Children.Add(geometryDrawing);
		}

		// Надписи
		[Obsolete]
		private void MarkerFun()
		{
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i <= 10; i++)
			{
				var formattedText = new FormattedText(
					$"{1 - i * 0.2,7:F}",
					CultureInfo.InvariantCulture,
					FlowDirection.LeftToRight,
					new Typeface("Verdana"),
					0.05,
					Brushes.Black
				);

				formattedText.SetFontWeight(FontWeights.Bold);

				var geometry = formattedText.BuildGeometry(new Point(-0.2, i * 0.1 - 0.03));
				geometryGroup.Children.Add(geometry);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup,
				Brush = Brushes.Wheat,
				Pen = new Pen(Brushes.DarkGoldenrod, 0.003)
			};


			_drawingGroup.Children.Add(geometryDrawing);
		}


		//(x-1) ^3

		[Obsolete]
		private void X3Fun()
		{
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < 2; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double)CountDot, .5 -Math.Pow(_dataList[2][i], 3) / 2),
					new Point((i + 1) / (double)CountDot,  .5- Math.Pow( _dataList[2][i + 1],3)/2)
				);
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup, Pen = new Pen(Brushes.Black, 0.005)
			};


			_drawingGroup.Children.Add(geometryDrawing);
		}

		private void X4Fun()
		{
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < 2; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double)CountDot, .5 - 1/_dataList[2][i]),
					new Point((i + 1) / (double)CountDot, .5 - 1/_dataList[2][i + 1])
				);
				geometryGroup.Children.Add(line);
			}

			var geometryDrawing = new GeometryDrawing
			{
				Geometry = geometryGroup,
				Pen = new Pen(Brushes.Black, 0.005)
			};


			_drawingGroup.Children.Add(geometryDrawing);
		}
	}
}