using System;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;

public partial class ViewCity : System.Web.UI.Page
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
                if (Request.Params["cityid"] != null)
                {
                    CityID.Value = Request.Params["cityid"];

                    CityInfo city = CityManager.GetCityInfo(Convert.ToInt32(CityID.Value));
                    if (city != null)
                    {
                        VillageName.Text = city.VillageName;
                        CityName.Text = city.CityName;
                        StateName.Text = city.State;
                        Pincode.Text = city.Pincode;
                    }
                }
            }
        }
    }

    public void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            CustomerManager.DeleteCustomerInfo(Convert.ToInt32(CityID.Value));
            Response.Redirect("Cities.aspx");
        }
        catch (PAException pe)
        {
            throw pe;
        }
    }
}
