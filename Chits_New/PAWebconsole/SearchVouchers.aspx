<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="SearchVouchers.aspx.cs" Inherits="SearchVouchers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Vouchers</div>
    <table width="100%" class="data_table">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            VoucherDate</td>
                        <td>
                            <input type="text" id="VoucherDate" runat="server" readonly onfocus="showCalendarControl(this);" />
                            <input type="hidden" id="HiddenDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            VoucherType</td>
                        <td>
                            <asp:DropDownList ID="VoucherType" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            VoucherNO</td>
                        <td>
                            <asp:TextBox ID="VoucherNO" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            FromLedger</td>
                        <td>
                            <asp:DropDownList ID="FromLedger" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            ToLedger</td>
                        <td>
                            <asp:DropDownList ID="ToLedger" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            Narration</td>
                        <td>
                            <asp:TextBox ID="Narration" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" runat="server" OnClick="Search_Click" Text="Search"></asp:Button>
                <input type="button" value="Add New" onclick="window.location.href='AddVouchers.aspx';" />
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
                        <asp:HyperLinkField DataTextField="VoucherID" HeaderText="Voucher ID" DataNavigateUrlFields="VoucherID"
                            DataNavigateUrlFormatString="AddVouchers.aspx?voucherID={0}" SortExpression="VoucherID" />
                        <asp:BoundField DataField="VoucherDate" HeaderText="Voucher Date" SortExpression="VoucherDate"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="VoucherTypeName" HeaderText="Voucher Type" SortExpression="VoucherTypeName" />
                        <asp:BoundField DataField="VoucherNO" HeaderText="Voucher NO" SortExpression="VoucherNO" />
                        <asp:BoundField DataField="FromLedgerName" HeaderText="From Ledger" SortExpression="FromLedgerName" />
                        <asp:BoundField DataField="ToLedgerName" HeaderText="To Ledger" SortExpression="ToLedgerName" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Right"
                            DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="Narration" HeaderText="Narration" SortExpression="Narration" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchVouchersInfo"
                    SelectCountMethod="SearchVouchersInfoCount" TypeName="PALibrary.Library.Component.VouchersManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HiddenDate" Name="voucherDate" PropertyName="Value"
                            Type="DateTime" />
                        <asp:ControlParameter ControlID="VoucherType" Name="voucherType" PropertyName="Text"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="VoucherNO" Name="voucherNO" PropertyName="Text"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="FromLedger" Name="fromLedger" PropertyName="Text"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="ToLedger" Name="toLedger" PropertyName="Text" Type="Int32" />
                        <asp:ControlParameter ControlID="Narration" Name="narration" PropertyName="Text"
                            Type="String" />
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
