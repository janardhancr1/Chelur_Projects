using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Model;


public partial class Groups : System.Web.UI.Page
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
                item.Text = group.GroupName;
                item.Value = group.GroupID.ToString();

                GroupNames.Items.Add(item);
            }
        }
    }
}
