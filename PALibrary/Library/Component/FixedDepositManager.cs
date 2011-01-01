using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class FixedDepositManager
    {
        public static void AddFixedDepositInfo(FixedDepositInfo fixedDepositInfo)
        {
            FixedDepositDAO.AddFixedDepositInfo(fixedDepositInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateFixedDepositInfo(FixedDepositInfo fixedDepositInfo)
        {
            FixedDepositDAO.AddFixedDepositInfo(fixedDepositInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteFixedDepositInfo(string fDNO)
        {
            FixedDepositDAO.DeleteFixedDepositInfo(fDNO);
        }

        public static List<FixedDepositInfo> SearchFixedDepositInfo(string fDNO, int customerID, DateTime startDate, string nomineeName, string relationship, decimal amount, decimal rate, string closed, int startRowIndex, int maximumRows)
        {
            return FixedDepositDAO.SearchFixedDepositInfo(FixedDepositDAO.SearchConditions(fDNO, customerID, startDate, nomineeName, relationship, amount, rate, closed), startRowIndex);
        }

        public static int SearchFixedDepositInfoCount(string fDNO, int customerID, DateTime startDate, string nomineeName, string relationship, decimal amount, decimal rate, string closed, int startRowIndex, int maximumRows)
        {
            return FixedDepositDAO.SearchFixedDepositInfoCount(FixedDepositDAO.SearchConditions(fDNO, customerID, startDate, nomineeName, relationship, amount, rate, closed));
        }

        public static FixedDepositInfo GetFixedDepositInfo(string fDNO)
        {
            return FixedDepositDAO.GetFixedDepositInfo(fDNO);
        }

        public static List<FixedDepositInfo> GetFixedDepositInfos()
        {
            return FixedDepositDAO.GetFixedDepositInfos();
        }

        public static List<FixedDepositInfo> GetFixedDepositInfos(DateTime fromDate, DateTime toDate, string closed, string orderBy)
        {
            List<FixedDepositInfo> details = FixedDepositDAO.GetFixedDepositInfos(fromDate, toDate, closed);
            if (orderBy.ToLower().Equals("balance"))
                details.Sort(new FDBalanceComaparer());
            return details;
        }

    }

    public class FDBalanceComaparer : IComparer<FixedDepositInfo>
    {
        public int Compare(FixedDepositInfo a, FixedDepositInfo b)
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
}