using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ResPhone.Attributes.Add("onkeypress", "onlyDigits(this);");
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
                List<CityInfo> cities = CityManager.GetCityInfos();

                foreach (CityInfo city in cities)
                {
                    ListItem item = new ListItem();

                    item.Text = city.VillageName;
                    item.Value = city.CityID.ToString();

                    ResVillage.Items.Add(item);
                }

                long accountNo = CustomerManager.GetNextAccountNo();
                AccountNo.Text = accountNo.ToString();

                if (Request.Params["customerid"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    CustomerID.Value = Request.Params["customerid"];
                    content.InnerText = "Update Customer";

                    CustomerInfo customer = CustomerManager.GetCustomerInfo(Convert.ToInt32(CustomerID.Value));
                    if (customer != null)
                    {
                        CustomerName.Text = customer.CustomerName;
                        FatherHusband.Text = customer.SonHusband;
                        AccountNo.Text = customer.AccountNO.ToString();
                        ResAddress.Text = customer.ResAddress;
                        ResVillage.SelectedValue = customer.ResVillage.ToString();
                        ResPhone.Text = customer.ResPhone;

                        Residence_Selected(sender, e);
                    }
                }
            }
        }
    }

    public void Save_Click(object sender, EventArgs e)
    {
        try
        {
            CustomerInfo customer = new CustomerInfo();

            customer.CustomerName = CustomerName.Text;
            customer.SonHusband = FatherHusband.Text;
            customer.AccountNO = Convert.ToInt32(AccountNo.Text);
            customer.ResAddress = ResAddress.Text;
            customer.ResVillage = Convert.ToInt32(ResVillage.SelectedValue);
            customer.ResPhone = ResPhone.Text;

            if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                customer.CustomerID = Convert.ToInt32(CustomerID.Value);
                CustomerManager.UpdateCustomerInfo(customer);
            }
            else if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                CustomerManager.AddCustomerInfo(customer);
            }
            Response.Redirect("Customer.aspx");
        }
        catch (PAException pe)
        {
            message.Text = pe.Message;
        }
    }

    public void Residence_Selected(object sender, EventArgs e)
    {
        if (ResVillage.SelectedValue.Length > 0)
        {
            CityInfo city = CityManager.GetCityInfo(Convert.ToInt32(ResVillage.SelectedValue));

            FullAddress.Text = city.VillageName + "," + city.CityName + " - " + city.Pincode;
        }
    }
}
