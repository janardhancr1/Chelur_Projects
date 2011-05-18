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

        public static List<ChitsBiddingInfo> SearchChitsBiddingInfo(int recordID, string chitNO, int installmentNO, decimal paidAmount, DateTime paidDate, int customerID, decimal leftAmount, int startRowIndex, int maximumRows)
        {
            return ChitsBiddingDAO.SearchChitsBiddingInfo(ChitsBiddingDAO.SearchConditions(recordID, chitNO, installmentNO, paidAmount, paidDate, customerID, leftAmount), startRowIndex);
        }

        public static int SearchChitsBiddingInfoCount(int recordID, string chitNO, int installmentNO, decimal paidAmount, DateTime paidDate, int customerID, decimal leftAmount, int startRowIndex, int maximumRows)
        {
            return ChitsBiddingDAO.SearchChitsBiddingInfoCount(ChitsBiddingDAO.SearchConditions(recordID, chitNO, installmentNO, paidAmount, paidDate, customerID, leftAmount));
        }

        public static ChitsBiddingInfo GetChitsBiddingInfo(int recordID)
        {
            return ChitsBiddingDAO.GetChitsBiddingInfo(recordID);
        }

        public static List<ChitsBiddingInfo> GetChitsBiddingInfos(string chitNo)
        {
            return ChitsBiddingDAO.GetChitsBiddingInfos(chitNo);
        }

    }
}