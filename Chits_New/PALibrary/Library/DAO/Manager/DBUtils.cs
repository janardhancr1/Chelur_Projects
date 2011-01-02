using System;
using System.Data;
using PALibrary.Library.Exception;

namespace PALibrary.Library.DAO.Manager
{
    public class DBUtils
    {
        /// <summary>
        /// Function used to close the database connection
        /// </summary>
        /// <param name="connection">input the connection object</param>
        public static void CloseConnection(IDbConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            catch (PAException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Function to close database transaction
        /// </summary>
        /// <param name="transaction">input the transaction object</param>
        public static void CloseConnection(IDbTransaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
            catch (PAException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Function to close the database reader
        /// </summary>
        /// <param name="reader">input the reader object</param>
        public static void CloseReader(IDataReader reader)
        {
            try
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            catch (PAException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Function to close the dataset
        /// </summary>
        /// <param name="dataset">input the dataset object</param>
        public static void CloseDataSet(DataSet dataset)
        {
            try
            {
                if (dataset != null)
                {
                    dataset.Dispose();
                }
            }
            catch (PAException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertInt(object value)
        {
            int result = 0;
            try
            {
                if (value != null && !Convert.IsDBNull(value))
                {
                    if (Convert.ToString(value).Trim().Length > 0)
                    {
                        result = Convert.ToInt32(value);
                    }
                }
            }
            catch (PAException ex)
            {
                result = 0;
                Console.WriteLine(ex.Message);
                return result;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ConvertDate(object value)
        {
            DateTime result = new DateTime();
            if (value != null && !Convert.IsDBNull(value))
                result = Convert.ToDateTime(value);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ConvertDecimal(object value)
        {
            decimal result = 0;
            if (value != null && !Convert.IsDBNull(value))
                result = Convert.ToDecimal(value);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertDouble(object value)
        {
            double result = 0;
            if (value != null && !Convert.IsDBNull(value))
                result = Convert.ToDouble(value);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertString(object value)
        {
            string result = "";
            if (!Convert.IsDBNull(value))
                result = Convert.ToString(value);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ConvertBool(object value)
        {
            bool result = false;
            if (!Convert.IsDBNull(value))
                result = Convert.ToBoolean(value);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static byte[] ConvertByte(object value)
        //{
        //    byte[] result = null;
        //    if (!Convert.IsDBNull(value))
        //    {
        //        ImageConverter convertor = new ImageConverter();
        //        result = (byte[])convertor.CanConvertTo(value, typeof(byte[]));
        //    }

        //    return result;
        //}
    }
}
