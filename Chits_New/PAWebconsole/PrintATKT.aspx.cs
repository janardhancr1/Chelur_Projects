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

public partial class PrintATKT : System.Web.UI.Page
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

            }
        }
    }
}
