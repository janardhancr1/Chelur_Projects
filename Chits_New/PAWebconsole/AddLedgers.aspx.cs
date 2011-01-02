using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddLedgers : System.Web.UI.Page
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
            LedgerID.Value = LedgersManager.GetNextLedgerID();
            mode.Value = DBConstant.MODE_ADD.ToString();
            OpeningBalance.Attributes.Add("onkeypress", "onlyFloat(this);");
            List<GroupsInfo> groups = AccountsManager.GetGroups();
            foreach (GroupsInfo group in groups)
            {
                ListItem item = new ListItem();
                item.Value = group.GroupID.ToString();
                item.Text = group.GroupName;
                GroupID.Items.Add(item);
            }

            if (Request.Params["ledgerID"] != null)
            {
                mode.Value = DBConstant.MODE_UPDATE.ToString();
                LedgerID.Value = Request.Params["ledgerID"];
                ActionTitle.Text = "Modify/Delete Ledgers";
                DeleteButton.Visible = true;
                NewButton.Visible = true;

                LedgersInfo ledgersInfo = LedgersManager.GetLedgersInfo(Convert.ToInt32(LedgerID.Value));
                if (ledgersInfo != null)
                {
                    LedgerName.Text = ledgersInfo.LedgerName;
                    if(ledgersInfo.LedgerName.Equals(DBConstant.CASH_LEDGER) || ledgersInfo.LedgerName.Equals(DBConstant.INTEREST_LEDGER)
                     || ledgersInfo.LedgerName.Equals(DBConstant.INTEREST_PAID_LEDGER))
                    {
                        DeleteButton.Visible = false;
                    }
                    OpeningBalance.Text = ledgersInfo.OpeningBalance.ToString();
                    BalanceType.SelectedValue = ledgersInfo.BalanceType;
                    GroupID.SelectedValue = ledgersInfo.GroupID.ToString();
                }
            }
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        LedgersInfo ledgersInfo = new LedgersInfo();

        ledgersInfo.LedgerName = LedgerName.Text;
        if (OpeningBalance.Text.Trim().Length > 0) ledgersInfo.OpeningBalance = Convert.ToDecimal(OpeningBalance.Text);
        ledgersInfo.BalanceType = BalanceType.Text;
        if (GroupID.Text.Trim().Length > 0) ledgersInfo.GroupID = Convert.ToInt32(GroupID.Text);

        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                LedgersManager.AddLedgersInfo(ledgersInfo);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                ledgersInfo.LedgerID = Convert.ToInt32(LedgerID.Value);
                LedgersManager.UpdateLedgersInfo(ledgersInfo);
            }
            Response.Redirect("AddLedgers.aspx?ledgerID=" + LedgerID.Value);
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
            LedgersManager.DeleteLedgersInfo(Convert.ToInt32(LedgerID.Value));
            Response.Redirect("AddLedgers.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }
}
