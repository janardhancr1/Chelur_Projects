using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Model;

public partial class PrintLedgerDetails : System.Web.UI.Page
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
            if (Request.Params["ledger"] != null)
            {
                LedgerName.Value = Request.Params["ledger"];
            }
            if (Request.Params["type"] != null)
            {
                LedgerType.Value = Request.Params["type"];
            }
            if (Request.Params["ledgerID"] != null)
            {
                LedgerID.Value = Request.Params["ledgerID"];
            }

            ObjectDataSource2.SelectParameters[0].DefaultValue = DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource2.SelectParameters[1].DefaultValue = DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource2.SelectParameters[2].DefaultValue = LedgerName.Value;

            ObjectDataSource1.SelectParameters[0].DefaultValue = DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource1.SelectParameters[1].DefaultValue = DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
            ObjectDataSource1.SelectParameters[2].DefaultValue = LedgerID.Value;
            ObjectDataSource1.SelectParameters[3].DefaultValue = LedgerName.Value;
            ObjectDataSource1.SelectParameters[4].DefaultValue = LedgerType.Value;

            if (!Page.IsPostBack)
            {
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}
