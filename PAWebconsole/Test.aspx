<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Provider Lists</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView HeaderStyle-BackColor="control" HeaderStyle-ForeColor="blue" RowStyle-BackColor="silver"
                runat="server" ID="providerView">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
