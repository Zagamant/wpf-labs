using System;
using System.Windows;

namespace Task1
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new CreateBrushes());
        }
    }
}
