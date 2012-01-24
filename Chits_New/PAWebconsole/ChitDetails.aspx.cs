using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
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

public partial class ChitDetails : System.Web.UI.Page
{
    private bool firstLoad = false;
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
                        ChitsTransManager.DeleteChitsTransInfo(transID);
                    }
                    if (chitsInfo != null)
                    {
                        ChitName.Value = chitsInfo.ChitName;
                        ChitAmount.Value = chitsInfo.ChitAmount.ToString();
                        ChitCommission.Value = chitsInfo.ChitCommission.ToString();
                        for (int i = 1; i <= chitsInfo.NoInstallments; i++)
                        {
                            SelectInstallment.Items.Add(i.ToString());
                            InstallmentNo.Items.Add(i.ToString());
                        }

                        int totalInstallments = ChitsBiddingManager.SearchChitsBiddingInfoCount(ChitNO.Value, 0, 0, new DateTime(), new DateTime(), 0, 0, 0, 0);
                        totalInstallments += 1;

                        int currentInstallment = ChitsBiddingManager.GetLastBiddingInstallment(ChitNO.Value);

                        InstallmentNo.SelectedValue = currentInstallment.ToString();
                        firstLoad = true;


                        List<ChitsBiddingInfo> lastBidding = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, currentInstallment - 1, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
                        if (lastBidding.Count > 0)
                        {
                            decimal comm = chitsInfo.ChitAmount * chitsInfo.ChitCommission / 100;
                            decimal leftAmount = lastBidding[0].LeftAmount - comm;
                            decimal installAmount = (chitsInfo.InstallmentAmount - (leftAmount / chitsInfo.NoInstallments));
                            InstallmentAmount.Value = installAmount.ToString();
                        }
                        else
                        {
                            InstallmentAmount.Value = chitsInfo.InstallmentAmount.ToString();
                        }

                        List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Value);
                        foreach (ChitsParticipateInfo customer in customers)
                        {
                            ListItem item = new ListItem();
                            item.Text = customer.CustomerName;
                            item.Value = customer.CustomerID.ToString();
                            Customer.Items.Add(item);

                            ListItem pItem = new ListItem();
                            pItem.Text = customer.CustomerName;
                            pItem.Value = customer.CustomerID.ToString();
                            Customer_ID.Items.Add(pItem);

                        }
                    }
                }
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void Gridview_PreRender(object sender, EventArgs e)
    {
        if (firstLoad)
        {
            if (GridView1.PageCount > 1)
                GridView1.PageIndex = GridView1.PageCount - 1;
        }
    }

    protected void Installment_Changed(object sender, EventArgs e)
    {
        if (InstallmentNo.SelectedValue.Length > 0)
        {
            int currentInstall = Convert.ToInt32(InstallmentNo.SelectedValue);
            ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Value);
            List<ChitsBiddingInfo> lastBidding = ChitsBiddingManager.SearchChitsBiddingInfo(ChitNO.Value, currentInstall - 1, 0, new DateTime(), new DateTime(), 0, 0, -1, 0);
            if (lastBidding.Count > 0 && currentInstall > 1)
            {
                decimal comm = chitsInfo.ChitAmount * chitsInfo.ChitCommission / 100;
                decimal leftAmount = lastBidding[0].LeftAmount - comm;
                decimal installAmount = (chitsInfo.InstallmentAmount - (leftAmount / chitsInfo.NoInstallments));
                InstallmentAmount.Value = installAmount.ToString();
            }
            else
            {
                InstallmentAmount.Value = chitsInfo.InstallmentAmount.ToString();
            }
        }
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        ChitsTransInfo chitTrans = new ChitsTransInfo();
        chitTrans.ChitNO = ChitNO.Value;
        chitTrans.Date = DateTime.ParseExact(PaidDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        chitTrans.InstallmentNO = Convert.ToInt32(InstallmentNo.SelectedValue);
        chitTrans.InstallmentAmount = Convert.ToInt32(InstallmentAmount.Value);
        chitTrans.CustomerID = Convert.ToInt32(Customer.SelectedValue);

        try
        {
            ChitsTransManager.AddChitsTransInfo(chitTrans);
        }
        catch (PAException ex)
        {
            throw ex;
        }
        Response.Redirect("ChitDetails.aspx?chitNO=" + ChitNO.Value);
    }
}
