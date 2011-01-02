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
    class FixedInterestDAO
    {
        public static void AddFixedInterestInfo(FixedInterestInfo fixedInterestInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedInterestInfo.QUERY_INSERT, fixedInterestInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedInterestInfo.QUERY_UPDATE, fixedInterestInfo.GetParameters());

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

        public static void DeleteFixedInterestInfo(int recordID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_RECORD_ID, recordID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, FixedInterestInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(int recordID, string fDNO, DateTime paidDate, decimal interestAmount, string voucherNO, DateTime interestUpto)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();
                if (recordID > 0)
                {
                    conditions.Add("Record_ID = " + FixedInterestInfo.PARAM_RECORD_ID);
                    parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_RECORD_ID, recordID));
                }

                if (fDNO != null)
                {
                    if (fDNO.Trim().Length > 0)
                    {
                        conditions.Add("FD_No LIKE " + FixedInterestInfo.PARAM_FD_NO);
                        parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_FD_NO, fDNO + "%"));
                    }
                }

                if (paidDate != null)
                {
                    if (paidDate.Year > 1980)
                    {
                        conditions.Add("Paid_Date = " + FixedInterestInfo.PARAM_PAID_DATE);
                        parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_PAID_DATE, paidDate));
                    }
                }

                if (interestAmount > 0)
                {
                    conditions.Add("Interest_Amount = " + FixedInterestInfo.PARAM_INTEREST_AMOUNT);
                    parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_INTEREST_AMOUNT, interestAmount));
                }

                if (voucherNO != null)
                {
                    if (voucherNO.Trim().Length > 0)
                    {
                        conditions.Add("Voucher_No LIKE " + FixedInterestInfo.PARAM_VOUCHER_NO);
                        parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_VOUCHER_NO, voucherNO + "%"));
                    }
                }

                if (interestUpto != null)
                {
                    if (interestUpto.Year > 1980)
                    {
                        conditions.Add("Interest_Upto = " + FixedInterestInfo.PARAM_INTEREST_UPTO);
                        parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_INTEREST_UPTO, interestUpto));
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

        public static List<FixedInterestInfo> SearchFixedInterestInfo(SearchHelper searchHelper, int startPage)
        {
            List<FixedInterestInfo> fixedInterestInfos = new List<FixedInterestInfo>();
            IDataReader reader = null;
            try
            {
                string query = FixedInterestInfo.QUERY_SEARCH;
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
                    FixedInterestInfo fixedInterestInfo = new FixedInterestInfo();
                    fixedInterestInfo.ReadValues(reader);

                    fixedInterestInfos.Add(fixedInterestInfo);
                }
                return fixedInterestInfos;
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

        public static int SearchFixedInterestInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = FixedInterestInfo.QUERY_COUNT;
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

        public static FixedInterestInfo GetFixedInterestInfo(int recordID)
        {
            FixedInterestInfo fixedInterestInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_RECORD_ID, recordID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedInterestInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    fixedInterestInfo = new FixedInterestInfo();
                    fixedInterestInfo.ReadValues(reader);
                }
                return fixedInterestInfo;
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

        public static List<FixedInterestInfo> GetFixedInterestInfos(string fDNO)
        {
            List<FixedInterestInfo> fixedInterestInfos = new List<FixedInterestInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_FD_NO, fDNO));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedInterestInfo.QUERY_SELECT_ALL, parameters);
                while (reader.Read())
                {
                    FixedInterestInfo fixedInterestInfo = new FixedInterestInfo();
                    fixedInterestInfo.ReadValues(reader);

                    fixedInterestInfos.Add(fixedInterestInfo);
                }
                return fixedInterestInfos;
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

        public static DayBookInfo GetOpeningInterestpaid(DateTime toDate, string ledgerName, int type)
        {
            DayBookInfo openingBalance = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_PAID_DATE, toDate));
                string query = "";

                //if (type == DBConstant.ACCOUNT_OPENING_CASH)
                //{
                //    parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_PAY_MODE, DBConstant.PAY_MODE_CASH));
                //    query = FixedInterestInfo.QUERY_SELECT_OPENING_INTEREST_CASH;
                //}
                //else if (type == DBConstant.ACCOUNT_OPENING_BANK)
                //{
                //    parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_PAY_MODE, DBConstant.PAY_MODE_CHEQUE));
                //    parameters.Add(DBManager.GetParameter(FixedInterestInfo.PARAM_CUSTOMER_NAME, ledgerName));
                //    query = FixedInterestInfo.QUERY_SELECT_OPENING_INTEREST_BANK;
                //}
                //else
                //{
                //    query = FixedInterestInfo.QUERY_SELECT_OPENING_INTEREST;
                //}
                query = FixedInterestInfo.QUERY_SELECT_OPENING_INTEREST;
                reader = SQLHelper.ExecuteReader(CommandType.Text, query, parameters);
                while (reader.Read())
                {
                    openingBalance = new DayBookInfo();
                    openingBalance.Particulars = DBConstant.VOUCHER_FDINTEREST;
                    openingBalance.Debit = DBUtils.ConvertDecimal(reader["Amount"]);
                    openingBalance.Credit = 0;
                }
                return openingBalance;
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

        public static List<FixedInterestInfo> GetInterestPaidDetails(DateTime fromDate, DateTime toDate)
        {
            List<FixedInterestInfo> fixedInterests = new List<FixedInterestInfo>();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_FROM_DATE, fromDate));
                parameters.Add(DBManager.GetParameter(FixedDepositInfo.PARAM_TO_DATE, toDate));

                reader = SQLHelper.ExecuteReader(CommandType.Text, FixedInterestInfo.QUERY_SELECT_PERIOD_INTEREST, parameters);
                while (reader.Read())
                {
                    FixedInterestInfo fixedInterest = new FixedInterestInfo();
                    fixedInterest.ReadValues(reader);
                    //pigmyInterest.PigmyACNo = DBUtil.ConvertString(reader["pigmy_acno"]);
                    //pigmyInterest.PaidDate = DBUtil.ConvertDate(reader["paid_date"]);
                    //pigmyInterest.VocuherNo = DBUtil.ConvertString(reader["voucher_no"]);
                    //pigmyInterest.CustomerName = DBUtil.ConvertString(reader["customer_name"]);
                    //pigmyInterest.Interest = DBUtil.ConvertDecimal(reader["interest"]);
                    //pigmyInterest.Amount = DBUtil.ConvertDecimal(reader["amount"]);
                    //pigmyInterest.PayMode = DBUtil.ConvertInt(reader["pay_mode"]);
                    //pigmyInterest.ChequeNo = DBUtil.ConvertString(reader["cheque_no"]);
                    //pigmyInterest.BankID = DBUtil.ConvertInt(reader["bank_id"]);

                    //BankDAO bankDao = new BankDAO();
                    //BankInfo bank = bankDao.GetBank(pigmyInterest.BankID);
                    //if (bank != null) pigmyInterest.BankName = bank.BankName;

                    fixedInterests.Add(fixedInterest);
                }
                return fixedInterests;
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