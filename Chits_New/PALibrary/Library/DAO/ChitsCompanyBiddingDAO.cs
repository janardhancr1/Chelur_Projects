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
    class ChitsCompanyBiddingDAO
    {
        public static void AddChitsCompanyBiddingInfo(ChitsCompanyBiddingInfo chitsCompanyBiddingInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsCompanyBiddingInfo.QUERY_INSERT, chitsCompanyBiddingInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsCompanyBiddingInfo.QUERY_UPDATE, chitsCompanyBiddingInfo.GetParameters());

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

        public static void DeleteChitsCompanyBiddingInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsCompanyBiddingInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int recordID, string chitNO, int installmentNO, decimal paidAmount, DateTime paidDate, int customerID)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (recordID > 0)
                {
                    conditions.Add("Record_Id = " + ChitsCompanyBiddingInfo.PARAM_RECORD_ID);
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_RECORD_ID, recordID));
                }

                if (chitNO != null)
                {
                    if (chitNO.Trim().Length > 0)
                    {
                        conditions.Add("Chit_No LIKE " + ChitsCompanyBiddingInfo.PARAM_CHIT_NO);
                        parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_CHIT_NO, chitNO + "%"));
                    }
                }

                if (installmentNO > 0)
                {
                    conditions.Add("Installment_No = " + ChitsCompanyBiddingInfo.PARAM_INSTALLMENT_NO);
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_INSTALLMENT_NO, installmentNO));
                }

                if (paidAmount > 0)
                {
                    conditions.Add("Paid_Amount = " + ChitsCompanyBiddingInfo.PARAM_PAID_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_PAID_AMOUNT, paidAmount));
                }

                if (paidDate != null)
                {
                    if (paidDate.Year > 1980)
                    {
                        conditions.Add("Paid_Date = " + ChitsCompanyBiddingInfo.PARAM_PAID_DATE);
                        parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_PAID_DATE, paidDate));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("Customer_Id = " + ChitsCompanyBiddingInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_CUSTOMER_ID, customerID));
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

        public static List<ChitsCompanyBiddingInfo> SearchChitsCompanyBiddingInfo(SearchHelper searchHelper, int startPage)
        {
            List<ChitsCompanyBiddingInfo> chitsCompanyBiddingInfos = new List<ChitsCompanyBiddingInfo>();
            IDataReader reader = null;
            try
            {
                string query = ChitsCompanyBiddingInfo.QUERY_SEARCH;
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
                    ChitsCompanyBiddingInfo chitsCompanyBiddingInfo = new ChitsCompanyBiddingInfo();
                    chitsCompanyBiddingInfo.ReadValues(reader);

                    chitsCompanyBiddingInfos.Add(chitsCompanyBiddingInfo);
                }
                return chitsCompanyBiddingInfos;
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

        public static int SearchChitsCompanyBiddingInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ChitsCompanyBiddingInfo.QUERY_COUNT;
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

        public static ChitsCompanyBiddingInfo GetChitsCompanyBiddingInfo(int recordID)
        {
            ChitsCompanyBiddingInfo chitsCompanyBiddingInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsCompanyBiddingInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    chitsCompanyBiddingInfo = new ChitsCompanyBiddingInfo();
                    chitsCompanyBiddingInfo.ReadValues(reader);
                }
                return chitsCompanyBiddingInfo;
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

        public static List<ChitsCompanyBiddingInfo> GetChitsCompanyBiddingInfos()
        {
            List<ChitsCompanyBiddingInfo> chitsCompanyBiddingInfos = new List<ChitsCompanyBiddingInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsCompanyBiddingInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    ChitsCompanyBiddingInfo chitsCompanyBiddingInfo = new ChitsCompanyBiddingInfo();
                    chitsCompanyBiddingInfo.ReadValues(reader);

                    chitsCompanyBiddingInfos.Add(chitsCompanyBiddingInfo);
                }
                return chitsCompanyBiddingInfos;
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

        public static DayBookInfo GetCompBiddingOpeningBalance(DateTime toDate, string ledgerName, int type)
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
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_PAID_DATE, toDate));

                    query = ChitsCompanyBiddingInfo.QUERY_SELECT_OPENING_COMPBID;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance.Particulars = DBConstant.COMP_BIDDING;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader["Amount"]);
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

        public static List<ChitsCompanyBiddingInfo> GetCompBids(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<ChitsCompanyBiddingInfo> bids = new List<ChitsCompanyBiddingInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_TO_DATE, toDate));

                    query = ChitsCompanyBiddingInfo.QUERY_SELECT_PERIOD_COMPBID;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsCompanyBiddingInfo.PARAM_TO_DATE, toDate));

                    query = ChitsCompanyBiddingInfo.QUERY_SELECT_PERIOD_COMPBID;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    ChitsCompanyBiddingInfo bid = new ChitsCompanyBiddingInfo();
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