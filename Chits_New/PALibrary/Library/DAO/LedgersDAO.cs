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
    class LedgersDAO
    {
        public static string GetNextLedgerID()
        {
            string nextLedgerID = "";
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, LedgersInfo.QUERY_NEXT_AUTO_NUMBER, null);
                while (reader.Read())
                {
                    nextLedgerID = DBUtils.ConvertString(reader["AUTO_INCREMENT"]);
                }
                return nextLedgerID;
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

        public static void AddLedgersInfo(LedgersInfo ledgersInfo, int mode)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                if (mode == DBConstant.MODE_ADD)
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, LedgersInfo.QUERY_INSERT, ledgersInfo.GetParameters());
                else
                    SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, LedgersInfo.QUERY_UPDATE, ledgersInfo.GetParameters());

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

        public static void DeleteLedgersInfo(int ledgerID)
        {
            IDbConnection connection = null;
            try
            {
                connection = DBManager.GetConnection();
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_LEDGER_ID, ledgerID));

                SQLHelper.ExecuteNonQuery(transaction, CommandType.Text, LedgersInfo.QUERY_DELETE, parameters);

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

        public static SearchHelper SearchConditions(string ledgerName, string balanceType, int groupID)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                List<string> conditions = new List<string>();

                if (ledgerName != null)
                {
                    if (ledgerName.Trim().Length > 0)
                    {
                        conditions.Add("Ledger_Name LIKE " + LedgersInfo.PARAM_LEDGER_NAME);
                        parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_LEDGER_NAME, ledgerName + "%"));
                    }
                }

                if (balanceType != null)
                {
                    if (balanceType.Trim().Length > 0)
                    {
                        conditions.Add("Balance_Type LIKE " + LedgersInfo.PARAM_BALANCE_TYPE);
                        parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_BALANCE_TYPE, balanceType + "%"));
                    }
                }

                if (groupID > 0)
                {
                    conditions.Add("Group_ID = " + LedgersInfo.PARAM_GROUP_ID);
                    parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_GROUP_ID, groupID));
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

        public static List<LedgersInfo> SearchLedgersInfo(SearchHelper searchHelper, int startPage)
        {
            List<LedgersInfo> ledgersInfos = new List<LedgersInfo>();
            IDataReader reader = null;
            try
            {
                string query = LedgersInfo.QUERY_SEARCH;
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
                    LedgersInfo ledgersInfo = new LedgersInfo();
                    ledgersInfo.ReadValues(reader);
                    GroupsInfo group = AccountsDAO.GetGroupName(ledgersInfo.GroupID);
                    if (group != null)
                    {
                        ledgersInfo.GroupName = group.GroupName;
                        ledgersInfo.MainGroup = group.MainGroup;
                        ledgersInfo.SubGroupName = group.SubGroup;
                    }

                    ledgersInfos.Add(ledgersInfo);
                }
                return ledgersInfos;
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

        public static int SearchLedgersInfoCount(SearchHelper searchHelper)
        {
            try
            {
                string query = LedgersInfo.QUERY_COUNT;
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

        public static LedgersInfo GetLedgersInfo(int ledgerID)
        {
            LedgersInfo ledgersInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_LEDGER_ID, ledgerID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, LedgersInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    ledgersInfo = new LedgersInfo();
                    ledgersInfo.ReadValues(reader);
                    GroupsInfo group = AccountsDAO.GetGroupName(ledgersInfo.GroupID);
                    if (group != null)
                    {
                        ledgersInfo.GroupName = group.GroupName;
                        ledgersInfo.MainGroup = group.MainGroup;
                        ledgersInfo.SubGroupName = group.SubGroup;
                    }
                }
                return ledgersInfo;
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

        public static LedgersInfo GetLedgersInfo(string ledgerName)
        {
            LedgersInfo ledgersInfo = null;
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(LedgersInfo.PARAM_LEDGER_NAME, ledgerName));

                reader = SQLHelper.ExecuteReader(CommandType.Text, LedgersInfo.QUERY_SELECT_NAME, parameters);
                while (reader.Read())
                {
                    ledgersInfo = new LedgersInfo();
                    ledgersInfo.ReadValues(reader);
                    GroupsInfo group = AccountsDAO.GetGroupName(ledgersInfo.GroupID);
                    if (group != null)
                    {
                        ledgersInfo.GroupName = group.GroupName;
                        ledgersInfo.MainGroup = group.MainGroup;
                        ledgersInfo.SubGroupName = group.SubGroup;
                    }
                }
                return ledgersInfo;
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

        public static List<LedgersInfo> GetLedgersInfos()
        {
            List<LedgersInfo> ledgersInfos = new List<LedgersInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, LedgersInfo.QUERY_SELECT_ALL, null);
                while (reader.Read())
                {
                    LedgersInfo ledgersInfo = new LedgersInfo();
                    ledgersInfo.ReadValues(reader);
                    GroupsInfo group = AccountsDAO.GetGroupName(ledgersInfo.GroupID);
                    if (group != null)
                    {
                        ledgersInfo.GroupName = group.GroupName;
                        ledgersInfo.MainGroup = group.MainGroup;
                        ledgersInfo.SubGroupName = group.SubGroup;
                    }

                    ledgersInfos.Add(ledgersInfo);
                }
                return ledgersInfos;
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

        public static List<DayBookInfo> GetHundiLoanLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //Hundi Loan
            List<HundiLoanInfo> hundiLoans = HundiLoanDAO.GetHundiLoans(fromDate, toDate, ledgerName, type);
            foreach (HundiLoanInfo hundiLoan in hundiLoans)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = hundiLoan.LoanDate;
                dayBook.Particulars = hundiLoan.CustomerName;
                dayBook.VoucherType = DBConstant.VOUCHER_HLOAN;
                dayBook.VoucherNo = Convert.ToInt32(hundiLoan.HlLoanno.Replace("HL", ""));
                dayBook.Debit = 0;
                dayBook.Credit = hundiLoan.LoanAmount;
                if (hundiLoan.PayMode == DBConstant.PAY_MODE_CASH)
                {
                    dayBook.FromLedger = DBConstant.CASH_LEDGER;
                    dayBook.Narration = DBConstant.CASH_LEDGER;
                }
                else if (hundiLoan.PayMode == DBConstant.PAY_MODE_CHEQUE)
                {
                    dayBook.FromLedger = hundiLoan.BankName;
                    dayBook.Narration = "Ch.No. " + hundiLoan.ChequeNO;
                }
                dayBook.ToLedger = hundiLoan.CustomerName;

                dayBooks.Add(dayBook);
            }

            //Hundi Trans
            List<HundiLoanInfo> hundiTrans = HundiLoanDAO.GetTransDetails(fromDate, toDate, ledgerName, type);
            foreach (HundiLoanInfo hundiLoan in hundiTrans)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = hundiLoan.LoanDate;
                dayBook.Particulars = hundiLoan.CustomerName;
                dayBook.VoucherType = DBConstant.VOUCHER_HLRECEIPT;
                dayBook.VoucherNo = Convert.ToInt32(hundiLoan.AccountNo);
                dayBook.Debit = hundiLoan.LoanAmount;
                dayBook.Credit = 0;
                dayBook.Narration = hundiLoan.HlLoanno;
                dayBook.FromLedger = hundiLoan.CustomerName;
                dayBook.ToLedger = DBConstant.CASH_LEDGER;

                dayBooks.Add(dayBook);
            }

            return dayBooks;
        }

        public static List<DayBookInfo> GetFixedDespositLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //Fixed Deposit
            List<FixedDepositInfo> fixedDeposits = FixedDepositDAO.GetFixedDeposits(fromDate, toDate, ledgerName, type);
            foreach (FixedDepositInfo fixedDeposit in fixedDeposits)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = fixedDeposit.StartDate;
                dayBook.Particulars = fixedDeposit.CustomerName;
                dayBook.VoucherType = DBConstant.VOUCHER_FIXEDDESPOSIT;
                dayBook.VoucherNo = Convert.ToInt32(fixedDeposit.FDNO.Replace("FD", ""));
                dayBook.Debit = fixedDeposit.Amount;
                dayBook.Credit = 0;
                //if (fixedDeposit.PayMode == DBConstant.PAY_MODE_CASH)
                //{
                dayBook.FromLedger = DBConstant.CASH_LEDGER;
                dayBook.Narration = DBConstant.CASH_LEDGER;
                //}
                //else if (fixedDeposit.PayMode == DBConstant.PAY_MODE_CHEQUE)
                //{
                //    dayBook.FromLedger = fixedDeposit.BankName;
                //    dayBook.Narration = "Ch.No. " + fixedDeposit.ChequeNO;
                //}
                dayBook.ToLedger = fixedDeposit.CustomerName;

                dayBooks.Add(dayBook);
            }

            //Fixed Trans
            List<FixedDepositInfo> fixedTrans = FixedTransDAO.GetTransDetails(fromDate, toDate, ledgerName, type);
            foreach (FixedDepositInfo fixedTran in fixedTrans)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = fixedTran.StartDate;
                dayBook.Particulars = fixedTran.CustomerName;
                dayBook.VoucherType = DBConstant.VOUCHER_FDPAYMENT;
                dayBook.VoucherNo = Convert.ToInt32(fixedTran.AccountNo);
                dayBook.Debit = 0;
                dayBook.Credit = fixedTran.Amount;
                dayBook.Narration = fixedTran.FDNO;
                dayBook.FromLedger = fixedTran.CustomerName;
                dayBook.ToLedger = DBConstant.CASH_LEDGER;

                dayBooks.Add(dayBook);
            }

            return dayBooks;
        }

        public static List<DayBookInfo> GetInterestLedger(DateTime fromDate, DateTime toDate)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //Hundi Interest
            HundiLoanDAO hundiDao = new HundiLoanDAO();
            List<HundiLoanInfo> hundiInterests = hundiDao.GetInterestPaidDetails(fromDate, toDate);
            foreach (HundiLoanInfo hundiLoan in hundiInterests)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = hundiLoan.LoanDate;
                dayBook.Particulars = DBConstant.INTEREST_LEDGER;
                dayBook.VoucherType = DBConstant.VOUCHER_HLINTEREST;
                dayBook.VoucherNo = Convert.ToInt32(hundiLoan.AccountNo);
                dayBook.Debit = hundiLoan.LoanAmount;
                dayBook.Credit = 0;
                dayBook.Narration = hundiLoan.HlLoanno;
                dayBook.FromLedger = DBConstant.INTEREST_LEDGER;
                dayBook.ToLedger = DBConstant.CASH_LEDGER;

                dayBooks.Add(dayBook);
            }
            return dayBooks;
        }

        public static List<DayBookInfo> GetInterestPaidLedger(DateTime fromDate, DateTime toDate)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //Fixed Interest
            List<FixedInterestInfo> fixedInterests = FixedInterestDAO.GetInterestPaidDetails(fromDate, toDate);
            foreach (FixedInterestInfo fixedInterest in fixedInterests)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = fixedInterest.PaidDate;
                dayBook.Particulars = DBConstant.INTEREST_PAID_LEDGER;
                dayBook.VoucherType = DBConstant.VOUCHER_FDINTEREST;
                dayBook.VoucherNo = Convert.ToInt32(fixedInterest.VoucherNO);
                dayBook.Debit = 0;
                dayBook.Credit = fixedInterest.InterestAmount;
                dayBook.Narration = fixedInterest.FDNO;
                //if (pigmyIntrest.PayMode == DBConstant.PAY_MODE_CASH)
                //{
                dayBook.FromLedger = DBConstant.CASH_LEDGER;
                dayBook.Narration = DBConstant.CASH_LEDGER;
                //}
                //else if (pigmyIntrest.PayMode == DBConstant.PAY_MODE_CHEQUE)
                //{
                //    dayBook.FromLedger = pigmyIntrest.BankName;
                //    dayBook.Narration = "Ch.No. " + pigmyIntrest.ChequeNo;
                //}

                dayBook.ToLedger = DBConstant.INTEREST_PAID_LEDGER;

                dayBooks.Add(dayBook);

            }
            return dayBooks;
        }

        public static List<DayBookInfo> GetATKTLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //ATKT
            List<ATKTInfo> ATKTinfos = ATKTDAO.GetATKTs(fromDate, toDate, ledgerName, type);
            foreach (ATKTInfo ATKTinfo in ATKTinfos)
            {
                if (ATKTinfo.ATKTDate >= fromDate && ATKTinfo.ATKTDate <= toDate)
                {
                    dayBook = new DayBookInfo();
                    dayBook.CurrentDate = ATKTinfo.ATKTDate;
                    dayBook.Debit = 0;
                    dayBook.Credit = 0;

                    dayBook.VoucherNo = Convert.ToInt32(ATKTinfo.ATKTNO.Replace("AK", ""));
                    if (ATKTinfo.TranType == DBConstant.ATKT_PAY)
                    {
                        dayBook.Particulars = DBConstant.CASH_LEDGER + " - " + ATKTinfo.PartyName;
                        dayBook.VoucherType = DBConstant.VOUCHER_ATKTPAY;
                        dayBook.Credit = ATKTinfo.Amount;

                        dayBook.FromLedger = DBConstant.CASH_LEDGER;
                        dayBook.ToLedger = ATKTinfo.PartyName;
                        dayBook.Narration = ATKTinfo.PartyName + " - " + ATKTinfo.ATKTNO;
                    }
                    else if (ATKTinfo.TranType == DBConstant.ATKT_RECP)
                    {
                        dayBook.Particulars = ATKTinfo.PartyName + " - " + DBConstant.CASH_LEDGER;
                        dayBook.VoucherType = DBConstant.VOUCHER_ATKTRECP;
                        dayBook.Debit = ATKTinfo.Amount;

                        dayBook.FromLedger = ATKTinfo.PartyName;
                        dayBook.ToLedger = DBConstant.CASH_LEDGER;
                        dayBook.Narration = DBConstant.CASH_LEDGER + " - " + ATKTinfo.ATKTNO;
                    }
                    dayBooks.Add(dayBook);
                }

                if (ATKTinfo.Closed == DBConstant.TYPE_CLOSED)
                {
                    if (ATKTinfo.ClosedDate >= fromDate && ATKTinfo.ClosedDate <= toDate)
                    {
                        dayBook = new DayBookInfo();
                        dayBook.CurrentDate = ATKTinfo.ClosedDate;
                        dayBook.Debit = 0;
                        dayBook.Credit = 0;

                        dayBook.VoucherNo = Convert.ToInt32(ATKTinfo.ATKTNO.Replace("AK", ""));
                        if (ATKTinfo.TranType == DBConstant.ATKT_RECP)
                        {
                            dayBook.Particulars = DBConstant.CASH_LEDGER + " - " + ATKTinfo.PartyName;
                            dayBook.VoucherType = DBConstant.VOUCHER_ATKTPAY;
                            dayBook.Credit = ATKTinfo.Amount;

                            dayBook.FromLedger = DBConstant.CASH_LEDGER;
                            dayBook.ToLedger = ATKTinfo.PartyName;
                            dayBook.Narration = ATKTinfo.PartyName + " - " + ATKTinfo.ATKTNO;
                        }
                        else if (ATKTinfo.TranType == DBConstant.ATKT_PAY)
                        {
                            dayBook.Particulars = ATKTinfo.PartyName + " - " + DBConstant.CASH_LEDGER;
                            dayBook.VoucherType = DBConstant.VOUCHER_ATKTRECP;
                            dayBook.Debit = ATKTinfo.Amount;

                            dayBook.FromLedger = ATKTinfo.PartyName;
                            dayBook.ToLedger = DBConstant.CASH_LEDGER;
                            dayBook.Narration = DBConstant.CASH_LEDGER + " - " + ATKTinfo.ATKTNO;
                        }
                        dayBooks.Add(dayBook);
                    }
                }
            }

            return dayBooks;
        }

        public static List<DayBookInfo> GetChitLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook = null;

            //Chit Trans
            List<ChitsTransInfo> trans = ChitsTransDAO.GetChitTrans(fromDate, toDate, ledgerName, type);
            foreach (ChitsTransInfo tran in trans)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = tran.Date;
                dayBook.Particulars = tran.ChitNO;
                dayBook.VoucherType = DBConstant.CHITS_INSTALLMENT;
                dayBook.VoucherNo = tran.RecordID;
                dayBook.Debit = tran.InstallmentAmount;
                dayBook.Credit = 0;
                dayBook.Narration = tran.ChitNO;
                dayBook.FromLedger = DBConstant.CASH_LEDGER;
                dayBook.ToLedger = tran.ChitNO;

                dayBooks.Add(dayBook);
            }

            //Chits Bids
            List<ChitsBiddingInfo> bids = ChitsBiddingDAO.GetChitBids(fromDate, toDate, ledgerName, type);
            foreach (ChitsBiddingInfo bid in bids)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = bid.PaidDate;
                dayBook.Particulars = bid.ChitNO;
                dayBook.VoucherType = DBConstant.CHITS_BIDDING;
                dayBook.VoucherNo = bid.RecordID;
                dayBook.Debit = 0;
                dayBook.Credit = bid.PaidAmount;
                dayBook.Narration = bid.ChitNO;
                dayBook.FromLedger = bid.ChitNO;
                dayBook.ToLedger = DBConstant.CASH_LEDGER;

                dayBooks.Add(dayBook);
            }

            return dayBooks;
        }

        #endregion
    }
}