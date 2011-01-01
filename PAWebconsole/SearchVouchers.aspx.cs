using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;

public partial class SearchVouchers : System.Web.UI.Page
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
            List<VoucherTypesInfo> voucherTypes = AccountsManager.GetVoucherTypes();
            foreach (VoucherTypesInfo voucherType in voucherTypes)
            {
                ListItem item = new ListItem();
                item.Value = voucherType.VoucherTypeID.ToString();
                item.Text = voucherType.VoucherTypeName;
                VoucherType.Items.Add(item);
            }

            List<LedgersInfo> ledgers = LedgersManager.GetLedgersInfos();
            foreach (LedgersInfo ledger in ledgers)
            {
                ToLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                FromLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
            }
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {

        GridView1.Visible = true;
        GridView1.DataSourceID = "ObjectDataSource1";
        //DateTime d = Convert.ToDateTime(VoucherDate.Value, "MM/dd/yyyy");
        //ObjectDataSource1.SelectParameters[0].DefaultValue = d.ToString();
        GridView1.DataBind();
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            e.Row.Cells[6].Text = e.Row.Cells[6].Text + "&nbsp;&nbsp;&nbsp;";
        }
    }

}
