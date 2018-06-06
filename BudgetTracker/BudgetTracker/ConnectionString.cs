using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker
{
    class ConnectionString
    {
        public string Get() {
            return "Data Source=KAELS-LENOVO-YO\\KB_SQLSERVER;Initial Catalog=KB_Database;Integrated Security=True";
        }
    }
}
