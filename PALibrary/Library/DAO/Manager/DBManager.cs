using System.Data;
using System.Data.Common;
using System.Configuration;
using PALibrary.Library.Exception;

namespace PALibrary.Library.DAO.Manager
{
    public class DBManager
    {
        //Private construtor
        private DBManager()
        {

        }

        /// <summary>
        /// Function to get the connection to the database
        /// </summary>
        /// <returns>The database connection object</returns>
        public static IDbConnection GetConnection()
        {
            IDbConnection dbconnection;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBServer"].ConnectionString;
                dbconnection = GetProvider().CreateConnection();
                dbconnection.ConnectionString = connectionString;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new PAException(ex.Message);
            }
            return dbconnection;
        }

        /// <summary>
        /// Function to get the database command
        /// </summary>
        /// <returns>return database command</returns>
        public static IDbCommand GetCommand()
        {
            IDbCommand command;
            try
            {
                command = GetProvider().CreateCommand();
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new PAException(ex.Message);
            }
            return command;
        }

        /// <summary>
        /// Function to get the parameter
        /// </summary>
        /// <returns>return parameter</returns>
        public static IDbDataParameter GetParameter(string name, object value)
        {
            IDbDataParameter parameter;
            try
            {
                parameter = GetProvider().CreateParameter();
                parameter.ParameterName = name;
                parameter.Value = value;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new PAException(ex.Message);
            }
            return parameter;
        }

        /// <summary>
        /// Function to get the provider 
        /// </summary>
        /// <returns>return the provider type</returns>
        private static DbProviderFactory GetProvider()
        {
            DbProviderFactory provider;
            try
            {
                string PROVIDER_TYPE = ConfigurationManager.ConnectionStrings["DBServer"].ProviderName;
                provider = DbProviderFactories.GetFactory(PROVIDER_TYPE);
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new PAException(ex.Message);
            }
            return provider;
        }
    }
}
