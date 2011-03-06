using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ViewChits : System.Web.UI.Page
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
                if (Request.Params["chitNO"] != null)
                {
                    ChitNO.Text = Request.Params["chitNO"];
                    //DeleteButton.Visible = true;

                    ChitsInfo chitsInfo = ChitsManager.GetChitsInfo(ChitNO.Text);
                    if (chitsInfo != null)
                    {
                        ChitName.Text = chitsInfo.ChitName;
                        ChitAmount.Text = chitsInfo.ChitAmount.ToString();
                        InstallmentAmount.Text = chitsInfo.InstallmentAmount.ToString();
                        NoInstallments.Text = chitsInfo.NoInstallments.ToString();
                    }
                }
            }
        }
    }
}
