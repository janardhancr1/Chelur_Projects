using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class FDInterest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InterestAmount.Attributes.Add("onkeypress", "javascript:onlyFloat(this);");
        VoucherNo.Attributes.Add("onkeypress", "javascript:onlyDigits(this);");
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.Params["FDNo"] != null)
                {
                    FDNo.Value = Request.Params["FDNo"];
                    DBConstant appConstants = (DBConstant)Session["AppConstants"];

                    FixedDepositInfo fixedDeposit = FixedDepositManager.GetFixedDepositInfo(FDNo.Value);
                    if (fixedDeposit != null)
                    {

                        FDNo.Value = fixedDeposit.FDNO;
                        FDDate.Value = fixedDeposit.StartDate.ToString("dd/MM/yyyy");
                        CustomerName.Value = fixedDeposit.CustomerName;
                        CurrentBalance.Value = fixedDeposit.Balance.ToString();
                        Rate.Value = fixedDeposit.Rate.ToString("#0.00");

                        decimal currentBalance = fixedDeposit.Amount;
                        DateTime currentDate = fixedDeposit.StartDate;
                        LastPaidDate.Value = currentDate.ToString("dd/MM/yyyy");

                        List<FixedTransInfo> fixedTrans = FixedTransManager.GetFixedTransInfos(FDNo.Value, fixedDeposit.Amount);
                        HtmlTableRow row = null;
                        HtmlTableCell cell = null;

                        double totalInterest = 0;
                        foreach (FixedTransInfo fixedTran in fixedTrans)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = currentDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedTran.PaidDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);


                            cell = new HtmlTableCell();
                            cell.InnerText = currentBalance.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            TimeSpan diffDays = fixedTran.PaidDate - currentDate;
                            double interestAmt = (float)currentBalance * (float)fixedDeposit.Rate * (float)(diffDays.Days) / 36500;
                            totalInterest = totalInterest + interestAmt;

                            currentBalance = fixedTran.Balance;
                            currentDate = fixedTran.PaidDate;

                            cell = new HtmlTableCell();
                            cell.InnerText = diffDays.Days.ToString();
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = interestAmt.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            DetailsTable.Rows.Add(row);
                            LastPaidDate.Value = currentDate.ToString("dd/MM/yyyy");
                        }

                        if (fixedDeposit.Balance != 0 && currentDate != appConstants.CurrentDate)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = currentDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = appConstants.CurrentDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedDeposit.Balance.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            TimeSpan diffDays = appConstants.CurrentDate - currentDate;

                            cell = new HtmlTableCell();
                            cell.InnerText = diffDays.Days.ToString();
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            double interestAmt = (float)fixedDeposit.Balance * (float)fixedDeposit.Rate * (float)(diffDays.Days) / 36500;
                            totalInterest = totalInterest + interestAmt;

                            cell = new HtmlTableCell();
                            cell.InnerText = interestAmt.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            DetailsTable.Rows.Add(row);
                        }

                        TotalInterest.Value = totalInterest.ToString("#0.00");

                        List<FixedInterestInfo> fixedInterests = FixedInterestManager.GetFixedInterestInfos(FDNo.Value);
                        double totalPaid = 0;
                        foreach (FixedInterestInfo fixedInterest in fixedInterests)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedInterest.PaidDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedInterest.InterestUpto.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedInterest.VoucherNO;
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            totalPaid = totalPaid + (float)fixedInterest.InterestAmount;

                            cell = new HtmlTableCell();
                            cell.InnerText = fixedInterest.InterestAmount.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.Align = "center";
                            cell.InnerHtml = "<input type='checkbox' id='dcheck' transid='" + fixedInterest.RecordID + "' onclick='javascript:selectIDs();' />";
                            cell.Width = "15%";
                            row.Cells.Add(cell);

                            InterestTable.Rows.Add(row);
                            LastPaidDate.Value = fixedInterest.InterestUpto.ToString("dd/MM/yyyy");
                        }

                        InterestPaid.Value = totalPaid.ToString("#0.00");

                        double balanceInterest = totalInterest - totalPaid;
                        //BalanceInterest.Value = balanceInterest.ToString("#0.00");
                    }
                }
            }
        }
    }

    protected void Paid_Click(object sender, EventArgs e)
    {
        FixedInterestInfo fixedInterest = new FixedInterestInfo();
        fixedInterest.FDNO = FDNo.Value;
        fixedInterest.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        fixedInterest.VoucherNO = VoucherNo.Text;
        fixedInterest.InterestAmount = Convert.ToDecimal(InterestAmount.Text);
        fixedInterest.InterestUpto = DateTime.ParseExact(PaidUpto.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        try
        {
            FixedInterestManager.AddFixedInterestInfo(fixedInterest);
            Response.Redirect("FDInterest.aspx?FDNo=" + FDNo.Value);
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        string[] transIDs = selectdIDs.Value.Split(',');
        List<int> recordIDs = new List<int>();
        for (int i = 1; i < transIDs.Length; i++)
        {
            recordIDs.Add(Convert.ToInt32(transIDs[i]));
        }
        try
        {
            FixedInterestManager.DeleteFixedInterestInfo(recordIDs);
            Response.Redirect("FDInterest.aspx?FDNo=" + FDNo.Value);
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }
}
