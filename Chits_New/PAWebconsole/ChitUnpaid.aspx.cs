using System;
using System.Data;
using System.Configuration;
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

public partial class ChitUnpaid : System.Web.UI.Page
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
                if (Request.Params["chitNO"] != null)
                {
                    ChitNO.Value = Request.Params["chitNO"];
                    CloseButton.Attributes.Add("onclick", "window.location.href='ChitDetails.aspx?chitNO=" + ChitNO.Value + "';");

                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();
                        InstallmentNo.Value = Request.Params["insNO"];
                    }

                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                    foreach (ChitsParticipateInfo customer in customers)
                    {
                        List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(ChitNO.Value, customer.CustomerID, Convert.ToInt32(InstallmentNo.Value), new DateTime(), -1, 0);

                        HtmlTableRow row = null;
                        HtmlTableCell cell = null;
                        if (trans.Count == 0)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerName;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerAddress;
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);
                        }
                    }
                }
            }
        }
    }
}
