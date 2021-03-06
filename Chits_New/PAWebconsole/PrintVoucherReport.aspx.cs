using System;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Model;

public partial class PrintVoucherReport : System.Web.UI.Page
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
            if (Request.Params["fromdate"] != null)
            {
                FromDate.Value = Request.Params["fromdate"];
            }
            if (Request.Params["todate"] != null)
            {
                ToDate.Value = Request.Params["todate"];
            }
            ObjectDataSource2.SelectParameters[0].DefaultValue = DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource2.SelectParameters[1].DefaultValue = DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource2.SelectParameters[2].DefaultValue = "Vouchers";

            ObjectDataSource1.SelectParameters[0].DefaultValue = DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource1.SelectParameters[1].DefaultValue = DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

            if (!Page.IsPostBack)
            {
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}
