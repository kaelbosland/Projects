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


namespace UniversityManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        int c = -1;
        int pIDCount = 1;
        public int accessLevel = -1;
        private int pID;
        private string username;

        public const int STUDENT_ACCESS = 0;
        public const int PROF_ACCESS = 1;
        public const int ADMIN_ACCESS = 2;


        public MainWindow(string username, int access, int p)
        {
            this.accessLevel = access;
            this.pID = p;
            this.username = username;
            InitializeComponent();
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice, choice1 };
            setAllToSomething(studentQs, System.Windows.Visibility.Hidden);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String procedure = "UNI_GetCurrentPID";
                SqlCommand command = new SqlCommand(procedure, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                command.ExecuteNonQuery();
                pIDCount = Convert.ToInt32(returnParameter.Value);

                conn.Close();
            }

            // Console.WriteLine(accessLevel);
            switch (accessLevel)
            {
                case STUDENT_ACCESS:
                    QuitButton.IsEnabled = false;
                    addPersonButton.IsEnabled = false;
                    SignUpProfButton.IsEnabled = false;
                    classForm.Visibility = Visibility.Visible;

                    break;
                case PROF_ACCESS:
                    dropButton.IsEnabled = false;
                    enrollButton.IsEnabled = false;
                    addPersonButton.IsEnabled = false;
                    break;
            }
        }

        private void resetTables()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                String procedure = "UNI_ResetTables";
                SqlCommand command = new SqlCommand(procedure, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow lw = new LoginWindow();
            lw.Show();
        }

        private void switchForms(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OutputWindow w = new OutputWindow(this.username, accessLevel, pID);
            w.Show();
        }

        private void setAllToSomething(TextBox[] boxes, System.Windows.Visibility a)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Visibility = a;
            }
        }

        private void reset(TextBox[] boxes)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Visibility = System.Windows.Visibility.Hidden;
                boxes[i].Text = "";
            }

            submitButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void sendAddQuery()
        {
            c = (choice.Text.ToLower().Equals("student")) ? 0 : 1;
            Person person = new Person(pIDCount, text2.Text, text3.Text);
            results.Text = person.addToDatabase();

            if (c == 0)
            {
                Student student = new Student(pIDCount, text2.Text, text3.Text, Int32.Parse(text4.Text), text5.Text, text6.Text, text7.Text, text8.Text, text9.Text);
                results.Text = student.addToDatabase();
            }
            else if (c == 1)
            {
                Professor prof = new Professor(pIDCount, text2.Text, text3.Text, text4.Text, text5.Text, text6.Text);
                results.Text = prof.addToDatabase();
            }
        }

        private void Enroll(object sender, RoutedEventArgs e)
        {
            if (c >= 2 && c <= 5)
            {
                submitButton.Visibility = System.Windows.Visibility.Visible;
            }
        }



        private void dropClass(object sender, RoutedEventArgs e)
        {
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice };
            reset(studentQs);

            submitButton.Visibility = System.Windows.Visibility.Hidden;
            TextBox[] choiceQs = { choice, choice1, q1, text1 };
            setAllToSomething(choiceQs, System.Windows.Visibility.Visible);
            TextBox[] choiceQs2 = { q2, q3, q4, q5, q6, q7, q8, q9, text2, text3, text4, text5, text6, text7, text8, text9 };
            setAllToSomething(choiceQs2, System.Windows.Visibility.Hidden);
            results.Text = "";
            choice1.Text = "*pID:";
            q1.Text = "*Course Code:";
            text1.IsEnabled = true;
            c = 4;
            if (pID != 0)
            {
                choice.Text = pID.ToString();
                choice.IsEnabled = false;
            }
        }

        private void quitTeaching(object sender, RoutedEventArgs e)
        {
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice };
            reset(studentQs);
            if (pID != 0)
            {
                choice.Text = pID.ToString();
                choice.IsEnabled = false;
            }

            submitButton.Visibility = System.Windows.Visibility.Hidden;
            //setting up the UI for the professor sign up screen
            TextBox[] choiceQs = { choice, choice1, q1, text1 };
            setAllToSomething(choiceQs, System.Windows.Visibility.Visible);
            TextBox[] choiceQs2 = { q2, q3, q4, q5, q6, q7, q8, q9, text2, text3, text4, text5, text6, text7, text8, text9 };
            setAllToSomething(choiceQs2, System.Windows.Visibility.Hidden);
            results.Text = "";
            choice1.Text = "*pID:";
            q1.Text = "*Course Code:";
            text1.IsEnabled = true;
            c = 5;
        }

        private void enrollStudent(object sender, RoutedEventArgs e)
        {
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice };
            reset(studentQs);
            if (pID != 0)
            {
                choice.Text = pID.ToString();
                choice.IsEnabled = false;
            }
            submitButton.Visibility = System.Windows.Visibility.Hidden;
            TextBox[] choiceQs = { choice, choice1, q1, text1 };
            setAllToSomething(choiceQs, System.Windows.Visibility.Visible);
            TextBox[] choiceQs2 = { q2, q3, q4, q5, q6, q7, q8, q9, text2, text3, text4, text5, text6, text7, text8, text9 };
            setAllToSomething(choiceQs2, System.Windows.Visibility.Hidden);
            results.Text = "";
            choice1.Text = "*pID:";
            q1.Text = "*Course Code:";
            text1.IsEnabled = true;
            c = 2;
        }

        private void signUpProf(object sender, RoutedEventArgs e)
        {

            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice };
            reset(studentQs);
            if (pID != 0)
            {
                choice.Text = pID.ToString();
                choice.IsEnabled = false;
            }
            submitButton.Visibility = System.Windows.Visibility.Hidden;
            //setting up the UI for the professor sign up screen
            TextBox[] choiceQs = { choice, choice1, q1, text1 };
            setAllToSomething(choiceQs, System.Windows.Visibility.Visible);
            TextBox[] choiceQs2 = { q2, q3, q4, q5, q6, q7, q8, q9, text2, text3, text4, text5, text6, text7, text8, text9 };
            setAllToSomething(choiceQs2, System.Windows.Visibility.Hidden);
            results.Text = "";
            choice1.Text = "*pID:";
            q1.Text = "*Course Code:";
            text1.IsEnabled = true;
            c = 3;
        }

        private void sendQuery()
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    String procedure = "";
                    switch (c)
                    {
                        case 2:
                            procedure = "UNI_EnrollStudentInClass";
                            break;
                        case 3:
                            procedure = "UNI_MakeProfTeachClass";
                            break;
                        case 4:
                            procedure = "UNI_DropClass";
                            break;
                        case 5:
                            procedure = "UNI_QuitTeachingClass";
                            break;
                    }

                    SqlCommand command = new SqlCommand(procedure, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter pID = new SqlParameter("@pID", System.Data.SqlDbType.Int);
                    int pIDVal = 0;
                    Int32.TryParse(choice.Text, out pIDVal);
                    pID.Value = pIDVal;
                    command.Parameters.Add(pID);

                    SqlParameter coursecode = new SqlParameter("@coursecode", System.Data.SqlDbType.VarChar);
                    coursecode.Value = text1.Text.ToUpper();
                    command.Parameters.Add(coursecode);

                    var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    command.ExecuteNonQuery();
                    result = Convert.ToInt32(returnParameter.Value);
                    conn.Close();

                    if (result == 5)
                    {
                        results.Text = "The query was completed sucessfully. Thanks!";
                    }
                    else
                    {
                        switch (result)
                        {

                            case 0:
                                results.Text = (c == 3) ? results.Text = "Error Code: " + result.ToString() + ". This professor is already teaching two classes, he/she cannot teach another."
                                : "Error Code: " + result.ToString() + ". You have provided an invalid pID, this pID does not belong to a professor.";
                                break;
                            case 1:
                                if (c == 2)
                                    results.Text = "Error Code: " + result.ToString() + ". You have no open spots to enroll in a class. Must drop one first!";
                                else if (c == 3)
                                    results.Text = "Error Code: " + result.ToString() + ". This class already has a professor, cannot assign prof to this class.";
                                else if (c == 4)
                                    results.Text = "Error Code: " + result.ToString() + ". This student is not even enrolled in this class!";
                                else if (c == 5)
                                    results.Text = "Error Code: " + result.ToString() + ". You have entered an invalid courseCode, please check your information and try again.";
                                break;
                            case 2:
                                if (c == 2)
                                    results.Text = "Error Code: " + result.ToString() + ". The course: " + text1.Text + " is full. You cannot enroll yet.";
                                else if (c == 3)
                                    results.Text = "Error Code: " + result.ToString() + ". You have provided an invalid course code. Please check" +
                                        " your information and try again.";
                                else if (c == 4)
                                    results.Text = "Error Code: " + result.ToString() + ". You have provided an invalid course code. Please check" +
                                        " your information and try again.";
                                else if (c == 5)
                                    results.Text = "Error Code: " + result.ToString() + ". This professor is not teaching the class provided, impossible to drop it!";
                                break;
                            case 3:
                                results.Text = "Error Code: " + result.ToString() + ". You have entered an invalid course code. Please check" +
                                    " your information and try again.";
                                break;
                            case 6:
                                results.Text = "Error Code: " + result.ToString() + ". The student is already enrolled in this class!";
                                break;
                        }
                    }
                }
            }
            catch (FormatException fe)
            {
                results.Text = "There seems to have been an error processing your request. You entered a value in the incorrect format (eg. Number where a " +
                "String was expected), or you left a required field blank. Please try again.";
            }
            catch (SqlException se)
            {
                results.Text = "Error Code: " + result.ToString() + ". Unknown Error.";
            }
        }

        private void addPerson(object sender, RoutedEventArgs e)
        {
            TextBox[] choiceQs = { choice, choice1 };
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice };
            reset(studentQs);
            setAllToSomething(choiceQs, System.Windows.Visibility.Visible);
            results.Text = "";
            choice1.Text = "Student or Professor?";
            text1.IsEnabled = false;
        }

        private void StudentOrProfessor(object sender, TextChangedEventArgs e)
        {
            choice1.IsEnabled = false;

            if (choice.Text.ToLower().Equals("student"))
            {
                text1.Text = pIDCount.ToString();
                c = 0;
                TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice, choice1 };
                setAllToSomething(studentQs, System.Windows.Visibility.Visible);
                q1.Text = "*pID:";
                q2.Text = "*firstName:";
                q3.Text = "*lastName:";
                q4.Text = "*year:";
                q5.Text = "*major:";
                q6.Text = "classOne:";
                q7.Text = "classTwo:";
                q8.Text = "classThree:";
                q9.Text = "classFour:";
            }
            else if (choice.Text.ToLower().Equals("professor"))
            {
                text1.Text = pIDCount.ToString();
                c = 1;
                TextBox[] profQs = { q1, q2, q3, q4, q5, q6, text1, text2, text3, text4, text5, text6, choice, choice1 };
                setAllToSomething(profQs, System.Windows.Visibility.Visible);
                q1.Text = "*pID";
                q2.Text = "*firstName";
                q3.Text = "*lastName";
                q4.Text = "*department";
                q5.Text = "classOne";
                q6.Text = "classTwo";
                q7.Visibility = System.Windows.Visibility.Hidden;
                q8.Visibility = System.Windows.Visibility.Hidden;
                q9.Visibility = System.Windows.Visibility.Hidden;
                text7.Visibility = System.Windows.Visibility.Hidden;
                text8.Visibility = System.Windows.Visibility.Hidden;
                text9.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            TextBox[] studentQs = { q1, q2, q3, q4, q5, q6, q7, q8, q9, text1, text2, text3, text4, text5, text6, text7, text8, text9, choice, choice1 };

            switch (c)
            {
                case 0:
                    sendAddQuery();
                    text1.IsEnabled = true;
                    break;
                case 1:
                    sendAddQuery();
                    text1.IsEnabled = true;
                    break;
                case 2:
                    sendQuery();
                    break;
                case 3:
                    sendQuery();
                    break;
                case 4:
                    sendQuery();
                    break;
                case 5:
                    sendQuery();
                    break;
            }

            reset(studentQs);

        }
        private void completedFormProf(object sender, RoutedEventArgs e)
        {
            if (c == 1)
            {
                submitButton.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void completedFormStudent(object sender, RoutedEventArgs e)
        {
            submitButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void showClassPage(object sender, RoutedEventArgs e)
        {
            ClassPage cp = new ClassPage(pID);
            cp.Show();
            this.Hide();
        }

        private void switchEmail(object sender, RoutedEventArgs e)
        {
            EmailSystem es = new EmailSystem(this.username, this.accessLevel, this.pID);
            es.Show();
            this.Hide();
        }
    }
}