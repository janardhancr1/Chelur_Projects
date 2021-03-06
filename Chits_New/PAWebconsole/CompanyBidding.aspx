﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="CompanyBidding.aspx.cs" Inherits="CompanyBidding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="Javascript">
        function openReport() {
            var chitNo = document.getElementById('<%= ChitNO.ClientID %>').value;
            var win = window.open('PrintChitCompBidding.aspx?chitNo=' + chitNo, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
            win.focus();
        }
    </script>
    <div id="content" class="nav_header" runat="server">
        Company Bidding</div>
    <table width="100%">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Chit No
                        </td>
                        <td>
                            <input type="text" id="ChitNO" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chit Name
                        </td>
                        <td>
                            <input type="text" id="ChitName" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Chit Amount
                        </td>
                        <td>
                            <input type="text" id="ChitAmount" runat="server" readonly />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Paid Date
                        </td>
                        <td>
                            <input type="text" id="PaidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PaidDate"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Installment No</td>
                        <td>
                            <asp:DropDownList ID="InstallmentNo" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InstallmentNo"
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Customer"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Amount
                        </td>
                        <td>
                            <input type="text" id="PaidAmount" runat="Server" maxlength="10" onkeypress="javascript:onlyDigits(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PaidAmount"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="AddButton" Text="Add" OnClick="Add_Click" runat="server" />
                            <asp:Label ID="message" runat="server" ForeColor="red"></asp:Label></td>
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
                <table width="100%" id="MembersTable" runat="server">
                    <tr class="nav_header">
                        <th align="left" width="40%">
                            CustomerName
                        </th>
                        <th align="left" width="15%">
                            Installment No
                        </th>
                        <th align="left" width="15%">
                            Bid Date
                        </th>
                        <th align="left" width="15%">
                            Paid Date
                        </th>
                        <th align="right" width="15%">
                            Bid Amount
                        </th>
                    </tr>
                </table>
                <hr />
                <table width="100%" id="CompBidding" runat="server">
                    <tr class="nav_header">
                        <th align="left" width="40%">
                            CustomerName
                        </th>
                        <th align="left" width="15%">
                            Installment No
                        </th>
                        <th align="left" width="15%">
                            Paid Date
                        </th>
                        <th align="right" width="15%">
                            Paid Amount
                        </th>
                        <th width="15%">&nbsp;</th>
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
            <td colspan="2" align="center">
                <input type="button" value="Print" onclick="javascript:openReport();" />
                <input type="button" value="Close" id="CloseButton" onclick="window.location.href='ViewChits.aspx?chitNO=<% =Request.Params["chitNO"] %>';" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
