using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ViewCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DeleteButton.Attributes.Add("onclick", "javascript:return confirmDelete();");
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.Params["customerid"] != null)
                {
                    CustomerID.Value = Request.Params["customerid"];

                    CustomerInfo customer = CustomerManager.GetCustomerInfo(Convert.ToInt32(CustomerID.Value));
                    if (customer != null)
                    {
                        CustomerName.Text = customer.CustomerName;
                        FatherHusband.Text = customer.SonHusband;
                        AccountNo.Text = customer.AccountNO.ToString();
                        ResAddress.Text = customer.ResAddress;
                        ResPhone.Text = customer.ResPhone;

                        CityInfo city = CityManager.GetCityInfo(customer.ResVillage);
                        ResCitypin.Text = city.VillageName + "," + city.CityName + " - " + city.Pincode;

                        HtmlTableRow row = null;
                        HtmlTableCell cell = null;

                        List<DayBookInfo> loans = CustomerManager.GetCustomerLoans(Convert.ToInt32(CustomerID.Value));
                        foreach (DayBookInfo loan in loans)
                        {
                            row = new HtmlTableRow();
                            row.Attributes.Add("style", "cursor:hand");
                            row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
                            row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

                            cell = new HtmlTableCell();
                            cell.InnerText = loan.VoucherType;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = loan.Narration;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = loan.CurrentDate.ToString("dd/MM/yyyy");
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            if (loan.Debit > 0)
                                cell.InnerText = loan.Debit.ToString("#0.00");
                            else
                                cell.InnerText = "";
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            if (loan.Particulars.Equals(DBConstant.TYPE_CLOSED))
                                cell.InnerText = "Closed";
                            else
                                cell.InnerText = "";
                            row.Cells.Add(cell);

                            //if (loan.Particulars.Equals(DBConstant.TYPE_CLOSED))
                            //    row.Attributes.Add("bgcolor", "red");

                            LoanTable.Rows.Add(row);
                        }
                    }
                }
            }
        }
    }

    public void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            CustomerManager.DeleteCustomerInfo(Convert.ToInt32(CustomerID.Value));
            Response.Redirect("Customer.aspx");
        }
        catch (PAException pe)
        {
            throw pe;
        }
    }
}
