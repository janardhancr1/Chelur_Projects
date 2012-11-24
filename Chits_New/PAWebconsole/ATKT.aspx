<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ATKT.aspx.cs" Inherits="ATKT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search ATKT</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            ATKTNO</td>
                        <td>
                            <asp:TextBox ID="ATKTNO" runat="server" MaxLength="7"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            PartyName</td>
                        <td>
                            <asp:TextBox ID="PartyName" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            ATKTDate</td>
                        <td>
                            <input type="text" id="ATKTDate" runat="server" onfocus="showCalendarControl(this);"
                                readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            TranType</td>
                        <td>
                            <asp:DropDownList ID="TranType" runat="server" Width="153px">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Amount</td>
                        <td>
                            <asp:TextBox ID="Amount" runat="server" MaxLength="10"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Remarks</td>
                        <td>
                            <asp:TextBox ID="Remarks" runat="server" MaxLength="255"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Closed</td>
                        <td>
                            <asp:DropDownList ID="Closed" runat="server" Width="153px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ClosedDate</td>
                        <td>
                            <input type="text" id="ClosedDate" runat="server" onfocus="showCalendarControl(this);"
                                readonly />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" runat="server" OnClick="Search_Click" Text="Search"></asp:Button>
                <input type="button" value="Add New" onclick="window.location.href='AddATKT.aspx';" />
                <input type="button" value="Print" onclick="window.location.href='PrintATKT.aspx';" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" Visible="false" PageSize="25"
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
                        <asp:HyperLinkField DataTextField="ATKTNO" HeaderText="ATKTNO" DataNavigateUrlFields="RecordID"
                            DataNavigateUrlFormatString="AddATKT.aspx?recordID={0}" SortExpression="ATKTNO" />
                        <asp:BoundField DataField="PartyName" HeaderText="PartyName" SortExpression="PartyName" />
                        <asp:BoundField DataField="ATKTDate" HeaderText="ATKTDate" SortExpression="ATKTDate"
                            DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="TranType" HeaderText="TranType" SortExpression="TranType" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                        <asp:BoundField DataField="ClosedType" HeaderText="Closed" SortExpression="ClosedType" />
                        <asp:BoundField DataField="ClosedDate" HeaderText="ClosedDate" SortExpression="ClosedDate"
                            DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="RecordID"
                            DataNavigateUrlFormatString="ViewATKT.aspx?recordID={0}" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchATKTInfo"
                    SelectCountMethod="SearchATKTInfoCount" TypeName="PALibrary.Library.Component.ATKTManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ATKTNO" Name="aTKTNO" PropertyName="Text" Type="Int32" />
                        <asp:ControlParameter ControlID="PartyName" Name="partyName" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="ATKTDate" Name="aTKTDate" PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="TranType" Name="tranType" PropertyName="SelectedValue"
                            Type="String" />
                        <asp:ControlParameter ControlID="Amount" Name="amount" PropertyName="Text" Type="Decimal" />
                        <asp:ControlParameter ControlID="Remarks" Name="remarks" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="Closed" Name="closed" PropertyName="SelectedValue"
                            Type="String" />
                        <asp:ControlParameter ControlID="ClosedDate" Name="closedDate" PropertyName="Value"
                            Type="DateTime" />
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
