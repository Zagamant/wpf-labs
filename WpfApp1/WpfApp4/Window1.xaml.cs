using System.Windows;

namespace Task4
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            textBox.Text = "Привет";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(textBox.Text);
        }
    }
}
