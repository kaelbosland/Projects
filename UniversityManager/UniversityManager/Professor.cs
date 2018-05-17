using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UniversityManager
{
    class Professor : Person
    {
        public int pID { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String department { get; set; }
        public String classOne { get; set; }
        public String classTwo { get; set; }

        public Professor(int pID, String first, String last, String dept, String one, String two) : base(pID, first, last)
        {
            this.pID = pID;
            this.firstName = first;
            this.lastName = last;
            this.department = dept;
            this.classOne = one.ToUpper();
            this.classTwo = two.ToUpper();
        }

        public override string addToDatabase()
        {
            int result = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("UNI_InsertProf", conn);
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

                    SqlParameter year = new SqlParameter("@department", System.Data.SqlDbType.VarChar);
                    year.Value = this.department;
                    command.Parameters.Add(year);

                    SqlParameter classOneP = new SqlParameter("@classOne", System.Data.SqlDbType.VarChar);
                    if (this.classTwo.Equals("")) { classOneP.Value = DBNull.Value; } else { classOneP.Value = this.classTwo; }
                    command.Parameters.Add(classOneP);

                    SqlParameter classTwoP = new SqlParameter("@classTwo", System.Data.SqlDbType.VarChar);
                    if (this.classTwo.Equals("")) { classTwoP.Value = DBNull.Value; } else { classTwoP.Value = this.classTwo; }
                    command.Parameters.Add(classTwoP);


                    command.ExecuteNonQuery();
                    result = Convert.ToInt32(returnParameter.Value);
                    conn.Close();

                    switch (result)
                    {
                        case 1:
                            return "Error Code: " + result.ToString() + ". You have entered an invalid department name or course code. Please" +
                                "check your information and try again.";
                        case 2:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classOne + " already has a professor!";
                        case 3:
                            return "Error Code: " + result.ToString() + ". The course: " + this.classTwo + " already has a professor!";
                        case 4:
                            return "Error Code: " + result.ToString() + ". You cannot assign youreslf to teach the same class twice!";
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

            return "The professor was added succesfully!";
        }
    }
}