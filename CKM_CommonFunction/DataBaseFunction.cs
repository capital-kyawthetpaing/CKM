﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace CKM_CommonFunction
{
    public class DataBaseFunction
    {
        public string GetConnectionString(string DatabaseServer, string DatabaseName, string DatabaseLoginID, string DatabasePassword, string TimeOut)
        {

            return "Data Source=" + DatabaseServer +
                   ";Initial Catalog=" + DatabaseName +
                   ";Persist Security Info=True;User ID=" + DatabaseLoginID +
                   ";Password=" + DatabasePassword +
                   ";Connection Timeout=" + TimeOut;

        }

        public DataTable SelectDatatable(string conStr, string sSQL, params SqlParameter[] para)
        {
            try
            {
                DataTable dt = new DataTable();
                var newCon = new SqlConnection(conStr);
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
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}
