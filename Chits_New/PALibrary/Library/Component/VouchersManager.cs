using System;
using System.Collections.Generic;

using PALibrary.Library.DAO;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

namespace PALibrary.Library.Component
{
    public class VouchersManager
    {
        public static string GetNextVoucherID()
        {
            return VouchersDAO.GetNextVoucherID();
        }

        public static void AddVouchersInfo(VouchersInfo vouchersInfo)
        {
            VouchersDAO.AddVouchersInfo(vouchersInfo, DBConstant.MODE_ADD);
        }

        public static void UpdateVouchersInfo(VouchersInfo vouchersInfo)
        {
            VouchersDAO.AddVouchersInfo(vouchersInfo, DBConstant.MODE_UPDATE);
        }

        public static void DeleteVouchersInfo(int voucherID)
        {
            VouchersDAO.DeleteVouchersInfo(voucherID);
        }

        public static List<VouchersInfo> SearchVouchersInfo(DateTime voucherDate, int voucherType, int voucherNO, int fromLedger, int toLedger, string narration, int startRowIndex, int maximumRows)
        {
            return VouchersDAO.SearchVouchersInfo(VouchersDAO.SearchConditions(voucherDate, voucherType, voucherNO, fromLedger, toLedger, narration), startRowIndex);
        }

        public static int SearchVouchersInfoCount(DateTime voucherDate, int voucherType, int voucherNO, int fromLedger, int toLedger, string narration, int startRowIndex, int maximumRows)
        {
            return VouchersDAO.SearchVouchersInfoCount(VouchersDAO.SearchConditions(voucherDate, voucherType, voucherNO, fromLedger, toLedger, narration));
        }

        public static VouchersInfo GetVouchersInfo(int voucherID)
        {
            return VouchersDAO.GetVouchersInfo(voucherID);
        }

        public static List<VouchersInfo> GetVouchersInfos()
        {
            return VouchersDAO.GetVouchersInfos();
        }

        public static List<VouchersInfo> GetVouchersInfos(DateTime fromDate, DateTime toDate)
        {
            return VouchersDAO.GetVouchers(fromDate, toDate, 0);
        }

    }
}