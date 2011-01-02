using System.Collections.Generic;
using System.Data;

using PALibrary.Library.DAO.Manager;
using PALibrary.Library.DAO.Helper;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;

namespace PALibrary.Library.DAO
{
    class UsersDAO
    {
        public static bool ValidateUser(UsersInfo usersInfo)
        {
            bool valid = false;
            try
            {
                long count = (long)SQLHelper.ExecuteScalar(CommandType.Text, UsersInfo.QUERY_USER_LOGIN, usersInfo.GetParameters());

                if (count > 0) valid = true;

                return valid;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static void ChangePassword(UsersInfo usersInfo)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(UsersInfo.PARAM_USER_NAME, usersInfo.UserName));
                parameters.Add(DBManager.GetParameter(UsersInfo.PARAM_NEW_PASSWORD, usersInfo.NewPassword));

                if (ValidateUser(usersInfo))
                {
                    IDbTransaction transaction = connection.BeginTransaction();

                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, UsersInfo.QUERY_CHANGE_PASSWORD, parameters);

                    transaction.Commit();
                    transaction.Dispose();
                }
                else
                {
                    throw new PAException("Password is incorrect.");
                }
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(connection);
            }
        }

    }
}