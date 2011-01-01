using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class TrialBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            DBConstant appConstants = (DBConstant)Session["AppConstants"];

            ListItem item = new ListItem();
            item.Text = "Financial Year";
            item.Value = DBConstant.MODE_ADD.ToString();
            item.Selected = true;
            ViewType.Items.Add(item);

            item = new ListItem();
            item.Text = "Period";
            item.Value = DBConstant.MODE_UPDATE.ToString();
            ViewType.Items.Add(item);

            FinYearFrom.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            FinYearTo.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            UptoYearFrom.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");

            ToDate.Value = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            FromDate.Value = appConstants.FinYearStart.ToString("dd/MM/yyyy");

            if (Request.Params["group"] != null)
            {
                Details_Click(sender, e);
            }
        }
    }

    protected void ViewType_Selected(object sender, EventArgs e)
    {
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_ADD.ToString()))
        {
            FinancialCell.Visible = true;
            UptoDateCell.Visible = false;
        }
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_UPDATE.ToString()))
        {
            FinancialCell.Visible = false;
            UptoDateCell.Visible = true;
        }
    }

    protected void View_Click(object sender, EventArgs e)
    {
        DateTime toDate = new DateTime();
        DBConstant appConstants = (DBConstant)Session["AppConstants"];

        if (ViewType.SelectedValue.Equals(DBConstant.MODE_ADD.ToString()))
        {
            toDate = appConstants.FinYearEnd;
        }
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_UPDATE.ToString()))
        {
            toDate = DateTime.ParseExact(UptoYearTo.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        List<DayBookInfo> trialBalance = AccountsManager.GetTrailBalance(toDate);
        HtmlTableRow row;
        HtmlTableCell cell;

        decimal debitTotal = 0;
        decimal creditTotal = 0;

        foreach (DayBookInfo tb in trialBalance)
        {
            if (tb.Debit > 0 || tb.Credit > 0)
            {
                debitTotal = debitTotal + tb.Debit;
                creditTotal = creditTotal + tb.Credit;

                row = new HtmlTableRow();
                row.Attributes.Add("style", "cursor:hand");
                row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");
                row.Attributes.Add("ondblclick", "window.location.href='TrialBalance.aspx?group=" + tb.Particulars + "';");

                cell = new HtmlTableCell();
                cell.InnerText = tb.Particulars;
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                if (tb.Debit > 0)
                    cell.InnerText = tb.Debit.ToString("#0.00");
                else
                    cell.InnerHtml = "&nbsp;";
                cell.Align = "right";
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                if (tb.Credit > 0)
                    cell.InnerText = tb.Credit.ToString("#0.00");
                else
                    cell.InnerHtml = "&nbsp;";
                cell.Align = "right";
                row.Cells.Add(cell);

                DetailsTable.Rows.Add(row);
            }
        }

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 3;
        cell.InnerHtml = "<hr/>";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        cell.Align = "center";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = debitTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = creditTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 3;
        cell.InnerHtml = "<hr/>";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);
    }

    public void Details_Click(object sender, EventArgs e)
    {
        if (Request.Params["group"] != null)
        {
            GroupSelected.Value = GroupSelected.Value == "0" ? "1" : "0";
        }
        DateTime toDate = new DateTime();
        DBConstant appConstants = (DBConstant)Session["AppConstants"];

        if (ViewType.SelectedValue.Equals(DBConstant.MODE_ADD.ToString()))
        {
            toDate = appConstants.FinYearEnd;
        }
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_UPDATE.ToString()))
        {
            toDate = DateTime.ParseExact(UptoYearTo.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        List<DayBookInfo> trialBalance = AccountsManager.GetTrailBalanceDetails(toDate);
        HtmlTableRow row;
        HtmlTableCell cell;

        decimal debitTotal = 0;
        decimal creditTotal = 0;

        if (GroupSelected.Value == "1")
        {
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Narration.Equals(Request.QueryString["group"]) && (tb.Debit > 0 || tb.Credit > 0))
                {
                    debitTotal = debitTotal + tb.Debit;
                    creditTotal = creditTotal + tb.Credit;

                    row = new HtmlTableRow();
                    row.Attributes.Add("style", "cursor:hand");
                    row.Attributes.Add("onmouseover",
                                       "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                    row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                    cell = new HtmlTableCell();
                    cell.InnerText = tb.Particulars;
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (tb.Debit > 0)
                        cell.InnerText = tb.Debit.ToString("#0.00");
                    else
                        cell.InnerHtml = "&nbsp;";
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (tb.Credit > 0)
                        cell.InnerText = tb.Credit.ToString("#0.00");
                    else
                        cell.InnerHtml = "&nbsp;";
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    DetailsTable.Rows.Add(row);
                }
            }
        }
        else
        {
            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Debit > 0 || tb.Credit > 0)
                {
                    debitTotal = debitTotal + tb.Debit;
                    creditTotal = creditTotal + tb.Credit;

                    row = new HtmlTableRow();
                    row.Attributes.Add("style", "cursor:hand");
                    row.Attributes.Add("onmouseover",
                                       "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                    row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                    cell = new HtmlTableCell();
                    cell.InnerText = tb.Particulars;
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (tb.Debit > 0)
                        cell.InnerText = tb.Debit.ToString("#0.00");
                    else
                        cell.InnerHtml = "&nbsp;";
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (tb.Credit > 0)
                        cell.InnerText = tb.Credit.ToString("#0.00");
                    else
                        cell.InnerHtml = "&nbsp;";
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    DetailsTable.Rows.Add(row);
                }
            }
        }

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 3;
        cell.InnerHtml = "<hr/>";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        cell.Align = "center";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = debitTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = creditTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 3;
        cell.InnerHtml = "<hr/>";
        row.Cells.Add(cell);

        DetailsTable.Rows.Add(row);
    }

}
