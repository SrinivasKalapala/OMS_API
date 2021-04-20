using System.Data;
using System.Data.SqlClient;

namespace OMS.DataAccess
{
    public class CommonDataAccess : DBUtil
    {
        SqlConnection con;
        SqlCommand cmd;

        public string AuthenticateUser(string UserName, string Password)
        {
            con = CreateConnection();
            string result = "";
            cmd = new SqlCommand("USP_CheckWEBApiActiveUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);
            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                result = row["UserName"].ToString();
            }

            CloseConnection();
            return result;
        }

        public void SaveExceptions(string AbsolutePath, string error)
        {
            cmd = new SqlCommand("USP_InserWebApiErrors", CreateConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AbsolutePath", AbsolutePath);
            cmd.Parameters.AddWithValue("@Error", error);
            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            CloseConnection();
        }

        public void SaveLogs(string requestInfo, string message, string type)
        {
            cmd = new SqlCommand("USP_SaveWebAPILogs", CreateConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@requestInfo", requestInfo);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@type", type);
            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            CloseConnection();
        }
    }
}
