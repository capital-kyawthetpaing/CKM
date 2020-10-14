using System;
using System.Collections.Generic;
using System.IO;

namespace CKM_CommonFunction
{
    public class FileFunction
    {
        /// <summary>
        /// to get information of config file
        /// if file path not exists of some error occurs, dic count will return with zero
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="Key"></param>
        /// <returns>dictionary</returns>
        /// Keys - DatabaseServer,DatabaseName,DatabaseLoginID,DatabasePassword,DatabaseServer
        public Dictionary<string, string> ReadConfig(string filePath, string Title, string Key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (System.IO.File.Exists(filePath))
            {
                IniFile_DL idl = new IniFile_DL(filePath);
                string config;
                switch (Title)
                {
                    case "DataBase":
                        config = idl.IniReadValue(Title, Key);

                        dic.Add("DatabaseServer", config.Split(',')[0]);
                        dic.Add("DatabaseName", config.Split(',')[1]);
                        dic.Add("DatabaseLoginID", config.Split(',')[2]);
                        dic.Add("DatabasePassword", config.Split(',')[3]);
                        break;
                }

                return dic;
            }
            return dic;
        }

        /// <summary>
        /// Get Connection String from default file path(my documents)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConnectionWithDefaultPath(string key)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/DBConfig/DBConfig.ini";
            Dictionary<string, string> dic = ReadConfig(filePath, "DataBase", key);

            return "Data Source=" + dic["DatabaseServer"] +
                    ";Initial Catalog=" + dic["DatabaseName"] +
                    ";Persist Security Info=True;User ID=" + dic["DatabaseLoginID"] +
                    ";Password=" + dic["DatabasePassword"] +
                    ";";
        }
    }
}
