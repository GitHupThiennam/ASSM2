using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace QuanLyThietBi.DB
{
    public class ClassDatabase
    {
        string databasename = CauHinh.Default.Database;
        public ClassDatabase()
        {
        }
        public ClassDatabase(string dbname)
        {
            databasename = dbname;
        }
        protected SqlConnection Connection
        {
            get;
            set;
        }
        protected string ConnectionString()
        {
            return "Server=" + CauHinh.Default.Server + ";User ID=" + CauHinh.Default.UId + "; Database=" + databasename + "; Password=" + CauHinh.Default.Pwd + ""; 
        }
        /// <summary>
        /// ham nay de lay du lieu
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable Select(String sql,ArrayList parameters)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(sql, ConnectionString());
            foreach (SqlParameter para in parameters)
            {
                adap.SelectCommand.Parameters.Add(para);
            }
            adap.Fill(table);
            if (adap.SelectCommand.Connection.State != ConnectionState.Closed) adap.SelectCommand.Connection.Close();
            return table;
        }
        /// <summary>
        /// Ham nay cho cau lenh insert, update hoac delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public void DoCommand(string sql, ArrayList parameters)
        {
            SqlCommand command = new SqlCommand(sql, new SqlConnection(ConnectionString()));
            command.CommandType = CommandType.Text;
            foreach (SqlParameter para in parameters)
            {
                command.Parameters.Add(para);
            }
            if (command.Connection.State != ConnectionState.Open) command.Connection.Open();
            command.ExecuteNonQuery();
            if (command.Connection.State != ConnectionState.Closed) command.Connection.Close();
        }
    }
}
