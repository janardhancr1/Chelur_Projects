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
    class AccountsDAO
    {
        public static List<GroupsInfo> GetGroups()
        {
            List<GroupsInfo> groups = new List<GroupsInfo>();
            IDataReader reader = null;
            try
            {

                reader = SQLHelper.ExecuteReader(CommandType.Text, GroupsInfo.QUERY_SEARCH, null);
                while (reader.Read())
                {
                    GroupsInfo group = new GroupsInfo();
                    group.ReadValues(reader);

                    groups.Add(group);
                }
                return groups;
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

        public static GroupsInfo GetGroupName(int groupID)
        {
            GroupsInfo group = new GroupsInfo();
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(GroupsInfo.PARAM_GROUP_ID, groupID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, GroupsInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    group.ReadValues(reader);
                }
                return group;
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

        public static List<VoucherTypesInfo> GetVoucherTypes()
        {
            List<VoucherTypesInfo> voucherTypes = new List<VoucherTypesInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, VoucherTypesInfo.QUERY_SEARCH, null);
                while (reader.Read())
                {
                    VoucherTypesInfo voucherType = new VoucherTypesInfo();
                    voucherType.ReadValues(reader);

                    voucherTypes.Add(voucherType);
                }
                return voucherTypes;
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

        public static string GetVoucherTypeName(int voucherTypeID)
        {
            VoucherTypesInfo voucherType = new VoucherTypesInfo();
            voucherType.VoucherTypeName = "";
            IDataReader reader = null;
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VoucherTypesInfo.PARAM_VOUCHERTYPE_ID, voucherTypeID));

                reader = SQLHelper.ExecuteReader(CommandType.Text, VoucherTypesInfo.QUERY_SELECT, parameters);
                while (reader.Read())
                {
                    voucherType.ReadValues(reader);
                }
                return voucherType.VoucherTypeName;
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

        public static int NextVoucherNo(DateTime voucherDate, int voucherType)
        {
            try
            {
                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_DATE, voucherDate));
                parameters.Add(DBManager.GetParameter(VouchersInfo.PARAM_VOUCHER_TYPE, voucherType));

                int voucherNo = DBUtils.ConvertInt(SQLHelper.ExecuteScalar(CommandType.Text, VouchersInfo.QUERY_NEXT_VOUCHER_NO, parameters));
                voucherNo++;
                return voucherNo;
            }
            catch (PAException ex)
            {
                throw new PAException(ex.Message);
            }
        }

        public static List<DayBookInfo> GetVoucherDetails(DateTime fromDate, DateTime toDate, int ledgerID)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();
            DayBookInfo dayBook;

            List<VouchersInfo> vouchers = VouchersDAO.GetVouchers(fromDate, toDate, ledgerID);
            foreach (VouchersInfo voucher in vouchers)
            {
                dayBook = new DayBookInfo();
                dayBook.CurrentDate = voucher.VoucherDate;
                dayBook.VoucherType = voucher.VoucherTypeName;
                dayBook.VoucherNo = voucher.VoucherNO;
                if (voucher.VoucherType.Equals(DBConstant.VOUCHER_PAYMENT))
                {
                    dayBook.Particulars = voucher.FromLedgerName;
                    dayBook.Debit = 0;
                    dayBook.Credit = voucher.Amount;
                }
                else if (voucher.VoucherType.Equals(DBConstant.VOUCHER_RECEIPT))
                {
                    dayBook.Particulars = voucher.FromLedgerName;
                    dayBook.Debit = voucher.Amount;
                    dayBook.Credit = 0;
                }
                else if (voucher.VoucherType.Equals(DBConstant.VOUCHER_CONTRA))
                {
                    if (LedgersDAO.GetLedgersInfo(voucher.FromLedger).LedgerName.Equals(DBConstant.CASH_LEDGER))
                    {
                        dayBook.Particulars = voucher.ToLedgerName;
                        dayBook.Debit = 0;
                        dayBook.Credit = voucher.Amount;
                    }
                    else if (LedgersDAO.GetLedgersInfo(voucher.FromLedger).GroupName.Equals(DBConstant.BANK_LEDGERS) && 
                        LedgersDAO.GetLedgersInfo(voucher.ToLedger).GroupName.Equals(DBConstant.BANK_LEDGERS) && 
                        LedgersDAO.GetLedgersInfo(voucher.ToLedger).LedgerID == ledgerID)
                    {
                        dayBook.Particulars = voucher.FromLedgerName;
                        dayBook.Debit = 0;
                        dayBook.Credit = voucher.Amount;
                    }
                    else
                    {
                        dayBook.Particulars = voucher.FromLedgerName;
                        dayBook.Debit = voucher.Amount;
                        dayBook.Credit = 0;
                    }
                }
                else
                {
                    dayBook.Particulars = voucher.FromLedgerName;
                    dayBook.Debit = 0;
                    dayBook.Credit = voucher.Amount;
                }
                dayBook.FromLedger = voucher.FromLedgerName;
                dayBook.ToLedger = voucher.ToLedgerName;
                dayBook.Narration = voucher.Narration;

                dayBooks.Add(dayBook);
            }

            return dayBooks;
        }

        public static DayBookInfo GetCashBookOpeningBalance(DateTime toDate)
        {
            decimal credit = 0;
            decimal debit = 0;

            LedgersInfo ledger = LedgersDAO.GetLedgersInfo(DBConstant.CASH_LEDGER);
            if (ledger != null)
            {
                if (ledger.BalanceType.Equals("Cr"))
                    credit = credit + ledger.OpeningBalance;
                else if (ledger.BalanceType.Equals("Dr"))
                    debit = debit + ledger.OpeningBalance;

                DayBookInfo voucherOpening = VouchersDAO.GetOpeningVoucher(toDate, ledger.LedgerID);
                if (voucherOpening != null)
                {
                    credit = credit + voucherOpening.Credit;
                    debit = debit + voucherOpening.Debit;
                }
            }

            DayBookInfo hlOpening = GetHundiLoanOpeningBalance(toDate, DBConstant.CASH_LEDGER, DBConstant.ACCOUNT_OPENING_CASH);
            if (hlOpening != null)
            {
                credit = credit + hlOpening.Credit;
                debit = debit + hlOpening.Debit;
            }

            DayBookInfo fdOpening = GetFixedDepositOpeningBalance(toDate, DBConstant.CASH_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (fdOpening != null)
            {
                credit = credit + fdOpening.Credit;
                debit = debit + fdOpening.Debit;
            }

            DayBookInfo atktOpening = GetATKTOpeningBalance(toDate, DBConstant.CASH_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (atktOpening != null)
            {
                credit = credit + atktOpening.Credit;
                debit = debit + atktOpening.Debit;
            }

            DayBookInfo interestOpening = GetInterestOpeningBalance(toDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING_CASH);
            if (interestOpening != null)
            {
                debit = debit + interestOpening.Credit;
                credit = credit + interestOpening.Debit;
            }

            DayBookInfo interestPaidOpening = GetInterestPaidOpeningBalance(toDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING_CASH);
            if (interestPaidOpening != null)
            {
                debit = debit + interestPaidOpening.Credit;
                credit = credit + interestPaidOpening.Debit;
            }

            DayBookInfo dayBook = null;
            if (credit > debit)
            {
                dayBook = new DayBookInfo();
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook = new DayBookInfo();
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetBankBookOpeningBalance(DateTime toDate, int ledgerID)
        {
            decimal credit = 0;
            decimal debit = 0;

            LedgersInfo ledger = LedgersDAO.GetLedgersInfo(ledgerID);
            if (ledger != null)
            {
                if (ledger.BalanceType.Equals("Cr"))
                    credit = credit + ledger.OpeningBalance;
                else if (ledger.BalanceType.Equals("Dr"))
                    debit = debit + ledger.OpeningBalance;

                DayBookInfo voucherOpening = VouchersDAO.GetOpeningVoucher(toDate, ledger.LedgerID);
                if (voucherOpening != null)
                {
                    credit = credit + voucherOpening.Credit;
                    debit = debit + voucherOpening.Debit;
                }
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static List<DayBookInfo> GetExpenses(DateTime toDate)
        {
            List<DayBookInfo> expenses = new List<DayBookInfo>();
            decimal direct = 0;
            decimal indirect = 0;
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {
                if (ledger.GroupName.Equals("DIRECT EXPENSES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        direct = direct + openingBalance.Debit;
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        direct = direct + openingBalance.Credit;
                    }
                }
                else if (ledger.GroupName.Equals("INDIRECT EXPENSES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        indirect = indirect + openingBalance.Debit;
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        indirect = indirect + openingBalance.Credit;
                    }
                }
            }

            DayBookInfo interestPaids = GetInterestPaidOpeningBalance(toDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (interestPaids.Debit > 0)
            {
                indirect = indirect + interestPaids.Debit;
            }
            else if (interestPaids.Credit > 0)
            {
                indirect = indirect + interestPaids.Credit;
            }

            if (direct > 0)
            {
                DayBookInfo directExpense = new DayBookInfo();
                directExpense.Particulars = "DIRECT EXPENSES";
                directExpense.Debit = direct;
                expenses.Add(directExpense);
            }
            if (indirect > 0)
            {
                DayBookInfo indirectExpense = new DayBookInfo();
                indirectExpense.Particulars = "INDIRECT EXPENSES";
                indirectExpense.Debit = indirect;
                expenses.Add(indirectExpense);
            }

            return expenses;
        }

        public static List<DayBookInfo> GetIncomes(DateTime toDate)
        {
            List<DayBookInfo> incomes = new List<DayBookInfo>();
            decimal direct = 0;
            decimal indirect = 0;
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {

                if (ledger.GroupName.Equals("DIRECT INCOMES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        direct = direct + openingBalance.Debit;
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        direct = direct + openingBalance.Credit;
                    }
                }
                else if (ledger.GroupName.Equals("INDIRECT INCOMES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        indirect = indirect + openingBalance.Debit;
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        indirect = indirect + openingBalance.Credit;
                    }
                }
            }

            DayBookInfo interests = GetInterestOpeningBalance(toDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (interests.Debit > 0)
            {
                indirect = indirect + interests.Debit;
            }
            else if (interests.Credit > 0)
            {
                indirect = indirect + interests.Credit;
            }

            if (direct > 0)
            {
                DayBookInfo directIncome = new DayBookInfo();
                directIncome.Particulars = "DIRECT INCOMES";
                directIncome.Debit = direct;
                incomes.Add(directIncome);
            }
            if (indirect > 0)
            {
                DayBookInfo indirectIncome = new DayBookInfo();
                indirectIncome.Particulars = "INDIRECT INCOMES";
                indirectIncome.Debit = indirect;
                incomes.Add(indirectIncome);
            }

            return incomes;
        }

        public static List<DayBookInfo> GetExpensesDetails(DateTime toDate)
        {
            List<DayBookInfo> expenses = new List<DayBookInfo>();
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {
                if (ledger.GroupName.Equals("DIRECT EXPENSES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        DayBookInfo directExpense = new DayBookInfo();
                        directExpense.Particulars = ledger.LedgerName;
                        directExpense.Debit = openingBalance.Debit;
                        expenses.Add(directExpense);
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        DayBookInfo directExpense = new DayBookInfo();
                        directExpense.Particulars = ledger.LedgerName;
                        directExpense.Debit = openingBalance.Credit;
                        expenses.Add(directExpense);
                    }
                }
                else if (ledger.GroupName.Equals("INDIRECT EXPENSES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        DayBookInfo indirectExpense = new DayBookInfo();
                        indirectExpense.Particulars = ledger.LedgerName;
                        indirectExpense.Debit = openingBalance.Debit;
                        expenses.Add(indirectExpense);
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        DayBookInfo indirectExpense = new DayBookInfo();
                        indirectExpense.Particulars = ledger.LedgerName;
                        indirectExpense.Debit = openingBalance.Credit;
                        expenses.Add(indirectExpense);
                    }
                }
            }

            DayBookInfo interestPaids = GetInterestPaidOpeningBalance(toDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (interestPaids.Debit > 0)
            {
                DayBookInfo indirectExpense = new DayBookInfo();
                indirectExpense.Particulars = DBConstant.INTEREST_PAID_LEDGER;
                indirectExpense.Debit = interestPaids.Debit;
                expenses.Add(indirectExpense);
            }
            else if (interestPaids.Credit > 0)
            {
                DayBookInfo indirectExpense = new DayBookInfo();
                indirectExpense.Particulars = DBConstant.INTEREST_PAID_LEDGER;
                indirectExpense.Debit = interestPaids.Credit;
                expenses.Add(indirectExpense);
            }

            return expenses;
        }

        public static List<DayBookInfo> GetIncomesDetails(DateTime toDate)
        {
            List<DayBookInfo> incomes = new List<DayBookInfo>();
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {

                if (ledger.GroupName.Equals("DIRECT INCOMES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        DayBookInfo directIncome = new DayBookInfo();
                        directIncome.Particulars = ledger.LedgerName;
                        directIncome.Debit = openingBalance.Debit;
                        incomes.Add(directIncome);
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        DayBookInfo directIncome = new DayBookInfo();
                        directIncome.Particulars = ledger.LedgerName;
                        directIncome.Debit = openingBalance.Credit;
                        incomes.Add(directIncome);
                    }
                }
                else if (ledger.GroupName.Equals("INDIRECT INCOMES"))
                {
                    DayBookInfo openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Debit > 0)
                    {
                        DayBookInfo indirectIncome = new DayBookInfo();
                        indirectIncome.Particulars = ledger.LedgerName;
                        indirectIncome.Debit = openingBalance.Debit;
                        incomes.Add(indirectIncome);
                    }
                    else if (openingBalance.Credit > 0)
                    {
                        DayBookInfo indirectIncome = new DayBookInfo();
                        indirectIncome.Particulars = ledger.LedgerName;
                        indirectIncome.Debit = openingBalance.Credit;
                        incomes.Add(indirectIncome);
                    }
                }
            }

            DayBookInfo interests = GetInterestOpeningBalance(toDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
            if (interests.Debit > 0)
            {
                DayBookInfo indirectIncome = new DayBookInfo();
                indirectIncome.Particulars = DBConstant.INTEREST_LEDGER;
                indirectIncome.Debit = interests.Debit;
                incomes.Add(indirectIncome);
            }
            else if (interests.Credit > 0)
            {
                DayBookInfo indirectIncome = new DayBookInfo();
                indirectIncome.Particulars = DBConstant.INTEREST_LEDGER;
                indirectIncome.Debit = interests.Credit;
                incomes.Add(indirectIncome);
            }

            return incomes;
        }

        public static List<DayBookInfo> GetTrailBalanceGroups()
        {
            List<DayBookInfo> trialGroups = new List<DayBookInfo>();
            IDataReader reader = null;
            try
            {
                reader = SQLHelper.ExecuteReader(CommandType.Text, GroupsInfo.QUERY_TB_GROUPS, null);
                while (reader.Read())
                {
                    DayBookInfo group = new DayBookInfo();
                    group.Particulars = DBUtils.ConvertString(reader["Sub_Group"]);
                    group.Narration = DBUtils.ConvertString(reader["Cr_Dr"]);
                    group.FromLedger = DBUtils.ConvertString(reader["Main_Group"]);

                    trialGroups.Add(group);
                }
                return trialGroups;
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

        public static List<DayBookInfo> GetTrailBalance(DateTime toDate)
        {
            List<DayBookInfo> trialBalance = GetTrailBalanceGroups();
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {
                if (ledger.LedgerName != DBConstant.INTEREST_LEDGER && ledger.LedgerName != DBConstant.INTEREST_PAID_LEDGER)
                {
                    DayBookInfo ledgerBalance = null;
                    if (ledger.LedgerName == DBConstant.CASH_LEDGER)
                        ledgerBalance = GetCashBookOpeningBalance(toDate);
                    else
                        ledgerBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    foreach (DayBookInfo tb in trialBalance)
                    {
                        if (tb.Particulars.Equals(ledger.SubGroupName))
                        {
                            tb.Credit = tb.Credit + ledgerBalance.Credit;
                            tb.Debit = tb.Debit + ledgerBalance.Debit;
                            break;
                        }
                    }
                }
            }

            DayBookInfo hlLoan = GetHundiLoanOpeningBalance(toDate, "HL", DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Particulars.Equals("CURRENT ASSETS"))
                {
                    tb.Credit = tb.Credit + hlLoan.Debit;
                    tb.Debit = tb.Debit + hlLoan.Credit;
                    break;
                }
            }

            DayBookInfo fixedDesposit = GetFixedDepositOpeningBalance(toDate, "FD", DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Particulars.Equals("CURRENT ASSETS"))
                {
                    tb.Credit = tb.Credit + fixedDesposit.Debit;
                    tb.Debit = tb.Debit + fixedDesposit.Credit;
                    break;
                }
            }

            DayBookInfo atktOpening = GetATKTOpeningBalance(toDate, "ATKT", DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Particulars.Equals("CURRENT ASSETS"))
                {
                    tb.Credit = tb.Credit + atktOpening.Debit;
                    tb.Debit = tb.Debit + atktOpening.Credit;
                    break;
                }
            }

            DayBookInfo interests = GetInterestOpeningBalance(toDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Particulars.Equals("INDIRECT INCOMES"))
                {
                    tb.Credit = tb.Credit + interests.Credit;
                    tb.Debit = tb.Debit + interests.Debit;
                    break;
                }
            }

            DayBookInfo interestPaids = GetInterestPaidOpeningBalance(toDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Particulars.Equals("INDIRECT EXPENSES"))
                {
                    tb.Credit = tb.Credit + interestPaids.Credit;
                    tb.Debit = tb.Debit + interestPaids.Debit;
                    break;
                }
            }

            decimal debit = 0;
            decimal credit = 0;

            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Debit > 0 || tb.Credit > 0)
                {
                    debit = debit + tb.Debit;
                    credit = credit + tb.Credit;
                    if (tb.Debit > tb.Credit)
                    {
                        tb.Debit = tb.Debit - tb.Credit;
                        tb.Credit = 0;
                    }
                    else if (tb.Credit > tb.Debit)
                    {
                        tb.Credit = tb.Credit - tb.Debit;
                        tb.Debit = 0;
                    }
                    else
                    {
                        tb.Debit = tb.Debit - tb.Credit;
                        tb.Credit = 0;
                    }
                }
            }

            if (debit > credit)
            {
                decimal debitDiff = debit - credit;
                if (debitDiff > 0)
                {
                    DayBookInfo diffTB = new DayBookInfo();
                    diffTB.Particulars = "Difference in opening balance";
                    diffTB.FromLedger = "LIABILITIES";
                    diffTB.Credit = debitDiff;
                    diffTB.Debit = 0;

                    trialBalance.Add(diffTB);
                }
            }
            else if (credit > debit)
            {
                decimal creditDiff = credit - debit;
                if (creditDiff > 0)
                {
                    DayBookInfo diffTB = new DayBookInfo();
                    diffTB.Particulars = "Difference in opening balance";
                    diffTB.FromLedger = "ASSETS";
                    diffTB.Debit = creditDiff;
                    diffTB.Credit = 0;

                    trialBalance.Add(diffTB);
                }
            }

            List<DayBookInfo> trialBalances = new List<DayBookInfo>();
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Credit > 0 || tb.Debit > 0)
                {
                    trialBalances.Add(tb);
                }
            }
            return trialBalances;
        }

        public static List<DayBookInfo> GetTrailBalanceDetails(DateTime toDate)
        {
            toDate.AddDays(1);
            List<DayBookInfo> trialDetails = new List<DayBookInfo>();
            DayBookInfo detail;
            List<LedgersInfo> ledgers = LedgersDAO.GetLedgersInfos();

            foreach (LedgersInfo ledger in ledgers)
            {
                if (ledger.LedgerName != DBConstant.INTEREST_LEDGER && ledger.LedgerName != DBConstant.INTEREST_PAID_LEDGER)
                {
                    DayBookInfo openingBalance = null;
                    if (ledger.LedgerName == DBConstant.CASH_LEDGER)
                        openingBalance = GetCashBookOpeningBalance(toDate);
                    else
                        openingBalance = GetBankBookOpeningBalance(toDate, ledger.LedgerID);
                    if (openingBalance.Credit > 0 || openingBalance.Debit > 0)
                    {
                        detail = new DayBookInfo();
                        detail.Particulars = ledger.LedgerName;
                        detail.Narration = ledger.SubGroupName;
                        detail.Debit = openingBalance.Debit;
                        detail.Credit = openingBalance.Credit;
                        trialDetails.Add(detail);
                    }
                }
            }

            DayBookInfo hlLoan = GetHundiLoanOpeningBalance(toDate, "HL", DBConstant.ACCOUNT_OPENING);
            detail = new DayBookInfo();
            detail.Particulars = "Hundi Loan";
            detail.Narration = "CURRENT ASSETS";
            detail.Debit = hlLoan.Credit;
            detail.Credit = hlLoan.Debit;
            trialDetails.Add(detail);

            DayBookInfo fixedDesposit = GetFixedDepositOpeningBalance(toDate, "FD", DBConstant.ACCOUNT_OPENING);
            detail = new DayBookInfo();
            detail.Particulars = "Fixed Deposit";
            detail.Narration = "CURRENT ASSETS";
            detail.Debit = fixedDesposit.Credit;
            detail.Credit = fixedDesposit.Debit;
            trialDetails.Add(detail);

            DayBookInfo atktOpening = GetATKTOpeningBalance(toDate, "ATKT", DBConstant.ACCOUNT_OPENING);
            detail = new DayBookInfo();
            detail.Particulars = "ATKT";
            detail.Narration = "CURRENT ASSETS";
            detail.Debit = atktOpening.Credit;
            detail.Credit = atktOpening.Debit;
            trialDetails.Add(detail);

            DayBookInfo interests = GetInterestOpeningBalance(toDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
            detail = new DayBookInfo();
            detail.Particulars = DBConstant.INTEREST_LEDGER;
            detail.Narration = "INDIRECT INCOMES";
            detail.Debit = interests.Debit;
            detail.Credit = interests.Credit;
            trialDetails.Add(detail);

            DayBookInfo interestPaids = GetInterestPaidOpeningBalance(toDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
            detail = new DayBookInfo();
            detail.Particulars = DBConstant.INTEREST_PAID_LEDGER;
            detail.Narration = "INDIRECT EXPENSES";
            detail.Debit = interestPaids.Debit;
            detail.Credit = interestPaids.Credit;
            trialDetails.Add(detail);

            return trialDetails;
        }

        public static DayBookInfo GetHundiLoanOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;
            DayBookInfo openingBalance = null;

            //Hundi Loans
            openingBalance = HundiLoanDAO.GetOpeningHundiLoans(toDate, ledgerName, type);
            if (openingBalance != null)
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            if (type != DBConstant.ACCOUNT_OPENING_BANK)
            {
                type = DBConstant.ACCOUNT_OPENING;
                //Hundi Loans Receipts
                openingBalance = HundiLoanDAO.GetOpeningTrans(toDate, ledgerName, type);
                if (openingBalance != null)
                {
                    credit = credit + openingBalance.Credit;
                    debit = debit + openingBalance.Debit;
                }
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetFixedDepositOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;
            DayBookInfo openingBalance = null;

            //Fixed Deposits
            openingBalance = FixedDepositDAO.GetOpeningFixedDeposits(toDate, ledgerName, type);
            if (openingBalance != null)
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            if (type != DBConstant.ACCOUNT_OPENING_BANK)
            {
                type = DBConstant.ACCOUNT_OPENING;
                //Fixed Payments
                openingBalance = FixedTransDAO.GetOpeningTrans(toDate, ledgerName, type);
                if (openingBalance != null)
                {
                    credit = credit + openingBalance.Credit;
                    debit = debit + openingBalance.Debit;
                }
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetInterestOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;

            LedgersInfo interestLedger = LedgersDAO.GetLedgersInfo(ledgerName);
            if (interestLedger != null)
            {
                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    if (interestLedger.BalanceType.Equals("Cr"))
                        credit = interestLedger.OpeningBalance;
                    else if (interestLedger.BalanceType.Equals("Dr"))
                        debit = interestLedger.OpeningBalance;

                    DayBookInfo voucherOpening = VouchersDAO.GetOpeningVoucher(toDate, interestLedger.LedgerID);
                    if (voucherOpening != null)
                    {
                        debit = debit + voucherOpening.Debit;
                        credit = credit + voucherOpening.Credit;
                    }
                }
            }

            DayBookInfo hundiOpening = HundiLoanDAO.GetOpeningInterest(toDate);
            if (hundiOpening != null)
            {
                debit = debit + hundiOpening.Debit;
                credit = credit + hundiOpening.Credit;
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetInterestPaidOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;

            LedgersInfo interestLedger = LedgersDAO.GetLedgersInfo(ledgerName);
            if (interestLedger != null)
            {
                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    if (interestLedger.BalanceType.Equals("Cr"))
                        credit = interestLedger.OpeningBalance;
                    else if (interestLedger.BalanceType.Equals("Dr"))
                        debit = interestLedger.OpeningBalance;

                    DayBookInfo voucherOpening = VouchersDAO.GetOpeningVoucher(toDate, interestLedger.LedgerID);
                    if (voucherOpening != null)
                    {
                        debit = debit + voucherOpening.Debit;
                        credit = credit + voucherOpening.Credit;
                    }
                }
            }

            DayBookInfo fdOpening = FixedInterestDAO.GetOpeningInterestpaid(toDate, ledgerName, type);
            if (fdOpening != null)
            {
                debit = debit + fdOpening.Debit;
                credit = credit + fdOpening.Credit;
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetATKTOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;
            DayBookInfo openingBalance = null;

            //Fixed Deposits
            openingBalance = ATKTDAO.GetOpeningATKT(toDate, ledgerName, type);
            if (openingBalance != null)
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetChitsOpeniningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;
            DayBookInfo openingBalance = null;

            //Chit Trans
            openingBalance = ChitsTransDAO.GetOpeningTrans(toDate, ledgerName, type);
            if (openingBalance != null)
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            //Chit Bid
            openingBalance = ChitsBiddingDAO.GetOpeningBids(toDate, ledgerName, type);
            if (openingBalance != null)
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }

        public static DayBookInfo GetChitCommissionOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            decimal credit = 0;
            decimal debit = 0;

            LedgersInfo interestLedger = LedgersDAO.GetLedgersInfo(ledgerName);
            if (interestLedger != null)
            {
                if (type == DBConstant.ACCOUNT_OPENING)
                {
                    if (interestLedger.BalanceType.Equals("Cr"))
                        credit = interestLedger.OpeningBalance;
                    else if (interestLedger.BalanceType.Equals("Dr"))
                        debit = interestLedger.OpeningBalance;

                    DayBookInfo voucherOpening = VouchersDAO.GetOpeningVoucher(toDate, interestLedger.LedgerID);
                    if (voucherOpening != null)
                    {
                        debit = debit + voucherOpening.Debit;
                        credit = credit + voucherOpening.Credit;
                    }
                }
            }

            List<ChitsBiddingInfo> chitBids = ChitsBiddingDAO.GetChitsBiddingInfos(toDate);
            foreach(ChitsBiddingInfo chitBid in chitBids)
            {
                ChitsInfo chitInfo = ChitsDAO.GetChitsInfo(chitBid.ChitNO);
                decimal chitCommission = chitInfo.ChitAmount * chitInfo.ChitCommission / 100;
                debit = debit + chitCommission;
            }

            DayBookInfo dayBook = new DayBookInfo();
            if (credit > debit)
            {
                dayBook.Credit = credit - debit;
            }
            else if (debit > credit)
            {
                dayBook.Debit = debit - credit;
            }
            return dayBook;
        }
    }
}
