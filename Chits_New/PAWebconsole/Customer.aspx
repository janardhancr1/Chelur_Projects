<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="Customer.aspx.cs" Inherits="Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search Customers</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Customer Name</td>
                        <td>
                            <asp:TextBox ID="CustomerName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Father/Husband Name</td>
                        <td>
                            <asp:TextBox ID="FatherHusband" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Account No</td>
                        <td>
                            <asp:TextBox ID="AccountNo" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Res Address</td>
                        <td>
                            <asp:TextBox ID="ResAddress" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Res City/Pin</td>
                        <td>
                            <asp:DropDownList ID="ResVillage" runat="server" Width="153px" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Res Phone</td>
                        <td>
                            <asp:TextBox ID="ResPhone" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" Text="Search" runat="server" OnClick="Search_Click" />
                <input type="button" value="Add New" onclick="window.location.href='AddCustomer.aspx';" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2" width="100%">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" Visible="false" PageSize="20"
                    OnRowDataBound="Gridview_RowBound">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No records found for your search criteria.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:HyperLinkField DataTextField="CustomerName" HeaderText="CustomerName" DataNavigateUrlFields="CustomerID"
                            DataNavigateUrlFormatString="AddCustomer.aspx?customerid={0}" SortExpression="CustomerName">
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="Father/Husband" DataField="SonHusband" SortExpression="SonHusband" />
                        <asp:BoundField HeaderText="AccountNo" DataField="AccountNO" SortExpression="AccountNO" />
                        <asp:BoundField HeaderText="ResAddress" DataField="ResAddress" SortExpression="ResAddress" />
                        <asp:BoundField HeaderText="FullAddress" DataField="FullAddress" SortExpression="FullAddress" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="CustomerID"
                            DataNavigateUrlFormatString="ViewCustomer.aspx?customerid={0}" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="PALibrary.Library.Component.CustomerManager"
                    SelectMethod="SearchCustomerInfo" SelectCountMethod="SearchCustomerInfoCount"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:Parameter Name="customerID" DefaultValue="0" Type="Int32" />
                        <asp:ControlParameter Name="customerName" PropertyName="Text" ControlID="CustomerName">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="sonHusband" PropertyName="Text" ControlID="FatherHusband">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="accountNO" PropertyName="Text" ControlID="AccountNo"
                            Type="Int32"></asp:ControlParameter>
                        <asp:ControlParameter Name="resAddress" PropertyName="Text" ControlID="ResAddress"></asp:ControlParameter>
                        <asp:ControlParameter Name="resVillage" PropertyName="SelectedValue" ControlID="ResVillage"
                            Type="Int32"></asp:ControlParameter>
                        <asp:ControlParameter Name="resPhone" PropertyName="Text" ControlID="ResPhone"></asp:ControlParameter>
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
