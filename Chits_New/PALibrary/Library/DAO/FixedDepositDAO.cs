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
    class FixedDepositDAO
    {
        public static void AddFixedDepositInfo(FixedDepositInfo fixedDepositInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedDepositInfo.QUERY_INSERT, fixedDepositInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedDepositInfo.QUERY_UPDATE, fixedDepositInfo.GetParameters());

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

        public static void DeleteFixedDepositInfo(string fDNO)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FD_NO, fDNO));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedDepositInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(string fDNO, int customerID, DateTime startDate, string nomineeName, string relationship, decimal amount, decimal rate, string closed)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (fDNO != null)
                {
                    if (fDNO.Trim().Length > 0)
                    {
                        conditions.Add("FD_No LIKE " + FixedDepositInfo.PARAM_FD_NO);
                        parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FD_NO, fDNO + "%"));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("f.Customer_ID = " + FixedDepositInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CUSTOMER_ID, customerID));
                }

                if (startDate != null)
                {
                    if (startDate.Year > 1980)
                    {
                        conditions.Add("Start_Date = " + FixedDepositInfo.PARAM_START_DATE);
                        parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_START_DATE, startDate));
                    }
                }

                if (nomineeName != null)
                {
                    if (nomineeName.Trim().Length > 0)
                    {
                        conditions.Add("Nominee_Name LIKE " + FixedDepositInfo.PARAM_NOMINEE_NAME);
                        parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_NOMINEE_NAME, nomineeName + "%"));
                    }
                }

                if (relationship != null)
                {
                    if (relationship.Trim().Length > 0)
                    {
                        conditions.Add("Relationship LIKE " + FixedDepositInfo.PARAM_RELATIONSHIP);
                        parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_RELATIONSHIP, relationship + "%"));
                    }
                }

                if (amount > 0)
                {
                    conditions.Add("Amount = " + FixedDepositInfo.PARAM_AMOUNT);
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_AMOUNT, amount));
                }

                if (rate > 0)
                {
                    conditions.Add("Rate = " + FixedDepositInfo.PARAM_RATE);
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_RATE, rate));
                }

                if (closed != null)
                {
                    if (closed.Trim().Length > 0)
                    {
                        conditions.Add("Closed LIKE " + FixedDepositInfo.PARAM_CLOSED);
                        parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CLOSED, closed + "%"));
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

        public static List<FixedDepositInfo> SearchFixedDepositInfo(SearchHelper searchHelper, int startPage)
        {
            List<FixedDepositInfo> fixedDepositInfos = new List<FixedDepositInfo>();
            IDataReader reader = null;
            try
            {
                string query = FixedDepositInfo.QUERY_SEARCH;
                //if (searchHelper.Conditions.Count > 0)
                //{
                //    query = query + " WHERE " + searchHelper.Conditions[0];
                //}
                for (int i = 0; i < searchHelper.Conditions.Count; i++)
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
                    FixedDepositInfo fixedDepositInfo = new FixedDepositInfo();
                    fixedDepositInfo.ReadValues(reader);
                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(fixedDepositInfo.CustomerID);
                    CityInfo city = CityDAO.GetCityInfo(customer.ResVillage);
                    fixedDepositInfo.CustomerAddress = "S/D/H " + customer.SonHusband + ", " + city.VillageName + ", " + city.CityName + ", " + city.State + "-" + city.Pincode;

                    List<FixedInterestInfo> interests = FixedInterestDAO.GetFixedInterestInfos(fixedDepositInfo.FDNO);
                    foreach (FixedInterestInfo interest in interests)
                    {
                        fixedDepositInfo.InterestPaid = interest.InterestUpto;
                    }

                    fixedDepositInfos.Add(fixedDepositInfo);
                }
                return fixedDepositInfos;
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

        public static int SearchFixedDepositInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = FixedDepositInfo.QUERY_COUNT;
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

        public static FixedDepositInfo GetFixedDepositInfo(string fDNO)
        {
            FixedDepositInfo fixedDepositInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FD_NO, fDNO));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedDepositInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    fixedDepositInfo = new FixedDepositInfo();
                    fixedDepositInfo.ReadValues(reader);
                    fixedDepositInfo.Balance = fixedDepositInfo.Amount - fixedDepositInfo.Balance;
                }
                return fixedDepositInfo;
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

        public static List<FixedDepositInfo> GetFixedDepositInfos()
        {
            List<FixedDepositInfo> fixedDepositInfos = new List<FixedDepositInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedDepositInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    FixedDepositInfo fixedDepositInfo = new FixedDepositInfo();
                    fixedDepositInfo.ReadValues(reader);

                    fixedDepositInfos.Add(fixedDepositInfo);
                }
                return fixedDepositInfos;
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

        public static List<FixedDepositInfo> GetFixedDepositInfos(DateTime fromDate, DateTime toDate, string closed)
        {
            List<FixedDepositInfo> fixedDepositInfos = new List<FixedDepositInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_TO_DATE, toDate));
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CLOSED, closed));

                string query = "";
                if (closed.Equals(DBConstant.TYPE_CLOSED) || closed.Equals(DBConstant.TYPE_PENDING))
                    query = FixedDepositInfo.QUERY_REPORT_ON_CLOSED;
                else if (closed.Equals(DBConstant.TYPE_ALL))
                    query = FixedDepositInfo.QUERY_REPORT_ALL;

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    FixedDepositInfo fixedDepositInfo = new FixedDepositInfo();
                    fixedDepositInfo.ReadValues(reader);
                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(fixedDepositInfo.CustomerID);
                    CityInfo city = CityDAO.GetCityInfo(customer.ResVillage);
                    fixedDepositInfo.CustomerAddress = city.VillageName + ", " + city.CityName;

                    List<FixedInterestInfo> interests = FixedInterestDAO.GetFixedInterestInfos(fixedDepositInfo.FDNO);
                    foreach (FixedInterestInfo interest in interests)
                    {
                        fixedDepositInfo.InterestPaid = interest.InterestUpto;
                    }

                    fixedDepositInfos.Add(fixedDepositInfo);
                }
                return fixedDepositInfos;
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

        public static DayBookInfo GetOpeningFixedDeposits(DateTime toDate, string customerName, int type)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_START_DATE, toDate));

                    query = FixedDepositInfo.QUERY_SELECT_OPENING;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_CUSTOMER)
                {
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_START_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = FixedDepositInfo.QUERY_SELECT_OPENING_CUSTOMER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_FIXEDDESPOSIT;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader["Amount"]);;
                    openingBalance.Credit = 0;
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

        public static List<FixedDepositInfo> GetFixedDeposits(DateTime fromDate, DateTime toDate, string customerName, int type)
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

                    query = FixedDepositInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_TO_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = FixedDepositInfo.QUERY_SELECT_LEDGER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    FixedDepositInfo fixedDeposit = new FixedDepositInfo();
                    fixedDeposit.ReadValues(reader);

                    //LedgersInfo bank = LedgersDAO.GetLedgersInfo(hundiLoan.BankID);
                    //if (bank != null) hundiLoan.BankName = bank.LedgerName;

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