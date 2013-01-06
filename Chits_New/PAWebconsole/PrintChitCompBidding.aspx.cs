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

public partial class PrintChitCompBidding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Write("<script>window.opener.location.reload(true);self.close();</script>");
        }
        else
        {
            if (Request.Params["chitNo"] != null)
            {
                ChitNo.Value = Request.Params["chitNo"];
            }

            ObjectDataSource1.SelectParameters[0].DefaultValue = ChitNo.Value;

            if (!Page.IsPostBack)
            {
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}
