﻿using System;
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

namespace WpfApp4
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            textBox.Text = "Привет студентам от Снеткова В.М.!";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(textBox.Text);
        }
    }
}