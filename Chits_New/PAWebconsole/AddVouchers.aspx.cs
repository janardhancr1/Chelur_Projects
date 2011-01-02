using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using PALibrary.Library.Component;
using PALibrary.Library.Exception;
using PALibrary.Library.Model;
using PALibrary.Library.Utils;

public partial class AddVouchers : System.Web.UI.Page
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
            VoucherID.Value = VouchersManager.GetNextVoucherID();
            mode.Value = DBConstant.MODE_ADD.ToString();
            VoucherNO.Attributes.Add("onkeypress", "onlyFloat(this);");
            Amount.Attributes.Add("onkeypress", "onlyFloat(this);");
            VoucherNO.Attributes.Add("style", "text-align: right;");
            Amount.Attributes.Add("style", "text-align: right;");
            VoucherDate.Attributes.Add("onfocus", "showCalendarControl(this);");

            DBConstant appConstants = (DBConstant)Session["AppConstants"];
            VoucherDate.Value = appConstants.CurrentDate.ToString("dd/MM/yyyy");

            List<VoucherTypesInfo> voucherTypes = AccountsManager.GetVoucherTypes();
            foreach (VoucherTypesInfo voucherType in voucherTypes)
            {
                ListItem item = new ListItem();
                item.Value = voucherType.VoucherTypeID.ToString();
                item.Text = voucherType.VoucherTypeName;
                VoucherType.Items.Add(item);
            }

            if (Request.Params["voucherID"] != null)
            {
                mode.Value = DBConstant.MODE_UPDATE.ToString();
                VoucherID.Value = Request.Params["voucherID"];
                ActionTitle.Text = "Modify/Delete Vouchers";
                DeleteButton.Visible = true;
                NewButton.Visible = true;

                VouchersInfo vouchersInfo = VouchersManager.GetVouchersInfo(Convert.ToInt32(VoucherID.Value));
                if (vouchersInfo != null)
                {
                    VoucherDate.Value = vouchersInfo.VoucherDate.ToString("dd/MM/yyyy");
                    VoucherType.SelectedValue = vouchersInfo.VoucherType.ToString();
                    VoucherType_Selected(sender, e);
                    VoucherNO.Text = vouchersInfo.VoucherNO.ToString();
                    FromLedger.Text = vouchersInfo.FromLedger.ToString();
                    ToLedger.Text = vouchersInfo.ToLedger.ToString();
                    Amount.Text = vouchersInfo.Amount.ToString();
                    Narration.Text = vouchersInfo.Narration;
                }
            }
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        VouchersInfo vouchersInfo = new VouchersInfo();

        if (VoucherID.Value.Trim().Length > 0) vouchersInfo.VoucherID = Convert.ToInt32(VoucherID.Value);
        if (VoucherDate.Value.Trim().Length > 0) vouchersInfo.VoucherDate = DateTime.ParseExact(VoucherDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (VoucherType.SelectedValue.Trim().Length > 0) vouchersInfo.VoucherType = Convert.ToInt32(VoucherType.SelectedValue);
        if (VoucherNO.Text.Trim().Length > 0) vouchersInfo.VoucherNO = Convert.ToInt32(VoucherNO.Text);
        if (FromLedger.Text.Trim().Length > 0) vouchersInfo.FromLedger = Convert.ToInt32(FromLedger.Text);
        if (ToLedger.Text.Trim().Length > 0) vouchersInfo.ToLedger = Convert.ToInt32(ToLedger.Text);
        if (Amount.Text.Trim().Length > 0) vouchersInfo.Amount = Convert.ToDecimal(Amount.Text);
        vouchersInfo.Narration = Narration.Text;

        try
        {
            if (mode.Value.Equals(DBConstant.MODE_ADD.ToString()))
            {
                VouchersManager.AddVouchersInfo(vouchersInfo);
            }
            else if (mode.Value.Equals(DBConstant.MODE_UPDATE.ToString()))
            {
                VouchersManager.UpdateVouchersInfo(vouchersInfo);
            }
            Response.Redirect("AddVouchers.aspx?voucherID=" + VoucherID.Value);
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
            VouchersManager.DeleteVouchersInfo(Convert.ToInt32(VoucherID.Value));
            Response.Redirect("AddVouchers.aspx");
        }
        catch (PAException ex)
        {
            ErrorMessage.Text = ex.Message;
        }
    }

    protected void VoucherType_Selected(object sender, EventArgs e)
    {
        FromLedger.Items.Clear();
        ToLedger.Items.Clear();

        FromLedger.Items.Add(new ListItem("--Select--", ""));
        ToLedger.Items.Add(new ListItem("--Select--", ""));
        if (VoucherType.SelectedValue.Length > 0)
        {
            if (VoucherDate.Value.Trim().Length > 0)
            {
                VoucherNO.Text = AccountsManager.NextVoucherNo(DateTime.ParseExact(VoucherDate.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                                               Convert.ToInt32(VoucherType.SelectedValue)).ToString();
            }
            else
            {
                ErrorMessage.Text = "No Date Selected";
            }
            List<LedgersInfo> ledgers = LedgersManager.GetLedgersInfos();
            switch (Convert.ToUInt32(VoucherType.SelectedValue))
            {
                case DBConstant.VOUCHER_PAYMENT:
                    foreach (LedgersInfo ledger in ledgers)
                    {
                        if (ledger.GroupName.Equals(DBConstant.CASH_LEDGERS) || ledger.GroupName.Equals(DBConstant.BANK_LEDGERS))
                            FromLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                        else
                            ToLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                    }
                    break;
                case DBConstant.VOUCHER_RECEIPT:
                    foreach (LedgersInfo ledger in ledgers)
                    {
                        if (ledger.GroupName.Equals(DBConstant.CASH_LEDGERS) || ledger.GroupName.Equals(DBConstant.BANK_LEDGERS))
                            ToLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                        else
                            FromLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                    }
                    break;
                case DBConstant.VOUCHER_CONTRA:
                    foreach (LedgersInfo ledger in ledgers)
                    {
                        if (ledger.GroupName.Equals(DBConstant.CASH_LEDGERS) || ledger.GroupName.Equals(DBConstant.BANK_LEDGERS))
                        {
                            FromLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                            ToLedger.Items.Add(new ListItem(ledger.LedgerName, ledger.LedgerID.ToString()));
                        }
                    }
                    break;
            }
        }
        else
        {
            VoucherNO.Text = "";
        }
    }
}
