<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="Chits.aspx.cs" Inherits="Chits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search Chits</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td width="50%">
                            Chit No</td>
                        <td width="50%">
                            <asp:TextBox ID="ChitNO" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Chit Name</td>
                        <td>
                            <asp:TextBox ID="ChitName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Chit Amount</td>
                        <td>
                            <asp:TextBox ID="ChitAmount" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Bid Date</td>
                        <td>
                            <asp:DropDownList ID="BidDate" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                    </tr>
                </table>
            </td>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Installment Amount</td>
                        <td>
                            <asp:TextBox ID="InstallmentAmount" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            No. Installments</td>
                        <td>
                            <asp:TextBox ID="NoInstallments" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Closed</td>
                        <td>
                            <asp:DropDownList ID="ClosedType" runat="server" Width="153px">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" runat="server" OnClick="Search_Click" Text="Search"></asp:Button>
                <input type="button" value="Add New" onclick="window.location.href='AddChits.aspx';" />
                <input type="button" value="Print" onclick="window.location.href='PrintChits.aspx';" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td valign="top" width="100%" colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" Visible="false" PageSize="20"
                    OnRowDataBound="Gridview_RowBound">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No Records found for your search criteria.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:HyperLinkField DataTextField="ChitNO" HeaderText="ChitNO" DataNavigateUrlFields="ChitNO"
                            DataNavigateUrlFormatString="AddChits.aspx?chitNO={0}" SortExpression="ChitNO" />
                        <asp:BoundField DataField="ChitName" HeaderText="ChitName" SortExpression="ChitName" />
                        <asp:BoundField DataField="ChitAmount" HeaderText="ChitAmount" SortExpression="ChitAmount" />
                        <asp:BoundField DataField="BidDate" HeaderText="BidDate" SortExpression="BidDate" />
                        <asp:BoundField DataField="InstallmentAmount" HeaderText="InstallmentAmount" SortExpression="InstallmentAmount" />
                        <asp:BoundField DataField="NoInstallments" HeaderText="NoInstallments" SortExpression="NoInstallments" />
                        <asp:BoundField DataField="ClosedType" HeaderText="Closed" SortExpression="Closed" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="ChitNO"
                            DataNavigateUrlFormatString="ViewChits.aspx?chitNO={0}" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchChitsInfo"
                    SelectCountMethod="SearchChitsInfoCount" TypeName="PALibrary.Library.Component.ChitsManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ChitNO" Name="chitNO" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="ChitName" Name="chitName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="ChitAmount" Name="chitAmount" PropertyName="Text"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="BidDate" Name="bidDate" PropertyName="SelectedValue"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="InstallmentAmount" Name="installmentAmount" PropertyName="Text"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="NoInstallments" Name="noInstallments" PropertyName="Text"
                            Type="Decimal" />
                        <asp:ControlParameter ControlID="ClosedType" Name="closed" PropertyName="Text" Type="String" />
                        <asp:Parameter Name="startRowIndex" Type="Int32" />
                        <asp:Parameter Name="maximumRows" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
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
