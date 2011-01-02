<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="HundiTrans.aspx.cs" Inherits="HundiTrans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function calcualte()
        {
            var balance = document.getElementById("ctl00_ContentPlaceHolder1_CurrentBalance").value;
            var paidAmount = document.getElementById("ctl00_ContentPlaceHolder1_Amount").value;
            var diff = parseFloat(balance) - parseFloat(paidAmount);
            document.getElementById("ctl00_ContentPlaceHolder1_Balance").value = diff;
        }
    </script>

    <div id="content" class="nav_header" runat="server">
       Hundi Loan Details</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            HL Loan No</td>
                        <td>
                            <input type="text" id="HLLoanNo" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Customer</td>
                        <td>
                            <input type="text" id="CustomerName" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Account No</td>
                        <td>
                            <input type="text" id="AccountNo" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Loan Amount</td>
                        <td>
                            <input type="text" id="LoanAmount" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Current Balance</td>
                        <td>
                            <input type="text" id="CurrentBalance" runat="server" readonly />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="CurrentBalance"
                                ControlToValidate="Amount" Display="Dynamic" Operator="LessThanEqual" Type="Double">Check the Balance</asp:CompareValidator></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
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
                            Receipt No</td>
                        <td>
                            <input type="text" id="ReceiptNo" runat="server" maxlength="10" onkeypress="javascript:onlyDigits(this);" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ReceiptNo"
                                Display="Dynamic">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Amount
                        </td>
                        <td>
                            <asp:TextBox ID="Amount" runat="Server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Amount"
                                Display="Dynamic">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Balance</td>
                        <td>
                            <input type="text" id="Balance" runat="server" readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Balance"
                                Display="Dynamic">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="SingleParagraph"
                                HeaderText="Please fill all required fields" />
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
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    AllowPaging="True" AllowSorting="false" BorderWidth="0" PageSize="20"
                    OnRowDataBound="Gridview_RowBound" DataSourceID="ObjectDataSource1">
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>
                                    <font color="red" size="2">No Receipts found.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="PaidDate" DataField="PaidDate" SortExpression="PaidDate"
                            HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="ReceiptNo" DataField="ReceiptNo" SortExpression="ReceiptNo" />
                        <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Balance" DataField="Balance" SortExpression="Balance"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:HyperLinkField Text="Delete" ControlStyle-ForeColor="Red" DataNavigateUrlFields="HlLoanno,RecordID"
                            DataNavigateUrlFormatString="HundiTrans.aspx?HlLoanno={0}&transid={1}" ItemStyle-Width="50px"
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
                    SelectMethod="GetHundiTransInfos">
                    <SelectParameters>
                        <asp:ControlParameter Name="hlLoanno" PropertyName="Value" ControlID="HLLoanNo"
                            Type="string"></asp:ControlParameter>
                        <asp:ControlParameter Name="loanAmount" PropertyName="Value" ControlID="LoanAmount"
                            Type="decimal"></asp:ControlParameter>
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
