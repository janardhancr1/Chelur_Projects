using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class LedgersManager
    {
        public static string GetNextLedgerID()
        {
            return LedgersDAO.GetNextLedgerID();
        }

        public static void AddLedgersInfo(LedgersInfo ledgersInfo)
        {
            LedgersDAO.AddLedgersInfo(ledgersInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateLedgersInfo(LedgersInfo ledgersInfo)
        {
            LedgersDAO.AddLedgersInfo(ledgersInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteLedgersInfo(int ledgerID)
        {
            LedgersDAO.DeleteLedgersInfo(ledgerID);
        }

        public static List<LedgersInfo> SearchLedgersInfo(string ledgerName, string balanceType, int groupID, int startRowIndex, int maximumRows)
        {
            return LedgersDAO.SearchLedgersInfo(LedgersDAO.SearchConditions(ledgerName, balanceType, groupID), startRowIndex);
        }

        public static int SearchLedgersInfoCount(string ledgerName, string balanceType, int groupID, int startRowIndex, int maximumRows)
        {
            return LedgersDAO.SearchLedgersInfoCount(LedgersDAO.SearchConditions(ledgerName, balanceType, groupID));
        }

        public static LedgersInfo GetLedgersInfo(int ledgerID)
        {
            return LedgersDAO.GetLedgersInfo(ledgerID);
        }

        public static LedgersInfo GetLedgersInfo(string ledgerName)
        {
            return LedgersDAO.GetLedgersInfo(ledgerName);
        }

        public static List<LedgersInfo> GetLedgersInfos()
        {
            return LedgersDAO.GetLedgersInfos();
        }

        #region Accounts
        public static List<DayBookInfo> GetHundiLoanLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            return LedgersDAO.GetHundiLoanLedger(fromDate, toDate, ledgerName, type);
        }

        public static List<DayBookInfo> GetFixedDespositLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            return LedgersDAO.GetFixedDespositLedger(fromDate, toDate, ledgerName, type);
        }

        public static List<DayBookInfo> GetATKTLedger(DateTime fromDate, DateTime toDate, string ledgerName, int type)
        {
            return LedgersDAO.GetATKTLedger(fromDate, toDate, ledgerName, type);
        }


        public static List<DayBookInfo> GetInterestLedger(DateTime fromDate, DateTime toDate)
        {
            List<DayBookInfo> details = LedgersDAO.GetInterestLedger(fromDate, toDate);
            LedgersInfo interestLedger = LedgersDAO.GetLedgersInfo(DBConstant.INTEREST_LEDGER);
            if (interestLedger != null)
            {
                List<DayBookInfo> vouchers = AccountsDAO.GetVoucherDetails(fromDate, toDate, interestLedger.LedgerID);
                details.AddRange(vouchers);
            }
            details.Sort(new ReportComparer());
            return details;
        }

        public static List<DayBookInfo> GetInterestPaidLedger(DateTime fromDate, DateTime toDate)
        {
            List<DayBookInfo> details = LedgersDAO.GetInterestPaidLedger(fromDate, toDate);
            LedgersInfo interestLedger = LedgersDAO.GetLedgersInfo(DBConstant.INTEREST_PAID_LEDGER);
            if (interestLedger != null)
            {
                List<DayBookInfo> vouchers = AccountsDAO.GetVoucherDetails(fromDate, toDate, interestLedger.LedgerID);
                details.AddRange(vouchers);
            }
            details.Sort(new ReportComparer());
            return details;
        }

        #endregion
    }
}