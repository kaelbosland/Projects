using System;
using System.Data;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OutputWindow : MetroWindow
    {

        private String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog = KB_Database; Integrated Security = True";
        int c = -1;
        List<Student> students = new List<Student>();
        List<Professor> profs = new List<Professor>();

        private static int accessLevel;
        private static int pid;

        String table;
        List<String> columns = new List<string>();

        public OutputWindow(int access, int p)
        {
            InitializeComponent();
            accessLevel = access;
            pid = p;
            if (accessLevel == 0) //student
            {
                student.IsEnabled = true;
                classes.IsEnabled = true;
                dept.IsEnabled = true;
                prog.IsEnabled = true;
            }
            else if (accessLevel == 1) //professor
            {
                prof.IsEnabled = true;
                classes.IsEnabled = true;
                dept.IsEnabled = true;
                prog.IsEnabled = true;
            }
            else
            {
                person.IsEnabled = true;
                student.IsEnabled = true;
                prof.IsEnabled = true;
                classes.IsEnabled = true;
                dept.IsEnabled = true;
                prog.IsEnabled = true;
            }
        }

        private void switchForms(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow(accessLevel, pid);
            mw.ShowDialog();
        }

        private void clickedViewProfile(int i, int pID, Window w)
        {
            if (i == 0)
            {
                int j = 0;
                for (j = 0; j < students.Count; j++)
                {
                    if (students[j].pID == pID)
                    {
                        break;
                    }
                }

                Profile p = new Profile(i, pID, w, students[j]);
                p.Show();
            }
            else
            {
                int j = 0;
                for (j = 0; j < profs.Count; j++)
                {
                    if (profs[j].pID == pID)
                    {
                        break;
                    }
                }

                Profile p = new Profile(i, pID, w, profs[j]);
                p.Show();
            }
        }
        private void buildQuery(object sender, RoutedEventArgs e)
        {

            String c = "";
            for (int i = 0; i < columns.Count; i++)
            {
                if (i == columns.Count - 1)
                {
                    c += columns[i];
                }
                else
                {
                    c += columns[i] + ", ";
                }
            }

            String query = "SELECT * FROM " + table;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    MetroWindow w = new MetroWindow();
                    w.SizeToContent = SizeToContent.WidthAndHeight;
                    w.Title = "Output Window";
                    ListBox l = new ListBox();
                    Canvas titles = new Canvas { Height = 30, Width = 1200 };

                    Label label1 = new Label { Margin = new System.Windows.Thickness(5), FontWeight = FontWeights.ExtraBold };
                    Label label2 = new Label { Margin = new System.Windows.Thickness(5), FontWeight = FontWeights.ExtraBold };
                    Label label3 = new Label { Margin = new System.Windows.Thickness(5), FontWeight = FontWeights.ExtraBold };
                    Label label4 = new Label { Margin = new System.Windows.Thickness(5), FontWeight = FontWeights.ExtraBold };
                    Label label5 = new Label { Margin = new System.Windows.Thickness(5), FontWeight = FontWeights.ExtraBold };

                    int choice = -1;

                    if (table.Equals("UNI_Person") || table.Equals("UNI_Student") || table.Equals("UNI_Professor"))
                    {
                        label1.Content = "PID";
                        label2.Content = "FIRST NAME";
                        label3.Content = "LAST NAME";
                    }

                    if (table.Equals("UNI_Student"))
                    {
                        choice = 0;
                        label4.Content = "YEAR";
                        label5.Content = "MAJOR";
                    }
                    else if (table.Equals("UNI_Professor"))
                    {
                        choice = 1;
                        label4.Content = "DEPARTMENT";
                    }
                    else if (table.Equals("UNI_Class"))
                    {
                        label1.Content = "COURSE CODE";
                        label2.Content = "CLASS NAME";
                        label3.Content = "NUMBER ENROLLED";
                        label4.Content = "MAX CAPACITY TO ENROLL";
                        label5.Content = "PROFESSOR ID";
                    }
                    else if (table.Equals("UNI_Program"))
                    {
                        label1.Content = "PROGRAM NAME";
                        label2.Content = "DEPARTMENT";
                        label3.Content = "PROGRAM LENGTH";
                    }
                    else if (table.Equals("UNI_Department"))
                    {
                        label1.Content = "DEPARTMENT NAME";
                    }

                    Canvas.SetLeft(label1, 0);
                    Canvas.SetLeft(label2, 200);
                    Canvas.SetLeft(label3, 400);
                    Canvas.SetLeft(label4, 600);
                    Canvas.SetLeft(label5, 800);

                    titles.Children.Add(label1);
                    titles.Children.Add(label2);
                    titles.Children.Add(label3);
                    titles.Children.Add(label4);
                    titles.Children.Add(label5);

                    l.Items.Add(titles);

                    while (reader.Read())
                    {
                        label1 = new Label { Margin = new System.Windows.Thickness(5) };
                        label2 = new Label { Margin = new System.Windows.Thickness(5) };
                        label3 = new Label { Margin = new System.Windows.Thickness(5) };
                        label4 = new Label { Margin = new System.Windows.Thickness(5) };
                        label5 = new Label { Margin = new System.Windows.Thickness(5) };

                        Canvas data = new Canvas { Height = 30, Width = 1200 }; ;

                        Button view = new Button();
                        view.Content = "See Profile...";
                        int pID = -1;

                        if (table.Equals("UNI_Person") || table.Equals("UNI_Professor") || table.Equals("UNI_Student"))
                        {
                            label1.Content = (c.Contains("pID")) ? Convert.ToString(reader["pID"]) : "-";
                            label2.Content = (c.Contains("firstName")) ? (String)(reader["firstName"]) : "-";
                            label3.Content = (c.Contains("lastName")) ? (String)(reader["lastname"]) : "-";
                            pID = Convert.ToInt32(reader["pID"]);
                        }

                        if (table.Equals("UNI_Student"))
                        {
                            String classOne = (reader["classOne"] != DBNull.Value) ? (String)(reader["classOne"]) : "-";
                            String classTwo = (reader["classTwo"] != DBNull.Value) ? (String)(reader["classTwo"]) : "-";
                            String classThree = (reader["classThree"] != DBNull.Value) ? (String)(reader["classThree"]) : "-";
                            String classFour = (reader["classFour"] != DBNull.Value) ? (String)(reader["classFour"]) : "-";

                            students.Add(new Student((int)reader["pID"],
                                (String)(reader["firstName"]),
                                (String)(reader["lastname"]),
                                (int)(reader["year"]),
                                (String)(reader["major"]),
                                classOne, classTwo, classThree, classFour));
                            Console.WriteLine((int)reader["pID"]);


                            label4.Content = (c.Contains("department")) ? (String)(reader["department"]) : "-";
                            view.Click += (s, e1) => { clickedViewProfile(choice, pID, w); };

                            label4.Content = (c.Contains("year")) ? Convert.ToString((reader["year"])) : "-";
                            label5.Content = (c.Contains("major")) ? (String)(reader["major"]) : "-";

                        }
                        else if (table.Equals("UNI_Professor"))
                        {

                            String classOne = (reader["classOne"] != DBNull.Value) ? (String)(reader["classOne"]) : "-";
                            String classTwo = (reader["classTwo"] != DBNull.Value) ? (String)(reader["classTwo"]) : "-";

                            profs.Add(new Professor((int)(reader["pID"]),
                                (String)(reader["firstName"]),
                                (String)(reader["lastname"]),
                                (String)(reader["department"]),
                                classOne, classTwo));

                            label4.Content = (c.Contains("department")) ? (String)(reader["department"]) : "-";
                            view.Click += (s, e1) => { clickedViewProfile(choice, pID, w); };
                        }

                        if (table.Equals("UNI_Class"))
                        {
                            label2.Content = (c.Contains("className")) ? (String)(reader["className"]) : "-";
                            label1.Content = (c.Contains("courseCode")) ? (String)(reader["courseCode"]) : "-";
                            label3.Content = (c.Contains("numEnrolled")) ? Convert.ToString((reader["numEnrolled"])) : "-";
                            label4.Content = (c.Contains("maxEnroll")) ? Convert.ToString((reader["maxEnroll"])) : "-";
                            label5.Content = (c.Contains("profID")) ? Convert.ToString((reader["profID"])) : "-";
                        }
                        else if (table.Equals("UNI_Program"))
                        {
                            label1.Content = (c.Contains("programName")) ? (String)(reader["programName"]) : "-";
                            label2.Content = (c.Contains("department")) ? (String)(reader["department"]) :
                            label3.Content = (c.Contains("programLength")) ? Convert.ToString(reader["programLength"]) : "-";
                        }
                        else if (table.Equals("UNI_Department"))
                        {
                            label1.Content = (c.Contains("departmentName")) ? (String)(reader["departmentName"]) : "-";
                        }

                        Canvas.SetLeft(label1, 0);
                        Canvas.SetLeft(label2, 200);
                        Canvas.SetLeft(label3, 400);
                        Canvas.SetLeft(label4, 600);
                        Canvas.SetLeft(label5, 800);

                        data.Children.Add(label1);
                        data.Children.Add(label2);
                        data.Children.Add(label3);
                        data.Children.Add(label4);
                        data.Children.Add(label5);
                        if (choice == 0 || choice == 1)
                        {
                            Canvas.SetLeft(view, 1000);
                            data.Children.Add(view);
                        }
                        l.Items.Add(data);
                    }

                    w.Content = l;
                    w.Show();

                    reader.Close();
                    conn.Close();
                }

            }
            catch (SqlException se)
            {
                results.Text = "An error has occurred. Please make sure you select a Table to display your query from, as well as" +
                    "at least one corresponding column.";
            }

            table = "";
            columns = new List<string>();
        }

        private void choseTable(object sender, RoutedEventArgs e)
        {
            table = "UNI_" + ((MenuItem)sender).Header;

            for (int i = 0; i < tableMenu.Items.Count; i++)
            {
                if (!((MenuItem)tableMenu.Items[i]).Header.Equals(((MenuItem)sender).Header))
                {
                    ((MenuItem)tableMenu.Items[i]).IsChecked = false;
                }
            }

            columnsMenu.Items.Clear();


            switch (((MenuItem)sender).Header)
            {
                case "Person":
                    var attributes = new List<String> { "pID", "firstName", "lastName" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
                case "Student":
                    attributes = new List<String> { "pID", "firstName", "lastName", "year", "major" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
                case "Professor":
                    attributes = new List<String> { "pID", "firstName", "lastName", "department" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
                case "Class":
                    attributes = new List<String> { "courseCode", "className", "numEnrolled", "maxEnroll", "professorID" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
                case "Department":
                    attributes = new List<String> { "departmentName" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
                case "Program":
                    attributes = new List<String> { "programName", "programName", "department" };
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        MenuItem a = new MenuItem { Header = attributes[i] };
                        a.Click += new RoutedEventHandler(choseColumns);
                        a.IsCheckable = true;
                        columnsMenu.Items.Add(a);
                    }
                    break;
            }

        }

        private void choseColumns(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).IsChecked)
            {
                columns.Add((string)((MenuItem)sender).Header);
            }
            else
            {
                columns.Remove((string)((MenuItem)sender).Header);
            }
        }
    }
}