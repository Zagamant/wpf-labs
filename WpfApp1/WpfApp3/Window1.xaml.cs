﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Task3
{
    public partial class Window1 : Window
    {

        private double  _originalLeft, 
                        _originalTop;

        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            // Возвращаем Thumb в исходное состояние
            Canvas.SetLeft(thumb1, 5);
            Canvas.SetTop(thumb1, 5);
        }

        private void thumb1_DragStarted(object sender, DragStartedEventArgs e)
        {
            _originalLeft = Canvas.GetLeft(thumb1);
            _originalTop = Canvas.GetTop(thumb1);
        }

        private void thumb1_DragDelta(object sender, DragDeltaEventArgs e)
        {
	        var left = _originalLeft + e.HorizontalChange;
	        var top = _originalTop + e.VerticalChange;
	        left = (left > this.ActualWidth - 50) ? (this.ActualWidth - 50) : left;
	        top = top > this.ActualHeight - 50 ? this.ActualHeight - 50 : top;
	        Canvas.SetLeft(thumb1, left);
	        Canvas.SetTop(thumb1, top);
	        _originalLeft = left;
	        _originalTop = top;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }
    }
}
