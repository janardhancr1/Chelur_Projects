<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="FDTrans.aspx.cs" Inherits="FDTrans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function calcualte()
        {
            if(document.getElementById("ctl00_ContentPlaceHolder1_CurrentBalance").value != '0.00')
            {
                var balance = document.getElementById("ctl00_ContentPlaceHolder1_CurrentBalance").value;
                var paidAmount = document.getElementById("ctl00_ContentPlaceHolder1_Amount").value;
                var diff = parseFloat(balance) - parseFloat(paidAmount);
                document.getElementById("ctl00_ContentPlaceHolder1_Balance").value = diff;
            }
            else
            {
                document.getElementById("ctl00_ContentPlaceHolder1_Balance").value = document.getElementById("ctl00_ContentPlaceHolder1_Amount").value;
            }
        }
    </script>

    <div id="content" class="nav_header" runat="server">
        Fixed Deposit Details</div>
    <table class="data_table" width="98%" align="center">
        <tr>
            <td width="50%" valign="top">
                <table width="100%">
                    <tr>
                        <td>
                            FD No</td>
                        <td>
                            <input type="text" id="FDNo" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Date</td>
                        <td>
                            <input type="text" id="FDDate" runat="server" readonly /></td>
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
                            Balance</td>
                        <td>
                            <input type="text" id="CurrentBalance" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Rate</td>
                        <td>
                            <input type="text" id="Rate" runat="server" readonly /></td>
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
                                    <font color="red" size="2">No Records found for your search criteria.</font></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="PaidDate" HeaderText="PaidDate" SortExpression="PaidDate"
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:BoundField HeaderText="ReceiptNo" DataField="ReceiptNo" SortExpression="ReceiptNo" />
                        <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" />
                        <asp:BoundField HeaderText="Balance" DataField="Balance" SortExpression="Balance" />
                        <asp:HyperLinkField Text="Delete" ControlStyle-ForeColor="Red" DataNavigateUrlFields="FDNO,RecordID"
                            DataNavigateUrlFormatString="FDrans.aspx?FDNo={0}&transid={1}" ItemStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SearchFixedTransInfo"
                    SelectCountMethod="SearchFixedTransInfoCount" TypeName="PALibrary.Library.Component.FixedTransManager"
                    EnablePaging="true">
                    <SelectParameters>
                        <asp:Parameter Name="recordID" Type="Int32" />
                        <asp:ControlParameter ControlID="FDNO" Name="fDNO" PropertyName="Value" Type="String" />
                        <asp:Parameter Name="paidDate" Type="DateTime" />
                        <asp:Parameter Name="amount" Type="Decimal" />
                        <asp:Parameter Name="receiptNO" Type="String" />
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
                <input type="button" value="Close" onclick="window.location.href='ViewFixedDeposit.aspx?FDNo=<%= FDNo.Value  %>';" />
                <input type="hidden" id="RecordID" runat="Server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="nav_header">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
