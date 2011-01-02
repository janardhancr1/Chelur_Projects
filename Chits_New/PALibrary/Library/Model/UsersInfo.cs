using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Model
{
    public class UsersInfo
    {
        public const string PARAM_USER_ID = DBConstant.DB_PARAM + "User_ID";
        public const string PARAM_USER_NAME = DBConstant.DB_PARAM + "User_Name";
        public const string PARAM_PASSWORD = DBConstant.DB_PARAM + "Password";
        public const string PARAM_NEW_PASSWORD = DBConstant.DB_PARAM + "NewPassword";

        public const string TABLE_NAME = "users";

        public const string QUERY_USER_LOGIN = "SELECT Count(*) FROM " + TABLE_NAME + " WHERE User_Name=" + PARAM_USER_NAME + " AND Password=md5(" + PARAM_PASSWORD + ")";
        public const string QUERY_CHANGE_PASSWORD = "UPDATE " + TABLE_NAME + " SET Password=MD5(" + PARAM_NEW_PASSWORD + ") WHERE User_Name=" + PARAM_USER_NAME;

        private int userID;
        private string userName;
        private string password;

        private string newPassword;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; }
        }

        public List<IDbDataParameter> GetParameters()
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();

            parameters.Add(DBManager.GetParameter(PARAM_USER_ID, userID));
            parameters.Add(DBManager.GetParameter(PARAM_USER_NAME, userName));
            parameters.Add(DBManager.GetParameter(PARAM_PASSWORD, password));

            return parameters;
        }
        public void ReadValues(IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "User_ID":
                        userID = DBUtils.ConvertInt(reader["User_ID"]);
                        break;
                    case "User_Name":
                        userName = DBUtils.ConvertString(reader["User_Name"]);
                        break;
                    case "Password":
                        password = DBUtils.ConvertString(reader["Password"]);
                        break;

                }
            }
        }
    }
}