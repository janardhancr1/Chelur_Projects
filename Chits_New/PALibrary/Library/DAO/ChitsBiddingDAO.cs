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

        public static SearchHelper SearchConditions(string chitNO, int installmentNO, decimal paidAmount, DateTime bidDate, DateTime paidDate, int customerID, decimal leftAmount)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();

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

                if (paidAmount > 0)
                {
                    conditions.Add("Paid_Amount = " + ChitsBiddingInfo.PARAM_PAID_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_PAID_AMOUNT, paidAmount));
                }

                if (bidDate != null)
                {
                    if (bidDate.Year > 1980)
                    {
                        conditions.Add("Bid_Date = " + ChitsBiddingInfo.PARAM_BID_DATE);
                        parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_BID_DATE, bidDate));
                    }
                }

                if (paidDate != null)
                {
                    if (paidDate.Year > 1980)
                    {
                        conditions.Add("Paid_Date = " + ChitsBiddingInfo.PARAM_PAID_DATE);
                        parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_PAID_DATE, paidDate));
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

        public static int GetLastBiddingInstallment(string chitNO)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_CHIT_NO, chitNO));

                int count = DBUtils.ConvertInt(SQLHelper.ExecuteScalar(CommandType.Text, ChitsBiddingInfo.QUERY_MAX_INSTALLMENT, parameters));
                count++;
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

        public static List<ChitsBiddingInfo> GetChitsBiddingInfos(string chitNO)
        {
            List<ChitsBiddingInfo> chitsBiddingInfos = new List<ChitsBiddingInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_CHIT_NO, chitNO));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsBiddingInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    ChitsBiddingInfo chitsBiddingInfo = new ChitsBiddingInfo();
                    chitsBiddingInfo.ReadValues(reader);

                    if (chitsBiddingInfo.CustomerID > 0)
                    {
                        CustomerInfo customer = CustomerDAO.GetCustomerInfo(chitsBiddingInfo.CustomerID);
                        chitsBiddingInfo.CustomerName = customer.CustomerName;
                        chitsBiddingInfo.CustomerAddress = customer.FullAddress;
                    }
                    else
                    {
                        chitsBiddingInfo.CustomerName = "Company Bidding";
                        chitsBiddingInfo.CustomerAddress = "";
                    }


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

        public static List<ChitsBiddingInfo> GetChitsBiddingInfos(DateTime paidDate)
        {
            List<ChitsBiddingInfo> chitsBiddingInfos = new List<ChitsBiddingInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_PAID_DATE, paidDate));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsBiddingInfo.QUERY_SELECT_UPTO, parameters);
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


        public static DayBookInfo GetOpeningBids(DateTime toDate, string ledgerName, int type)
        {
            DayBookInfo openingBalance = new DayBookInfo();
            openingBalance.Debit = 0;
            openingBalance.Credit = 0;
            IDataReader reader = null;
            try
            {
                string query = "";
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();

                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_PAID_DATE, toDate));

                    query = ChitsBiddingInfo.QUERY_SELECT_OPENING;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance.Particulars = DBConstant.CHITS_BIDDING;
                    openingBalance.Credit = DBUtils.ConvertDecimal(reader["Amount"]);
                }

                return openingBalance;
            }
            catch (PAException pe)
            {
                throw new PAException(pe.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

        public static List<ChitsBiddingInfo> GetChitBids(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<ChitsBiddingInfo> bids = new List<ChitsBiddingInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_TO_DATE, toDate));

                    query = ChitsBiddingInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsBiddingInfo.PARAM_TO_DATE, toDate));

                    query = ChitsBiddingInfo.QUERY_SELECT_PERIOD;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    ChitsBiddingInfo bid = new ChitsBiddingInfo();
                    bid.ReadValues(reader);

                    bids.Add(bid);
                }
                return bids;
            }
            catch (PAException pe)
            {
                throw new PAException(pe.Message);
            }
            finally
            {
                DBUtils.CloseReader(reader);
            }
        }

    }
}