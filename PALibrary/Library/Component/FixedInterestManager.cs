using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class FixedInterestManager
    {
        public static void AddFixedInterestInfo(FixedInterestInfo fixedInterestInfo)
        {
            FixedInterestDAO.AddFixedInterestInfo(fixedInterestInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateFixedInterestInfo(FixedInterestInfo fixedInterestInfo)
        {
            FixedInterestDAO.AddFixedInterestInfo(fixedInterestInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteFixedInterestInfo(List<int> recordIDs)
        {
            foreach(int recordID in recordIDs)
            {
                FixedInterestDAO.DeleteFixedInterestInfo(recordID);
            }
        }

        public static List<FixedInterestInfo> SearchFixedInterestInfo(int recordID, string fDNO, DateTime paidDate, decimal interestAmount, string voucherNO, DateTime interestUpto, int startRowIndex, int maximumRows)
        {
            return FixedInterestDAO.SearchFixedInterestInfo(FixedInterestDAO.SearchConditions(recordID, fDNO, paidDate, interestAmount, voucherNO, interestUpto), startRowIndex);
        }

        public static int SearchFixedInterestInfoCount(int recordID, string fDNO, DateTime paidDate, decimal interestAmount, string voucherNO, DateTime interestUpto, int startRowIndex, int maximumRows)
        {
            return FixedInterestDAO.SearchFixedInterestInfoCount(FixedInterestDAO.SearchConditions(recordID, fDNO, paidDate, interestAmount, voucherNO, interestUpto));
        }

        public static FixedInterestInfo GetFixedInterestInfo(int recordID)
        {
            return FixedInterestDAO.GetFixedInterestInfo(recordID);
        }

        public static List<FixedInterestInfo> GetFixedInterestInfos(string fDNO)
        {
            return FixedInterestDAO.GetFixedInterestInfos(fDNO);
        }

    }
}