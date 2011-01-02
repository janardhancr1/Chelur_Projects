using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class DayBook : System.Web.UI.Page
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
            DBConstant appConstants = (DBConstant) Session["AppConstants"];
            DayDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
            View_Click(sender, e);
        }
    }

    public void View_Click(object sender, EventArgs e)
    {
        List<DayBookInfo> dayBooks =
            AccountsManager.GetDayBook(DateTime.ParseExact(DayDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                       DateTime.ParseExact(DayDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));

        HtmlTableRow row;
        HtmlTableCell cell;
        int i = 1;
        foreach (DayBookInfo dayBook in dayBooks)
        {
            row = new HtmlTableRow();

            cell = new HtmlTableCell();
            cell.InnerText = i.ToString();
            cell.Width = "5%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.CurrentDate.ToString("dd/MM/yyyy");
            cell.Width = "10%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = DBConstant.PARTICULARS_BY + " " + dayBook.FromLedger;
            cell.Width = "15%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = DBConstant.PARTICULARS_TO + " " + dayBook.ToLedger;
            cell.Width = "15%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.VoucherType;
            cell.Width = "10%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.VoucherNo.ToString();
            cell.Width = "10%";
            cell.Align = "center";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            if (dayBook.Credit > 0)
                cell.InnerText = dayBook.Credit.ToString("#0.00");
            else
                cell.InnerHtml = "&nbsp;";
            cell.Width = "10%";
            cell.Align = "right";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            if (dayBook.Debit > 0)
                cell.InnerText = dayBook.Debit.ToString("#0.00");
            else
                cell.InnerHtml = "&nbsp;";
            cell.Width = "10%";
            cell.Align = "right";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.Narration;
            cell.Width = "15%";
            row.Cells.Add(cell);

            DayTable.Rows.Add(row);
            i++;
        }
    }
}
