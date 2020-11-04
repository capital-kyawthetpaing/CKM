using System;
using System.Data;
using System.Data.SqlClient;
using CKM_CommonFunction;

namespace CKM_DataLayer
{
    public class CKMDL
    {
        CommonFunction commonFunction;
        public CKMDL()
        {
            commonFunction = new CommonFunction();
        }

        public DataTable SelectDatatable(string StoreprocedureName, string ConnectionString, params SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            var newCon = new SqlConnection(ConnectionString);
            using (var adapt = new SqlDataAdapter(StoreprocedureName, newCon))
            {
                newCon.Open();
                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (para != null)
                {
                    para = ChangeToDBNull(para);
                    adapt.SelectCommand.Parameters.AddRange(para);
                }

                adapt.Fill(dt);
                newCon.Close();
            }
            return dt;
        }

        public string SelectJson(string sSQL, string ConStr, params SqlParameter[] para)
        {
            DataTable dt = new DataTable("data");
            var newCon = new SqlConnection(ConStr);
            using (var adapt = new SqlDataAdapter(sSQL, newCon))
            {
                newCon.Open();
                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (para != null)
                {
                    para = ChangeToDBNull(para);
                    adapt.SelectCommand.Parameters.AddRange(para);
                }

                adapt.Fill(dt);
                newCon.Close();
            }
            return commonFunction.DataTableToJSONWithJSONNet(dt);
        }

        private SqlParameter[] ChangeToDBNull(SqlParameter[] para)
        {
            foreach (var p in para)
            {
                if (p.Value == null || string.IsNullOrWhiteSpace(p.Value.ToString()))
                {
                    p.Value = DBNull.Value;
                    p.SqlValue = DBNull.Value;
                }
                else
                {
                    p.Value = p.Value.ToString().Trim();
                    p.SqlValue = p.Value.ToString().Trim();
                }
            }

            return para;
        }

        public string GetConnectionString(string DatabaseServer, string DatabaseName, string DatabaseLoginID, string DatabasePassword, string TimeOut)
        {

            return "Data Source=" + DatabaseServer +
                   ";Initial Catalog=" + DatabaseName +
                   ";Persist Security Info=True;User ID=" + DatabaseLoginID +
                   ";Password=" + DatabasePassword +
                   ";Connection Timeout=" + TimeOut;

        }

        public string InsertUpdateDeleteData(string sSQL,string conStr, params SqlParameter[] para)
        {
            try
            {
                var newCon = new SqlConnection(conStr);
                if(para != null)
                    para = ChangeToDBNull(para);
                SqlCommand cmd = new SqlCommand(sSQL, newCon)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddRange(para);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return "true";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
