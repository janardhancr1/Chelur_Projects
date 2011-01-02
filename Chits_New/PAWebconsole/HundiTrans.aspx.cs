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

public partial class HundiTrans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Amount.Attributes.Add("onkeypress", "javascript:onlyFloat(this);");
        Amount.Attributes.Add("onblur", "javascript:calcualte();");
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

                    HundiLoanManager hundiManager = new HundiLoanManager();
                    if (Request.Params["transid"] != null)
                    {
                        int transID = Convert.ToInt32(Request.Params["transid"]);
                        HundiLoanManager.DeleteHundiTransInfo(transID);
                    }

                    HundiLoanInfo hundiLoan = HundiLoanManager.GetHundiLoanInfo(HLLoanNo.Value);
                    if (hundiLoan != null)
                    {
                        HLLoanNo.Value = hundiLoan.HlLoanno;
                        CustomerName.Value = hundiLoan.CustomerName;
                        LoanAmount.Value = hundiLoan.LoanAmount.ToString();
                        CurrentBalance.Value = hundiLoan.Balance.ToString();
                        AccountNo.Value = hundiLoan.AccountNo;
                    }
                }
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='LightBlue';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            e.Row.Cells[4].Attributes.Add("onclick", "javascript:return confirm('Date : " + e.Row.Cells[0].Text + " Receipt no:" + e.Row.Cells[1].Text + "\\nAre you sure to delete?');");
        }
    }

    public void Add_Click(object sender, EventArgs e)
    {
        HundiTransInfo hundiTrans = new HundiTransInfo();
        hundiTrans.HlLoanno = HLLoanNo.Value;
        hundiTrans.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
        hundiTrans.ReceiptNO = ReceiptNo.Value;
        hundiTrans.Amount = Convert.ToDecimal(Amount.Text);
        hundiTrans.Balance = Convert.ToDecimal(CurrentBalance.Value) - hundiTrans.Amount;

        HundiLoanManager.AddHundiTransInfo(hundiTrans);
        Response.Redirect("HundiTrans.aspx?HlLoanno=" + HLLoanNo.Value);
    }
}
