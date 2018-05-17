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
    /// Interaction logic for EmailSystem.xaml
    /// </summary>
    public partial class EmailSystem : MetroWindow
    {

        String user;
        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        int currentID;
        int pID;

        public EmailSystem(String username, int accessLevel, int pID)
        {
            this.user = username;
            this.pID = pID;
            InitializeComponent();
            from.Text = username;
            if (accessLevel == 1)
            {
                announcement.Visibility = Visibility.Visible;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("UNI_GetCurrentEmailID", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();
                currentID = (int)returnParameter.Value;

                conn.Close();
            }
        }

        private void changingTab(object sender, SelectionChangedEventArgs e)
        {

            from.Visibility = System.Windows.Visibility.Hidden;
            to.Visibility = System.Windows.Visibility.Hidden;
            subject.Visibility = System.Windows.Visibility.Hidden;
            msg.Visibility = System.Windows.Visibility.Hidden;
            back.Visibility = System.Windows.Visibility.Hidden;
            sendEmail.Visibility = System.Windows.Visibility.Hidden;

            if (tabControl.SelectedItem.Equals(tabControl.Items[0]))
            {
                //MY INBOX TAB
                ListBox myEmail = new ListBox();
                Canvas titles = new Canvas { Height = 50, Width = 1000 };

                Label id = new Label { Content = "Email ID", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label from = new Label { Content = "From", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label subject1 = new Label { Content = "Subject", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label status = new Label { Content = "Status", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };

                Button message;
                Button reply;

                Canvas.SetLeft(id, 0);
                Canvas.SetLeft(from, 100);
                Canvas.SetLeft(subject1, 250);
                Canvas.SetLeft(status, 450);

                titles.Children.Add(id);
                titles.Children.Add(from);
                titles.Children.Add(subject1);
                titles.Children.Add(status);

                myEmail.Items.Add(titles);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_CheckEmail", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter name = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                    name.Value = this.user;
                    command.Parameters.Add(name);
                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        Canvas email = new Canvas { Height = 50, Width = 1000 };

                        id = new Label { Content = (reader["emailID"]).ToString(), Height = 50, Width = 200 };
                        from = new Label { Content = (string)reader["sender"], Height = 50, Width = 200 };
                        subject1 = new Label { Content = (string)reader["subject"], Height = 50, Width = 200 };
                        status = new Label { Content = (string)reader["status"], Height = 50, Width = 200 };
                        message = new Button { Content = "See Full Message" };
                        reply = new Button { Content = "Reply to Email" };

                        String f = (string)reader["sender"];
                        String sub = (string)reader["subject"];
                        String m = (string)reader["message"];
                        String s = (string)reader["status"];
                        int eID = Convert.ToInt32(reader["emailID"]);

                        message.Click += (s2, e2) => { seeMessage(m, s, eID, this.user, true); };
                        reply.Click += (s2, e2) => { replyMessage(f, sub); };

                        Canvas.SetLeft(id, 0);
                        Canvas.SetLeft(from, 100);
                        Canvas.SetLeft(subject1, 250);
                        Canvas.SetLeft(status, 450);
                        Canvas.SetLeft(message, 650);
                        Canvas.SetLeft(reply, 800);

                        email.Children.Add(id);
                        email.Children.Add(from);
                        email.Children.Add(subject1);
                        email.Children.Add(status);
                        email.Children.Add(message);
                        email.Children.Add(reply);

                        myEmail.Items.Add(email);
                    }

                    conn.Close();
                }

                inbox.Content = myEmail;
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[1]))
            {
                //SEND AN EMAIL TAB
                from.Visibility = System.Windows.Visibility.Visible;
                to.Visibility = System.Windows.Visibility.Visible;
                subject.Visibility = System.Windows.Visibility.Visible;
                msg.Visibility = System.Windows.Visibility.Visible;
                sendEmail.Visibility = System.Windows.Visibility.Visible;
                subject.Text = "";
                to.Text = "";
                msg.Text = "";
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[2]))
            {
                //CONTACTS LIST TAB
                ListBox contactList = new ListBox();
                Canvas titles = new Canvas { Height = 50, Width = 800 };

                Label from = new Label { Content = "Username", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label subject1 = new Label { Content = "Type of User", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };

                Canvas.SetLeft(from, 0);
                Canvas.SetLeft(subject1, 200);

                titles.Children.Add(from);
                titles.Children.Add(subject1);

                contactList.Items.Add(titles);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_ShowContactList", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter username = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                    username.Value = this.user;
                    command.Parameters.Add(username);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Canvas email = new Canvas { Height = 50, Width = 800 };
                        String recipient = (string)reader["username"];

                        Label l = new Label { Content = recipient, Height = 50, Width = 200 };
                        Label t = new Label { Content = (string)reader["type"], Height = 50, Width = 200 };

                        Button button = new Button { Content = "Send Email" };
                        button.Click += (s, e2) => { forceSwitchTab(recipient); };

                        Canvas.SetLeft(l, 0);
                        Canvas.SetLeft(t, 200);
                        Canvas.SetLeft(button, 400);

                        email.Children.Add(l);
                        email.Children.Add(t);
                        email.Children.Add(button);

                        contactList.Items.Add(email);
                    }

                    conn.Close();
                }

                contacts.Content = contactList;
            }

            else if (tabControl.SelectedItem.Equals(tabControl.Items[3]))
            {
                //SENT MAIL FOLDER TAB
                ListBox sentMail = new ListBox();
                Canvas titles = new Canvas { Height = 50, Width = 1000 };

                Label id = new Label { Content = "Email ID", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label from = new Label { Content = "To", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label subject1 = new Label { Content = "Subject", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };
                Label status = new Label { Content = "Status", Height = 50, Width = 200, FontWeight = FontWeights.ExtraBold };

                Canvas.SetLeft(id, 0);
                Canvas.SetLeft(from, 100);
                Canvas.SetLeft(subject1, 250);
                Canvas.SetLeft(status, 450);

                titles.Children.Add(id);
                titles.Children.Add(from);
                titles.Children.Add(subject1);
                titles.Children.Add(status);

                sentMail.Items.Add(titles);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_CheckSentMail", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter name = new SqlParameter("@username", System.Data.SqlDbType.VarChar);
                    name.Value = this.user;
                    command.Parameters.Add(name);
                    SqlDataReader reader = command.ExecuteReader();

                    Button message;

                    while (reader.Read())
                    {
                        Canvas sent = new Canvas { Height = 50, Width = 1000 };

                        id = new Label { Content = (reader["emailID"]).ToString(), Height = 50, Width = 200 };
                        from = new Label { Content = (string)reader["reciever"], Height = 50, Width = 200 };
                        subject1 = new Label { Content = (string)reader["subject"], Height = 50, Width = 200 };
                        status = new Label { Content = (string)reader["status"], Height = 50, Width = 200 };
                        message = new Button { Content = "See Full Message" };

                        String f = (string)reader["reciever"];
                        String sub = (string)reader["subject"];
                        String m = (string)reader["message"];
                        String s = (string)reader["status"];
                        int eID = Convert.ToInt32(reader["emailID"]);

                        message.Click += (s2, e2) => { seeMessage(m, s, eID, "", false); };

                        Canvas.SetLeft(id, 0);
                        Canvas.SetLeft(from, 100);
                        Canvas.SetLeft(subject1, 250);
                        Canvas.SetLeft(status, 450);
                        Canvas.SetLeft(message, 650);

                        sent.Children.Add(id);
                        sent.Children.Add(from);
                        sent.Children.Add(subject1);
                        sent.Children.Add(status);
                        sent.Children.Add(message);

                        sentMail.Items.Add(sent);
                    }

                    conn.Close();
                }

                sentFolder.Content = sentMail;
            }
            else if (tabControl.SelectedItem.Equals(tabControl.Items[4]))
            {
                //LOG OUT TAB
                back.Visibility = System.Windows.Visibility.Visible;
                annoucementText.Text = "";
                courseCode.Text = "";
            }
        }
        private void forceSwitchTab(String s)
        {
            tabControl.SelectedItem = tabControl.Items[1];
            to.Text = s;
        }

        private void sendEmailQuery(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("UNI_SendEmail", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter id = new SqlParameter("@emailID", System.Data.SqlDbType.VarChar);
                id.Value = this.currentID;
                command.Parameters.Add(id);

                SqlParameter username = new SqlParameter("@sender", System.Data.SqlDbType.VarChar);
                username.Value = this.user;
                command.Parameters.Add(username);

                SqlParameter reciever = new SqlParameter("@reciever", System.Data.SqlDbType.VarChar);
                reciever.Value = to.Text;
                command.Parameters.Add(reciever);

                SqlParameter sub = new SqlParameter("@subject", System.Data.SqlDbType.VarChar);
                sub.Value = subject.Text;
                command.Parameters.Add(sub);

                SqlParameter message = new SqlParameter("@message", System.Data.SqlDbType.VarChar);
                message.Value = msg.Text;
                command.Parameters.Add(message);

                SqlParameter status = new SqlParameter("@status", System.Data.SqlDbType.VarChar);
                status.Value = "UNREAD";
                command.Parameters.Add(status);

                var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();
                int r = Convert.ToInt32(returnParameter.Value);

                switch (r)
                {
                    case 0:
                        msg.Text = "You have entered an invalid username for the recipient, and your email has bounced back. Please try again.";
                        break;
                    case 5:
                        msg.Text = "Your email was sent successfully!";
                        this.currentID++;
                        break;
                }

                conn.Close();
            }
        }

        private void seeMessage(String message, String status, int eID, String s, bool inInbox)
        {
            MetroWindow w = new MetroWindow { Height = 300, Width = 300 };
            TextBox t = new TextBox { TextWrapping = TextWrapping.Wrap };
            t.Text = message;
            w.Content = t;
            w.Show();

            if (inInbox)
            {

                if (status.Equals("UNREAD"))
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand("UNI_ReadEmail", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter id = new SqlParameter("@emailID", System.Data.SqlDbType.Int);
                        id.Value = eID;
                        command.Parameters.Add(id);

                        SqlParameter sender = new SqlParameter("@sender", System.Data.SqlDbType.VarChar);
                        sender.Value = s;
                        command.Parameters.Add(sender);

                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        private void replyMessage(String sender, String sub)
        {
            tabControl.SelectedItem = tabControl.Items[1];
            to.Text = sender;
            subject.Text = "RE: " + sub;
        }

        private void makeSubmitVisible(object sender, TextChangedEventArgs e)
        {
            if (msg.Text.Length <= 240 && to.Text.Length >= 1 && to.Text.Length <= 20 && subject.Text.Length <= 50)
            {
                sendEmail.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }

        private void sendAnnouncement(object sender, RoutedEventArgs e)
        {

            if (courseCode.Text.Length >= 5 && annoucementText.Text.Length >= 5)
            {
                int result = -1;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_MakeAnnouncement", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter("@courseCode", System.Data.SqlDbType.VarChar);
                    param1.Value = courseCode.Text;
                    command.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter("@announcement", System.Data.SqlDbType.VarChar);
                    param2.Value = annoucementText.Text;
                    command.Parameters.Add(param2);

                    SqlParameter param3 = new SqlParameter("@professorID", System.Data.SqlDbType.VarChar);
                    param3.Value = this.pID;
                    command.Parameters.Add(param3);

                    var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    result = (int)returnParameter.Value;
                    conn.Close();
                }

                switch (result)
                {
                    case 1:
                        annoucementText.Text = "The professor is not teaching the class specified!";
                        break;
                    case 5:
                        annoucementText.Text = "The announcement was sent successfully!";
                        break;
                }
            }
        }
    }
}