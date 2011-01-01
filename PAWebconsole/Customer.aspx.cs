using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;

public partial class Customer : System.Web.UI.Page
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
            if(!IsPostBack)
            {
                List<CityInfo> cities = CityManager.GetCityInfos();

                foreach (CityInfo city in cities)
                {
                    ListItem item = new ListItem();

                    item.Text = city.VillageName;
                    item.Value = city.CityID.ToString();

                    ResVillage.Items.Add(item);
                }
            }
        }
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='LightBlue';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        GridView1.DataSourceID = "ObjectDataSource1";
        GridView1.DataBind();
    }
}
