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

public partial class AddATKT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Amount.Attributes.Add("onkeypress", "onlyFloat(this);");
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

                TranType.Items.Clear();

                ListItem item1 = null;

                item1 = new ListItem();
                item1.Value = "";
                item1.Text = "--Select--";
                TranType.Items.Add(item1);

                item1 = new ListItem();
                item1.Value = DBConstant.ATKT_PAY;
                item1.Text = "Payment";
                TranType.Items.Add(item1);

                item1 = new ListItem();
                item1.Value = DBConstant.ATKT_RECP;
                item1.Text = "Receipt";
                TranType.Items.Add(item1);

                DBConstant appConstants = (DBConstant)Session["AppConstants"];

                if (appConstants != null)
                {
                    ATKTDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");
                }

                string nextNo = UtilManager.GetNextSerial("AT_KT", "ATKT_No");
                int num = 0;
                if (nextNo.Length > 0)
                    num = Convert.ToInt32(nextNo.Substring(2));
                num = num + 1;
                const string zeros = "000";
                string n = num.ToString();
                if (num < 999)
                    ATKTNO.Text = "AK" + zeros.Substring(0, zeros.Length - n.Length) + num;
                else
                    ATKTNO.Text = "AK" + num;

                if (Request.Params["recordID"] != null)
                {
                    mode.Value = DBConstant.MODE_UPDATE.ToString();
                    RecordID.Value = Request.Params["recordID"];
                    ActionTitle.Text = "Modify ATKT";

                    ATKTInfo aTKTInfo = ATKTManager.GetATKTInfo(Convert.ToInt32(RecordID.Value));
                    if (aTKTInfo != null)
                    {
                        ATKTNO.Text = aTKTInfo.ATKTNO.ToString();
                        PartyName.Text = aTKTInfo.PartyName;
                        ATKTDate.Value = aTKTInfo.ATKTDate.ToString("dd/MM/yyyy");
                        TranType.Text = aTKTInfo.TranType;
                        Amount.Text = aTKTInfo.Amount.ToString();
                        Remarks.Text = aTKTInfo.Remarks;
                    }
                }
            }
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        ATKTInfo aTKTInfo = new ATKTInfo();

        aTKTInfo.ATKTNO = ATKTNO.Text;
        aTKTInfo.PartyName = PartyName.Text;
        if (ATKTDate.Value.Trim().Length > 0) aTKTInfo.ATKTDate = DateTime.ParseExact(ATKTDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        aTKTInfo.TranType = TranType.Text;
        if (Amount.Text.Trim().Length > 0) aTKTInfo.Amount = Convert.ToDecimal(Amount.Text);
        aTKTInfo.Remarks = Remarks.Text;
        aTKTInfo.Closed = DBConstant.TYPE_PENDING;
        
        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                ATKTManager.AddATKTInfo(aTKTInfo);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                ATKTManager.UpdateATKTInfo(aTKTInfo);
            }
            Response.Redirect("ATKT.aspx");
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
            ATKTManager.DeleteATKTInfo(Convert.ToInt32(RecordID.Value));
            Response.Redirect("AddAt_kt.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }

}
