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

namespace Desktop_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        List<string> Operators;
        History LastHistory;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            // for API Call
            AppBL appBL = new AppBL();
            string calcResult = appBL.Calc(txbField1.Text, CmbOperator.Text, txbField2.Text);
            txtResult.Text = "Result: " + calcResult;

            // lest show to the user some history
            txtHistory.Visibility = Visibility.Visible;
            HistoryTable.Visibility = Visibility.Visible;

            string countResult = appBL.GetCountThisMonth(CmbOperator.Text);
            txtCount.Text = "Total calculations for this operator since the start of the month: " + countResult;

            viewModel.LoadHistoryData(CmbOperator.Text);
        }
    }
}
