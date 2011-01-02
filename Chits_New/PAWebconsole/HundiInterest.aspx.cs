using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

public partial class HundiInterest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InterestAmount.Attributes.Add("onkeypress", "javascript:onlyFloat(this);");
        ReceiptNo.Attributes.Add("onkeypress", "javascript:onlyDigits(this);");
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {

                if (Request.Params["HlLoanno"] != null)
                {
                    HLLoanNo.Value = Request.Params["HlLoanno"];
                    CloseButton.Attributes.Add("onclick", "window.location.href='ViewHundiLoan.aspx?HlLoanno=" + HLLoanNo.Value + "';");
                    DBConstant appConstants = (DBConstant)Session["AppConstants"];

                    HundiLoanInfo hundiLoan = HundiLoanManager.GetHundiLoanInfo(HLLoanNo.Value);
                    if (hundiLoan != null)
                    {
                        HLLoanNo.Value = hundiLoan.HlLoanno;
                        CustomerName.Value = hundiLoan.CustomerName;
                        LoanAmount.Value = hundiLoan.LoanAmount.ToString();
                        CurrentBalance.Value = hundiLoan.Balance.ToString();
                        AccountNo.Value = hundiLoan.AccountNo;
                        Rate.Value = hundiLoan.Rate.ToString();

                        decimal currentBalance = hundiLoan.LoanAmount;
                        DateTime currentDate = hundiLoan.LoanDate;
                        LastPaidDate.Value = currentDate.ToString("dd/MM/yyyy");

                        List<HundiTransInfo> hundiTrans = HundiLoanManager.GetHundiTransInfos(HLLoanNo.Value, hundiLoan.LoanAmount);
                        HtmlTableRow row = null;
                        HtmlTableCell cell = null;

                        double totalInterest = 0;
                        foreach (HundiTransInfo hundiTran in hundiTrans)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = currentDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = hundiTran.PaidDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);


                            cell = new HtmlTableCell();
                            cell.InnerText = currentBalance.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            TimeSpan diffDays = hundiTran.PaidDate - currentDate;
                            double interestAmt = (float)currentBalance * (float)hundiLoan.Rate * (float)(diffDays.Days) / 36500;
                            totalInterest = totalInterest + interestAmt;

                            currentBalance = hundiTran.Balance;
                            currentDate = hundiTran.PaidDate;

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

                        if (hundiLoan.Balance != 0 && currentDate != appConstants.CurrentDate)
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
                            cell.InnerText = hundiLoan.Balance.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            TimeSpan diffDays = appConstants.CurrentDate - currentDate;

                            cell = new HtmlTableCell();
                            cell.InnerText = diffDays.Days.ToString();
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            double interestAmt = (float)hundiLoan.Balance * (float)hundiLoan.Rate * (float)(diffDays.Days) / 36500;
                            totalInterest = totalInterest + interestAmt;

                            cell = new HtmlTableCell();
                            cell.InnerText = interestAmt.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            DetailsTable.Rows.Add(row);
                        }

                        TotalInterest.Value = totalInterest.ToString("#0.00");

                        List<HundiInterestInfo> hundiInterests = HundiLoanManager.GetHundiInterestInfos(HLLoanNo.Value);
                        double totalPaid = 0;
                        foreach (HundiInterestInfo hundiInterest in hundiInterests)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = hundiInterest.PaidDate.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = hundiInterest.InterestUpto.ToString("dd/MM/yyyy");
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = hundiInterest.ReceiptNO;
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            totalPaid = totalPaid + (float)hundiInterest.InterestAmount;

                            cell = new HtmlTableCell();
                            cell.InnerText = hundiInterest.InterestAmount.ToString("#0.00");
                            cell.Align = "right";
                            cell.Width = "20%";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.Align = "center";
                            cell.InnerHtml = "<input type='checkbox' id='dcheck' transid='" + hundiInterest.RecordID + "' onclick='javascript:selectIDs();' />";
                            cell.Width = "15%";
                            row.Cells.Add(cell);

                            InterestTable.Rows.Add(row);
                            LastPaidDate.Value = hundiInterest.InterestUpto.ToString("dd/MM/yyyy");
                        }

                        InterestCollected.Value = totalPaid.ToString("#0.00");

                        double balanceInterest = totalInterest - totalPaid;
                        //BalanceInterest.Value = balanceInterest.ToString("#0.00");
                    }
                }
            }
        }
    }

    public void Add_Click(object sender, EventArgs e)
    {
        HundiInterestInfo hundiInterest = new HundiInterestInfo();
        hundiInterest.HlLoanno =HLLoanNo.Value;
        hundiInterest.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
        hundiInterest.ReceiptNO = ReceiptNo.Text;
        hundiInterest.InterestAmount = Convert.ToDecimal(InterestAmount.Text);
        hundiInterest.InterestUpto = DateTime.ParseExact(PaidUpto.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        try
        {
            HundiLoanManager.AddHundiInterestInfo(hundiInterest, Convert.ToDecimal(CurrentBalance.Value));
            Response.Redirect("HundiInterest.aspx?HlLoanno=" + HLLoanNo.Value);
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }

    public void DeleteClick(object sender, EventArgs e)
    {
        string[] transIDs = selectdIDs.Value.Split(',');
        List<int> recordIDs = new List<int>();
        for (int i = 1; i < transIDs.Length; i++)
        {
            recordIDs.Add(Convert.ToInt32(transIDs[i]));
        }
        try
        {
            HundiLoanManager hundiManager = new HundiLoanManager();
            HundiLoanManager.DeleteHundiInterestInfo(recordIDs);
            Response.Redirect("HundiInterest.aspx?HlLoanno=" + HLLoanNo.Value);
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }
}
