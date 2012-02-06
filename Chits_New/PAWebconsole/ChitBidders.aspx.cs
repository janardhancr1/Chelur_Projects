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

                    if (Request.Params["t"].ToString().Equals("cbid"))
                    {
                        CloseButton.Attributes.Remove("onclick");
                        CloseButton.Attributes.Add("onclick", "window.location.href='ViewChits.aspx?chitNO=" + ChitNO.Value + "';");
                        decimal total = 0;
                        trans = ChitsBiddingManager.GetChitsBiddingInfos(ChitNO.Value);
                        foreach (ChitsBiddingInfo chitBid in trans)
                        {
                            if (chitBid.CustomerID == 0)
                            {
                                row = new HtmlTableRow();

                                cell = new HtmlTableCell();
                                cell.InnerText = "Company Bidding";
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.InnerText = "-";
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.InnerText = chitBid.InstallmentNO.ToString();
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.InnerText = chitBid.BidDate.ToString("dd/MM/yyyy");
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.InnerText = chitBid.PaidDate.ToString("dd/MM/yyyy");
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.InnerText = chitBid.PaidAmount.ToString();
                                total += chitBid.PaidAmount;
                                row.Cells.Add(cell);

                                MembersTable.Rows.Add(row);
                            }
                        }

                        row = new HtmlTableRow();
                        cell = new HtmlTableCell();
                        cell.ColSpan = 6;
                        cell.InnerHtml = "<hr />";
                        row.Cells.Add(cell);
                        MembersTable.Rows.Add(row);

                        row = new HtmlTableRow();
                        cell = new HtmlTableCell();
                        cell.ColSpan = 5;
                        cell.InnerText = "Total :";
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = total.ToString();
                        row.Cells.Add(cell);

                        MembersTable.Rows.Add(row);
                    }
                    else
                    {
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
