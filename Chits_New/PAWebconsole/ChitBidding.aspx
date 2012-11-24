<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitBidding.aspx.cs" Inherits="ChitBidding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Chit Bidding</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
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
                    <tr>
                        <td>
                            No of Installment</td>
                        <td>
                            <input type="text" id="NoInstallments" runat="server" readonly /></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Bid Date
                        </td>
                        <td>
                            <input type="text" id="BidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="BidDate"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paid Date
                        </td>
                        <td>
                            <input type="text" id="PaidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PaidDate"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bidder</td>
                        <td>
                            <asp:DropDownList ID="Customer" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Customer"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paid Ammount
                        </td>
                        <td>
                            <input type="text" id="BidAmount" runat="Server" maxlength="10" onkeypress="javascript:onlyDigits(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="BidAmount"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Installment No
                        </td>
                        <td>
                            <asp:DropDownList ID="InstallmentNo" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="InstallmentNo"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="AddButton" Text="Add" OnClick="Add_Click" runat="server" />
                            <asp:Label ID="message" runat="server" ForeColor="red"></asp:Label></td>
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
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" PageSize="25" OnRowDataBound="Gridview_RowBound"
                    DataSourceID="ObjectDataSource1">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No Biddings found.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Paid Date" DataField="PaidDate" SortExpression="PaidDate"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="Bid Date" DataField="BidDate" SortExpression="BidDate"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="Installment No" DataField="InstallmentNO" SortExpression="InstallmentNO" />
                        <asp:BoundField HeaderText="Paid Amount" DataField="PaidAmount" SortExpression="BidAmount"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Left Amount" DataField="LeftAmount" SortExpression="LeftAmount"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                        <asp:BoundField DataField="CustomerAddress" HeaderText="Customer Address" SortExpression="CustomerAddress" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <a onclick="javascript:return confirm('Are you sure to Delete?');" style="color:Red" href="ChitBidding.aspx?chitNO=<%#Eval("ChitNO")%>&transid=<%#Eval("RecordID")%>">Delete</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetChitsBiddingInfos"
                    TypeName="PALibrary.Library.Component.ChitsBiddingManager">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ChitNO" PropertyName="Value" Name="chitNo" Type="string" />
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
            <td colspan="2" align="center">
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
