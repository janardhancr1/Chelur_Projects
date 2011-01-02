using System;
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
using PALibrary.Library.Model;

public partial class FDTrans : System.Web.UI.Page
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
            Amount.Attributes.Add("onkeypress", "javascript:onlyFloat(this);");
            Amount.Attributes.Add("onblur", "javascript:calcualte();");
            if (!IsPostBack)
            {
                if (Request.Params["FDNo"] != null)
                {
                    FDNo.Value = Request.Params["FDNo"];

                    if (Request.Params["transid"] != null)
                    {
                        int transID = Convert.ToInt32(Request.Params["transid"]);
                        FixedTransManager.DeleteFixedTransInfo(transID);
                    }

                    FixedDepositInfo fixedDeposit = FixedDepositManager.GetFixedDepositInfo(FDNo.Value);
                    if (fixedDeposit != null)
                    {

                        FDNo.Value = fixedDeposit.FDNO;
                        FDDate.Value = fixedDeposit.StartDate.ToString("dd/MM/yyyy");
                        CustomerName.Value = fixedDeposit.CustomerName;
                        CurrentBalance.Value = fixedDeposit.Balance.ToString();
                        Rate.Value = fixedDeposit.Rate.ToString("#0.00");
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

    protected void Add_Click(object sender, EventArgs e)
    {
        FixedTransInfo transInfo = new FixedTransInfo();
        transInfo.FDNO = FDNo.Value;
        transInfo.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        transInfo.ReceiptNO = ReceiptNo.Value;
        transInfo.Amount = Convert.ToDecimal(Amount.Text);
        transInfo.Balance = Convert.ToDecimal(CurrentBalance.Value) - transInfo.Amount;

        FixedTransManager.AddFixedTransInfo(transInfo);
        Response.Redirect("FDTrans.aspx?FDNo=" + FDNo.Value);
    }
}
