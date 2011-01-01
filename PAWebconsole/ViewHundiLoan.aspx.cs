using System;
using System.Data;
using System.Configuration;
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


public partial class ViewHundiLoan : System.Web.UI.Page
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
                if (Request.Params["HlLoanno"] != null)
                {
                    HLLoanNo.Text = Request.Params["HlLoanno"];
                    Link1.Attributes.Remove("href");
                    Link1.Attributes.Add("href", "HundiTrans.aspx?HlLoanno=" + HLLoanNo.Text);
                    Link2.Attributes.Remove("href");
                    Link2.Attributes.Add("href", "HundiInterest.aspx?HlLoanno=" + HLLoanNo.Text);

                    HundiLoanInfo hundiLoan = HundiLoanManager.GetHundiLoanInfo(HLLoanNo.Text);
                    if (hundiLoan != null)
                    {
                        CustomerInfo customer = CustomerManager.GetCustomerInfo(hundiLoan.CustomerID);
                        FatherName.Text = customer.SonHusband;
                        Address.Text = customer.FullAddress;

                        HLLoanNo.Text = hundiLoan.HlLoanno;
                        LoanDate.Text = hundiLoan.LoanDate.ToString("dd/MM/yyyy");
                        CustomerName.Text = hundiLoan.CustomerName;
                        CoObligent.Text = hundiLoan.CoObligent;
                        CoObligentAddress.Text = hundiLoan.CoobligentAddress;
                        LoanAmount.Text = hundiLoan.LoanAmount.ToString();
                        Rate.Text = hundiLoan.Rate.ToString("#0.00");
                        AccountNo.Text = hundiLoan.AccountNo;
                        ClosedType.Value = hundiLoan.Closed;
                    }
                }
            }
        }
    }

    public void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            HundiLoanManager.DeleteHundiLoanInfo(HLLoanNo.Text);
            Response.Redirect("HundiLoans.aspx");
        }
        catch (PAException pe)
        {
            throw new PAException(pe.Message);
        }
    }
}
