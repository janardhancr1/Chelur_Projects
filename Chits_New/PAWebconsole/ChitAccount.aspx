<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitAccount.aspx.cs" Inherits="ChitAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="Javascript">
       function openReport()
       {
            var chitNo = document.getElementById('<%= ChitNO.ClientID %>').value;
            var customerID = '<%= Request.Params["customerID"] %>';
            var win = window.open('PrintChitAccount.aspx?chitNo=' + chitNo + "&customerID=" + customerID, 'RepoWind', 'top=100,left=250,height=600,width=600,status=yes,resizable=yes');
            win.focus();
        }
    </script>
    <div id="content" class="nav_header" runat="server">
        Chit Member Account</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td>
                <table>
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
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            Customer Name</td>
                        <td>
                            <input type="text" id="CustomerName" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Customer Address</td>
                        <td>
                            <textarea id="CustomerAddress" runat="server" cols="30" readonly></textarea></td>
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
                        <th align="left" width="15%">
                            Installment No</th>
                        <th align="left" width="15%">
                            Installment Amount</th>
                        <th align="left" width="15%">
                            Discount Amount</th>
                        <th align="left" width="15%">
                            Paid Date</th>
                        <th align="left" width="20%">
                            Bid Amount</th>
                        <th align="left" width="20%">
                            Bid Date</th>
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
