using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;

public partial class SearchLedgers : System.Web.UI.Page
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
            List<GroupsInfo> groups = AccountsManager.GetGroups();
            foreach (GroupsInfo group in groups)
            {
                ListItem item = new ListItem();
                item.Value = group.GroupID.ToString();
                item.Text = group.GroupName;
                GroupID.Items.Add(item);
            }
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {

        GridView1.Visible = true;
        GridView1.DataSourceID = "ObjectDataSource1";
        GridView1.DataBind();
    }

    protected void Gridview_RowBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:hand");
            e.Row.Attributes.Add("onmouseover", "this.oldColor=this.style.backgroundColor;this.style.backgroundColor='highlight';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.oldColor;");

            e.Row.Cells[2].Text = e.Row.Cells[2].Text + "&nbsp;&nbsp;&nbsp;";
        }
    }

}
