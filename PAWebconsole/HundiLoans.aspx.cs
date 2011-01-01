using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class HundiLoans : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ClosedType.Items.Clear();

                ListItem item = null;

                item = new ListItem();
                item.Value = "";
                item.Text = "--Select--";
                ClosedType.Items.Add(item);

                item = new ListItem();
                item.Value = DBConstant.TYPE_PENDING;
                item.Text = "Pending";
                ClosedType.Items.Add(item);

                item = new ListItem();
                item.Value = DBConstant.TYPE_CLOSED;
                item.Text = "Closed";
                ClosedType.Items.Add(item);

                item = new ListItem();
                item.Value = DBConstant.TYPE_ALL;
                item.Text = "All";
                ClosedType.Items.Add(item);

                List<CustomerInfo> customers = CustomerManager.GetCustomerInfos();

                foreach (CustomerInfo cust in customers)
                {
                    ListItem item1 = new ListItem();
                    item1.Text = cust.CustomerName;
                    item1.Value = cust.CustomerID.ToString();

                    CustomerName.Items.Add(item1);
                }

                List<LedgersInfo> banks = LedgersManager.GetLedgersInfos();
                foreach (LedgersInfo bank in banks)
                {
                    if (bank.GroupID == 14)
                    {
                        ListItem item2 = new ListItem();
                        item2.Value = bank.LedgerID.ToString();
                        item2.Text = bank.LedgerName;
                        BankName.Items.Add(item2);
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

            if (e.Row.Cells[7].Text.Equals("01/01/0001"))
                e.Row.Cells[7].Text = "";
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        GridView1.DataSourceID = "ObjectDataSource1";
        GridView1.DataBind();
    }
}
