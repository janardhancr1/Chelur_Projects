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

public partial class ChitBidding : System.Web.UI.Page
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
                    CloseButton.Attributes.Add("onclick", "window.location.href='ViewChits.aspx?chitNO=" + ChitNO.Value + "';");

                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
                    if (Request.Params["transid"] != null)
                    {
                        int transID = Convert.ToInt32(Request.Params["transid"]);
                        ChitsBiddingManager.DeleteChitsBiddingInfo(transID);
                    }
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();
                        NoInstallments.Value = chitsInfo.NoInstallments.ToString();

                        for (int i = 1; i <= chitsInfo.NoInstallments; i++)
                        {
                            InstallmentNo.Items.Add(i.ToString());
                        }

                        int totalInstallments = ChitsBiddingManager.SearchChitsBiddingInfoCount(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), 0, 0, 0, 0);
                        totalInstallments += 1;
                        InstallmentNo.SelectedValue = totalInstallments.ToString();
                    }
                    List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                    foreach (ChitsParticipateInfo customer in customers)
                    {
                        ListItem item = new ListItem();
                        item.Text = customer.CustomerName;
                        item.Value = customer.CustomerID.ToString();
                        Customer.Items.Add(item);
                    }

                    Customer.Items.Add(new ListItem("Company Bidding", "0"));

                }
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        message.Text = "";
        ChitsBiddingInfo chitBidding = new ChitsBiddingInfo();
        chitBidding.ChitNO = ChitNO.Value;
        chitBidding.PaidDate = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        chitBidding.BidDate = DateTime.ParseExact(BidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        chitBidding.PaidAmount = Convert.ToDecimal(BidAmount.Value);
        chitBidding.InstallmentNO = Convert.ToInt32(InstallmentNo.SelectedValue);
        chitBidding.CustomerID = Convert.ToInt32(Customer.SelectedValue);
        chitBidding.LeftAmount = Convert.ToDecimal(ChitAmount.Value) - Convert.ToDecimal(BidAmount.Value);

        try
        {
            int cnt = ChitsBiddingManager.SearchChitsBiddingInfoCount(chitBidding.ChitNO, 0, 0, new DateTime(), new DateTime(), chitBidding.CustomerID, 0, -1, 0);
            if (cnt == 0)
            {
                ChitsBiddingManager.AddChitsBiddingInfo(chitBidding);
                Response.Redirect("ChitBidding.aspx?chitNO=" + ChitNO.Value);
            }
            else
            {
                message.Text = "Already Bidded";
            }
        }
        catch (PAException ex)
        {
            throw ex;
        }
        
    }
}
