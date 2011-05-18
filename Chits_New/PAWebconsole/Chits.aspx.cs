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

using PALibrary.Library.Utils;
using PALibrary.Library.Model;
using PALibrary.Library.Exception;

public partial class Chits : System.Web.UI.Page
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

                for (int i = 1; i < 31; i++)
                {
                    BidDate.Items.Add(i.ToString());
                }
            }
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {

        GridView1.Visible = true;
        GridView1.DataSourceID = "ObjectDataSource1";
        GridView1.DataBind();
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='LightGrey';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");
        }
    }

}
