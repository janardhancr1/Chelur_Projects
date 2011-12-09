using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ChitsTransManager
    {
        public static void AddChitsTransInfo(ChitsTransInfo chitsTransInfo)
        {
            ChitsTransDAO.AddChitsTransInfo(chitsTransInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateChitsTransInfo(ChitsTransInfo chitsTransInfo)
        {
            ChitsTransDAO.AddChitsTransInfo(chitsTransInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteChitsTransInfo(int recordID)
        {
            ChitsTransDAO.DeleteChitsTransInfo(recordID);
        }

        public static List<ChitsTransInfo> SearchChitsTransInfo(string chitNO, int customerID, int installmentNO, decimal installmentAmount, DateTime date, int startRowIndex, int maximumRows)
        {
            return ChitsTransDAO.SearchChitsTransInfo(ChitsTransDAO.SearchConditions(chitNO, customerID, installmentNO, installmentAmount, date), startRowIndex);
        }

        public static int SearchChitsTransInfoCount(string chitNO, int customerID, int installmentNO, decimal installmentAmount, DateTime date, int startRowIndex, int maximumRows)
        {
            return ChitsTransDAO.SearchChitsTransInfoCount(ChitsTransDAO.SearchConditions(chitNO, customerID, installmentNO, installmentAmount, date));
        }

        public static ChitsTransInfo GetChitsTransInfo(int recordID)
        {
            return ChitsTransDAO.GetChitsTransInfo(recordID);
        }

        public static List<ChitsTransInfo> GetChitsTransInfos()
        {
            return ChitsTransDAO.GetChitsTransInfos();
        }

    }
}