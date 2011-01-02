using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using PALibrary.Library.DAO.Manager;
using PALibrary.Library.DAO.Helper;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.DAO
{
    class HundiLoanDAO
    {
        #region HundiLoan Details
        public static void AddHundiLoanInfo(HundiLoanInfo hundiLoanInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiLoanInfo.QUERY_INSERT, hundiLoanInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiLoanInfo.QUERY_UPDATE, hundiLoanInfo.GetParameters());

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

        public static void DeleteHundiLoanInfo(string hlLoanno)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_HL_LOANNO, hlLoanno));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiLoanInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(string hlLoanno, int customerID, string coObligent, string coobligentAddress, decimal loanAmount, DateTime loanDate, string closed, decimal rate, int payMode, string chequeNO, int bankID)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (hlLoanno != null)
                {
                    if (hlLoanno.Trim().Length > 0)
                    {
                        conditions.Add("Hl_loanno LIKE " + HundiLoanInfo.PARAM_HL_LOANNO);
                        parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_HL_LOANNO, hlLoanno + "%"));
                    }
                }

                if (customerID > 0)
                {
                    conditions.Add("h.Customer_id = " + HundiLoanInfo.PARAM_CUSTOMER_ID);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_ID, customerID));
                }

                if (coObligent != null)
                {
                    if (coObligent.Trim().Length > 0)
                    {
                        conditions.Add("Co_obligent LIKE " + HundiLoanInfo.PARAM_CO_OBLIGENT);
                        parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CO_OBLIGENT, coObligent + "%"));
                    }
                }

                if (coobligentAddress != null)
                {
                    if (coobligentAddress.Trim().Length > 0)
                    {
                        conditions.Add("Coobligent_address LIKE " + HundiLoanInfo.PARAM_COOBLIGENT_ADDRESS);
                        parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_COOBLIGENT_ADDRESS, coobligentAddress + "%"));
                    }
                }

                if (loanAmount > 0)
                {
                    conditions.Add("Loan_amount = " + HundiLoanInfo.PARAM_LOAN_AMOUNT);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_AMOUNT, loanAmount));
                }

                if (loanDate.Year > 1980)
                {
                    conditions.Add("Loan_date = " + HundiLoanInfo.PARAM_LOAN_DATE);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_DATE, loanDate));
                }

                if (closed != null)
                {
                    if (closed.Trim().Length > 0)
                    {
                        if (!closed.Equals(DBConstant.TYPE_ALL))
                        {
                            conditions.Add("Closed LIKE " + HundiLoanInfo.PARAM_CLOSED);
                            parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CLOSED, closed + "%"));
                        }
                    }
                }

                if (rate > 0)
                {
                    conditions.Add("Rate = " + HundiLoanInfo.PARAM_RATE);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_RATE, rate));
                }

                if (payMode > 0)
                {
                    conditions.Add("Pay_mode = " + HundiLoanInfo.PARAM_PAY_MODE);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_PAY_MODE, payMode));
                }

                if (chequeNO != null)
                {
                    if (chequeNO.Trim().Length > 0)
                    {
                        conditions.Add("Cheque_no LIKE " + HundiLoanInfo.PARAM_CHEQUE_NO);
                        parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CHEQUE_NO, chequeNO + "%"));
                    }
                }

                if (bankID > 0)
                {
                    conditions.Add("Bank_id = " + HundiLoanInfo.PARAM_BANK_ID);
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_BANK_ID, bankID));
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

        public static List<HundiLoanInfo> SearchHundiLoanInfo(SearchHelper searchHelper, int startPage)
        {
            List<HundiLoanInfo> hundiLoanInfos = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                string query = HundiLoanInfo.QUERY_SEARCH;
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
                    HundiLoanInfo hundiLoanInfo = new HundiLoanInfo();
                    hundiLoanInfo.ReadValues(reader);
                    hundiLoanInfo.Balance = hundiLoanInfo.LoanAmount - hundiLoanInfo.PaidAmount;

                    List<HundiInterestInfo> interests = GetHundiInterestInfos(hundiLoanInfo.HlLoanno);
                    foreach (HundiInterestInfo interest in interests)
                    {
                        hundiLoanInfo.InterestPaid = interest.InterestUpto;
                    }

                    LedgersInfo bank = LedgersDAO.GetLedgersInfo(hundiLoanInfo.BankID);
                    if (bank != null) hundiLoanInfo.BankName = bank.LedgerName;
                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(hundiLoanInfo.CustomerID);
                    CityInfo city = CityDAO.GetCityInfo(customer.ResVillage);
                    hundiLoanInfo.CustomerAddress = "S/D/H " + customer.SonHusband + ", " + city.VillageName + ", " + city.CityName + ", " + city.State + "-" + city.Pincode;

                    hundiLoanInfos.Add(hundiLoanInfo);
                }
                return hundiLoanInfos;
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

        public static int SearchHundiLoanInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = HundiLoanInfo.QUERY_COUNT;
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

        public static HundiLoanInfo GetHundiLoanInfo(string hlLoanno)
        {
            HundiLoanInfo hundiLoanInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_HL_LOANNO, hlLoanno));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiLoanInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    hundiLoanInfo = new HundiLoanInfo();
                    hundiLoanInfo.ReadValues(reader);
                    hundiLoanInfo.Balance = hundiLoanInfo.LoanAmount - hundiLoanInfo.PaidAmount;
                }
                return hundiLoanInfo;
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

        public static List<HundiLoanInfo> GetHundiLoanInfos()
        {
            List<HundiLoanInfo> hundiLoanInfos = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiLoanInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    HundiLoanInfo hundiLoanInfo = new HundiLoanInfo();
                    hundiLoanInfo.ReadValues(reader);
                    hundiLoanInfo.Balance = hundiLoanInfo.LoanAmount - hundiLoanInfo.PaidAmount;
                    hundiLoanInfos.Add(hundiLoanInfo);
                }
                return hundiLoanInfos;
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

        public static List<HundiLoanInfo> GetHundiLoanInfos(DateTime fromDate, DateTime toDate, string closed)
        {
            List<HundiLoanInfo> hundiLoanInfos = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CLOSED, closed));
                
                string query = "";
                if (closed.Equals(DBConstant.TYPE_CLOSED) || closed.Equals(DBConstant.TYPE_PENDING))
                    query = HundiLoanInfo.QUERY_REPORT_ON_CLOSED;
                else if (closed.Equals(DBConstant.TYPE_ALL))
                    query = HundiLoanInfo.QUERY_REPORT_ALL;

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    HundiLoanInfo hundiLoanInfo = new HundiLoanInfo();
                    hundiLoanInfo.ReadValues(reader);
                    hundiLoanInfo.Balance = hundiLoanInfo.LoanAmount - hundiLoanInfo.PaidAmount;

                    List<HundiInterestInfo> interests = GetHundiInterestInfos(hundiLoanInfo.HlLoanno);
                    foreach (HundiInterestInfo interest in interests)
                    {
                        hundiLoanInfo.InterestPaid = interest.InterestUpto;
                    }

                    CustomerInfo customer = CustomerDAO.GetCustomerInfo(hundiLoanInfo.CustomerID);
                    CityInfo city = CityDAO.GetCityInfo(customer.ResVillage);
                    hundiLoanInfo.CustomerAddress = "S/D/H " + customer.SonHusband + ", " + city.VillageName + ", " + city.CityName + ", " + city.State + "-" + city.Pincode;

                    hundiLoanInfos.Add(hundiLoanInfo);
                }
                return hundiLoanInfos;
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

        #endregion

        #region Hundi Trans Details
        public static List<HundiTransInfo> GetHundiTransInfos(string hlLoanno, decimal loanAmount)
        {
            List<HundiTransInfo> hundiTransInfos = new List<HundiTransInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_HL_LOANNO, hlLoanno));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiTransInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    HundiTransInfo hundiTransInfo = new HundiTransInfo();
                    hundiTransInfo.ReadValues(reader);
                    loanAmount = loanAmount - hundiTransInfo.Amount;
                    hundiTransInfo.Balance = loanAmount;
                    hundiTransInfos.Add(hundiTransInfo);
                }
                return hundiTransInfos;
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

        public static void AddHundiTransInfo(HundiTransInfo hundiTransInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiTransInfo.QUERY_INSERT, hundiTransInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiTransInfo.QUERY_UPDATE, hundiTransInfo.GetParameters());

                if (hundiTransInfo.Balance == 0)
                {
                    List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_HL_LOANNO, hundiTransInfo.HlLoanno));

                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiLoanInfo.QUERY_SET_CLOSED, parameters);
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

        public static void DeleteHundiTransInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiTransInfo.QUERY_DELETE, parameters);

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

        public static HundiTransInfo GetHundiTransInfo(int recordID)
        {
            HundiTransInfo hundiTransInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiTransInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    hundiTransInfo = new HundiTransInfo();
                    hundiTransInfo.ReadValues(reader);
                }
                return hundiTransInfo;
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
        #endregion

        #region Hundi Interest Details

        public static List<HundiInterestInfo> GetHundiInterestInfos(string hlLoanno)
        {
            List<HundiInterestInfo> hundiInterestInfos = new List<HundiInterestInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_HL_LOANNO, hlLoanno));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiInterestInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    HundiInterestInfo hundiInterestInfo = new HundiInterestInfo();
                    hundiInterestInfo.ReadValues(reader);

                    hundiInterestInfos.Add(hundiInterestInfo);
                }
                return hundiInterestInfos;
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

        public static void AddHundiInterestInfo(HundiInterestInfo hundiInterestInfo, decimal balance, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiInterestInfo.QUERY_INSERT, hundiInterestInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiInterestInfo.QUERY_UPDATE, hundiInterestInfo.GetParameters());

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

        public static void DeleteHundiInterestInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiInterestInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, HundiInterestInfo.QUERY_DELETE, parameters);

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
        #endregion

        #region Accounts

        public static DayBookInfo GetOpeningHundiLoans(DateTime toDate, string customerName, int type)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_DATE, toDate));

                    query = HundiLoanInfo.QUERY_SELECT_OPENING;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_CUSTOMER)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = HundiLoanInfo.QUERY_SELECT_OPENING_CUSTOMER;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_CASH)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_PAY_MODE, DBConstant.PAY_MODE_CASH));

                    query = HundiLoanInfo.QUERY_SELECT_OPENING_CASH;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_BANK)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_LOAN_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_PAY_MODE, DBConstant.PAY_MODE_CHEQUE));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = HundiLoanInfo.QUERY_SELECT_OPENING_BANK;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_HLOAN;
                    openingBalance.Debit = 0;
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

        public static List<HundiLoanInfo> GetHundiLoans(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<HundiLoanInfo> hundiLoans = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));

                    query = HundiLoanInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = HundiLoanInfo.QUERY_SELECT_LEDGER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    HundiLoanInfo hundiLoan = new HundiLoanInfo();
                    hundiLoan.ReadValues(reader);

                    LedgersInfo bank = LedgersDAO.GetLedgersInfo(hundiLoan.BankID);
                    if (bank != null) hundiLoan.BankName = bank.LedgerName;

                    hundiLoans.Add(hundiLoan);
                }
                return hundiLoans;
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
                    parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_PAID_DATE, toDate));

                    query = HundiTransInfo.QUERY_SELECT_OPENING;
                }
                else if (type == DBConstant.ACCOUNT_OPENING_CUSTOMER)
                {
                    parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_PAID_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = HundiTransInfo.QUERY_SELECT_OPENING_CUSTOMER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_HLRECEIPT;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader["Amount"]);
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

        public static List<HundiLoanInfo> GetTransDetails(DateTime fromDate, DateTime toDate, string customerName, int type)
        {
            List<HundiLoanInfo> hundiLoans = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                string query = "";

                if (type == DBConstant.ACCOUNT_PERIOD)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));

                    query = HundiTransInfo.QUERY_SELECT_PERIOD;
                }
                else if (type == DBConstant.ACCOUNT_LEDGER)
                {
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));
                    parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_CUSTOMER_NAME, customerName));

                    query = HundiTransInfo.QUERY_SELECT_LEDGER;
                }

                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    HundiLoanInfo hundiLoan = new HundiLoanInfo();
                    hundiLoan.HlLoanno = DBUtils.ConvertString(reader["Hl_loanno"]);
                    hundiLoan.LoanDate = DBUtils.ConvertDate(reader["Paid_date"]);
                    hundiLoan.AccountNo = DBUtils.ConvertString(reader["Receipt_no"]);
                    hundiLoan.LoanAmount = DBUtils.ConvertDecimal(reader["Amount"]);
                    hundiLoan.CustomerName = DBUtils.ConvertString(reader["customer_name"]);

                    hundiLoans.Add(hundiLoan);
                }
                return hundiLoans;
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

        public static DayBookInfo GetOpeningInterest(DateTime toDate)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiTransInfo.PARAM_PAID_DATE, toDate));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiInterestInfo.QUERY_SELECT_OPENING, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_HLINTEREST;
                    openingBalance.Debit = 0;
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

        public List<HundiLoanInfo> GetInterestPaidDetails(DateTime fromDate, DateTime toDate)
        {
            List<HundiLoanInfo> hundiLoans = new List<HundiLoanInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(HundiLoanInfo.PARAM_TO_DATE, toDate));

                reader = SQLHelper.ExecuteReader(CommandType.Text, HundiInterestInfo.QUERY_SELECT_PERIOD, parameters);
                while (reader.Read())
                {
                    HundiLoanInfo hundiLoan = new HundiLoanInfo();
                    hundiLoan.HlLoanno = DBUtils.ConvertString(reader["Hl_loanno"]);
                    hundiLoan.LoanDate = DBUtils.ConvertDate(reader["Paid_date"]);
                    hundiLoan.AccountNo = DBUtils.ConvertString(reader["Receipt_no"]);
                    hundiLoan.LoanAmount = DBUtils.ConvertDecimal(reader["Interest_amount"]);

                    hundiLoans.Add(hundiLoan);
                }
                return hundiLoans;
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