using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using MahApps.Metro.Controls;

namespace UniversityManager
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : MetroWindow
    {

        //0 = student, 1 = professor
        int typeProfile = -1;
        int profilePID = -1;
        Window w;
        Student s;
        Professor p;


        public Profile(int i, int pID, Window w, object s)
        {
            InitializeComponent();
            this.s = (i == 0) ? (Student)s : null;
            this.p = (i == 1) ? (Professor)s : null;
            this.typeProfile = i;
            this.profilePID = pID;
            this.w = w;
            w.Hide();
            setUp();
        }

        private void setUp()
        {

            text1.Text = this.profilePID.ToString();
            if (this.typeProfile == 0)
            {
                this.Title = s.firstName + " " + s.lastName;
                four.Content = "Year:";
                five.Content = "Major:";
                six.Content = "Class One:";
                seven.Content = "Class Two:";
                eight.Content = "Class Three:";
                nine.Content = "Class Four:";

                text2.Text = s.firstName;
                text3.Text = s.lastName;
                text4.Text = s.year.ToString();
                text5.Text = s.major;
                text6.Text = s.classOne;
                text7.Text = s.classTwo;
                text8.Text = s.classThree;
                text9.Text = s.classFour;
            }
            else
            {
                this.Title = p.firstName + " " + p.lastName;
                four.Content = "Department:";
                five.Content = "Class One:";
                six.Content = "Class Two:";

                text2.Text = p.firstName;
                text3.Text = p.lastName;
                text4.Text = p.department;
                text5.Text = p.classOne;
                text6.Text = p.classTwo;

                text7.Visibility = System.Windows.Visibility.Hidden;
                seven.Visibility = System.Windows.Visibility.Hidden;
                eight.Visibility = System.Windows.Visibility.Hidden;
                nine.Visibility = System.Windows.Visibility.Hidden;
                text8.Visibility = System.Windows.Visibility.Hidden;
                text9.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            w.Show();
        }
    }
}