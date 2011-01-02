using System;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];
        if (loggedIn == null)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        UsersInfo loggedIn = (UsersInfo)Session["user"];

        UsersInfo userInfo = new UsersInfo();
        userInfo.UserName = loggedIn.UserName;
        userInfo.Password = loggedIn.Password;
        userInfo.NewPassword = NewPassword.Text;

        try
        {
            UsersManager.ChangePassword(userInfo);
            ClientScript.RegisterClientScriptBlock(GetType(), "AltKey", "<script>alert('Password Changed Successfully.');window.location.href='HomePage.aspx';</script>");
        }
        catch (PAException kx)
        {
            message.Text = kx.Message;
        }
    }
}
