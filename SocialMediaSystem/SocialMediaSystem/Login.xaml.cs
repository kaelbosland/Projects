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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace SocialMediaSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void signIn(object sender, RoutedEventArgs e)
        {
            firstName.Visibility = System.Windows.Visibility.Hidden;
            lastName.Visibility = System.Windows.Visibility.Hidden;
            register.Visibility = System.Windows.Visibility.Hidden;
            result.Text = "";

            if (username.Text.Length >= 5 && username.Text.Length <= 30)
            {
                if (password.Text.Length >= 5)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            SqlCommand command = new SqlCommand("MSG_SignIn", conn);
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            var returnParam = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                            returnParam.Direction = System.Data.ParameterDirection.ReturnValue;

                            SqlParameter u = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                            u.Value = username.Text;
                            command.Parameters.Add(u);

                            SqlParameter p = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
                            p.Value = password.Text;
                            command.Parameters.Add(p);

                            command.ExecuteNonQuery();
                            int r = Convert.ToInt32(returnParam.Value);
                            conn.Close();

                            switch (r)
                            {
                                case 0:
                                    result.Text = "That username does not exist.";
                                    break;
                                case 1:
                                    result.Text = "Incorrect password!";
                                    break;
                                case 5:
                                    result.Text = "Successful Login!";
                                    this.Hide();
                                    Dashboard dash = new Dashboard(username.Text);
                                    dash.Show();
                                    break;
                            }
                        }

                    } catch (SqlException se)  {
                        result.Text = "Unknown error occured! @kael what you doing fam";
                    }  
                }
            }

            username.Text = "";
            password.Text = "";
        }

        private void signUp(object sender, RoutedEventArgs e)
        {
            username.Text = "";
            password.Text = "";
            firstName.Text = "";
            lastName.Text = "";
            firstName.Visibility = System.Windows.Visibility.Visible;
            lastName.Visibility = System.Windows.Visibility.Visible;
        }

        private void sendRegisterQuery (object sender, RoutedEventArgs e)
        {

            if (username.Text.Length >= 5 && username.Text.Length <= 30)
            {
                if (password.Text.Length >= 5)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            SqlCommand command = new SqlCommand("MSG_SignUp", conn);
                            command.CommandType = System.Data.CommandType.StoredProcedure;

                            var returnParam = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                            returnParam.Direction = System.Data.ParameterDirection.ReturnValue;

                            SqlParameter u = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                            u.Value = username.Text;
                            command.Parameters.Add(u);

                            SqlParameter p = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
                            p.Value = password.Text;
                            command.Parameters.Add(p);

                            SqlParameter f = new SqlParameter("@firstName", System.Data.SqlDbType.VarChar);
                            f.Value = firstName.Text;
                            command.Parameters.Add(f);


                            SqlParameter l = new SqlParameter("@lastName", System.Data.SqlDbType.VarChar);
                            l.Value = lastName.Text;
                            command.Parameters.Add(l);

                            command.ExecuteNonQuery();
                            conn.Close();

                            int r = Convert.ToInt32(returnParam.Value);

                            if (r == 5)
                            {
                                result.Text = "You were added into the system correctly. Return to Sign In Screen to continue.";
                            } else if (r == 0)
                            {
                                result.Text = "The username was already taken, try again";
                                username.Text = "";
                            }
                        }
                    } catch (SqlException se)
                    {
                        result.Text = "Unknown error occured! @kael what you doing fam";
                    }
                } else
                {
                    result.Text = "Password must be at least 5 characters";
                }
            } else
            {
                result.Text = "Username must be at least 5 characters, and less than 30 characters.";
            }

        }

        private void doneRegistering (object sender, RoutedEventArgs e)
        {
            register.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
