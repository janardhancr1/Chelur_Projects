using System;
using System.Collections.Generic;
using System.Text;

using PALibrary.Library.Model;

namespace PALibrary.Library.Component
{
    public class ChitsReportManager
    {
        public List<ChitsReportInfo> GetChitCustomerAccount(string chitNo, int customerID)
        {
            List<ChitsReportInfo> chitsReports = new List<ChitsReportInfo>();
            ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(chitNo);
            if (chitsInfo != null)
            {
                List<ChitsBiddingInfo> biddings = ChitsBiddingManager.SearchChitsBiddingInfo(chitNo, 0, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                int totalInstallments = biddings.Count + 1;

                List<ChitsParticipateInfo> customers = ChitsParticipateManager.SearchChitsParticipateInfo(0, chitNo, customerID, -1, 0);
                if (customers.Count > 0)
                {
                    decimal paidTotal = 0;
                    decimal bidTotal = 0;

                    for (int i = 1; i < totalInstallments; i++)
                    {
                        ChitsReportInfo chitReport = new ChitsReportInfo();
                        chitReport.ChitName = chitsInfo.ChitName;
                        chitReport.InstallmentNO = i;

                        chitReport.CustomerName = customers[0].CustomerName;
                        chitReport.CustomerAddress = customers[0].CustomerAddress;

                        List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(chitNo, customerID, i, 0, new DateTime(), -1, 0);

                        if (trans.Count > 0)
                        {
                            chitReport.InstallmentAmount = trans[0].InstallmentAmount;
                            chitReport.PaidDate = trans[0].Date;
                            paidTotal += trans[0].InstallmentAmount;
                        }

                        List<ChitsBiddingInfo> bid = ChitsBiddingManager.SearchChitsBiddingInfo(chitNo, i, 0, new DateTime(), new DateTime(), customerID, 0, -1, 0);

                        if (bid.Count > 0)
                        {
                            chitReport.PaidAmount = bid[0].PaidAmount;
                            chitReport.BidDate = bid[0].BidDate;
                            bidTotal += bid[0].PaidAmount;
                        }

                        chitsReports.Add(chitReport);
                    }
                }
            }
            return chitsReports;
        }
    }
}
