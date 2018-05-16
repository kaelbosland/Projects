using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UniversityManager
{
    class Person
    {
        int pID { get; set; }
        String firstName { get; set; }
        String lastName { get; set; }
        protected String connectionString = "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog = KB_Database; Integrated Security = True";

        public Person()
        {

        }
        public Person(int pID, String first, String last)
        {
            this.pID = pID;
            this.firstName = first;
            this.lastName = last;
        }

        public virtual string addToDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //BEFORE WE CAN ADD A STUDENT/PROF TO THE DATABASE, WE HAVE TO FIRST ADD THIS PERSON TO THE DATABASE
                    String procedure = "UNI_InsertPerson";
                    SqlCommand addPerson = new SqlCommand(procedure, conn);
                    addPerson.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter pID2 = new SqlParameter("@pID", System.Data.SqlDbType.Int);
                    pID2.Value = this.pID;
                    addPerson.Parameters.Add(pID2);

                    SqlParameter firstName2 = new SqlParameter("@firstName", System.Data.SqlDbType.VarChar);
                    firstName2.Value = this.firstName;
                    addPerson.Parameters.Add(firstName2);

                    SqlParameter lastName2 = new SqlParameter("@lastName", System.Data.SqlDbType.VarChar);
                    lastName2.Value = this.lastName;
                    addPerson.Parameters.Add(lastName2);

                    addPerson.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException se)
            {
                return (se.Message);
            }

            return "The person was added successfully!";
        }
    }
}
