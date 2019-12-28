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

using System.Windows.Controls.Primitives;// Для ButtonBase

namespace WpfApp1
{
    partial class MainWindow
    {
        public static RoutedCommand ShowWork;
        public static RoutedCommand ShowContact;
        public static RoutedCommand ShowMain;

        static MainWindow()
        {
            InputGestureCollection coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F1, ModifierKeys.None, "F1"));
            ShowMain= new RoutedCommand("ShowMain", typeof(MainWindow), coll);

            coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F2, ModifierKeys.None, "F2"));
            ShowWork = new RoutedCommand("ShowWork", typeof(MainWindow), coll);

            coll = new InputGestureCollection();
            coll.Add(new KeyGesture(Key.F3, ModifierKeys.None, "F3"));
            ShowContact= new RoutedCommand("ShowContact", typeof(MainWindow), coll);
        }

    }
}
