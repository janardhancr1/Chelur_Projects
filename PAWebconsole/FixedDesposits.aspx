<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="FixedDesposits.aspx.cs" Inherits="FixedDesposits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search Fixed Desposits</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td width="50%">
                            FD No</td>
                        <td width="50%">
                            <asp:TextBox ID="FDNo" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date
                        </td>
                        <td>
                            <input type="text" id="FDDate" runat="server" onfocus="showCalendarControl(this);"
                                readonly />
                            <input type="hidden" id="HiddenDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer Name</td>
                        <td>
                            <asp:DropDownList ID="CustomerName" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nominee Name</td>
                        <td>
                            <asp:TextBox ID="NomineeName" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Nominee Relationship:
                        </td>
                        <td>
                            <asp:TextBox ID="NomineeRelation" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            FD Amount
                        </td>
                        <td>
                            <asp:TextBox ID="FDAmount" runat="server" MaxLength="10"></asp:TextBox>
                    </tr>
                    <tr>
                        <td>
                            Rate
                        </td>
                        <td>
                            <asp:TextBox ID="Rate" runat="server" MaxLength="6"></asp:TextBox>
                    </tr>
                    <tr>
                        <td>
                            Closed
                        </td>
                        <td>
                            <asp:DropDownList ID="ClosedType" runat="server" Width="153px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" Text="Search" runat="server" OnClick="Search_Click" />
                <input type="button" value="Add New" onclick="window.location.href='AddFixedDesposit.aspx';" />
                <input type="button" value="Print" onclick="window.location.href='PrintFixedDesposit.aspx';" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" />
            </td>
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
                        <asp:HyperLinkField DataTextField="FDNO" HeaderText="FDNO" DataNavigateUrlFields="FDNO"
                            DataNavigateUrlFormatString="AddFixedDesposit.aspx?recordID={0}" SortExpression="FDNO" />
                        <asp:BoundField HeaderText="AccountNo" DataField="AccountNo" SortExpression="AccountNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="CustomerName" DataField="CustomerName" SortExpression="CustomerName" />
                        <asp:BoundField HeaderText="CustomerAddress" DataField="CustomerAddress" SortExpression="CustomerAddress" />
                        <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField DataField="Balance" HeaderText="Balance" SortExpression="Balance" ItemStyle-HorizontalAlign="Right" />
                         <asp:BoundField HeaderText="InterestUpto" DataField="InterestPaid" SortExpression="InterestPaid"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="FDNO"
                            DataNavigateUrlFormatString="ViewFixedDeposit.aspx?FDNo={0}" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchFixedDepositInfo"
                    SelectCountMethod="SearchFixedDepositInfoCount" TypeName="PALibrary.Library.Component.FixedDepositManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="FDNO" Name="fDNO" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="CustomerName" Name="customerID" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="FDDate" Name="startDate" PropertyName="Value" Type="DateTime" />
                        <asp:ControlParameter ControlID="NomineeName" Name="nomineeName" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="NomineeRelation" Name="relationship" PropertyName="Text"
                            Type="String" />
                        <asp:ControlParameter ControlID="FDAmount" Name="amount" PropertyName="Text" Type="Decimal" />
                        <asp:ControlParameter ControlID="Rate" Name="rate" PropertyName="Text" Type="Decimal" />
                        <asp:ControlParameter ControlID="ClosedType" Name="closed" PropertyName="SelectedValue" Type="String" />
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
