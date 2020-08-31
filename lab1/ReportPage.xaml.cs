using System.Windows;
using System.Windows.Controls;

namespace lab1
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }
        
        public ReportPage(object data):this()
        {
            // Bind to expense report data.
            this.DataContext = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            var expenseReportPage = new ReportPage();
            this.NavigationService?.Navigate(expenseReportPage);
        }
    }
}