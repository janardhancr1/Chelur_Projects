using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class PrintTrailBalanceDetails : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                //CompID.Value = company.CompID.ToString();

                if (Request.Params["todate"] != null)
                {
                    ToDate.Value = DateTime.ParseExact(Request.Params["todate"], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString(); 
                }

                if (Request.Params["fromdate"] != null)
                {
                    FromDate.Value = Request.Params["fromdate"];
                }

                ObjectDataSource2.SelectParameters[0].DefaultValue = FromDate.Value;
                ObjectDataSource2.SelectParameters[1].DefaultValue = ToDate.Value;
                ObjectDataSource2.SelectParameters[2].DefaultValue = "Trial Balance";

                //ObjectDataSource2.SelectParameters[0].DefaultValue = CompID.Value;

                ObjectDataSource1.SelectParameters[0].DefaultValue = ToDate.Value;

                if (!Page.IsPostBack)
                {
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    }
}
