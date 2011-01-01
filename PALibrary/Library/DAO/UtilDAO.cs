using System;
using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.DAO.Helper;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.DAO
{
    public class UtilDAO
    {
        public static string GetNextSerial(string tableName, string fieldName)
        {
            string query = "SELECT " + fieldName + " FROM " + tableName + " ORDER BY CAST(SUBSTRING(" + fieldName + ", 3, LENGTH(" + fieldName + ")-2) AS UNSIGNED) DESC LIMIT 0,1";
            object result = SQLHelper.ExecuteScalar(CommandType.Text, query, null);
            string serialNo = DBUtils.ConvertString(result);
            return serialNo;
        }
    }
}
