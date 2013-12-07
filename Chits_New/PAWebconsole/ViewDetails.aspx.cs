using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ViewDetails : System.Web.UI.Page
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
            FromDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
            ToDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
            if (Request.Params["type"] != null)
            {
                LedgerType.Value = Request.Params["type"];
                LedgerID.Value = "0";
                CloseButton.Attributes.Add("onclick", "window.location.href='MonthlyLedger.aspx?type=" + LedgerType.Value + "';");
            }
            else if (Request.Params["ledgerID"] != null)
            {
                LedgersInfo ledger = LedgersManager.GetLedgersInfo(Convert.ToInt32(Request.QueryString["ledgerID"]));
                if (ledger != null)
                {
                    LedgerName.Text = ledger.LedgerName;
                    LedgerID.Value = ledger.LedgerID.ToString();
                    GroupName.Value = ledger.GroupName;
                }
                if (LedgerName.Text.Equals(DBConstant.CASH_LEDGER))
                    CloseButton.Attributes.Add("onclick", "window.location.href='CashBook.aspx';");
                else
                    CloseButton.Attributes.Add("onclick", "window.location.href='ViewLedger.aspx?ledgerID=" + LedgerID.Value + "';");
            }
            View_Click(sender, e);
        }
    }

    protected void View_Click(object sender, EventArgs e)
    {
        DetailsTable.Rows.Clear();
        OpenCredit.Text = "";
        OpenDebit.Text = "";
        PeriodCredit.Text = "";
        PeriodDebit.Text = "";
        CloseCredit.Text = "";
        CloseDebit.Text = "";

        DayBookInfo openingBalance = new DayBookInfo();
        List<DayBookInfo> dayBooks = new List<DayBookInfo>();

        LedgersInfo voucherLedger = null;

        DateTime fromDate = DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime toDate = DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        if (Request.Params["type"] != null)
        {
            switch (LedgerType.Value)
            {
                case "1":
                    LedgerName.Text = "Hundi Loan Ledger";
                    openingBalance = AccountsManager.GetHundiLoanOpeningBalance(fromDate, "HL", DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetHundiLoanLedger(fromDate, toDate, "HL", DBConstant.ACCOUNT_PERIOD);
                    break;
                case "2":
                    LedgerName.Text = "Fixed Deposit Ledger";
                    openingBalance = AccountsManager.GetFixedDepositOpeningBalance(fromDate, "FD", DBConstant.ACCOUNT_OPENING);
                    openingBalance.SwapOpeningBalance();
                    dayBooks = LedgersManager.GetFixedDespositLedger(fromDate, toDate, "FD", DBConstant.ACCOUNT_PERIOD);
                    break;
                case "3":
                    LedgerName.Text = "ATKT Ledger";
                    openingBalance = AccountsManager.GetATKTOpeningBalance(fromDate, "ATKT", DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetATKTLedger(fromDate, toDate, "ATKT", DBConstant.ACCOUNT_PERIOD);
                    break;
                case "4":
                    LedgerName.Text = "Chits Ledger";
                    openingBalance = AccountsManager.GetChitsOpeniningBalance(fromDate, DBConstant.CHITS_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetChitLedger(fromDate, toDate, DBConstant.CHITS_LEDGER, DBConstant.ACCOUNT_PERIOD);
                    voucherLedger = LedgersManager.GetLedgersInfo(DBConstant.CHITS_LEDGER);
                    break;
                case "5":
                    LedgerName.Text = "Interest Collected Ledger";
                    openingBalance = AccountsManager.GetInterestOpeningBalance(fromDate, DBConstant.INTEREST_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetInterestLedger(fromDate, toDate);
                    break;
                case "6":
                    LedgerName.Text = "Interest Paid Ledger";
                    openingBalance = AccountsManager.GetInterestPaidOpeningBalance(fromDate, DBConstant.INTEREST_PAID_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetInterestPaidLedger(fromDate, toDate);
                    break;
                case "7":
                    LedgerName.Text = "Chit Commission Ledger";
                    openingBalance = AccountsManager.GetChitCommissionOpeningBalance(fromDate, DBConstant.CHIT_COMMISSION_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetChitCommissionLedger(fromDate, toDate, DBConstant.CHIT_COMMISSION_LEDGER, DBConstant.ACCOUNT_PERIOD);
                    voucherLedger = LedgersManager.GetLedgersInfo(DBConstant.CHIT_COMMISSION_LEDGER);
                    break;
                case "8":
                    LedgerName.Text = "Chit Discount Ledger";
                    openingBalance = AccountsManager.GetChitDiscountOpeningBalance(fromDate, DBConstant.CHIT_DISCOUNT_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetChitDiscountLedger(fromDate, toDate, DBConstant.CHIT_DISCOUNT_LEDGER, DBConstant.ACCOUNT_PERIOD);
                    voucherLedger = LedgersManager.GetLedgersInfo(DBConstant.CHIT_DISCOUNT_LEDGER);
                    break;
                case "9":
                    LedgerName.Text = "Company Bidding Ledger";
                    openingBalance = AccountsManager.GetCompBiddingOpeningBalance(fromDate, DBConstant.COMPANY_BIDDING_LEDGER, DBConstant.ACCOUNT_OPENING);
                    dayBooks = LedgersManager.GetCompanyBiddingLedger(fromDate, toDate, DBConstant.COMPANY_BIDDING_LEDGER, DBConstant.ACCOUNT_PERIOD);
                    voucherLedger = LedgersManager.GetLedgersInfo(DBConstant.COMPANY_BIDDING_LEDGER);
                    break;
            }
            if (voucherLedger != null)
            {
                List<DayBookInfo> commVouchers = AccountsManager.GetVoucherDetails(fromDate, toDate, voucherLedger.LedgerID);
                dayBooks.AddRange(commVouchers);
            }

        }
        else if (Request.Params["ledgerID"] != null)
        {
            if (LedgerName.Text.Equals(DBConstant.CASH_LEDGER))
            {
                openingBalance = AccountsManager.GetCashBookOpeningBalance(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                dayBooks = AccountsManager.GetLedgerDetails(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                          DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                          Convert.ToInt32(LedgerID.Value));
                List<DayBookInfo> hundiLoans = LedgersManager.GetHundiLoanLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), "HL", DBConstant.ACCOUNT_PERIOD);
                dayBooks.AddRange(hundiLoans);

                List<DayBookInfo> fixedDeposit = LedgersManager.GetFixedDespositLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), "FD", DBConstant.ACCOUNT_PERIOD);
                dayBooks.AddRange(fixedDeposit);

                List<DayBookInfo> atktInfos = LedgersManager.GetATKTLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), "ATKT", DBConstant.ACCOUNT_PERIOD);
                dayBooks.AddRange(atktInfos);

                List<DayBookInfo> chitInfos = LedgersManager.GetChitLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), "CHIT", DBConstant.ACCOUNT_PERIOD);
                foreach (DayBookInfo chitInfo in chitInfos)
                {
                    if (!chitInfo.VoucherType.Equals(DBConstant.CHITS_COMMISSION))
                        dayBooks.Add(chitInfo);
                }

                List<DayBookInfo> chitDiscounts = LedgersManager.GetChitDiscountLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DBConstant.CHIT_DISCOUNT_LEDGER, DBConstant.ACCOUNT_PERIOD);
                dayBooks.AddRange(chitDiscounts);

                List<DayBookInfo> chitCompanyBidding = LedgersManager.GetCompanyBiddingLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                      DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), DBConstant.COMPANY_BIDDING_LEDGER, DBConstant.ACCOUNT_PERIOD);
                dayBooks.AddRange(chitCompanyBidding);

                List<DayBookInfo> interestColleted = LedgersManager.GetInterestLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                  DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                foreach (DayBookInfo day in interestColleted)
                {
                    if (day.VoucherType == DBConstant.VOUCHER_HLINTEREST)
                    {
                        dayBooks.Add(day);
                    }
                }

                List<DayBookInfo> interestPaid = LedgersManager.GetInterestPaidLedger(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                  DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                foreach (DayBookInfo day in interestPaid)
                {
                    if (day.VoucherType == DBConstant.VOUCHER_FDINTEREST)
                    {
                        dayBooks.Add(day);
                    }
                }

                dayBooks.Sort(new ReportComparer());
            }
            else
            {
                openingBalance = AccountsManager.GetLedgerOpeningBalance(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture), Convert.ToInt32(LedgerID.Value));
                dayBooks = AccountsManager.GetLedgerDetails(DateTime.ParseExact(FromDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                          DateTime.ParseExact(ToDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                                          Convert.ToInt32(LedgerID.Value));
            }
        }

        if (openingBalance != null)
        {
            if (LedgerType.Value == "1" || LedgerType.Value == "3" || LedgerType.Value == "9" || LedgerType.Value == "4")
            {
                if (openingBalance.Credit > 0)
                {
                    OpenDebit.Text = openingBalance.Credit.ToString("#0.00");
                }
                else if (openingBalance.Debit > 0)
                {
                    OpenCredit.Text = openingBalance.Debit.ToString("#0.00");
                }
            }
            else
            {
                if (openingBalance.Credit > 0)
                {
                    OpenCredit.Text = openingBalance.Credit.ToString("#0.00");
                }
                else if (openingBalance.Debit > 0)
                {
                    OpenDebit.Text = openingBalance.Debit.ToString("#0.00");
                }
            }
        }

        if (dayBooks != null)
            Display(dayBooks);
    }

    protected void Display(List<DayBookInfo> dayBooks)
    {
        dayBooks.Sort(new ReportComparer());
        decimal credit = 0;
        decimal debit = 0;
        HtmlTableRow row;
        HtmlTableCell cell;
        int i = 1;
        foreach (DayBookInfo dayBook in dayBooks)
        {
            row = new HtmlTableRow();
            row.Attributes.Add("style", "cursor:hand");
            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            cell = new HtmlTableCell();
            cell.InnerText = i.ToString();
            cell.Width = "5%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.CurrentDate.ToString("dd/MM/yyyy");
            cell.Width = "10%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.FromLedger.Equals(LedgerName.Text) ? dayBook.ToLedger : dayBook.FromLedger;
            if (GetConditon(dayBook))
                cell.InnerText = dayBook.Debit > 0 ? DBConstant.PARTICULARS_TO + cell.InnerText : DBConstant.PARTICULARS_BY + cell.InnerText;
            else
                cell.InnerText = dayBook.Credit > 0 ? DBConstant.PARTICULARS_BY + cell.InnerText : DBConstant.PARTICULARS_TO + cell.InnerText;

            cell.Width = "25%";
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
            if (GetConditon(dayBook))
            {
                if (dayBook.Debit > 0)
                {
                    cell.InnerText = dayBook.Debit.ToString("#0.00");
                    debit = debit + dayBook.Debit;
                }
                else
                    cell.InnerHtml = "&nbsp;";
            }
            else
            {
                if (dayBook.Credit > 0)
                {
                    cell.InnerText = dayBook.Credit.ToString("#0.00");
                    debit = debit + dayBook.Credit;
                }
                else
                    cell.InnerHtml = "&nbsp;";
            }

            cell.Align = "right";
            cell.Width = "10%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();

            if (GetConditon(dayBook))
            {
                if (dayBook.Credit > 0)
                {
                    cell.InnerText = dayBook.Credit.ToString("#0.00");
                    credit = credit + dayBook.Credit;
                }
                else
                    cell.InnerHtml = "&nbsp;";
            }
            else
            {
                if (dayBook.Debit > 0)
                {
                    cell.InnerText = dayBook.Debit.ToString("#0.00");
                    credit = credit + dayBook.Debit;
                }
                else
                    cell.InnerHtml = "&nbsp;";
            }

            cell.Align = "right";
            cell.Width = "10%";
            row.Cells.Add(cell);

            cell = new HtmlTableCell();
            cell.InnerText = dayBook.Narration;
            if (GetConditon(dayBook))
                cell.InnerText = dayBook.Debit > 0 ? DBConstant.NARATION_TOWARDS + dayBook.Narration : DBConstant.NARATION_FROM + dayBook.Narration;
            else
                cell.InnerText = dayBook.Credit > 0 ? DBConstant.NARATION_FROM + dayBook.Narration : DBConstant.NARATION_TOWARDS + dayBook.Narration;
            cell.Width = "20%";
            row.Cells.Add(cell);

            DetailsTable.Rows.Add(row);
            i++;
        }

        PeriodCredit.Text = credit.ToString("#0.00");
        PeriodDebit.Text = debit.ToString("#0.00");
        if (OpenCredit.Text.Length > 0)
        {
            credit = credit + Convert.ToDecimal(OpenCredit.Text);
        }
        if (OpenDebit.Text.Length > 0)
        {
            debit = debit + Convert.ToDecimal(OpenDebit.Text);
        }
        if (credit > debit)
        {
            credit = credit - debit;
            CloseCredit.Text = credit.ToString("#0.00");
        }
        else if (debit > credit)
        {
            debit = debit - credit;
            CloseDebit.Text = debit.ToString("#0.00");
        }
    }

    private bool GetConditon(DayBookInfo dayBook)
    {
        bool valid = false;
        if (LedgerName.Text.Equals(DBConstant.CASH_LEDGER))
        {
            valid = true;
        }

        if (GroupName.Value.Equals(DBConstant.BANK_LEDGERS) && !dayBook.VoucherType.Equals(AccountsManager.GetVoucherTypeName(DBConstant.VOUCHER_CONTRA)))
        {
            valid = true;
        }
        return valid;
    }
}
