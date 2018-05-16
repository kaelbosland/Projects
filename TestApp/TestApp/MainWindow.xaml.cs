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
using MahApps.Metro.Controls;
using System.Windows.Media.Animation;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            /*
            Storyboard story = new Storyboard();

            ColorAnimation first = new ColorAnimation();
            first.From = Colors.Cyan;
            first.To = Colors.Turquoise;
            first.BeginTime = TimeSpan.FromSeconds(0);
            first.Duration = new Duration(new TimeSpan());
            this.Background = new SolidColorBrush(Colors.Cyan);
            this.Background.BeginAnimation(SolidColorBrush.ColorProperty, first);
            story.Children.Add(first);
            
            story.Begin();
            */
        }

    }
}
