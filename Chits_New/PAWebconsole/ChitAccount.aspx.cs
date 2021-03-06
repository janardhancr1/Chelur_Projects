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

public partial class ChitAccount : System.Web.UI.Page
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
                    CloseButton.Attributes.Add("onclick", "window.location.href='ChitMembers.aspx?chitNO=" + ChitNO.Value + "';");

                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();

                        List<ChitsBiddingInfo> biddings = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                        int totalInstallments = biddings.Count + 1;

                        int customerID = Convert.ToInt32(Request.Params["customerID"]);
                        List<ChitsParticipateInfo> customers = ChitsParticipateManager.SearchChitsParticipateInfo(0, ChitNO.Value, customerID, -1, 0);
                        if (customers.Count > 0)
                        {

                            CustomerName.Value = customers[0].CustomerName;
                            CustomerAddress.Value = customers[0].CustomerAddress;

                            decimal paidTotal = 0;
                            decimal discountTotal = 0;
                            decimal bidTotal = 0;

                            HtmlTableRow row = null;
                            HtmlTableCell cell = null;

                            for (int i = 1; i < totalInstallments; i++)
                            {
                                List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(ChitNO.Value, customerID, i, 0, new DateTime(), -1, 0);

                                row = new HtmlTableRow();

                                cell = new HtmlTableCell();
                                cell.InnerText = i.ToString();
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.Align = "right";
                                if (trans.Count > 0)
                                {
                                    cell.InnerText = trans[0].InstallmentAmount.ToString();
                                    paidTotal += trans[0].InstallmentAmount;
                                }
                                else
                                    cell.InnerText = "";
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.Align = "right";
                                if (trans.Count > 0)
                                {
                                    cell.InnerText = trans[0].DiscountAmount.ToString();
                                    discountTotal += trans[0].DiscountAmount;
                                }
                                else
                                    cell.InnerText = "";
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.Align = "center";
                                if (trans.Count > 0)
                                    cell.InnerText = trans[0].Date.ToString("dd/MM/yyyy");
                                else
                                    cell.InnerText = "";
                                row.Cells.Add(cell);

                                List<ChitsBiddingInfo> bid = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, i, 0, new DateTime(), new DateTime(), customerID, 0, -1, 0);

                                cell = new HtmlTableCell();
                                cell.Align = "right";
                                cell.InnerText = "";
                                if (bid.Count > 0)
                                {
                                    cell.InnerText = bid[0].PaidAmount.ToString();
                                    bidTotal += bid[0].PaidAmount;
                                }

                                List<ChitsCompanyBiddingInfo> compBids = ChitsCompanyBiddingManager.SearchChitsCompanyBiddingInfo(0, ChitNO.Value, i, 0, new DateTime(), customerID, -1, 0);
                                if (compBids.Count > 0)
                                {
                                    cell.InnerText = compBids[0].PaidAmount.ToString();
                                    bidTotal += compBids[0].PaidAmount;
                                }
                                    
                                row.Cells.Add(cell);

                                cell = new HtmlTableCell();
                                cell.Align = "center";
                                if (bid.Count > 0)
                                    cell.InnerText = bid[0].BidDate.ToString("dd/MM/yyyy");
                                else
                                    cell.InnerText = "";
                                row.Cells.Add(cell);

                                MembersTable.Rows.Add(row);
                            }

                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerHtml = "<hr/>";
                            cell.ColSpan = 6;
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);

                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = "Total : ";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.Align = "right";
                            cell.InnerText = paidTotal.ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.Align = "right";
                            cell.InnerText = discountTotal.ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = "Total : ";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.Align = "right";
                            cell.InnerText = bidTotal.ToString();
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerHtml = "&nbsp;";
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);

                        }
                    }
                }
            }
        }
    }
}

