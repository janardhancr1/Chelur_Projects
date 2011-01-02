using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class ViewATKT : System.Web.UI.Page
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
                if (Request.Params["recordID"] != null)
                {
                    RecordID.Value = Request.Params["recordID"];
                    ATKTInfo aTKTInfo = ATKTManager.GetATKTInfo(Convert.ToInt32(RecordID.Value));
                    if (aTKTInfo != null)
                    {
                        ATKTNO.Text = aTKTInfo.ATKTNO.ToString();
                        PartyName.Text = aTKTInfo.PartyName;
                        ATKTDate.Text = aTKTInfo.ATKTDate.ToString("dd/MM/yyyy");
                        TranType.Text = aTKTInfo.TranType;
                        Amount.Text = aTKTInfo.Amount.ToString();
                        Remarks.Text = aTKTInfo.Remarks;

                        if(aTKTInfo.Closed == DBConstant.TYPE_CLOSED)
                        {
                            CloseButton.Visible = false;
                            Closed.Text = "Closed";
                            ClosedDate.Text = aTKTInfo.ClosedDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            Closed.Text = "Pending";
                            CloseButton.Text = aTKTInfo.TranType == DBConstant.ATKT_PAY ? "Receive" : "Pay";    
                        }
                        
                    }
                }
            }
        }
    }

    protected void Closed_Click(object sender, EventArgs e)
    {
        ATKTInfo aTKTInfo = new ATKTInfo();
        DBConstant appConstants = (DBConstant)Session["AppConstants"];
        aTKTInfo.ATKTNO = ATKTNO.Text;
        aTKTInfo.RecordID = Convert.ToInt32(RecordID.Value);
        aTKTInfo.Closed = DBConstant.TYPE_CLOSED;
        aTKTInfo.ClosedDate = appConstants.CurrentDate;

        try
        {
            ATKTManager.CloseATKT(aTKTInfo);
            Response.Redirect("ATKT.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }

    public void Delete_Click(object sender, EventArgs e)
    {
        try
        {
            ATKTManager.DeleteATKTInfo(Convert.ToInt32(RecordID.Value));
            Response.Redirect("ATKT.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }
}
