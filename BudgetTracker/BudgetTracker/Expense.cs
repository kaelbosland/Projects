using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace BudgetTracker
{
    class Expense : ConnectionString
    {
        public float amount { get; set; }
        public string description { get; set; }
        public string category { get; set; }

        public bool addToDatabase()
        {
            using (SqlConnection conn = new SqlConnection(Get()))
            {

            }
            return false;
        }
    }
}
