using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ChitsCompanyBiddingManager
    {
        public static void AddChitsCompanyBiddingInfo(ChitsCompanyBiddingInfo chitsCompanyBiddingInfo)
        {
            ChitsCompanyBiddingDAO.AddChitsCompanyBiddingInfo(chitsCompanyBiddingInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateChitsCompanyBiddingInfo(ChitsCompanyBiddingInfo chitsCompanyBiddingInfo)
        {
            ChitsCompanyBiddingDAO.AddChitsCompanyBiddingInfo(chitsCompanyBiddingInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteChitsCompanyBiddingInfo(int recordID)
        {
            ChitsCompanyBiddingDAO.DeleteChitsCompanyBiddingInfo(recordID);
        }

        public static List<ChitsCompanyBiddingInfo> SearchChitsCompanyBiddingInfo(int recordID, string chitNO, int installmentNO, decimal paidAmount, DateTime paidDate, int customerID, int startRowIndex, int maximumRows)
        {
            return ChitsCompanyBiddingDAO.SearchChitsCompanyBiddingInfo(ChitsCompanyBiddingDAO.SearchConditions(recordID, chitNO, installmentNO, paidAmount, paidDate, customerID), startRowIndex);
        }

        public static int SearchChitsCompanyBiddingInfoCount(int recordID, string chitNO, int installmentNO, decimal paidAmount, DateTime paidDate, int customerID, int startRowIndex, int maximumRows)
        {
            return ChitsCompanyBiddingDAO.SearchChitsCompanyBiddingInfoCount(ChitsCompanyBiddingDAO.SearchConditions(recordID, chitNO, installmentNO, paidAmount, paidDate, customerID));
        }

        public static ChitsCompanyBiddingInfo GetChitsCompanyBiddingInfo(int recordID)
        {
            return ChitsCompanyBiddingDAO.GetChitsCompanyBiddingInfo(recordID);
        }

        public static List<ChitsCompanyBiddingInfo> GetChitsCompanyBiddingInfos()
        {
            return ChitsCompanyBiddingDAO.GetChitsCompanyBiddingInfos();
        }

    }
}