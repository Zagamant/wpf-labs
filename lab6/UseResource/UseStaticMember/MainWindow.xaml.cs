
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
    
namespace UseStaticMember
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    
    public static class UserClass
    {
        // Статические поля только для чтения
        public static readonly LinearGradientBrush backgroundBrush = // Имя любое
            new LinearGradientBrush(Colors.Blue,
                Colors.White, new Point(0.5, 0.5), new Point(1, 1));
        public static readonly LinearGradientBrush foregroundBrush = // Имя любое
            new LinearGradientBrush(Colors.Red,
                Colors.White, new Point(1, 1), new Point(0, 0));
        public static readonly Double FontSize = 24;    // Имя любое
        public static readonly FontFamily FontFamily =  // Имя любое
            new FontFamily("Times New Roman Italic");
    
        // Статические свойства доступа
        public static String OSVersion  // Имя любое      
        {
            get { return Environment.OSVersion.ToString(); }
        }
    
        public static String Version    // Имя любое
        {
            get { return Environment.Version.ToString(); }
        }
    
        public static String DateTime   // Имя любое
        {
            get { return System.DateTime.Now.ToString(); }
        }
    }
}

