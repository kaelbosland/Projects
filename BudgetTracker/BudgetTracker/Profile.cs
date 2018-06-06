using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BudgetTracker
{
    class Profile : ConnectionString
    {
        public string username { get; set; }
        public float startingAmount { get; set; }
        public int timelineInWeeks { get; set; }
        public string startDate { get; set; }

        public bool addToDatabase ()
        {
            using (SqlConnection conn = new SqlConnection(Get()))
            {
                Console.WriteLine(username);
            }
            return false;
        }
    }
}
