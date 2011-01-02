using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class FixedTransManager
    {
        public static void AddFixedTransInfo(FixedTransInfo fixedTransInfo)
        {
            FixedTransDAO.AddFixedTransInfo(fixedTransInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateFixedTransInfo(FixedTransInfo fixedTransInfo)
        {
            FixedTransDAO.AddFixedTransInfo(fixedTransInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteFixedTransInfo(int recordID)
        {
            FixedTransDAO.DeleteFixedTransInfo(recordID);
        }

        public static List<FixedTransInfo> SearchFixedTransInfo(int recordID, string fDNO, DateTime paidDate, decimal amount, string receiptNO, int startRowIndex, int maximumRows)
        {
            return FixedTransDAO.SearchFixedTransInfo(FixedTransDAO.SearchConditions(recordID, fDNO, paidDate, amount, receiptNO), startRowIndex);
        }

        public static int SearchFixedTransInfoCount(int recordID, string fDNO, DateTime paidDate, decimal amount, string receiptNO, int startRowIndex, int maximumRows)
        {
            return FixedTransDAO.SearchFixedTransInfoCount(FixedTransDAO.SearchConditions(recordID, fDNO, paidDate, amount, receiptNO));
        }

        public static FixedTransInfo GetFixedTransInfo(int recordID)
        {
            return FixedTransDAO.GetFixedTransInfo(recordID);
        }

        public static List<FixedTransInfo> GetFixedTransInfos()
        {
            return FixedTransDAO.GetFixedTransInfos();
        }

        public static List<FixedTransInfo> GetFixedTransInfos(string fDNO, decimal amount)
        {
           return FixedTransDAO.GetFixedTransInfos(fDNO, amount);
        }

    }
}