using System;
using System.Collections.Generic;
using System.Text;

using PALibrary.Library.Model;
using PALibrary.Library.Utils;

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
                            chitReport.DiscountAmount = trans[0].DiscountAmount;
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

        public List<ChitsReportInfo> GetMembers(string chitNo, string type)
        {
            List<ChitsReportInfo> chits = new List<ChitsReportInfo>();
            ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(chitNo);
            List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(chitNo);

            if (type == DBConstant.CHIT_UNPAID)
            {
                int totalInstallments = ChitsBiddingManager.SearchChitsBiddingInfoCount(chitNo, 0, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                totalInstallments++;
                foreach (ChitsParticipateInfo customer in customers)
                {
                    decimal totalAmount = 0;
                    string installments = "";
                    bool found = false;

                    for (int i = 1; i < totalInstallments; i++)
                    {
                        List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(chitNo, customer.CustomerID, i, 0, new DateTime(), -1, 0);

                        if (trans.Count == 0)
                        {
                            found = true;

                            decimal installAmount = chitsInfo.InstallmentAmount;
                            if (i > 1 && i < totalInstallments)
                            {
                                List<ChitsBiddingInfo> lastBidding = ChitsBiddingManager.SearchChitsBiddingInfo(chitNo, i - 1, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                                decimal comm = chitsInfo.ChitAmount * chitsInfo.ChitCommission / 100;
                                decimal leftAmount = lastBidding[0].LeftAmount - comm;
                                installAmount = (chitsInfo.InstallmentAmount - (leftAmount / chitsInfo.NoInstallments));

                            }

                            totalAmount += installAmount;
                            installments += i.ToString() + ", ";

                        }
                    }
                    if (found == true)
                    {
                        ChitsReportInfo chit = new ChitsReportInfo();

                        chit.CustomerName = customer.CustomerName;
                        chit.CustomerAddress = customer.CustomerAddress;
                        chit.PaidAmount = totalAmount;
                        chit.ChitName = installments;

                        chits.Add(chit);

                    }
                }
            }

            if (type == DBConstant.CHIT_BIDDER)
            {
                foreach (ChitsParticipateInfo customer in customers)
                {
                    List<ChitsBiddingInfo> trans = ChitsBiddingManager.SearchChitsBiddingInfo(chitNo, 0, 0, new DateTime(), new DateTime(), customer.CustomerID, 0, -1, 0);

                    if (trans.Count > 0)
                    {
                        ChitsReportInfo chit = new ChitsReportInfo();

                        chit.CustomerName = customer.CustomerName;
                        chit.CustomerAddress = customer.CustomerAddress;
                        chit.InstallmentNO = trans[0].InstallmentNO;
                        chit.BidDate = trans[0].BidDate;
                        chit.PaidDate = trans[0].PaidDate;
                        chit.PaidAmount = trans[0].PaidAmount;

                        chits.Add(chit);
                    }
                }
                chits.Sort(new ChitNoComparer());
            }

            if (type == DBConstant.CHIT_UNBIDDER)
            {
                foreach (ChitsParticipateInfo customer in customers)
                {
                    List<ChitsBiddingInfo> trans = ChitsBiddingManager.SearchChitsBiddingInfo(chitNo, 0, 0, new DateTime(), new DateTime(), customer.CustomerID, 0, -1, 0);

                    if (trans.Count == 0)
                    {
                         int compBidCnt = ChitsCompanyBiddingManager.SearchChitsCompanyBiddingInfoCount(0, chitNo, 0, 0, new DateTime(), customer.CustomerID, -1, 0);
                         if (compBidCnt == 0)
                         {
                             ChitsReportInfo chit = new ChitsReportInfo();

                             chit.CustomerName = customer.CustomerName;
                             chit.CustomerAddress = customer.CustomerAddress;
                             chits.Add(chit);
                         }
                    }
                }
            }

            return chits;
        }
    }

    public class ChitNoComparer : IComparer<ChitsReportInfo>
    {
        public int Compare(ChitsReportInfo ta, ChitsReportInfo tb)
        {
            return ta.InstallmentNO.CompareTo(tb.InstallmentNO);
        }
    }
}
