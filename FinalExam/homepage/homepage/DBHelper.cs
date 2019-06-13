using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace homepage
{
    class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            string debugpath = Application.StartupPath;
            string binpath = debugpath.Substring(0, debugpath.LastIndexOf("\\"));
            string path = binpath.Substring(0, binpath.LastIndexOf("\\"));
            string constr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+path+"\\Database1.mdf;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            return con;
        }
    }
}
