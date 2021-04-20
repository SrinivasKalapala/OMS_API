using System.Configuration;
using System.Data.SqlClient;

namespace OMS.DataAccess
{
    public class DBUtil
    {
        SqlConnection con;
        SqlCommand cmd;

        public SqlConnection CreateConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["OMS"].ConnectionString);
            con.Open();
            return con;
        }

        public void CloseConnection()
        {
            con.Close();
        }
    }
}
