<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="HundiLoans.aspx.cs" Inherits="HundiLoans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search Hundi Loans</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Hundi Loan No</td>
                        <td>
                            <asp:TextBox ID="HLLoanNo" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date
                        </td>
                        <td>
                            <input type="text" id="LoanDate" runat="server" onfocus="showCalendarControl(this);"
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
                            Pay Mode
                        </td>
                        <td>
                            <asp:DropDownList ID="PayMode" runat="server" Width="153px">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="ChequeRow" runat="server" visible="false">
                        <td>
                            Cheque No</td>
                        <td>
                            <asp:TextBox ID="ChequeNo" runat="server" MaxLength="10"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr id="BankRow" runat="server" visible="false">
                        <td>
                            Bank Name</td>
                        <td>
                            <asp:DropDownList ID="BankName" runat="server" Width="153px">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            CoObligent</td>
                        <td>
                            <asp:TextBox ID="CoObligent" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CoObligent Address</td>
                        <td>
                            <asp:TextBox ID="CoObligentAddress" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Loan Amount
                        </td>
                        <td>
                            <asp:TextBox ID="LoanAmount" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Rate
                        </td>
                        <td>
                            <asp:TextBox ID="Rate" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
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
                <input type="button" value="Add New" onclick="window.location.href='AddHundiLoan.aspx';" />
                <input type="button" value="Print" onclick="window.location.href='PrintHundiLoan.aspx';" />
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
                                    <font color="red" size="2">No records found for your search criteria.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:HyperLinkField DataTextField="HlLoanno" HeaderText="HLoanNo" DataNavigateUrlFields="HlLoanno"
                            DataNavigateUrlFormatString="AddHundiLoan.aspx?recordid={0}" SortExpression="HlLoanno">
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="AccountNo" DataField="AccountNo" SortExpression="AccountNo" />
                        <asp:BoundField HeaderText="CustomerName" DataField="CustomerName" SortExpression="CustomerName" />
                        <asp:BoundField HeaderText="CustomerAddress" DataField="CustomerAddress" SortExpression="CustomerAddress" />
                        <asp:BoundField HeaderText="LoanDate" DataField="LoanDate" SortExpression="LoanDate"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="LoanAmount" DataField="LoanAmount" SortExpression="LoanAmount"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Balance" DataField="Balance" SortExpression="Balance"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="InterestUpto" DataField="InterestPaid" SortExpression="InterestPaid"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="HlLoanno"
                            DataNavigateUrlFormatString="ViewHundiLoan.aspx?HlLoanno={0}" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="PALibrary.Library.Component.HundiLoanManager"
                    SelectMethod="SearchHundiLoanInfo" SelectCountMethod="SearchHundiLoanInfoCount"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:ControlParameter Name="hlLoanno" PropertyName="Text" ControlID="HLLoanNo"></asp:ControlParameter>
                        <asp:ControlParameter Name="customerID" PropertyName="Text" ControlID="CustomerName"
                            Type="Int32"></asp:ControlParameter>
                        <asp:ControlParameter Name="coObligent" PropertyName="Text" ControlID="CoObligent"></asp:ControlParameter>
                        <asp:ControlParameter Name="coobligentAddress" PropertyName="Text" ControlID="CoObligentAddress">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="loanAmount" PropertyName="Text" ControlID="LoanAmount"
                            Type="Decimal"></asp:ControlParameter>
                        <asp:ControlParameter Name="loanDate" PropertyName="Value" ControlID="HiddenDate"
                            Type="DateTime"></asp:ControlParameter>
                        <asp:ControlParameter Name="closed" PropertyName="SelectedValue" ControlID="ClosedType">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="rate" PropertyName="Text" ControlID="Rate" Type="Decimal">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="payMode" PropertyName="SelectedValue" ControlID="PayMode"
                            Type="Int32"></asp:ControlParameter>
                        <asp:ControlParameter Name="chequeNO" PropertyName="Text" ControlID="ChequeNo"></asp:ControlParameter>
                        <asp:ControlParameter Name="bankID" PropertyName="Text" ControlID="BankName" Type="Int32">
                        </asp:ControlParameter>
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
