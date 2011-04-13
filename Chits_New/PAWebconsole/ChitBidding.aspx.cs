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
                    }

                    int totalInstallments = ChitsBiddingManager.SearchChitsBiddingInfoCount(0, ChitNO.Value, 0, 0, new DateTime(), 0, 0, 0, 0);
                    totalInstallments += 1;
                    InstallmentNo.Value = totalInstallments.ToString();
                }

                List<CustomerInfo> customers = CustomerManager.GetCustomerInfos();
                foreach (CustomerInfo customer in customers)
                {
                    ListItem item = new ListItem();
                    item.Text = customer.CustomerName;
                    item.Value = customer.CustomerID.ToString();
                    Customer.Items.Add(item);
                }
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        ChitsBiddingInfo chitBidding = new ChitsBiddingInfo();
        chitBidding.ChitNO = ChitNO.Value;
        chitBidding.BidDate = DateTime.ParseExact(BidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        chitBidding.BidAmount = Convert.ToDecimal(BidAmount.Value);
        chitBidding.InstallmentNO = Convert.ToInt32(InstallmentNo.Value);
        chitBidding.CustomerID = Convert.ToInt32(Customer.SelectedValue);
        chitBidding.LeftAmount = Convert.ToDecimal(ChitAmount.Value) - Convert.ToDecimal(BidAmount.Value);

        try
        {
            ChitsBiddingManager.AddChitsBiddingInfo(chitBidding);
        }
        catch (PAException ex)
        {
            throw ex;
        }
        GridView1.DataBind();
    }
}
