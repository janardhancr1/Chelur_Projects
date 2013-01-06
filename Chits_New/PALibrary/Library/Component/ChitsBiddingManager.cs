using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ChitsBiddingManager
    {
        public static void AddChitsBiddingInfo(ChitsBiddingInfo chitsBiddingInfo)
        {
            ChitsBiddingDAO.AddChitsBiddingInfo(chitsBiddingInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateChitsBiddingInfo(ChitsBiddingInfo chitsBiddingInfo)
        {
            ChitsBiddingDAO.AddChitsBiddingInfo(chitsBiddingInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteChitsBiddingInfo(int recordID)
        {
            ChitsBiddingDAO.DeleteChitsBiddingInfo(recordID);
        }

        public static List<ChitsBiddingInfo> SearchChitsBiddingInfo(string chitNO, int installmentNO, decimal paidAmount, DateTime bidDate, DateTime paidDate, int customerID, decimal leftAmount, int startRowIndex, int maximumRows)
        {
            return ChitsBiddingDAO.SearchChitsBiddingInfo(ChitsBiddingDAO.SearchConditions(chitNO, installmentNO, paidAmount, bidDate, paidDate, customerID, leftAmount), startRowIndex);
        }

        public static int SearchChitsBiddingInfoCount(string chitNO, int installmentNO, decimal paidAmount, DateTime bidDate, DateTime paidDate, int customerID, decimal leftAmount, int startRowIndex, int maximumRows)
        {
            return ChitsBiddingDAO.SearchChitsBiddingInfoCount(ChitsBiddingDAO.SearchConditions(chitNO, installmentNO, paidAmount, bidDate, paidDate, customerID, leftAmount));
        }

        public static int GetLastBiddingInstallment(string chitNO)
        {
            return ChitsBiddingDAO.GetLastBiddingInstallment(chitNO);
        }

        public static ChitsBiddingInfo GetChitsBiddingInfo(int recordID)
        {
            return ChitsBiddingDAO.GetChitsBiddingInfo(recordID);
        }

        public static List<ChitsBiddingInfo> GetChitsBiddingInfos(string chitNO)
        {
            return ChitsBiddingDAO.GetChitsBiddingInfos(chitNO);
        }

        public static List<ChitsBiddingInfo> GetChitsCompanyBiddingInfos(DateTime fromDate, DateTime toDate)
        {
            return ChitsBiddingDAO.GetChitsCompanyBiddingInfos(fromDate, toDate);
        }

        public static List<ChitsBiddingInfo> GetChitsCompanyBiddingInfos(string chitNO)
        {
            return ChitsBiddingDAO.GetChitsCompanyBiddingInfos(chitNO);
        }

    }
}