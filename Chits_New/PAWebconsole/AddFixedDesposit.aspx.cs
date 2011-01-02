using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddFixedDesposit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FDAmount.Attributes.Add("onkeypress", "onlyFloat(this);");
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

                DBConstant appConstants = (DBConstant)Session["AppConstants"];

                if (appConstants != null)
                {
                    FDDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
                }

                string nextNo = UtilManager.GetNextSerial("FixedDeposit", "FD_No");
                int num = 0;
                if (nextNo.Length > 0)
                    num = Convert.ToInt32(nextNo.Substring(2));
                num = num + 1;
                const string zeros = "000";
                string n = num.ToString();
                if (num < 999)
                    FDNo.Text = "FD" + zeros.Substring(0, zeros.Length - n.Length) + num;
                else
                    FDNo.Text = "FD" + num;

                if (Request.Params["recordid"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    RecordID.Value = Request.Params["recordid"];
                    content.InnerText = "Update Fixed Deposit";

                    FixedDepositInfo fixedDeposit = FixedDepositManager.GetFixedDepositInfo(RecordID.Value);
                    if (fixedDeposit != null)
                    {
                        FDNo.Text = fixedDeposit.FDNO;
                        FDDate.Value = fixedDeposit.StartDate.ToString("dd/MM/yyyy");
                        CustomerName.SelectedValue = fixedDeposit.CustomerID.ToString();
                        NomineeName.Text = fixedDeposit.NomineeName;
                        NomineeRelation.Text = fixedDeposit.Relationship;
                        FDAmount.Text = fixedDeposit.Amount.ToString();
                        Rate.Text = fixedDeposit.Rate.ToString("#0.00");
                        CustomerName_Selected(sender, e);
                    }
                }
            }
        }
    }

    public void CustomerName_Selected(object sender, EventArgs e)
    {
        AccountNo.Text = "";
        if (CustomerName.SelectedValue.Trim().Length > 0)
        {
            CustomerInfo customer = CustomerManager.GetCustomerInfo(Convert.ToInt32(CustomerName.SelectedValue));
            AccountNo.Text = customer.AccountNO.ToString();
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        FixedDepositInfo fixedDepositInfo = new FixedDepositInfo();

        fixedDepositInfo.FDNO = FDNo.Text;
        if (CustomerName.Text.Trim().Length > 0) fixedDepositInfo.CustomerID = Convert.ToInt32(CustomerName.Text);
        if (FDDate.Value.Trim().Length > 0) fixedDepositInfo.StartDate = DateTime.ParseExact(FDDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        fixedDepositInfo.NomineeName = NomineeName.Text;
        fixedDepositInfo.Relationship = NomineeRelation.Text;
        if (FDAmount.Text.Trim().Length > 0) fixedDepositInfo.Amount = Convert.ToDecimal(FDAmount.Text);
        if (Rate.Text.Trim().Length > 0) fixedDepositInfo.Rate = Convert.ToDecimal(Rate.Text);
        fixedDepositInfo.Closed = DBConstant.TYPE_PENDING;

        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                FixedDepositManager.AddFixedDepositInfo(fixedDepositInfo);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                FixedDepositManager.UpdateFixedDepositInfo(fixedDepositInfo);
            }
            Response.Redirect("FixedDesposits.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }

    }

    

}
