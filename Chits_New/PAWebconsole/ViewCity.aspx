<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ViewCity.aspx.cs" Inherits="ViewCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        View Village</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="25%">
                Village Name</td>
            <td width="25%">
                <b>
                    <asp:Label ID="VillageName" runat="server"></asp:Label></b>
            </td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                City Name</td>
            <td>
                <b>
                    <asp:Label ID="CityName" runat="server"></asp:Label></b>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                State Name</td>
            <td>
                <b>
                    <asp:Label ID="StateName" runat="Server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                Pincode</td>
            <td>
                <b>
                    <asp:Label ID="Pincode" runat="server"></asp:Label></b>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Button ID="DeleteButton" Text="Delete" runat="server" OnClick="Delete_Click" />
                <input type="button" value="Close" onclick="window.location.href='Cities.aspx';" />
                <input type="hidden" id="CityID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="nav_header" colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
