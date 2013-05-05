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
    class ChitsTransDAO
    {
        public static void AddChitsTransInfo(ChitsTransInfo chitsTransInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsTransInfo.QUERY_INSERT, chitsTransInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsTransInfo.QUERY_UPDATE, chitsTransInfo.GetParameters());

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

        public static void DeleteChitsTransInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, ChitsTransInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(string chitNO, int customerID, int installmentNO, decimal installmentAmount, DateTime date)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();

                if (chitNO != null)
                {
                    if (chitNO.Trim().Length > 0)
                    {
                        conditions.Add("Chit_No LIKE " + ChitsTransInfo.PARAM_CHIT_NO);
                        parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_CHIT_NO, chitNO + "%"));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("Customer_ID = " + ChitsTransInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_CUSTOMER_ID, customerID));
                }

                if (installmentNO > 0)
                {
                    conditions.Add("Installment_No = " + ChitsTransInfo.PARAM_INSTALLMENT_NO);
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_INSTALLMENT_NO, installmentNO));
                }

                if (installmentAmount > 0)
                {
                    conditions.Add("Installment_Amount = " + ChitsTransInfo.PARAM_INSTALLMENT_AMOUNT);
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_INSTALLMENT_AMOUNT, installmentAmount));
                }

                if (date != null)
                {
                    if (date.Year > 1980)
                    {
                        conditions.Add("Date = " + ChitsTransInfo.PARAM_DATE);
                        parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_DATE, date));
                    }
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

        public static List<ChitsTransInfo> SearchChitsTransInfo(SearchHelper searchHelper, int startPage)
        {
            List<ChitsTransInfo> chitsTransInfos = new List<ChitsTransInfo>();
            IDataReader reader = null;
            try
            {
                string query = ChitsTransInfo.QUERY_SEARCH;
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
                    ChitsTransInfo chitsTransInfo = new ChitsTransInfo();
                    chitsTransInfo.ReadValues(reader);

                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(chitsTransInfo.CustomerID);
                    chitsTransInfo.CustomerName = customer.CustomerName;
                    chitsTransInfo.CustomerAddress = customer.FullAddress;

                    chitsTransInfos.Add(chitsTransInfo);
                }
                return chitsTransInfos;
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

        public static int SearchChitsTransInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = ChitsTransInfo.QUERY_COUNT;
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

        public static ChitsTransInfo GetChitsTransInfo(int recordID)
        {
            ChitsTransInfo chitsTransInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsTransInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    chitsTransInfo = new ChitsTransInfo();
                    chitsTransInfo.ReadValues(reader);
                }
                return chitsTransInfo;
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

        public static List<ChitsTransInfo> GetChitsTransInfos()
        {
            List<ChitsTransInfo> chitsTransInfos = new List<ChitsTransInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, ChitsTransInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    ChitsTransInfo chitsTransInfo = new ChitsTransInfo();
                    chitsTransInfo.ReadValues(reader);

                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(chitsTransInfo.CustomerID);
                    chitsTransInfo.CustomerName = customer.CustomerName;
                    chitsTransInfo.CustomerAddress = customer.FullAddress;

                    chitsTransInfos.Add(chitsTransInfo);
                }
                return chitsTransInfos;
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

        public static DayBookInfo GetOpeningTrans(DateTime toDate, string ledgerName, int type)
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
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_OPENING;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance.Particulars = DBConstant.CHITS_INSTALLMENT;
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

        public static List<ChitsTransInfo> GetChitTrans(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<ChitsTransInfo> trans = new List<ChitsTransInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_TO_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_TO_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_PERIOD;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    ChitsTransInfo tran = new ChitsTransInfo();
                    tran.ReadValues(reader);

                    trans.Add(tran);
                }
                return trans;
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


        public static DayBookInfo GetOpeningDiscount(DateTime toDate, string ledgerName, int type)
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
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_DISCOUNT_OPENING;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance.Particulars = DBConstant.CHITS_DISCOUNT;
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

        public static List<ChitsTransInfo> GetChitDiscounts(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<ChitsTransInfo> trans = new List<ChitsTransInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_TO_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_DISCOUNT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(ChitsTransInfo.PARAM_TO_DATE, toDate));

                    query = ChitsTransInfo.QUERY_SELECT_DISCOUNT_PERIOD;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    ChitsTransInfo tran = new ChitsTransInfo();
                    tran.ReadValues(reader);

                    trans.Add(tran);
                }
                return trans;
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