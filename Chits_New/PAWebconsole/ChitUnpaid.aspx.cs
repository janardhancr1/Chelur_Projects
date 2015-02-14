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
                    PrintType.Value = DBConstant.CHIT_UNPAID;

                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();
                    }

                    List<ChitsBiddingInfo> biddings = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                    int totalInstallments = biddings.Count + 1;

                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                    decimal chitTotal = 0;
                    HtmlTableRow row = null;
                    HtmlTableCell cell = null;

                    foreach (ChitsParticipateInfo customer in customers)
                    {
                        decimal totalAmount = 0;
                        string installments = "";
                        bool found = false;

                        for (int i = 1; i < totalInstallments; i++)
                        {
                            List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(ChitNO.Value, customer.CustomerID, i, 0, new DateTime(), -1, 0);

                            if (trans.Count == 0)
                            {
                                found = true;

                                decimal installAmount = chitsInfo.InstallmentAmount;
                                if (i > 1 && i < totalInstallments)
                                {
                                    List<ChitsBiddingInfo> lastBidding = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, i - 1, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                                    decimal comm = chitsInfo.ChitAmount * chitsInfo.ChitCommission / 100;
                                    decimal leftAmount = lastBidding[0].LeftAmount - comm;
                                    installAmount = (chitsInfo.InstallmentAmount - (leftAmount / chitsInfo.NoInstallments));

                                }

                                totalAmount += installAmount;
                                installments += i.ToString() + ", ";

                            }
                        }
                        if (found == true)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerName;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = customer.CustomerAddress;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = totalAmount.ToString();
                            row.Cells.Add(cell);


                            cell = new HtmlTableCell();
                            cell.InnerText = installments;
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);

                        }

                        chitTotal += totalAmount;
                    }

                    row = new HtmlTableRow();

                    cell = new HtmlTableCell();
                    cell.InnerHtml = "<hr />";
                    cell.ColSpan = 4;
                    row.Cells.Add(cell);

                    MembersTable.Rows.Add(row);

                    row = new HtmlTableRow();

                    cell = new HtmlTableCell();
                    cell.InnerText = "Total";
                    cell.ColSpan = 2;
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = chitTotal.ToString();
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
