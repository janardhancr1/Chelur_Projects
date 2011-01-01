using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class BankBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            List<LedgersInfo> ledgers = LedgersManager.GetLedgersInfos();
            foreach (LedgersInfo ledger in ledgers)
            {
                if (ledger.GroupName == DBConstant.BANK_LEDGERS)
                {
                    ListItem item = new ListItem();
                    item.Text = ledger.LedgerName;
                    item.Value = ledger.LedgerID.ToString();
                    BankName.Items.Add(item);
                }
            }

            DBConstant appConstants = (DBConstant)Session["AppConstants"];
            FromYear.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            ToYear.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            if (BankName.SelectedValue.Length > 0)
            {
                DayBookInfo openingBalance = AccountsManager.GetBankBookOpeningBalance(appConstants.FinYearStart, Convert.ToInt32(BankName.SelectedValue));
                Display_OpeningBalance(openingBalance);
                List<DayBookInfo> monthlySummary = AccountsManager.GetBankMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, Convert.ToInt32(BankName.SelectedValue));
                Display_MonthlySummary(openingBalance, monthlySummary);
            }
        }
    }

    public void Display_OpeningBalance(DayBookInfo openingBalance)
    {
        if (openingBalance != null)
        {
            if (openingBalance.Credit > 0)
            {
                DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Credit.ToString("#0.00") + " Cr";
            }
            else if (openingBalance.Debit > 0)
            {
                DetailsTable.Rows[1].Cells[3].InnerText = openingBalance.Debit.ToString("#0.00") + " Dr";
            }
            else
            {
                DetailsTable.Rows[1].Cells[3].InnerText = "0.00 Dr";
            }
        }
        else
        {
            DetailsTable.Rows[1].Cells[3].InnerText = "0.00 Dr";
        }
    }

    public void BankName_Selected(object sender, EventArgs e)
    {
        DBConstant appConstants = (DBConstant)Session["AppConstants"];
        FromYear.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
        ToYear.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
        DayBookInfo openingBalance = AccountsManager.GetBankBookOpeningBalance(appConstants.FinYearStart, Convert.ToInt32(BankName.SelectedValue));
        Display_OpeningBalance(openingBalance);
        List<DayBookInfo> monthlySummary = AccountsManager.GetBankMonthlySummary(appConstants.FinYearStart, appConstants.FinYearEnd, Convert.ToInt32(BankName.SelectedValue));
        Display_MonthlySummary(openingBalance, monthlySummary);
    }

    public void Display_MonthlySummary(DayBookInfo openingBalance, List<DayBookInfo> monthlySummary)
    {
        decimal credit = 0;
        decimal debit = 0;
        decimal closingBalance;

        if (openingBalance != null)
        {
            credit = credit + openingBalance.Credit;
            debit = debit + openingBalance.Debit;
        }

        int i = 3;
        foreach (DayBookInfo month in monthlySummary)
        {
            DetailsTable.Rows[i].Cells[1].InnerText = month.Debit > 0 ? month.Debit.ToString("#0.00") : "";
            DetailsTable.Rows[i].Cells[2].InnerText = month.Credit > 0 ? month.Credit.ToString("#0.00") : "";

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
        Response.Redirect("ViewDetails.aspx?ledgerID=" + BankName.SelectedValue);
    }
}
