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

namespace BudgetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignUp(object sender, RoutedEventArgs e)
        {
            string user = username.Text;
            float starting = float.Parse(startAmt.Text);
            int time = int.Parse(timeline.Text);
            string startDate = DateTime.Today.ToString("dd-MM-yyyy");
            Profile p = new Profile { username = user, startingAmount = starting, timelineInWeeks = time, startDate = startDate };
            p.addToDatabase();
        }
    }
}
