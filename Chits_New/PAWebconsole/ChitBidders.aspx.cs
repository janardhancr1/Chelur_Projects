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

public partial class ChitBidders : System.Web.UI.Page
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
                    }

                    List<ChitsBiddingInfo> trans = new List<ChitsBiddingInfo>();
                    HtmlTableRow row = null;
                    HtmlTableCell cell = null;


                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                    foreach (ChitsParticipateInfo customer in customers)
                    {

                        trans = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), customer.CustomerID, 0, -1, 0);

                        if (trans.Count > 0 && Request.Params["t"].ToString().Equals("bid"))
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerName;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerAddress;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = trans[0].InstallmentNO.ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = trans[0].BidDate.ToString("dd/MM/yyyy");
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = trans[0].PaidDate.ToString("dd/MM/yyyy");
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = trans[0].PaidAmount.ToString();
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);
                        }

                        if (trans.Count == 0 && Request.Params["t"].ToString().Equals("unbid"))
                        {
                            int compBidCnt = ChitsCompanyBiddingManager.SearchChitsCompanyBiddingInfoCount(0, ChitNO.Value, 0, 0, new DateTime(), customer.CustomerID, -1, 0);
                            if (compBidCnt == 0)
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
}
