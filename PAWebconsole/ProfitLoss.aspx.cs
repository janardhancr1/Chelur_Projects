using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ProfitLoss : System.Web.UI.Page
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
            FinYearFrom.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            FinYearTo.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            UptoYearFrom.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            ToDate.Value = appConstants.FinYearEnd.ToString("dd/MM/yyyy");

            ListItem item = new ListItem();
            item.Text = "Financial Year";
            item.Value = DBConstant.MODE_ADD.ToString();
            item.Selected = true;
            ViewType.Items.Add(item);

            item = new ListItem();
            item.Text = "Period";
            item.Value = DBConstant.MODE_UPDATE.ToString();
            ViewType.Items.Add(item);
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
        SelectedType.Value = "1";

        if (ViewType.SelectedValue.Equals(DBConstant.MODE_ADD.ToString()))
        {
            toDate = appConstants.FinYearEnd;
        }
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_UPDATE.ToString()))
        {
            toDate = DateTime.ParseExact(UptoYearTo.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        List<DayBookInfo> expenses = AccountsManager.GetExpenses(toDate);
        List<DayBookInfo> incomes = AccountsManager.GetIncomes(toDate);

        HtmlTableRow row;
        HtmlTableCell cell;

        decimal loss = 0;
        decimal profit = 0;

        foreach (DayBookInfo expense in expenses)
        {
            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = expense.Particulars;
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = expense.Debit.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            ExpenseTable.Rows.Add(row);
            loss = loss + expense.Debit;
        }

        foreach (DayBookInfo income in incomes)
        {
            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = income.Particulars;
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = income.Debit.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            IncomeTable.Rows.Add(row);
            profit = profit + income.Debit;
        }

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = loss.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        TotalExpense.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = profit.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        TotalIncome.Rows.Add(row);

        decimal grandTotal = 0;

        if (profit > loss)
        {
            decimal total = profit - loss;
            grandTotal = loss + total;

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "Gross Profit B/F";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = total.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            Loss.Rows.Add(row);

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerHtml = "&nbsp;";
            cell.Align = "right";
            row.Cells.Add(cell);

            Profit.Rows.Add(row);
        }

        if (loss > profit)
        {
            decimal total = loss - profit;
            grandTotal = profit + total;

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "Gross Loss B/F";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = total.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            Profit.Rows.Add(row);

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerHtml = "&nbsp;";
            cell.Align = "right";
            row.Cells.Add(cell);

            Loss.Rows.Add(row);
        }

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 2;
        cell.InnerHtml = "<hr>";
        row.Cells.Add(cell);

        Loss.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = grandTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        Loss.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 2;
        cell.InnerHtml = "<hr>";
        row.Cells.Add(cell);

        Profit.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = grandTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        Profit.Rows.Add(row);
    }

    protected void Details_Click(object sender, EventArgs e)
    {
        DateTime toDate = new DateTime();
        DBConstant appConstants = (DBConstant)Session["AppConstants"];
        SelectedType.Value = "2";

        if (ViewType.SelectedValue.Equals(DBConstant.MODE_ADD.ToString()))
        {
            toDate = appConstants.FinYearEnd;
        }
        if (ViewType.SelectedValue.Equals(DBConstant.MODE_UPDATE.ToString()))
        {
            toDate = Convert.ToDateTime(UptoYearTo.Value);
        }

        List<DayBookInfo> expenses = AccountsManager.GetExpensesDetails(toDate);
        List<DayBookInfo> incomes = AccountsManager.GetIncomesDetails(toDate);

        HtmlTableRow row;
        HtmlTableCell cell;

        decimal loss = 0;
        decimal profit = 0;

        foreach (DayBookInfo expense in expenses)
        {
            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = expense.Particulars;
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = expense.Debit.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            ExpenseTable.Rows.Add(row);
            loss = loss + expense.Debit;
        }

        foreach (DayBookInfo income in incomes)
        {
            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = income.Particulars;
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = income.Debit.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            IncomeTable.Rows.Add(row);
            profit = profit + income.Debit;
        }

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = loss.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        TotalExpense.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = profit.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        TotalIncome.Rows.Add(row);
        decimal grandTotal = 0;

        if (profit > loss)
        {
            decimal total = profit - loss;
            grandTotal = loss + total;

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "Gross Profit B/F";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = total.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            Loss.Rows.Add(row);

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerHtml = "&nbsp;";
            cell.Align = "right";
            row.Cells.Add(cell);

            Profit.Rows.Add(row);
        }

        if (loss > profit)
        {
            decimal total = loss - profit;
            grandTotal = profit + total;

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "Gross Loss B/F";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = total.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            Profit.Rows.Add(row);

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerHtml = "&nbsp;";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = "";
            cell.Align = "right";
            row.Cells.Add(cell);

            Loss.Rows.Add(row);
        }

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 2;
        cell.InnerHtml = "<hr>";
        row.Cells.Add(cell);

        Loss.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = grandTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        Loss.Rows.Add(row);

        row = new HtmlTableRow();

        cell = new HtmlTableCell();
        cell.ColSpan = 2;
        cell.InnerHtml = "<hr>";
        row.Cells.Add(cell);

        Profit.Rows.Add(row);

        row = new HtmlTableRow();
        row.Attributes.Add("style", "cursor:hand");
        row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
        row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

        cell = new HtmlTableCell();
        cell.InnerText = "Grand Total";
        row.Cells.Add(cell);

        cell = new HtmlTableCell();
        cell.InnerText = grandTotal.ToString("#0.00");
        cell.Align = "right";
        row.Cells.Add(cell);

        Profit.Rows.Add(row);
    }
}
