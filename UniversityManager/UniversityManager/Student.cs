using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UniversityManager
{
    class Student : Person
    {
        public int pID { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public int year { get; set; }
        public String major { get; set; }
        public String classOne { get; set; }
        public String classTwo { get; set; }
        public String classThree { get; set; }
        public String classFour { get; set; }

        public Student(int pID, String first, String last, int y, String major, String one, String two, String three, String four) : base(pID, first, last)
        {
            this.pID = pID;
            this.firstName = first;
            this.lastName = last;
            this.year = y;
            this.major = major;
            this.classOne = one;
            this.classTwo = two;
            this.classThree = three;
            this.classFour = four;
        }

        public override string addToDatabase()
        {

            int result = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_InsertStudent", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var returnParameter = command.Parameters.Add("@ReturnVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter pIDP = new SqlParameter("@pID", System.Data.SqlDbType.Int);
                    pIDP.Value = this.pID;
                    command.Parameters.Add(pIDP);

                    SqlParameter firstNameP = new SqlParameter("@firstName", System.Data.SqlDbType.VarChar);
                    firstNameP.Value = this.firstName;
                    command.Parameters.Add(firstNameP);

                    SqlParameter lastNameP = new SqlParameter("@lastName", System.Data.SqlDbType.VarChar);
                    lastNameP.Value = this.lastName;
                    command.Parameters.Add(lastNameP);

                    SqlParameter year = new SqlParameter("@year", System.Data.SqlDbType.Int);
                    year.Value = this.year;
                    command.Parameters.Add(year);

                    SqlParameter major = new SqlParameter("@major", System.Data.SqlDbType.VarChar);
                    major.Value = this.major;
                    command.Parameters.Add(major);

                    SqlParameter classOneP = new SqlParameter("@classOne", System.Data.SqlDbType.VarChar);
                    if (this.classOne.Equals("")) { classOneP.Value = DBNull.Value; } else { classOneP.Value = this.classOne; }
                    command.Parameters.Add(classOneP);

                    SqlParameter classTwoP = new SqlParameter("@classTwo", System.Data.SqlDbType.VarChar);
                    if (this.classTwo.Equals("")) { classTwoP.Value = DBNull.Value; } else { classTwoP.Value = this.classTwo; }
                    command.Parameters.Add(classTwoP);

                    SqlParameter classThree = new SqlParameter("@classThree", System.Data.SqlDbType.VarChar);
                    if (this.classThree.Equals("")) { classThree.Value = DBNull.Value; } else { classThree.Value = this.classThree; }
                    command.Parameters.Add(classThree);

                    SqlParameter classFour = new SqlParameter("@classFour", System.Data.SqlDbType.VarChar);
                    if (this.classFour.Equals("")) { classFour.Value = DBNull.Value; } else { classFour.Value = this.classFour; }
                    command.Parameters.Add(classFour);

                    command.ExecuteNonQuery();
                    result = (int)(returnParameter.Value);
                    conn.Close();

                    switch (result)
                    {
                        case 0:
                            return "Error Code: " + result.ToString() + ". You have entered an invalid program name or course code. Please" +
                                "check your information and try again.";
                        case 1:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classOne + " is full. You cannot enroll at this " +
                                "time.";
                        case 2:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classTwo + " is full. You cannot enroll at this " +
                                "time.";
                        case 3:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classThree + " is full. You cannot enroll at this " +
                                "time.";
                        case 4:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classFour + " is full. You cannot enroll at this " +
                                "time.";
                        case 6:
                            return "Error Code: " + result.ToString() + ". Cannot enroll in the same class multiple times!";
                    }
                }

            }
            catch (SqlException se)
            {
                return se.Message;
            }
            catch (FormatException fe)
            {
                return "Data was provided in an incorrect format. Please check your information and try again.";
            }

            return "The student was added succesfully!";
        }
    }
}