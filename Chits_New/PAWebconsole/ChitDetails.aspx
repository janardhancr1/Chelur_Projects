<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="ChitDetails.aspx.cs" Inherits="ChitDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header" runat="server">
        Chit Details</div>
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
                            Chit Commission</td>
                        <td>
                            <input type="text" id="ChitCommission" runat="server" readonly /></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            Paid Date
                        </td>
                        <td>
                            <input type="text" id="PaidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PaidDate"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Customer
                        </td>
                        <td>
                            <asp:DropDownList ID="Customer" runat="server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Customer"
                                Display="Dynamic">Select customer</asp:RequiredFieldValidator>
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
                            Installment Amount
                        </td>
                        <td>
                            <input type="text" id="InstallmentAmount" runat="Server" maxlength="10" onkeypress="javascript:onlyDigits(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="InstallmentAmount"
                                Display="Dynamic">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="AddButton" Text="Add" OnClick="Add_Click" runat="server" /></td>
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
                <table width="100%">
                    <tr>
                        <td>
                            Participator :
                            <asp:DropDownList ID="Customer_ID" runat="server" Width="153px" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Installment No :
                            <asp:DropDownList ID="SelectInstallment" runat="server" Width="153px" AutoPostBack="true">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <a href="ChitUnpaid.aspx?chitNO=<%= ChitNO.Value %>" >Unpaid Members</a>&nbsp;&nbsp;
                            <a href="ChitBidders.aspx?chitNO=<%= ChitNO.Value %>&t=bid">Bidders</a>&nbsp;&nbsp;
                            <a href="ChitBidders.aspx?chitNO=<%= ChitNO.Value %>&t=unbid">UnBidders</a>
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
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" PageSize="25" OnRowDataBound="Gridview_RowBound"
                    DataSourceID="ObjectDataSource1">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No Transactions found.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="Paid Date" DataField="Date" SortExpression="Date" HtmlEncode="false"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="Installment No" DataField="InstallmentNO" SortExpression="InstallmentNO" />
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" SortExpression="CustomerName" />
                        <asp:BoundField DataField="CustomerAddress" HeaderText="Customer Address" SortExpression="CustomerAddress" />
                        <asp:HyperLinkField Text="Delete" ControlStyle-ForeColor="Red" DataNavigateUrlFields="ChitNO,RecordID"
                            DataNavigateUrlFormatString="ChitDetails.aspx?chitNO={0}&transid={1}" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchChitsTransInfo"
                    SelectCountMethod="SearchChitsTransInfoCount" TypeName="PALibrary.Library.Component.ChitsTransManager" EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ChitNO" Name="chitNO" PropertyName="Value" Type="String" />
                        <asp:ControlParameter ControlID="Customer_ID" Name="customerID" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:ControlParameter ControlID="SelectInstallment" Name="installmentNO" PropertyName="SelectedValue"
                            Type="Int32" />
                        <asp:Parameter Name="installmentAmount" Type="decimal" DefaultValue="0"/>
                        <asp:Parameter Name="date" Type="DateTime" DefaultValue="01/01/1900" />
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
