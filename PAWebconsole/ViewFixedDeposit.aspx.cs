using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;

public partial class ViewFixedDeposit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo) Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.Params["FDNo"] != null)
                {
                    FDNo.Text = Request.Params["FDNo"];
                    Link1.Attributes.Remove("href");
                    Link1.Attributes.Add("href", "FDTrans.aspx?FDNo=" + FDNo.Text);
                    Link2.Attributes.Remove("href");
                    Link2.Attributes.Add("href", "FDInterest.aspx?FDNo=" + FDNo.Text);

                    FixedDepositInfo fixedDeposit = FixedDepositManager.GetFixedDepositInfo(FDNo.Text);
                    if (fixedDeposit != null)
                    {
                        CustomerInfo customer = CustomerManager.GetCustomerInfo(fixedDeposit.CustomerID);
                        FatherName.Text = customer.SonHusband;
                        Address.Text = customer.FullAddress;

                        FDNo.Text = fixedDeposit.FDNO;
                        FDDate.Text = fixedDeposit.StartDate.ToString("dd/MM/yyyy");
                        CustomerName.Text = fixedDeposit.CustomerName;
                        NomineeName.Text = fixedDeposit.NomineeName;
                        NomineeRelation.Text = fixedDeposit.Relationship;
                        FDAmount.Text = fixedDeposit.Amount.ToString();
                        Rate.Text = fixedDeposit.Rate.ToString("#0.00");
                    }
                }
            }
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            FixedDepositManager.DeleteFixedDepositInfo(FDNo.Text);
            Response.Redirect("FixedDesposits.aspx");
        }
        catch (PAException ex)
        {
            throw new PAException(ex.Message);
        }
    }
}
