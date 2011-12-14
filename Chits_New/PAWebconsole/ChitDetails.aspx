<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitDetails.aspx.cs" Inherits="ChitDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Chit Details</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Chit No</td>
                        <td>
                            <input type="text" id="ChitNO" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Chit Name</td>
                        <td>
                            <input type="text" id="ChitName" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chit Amount</td>
                        <td>
                            <input type="text" id="ChitAmount" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Chit Commission</td>
                        <td>
                            <input type="text" id="ChitCommission" runat="server" readonly /></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Paid Date
                        </td>
                        <td>
                            <input type="text" id="PaidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PaidDate"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer
                        </td>
                        <td>
                            <asp:DropDownList ID="Customer" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Customer"
                                Display="Dynamic">Select customer</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Installment No
                        </td>
                        <td>
                            <asp:DropDownList ID="InstallmentNo" runat="server" Width="153px" AutoPostBack="true" OnSelectedIndexChanged="Installment_Changed">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InstallmentNo"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Installment Amount
                        </td>
                        <td>
                            <input type="text" id="InstallmentAmount" runat="Server" maxlength="10" onkeypress="javascript:onlyDigits(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InstallmentAmount"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="AddButton" Text="Add" OnClick="Add_Click" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td>
                            <a href="ChitUnpaid.aspx?chitNO=<%= ChitNO.Value %>" >Unpaid Members</a>&nbsp;&nbsp;
                            <a href="ChitBidders.aspx?chitNO=<%= ChitNO.Value %>&t=bid">Bidders</a>&nbsp;&nbsp;
                            <a href="ChitBidders.aspx?chitNO=<%= ChitNO.Value %>&t=unbid">UnBidders</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" id="DetailsTable" runat="server">
                    
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <input type="button" value="Close" id="CloseButton" runat="Server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
