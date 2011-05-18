using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class AccountsManager
    {
        #region Helpers

        public static List<GroupsInfo> GetGroups()
        {
            return AccountsDAO.GetGroups();
        }

        public static List<VoucherTypesInfo> GetVoucherTypes()
        {
            return AccountsDAO.GetVoucherTypes();
        }

        public static int NextVoucherNo(DateTime voucherDate, int voucherType)
        {
            return AccountsDAO.NextVoucherNo(voucherDate, voucherType);
        }

        public static List<DayBookInfo> GetDayBook(DateTime fromDate, DateTime toDate)
        {
            List<DayBookInfo> dayBooks = new List<DayBookInfo>();

            List<DayBookInfo> hundiLoans = LedgersDAO.GetHundiLoanLedger(fromDate, toDate, "HL", DBConstant.ACCOUNT_PERIOD);
            foreach (DayBookInfo day in hundiLoans)
            {
                dayBooks.Add(day);
            }

            List<DayBookInfo> fixedDeposits = LedgersDAO.GetFixedDespositLedger(fromDate, toDate, "FD", DBConstant.ACCOUNT_PERIOD);
            foreach (DayBookInfo day in fixedDeposits)
            {
                dayBooks.Add(day);
            }

            List<DayBookInfo> atktInfos = LedgersDAO.GetATKTLedger(fromDate, toDate, "ATKT", DBConstant.ACCOUNT_PERIOD);
            foreach (DayBookInfo day in atktInfos)
            {
                dayBooks.Add(day);
            }

            List<DayBookInfo> vouchers = AccountsDAO.GetVoucherDetails(fromDate, toDate, 0);
            foreach (DayBookInfo day in vouchers)
            {
                dayBooks.Add(day);
            }
            return dayBooks;
        }

        public static List<DayBookInfo> GetLedgerDetails(DateTime fromDate, DateTime toDate, int ledgerID)
        {
            return AccountsDAO.GetVoucherDetails(fromDate, toDate, ledgerID);
        }

        public static string GetVoucherTypeName(int voucherTypeID)
        {
            return AccountsDAO.GetVoucherTypeName(voucherTypeID);
        }

        #endregion

        #region Opening Balance

        public static DayBookInfo GetCashBookOpeningBalance(DateTime toDate)
        {
            return AccountsDAO.GetCashBookOpeningBalance(toDate);
        }

        public static DayBookInfo GetBankBookOpeningBalance(DateTime toDate, int ledgerID)
        {
            return AccountsDAO.GetBankBookOpeningBalance(toDate, ledgerID);
        }

        public static DayBookInfo GetLedgerOpeningBalance(DateTime toDate, int ledgerID)
        {
            return AccountsDAO.GetBankBookOpeningBalance(toDate, ledgerID);
        }

        public static DayBookInfo GetHundiLoanOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            return AccountsDAO.GetHundiLoanOpeningBalance(toDate, ledgerName, type);
        }

        public static DayBookInfo GetFixedDepositOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            return AccountsDAO.GetFixedDepositOpeningBalance(toDate, ledgerName, type);
        }

        public static DayBookInfo GetInterestOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            return AccountsDAO.GetInterestOpeningBalance(toDate, ledgerName, type);
        }

        public static DayBookInfo GetInterestPaidOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            return AccountsDAO.GetInterestPaidOpeningBalance(toDate, ledgerName, type);
        }

        public static DayBookInfo GetATKTOpeningBalance(DateTime toDate, string ledgerName, int type)
        {
            return AccountsDAO.GetATKTOpeningBalance(toDate, ledgerName, type);
        }

        #endregion

        #region Monthly Summary

        public static List<DayBookInfo> GetCashMonthlySummary(DateTime fromDate, DateTime toDate, string ledgerName)
        {
            LedgersInfo ledger = LedgersDAO.GetLedgersInfo(DBConstant.CASH_LEDGER);
            List<DayBookInfo> details = new List<DayBookInfo>();
            if (ledger != null)
            {
                details = AccountsDAO.GetVoucherDetails(fromDate, toDate, ledger.LedgerID);
            }

            List<DayBookInfo> dayBooks = LedgersDAO.GetHundiLoanLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo day in dayBooks)
            {
                if (day.FromLedger.Equals(DBConstant.CASH_LEDGER) || day.ToLedger.Equals(DBConstant.CASH_LEDGER))
                    details.Add(day);
            }

            dayBooks = LedgersDAO.GetFixedDespositLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo day in dayBooks)
            {
                if (day.FromLedger.Equals(DBConstant.CASH_LEDGER) || day.ToLedger.Equals(DBConstant.CASH_LEDGER))
                    details.Add(day);
            }

            dayBooks = LedgersDAO.GetATKTLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            foreach (DayBookInfo day in dayBooks)
            {
                if (day.FromLedger.Equals(DBConstant.CASH_LEDGER) || day.ToLedger.Equals(DBConstant.CASH_LEDGER))
                    details.Add(day);
            }

            dayBooks = LedgersDAO.GetInterestLedger(fromDate, toDate);
            foreach (DayBookInfo day in dayBooks)
            {
                if (day.FromLedger.Equals(DBConstant.CASH_LEDGER) || day.ToLedger.Equals(DBConstant.CASH_LEDGER))
                    details.Add(day);
            }

            dayBooks = LedgersDAO.GetInterestPaidLedger(fromDate, toDate);
            foreach (DayBookInfo day in dayBooks)
            {
                if (day.FromLedger.Equals(DBConstant.CASH_LEDGER) || day.ToLedger.Equals(DBConstant.CASH_LEDGER))
                    details.Add(day);
            }
            details.Sort(new ReportComparer());

            List<DayBookInfo> monthlySummary = GetMonthlySummary(details);
            return monthlySummary;
        }

        public static List<DayBookInfo> GetBankMonthlySummary(DateTime fromDate, DateTime toDate, int ledgerID)
        {
            LedgersInfo ledger = LedgersDAO.GetLedgersInfo(ledgerID);
            List<DayBookInfo> details = AccountsDAO.GetVoucherDetails(fromDate, toDate, ledger.LedgerID);
            foreach (DayBookInfo day in details)
            {
                if (day.VoucherType.Equals(GetVoucherTypeName(DBConstant.VOUCHER_CONTRA)))
                {
                    if (day.FromLedger.Equals(ledger.LedgerName) && LedgersDAO.GetLedgersInfo(day.ToLedger).GroupName.Equals(DBConstant.BANK_LEDGERS))
                    {
                        decimal temp1 = day.Credit;
                        day.Credit = day.Debit;
                        day.Debit = temp1;
                    }
                    else if (day.ToLedger.Equals(ledger.LedgerName) && LedgersDAO.GetLedgersInfo(day.FromLedger).GroupName.Equals(DBConstant.BANK_LEDGERS))
                    {
                        decimal temp1 = day.Credit;
                        day.Credit = day.Debit;
                        day.Debit = temp1;
                    }
                    else
                    {
                        decimal temp = day.Credit;
                        day.Credit = day.Debit;
                        day.Debit = temp;
                    }
                }
            }
            details.Sort(new ReportComparer());

            List<DayBookInfo> monthlySummary = GetMonthlySummary(details);
            return monthlySummary;
        }

        public static List<DayBookInfo> GetLedgerMonthlySummary(DateTime fromDate, DateTime toDate, int ledgerID)
        {
            LedgersInfo ledger = LedgersDAO.GetLedgersInfo(ledgerID);
            List<DayBookInfo> details = AccountsDAO.GetVoucherDetails(fromDate, toDate, ledger.LedgerID);
            foreach (DayBookInfo day in details)
            {
                decimal temp = day.Credit;
                day.Credit = day.Debit;
                day.Debit = temp;
            }
            details.Sort(new ReportComparer());

            List<DayBookInfo> monthlySummary = GetMonthlySummary(details);
            return monthlySummary;
        }

        public static List<DayBookInfo> GetMonthlySummary(DateTime fromDate, DateTime toDate, string ledgerName, int type, int ledger)
        {
            //LedgerDAO ledgerDao = new LedgerDAO();
            //VoucherDAO voucherDao = new VoucherDAO();
            List<DayBookInfo> details = null;
            switch (ledger)
            {
                case 1:
                    details = LedgersDAO.GetHundiLoanLedger(fromDate, toDate, ledgerName, type);
                    break;
                case 2:
                    details = LedgersDAO.GetFixedDespositLedger(fromDate, toDate, ledgerName, type);
                    break;
                case 3:
                    details = LedgersDAO.GetATKTLedger(fromDate, toDate, ledgerName, type);
                    break;
                //case 4:
                //    details = ledgerDao.GetPronoteLoanLedger(fromDate, toDate, ledgerName, type);
                //    break;
                case 5:
                    details = LedgersDAO.GetInterestLedger(fromDate, toDate);
                    LedgersInfo interestLeger = LedgersDAO.GetLedgersInfo(ledgerName);
                    if (interestLeger != null)
                    {
                        List<DayBookInfo> interestVouchers = AccountsDAO.GetVoucherDetails(fromDate, toDate, interestLeger.LedgerID);
                        foreach (DayBookInfo voucher in interestVouchers)
                        {
                            details.Add(voucher);
                        }
                    }
                    break;
                case 6:
                    details = LedgersDAO.GetInterestPaidLedger(fromDate, toDate);
                    if (type == DBConstant.ACCOUNT_OPENING)
                        type = DBConstant.ACCOUNT_OPENING_CUSTOMER;
                    else if (type == DBConstant.ACCOUNT_PERIOD)
                        type = DBConstant.ACCOUNT_LEDGER;
                    LedgersInfo interestPaidLeger = LedgersDAO.GetLedgersInfo(ledgerName);
                    if (interestPaidLeger != null)
                    {
                        List<DayBookInfo> interestPaidVouchers = AccountsDAO.GetVoucherDetails(fromDate, toDate, interestPaidLeger.LedgerID);
                        foreach (DayBookInfo voucher in interestPaidVouchers)
                        {
                            details.Add(voucher);
                        }
                    }
                    break;
                //case 9:
                //    details = ledgerDao.GetCustomerLedger(fromDate, toDate, ledgerName, compID);
                //    break;
                //case 10:
                //    details = voucherDao.GetVoucherDetails(fromDate, toDate, ledgerName);
                //    break;
                //case 13:
                //    details = ledgerDao.GetAuctionProfitLedger(fromDate, toDate);
                //    List<DayBookInfo> auctionProfitVouchers = voucherDao.GetVoucherDetails(fromDate, toDate, DBConstant.AUCTION_PROFIT_LEDGER);
                //    foreach (DayBookInfo voucher in auctionProfitVouchers)
                //    {
                //        details.Add(voucher);
                //    }
                //    break;

            }
            if (details != null)
            {
                details.Sort(new ReportComparer());
            }
            List<DayBookInfo> monthlySummary = GetLedgerMonthlySummary(details);
            return monthlySummary;
        }

        private static List<DayBookInfo> GetMonthlySummary(IEnumerable<DayBookInfo> details)
        {
            List<DayBookInfo> monthlySummary = new List<DayBookInfo>();
            DayBookInfo april = new DayBookInfo();
            DayBookInfo may = new DayBookInfo();
            DayBookInfo june = new DayBookInfo();
            DayBookInfo july = new DayBookInfo();
            DayBookInfo august = new DayBookInfo();
            DayBookInfo september = new DayBookInfo();
            DayBookInfo october = new DayBookInfo();
            DayBookInfo november = new DayBookInfo();
            DayBookInfo december = new DayBookInfo();
            DayBookInfo january = new DayBookInfo();
            DayBookInfo february = new DayBookInfo();
            DayBookInfo march = new DayBookInfo();

            if (details != null)
            {
                foreach (DayBookInfo day in details)
                {
                    switch (day.CurrentDate.Month)
                    {
                        case 1:
                            january.Credit = january.Credit + day.Credit;
                            january.Debit = january.Debit + day.Debit;
                            break;
                        case 2:
                            february.Credit = february.Credit + day.Credit;
                            february.Debit = february.Debit + day.Debit;
                            break;
                        case 3:
                            march.Credit = march.Credit + day.Credit;
                            march.Debit = march.Debit + day.Debit;
                            break;
                        case 4:
                            april.Credit = april.Credit + day.Credit;
                            april.Debit = april.Debit + day.Debit;
                            break;
                        case 5:
                            may.Credit = may.Credit + day.Credit;
                            may.Debit = may.Debit + day.Debit;
                            break;
                        case 6:
                            june.Credit = june.Credit + day.Credit;
                            june.Debit = june.Debit + day.Debit;
                            break;
                        case 7:
                            july.Credit = july.Credit + day.Credit;
                            july.Debit = july.Debit + day.Debit;
                            break;
                        case 8:
                            august.Credit = august.Credit + day.Credit;
                            august.Debit = august.Debit + day.Debit;
                            break;
                        case 9:
                            september.Credit = september.Credit + day.Credit;
                            september.Debit = september.Debit + day.Debit;
                            break;
                        case 10:
                            october.Credit = october.Credit + day.Credit;
                            october.Debit = october.Debit + day.Debit;
                            break;
                        case 11:
                            november.Credit = november.Credit + day.Credit;
                            november.Debit = november.Debit + day.Debit;
                            break;
                        case 12:
                            december.Credit = december.Credit + day.Credit;
                            december.Debit = december.Debit + day.Debit;
                            break;
                    }
                }
            }

            monthlySummary.Add(april);
            monthlySummary.Add(may);
            monthlySummary.Add(june);
            monthlySummary.Add(july);
            monthlySummary.Add(august);
            monthlySummary.Add(september);
            monthlySummary.Add(october);
            monthlySummary.Add(november);
            monthlySummary.Add(december);
            monthlySummary.Add(january);
            monthlySummary.Add(february);
            monthlySummary.Add(march);

            return monthlySummary;
        }

        private static List<DayBookInfo> GetLedgerMonthlySummary(IEnumerable<DayBookInfo> details)
        {
            List<DayBookInfo> monthlySummary = new List<DayBookInfo>();
            DayBookInfo april = new DayBookInfo();
            DayBookInfo may = new DayBookInfo();
            DayBookInfo june = new DayBookInfo();
            DayBookInfo july = new DayBookInfo();
            DayBookInfo august = new DayBookInfo();
            DayBookInfo september = new DayBookInfo();
            DayBookInfo october = new DayBookInfo();
            DayBookInfo november = new DayBookInfo();
            DayBookInfo december = new DayBookInfo();
            DayBookInfo january = new DayBookInfo();
            DayBookInfo february = new DayBookInfo();
            DayBookInfo march = new DayBookInfo();

            if (details != null)
            {
                foreach (DayBookInfo day in details)
                {
                    switch (day.CurrentDate.Month)
                    {
                        case 1:
                            january.Credit = january.Credit + day.Debit;
                            january.Debit = january.Debit + day.Credit;
                            break;
                        case 2:
                            february.Credit = february.Credit + day.Debit;
                            february.Debit = february.Debit + day.Credit;
                            break;
                        case 3:
                            march.Credit = march.Credit + day.Debit;
                            march.Debit = march.Debit + day.Credit;
                            break;
                        case 4:
                            april.Credit = april.Credit + day.Debit;
                            april.Debit = april.Debit + day.Credit;
                            break;
                        case 5:
                            may.Credit = may.Credit + day.Debit;
                            may.Debit = may.Debit + day.Credit;
                            break;
                        case 6:
                            june.Credit = june.Credit + day.Debit;
                            june.Debit = june.Debit + day.Credit;
                            break;
                        case 7:
                            july.Credit = july.Credit + day.Debit;
                            july.Debit = july.Debit + day.Credit;
                            break;
                        case 8:
                            august.Credit = august.Credit + day.Debit;
                            august.Debit = august.Debit + day.Credit;
                            break;
                        case 9:
                            september.Credit = september.Credit + day.Debit;
                            september.Debit = september.Debit + day.Credit;
                            break;
                        case 10:
                            october.Credit = october.Credit + day.Debit;
                            october.Debit = october.Debit + day.Credit;
                            break;
                        case 11:
                            november.Credit = november.Credit + day.Debit;
                            november.Debit = november.Debit + day.Credit;
                            break;
                        case 12:
                            december.Credit = december.Credit + day.Debit;
                            december.Debit = december.Debit + day.Credit;
                            break;
                    }
                }
            }

            monthlySummary.Add(april);
            monthlySummary.Add(may);
            monthlySummary.Add(june);
            monthlySummary.Add(july);
            monthlySummary.Add(august);
            monthlySummary.Add(september);
            monthlySummary.Add(october);
            monthlySummary.Add(november);
            monthlySummary.Add(december);
            monthlySummary.Add(january);
            monthlySummary.Add(february);
            monthlySummary.Add(march);

            return monthlySummary;
        }
        #endregion

        #region Profit & Loss

        public static List<DayBookInfo> GetExpenses(DateTime toDate)
        {
            return AccountsDAO.GetExpenses(toDate);
        }

        public static List<DayBookInfo> GetIncomes(DateTime toDate)
        {
            return AccountsDAO.GetIncomes(toDate);
        }

        public static List<DayBookInfo> GetExpensesDetails(DateTime toDate)
        {
            return AccountsDAO.GetExpensesDetails(toDate);
        }

        public static List<DayBookInfo> GetIncomesDetails(DateTime toDate)
        {
            return AccountsDAO.GetIncomesDetails(toDate);
        }

        public List<DayBookInfo> GetProfitLoss(DateTime todate)
        {
            AccountsDAO accountDao = new AccountsDAO();
            int maxCount = 0;
            decimal loss = 0;
            decimal profit = 0;

            List<DayBookInfo> profitlossDetails = new List<DayBookInfo>();
            List<DayBookInfo> expenses = GetExpenses(todate.AddDays(1));
            List<DayBookInfo> incomes = GetIncomes(todate.AddDays(1));

            if (expenses.Count > incomes.Count)
                maxCount = expenses.Count;
            else if (expenses.Count < incomes.Count)
                maxCount = incomes.Count;
            else
                maxCount = expenses.Count;

            for (int i = 0; i < maxCount; i++)
            {
                DayBookInfo detail = new DayBookInfo();
                if (i < expenses.Count)
                {
                    detail.FromLedger = expenses[i].Particulars;
                    detail.Debit = expenses[i].Debit;
                    loss = loss + expenses[i].Debit;
                }
                if (i < incomes.Count)
                {
                    detail.ToLedger = incomes[i].Particulars;
                    detail.Credit = incomes[i].Debit;
                    profit = profit + incomes[i].Debit;
                }
                profitlossDetails.Add(detail);
            }
            if (profit > loss)
            {
                DayBookInfo detail = new DayBookInfo();
                detail.FromLedger = "Gross Profit C/O";
                detail.Debit = profit - loss;
                detail.ToLedger = "";
                detail.Credit = 0;
                profitlossDetails.Add(detail);

                DayBookInfo detail2 = new DayBookInfo();
                detail2.FromLedger = "";
                detail2.Debit = 0;
                detail2.ToLedger = "";
                detail2.Credit = 0;
                profitlossDetails.Add(detail2);

                DayBookInfo detail3 = new DayBookInfo();
                detail3.FromLedger = "Total";
                detail3.Debit = loss + (profit - loss);
                detail3.ToLedger = "Total";
                detail3.Credit = loss + (profit - loss);
                profitlossDetails.Add(detail3);

                DayBookInfo detail4 = new DayBookInfo();
                detail4.FromLedger = "";
                detail4.Debit = 0;
                detail4.ToLedger = "";
                detail4.Credit = 0;
                profitlossDetails.Add(detail4);

                DayBookInfo detail5 = new DayBookInfo();
                detail5.FromLedger = "Net Profit";
                detail5.Debit = profit - loss;
                detail5.ToLedger = "Gross Profit B/F";
                detail5.Credit = profit - loss;
                profitlossDetails.Add(detail5);
            }
            if (loss > profit)
            {
                DayBookInfo detail = new DayBookInfo();
                detail.FromLedger = "";
                detail.Debit = 0;
                detail.ToLedger = "Gross Loss C/O";
                detail.Credit = loss - profit;
                profitlossDetails.Add(detail);

                DayBookInfo detail2 = new DayBookInfo();
                detail2.FromLedger = "";
                detail2.Debit = 0;
                detail2.ToLedger = "";
                detail2.Credit = 0;
                profitlossDetails.Add(detail2);

                DayBookInfo detail3 = new DayBookInfo();
                detail3.FromLedger = "Total";
                detail3.Debit = profit + (loss - profit);
                detail3.ToLedger = "Total";
                detail3.Credit = profit + (loss - profit);
                profitlossDetails.Add(detail3);

                DayBookInfo detail4 = new DayBookInfo();
                detail4.FromLedger = "";
                detail4.Debit = 0;
                detail4.ToLedger = "";
                detail4.Credit = 0;
                profitlossDetails.Add(detail4);

                DayBookInfo detail5 = new DayBookInfo();
                detail5.FromLedger = "Gross Loss B/F";
                detail5.Debit = loss - profit;
                detail5.ToLedger = "Net Loss";
                detail5.Credit = loss - profit;
                profitlossDetails.Add(detail5);
            }

            return profitlossDetails;
        }

        public List<DayBookInfo> GetProfitLossDetails(DateTime todate)
        {
            AccountsDAO accountDao = new AccountsDAO();
            int maxCount = 0;
            decimal loss = 0;
            decimal profit = 0;

            List<DayBookInfo> profitlossDetails = new List<DayBookInfo>();
            List<DayBookInfo> expenses = GetExpensesDetails(todate.AddDays(1));
            List<DayBookInfo> incomes = GetIncomesDetails(todate.AddDays(1));

            if (expenses.Count > incomes.Count)
                maxCount = expenses.Count;
            else if (expenses.Count < incomes.Count)
                maxCount = incomes.Count;
            else
                maxCount = expenses.Count;

            for (int i = 0; i < maxCount; i++)
            {
                DayBookInfo detail = new DayBookInfo();
                if (i < expenses.Count)
                {
                    detail.FromLedger = expenses[i].Particulars;
                    detail.Debit = expenses[i].Debit;
                    loss = loss + expenses[i].Debit;
                }
                if (i < incomes.Count)
                {
                    detail.ToLedger = incomes[i].Particulars;
                    detail.Credit = incomes[i].Debit;
                    profit = profit + incomes[i].Debit;
                }
                profitlossDetails.Add(detail);
            }
            if (profit > loss)
            {
                DayBookInfo detail = new DayBookInfo();
                detail.FromLedger = "Gross Profit C/O";
                detail.Debit = profit - loss;
                detail.ToLedger = "";
                detail.Credit = 0;
                profitlossDetails.Add(detail);

                DayBookInfo detail2 = new DayBookInfo();
                detail2.FromLedger = "";
                detail2.Debit = 0;
                detail2.ToLedger = "";
                detail2.Credit = 0;
                profitlossDetails.Add(detail2);

                DayBookInfo detail3 = new DayBookInfo();
                detail3.FromLedger = "Total";
                detail3.Debit = loss + (profit - loss);
                detail3.ToLedger = "Total";
                detail3.Credit = loss + (profit - loss);
                profitlossDetails.Add(detail3);

                DayBookInfo detail4 = new DayBookInfo();
                detail4.FromLedger = "";
                detail4.Debit = 0;
                detail4.ToLedger = "";
                detail4.Credit = 0;
                profitlossDetails.Add(detail4);

                DayBookInfo detail5 = new DayBookInfo();
                detail5.FromLedger = "Net Profit";
                detail5.Debit = profit - loss;
                detail5.ToLedger = "Gross Profit B/F";
                detail5.Credit = profit - loss;
                profitlossDetails.Add(detail5);
            }
            if (loss > profit)
            {
                DayBookInfo detail = new DayBookInfo();
                detail.FromLedger = "";
                detail.Debit = 0;
                detail.ToLedger = "Gross Loss C/O";
                detail.Credit = loss - profit;
                profitlossDetails.Add(detail);

                DayBookInfo detail2 = new DayBookInfo();
                detail2.FromLedger = "";
                detail2.Debit = 0;
                detail2.ToLedger = "";
                detail2.Credit = 0;
                profitlossDetails.Add(detail2);

                DayBookInfo detail3 = new DayBookInfo();
                detail3.FromLedger = "Total";
                detail3.Debit = profit + (loss - profit);
                detail3.ToLedger = "Total";
                detail3.Credit = profit + (loss - profit);
                profitlossDetails.Add(detail3);

                DayBookInfo detail4 = new DayBookInfo();
                detail4.FromLedger = "";
                detail4.Debit = 0;
                detail4.ToLedger = "";
                detail4.Credit = 0;
                profitlossDetails.Add(detail4);

                DayBookInfo detail5 = new DayBookInfo();
                detail5.FromLedger = "Gross Loss B/F";
                detail5.Debit = loss - profit;
                detail5.ToLedger = "Net Loss";
                detail5.Credit = loss - profit;
                profitlossDetails.Add(detail5);
            }

            return profitlossDetails;
        }

        #endregion

        #region Trial Balance

        public static List<DayBookInfo> GetTrailBalance(DateTime toDate)
        {
            return AccountsDAO.GetTrailBalance(toDate.AddDays(1));
        }

        public static List<DayBookInfo> GetTrailBalanceDetails(DateTime toDate)
        {
            return AccountsDAO.GetTrailBalanceDetails(toDate.AddDays(1));
        }

        #endregion

        #region Print
        public List<DayBookInfo> GetPrintLedger(DateTime fromDate, DateTime toDate, int ledgerID, string ledgerName, int type)
        {
            List<DayBookInfo> details = new List<DayBookInfo>();

            DayBookInfo openingBalance = null;

            if (type == 1)
                openingBalance = GetHundiLoanOpeningBalance(fromDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            else if (type == 2)
                openingBalance = GetFixedDepositOpeningBalance(fromDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            else if (type == 3)
                openingBalance = GetATKTOpeningBalance(fromDate, ledgerName, DBConstant.ACCOUNT_OPENING);
            else if (type == 5)
                openingBalance = GetInterestOpeningBalance(fromDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
            else if (type == 6)
                openingBalance = GetInterestPaidOpeningBalance(fromDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
            else if (type == 7)
                openingBalance = GetLedgerOpeningBalance(fromDate, ledgerID);
            else if (type == 8)
                openingBalance = GetBankBookOpeningBalance(fromDate, ledgerID);
            else if (type == 9)
                openingBalance = GetCashBookOpeningBalance(fromDate);
            else
                openingBalance = GetLedgerOpeningBalance(fromDate, ledgerID);

            decimal credit = 0;
            decimal debit = 0;


            List<DayBookInfo> ledgerDetails = null;

            if (type == 1)
                ledgerDetails = LedgersDAO.GetHundiLoanLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_PERIOD);
            else if (type == 2)
                ledgerDetails = LedgersDAO.GetFixedDespositLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_PERIOD);
            else if (type == 3)
                ledgerDetails = LedgersDAO.GetATKTLedger(fromDate, toDate, ledgerName, DBConstant.ACCOUNT_PERIOD);
            else if (type == 5)
                ledgerDetails = LedgersDAO.GetInterestLedger(fromDate, toDate);
            else if (type == 6)
                ledgerDetails = LedgersDAO.GetInterestPaidLedger(fromDate, toDate);
            else if (type == 7)
                ledgerDetails = GetLedgerDetails(fromDate, toDate, ledgerID);
            else if (type == 8)
                ledgerDetails = GetLedgerDetails(fromDate, toDate, ledgerID);
            else if (type == 9)
                ledgerDetails = GetLedgerDetails(fromDate, toDate, ledgerID);
            else
                ledgerDetails = GetLedgerDetails(fromDate, toDate, ledgerID);

            foreach (DayBookInfo ledgerDetail in ledgerDetails)
            {
                if (type != 11 && type != 12)
                {
                    decimal detlTemp = ledgerDetail.Debit;
                    ledgerDetail.Debit = ledgerDetail.Credit;
                    ledgerDetail.Credit = detlTemp;

                    if (ledgerDetail.Debit > 0)
                        ledgerDetail.Particulars = DBConstant.PARTICULARS_BY + ledgerDetail.Particulars;
                    else if (ledgerDetail.Credit > 0)
                        ledgerDetail.Particulars = DBConstant.PARTICULARS_TO + ledgerDetail.Particulars;

                    details.Add(ledgerDetail);

                    credit = credit + ledgerDetail.Debit;
                    debit = debit + ledgerDetail.Credit;
                }
                else
                {
                    if (ledgerDetail.Debit > 0)
                        ledgerDetail.Particulars = DBConstant.PARTICULARS_BY + ledgerDetail.Particulars;
                    else if (ledgerDetail.Credit > 0)
                        ledgerDetail.Particulars = DBConstant.PARTICULARS_TO + ledgerDetail.Particulars;

                    details.Add(ledgerDetail);

                    credit = credit + ledgerDetail.Credit;
                    debit = debit + ledgerDetail.Debit;
                }
            }

            details.Sort(new LedgerComparer());
            int i = 1;
            foreach (DayBookInfo detail in details)
            {
                detail.ToLedger = i.ToString();
                i++;
            }

            if (openingBalance != null)
            {
                openingBalance.Particulars = "Opening Balance";
                if (type != 7 && type != 8 && type != 11 && type != 12 && type != 10)
                {
                    decimal openTemp = openingBalance.Debit;
                    openingBalance.Debit = openingBalance.Credit;
                    openingBalance.Credit = openTemp;
                }
                details.Insert(0, openingBalance);
            }

            DayBookInfo currentTotal = new DayBookInfo();
            currentTotal.Particulars = "Current Total";
            if (type != 11 && type != 12)
            {
                currentTotal.Credit = debit;
                currentTotal.Debit = credit;
            }
            else
            {
                currentTotal.Credit = credit;
                currentTotal.Debit = debit;
            }
            details.Add(currentTotal);

            if (type != 11 && type != 12)
            {
                credit = credit + openingBalance.Debit;
                debit = debit + openingBalance.Credit;
            }
            else
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }

            DayBookInfo closingBalance = new DayBookInfo();
            closingBalance.Particulars = "Closing Balance";
            if (type != 11 && type != 12)
            {
                if (credit > debit)
                {
                    closingBalance.Debit = credit - debit;
                }
                else if (debit > credit)
                {
                    closingBalance.Credit = debit - credit;
                }
            }
            else
            {
                if (credit > debit)
                {
                    closingBalance.Credit = credit - debit;
                }
                else if (debit > credit)
                {
                    closingBalance.Debit = debit - credit;
                }
            }
            details.Add(closingBalance);

            return details;
        }
        #endregion
    }

    public class ReportComparer : IComparer<DayBookInfo>
    {
        public int Compare(DayBookInfo ta, DayBookInfo tb)
        {
            int retVal = DateTime.Compare(ta.CurrentDate, tb.CurrentDate);
            return retVal;
        }
    }
}
