using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Task9
{
    internal class CustomPanel : Panel
    {
        /// <summary>
        /// Измерение
        /// На входе - сколько есть у родителя для менеджера
        /// На выходе - сколько просит менеджер для себя
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var maxChildWidth = 0.0;
            var maxChildHeight = 0.0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                maxChildWidth = Math.Max(child.DesiredSize.Width, maxChildWidth);
                maxChildHeight = Math.Max(child.DesiredSize.Height, maxChildHeight);
            }

            var idealCircumference =
                maxChildWidth * InternalChildren.Count;

            var idealRadius =
                idealCircumference / (Math.PI * 2) + maxChildHeight;

            var ideal = new Size(idealRadius * 2, idealRadius * 2);

            var desired = ideal; 

            if (!double.IsInfinity(availableSize.Width) && availableSize.Width < desired.Width)
            {
                desired.Width = availableSize.Width;
            }

            if (double.IsInfinity(availableSize.Height))
            {
                return desired;
            }

            if (availableSize.Height < desired.Height)
            {
                desired.Height = availableSize.Height;
            }

            return desired;
        }

        /// <summary>
        /// Установка (заключение контракта)
        /// Делится отведенная для менеджера площадь
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {

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

            var angleInc = 360.0 / InternalChildren.Count;
            var angle = 0.0;

            // Расставляем по кругу все дочерние элементы
            foreach (UIElement child in InternalChildren)
            {
                var childLocation = new Point(
                    layoutRect.Left + (layoutRect.Width - child.DesiredSize.Width) / 2,
                    layoutRect.Top);

                child.RenderTransform = new RotateTransform(
                    angle,
                    child.DesiredSize.Width / 2,
                    finalSize.Height / 2 - layoutRect.Top);

                child.Arrange(new Rect(childLocation, child.DesiredSize));

                angle += angleInc;
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}