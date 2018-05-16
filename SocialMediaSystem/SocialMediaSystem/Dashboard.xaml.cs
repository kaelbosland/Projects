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

namespace SocialMediaSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : MetroWindow
    {

        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        private String user;
        public Dashboard(String username)
        {
            this.user = username;
            InitializeComponent();
        }

        private void changingTab(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedItem.Equals(tabControl.Items[0]))
            {
                //THIS IS THE MY FEED PAGE

            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[1]))
            {
                //THIS IS THE MAKE A POST PAGE

            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[2]))
            {
                //THIS IS THE FOLLOW SOMEONE PAGE
                ListBox maList = new ListBox();
                Canvas titles = new Canvas { Height = 50, Width = 800 };

                Label label1 = new Label { Content = "Username", Height = 100, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label label2 = new Label { Content = "First Name", Height = 100, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label label3 = new Label { Content = "Last Name", Height = 100, Width = 200, FontWeight = FontWeights.ExtraBold };

                Canvas.SetLeft(label1, 0);
                Canvas.SetLeft(label2, 200);
                Canvas.SetLeft(label3, 400);

                titles.Children.Add(label1);
                titles.Children.Add(label2);
                titles.Children.Add(label3);

                maList.Items.Add(titles);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("MSG_ShowUsers", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter u = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                    u.Value = user;
                    command.Parameters.Add(u);
                    
                    SqlDataReader reader = command.ExecuteReader();
                    Canvas data = new Canvas();

                    Button follow;

                    while (reader.Read())
                    {
                        if (!(reader["username"]).Equals(this.user))
                        {
                            data = new Canvas { Height = 50, Width = 800 };

                            label1 = new Label { Content = reader["username"] };
                            label2 = new Label { Content = reader["firstName"] };
                            label3 = new Label { Content = reader["lastName"] };
                            follow = new Button { Content = "Follow" };
                            follow.Click += (s, e2) => followUser(sender, e, label1.Content.ToString());

                            Canvas.SetLeft(label1, 0);
                            Canvas.SetLeft(label2, 200);
                            Canvas.SetLeft(label3, 400);
                            Canvas.SetLeft(follow, 600);

                            data.Children.Add(label1);
                            data.Children.Add(label2);
                            data.Children.Add(label3);
                            data.Children.Add(follow);

                            maList.Items.Add(data);
                        }
                    }
                    conn.Close();
                }

                people.Content = maList;

            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[3]))
            {
                //THIS IS THE SIGN OUT PAGE

            }
        }

        private void followUser(object sender, RoutedEventArgs e, String userToFollow)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("MSG_FollowUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter u = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
                u.Value = user;
                command.Parameters.Add(u);

                SqlParameter f = new SqlParameter("@userToFollow", System.Data.SqlDbType.VarChar);
                f.Value = userToFollow;
                command.Parameters.Add(f);

                command.ExecuteNonQuery();
                conn.Close();
            }


        }
    }
}
