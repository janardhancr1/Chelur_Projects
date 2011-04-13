using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ChitsParticipateManager
    {
        public static void AddChitsParticipateInfo(ChitsParticipateInfo chitsParticipateInfo)
        {
            ChitsParticipateDAO.AddChitsParticipateInfo(chitsParticipateInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateChitsParticipateInfo(ChitsParticipateInfo chitsParticipateInfo)
        {
            ChitsParticipateDAO.AddChitsParticipateInfo(chitsParticipateInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteChitsParticipateInfo(int recordID)
        {
            ChitsParticipateDAO.DeleteChitsParticipateInfo(recordID);
        }

        public static List<ChitsParticipateInfo> SearchChitsParticipateInfo(int recordID, string chitNO, int customerID, int startRowIndex, int maximumRows)
        {
            return ChitsParticipateDAO.SearchChitsParticipateInfo(ChitsParticipateDAO.SearchConditions(recordID, chitNO, customerID), startRowIndex);
        }

        public static int SearchChitsParticipateInfoCount(int recordID, string chitNO, int customerID, int startRowIndex, int maximumRows)
        {
            return ChitsParticipateDAO.SearchChitsParticipateInfoCount(ChitsParticipateDAO.SearchConditions(recordID, chitNO, customerID));
        }

        public static ChitsParticipateInfo GetChitsParticipateInfo(int recordID)
        {
            return ChitsParticipateDAO.GetChitsParticipateInfo(recordID);
        }

        public static List<ChitsParticipateInfo> GetChitsParticipateInfos(string chitNo)
        {
            return ChitsParticipateDAO.GetChitsParticipateInfos(chitNo);
        }

    }
}