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

namespace Notepad2
{
    partial class MainWindow
    {
        //------------------------------------------------------
        //
        //  Прочие обработчики
        //
        //------------------------------------------------------

        private void FontOnExecute(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog dlg =
                new System.Windows.Forms.FontDialog();
            switch (dlg.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.Abort:
                case System.Windows.Forms.DialogResult.Cancel:
                case System.Windows.Forms.DialogResult.Ignore:
                case System.Windows.Forms.DialogResult.No:
                case System.Windows.Forms.DialogResult.None:
                case System.Windows.Forms.DialogResult.OK:
                case System.Windows.Forms.DialogResult.Retry:
                case System.Windows.Forms.DialogResult.Yes:
                    break;
            }
        }

        private void WordWrapOnExecute(object sender, RoutedEventArgs e)
        {
            if (itemWordWrap.IsChecked)
                txtBox1.TextWrapping = TextWrapping.Wrap;
            else
                txtBox1.TextWrapping = TextWrapping.NoWrap;
        }

        private void AboutOnExecute(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Сведения об авторах...\n" +
                "Сведения о программе...", "About");
        }
    }
}
