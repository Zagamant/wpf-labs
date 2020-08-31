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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Works.xaml
    /// </summary>
    public partial class Works : Window
    {
        public Works()
        {
            InitializeComponent();
        }
        private void WorkClick(object sender, RoutedEventArgs e)
        {
            Works works = new Works();
            this.Content = works.Content;
        }
        private void ContactClick(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            this.Content = contact.Content;
        }

        private void MainClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Content = mainWindow.Content;
        }
    }
}