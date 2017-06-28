using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace Fish
{
    public abstract class SqlHelper
    {
        public static readonly string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Fish_sql"].ToString();
        #region 執行CMD操作防INJECTION，返回資料表  +static DataTable ExcuteTable(string sql)
        /// <summary>
        /// 傳入SQL語句，回傳DataTable物件
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable cmdTable(SqlCommand command)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            DataSet dataSet = new DataSet();
            command.Connection = conn;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            command.ExecuteNonQuery();
            sqlDataAdapter.Fill(dataSet);
            conn.Close();

            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
        }
        //end
        #endregion
        #region 執行CMD操作防INJECTION，返回數字 +static DataTable ExcuteTable(string sql)
        /// <summary>
        /// 傳入SQL語句，回傳int
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int cmdCheck(SqlCommand command)
        {
            int check_num = 0;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            command.Connection = conn;
            check_num = command.ExecuteNonQuery();
            conn.Close();
            return check_num;
        }
        //end
        #endregion
        #region 執行CMD操作防INJECTION，返回數字 +static DataTable ExecuteScalar(string sql) @@IDENTITY
        /// <summary>
        /// 傳入SQL語句，回傳int
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int Return_IDENTITY(SqlCommand command)
        {
            int primaryKey;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            command.Connection = conn;
            primaryKey = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            return primaryKey;
        }
        //end
        #endregion
    }
}