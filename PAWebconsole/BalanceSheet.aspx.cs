using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class BalanceSheet : System.Web.UI.Page
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
            FromYear.Text = appConstants.FinYearStart.ToString("dd/MM/yyyy");
            ToYear.Text = appConstants.FinYearEnd.ToString("dd/MM/yyyy");
            List<DayBookInfo> trialBalance = AccountsManager.GetTrailBalance(appConstants.FinYearEnd);
            HtmlTableRow row;
            HtmlTableCell cell;

            decimal debitTotal = 0;
            decimal creditTotal = 0;

            foreach (DayBookInfo tb in trialBalance)
            {
                if (tb.Debit > 0 || tb.Credit > 0)
                {
                    row = new HtmlTableRow();
                    row.Attributes.Add("style", "cursor:hand");
                    row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                    row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                    cell = new HtmlTableCell();
                    cell.InnerText = tb.Particulars;
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (tb.Debit > 0)
                        cell.InnerText = tb.Debit.ToString("#0.00");
                    else if (tb.Credit > 0)
                        cell.InnerText = tb.Credit.ToString("#0.00");
                    cell.Align = "right";
                    row.Cells.Add(cell);

                    if (tb.FromLedger.Equals("LIABILITIES"))
                    {
                        if (tb.Debit > 0)
                        {
                            debitTotal = debitTotal + tb.Debit;
                            ExpenseTable.Rows.Add(row);
                        }
                        else
                        {
                            creditTotal = creditTotal + tb.Credit;
                            IncomeTable.Rows.Add(row);
                        }
                    }
                    else if (tb.FromLedger.Equals("ASSETS"))
                    {
                        if (tb.Debit > 0)
                        {
                            debitTotal = debitTotal + tb.Debit;
                            ExpenseTable.Rows.Add(row);
                        }
                        else
                        {
                            creditTotal = creditTotal + tb.Credit;
                            IncomeTable.Rows.Add(row);
                        }
                    }
                }

            }

            List<DayBookInfo> expenses = AccountsManager.GetExpenses(appConstants.FinYearEnd);
            List<DayBookInfo> incomes = AccountsManager.GetIncomes(appConstants.FinYearEnd);

            decimal loss = 0;
            decimal profit = 0;

            foreach (DayBookInfo expense in expenses)
            {
                loss = loss + expense.Debit;
            }

            foreach (DayBookInfo income in incomes)
            {
                profit = profit + income.Debit;
            }

            if (profit > loss)
            {
                decimal total = profit - loss;
                creditTotal = creditTotal + total;
                row = new HtmlTableRow();
                row.Attributes.Add("style", "cursor:hand");
                row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                cell = new HtmlTableCell();
                cell.InnerText = "Net Profit";
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = total.ToString("#0.00");
                cell.Align = "right";
                row.Cells.Add(cell);

                IncomeTable.Rows.Add(row);
            }

            if (loss > profit)
            {
                decimal total = loss - profit;
                debitTotal = debitTotal + total;
                row = new HtmlTableRow();
                row.Attributes.Add("style", "cursor:hand");
                row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                cell = new HtmlTableCell();
                cell.InnerText = "Net Loss";
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.InnerText = total.ToString("#0.00");
                cell.Align = "right";
                row.Cells.Add(cell);

                ExpenseTable.Rows.Add(row);
            }

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

            Loss.Rows.Add(row);

            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = "Grand Total";
            cell.Align = "center";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = creditTotal.ToString("#0.00");
            cell.Align = "right";
            row.Cells.Add(cell);

            Profit.Rows.Add(row);
        }
    }

}
