<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="SearchLedgers.aspx.cs" Inherits="SearchLedgers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Ledgers</div>
    <table width="100%" class="data_table">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            LedgerName</td>
                        <td>
                            <asp:TextBox ID="LedgerName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            BalanceType</td>
                        <td>
                            <asp:DropDownList ID="BalanceType" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                <asp:ListItem Value="Dr">Dr</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Group</td>
                        <td>
                            <asp:DropDownList ID="GroupID" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="SearchButton" runat="server" OnClick="Search_Click" Text="Search"></asp:Button>
                            <input type="button" value="Add New" onclick="window.location.href='AddLedgers.aspx';" />
                            <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" />
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" Visible="false" PageSize="20"
                    OnRowDataBound="Gridview_RowBound">
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No Records found for your search criteria.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:HyperLinkField DataTextField="LedgerID" HeaderText="Ledger ID" DataNavigateUrlFields="LedgerID"
                            DataNavigateUrlFormatString="AddLedgers.aspx?ledgerID={0}" SortExpression="LedgerID" />
                        <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" SortExpression="LedgerName" />
                        <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" SortExpression="OpeningBalance"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="BalanceType" HeaderText="Balance Type" SortExpression="BalanceType" />
                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName" />
                    </Columns>
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchLedgersInfo"
                    SelectCountMethod="SearchLedgersInfoCount" TypeName="PALibrary.Library.Component.LedgersManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="LedgerName" Name="ledgerName" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="BalanceType" Name="balanceType" PropertyName="SelectedValue"
                            Type="String" />
                        <asp:ControlParameter ControlID="GroupID" Name="groupID" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:Parameter Name="startRowIndex" Type="Int32" />
                        <asp:Parameter Name="maximumRows" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <i>You are viewing page
                    <%= GridView1.PageCount == 0 ?  GridView1.PageIndex :GridView1.PageIndex  + 1 %>
                    of
                    <%= GridView1.PageCount %>
                </i>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
