using System;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddCity : System.Web.UI.Page
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
                mode.Value = DBConstant.MODE_ADD.ToString();
                if (Request.Params["cityid"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    CityID.Value = Request.Params["cityid"];
                    content.InnerText = "Update Village";

                    CityInfo city = CityManager.GetCityInfo((Convert.ToInt32(CityID.Value)));
                    if (city != null)
                    {
                        VillageName.Text = city.VillageName;
                        CityName.Text = city.CityName;
                        StateName.SelectedValue = city.State;
                        Pincode.Text = city.Pincode;
                    }
                }
            }
        }
    }

    public void Save_Click(object sender, EventArgs e)
    {
        try
        {
            CityInfo city = new CityInfo();

            city.VillageName = VillageName.Text;
            city.CityName = CityName.Text;
            city.State = StateName.Text;
            city.Pincode = Pincode.Text;
            if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                city.CityID = Convert.ToInt32(CityID.Value);
                CityManager.UpdateCityInfo(city);
            }
            else if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                CityManager.AddCityInfo(city);
            }
            Response.Redirect("Cities.aspx");
        }
        catch (PAException pe)
        {
            message.Text = pe.Message;
        }
    }
}
