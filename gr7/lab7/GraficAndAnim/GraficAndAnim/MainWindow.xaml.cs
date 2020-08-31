using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficAndAnim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            const int countDot = 120;
            double[] x = new double[countDot];
            double[] y = new double[countDot];
            for (int i = 0; i < x.Length; i++)
            {
                double angle = Math.PI * 2 / (countDot/3) * i;
                x[i] = 2 * 30 * Math.Cos(angle) - 25 * Math.Cos(2 * angle);
                y[i] = 2 * 30 * Math.Sin(angle) - 25 * Math.Sin(2 * angle);
            }

            Path path = new Path();
            PathGeometry geometry = (PathGeometry)FindResource("pathg"); ;
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(250+x[0], 100-y[0]);
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();

            for (int i = 1; i < x.Length; i++)
            {
                pBezierSegment.Points.Add(new Point(250+x[i], 100-y[i]));
            }

            figure.Segments.Add(pBezierSegment);
            geometry.Figures.Add(figure);
            path.Data = geometry;
        }

    }

}
