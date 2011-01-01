using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ATKT : System.Web.UI.Page
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
                Closed.Items.Clear();

                ListItem item = null;

                item = new ListItem();
                item.Value = "";
                item.Text = "--Select--";
                Closed.Items.Add(item);

                item = new ListItem();
                item.Value = DBConstant.TYPE_PENDING;
                item.Text = "Pending";
                Closed.Items.Add(item);

                item = new ListItem();
                item.Value = DBConstant.TYPE_CLOSED;
                item.Text = "Closed";
                Closed.Items.Add(item);

                TranType.Items.Clear();

                ListItem item1 = null;

                item1 = new ListItem();
                item1.Value = "";
                item1.Text = "--Select--";
                TranType.Items.Add(item1);

                item1 = new ListItem();
                item1.Value = DBConstant.ATKT_PAY;
                item1.Text = "Payment";
                TranType.Items.Add(item1);

                item1 = new ListItem();
                item1.Value = DBConstant.ATKT_RECP;
                item1.Text = "Receipt";
                TranType.Items.Add(item1);
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='LightGrey';");
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
