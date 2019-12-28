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

namespace WpfApp9
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
    }
}

namespace WpfApp9
{
    class MyPanel : Panel
    {
        // Измерение
        //
        // На входе - сколько есть у родителя для менеджера
        // На выходе - сколько просит менеджер для себя
        protected override Size MeasureOverride(Size availableSize)
        {
            double maxChildWidth = 0.0;
            double maxChildHeight = 0.0;

            // Измерить требования каждого потомка и определить самые большие
            foreach (UIElement child in this.InternalChildren)
            {
                child.Measure(availableSize);
                maxChildWidth = Math.Max(child.DesiredSize.Width, maxChildWidth);
                maxChildHeight = Math.Max(child.DesiredSize.Height, maxChildHeight);
            }

            // Приблизительный несовершенный алгоритм
            //
            // Требуемая для размещения всех элементов длина окружности
            double idealCircumference =
                maxChildWidth * this.InternalChildren.Count;
            // Требуемый радиус окружности
            double idealRadius =
                (idealCircumference / (Math.PI * 2) + maxChildHeight);
            // Необходимые размеры описывающего окружность квадрата
            Size ideal = new Size(idealRadius * 2, idealRadius * 2);

            Size desired = ideal;// Менеджер столько хочет для себя
            // Если выделяемый менеджеру размер небесконечен
            if (!double.IsInfinity(availableSize.Width))
            {
                // Если требуемого размера менеджеру нехватает
                if (availableSize.Width < desired.Width)
                {
                    // Корректируем размер менеджера
                    desired.Width = availableSize.Width;
                }
            }
            // То же самое и для высоты
            if (!double.IsInfinity(availableSize.Height))
            {
                if (availableSize.Height < desired.Height)
                {
                    desired.Height = availableSize.Height;
                }
            }

            return desired;
        }

        // Установка (заключение контракта)
        //
        // Делим отведенную для менеджера площадь
        protected override Size ArrangeOverride(Size finalSize)
        {
            // Размещаем квадратный менеджер в
            // центре выделенной родителем области
            Rect layoutRect;
            if (finalSize.Width > finalSize.Height)
            {
                layoutRect = new Rect(
                    (finalSize.Width - finalSize.Height) / 2,
                    0,
                    finalSize.Height,
                    finalSize.Height);
            }
            else
            {
                layoutRect = new Rect(
                    0,
                    (finalSize.Height - finalSize.Width) / 2,
                    finalSize.Width,
                    finalSize.Width);
            }

            double angleInc = 360.0 / this.InternalChildren.Count;
            double angle = 0;

            // Расставляем по кругу все дочерние элементы
            foreach (UIElement child in this.InternalChildren)
            {
                // Располагаем очередной элемент в верхней точке круга
                Point childLocation = new Point(
                    layoutRect.Left + ((layoutRect.Width - child.DesiredSize.Width) / 2),
                    layoutRect.Top);

                // Переместили по часовой стрелке и повернули вокруг оси
                child.RenderTransform = new RotateTransform(
                    angle,
                    child.DesiredSize.Width / 2,
                    finalSize.Height / 2 - layoutRect.Top);

                // Утвердили окончательный размер и положение потомка
                child.Arrange(new Rect(childLocation, child.DesiredSize));

                angle += angleInc;
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
