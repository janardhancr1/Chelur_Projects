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

                    List<ChitsBiddingInfo> biddings = ChitsBiddingManager.SearchChitsBiddingInfo(0, ChitNO.Value, 0, 0, new DateTime(), 0, 0, -1, 0);
                    int totalInstallments = biddings.Count + 1;

                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);

                    foreach (ChitsParticipateInfo customer in customers)
                    {
                        for (int i = 1; i < totalInstallments; i++)
                        {
                            List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(ChitNO.Value, customer.CustomerID, i, new DateTime(), -1, 0);

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

                                cell = new HtmlTableCell();
                                cell.InnerText = i.ToString();
                                row.Cells.Add(cell);

                                decimal installAmount = chitsInfo.InstallmentAmount;
                                if (i > 1)
                                {
                                    decimal comm = chitsInfo.ChitAmount * chitsInfo.ChitCommission / 100;
                                    decimal leftAmount = biddings[i].LeftAmount - comm;
                                    installAmount = (chitsInfo.InstallmentAmount - (leftAmount / chitsInfo.NoInstallments));

                                }
                                cell = new HtmlTableCell();
                                cell.InnerText = installAmount.ToString();
                                row.Cells.Add(cell);

                                MembersTable.Rows.Add(row);
                            }
                        }
                    }
                }
            }
        }
    }
}
