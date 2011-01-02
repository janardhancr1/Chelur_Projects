using System.Collections.Generic;
using System.Data;
using PALibrary.Library.DAO.Manager;
using PALibrary.Library.Exception;

namespace PALibrary.Library.DAO.Helper
{
    public class SQLHelper
    {
        private SQLHelper()
        {

        }

        public static int ExecuteNonQuery(IDbTransaction transaction, CommandType commandtype, string commandText, List<IDbDataParameter> parameters)
        {
            IDbCommand command = DBManager.GetCommand();
            PrepareCommand(command, transaction.Connection, transaction, commandtype, commandText, parameters);
            int result = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return result;
        }

        public static int ExecuteNonQuery(CommandType commandtype, string commandText, List<IDbDataParameter> parameters)
        {
            IDbConnection connection = DBManager.GetConnection();
            IDbCommand command = DBManager.GetCommand();

            try
            {
                connection.Open();
                PrepareCommand(command, connection, null, commandtype, commandText, parameters);
                int result = command.ExecuteNonQuery();
                command.Parameters.Clear();
                return result;
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

        public static IDataReader ExecuteReader(CommandType commandType, string commandText, List<IDbDataParameter> parameters)
        {
            IDbConnection connection = DBManager.GetConnection();
            IDbCommand command = DBManager.GetCommand();

            try
            {
                connection.Open();
                PrepareCommand(command, connection, null, commandType, commandText, parameters);
                IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                return reader;
            }
            catch (PAException ex)
            {
                DBUtils.CloseConnection(connection);
                throw new PAException(ex.Message);
            }
        }


        public static object ExecuteScalar(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, List<IDbDataParameter> parameters)
        {
            IDbCommand command = DBManager.GetCommand();
            try
            {
                PrepareCommand(command, connection, transaction, commandType, commandText, parameters);
                object result = command.ExecuteScalar();
                command.Parameters.Clear();
                return result;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static object ExecuteScalar(CommandType commandType, string commandText, List<IDbDataParameter> parameters)
        {
            IDbConnection connection = DBManager.GetConnection();
            IDbCommand command = DBManager.GetCommand();

            try
            {
                connection.Open();
                PrepareCommand(command, connection, null, commandType, commandText, parameters);
                object result = command.ExecuteScalar();
                command.Parameters.Clear();
                return result;
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

        private static void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> parameters)
        {
            if (command == null) throw new PAException("command cannot be null");
            if (connection == null) throw new PAException("connection cannot be null");

            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
                command.Transaction = transaction;

            if (parameters != null)
            {
                foreach (IDbDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
        }
    }
}
