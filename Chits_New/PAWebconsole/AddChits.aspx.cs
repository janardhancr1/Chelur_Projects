using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddChits : System.Web.UI.Page
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

                string nextNo = UtilManager.GetNextSerial("Chits", "Chit_No");
                int num = 0;
                if (nextNo.Length > 0)
                    num = Convert.ToInt32(nextNo.Substring(2));
                num = num + 1;
                const string zeros = "000";
                string n = num.ToString();
                if (num < 999)
                    ChitNO.Text = "CH" + zeros.Substring(0, zeros.Length - n.Length) + num;
                else
                    ChitNO.Text = "CH" + num;


                if (Request.Params["chitNO"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    ChitNO.Text = Request.Params["chitNO"];
                    ActionTitle.Text = "Modify/Delete Chits";
                    DeleteButton.Visible = true;

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

    protected void Save_Click(object sender, EventArgs e)
    {
        ChitsInfo chitsInfo = new ChitsInfo();

        chitsInfo.ChitNO = ChitNO.Text;
        chitsInfo.ChitName = ChitName.Text;
        if (ChitAmount.Text.Trim().Length > 0) chitsInfo.ChitAmount = Convert.ToDecimal(ChitAmount.Text);
        if (InstallmentAmount.Text.Trim().Length > 0) chitsInfo.InstallmentAmount = Convert.ToDecimal(InstallmentAmount.Text);
        if (NoInstallments.Text.Trim().Length > 0) chitsInfo.NoInstallments = Convert.ToDecimal(NoInstallments.Text);
        chitsInfo.Closed = DBConstant.TYPE_PENDING;

        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                ChitsManager.AddChitsInfo(chitsInfo);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                ChitsManager.UpdateChitsInfo(chitsInfo);
            }
            Response.Redirect("Chits.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            ChitsManager.DeleteChitsInfo(ChitNO.Text);
            Response.Redirect("Chits.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }
}
