﻿using System;
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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            // Инициализация разметочной части
            InitializeComponent();

            // Корректировка заголовка окна
            this.Title += "=\"Так голодают буржуины!\"";

            // Дочернее окно не отображать в панели задач ОС 
            this.ShowInTaskbar = false;

            // Регистрация обработчика
            this.Closed += new EventHandler(Window2_Closed);
        }

        private void Window2_Closed(object sender, EventArgs e)
        {
            MainWindow.Wnd2 = null;        // Для предотвращения повторного запуска
            GC.WaitForFullGCComplete(); // Ждать завершения сборки мусора
            GC.Collect();               // Начать сборку мусора
        }
    }

}
