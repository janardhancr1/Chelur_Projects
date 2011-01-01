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
    class FixedTransDAO
    {
        public static void AddFixedTransInfo(FixedTransInfo fixedTransInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedTransInfo.QUERY_INSERT, fixedTransInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedTransInfo.QUERY_UPDATE, fixedTransInfo.GetParameters());

                if (fixedTransInfo.Balance == 0)
                {
                    List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FD_NO, fixedTransInfo.FDNO));

                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedDepositInfo.QUERY_SET_CLOSED, parameters);
                }

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

        public static void DeleteFixedTransInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedTransInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int recordID, string fDNO, DateTime paidDate, decimal amount, string receiptNO)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (recordID > 0)
                {
                    conditions.Add("Record_ID = " + FixedTransInfo.PARAM_RECORD_ID);
                    parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_RECORD_ID, recordID));
                }

                if (fDNO != null)
                {
                    if (fDNO.Trim().Length > 0)
                    {
                        conditions.Add("FD_No LIKE " + FixedTransInfo.PARAM_FD_NO);
                        parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_FD_NO, fDNO + "%"));
                    }
                }

                if (paidDate != null)
                {
                    if (paidDate.Year > 1980)
                    {
                        conditions.Add("Paid_Date = " + FixedTransInfo.PARAM_PAID_DATE);
                        parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_PAID_DATE, paidDate));
                    }
                }

                if (amount > 0)
                {
                    conditions.Add("Amount = " + FixedTransInfo.PARAM_AMOUNT);
                    parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_AMOUNT, amount));
                }

                if (receiptNO != null)
                {
                    if (receiptNO.Trim().Length > 0)
                    {
                        conditions.Add("Receipt_No LIKE " + FixedTransInfo.PARAM_RECEIPT_NO);
                        parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_RECEIPT_NO, receiptNO + "%"));
                    }
                }

                SearchHelper searchHelper = new SearchHelper();
                FixedDepositInfo fixedDeposit = FixedDepositDAO.GetFixedDepositInfo(fDNO);
                if (fixedDeposit != null)
                    searchHelper.Amount = fixedDeposit.Amount;
                searchHelper.Conditions = conditions;
                searchHelper.Parameters = parameters;
                return searchHelper;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static List<FixedTransInfo> SearchFixedTransInfo(SearchHelper searchHelper, int startPage)
        {
            List<FixedTransInfo> fixedTransInfos = new List<FixedTransInfo>();
            IDataReader reader = null;
            try
            {
                string query = FixedTransInfo.QUERY_SEARCH;
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

                decimal balance = searchHelper.Amount;

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, searchHelper.Parameters);
                while (reader.Read())
                {
                    FixedTransInfo fixedTransInfo = new FixedTransInfo();
                    fixedTransInfo.ReadValues(reader);
                    balance = balance - fixedTransInfo.Amount;
                    fixedTransInfo.Balance = balance;

                    fixedTransInfos.Add(fixedTransInfo);
                }
                return fixedTransInfos;
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

        public static int SearchFixedTransInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = FixedTransInfo.QUERY_COUNT;
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

        public static FixedTransInfo GetFixedTransInfo(int recordID)
        {
            FixedTransInfo fixedTransInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedTransInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    fixedTransInfo = new FixedTransInfo();
                    fixedTransInfo.ReadValues(reader);
                }
                return fixedTransInfo;
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

        public static List<FixedTransInfo> GetFixedTransInfos()
        {
            List<FixedTransInfo> fixedTransInfos = new List<FixedTransInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedTransInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    FixedTransInfo fixedTransInfo = new FixedTransInfo();
                    fixedTransInfo.ReadValues(reader);

                    fixedTransInfos.Add(fixedTransInfo);
                }
                return fixedTransInfos;
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

        public static List<FixedTransInfo> GetFixedTransInfos(string fDNO, decimal amount)
        {
            List<FixedTransInfo> fixedTransInfos = new List<FixedTransInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_FD_NO, fDNO));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedTransInfo.QUERY_SELECT_FD, parameters);
                while (reader.Read())
                {
                    FixedTransInfo fixedTransInfo = new FixedTransInfo();
                    fixedTransInfo.ReadValues(reader);
                    amount = amount - fixedTransInfo.Amount;
                    fixedTransInfo.Balance = amount;
                    fixedTransInfos.Add(fixedTransInfo);
                }
                return fixedTransInfos;
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

        #region Accounts

        public static DayBookInfo GetOpeningTrans(DateTime toDate, string customerName, int type)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_PAID_DATE, toDate));

                    query = FixedTransInfo.QUERY_SELECT_OPENING;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_CUSTOMER)
                {
                    parameters.Add(DBManager.GetParameter(FixedTransInfo.PARAM_PAID_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = FixedTransInfo.QUERY_SELECT_OPENING_CUSTOMER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_FDPAYMENT;
                    openingBalance.Debit = 0;
                    openingBalance.Credit = DBUtils.ConvertDecimal(reader["Amount"]); ;
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

        public static List<FixedDepositInfo> GetTransDetails(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<FixedDepositInfo> fixedDeposits = new List<FixedDepositInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_TO_DATE, toDate));

                    query = FixedTransInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_TO_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = FixedTransInfo.QUERY_SELECT_LEDGER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    FixedDepositInfo fixedDeposit = new FixedDepositInfo();
                    fixedDeposit.FDNO = DBUtils.ConvertString(reader["FD_No"]);
                    fixedDeposit.StartDate = DBUtils.ConvertDate(reader["Paid_date"]);
                    fixedDeposit.AccountNo = DBUtils.ConvertString(reader["Receipt_no"]);
                    fixedDeposit.Amount = DBUtils.ConvertDecimal(reader["Amount"]);
                    fixedDeposit.CustomerName = DBUtils.ConvertString(reader["customer_name"]);

                    fixedDeposits.Add(fixedDeposit);
                }
                return fixedDeposits;
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

        #endregion
    }
}