using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Deliveries.App_Code;


namespace Deliveries.App_Code
{
    public class DAL
    {
        readonly string CONEECTION_STRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Projects\\Deliveries\\Deliveries\\App_Data\\deliveries1.accdb;Persist Security Info=False;";
        OleDbConnection myConnection;
        const string FILE_NAME = "deliveries1.accdb";
        private SqlCommand command = new SqlCommand();
        
        public DAL()
        {
            
            
            if (HttpContext.Current != null)
            {

                string location = HttpContext.Current.Server.MapPath("~/App_Data/" + FILE_NAME);
                CONEECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0; data source=" + location;
            }
        myConnection = new OleDbConnection(CONEECTION_STRING);
        }

        //פעולה המפעילה את השאילתא שנשלחת על בסיס הנתונים
        public DataSet excuteQuery(String sql)
        {
            DataSet dataset = new DataSet();
            try
            {
                myConnection.Open();
                string sSql = sql;
                OleDbCommand myCmd = new OleDbCommand(sSql, myConnection);
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = myCmd;
                adapter.Fill(dataset);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                myConnection.Close();
            }
            return dataset;
        }

    }
}