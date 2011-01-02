using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class HundiLoanManager
    {
        public static void AddHundiLoanInfo(HundiLoanInfo hundiLoanInfo)
        {
            HundiLoanDAO.AddHundiLoanInfo(hundiLoanInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateHundiLoanInfo(HundiLoanInfo hundiLoanInfo)
        {
            HundiLoanDAO.AddHundiLoanInfo(hundiLoanInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteHundiLoanInfo(string hlLoanno)
        {
            HundiLoanDAO.DeleteHundiLoanInfo(hlLoanno);
        }

        public static List<HundiLoanInfo> SearchHundiLoanInfo(string hlLoanno, int customerID, string coObligent, string coobligentAddress, decimal loanAmount, DateTime loanDate, string closed, decimal rate, int payMode, string chequeNO, int bankID, int startRowIndex, int maximumRows)
        {
            return HundiLoanDAO.SearchHundiLoanInfo(HundiLoanDAO.SearchConditions(hlLoanno, customerID, coObligent, coobligentAddress, loanAmount, loanDate, closed, rate, payMode, chequeNO, bankID), startRowIndex);
        }

        public static int SearchHundiLoanInfoCount(string hlLoanno, int customerID, string coObligent, string coobligentAddress, decimal loanAmount, DateTime loanDate, string closed, decimal rate, int payMode, string chequeNO, int bankID, int startRowIndex, int maximumRows)
        {
            return HundiLoanDAO.SearchHundiLoanInfoCount(HundiLoanDAO.SearchConditions(hlLoanno, customerID, coObligent, coobligentAddress, loanAmount, loanDate, closed, rate, payMode, chequeNO, bankID));
        }

        public static HundiLoanInfo GetHundiLoanInfo(string hlLoanno)
        {
            return HundiLoanDAO.GetHundiLoanInfo(hlLoanno);
        }

        public static List<HundiLoanInfo> GetHundiLoanInfos()
        {
            return HundiLoanDAO.GetHundiLoanInfos();
        }

        public static List<HundiLoanInfo> GetHundiLoanInfos(DateTime fromDate, DateTime toDate, string closed, string orderBy)
        {
            List<HundiLoanInfo> details = HundiLoanDAO.GetHundiLoanInfos(fromDate, toDate, closed);
            if (orderBy.ToLower().Equals("balance"))
                details.Sort(new BalanceComaparer());
            return details;

        }

        public static List<HundiTransInfo> GetHundiTransInfos(string hlLoanno, decimal loanAmount)
        {
            List<HundiTransInfo> details = HundiLoanDAO.GetHundiTransInfos(hlLoanno, loanAmount);
            details.Sort(new RecordIDComparer());
            return details;
        }

        public static void AddHundiTransInfo(HundiTransInfo hundiTransInfo)
        {
            HundiLoanDAO.AddHundiTransInfo(hundiTransInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateHundiTransInfo(HundiTransInfo hundiTransInfo)
        {
            HundiLoanDAO.AddHundiTransInfo(hundiTransInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteHundiTransInfo(int recordID)
        {
            HundiLoanDAO.DeleteHundiTransInfo(recordID);
        }

        public static List<HundiInterestInfo> GetHundiInterestInfos(string hlLoanno)
        {
            return HundiLoanDAO.GetHundiInterestInfos(hlLoanno);
        }

        public static void AddHundiInterestInfo(HundiInterestInfo hundiInterestInfo, decimal balance)
        {
            HundiLoanDAO.AddHundiInterestInfo(hundiInterestInfo, balance, DBConstant.MODE_ADD);
        }

        public static void UpdateHundiInterestInfo(HundiInterestInfo hundiInterestInfo, decimal balance)
        {
            HundiLoanDAO.AddHundiInterestInfo(hundiInterestInfo, balance, DBConstant.MODE_UPDATE);
        }

        public static void DeleteHundiInterestInfo(List<int> recordIDs)
        {
            foreach (int recordID in recordIDs)
            {
                HundiLoanDAO.DeleteHundiInterestInfo(recordID);
            }
        }

    }

    public class BalanceComaparer : IComparer<HundiLoanInfo>
    {
        public int Compare(HundiLoanInfo a, HundiLoanInfo b)
        {
            int returnValue = 1;
            if (a != null && b == null)
            {
                returnValue = 0;
            }
            else if (a == null && b != null)
            {
                returnValue = 0;
            }
            else if (a != null && b != null)
            {
                returnValue = b.Balance.CompareTo(a.Balance);
            }
            return returnValue;
        }
    }

    public class RecordIDComparer : IComparer<HundiTransInfo>
    {
        public int Compare(HundiTransInfo a, HundiTransInfo b)
        {
            int returnValue = 1;
            if (a != null && b == null)
            {
                returnValue = 0;
            }
            else if (a == null && b != null)
            {
                returnValue = 0;
            }
            else if (a != null && b != null)
            {
                returnValue = a.RecordID.CompareTo(b.RecordID);
            }
            return returnValue;
        }
    }
}