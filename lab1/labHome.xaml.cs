using System.Windows;
using System.Windows.Controls;

namespace lab1
{
    public partial class LabHome : Page
    {
        public LabHome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            var expenseReportPage = new ReportPage(this.peopleListBox.SelectedItem);
            this.NavigationService.Navigate(expenseReportPage);

        }
    }
}