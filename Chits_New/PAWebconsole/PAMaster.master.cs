using System;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class PAMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AppConstants"] != null)
        {
            DBConstant appConstants = (DBConstant)Session["AppConstants"];
            CurrentDate.Text = "CurrentDate : " + appConstants.CurrentDate.ToString("dd/MM/yyyy");
            DayBookInfo closingBalance = AccountsManager.GetCashBookOpeningBalance(appConstants.CurrentDate.AddDays(1));
            if (closingBalance != null)
                if (closingBalance.Debit > 0)
                    CashInHand.Text = closingBalance.Debit.ToString("#0.00") + " Dr";
                else if(closingBalance.Credit > 0)
                    CashInHand.Text = closingBalance.Credit.ToString("#0.00") + " Cr";
                else
                    CashInHand.Text = closingBalance.Debit.ToString("#0.00") + " Dr";

        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}
