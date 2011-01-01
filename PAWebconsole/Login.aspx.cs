using System;
using System.Globalization;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AppConstants"] = null;
        Session["user"] = null;
        if (!IsPostBack)
            CurrentDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
    }

    public void Login_Click(object sender, EventArgs e)
    {
        try
        {
            UsersInfo user = new UsersInfo();
            user.UserName = UserName.Text;
            user.Password = Password.Text;
            if (UsersManager.ValidateUser(user))
            {
                DBConstant appConstants = new DBConstant();
                DateTime currentDate = DateTime.ParseExact(CurrentDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                appConstants.CurrentDate = currentDate;
                int year;
                if (currentDate.Month <= 3)
                {
                    year = currentDate.Year - 1;
                    appConstants.FinYearStart = Convert.ToDateTime("04/01/" + year);
                    appConstants.FinYearEnd = Convert.ToDateTime("03/31/" + currentDate.Year);
                }
                else if (currentDate.Month >= 4)
                {
                    year = currentDate.Year + 1;
                    appConstants.FinYearStart = Convert.ToDateTime("04/01/" + currentDate.Year);
                    appConstants.FinYearEnd = Convert.ToDateTime("03/31/" + year);
                }
                Session["AppConstants"] = appConstants;
                Session["user"] = user;
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                ErrorMessage.Text = "Invalid UserName/Password";
            }
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }
}
