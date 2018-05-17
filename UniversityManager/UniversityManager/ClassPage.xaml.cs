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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data.SqlClient;

namespace UniversityManager
{
    /// <summary>
    /// Interaction logic for ClassPage.xaml
    /// </summary>
    public partial class ClassPage : MetroWindow
    {

        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        private string one;
        private string two;
        private string three;
        private string four;
        private List<Tuple<String, String>> news = new List<Tuple<String, String>>();

        public ClassPage(int pID)
        {
            InitializeComponent();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("UNI_WhatClasses", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@pID", System.Data.SqlDbType.Int);
                id.Value = pID;
                command.Parameters.Add(id);
                Console.WriteLine(id.Value);
                SqlDataReader reader = command.ExecuteReader();

                SqlParameter c1 = new SqlParameter("@classOne", System.Data.SqlDbType.VarChar);
                SqlParameter c2 = new SqlParameter("@classTwo", System.Data.SqlDbType.VarChar);
                SqlParameter c3 = new SqlParameter("@classThree", System.Data.SqlDbType.VarChar);
                SqlParameter c4 = new SqlParameter("@classFour", System.Data.SqlDbType.VarChar);

                while (reader.Read())
                {
                    this.one = checkSafe(reader["classOne"]);
                    this.two = checkSafe(reader["classTwo"]);
                    this.three = checkSafe(reader["classThree"]);
                    this.four = checkSafe(reader["classFour"]);

                    classOne.Header = checkSafe(this.one);
                    classTwo.Header = checkSafe(this.two);
                    classThree.Header = checkSafe(this.three);
                    classFour.Header = checkSafe(this.four);
                }

                reader.Close();

                c1.Value = this.one;
                c2.Value = this.two;
                c3.Value = this.three;
                c4.Value = this.four;

                command = new SqlCommand("UNI_GetAnnouncements", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(c1);
                command.Parameters.Add(c2);
                command.Parameters.Add(c3);
                command.Parameters.Add(c4);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    news.Add(new Tuple<string, string>(checkSafe((string)reader["announcement"]), checkSafe((string)reader["courseCode"])));
                }
                conn.Close();
            }
        }

        public void changingTab(object sender, SelectionChangedEventArgs e)
        {
            String selectedClass = "";

            if (tabControl.SelectedItem.Equals(tabControl.Items[0]))
            {
                //CLASS ONE TAB SELECTED
                selectedClass = (string)classOne.Header;
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[1]))
            {
                //CLASS TWO TAB SELECTED
                selectedClass = (string)classTwo.Header;
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[2]))
            {
                //CLASS THREE TAB SELECTED
                selectedClass = (string)classThree.Header;
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[3]))
            {
                //CLASS FOUR TAB SELECTED
                selectedClass = (string)classFour.Header;
            }

            ListBox announcements = new ListBox();

            if (selectedClass.Equals(this.one))
            {
                announcements = filterAnnouncements(this.one);
                classOne.Content = announcements;
            }
            else if (selectedClass.Equals(this.two))
            {
                announcements = filterAnnouncements(this.two);
                classTwo.Content = announcements;
            }
            else if (selectedClass.Equals(this.three))
            {
                announcements = filterAnnouncements(this.three);
                classThree.Content = announcements;
            }
            else
            {
                announcements = filterAnnouncements(this.four);
                classFour.Content = announcements;
            }
        }

        public string checkSafe(object s)
        {
            if (s.Equals(DBNull.Value))
            {
                return "";
            }
            else
            {
                return ((string)s).ToUpper();
            }
        }

        public ListBox filterAnnouncements(String courseCode)
        {
            ListBox myEmail = new ListBox();
            Canvas titles = new Canvas { Height = 50, Width = 1000 };

            Label id = new Label { Content = "CourseCode", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
            Label from = new Label { Content = "Message", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };

            Canvas.SetLeft(id, 0);
            Canvas.SetLeft(from, 100);


            titles.Children.Add(id);
            titles.Children.Add(from);


            myEmail.Items.Add(titles);

            for (int i = 0; i < news.Count; i++)
            {
                if (news[i].Item2.Equals(courseCode))
                {
                    Canvas data = new Canvas { Height = 50, Width = 1000 };

                    id = new Label { Content = news[i].Item2, Height = 50, Width = 200 };
                    TextBox a = new TextBox { Text = news[i].Item1, Height = 50, Width = 500, IsEnabled = false };
                    a.BorderThickness = new Thickness(0, 0, 0, 0);
                    a.TextWrapping = TextWrapping.Wrap;
                    Canvas.SetLeft(id, 0);
                    Canvas.SetLeft(a, 100);

                    data.Children.Add(id);
                    data.Children.Add(a);

                    myEmail.Items.Add(data);
                }
            }
            return myEmail;
        }
    }
}