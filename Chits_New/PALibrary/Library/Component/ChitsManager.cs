using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class ChitsManager
    {
        public static void AddChitsInfo(ChitsInfo chitsInfo)
        {
            ChitsDAO.AddChitsInfo(chitsInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateChitsInfo(ChitsInfo chitsInfo)
        {
            ChitsDAO.AddChitsInfo(chitsInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteChitsInfo(string chitNO)
        {
            ChitsDAO.DeleteChitsInfo(chitNO);
        }

        public static List<ChitsInfo> SearchChitsInfo(string chitNO, string chitName, decimal chitAmount, decimal installmentAmount, decimal noInstallments, string closed, int startRowIndex, int maximumRows)
        {
            return ChitsDAO.SearchChitsInfo(ChitsDAO.SearchConditions(chitNO, chitName, chitAmount, installmentAmount, noInstallments, closed), startRowIndex);
        }

        public static int SearchChitsInfoCount(string chitNO, string chitName, decimal chitAmount, decimal installmentAmount, decimal noInstallments, string closed, int startRowIndex, int maximumRows)
        {
            return ChitsDAO.SearchChitsInfoCount(ChitsDAO.SearchConditions(chitNO, chitName, chitAmount, installmentAmount, noInstallments, closed));
        }

        public static ChitsInfo GetChitsInfo(string chitNO)
        {
            return ChitsDAO.GetChitsInfo(chitNO);
        }

        public static List<ChitsInfo> GetChitsInfos()
        {
            return ChitsDAO.GetChitsInfos();
        }

    }
}