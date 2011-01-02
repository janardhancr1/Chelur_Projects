using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddHundiLoan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoanAmount.Attributes.Add("onkeypress", "onlyFloat(this);");
        Rate.Attributes.Add("onkeypress", "onlyFloat(this);");
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                mode.Value = DBConstant.MODE_ADD.ToString();

                List<CustomerInfo> customers = CustomerManager.GetCustomerInfos();
                foreach (CustomerInfo cust in customers)
                {
                    ListItem item = new ListItem();
                    item.Text = cust.CustomerName;
                    item.Value = cust.CustomerID.ToString();

                    CustomerName.Items.Add(item);
                }

                List<LedgersInfo> banks = LedgersManager.GetLedgersInfos();
                foreach (LedgersInfo bank in banks)
                {
                    if (bank.GroupID == 14)
                    {
                        ListItem item2 = new ListItem();
                        item2.Value = bank.LedgerID.ToString();
                        item2.Text = bank.LedgerName;
                        BankName.Items.Add(item2);
                    }
                }

                DBConstant appConstants = (DBConstant)Session["AppConstants"];

                if (appConstants != null)
                {
                    LoanDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
                }

                string nextNo = UtilManager.GetNextSerial("hundiloans", "hl_loanno");
                int num = 0;
                if (nextNo.Length > 0)
                    num = Convert.ToInt32(nextNo.Substring(2));
                num = num + 1;
                const string zeros = "000";
                string n = num.ToString();
                if (num < 999)
                    HLLoanNo.Text = "HL" + zeros.Substring(0, zeros.Length - n.Length) + num;
                else
                    HLLoanNo.Text = "HL" + num;

                if (Request.Params["recordid"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    RecordID.Value = Request.Params["recordid"];
                    content.InnerText = "Update Hundi Loan";

                    HundiLoanInfo hundiLoan = HundiLoanManager.GetHundiLoanInfo(RecordID.Value);
                    if (hundiLoan != null)
                    {
                        HLLoanNo.Text = hundiLoan.HlLoanno;
                        LoanDate.Value = hundiLoan.LoanDate.ToString("dd/MM/yyyy");
                        CustomerName.SelectedValue = hundiLoan.CustomerID.ToString();
                        CoObligent.Text = hundiLoan.CoObligent;
                        CoObligentAddress.Text = hundiLoan.CoobligentAddress;
                        LoanAmount.Text = hundiLoan.LoanAmount.ToString();
                        Rate.Text = hundiLoan.Rate.ToString("#0.00");
                        PayMode.SelectedValue = hundiLoan.PayMode.ToString();
                        ChequeNo.Text = hundiLoan.ChequeNO;
                        BankName.SelectedValue = hundiLoan.BankID.ToString();
                        PayMode_Selected(sender, e);
                        CustomerName_Selected(sender, e);
                    }
                }
            }
        }
    }

    public void PayMode_Selected(object sender, EventArgs e)
    {
        if (PayMode.SelectedValue.Equals("2"))
        {
            ChequeRow.Visible = true;
            BankRow.Visible = true;
        }
        else
        {
            ChequeRow.Visible = false;
            BankRow.Visible = false;
        }
    }

    public void Save_Click(object sender, EventArgs e)
    {
        HundiLoanInfo hundiLoan = new HundiLoanInfo();

        hundiLoan.HlLoanno = HLLoanNo.Text;
        hundiLoan.LoanAmount = Convert.ToDecimal(LoanAmount.Text);
        hundiLoan.LoanDate = DateTime.ParseExact(LoanDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        hundiLoan.CustomerID = Convert.ToInt32(CustomerName.SelectedValue);
        hundiLoan.PayMode = Convert.ToInt32(PayMode.SelectedValue);
        hundiLoan.ChequeNO = ChequeNo.Text;
        hundiLoan.BankID = Convert.ToInt32(BankName.SelectedValue);
        hundiLoan.CoObligent = CoObligent.Text;
        hundiLoan.CoobligentAddress = CoObligentAddress.Text;
        hundiLoan.Rate = Convert.ToDecimal(Rate.Text);
        hundiLoan.Closed = DBConstant.TYPE_PENDING;

        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                HundiLoanManager.AddHundiLoanInfo(hundiLoan);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                HundiLoanManager.UpdateHundiLoanInfo(hundiLoan);
            }
            Response.Redirect("HundiLoans.aspx");
        }
        catch (PAException pe)
        {
            ErrorMessage.Text = pe.Message;
        }
    }

    public void CustomerName_Selected(object sender, EventArgs e)
    {
        AccountNo.Text = "";
        Address.Text = "";
        if (CustomerName.SelectedValue.Trim().Length > 0)
        {
            CustomerInfo customer = CustomerManager.GetCustomerInfo(Convert.ToInt32(CustomerName.SelectedValue));
            AccountNo.Text = customer.AccountNO.ToString();
            Address.Text = customer.ResAddress + customer.FullAddress;
        }
    }
}
