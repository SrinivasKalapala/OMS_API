using OMS.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System;

namespace OMS.DataAccess
{
    public class CustomerDataAccess : DBUtil
    {
        public int CreateCustomer(Customer customer)
        {
            int cutomerID = 0;

            SqlCommand cmd = new SqlCommand("USP_CreateCustomer", CreateConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = customer.Email;
            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                cutomerID = Convert.ToInt32(row["cutomerID"] == DBNull.Value ? 0: row["cutomerID"]);
            }
            CloseConnection();
            return cutomerID;
        }
    }
}
