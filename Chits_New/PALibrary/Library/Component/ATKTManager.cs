using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ATKTManager
    {
        public static void AddATKTInfo(ATKTInfo aTKTInfo)
        {
            ATKTDAO.AddATKTInfo(aTKTInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateATKTInfo(ATKTInfo aTKTInfo)
        {
            ATKTDAO.AddATKTInfo(aTKTInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteATKTInfo(int recordID)
        {
            ATKTDAO.DeleteATKTInfo(recordID);
        }

        public static List<ATKTInfo> SearchATKTInfo(string aTKTNO, string partyName, DateTime aTKTDate, string tranType, decimal amount, string remarks, string closed, DateTime closedDate, int startRowIndex, int maximumRows)
        {
            return ATKTDAO.SearchATKTInfo(ATKTDAO.SearchConditions(aTKTNO, partyName, aTKTDate, tranType, amount, remarks, closed, closedDate), startRowIndex);
        }

        public static int SearchATKTInfoCount(string aTKTNO, string partyName, DateTime aTKTDate, string tranType, decimal amount, string remarks, string closed, DateTime closedDate, int startRowIndex, int maximumRows)
        {
            return ATKTDAO.SearchATKTInfoCount(ATKTDAO.SearchConditions(aTKTNO, partyName, aTKTDate, tranType, amount, remarks, closed, closedDate));
        }

        public static ATKTInfo GetATKTInfo(int recordID)
        {
            return ATKTDAO.GetATKTInfo(recordID);
        }

        public static List<ATKTInfo> GetATKTInfos()
        {
            return ATKTDAO.GetATKTInfos();
        }

        public static List<ATKTInfo> GetATKTInfos(DateTime fromDate, DateTime toDate, string closed)
        {
            return ATKTDAO.GetATKTInfos(fromDate, toDate, closed);
        }

        public static void CloseATKT(ATKTInfo aTKTInfo)
        {
            ATKTDAO.CloseATKT(aTKTInfo);
        }

    }
}