using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraficAndAnim
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			const int countDot = 120;
			var x = new double[countDot];
			var y = new double[countDot];
			for (var i = 0; i < x.Length; i++)
			{
				var angle = Math.PI * 2 / (countDot / 3) * i;
				x[i] = 10 * (Math.Cos(angle) + angle * Math.Sin(angle));
				y[i] = 10 * (Math.Sin(angle) - angle * Math.Cos(angle));
			}

			var path = new Path();
			var geometry = (PathGeometry) FindResource("pathg");
			;
			var figure = new PathFigure {StartPoint = new Point(150 + x[0], 150 - y[0])};
			var pBezierSegment = new PolyBezierSegment();

			for (var i = 1; i < x.Length; i++) pBezierSegment.Points.Add(new Point(150 + x[i], 150 - y[i]));

			figure.Segments.Add(pBezierSegment);
			geometry.Figures.Add(figure);
			path.Data = geometry;
		}
	}
}