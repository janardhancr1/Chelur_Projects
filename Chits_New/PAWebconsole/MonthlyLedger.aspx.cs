using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class MonthlyLedger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            DBConstant appConstants = (DBConstant)Session["AppConstants"];
            FromYear.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            ToYear.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            if (Request.Params["type"] != null)
            {
                Ledger.Value = Request.Params["type"].ToString();
                DayBookInfo openingBalance = null;
                List<DayBookInfo> monthlySummary = null;
                switch (Request.Params["type"].ToString())
                {
                    case "1":
                        LedgerName.Text = "Hundi Loan Ledger";
                        openingBalance = AccountsManager.GetHundiLoanOpeningBalance(appConstants.FinYearStart, "HL", DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, "HL", DBConstant.ACCOUNT_PERIOD, 1);
                        break;
                    case "2":
                        LedgerName.Text = "Fixed Deposit Ledger";
                        openingBalance = AccountsManager.GetFixedDepositOpeningBalance(appConstants.FinYearStart, "FD", DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, "FD", DBConstant.ACCOUNT_PERIOD, 2);
                        break;
                    case "3":
                        LedgerName.Text = "ATKT Ledger";
                        openingBalance = AccountsManager.GetATKTOpeningBalance(appConstants.FinYearStart, "ATKT", DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, "ATKT", DBConstant.ACCOUNT_PERIOD, 3);
                        break;
                    case "4":
                        LedgerName.Text = "Chits Ledger";
                        openingBalance = AccountsManager.GetChitsOpeniningBalance(appConstants.FinYearStart, "CHIT", DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, "CHIT", DBConstant.ACCOUNT_PERIOD, 4);
                        break;
                    case "5":
                        LedgerName.Text = "Interest Collected Ledger";
                        openingBalance = AccountsManager.GetInterestOpeningBalance(appConstants.FinYearStart, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_PERIOD, 5);
                        break;
                    case "6":
                        LedgerName.Text = "Interest Paid Ledger";
                        openingBalance = AccountsManager.GetInterestPaidOpeningBalance(appConstants.FinYearStart, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_PERIOD, 6);
                        break;
                    case "7":
                        LedgerName.Text = "Chit Commission Ledger";
                        openingBalance = AccountsManager.GetChitCommissionOpeningBalance(appConstants.FinYearStart, DBConstant.CHIT_COMMISSION_LEDGER, DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, DBConstant.CHIT_COMMISSION_LEDGER, DBConstant.ACCOUNT_PERIOD, 7);
                        break;
                    case "8":
                        LedgerName.Text = "Chit Discount Ledger";
                        openingBalance = AccountsManager.GetChitDiscountOpeningBalance(appConstants.FinYearStart, DBConstant.CHIT_DISCOUNT_LEDGER, DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, DBConstant.CHIT_DISCOUNT_LEDGER, DBConstant.ACCOUNT_PERIOD, 8);
                        break;
                    case "9":
                        LedgerName.Text = "Company Bidding Ledger";
                        openingBalance = AccountsManager.GetCompBiddingOpeningBalance(appConstants.FinYearStart, DBConstant.COMPANY_BIDDING_LEDGER, DBConstant.ACCOUNT_OPENING);
                        monthlySummary = AccountsManager.GetMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, DBConstant.COMPANY_BIDDING_LEDGER, DBConstant.ACCOUNT_PERIOD, 9);
                        break;
                    case "10":
                        //if (Request.Params["ledgerid"] != null)
                        //{
                        //    LedgerID.Value = Request.Params["ledgerid"].ToString();
                        //    LedgerInfo ledger = ledgerManager.GetLedger(Convert.ToInt32(LedgerID.Value));
                        //    LedgerName.Text = ledger.LedgerName;
                        //    openingBalance = accountManager.GetLedgerOpeningBalance(appConstants.FinYearStart,
                        //                                                            LedgerName.Text);
                        //    monthlySummary = accountManager.GetMonthlySummary(appConstants.FinYearStart,
                        //                                                      appConstants.FinYearEnd, LedgerName.Text,
                        //                                                      company.CompID, DBConstant.ACCOUNT_LEDGER,
                        //                                                      10);
                        //}
                        break;
                    case "11":
                        //LedgerName.Text = "Auction Profit Ledger";
                        //openingBalance = accountManager.GetAuctionProfitOpeningBalance(appConstants.FinYearStart);
                        //monthlySummary = accountManager.GetMonthlySummary(appConstants.FinYearStart,
                        //                                                  appConstants.FinYearEnd, LedgerName.Text,
                        //                                                  company.CompID, DBConstant.ACCOUNT_PERIOD, 13);
                        break;
                }
                Display_OpeningBalance(openingBalance, Request.Params["type"]);
                Display_MonthlySummary(openingBalance, monthlySummary, Request.Params["type"]);
            }
        }
    }

    public void Display_OpeningBalance(DayBookInfo openingBalance, string type)
    {
        if (openingBalance != null)
        {
            if (type.Equals("3") || type.Equals("5") || type.Equals("6"))
            {
                if (openingBalance.Credit > 0)
                {
                    DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Credit.ToString("#0.00") + " Cr";
                }
                else if (openingBalance.Debit > 0)
                {
                    DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Debit.ToString("#0.00") + " Dr";
                }
            }
            else
            {
                if (openingBalance.Credit > 0)
                {
                    DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Credit.ToString("#0.00") + " Dr";
                }
                else if (openingBalance.Debit > 0)
                {
                    DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Debit.ToString("#0.00") + " Cr";
                }
            }
        }
    }

    public void Display_MonthlySummary(DayBookInfo openingBalance, List<DayBookInfo> monthlySummary, string type)
    {
        decimal credit = 0;
        decimal debit = 0;
        decimal closingBalance = 0;

        if (openingBalance != null)
        {
            if (type.Equals("3") || type.Equals("5") || type.Equals("6"))
            {
                credit = credit + openingBalance.Credit;
                debit = debit + openingBalance.Debit;
            }
            else
            {
                credit = credit + openingBalance.Debit;
                debit = debit + openingBalance.Credit;
            }
        }

        int i = 3;
        foreach (DayBookInfo month in monthlySummary)
        {
            if (month.Debit > 0)
                DetailsTable.Rows[i].Cells[1].InnerText = month.Debit.ToString("#0.00");
            if (month.Credit > 0)
                DetailsTable.Rows[i].Cells[2].InnerText = month.Credit.ToString("#0.00");

            credit = credit + month.Credit;
            debit = debit + month.Debit;

            if (credit > debit)
            {
                closingBalance = credit - debit;
                DetailsTable.Rows[i].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Cr";
                DetailsTable.Rows[16].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Cr";
            }
            else if (debit > credit)
            {
                closingBalance = debit - credit;
                DetailsTable.Rows[i].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Dr";
                DetailsTable.Rows[16].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Dr";
            }
            else
            {
                closingBalance = debit - credit;
                DetailsTable.Rows[i].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Dr";
                DetailsTable.Rows[16].Cells[3].InnerText = closingBalance.ToString("#0.00") + " Dr";
            }
            i++;
        }

        DetailsTable.Rows[16].Cells[1].InnerText = debit.ToString("#0.00");
        DetailsTable.Rows[16].Cells[2].InnerText = credit.ToString("#0.00");
    }


    public void Details_Click(object sender, EventArgs e)
    {
        if (Ledger.Value.Equals("10") || Ledger.Value.Equals("11"))
            Response.Redirect("ViewDetails.aspx?type=" + Ledger.Value + "&ledgerid=" + LedgerID.Value);
        else
            Response.Redirect("ViewDetails.aspx?type=" + Ledger.Value);
    }
}
