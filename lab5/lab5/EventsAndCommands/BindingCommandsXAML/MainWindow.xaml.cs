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

namespace BindingCommandsXAML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик команды Open
        System.Windows.Forms.OpenFileDialog openFileDialog = null;
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (openFileDialog == null)
                openFileDialog = new System.Windows.Forms.OpenFileDialog();

            openFileDialog.ShowDialog();
            this.Focus();
        }

        // Обработчик команды Save
        System.Windows.Forms.SaveFileDialog saveFileDialog = null;
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (saveFileDialog == null)
                saveFileDialog = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog.ShowDialog();
            this.Focus();
        }

        // Выход по клавише Esc с предупреждением 
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    MessageBoxResult result = MessageBox.Show("Закрыть приложение ?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                        this.Close();
                    break;
            }
        }

        private void directCommandsOpen_Click(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Open.Execute(null, this);
        }

        private void directCommandsSave_Click(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Save.Execute(null, this);
        }

        private void directBindingsOpen_Click(object sender, RoutedEventArgs e)
        {
            this.CommandBindings[0].Command.Execute(null);
        }

        private void directBindingsSave_Click(object sender, RoutedEventArgs e)
        {
            this.CommandBindings[1].Command.Execute(null);
        }


    }

}
