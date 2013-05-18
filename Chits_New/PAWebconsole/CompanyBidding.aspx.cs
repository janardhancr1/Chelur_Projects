using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class CompanyBidding : System.Web.UI.Page
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
                if (Request.Params["recordID"] != null)
                {
                    ChitsCompanyBiddingManager.DeleteChitsCompanyBiddingInfo(Convert.ToInt32(Request.Params["recordID"]));
                }
                if (Request.Params["chitNO"] != null)
                {
                    ChitNO.Value = Request.Params["chitNO"];
                    
                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();
                    }

                    List<ChitsBiddingInfo> trans = new List<ChitsBiddingInfo>();
                    HtmlTableRow row = null;
                    HtmlTableCell cell = null;


                    decimal total = 0;
                    decimal paidTotal = 0;
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
                            cell.Align = "right";
                            total += chitBid.PaidAmount;
                            row.Cells.Add(cell);

                            MembersTable.Rows.Add(row);

                            ListItem item = new ListItem();
                            item.Text = chitBid.InstallmentNO.ToString();
                            item.Value = chitBid.InstallmentNO.ToString();
                            InstallmentNo.Items.Add(item);
                        }
                    }

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 5;
                    cell.InnerHtml = "<hr />";
                    row.Cells.Add(cell);
                    MembersTable.Rows.Add(row);

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 4;
                    cell.InnerText = "Total :";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = total.ToString();
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    MembersTable.Rows.Add(row);

                    List<ChitsCompanyBiddingInfo> biddings = ChitsCompanyBiddingManager.SearchChitsCompanyBiddingInfo(0, ChitNO.Value, 0, 0, new DateTime(), 0, -1, 0);
                    foreach(ChitsCompanyBiddingInfo bid in biddings)
                    {
                        row = new HtmlTableRow();

                        cell = new HtmlTableCell();
                        cell.InnerText = CustomerManager.GetCustomerInfo(bid.CustomerID).CustomerName;
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = bid.InstallmentNO.ToString();
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = bid.PaidDate.ToString("dd/MM/yyyy");
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = bid.PaidAmount.ToString();
                        cell.Align = "right";
                        paidTotal += bid.PaidAmount;
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerHtml = "<a onclick=\"javascript:return confirm('Are you sure to Delete?');\" style='color:Red;' href='CompanyBidding.aspx?recordID=" + bid.RecordID.ToString() + "&chitNO=" + bid.ChitNO + "'>Delete</a>";
                        row.Cells.Add(cell);

                        CompBidding.Rows.Add(row);
                    }

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 5;
                    cell.InnerHtml = "<hr />";
                    row.Cells.Add(cell);
                    CompBidding.Rows.Add(row);

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 3;
                    cell.InnerText = "Total :";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = paidTotal.ToString();
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerHtml = "&nbsp;";
                    row.Cells.Add(cell);

                    CompBidding.Rows.Add(row);

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 5;
                    cell.InnerHtml = "<hr />";
                    row.Cells.Add(cell);
                    CompBidding.Rows.Add(row);

                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell.ColSpan = 3;
                    cell.InnerText = "Grand Total :";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerHtml = "&nbsp;";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    cell.InnerText = (total - paidTotal).ToString();
                    cell.Align = "right";
                    row.Cells.Add(cell);
                    
                    CompBidding.Rows.Add(row);

                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                    foreach (ChitsParticipateInfo customer in customers)
                    {

                        trans = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), customer.CustomerID, 0, -1, 0);

                        if (trans.Count == 0)
                        {
                            ListItem item = new ListItem();
                            item.Text = customer.CustomerName;
                            item.Value = customer.CustomerID.ToString();
                            Customer.Items.Add(item);
                        }
                    }
                }
            }
        }
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        ChitsCompanyBiddingInfo chitsCompanyBiddingInfo = new ChitsCompanyBiddingInfo();

        chitsCompanyBiddingInfo.ChitNO = ChitNO.Value;
        if (InstallmentNo.SelectedValue.Trim().Length > 0) chitsCompanyBiddingInfo.InstallmentNO = Convert.ToInt32(InstallmentNo.SelectedValue);
        if (PaidAmount.Value.Trim().Length > 0) chitsCompanyBiddingInfo.PaidAmount = Convert.ToDecimal(PaidAmount.Value);
        if (PaidDate.Value.Trim().Length > 0) chitsCompanyBiddingInfo.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
        if (Customer.SelectedValue.Trim().Length > 0) chitsCompanyBiddingInfo.CustomerID = Convert.ToInt32(Customer.SelectedValue);

        try
        {
            ChitsCompanyBiddingManager.AddChitsCompanyBiddingInfo(chitsCompanyBiddingInfo);
            Response.Redirect("CompanyBidding.aspx?chitNO=" + ChitNO.Value);
        }
        catch (PAException ex)
        {
            message.Text = ex.Message;
        }

    }
}