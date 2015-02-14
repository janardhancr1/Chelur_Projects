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

public partial class PrintChitUnpaid : System.Web.UI.Page
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
            if (Request.Params["chitno"] != null)
            {
                ChitNo.Value = Request.Params["chitno"];
            }
            if (Request.Params["type"] != null)
            {
                PrintType.Value = Request.Params["type"];
            }

            if (Request.Params["chitName"] != null)
            {
                ChitName.Value = Request.Params["chitName"];
            }

            ObjectDataSource2.SelectParameters[0].DefaultValue = DateTime.Today.ToShortDateString();
            ObjectDataSource2.SelectParameters[1].DefaultValue = DateTime.Today.ToShortDateString();
            ObjectDataSource2.SelectParameters[2].DefaultValue = ChitName.Value;

            ObjectDataSource1.SelectParameters[0].DefaultValue = ChitNo.Value;
            ObjectDataSource1.SelectParameters[1].DefaultValue = PrintType.Value;

            if (!Page.IsPostBack)
            {
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}
