using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ViewChits : System.Web.UI.Page
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

            if (Request.Params["chitNO"] != null)
            {
                ChitNO.Text = Request.Params["chitNO"];
                Link1.Attributes.Remove("href");
                Link1.Attributes.Add("href", "ChitDetails.aspx?chitNO=" + ChitNO.Text);
                Link2.Attributes.Remove("href");
                Link2.Attributes.Add("href", "ChitBidding.aspx?chitNO=" + ChitNO.Text);
                Link3.Attributes.Remove("href");
                Link3.Attributes.Add("href", "ChitBidders.aspx?chitNO=" + ChitNO.Text + "&t=cbid");
                Link4.Attributes.Remove("href");
                Link4.Attributes.Add("href", "ChitMembers.aspx?chitNO=" + ChitNO.Text);
                //DeleteButton.Visible = true;

                ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Text);
                if (chitsInfo != null)
                {
                    ChitName.Text = chitsInfo.ChitName;
                    ChitAmount.Text = chitsInfo.ChitAmount.ToString();
                    InstallmentAmount.Text = chitsInfo.InstallmentAmount.ToString();
                    NoInstallments.Text = chitsInfo.NoInstallments.ToString();
                }

                int totalInstallments = ChitsBiddingManager.SearchChitsBiddingInfoCount(ChitNO.Text, 0, 0, new DateTime(), new DateTime(), 0, 0, 0, 0);
                totalInstallments += 1;

                HtmlTableRow row = null;
                HtmlTableCell cell = null;

                row = new HtmlTableRow();
                row.Attributes.Add("class", "nav_header");

                cell = new HtmlTableCell();
                cell.InnerText = "Customer Name";
                row.Cells.Add(cell);

                DateTime stratMonth = chitsInfo.ChitStartDate.AddMonths(-1);
                for (int i = 1; i < totalInstallments; i++)
                {
                    cell = new HtmlTableCell();
                    cell.InnerText = i.ToString() + "(" + chitsInfo.BidDate + "/" + stratMonth.AddMonths(i).Month + ")";
                    row.Cells.Add(cell);
                }

                DetailsTable.Rows.Add(row);

                List<ChitsParticipateInfo> customers = ChitsParticipateManager.GetChitsParticipateInfos(ChitNO.Text);
                foreach (ChitsParticipateInfo customer in customers)
                {
                    row = new HtmlTableRow();

                    cell = new HtmlTableCell();
                    cell.InnerText = customer.CustomerName;
                    row.Cells.Add(cell);

                    for (int i = 1; i < totalInstallments; i++)
                    {
                        List<ChitsTransInfo> trans = ChitsTransManager.SearchChitsTransInfo(ChitNO.Text, customer.CustomerID, i, 0, new DateTime(), -1, 0);

                        cell = new HtmlTableCell();
                        if (trans.Count > 0)
                            cell.InnerText = trans[0].Date.ToString("dd/MM");
                        else
                            cell.InnerText = "";
                        row.Cells.Add(cell);
                    }

                    DetailsTable.Rows.Add(row);

                }
            }

        }
    }

    public void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            ChitsManager.DeleteChitsInfo(ChitNO.Text);
            Response.Redirect("Chits.aspx");
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }

    public void Close_Click(object sender, EventArgs e)
    {
        try
        {
            ChitsManager.CloseChitsInfo(ChitNO.Text);
            Response.Redirect("Chits.aspx");
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }

    public void Print_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=ChitDetails.xls");
        Response.Charset = "";
        this.EnableViewState = false;

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        DetailsTable.RenderControl(htw);

        Response.Write(sw.ToString());
        Response.End();

    }
}
