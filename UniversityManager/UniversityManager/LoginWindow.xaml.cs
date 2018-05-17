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
using System.Data.SqlClient;
using MahApps.Metro.Controls;

namespace UniversityManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        int c = -1;

        //0 = student, 1 = prof, 2 = admin!
        public int identity = -1;
        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            type.Visibility = System.Windows.Visibility.Hidden;
            typeLabel.Visibility = System.Windows.Visibility.Hidden;

            if (username.Text.Length >= 1 && password.Text.Length >= 1 && pID.Text.Length >= 1)
            {
                c = 0;
                tryLogin();
            }
        }

        private void tryLogin()
        {
            int r = -1;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String procedure = "UNI_CheckUsername";
                    SqlCommand command = new SqlCommand(procedure, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter pIDParam = new SqlParameter("@pid", System.Data.SqlDbType.Int);
                    pIDParam.Value = Int32.Parse(pID.Text);
                    command.Parameters.Add(pIDParam);

                    SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                    usernameParam.Value = (username.Text);
                    command.Parameters.Add(usernameParam);

                    SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
                    passwordParam.Value = (password.Text);
                    command.Parameters.Add(passwordParam);

                    command.ExecuteNonQuery();
                    r = Convert.ToInt32(returnParameter.Value);

                    switch (r)
                    {
                        case 1:
                            //success
                            procedure = "UNI_CheckType";
                            command = new SqlCommand(procedure, conn);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                            returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                            pIDParam = new SqlParameter("@pid", System.Data.SqlDbType.Int);
                            pIDParam.Value = Int32.Parse(pID.Text);
                            command.Parameters.Add(pIDParam);

                            command.ExecuteNonQuery();
                            int s = Convert.ToInt32(returnParameter.Value);

                            switch (s)
                            {
                                case 0:
                                    this.identity = 0;
                                    break;
                                case 1:
                                    this.identity = 1;
                                    break;
                                case 2:
                                    this.identity = 2;
                                    break;
                            }

                            Console.WriteLine(this.identity);

                            this.Hide();
                            if (c == 0)
                            {
                                MainWindow mw = new MainWindow(this.username.Text, this.identity, Convert.ToInt32(pID.Text));
                                mw.results.Text = "Login Successful! Welcome.";
                                mw.Show();
                            }
                            else if (c == 1)
                            {
                                EmailSystem es = new EmailSystem(this.username.Text, this.identity, Convert.ToInt32(pID.Text));
                                //es.results.Text = "Login Successful! Welcome.";
                                es.Show();
                            }
                            break;
                        case 2:
                            //invalid password
                            result.Text = "You have entered an incorrect username/password";
                            break;
                        case 0:
                            //invalid username
                            result.Text = "You have entered an incorrect username/password";
                            break;
                        case 3:
                            //invalid pID
                            result.Text = "You have entered an incorrect pID";
                            break;
                    }

                    conn.Close();
                }
            }
            catch (SqlException se)
            {

            }
            catch (FormatException)
            {
                result.Text = "You have entered values in an incorrect format.";
            }
        }

        private void signUp(object sender, RoutedEventArgs e)
        {
            type.Visibility = System.Windows.Visibility.Visible;
            typeLabel.Visibility = System.Windows.Visibility.Visible;

            if (username.Text.Length >= 1 && pID.Text.Length >= 1)
            {

                if (password.Text.Length >= 5)
                {
                    int r;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        String procedure = "UNI_CreateProfile";
                        SqlCommand command = new SqlCommand(procedure, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                        returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                        SqlParameter pIDParam = new SqlParameter("@pID", System.Data.SqlDbType.Int);
                        pIDParam.Value = Int32.Parse(pID.Text);
                        command.Parameters.Add(pIDParam);

                        SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                        usernameParam.Value = (username.Text);
                        command.Parameters.Add(usernameParam);

                        SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar);
                        passwordParam.Value = (password.Text);
                        command.Parameters.Add(passwordParam);

                        SqlParameter typeParam = new SqlParameter("@type", System.Data.SqlDbType.VarChar);
                        typeParam.Value = (type.Text);
                        command.Parameters.Add(typeParam);

                        command.ExecuteNonQuery();
                        r = Convert.ToInt32(returnParameter.Value);

                        switch (r)
                        {
                            case 1:
                                result.Text = "Username has already been taken, try again.";
                                break;
                            case 3:
                                result.Text = "Invalid pID, unable to link with existing account. Try again";
                                break;
                            case 5:
                                result.Text = "You have been added sucessfully. Click Sign In to access the Main Window.";
                                break;
                        }
                        conn.Close();
                    }
                }
                else
                {
                    result.Text = "Invalid password, too short. Try again.";
                }
            }
        }

        private void goToEmail(object sender, RoutedEventArgs e)
        {
            type.Visibility = System.Windows.Visibility.Hidden;
            typeLabel.Visibility = System.Windows.Visibility.Hidden;

            if (username.Text.Length >= 1 && password.Text.Length >= 1 && pID.Text.Length >= 1)
            {
                c = 1;
                tryLogin();
            }
        }
    }
}