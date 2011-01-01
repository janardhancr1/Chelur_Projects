<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Personal Accounting</title>
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <link href="css/CalendarControl.css" rel="stylesheet" type="text/css" />

    <script src="js/CalendarControl.js" language="javascript"></script>

</head>
<body>
    <table id="wrapper" width="850" border="0" align="center" cellpadding="5" cellspacing="0">
        <tr>
            <td colspan="2">
                <div id="header">
                    <h1 id="h1" runat="server">
                        Personal Account
                    </h1>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" height="400px">
                <form id="mainForm" runat="server">
                    <table align="center">
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                                    HeaderText="Please fill all the values" />
                                <br />
                                <asp:Label ID="ErrorMessage" runat="server" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                UserName</td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Password</td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                Date</td>
                            <td>
                                <input type="text" id="CurrentDate" runat="server" onfocus="showCalendarControl(this);"
                                    readonly />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="CurrentDate">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="Login_Click" />
                                <input type="button" id="CloseButton" value="Close" onclick="self.close();" />
                            </td>
                        </tr>
                    </table>
                </form>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="10">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <div id="footer">
                    ©
                    <%= DateTime.Today.Year.ToString() %>
                    Likhitech</div>
            </td>
        </tr>
    </table>
</body>
</html>
