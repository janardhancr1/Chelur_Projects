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
    class ChitsParticipateDAO
    {
        public static void AddChitsParticipateInfo(ChitsParticipateInfo chitsParticipateInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsParticipateInfo.QUERY_INSERT, chitsParticipateInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsParticipateInfo.QUERY_UPDATE, chitsParticipateInfo.GetParameters());

                transaction.Commit();
                transaction.Dispose();
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

        public static void DeleteChitsParticipateInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsParticipateInfo.QUERY_DELETE, parameters);

                transaction.Commit();
                transaction.Dispose();
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

        public static SearchHelper SearchConditions(int recordID, string chitNO, int customerID)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (recordID > 0)
                {
                    conditions.Add("Record_ID = " + ChitsParticipateInfo.PARAM_RECORD_ID);
                    parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_RECORD_ID, recordID));
                }

                if (chitNO != null)
                {
                    if (chitNO.Trim().Length > 0)
                    {
                        conditions.Add("Chit_No LIKE " + ChitsParticipateInfo.PARAM_CHIT_NO);
                        parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_CHIT_NO, chitNO + "%"));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("Customer_ID = " + ChitsParticipateInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_CUSTOMER_ID, customerID));
                }

                SearchHelper searchHelper = new SearchHelper();
                searchHelper.Conditions = conditions;
                searchHelper.Parameters = parameters;
                return searchHelper;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static List<ChitsParticipateInfo> SearchChitsParticipateInfo(SearchHelper searchHelper, int startPage)
        {
            List<ChitsParticipateInfo> chitsParticipateInfos = new List<ChitsParticipateInfo>();
            IDataReader reader = null;
            try
            {
                string query = ChitsParticipateInfo.QUERY_SEARCH;
                if (searchHelper.Conditions.Count > 0)
                {
                    query = query + " WHERE " + searchHelper.Conditions[0];
                }
                for (int i = 1; i < searchHelper.Conditions.Count; i++)
                {
                    query = query + " AND " + searchHelper.Conditions[i];
                }

                if (startPage >= 0)
                {
                    int currentPage = DBConstant.PAGE_SIZE * (startPage / DBConstant.PAGE_SIZE);
                    query += " LIMIT " + currentPage + "," + DBConstant.PAGE_SIZE;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, searchHelper.Parameters);
                while (reader.Read())
                {
                    ChitsParticipateInfo chitsParticipateInfo = new ChitsParticipateInfo();
                    chitsParticipateInfo.ReadValues(reader);

                    chitsParticipateInfos.Add(chitsParticipateInfo);
                }
                return chitsParticipateInfos;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static int SearchChitsParticipateInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ChitsParticipateInfo.QUERY_COUNT;
                if (searchHelper.Conditions.Count > 0)
                {
                    query = query + " WHERE " + searchHelper.Conditions[0];
                }
                for (int i = 1; i < searchHelper.Conditions.Count; i++)
                {
                    query = query + " AND " + searchHelper.Conditions[i];
                }

                int count = DBUtils.ConvertInt(SQLHelper.ExecuteScalar(CommandType.Text, query, searchHelper.Parameters));
                return count;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static ChitsParticipateInfo GetChitsParticipateInfo(int recordID)
        {
            ChitsParticipateInfo chitsParticipateInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsParticipateInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    chitsParticipateInfo = new ChitsParticipateInfo();
                    chitsParticipateInfo.ReadValues(reader);
                }
                return chitsParticipateInfo;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static List<ChitsParticipateInfo> GetChitsParticipateInfos(string chitNo)
        {
            List<ChitsParticipateInfo> chitsParticipateInfos = new List<ChitsParticipateInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_CHIT_NO, chitNo));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsParticipateInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    ChitsParticipateInfo chitsParticipateInfo = new ChitsParticipateInfo();
                    chitsParticipateInfo.ReadValues(reader);

                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(chitsParticipateInfo.CustomerID);
                    chitsParticipateInfo.CustomerName = customer.CustomerName;
                    chitsParticipateInfo.CustomerAddress = customer.FullAddress;

                    chitsParticipateInfos.Add(chitsParticipateInfo);
                }
                return chitsParticipateInfos;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

    }
}