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
    class ChitsBiddingDAO
    {
        public static void AddChitsBiddingInfo(ChitsBiddingInfo chitsBiddingInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsBiddingInfo.QUERY_INSERT, chitsBiddingInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsBiddingInfo.QUERY_UPDATE, chitsBiddingInfo.GetParameters());

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

        public static void DeleteChitsBiddingInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsBiddingInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int recordID, string chitNO, int installmentNO, decimal bidAmount, DateTime bidDate, int customerID, decimal leftAmount)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (recordID > 0)
                {
                    conditions.Add("Record_ID = " + ChitsBiddingInfo.PARAM_RECORD_ID);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_RECORD_ID, recordID));
                }

                if (chitNO != null)
                {
                    if (chitNO.Trim().Length > 0)
                    {
                        conditions.Add("Chit_No LIKE " + ChitsBiddingInfo.PARAM_CHIT_NO);
                        parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_CHIT_NO, chitNO + "%"));
                    }
                }

                if (installmentNO > 0)
                {
                    conditions.Add("Installment_No = " + ChitsBiddingInfo.PARAM_INSTALLMENT_NO);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_INSTALLMENT_NO, installmentNO));
                }

                if (bidAmount > 0)
                {
                    conditions.Add("Bid_Amount = " + ChitsBiddingInfo.PARAM_BID_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_BID_AMOUNT, bidAmount));
                }

                if (bidDate != null)
                {
                    if (bidDate.Year > 1980)
                    {
                        conditions.Add("Bid_Date = " + ChitsBiddingInfo.PARAM_BID_DATE);
                        parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_BID_DATE, bidDate));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("Customer_ID = " + ChitsBiddingInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_CUSTOMER_ID, customerID));
                }

                if (leftAmount > 0)
                {
                    conditions.Add("Left_Amount = " + ChitsBiddingInfo.PARAM_LEFT_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_LEFT_AMOUNT, leftAmount));
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

        public static List<ChitsBiddingInfo> SearchChitsBiddingInfo(SearchHelper searchHelper, int startPage)
        {
            List<ChitsBiddingInfo> chitsBiddingInfos = new List<ChitsBiddingInfo>();
            IDataReader reader = null;
            try
            {
                string query = ChitsBiddingInfo.QUERY_SEARCH;
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
                    ChitsBiddingInfo chitsBiddingInfo = new ChitsBiddingInfo();
                    chitsBiddingInfo.ReadValues(reader);

                    chitsBiddingInfos.Add(chitsBiddingInfo);
                }
                return chitsBiddingInfos;
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

        public static int SearchChitsBiddingInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ChitsBiddingInfo.QUERY_COUNT;
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

        public static ChitsBiddingInfo GetChitsBiddingInfo(int recordID)
        {
            ChitsBiddingInfo chitsBiddingInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsBiddingInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    chitsBiddingInfo = new ChitsBiddingInfo();
                    chitsBiddingInfo.ReadValues(reader);
                }
                return chitsBiddingInfo;
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

        public static List<ChitsBiddingInfo> GetChitsBiddingInfos(string chitNo)
        {
            List<ChitsBiddingInfo> chitsBiddingInfos = new List<ChitsBiddingInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsParticipateInfo.PARAM_CHIT_NO, chitNo));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsBiddingInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    ChitsBiddingInfo chitsBiddingInfo = new ChitsBiddingInfo();
                    chitsBiddingInfo.ReadValues(reader);

                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(chitsBiddingInfo.CustomerID);
                    chitsBiddingInfo.CustomerName = customer.CustomerName;
                    chitsBiddingInfo.CustomerAddress = customer.FullAddress;

                    chitsBiddingInfos.Add(chitsBiddingInfo);
                }
                return chitsBiddingInfos;
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