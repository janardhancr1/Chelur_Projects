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

        public static void CloseChitsInfo(string chitNO)
        {
            ChitsDAO.CloseChitsInfo(chitNO);
        }

        public static List<ChitsInfo> SearchChitsInfo(string chitNO, string chitName, decimal chitAmount, int bidDate, decimal installmentAmount, decimal noInstallments, string closed, int startRowIndex, int maximumRows)
        {
            return ChitsDAO.SearchChitsInfo(ChitsDAO.SearchConditions(chitNO, chitName, chitAmount, bidDate, installmentAmount, noInstallments, closed), startRowIndex);
        }

        public static int SearchChitsInfoCount(string chitNO, string chitName, decimal chitAmount, int bidDate, decimal installmentAmount, decimal noInstallments, string closed, int startRowIndex, int maximumRows)
        {
            return ChitsDAO.SearchChitsInfoCount(ChitsDAO.SearchConditions(chitNO, chitName, chitAmount, bidDate, installmentAmount, noInstallments, closed));
        }

        public static ChitsInfo GetChitsInfo(string chitNO)
        {
            return ChitsDAO.GetChitsInfo(chitNO);
        }

        public static List<ChitsInfo> GetChitsInfos()
        {
            return ChitsDAO.GetChitsInfos();
        }

        public static List<ChitsInfo> GetChitsInfos(DateTime fromDate, DateTime toDate, string closed)
        {
            return ChitsDAO.GetChitsInfos(fromDate, toDate, closed);
        }

        public static List<ChitsInfo> GetChitsMonhlyDueStatement()
        {
            return ChitsDAO.GetChitsMonhlyDueStatement();
        }
    }
}