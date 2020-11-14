using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
	public partial class MainWindow : Window
	{
		// Количество отрезков
		private const int countDot = 20;

		// Список для хранения данных
		private readonly List<double[]> dataList = new List<double[]>();

		// Контейнер слоев рисунков
		private readonly DrawingGroup drawingGroup = new DrawingGroup();

		[Obsolete]
		public MainWindow()
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
			var sin = new double[countDot + 1];
			var cos = new double[countDot + 1];
			var points = new double[countDot + 1];

			for (var i = 0; i < sin.Length; i++)
			{
				var angle = Math.PI * 2 / countDot * i;
				sin[i] = Math.Sin(angle);
				cos[i] = Math.Cos(angle);
				points[i] = Math.Cosh(angle) - 1;
			}

			dataList.Add(sin);
			dataList.Add(cos);
			dataList.Add(points);
			//косинус гиперболический
		}

		// Послойное формирование рисунка в Z-последовательности
		[Obsolete]
		private void Execute()
		{
			BackgroundFun(); // Фон
			GridFun(); // Мелкая сетка
			SinFun(); // Строим синус линией
			CosFun(); // Строим косинус точками
			PointsFun();
			MarkerFun(); // Подписи
			//PointsFun();
		}

		// Фон
		private void BackgroundFun()
		{
			// Создаем объект для описания геометрической фигуры
			var geometryDrawing = new GeometryDrawing();

			// Описываем и сохраняем геометрию квадрата
			var rectGeometry = new RectangleGeometry();
			rectGeometry.Rect = new Rect(0, 0, 1, 1);
			geometryDrawing.Geometry = rectGeometry;

			// Настраиваем перо и кисть
			geometryDrawing.Pen = new Pen(Brushes.BurlyWood, 0.01); // Перо рамки
			geometryDrawing.Brush = Brushes.SeaShell; // Кисть закраски

			// Добавляем готовый слой в контейнер отображения
			drawingGroup.Children.Add(geometryDrawing);
		}

		// Горизонтальная сетка
		private void GridFun()
		{
			// Создаем коллекцию для описания геометрических фигур
			var geometryGroup = new GeometryGroup();

			// Создаем и добавляем в коллекцию десять параллельных линий 
			for (var i = 1; i < 10; i++)
			{
				var line = new LineGeometry(new Point(1.0, i * 0.1), new Point(-0.1, i * 0.1));
				geometryGroup.Children.Add(line);
			}

			// Сохраняем описание геометрии
			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			// Настраиваем перо
			geometryDrawing.Pen = new Pen(Brushes.Tan, 0.003);
			double[] dashes = {1, 1, 1, 1, 1}; // Образец штриха
			geometryDrawing.Pen.DashStyle = new DashStyle(dashes, -.1);

			// Настраиваем кисть 
			geometryDrawing.Brush = Brushes.Beige;

			// Добавляем готовый слой в контейнер отображения
			drawingGroup.Children.Add(geometryDrawing);
		}

		// Строим синус линией
		private void SinFun()
		{
			// Строим описание синусоиды
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < dataList[0].Length - 1; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double) countDot, 0.5 - dataList[0][i] / 2.0),
					new Point((i + 1) / (double) countDot, 0.5 - dataList[0][i + 1] / 2.0)
				);
				geometryGroup.Children.Add(line);
			}

			// Сохраняем описание геометрии
			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			// Настраиваем перо
			geometryDrawing.Pen = new Pen(Brushes.Purple, 0.005);

			// Добавляем готовый слой в контейнер отображения
			drawingGroup.Children.Add(geometryDrawing);
		}


		private void PointsFun()
		{
			// Строим описание синусоиды
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < 4; i++)
			{
				var line = new LineGeometry(
					new Point(i / (double) countDot, 0.45 - dataList[2][i] / 2.0),
					new Point((i + 1) / (double) countDot, 0.45 - dataList[2][i + 1] / 2.0)
				);
				geometryGroup.Children.Add(line);
			}

			// Сохраняем описание геометрии
			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			// Настраиваем перо
			geometryDrawing.Pen = new Pen(Brushes.Blue, 0.005);

			// Добавляем готовый слой в контейнер отображения
			drawingGroup.Children.Add(geometryDrawing);
		}

		// Строим косинус точками
		private void CosFun()
		{
			// Строим описание косинусоиды
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i < dataList[1].Length; i++)
			{
				var ellips = new EllipseGeometry(new Point(i / (double) countDot, 0.5 - dataList[1][i] / 2.0), 0.01,
					0.01);
				geometryGroup.Children.Add(ellips);
			}

			// Сохраняем описание геометрии
			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			// Настраиваем перо
			geometryDrawing.Pen = new Pen(Brushes.Teal, 0.005);

			// Добавляем готовый слой в контейнер отображения
			drawingGroup.Children.Add(geometryDrawing);
		}

		// Надписи
		[Obsolete]
		private void MarkerFun()
		{
			var geometryGroup = new GeometryGroup();
			for (var i = 0; i <= 10; i++)
			{
				var formattedText = new FormattedText(
					string.Format("{0,7:F}", 1 - i * 0.2),
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

			var geometryDrawing = new GeometryDrawing();
			geometryDrawing.Geometry = geometryGroup;

			geometryDrawing.Brush = Brushes.Wheat;
			geometryDrawing.Pen = new Pen(Brushes.DarkGoldenrod, 0.003);

			drawingGroup.Children.Add(geometryDrawing);
		}
	}
}