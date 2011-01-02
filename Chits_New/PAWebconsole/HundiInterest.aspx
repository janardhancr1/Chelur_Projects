<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="HundiInterest.aspx.cs" Inherits="HundiInterest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript">
        function selectAll(ele, theElement)
        {
            if(theElement.length == null)
		    {
			    if(ele.checked)
				    theElement.checked=true;
			    else
				    theElement.checked=false;
		    }
		    else
		    {
			    for(i=0; i<theElement.length; i++)
			    {
				    if(ele.checked)
					    theElement[i].checked=true;
				    else
					    theElement[i].checked=false;
			    }
		    }
		    selectIDs();
        }
        
        function selectIDs()
        {
            document.getElementById('ctl00_ContentPlaceHolder1_selectdIDs').value="0";
            var checkboxList = document.getElementsByTagName('input'); 
            var ch=0;
            for(var i=0;i<checkboxList.length;i++)
            {
                var element = checkboxList[i];
                if(element.type == 'checkbox')
                {
                    if(element.checked == true && element.getAttribute('transid') != null)
                    {
                        document.getElementById('ctl00_ContentPlaceHolder1_selectdIDs').value = document.getElementById('ctl00_ContentPlaceHolder1_selectdIDs').value+","+element.getAttribute('transid');
                        ch=1;
                    }
                }
            }
            if(ch==0)
                document.getElementById('SelectAll').checked = false;
            //alert(document.getElementById('ctl00_ContentPlaceHolder1_selectdIDs').value);
        }
        
        function checkSelected(theForm)
        {   
            dTable = document.getElementById("ctl00_ContentPlaceHolder1_InterestTable");
            ch = false;
            if(dTable.rows.length > 0)
            {
                var checkboxList = document.getElementsByTagName('input'); 
                
                for(var i=0;i<checkboxList.length;i++)
                {
                    var element = checkboxList[i];
                    if(element.type == 'checkbox')
                    {
                        if(element.checked == true && element.getAttribute('transid') != null)
                        {
                            ch = true;
                        }
                    }
                }
		    }
   		    if(ch==false)
		    {
		        alert("Please select the vouchers");
		        return false;
		    }
		    else
		    {
                document.getElementById('ctl00_ContentPlaceHolder1_PaidDate').value = "-";
                document.getElementById('ctl00_ContentPlaceHolder1_ReceiptNo').value = "-";
                document.getElementById('ctl00_ContentPlaceHolder1_InterestAmount').value = "0";
                document.getElementById('ctl00_ContentPlaceHolder1_PaidUpto').value = "-";
		        var ans;
		        ans=confirm("Are you sure you want to delete selected values?");
		        if (!ans)
		            return false;
		    }
		    return true;
        }
        
        function calculate()
        {
            var lastDays = document.getElementById('ctl00_ContentPlaceHolder1_LastPaidDate').value;
            var currentDays = document.getElementById('ctl00_ContentPlaceHolder1_PaidUpto').value;
            
            if(currentDays == "")
            {
                alert('Please select the date');
            }
            else
            {
                var lastDay = lastDays.split('/');
                var currentDay = currentDays.split('/')
            
                var d1 = new Date(lastDay[2],lastDay[1],lastDay[0]);
                var d2 = new Date(currentDay[2],currentDay[1],currentDay[0]);
                
                var one_day=1000*60*60*24
                var days = Math.ceil((d2.getTime()-d1.getTime())/(one_day));
                var rate = document.getElementById('ctl00_ContentPlaceHolder1_Rate').value
                var balance = document.getElementById('ctl00_ContentPlaceHolder1_CurrentBalance').value
                
                document.getElementById('ctl00_ContentPlaceHolder1_InterestAmount').value = Math.round(balance * rate * days / 36500);
                
            }
        }
    </script>

    <div id="Div1" class="nav_header" runat="server">
        Interest Details</div>
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
                            Current Balance</td>
                        <td>
                            <input type="text" id="CurrentBalance" runat="server" readonly /></td>
                    </tr>
                    <tr>
                        <td>
                            Loan Amount</td>
                        <td>
                            <input type="text" id="LoanAmount" runat="server" readonly /></td>
                    </tr>
                </table>
            </td>
            <td width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Paid Upto</td>
                        <td>
                            <input type="text" id="PaidUpto" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                ControlToValidate="PaidUpto">*</asp:RequiredFieldValidator>
                            &nbsp;<a href="#" onclick="calculate();">Calculate</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Paid Date</td>
                        <td>
                            <input type="text" id="PaidDate" runat="Server" onfocus="showCalendarControl(this);"
                                readonly />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                ControlToValidate="PaidDate">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Receipt No</td>
                        <td>
                            <asp:TextBox ID="ReceiptNo" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                ControlToValidate="ReceiptNo">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Interest Amount</td>
                        <td>
                            <asp:TextBox ID="InterestAmount" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                ControlToValidate="InterestAmount">*</asp:RequiredFieldValidator>
                        </td>
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
                            <asp:Button ID="AddButton" Text="Add" runat="server" OnClick="Add_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
    </table>
    <table width="98%" align="center">
        <tr>
            <td>
                <b>Interest Details</b></td>
            <td>
                <b>Interest Collected Details</b></td>
            <td align="right">
                <a href="#" runat="server" id="A1" onserverclick="DeleteClick" onclick="javascript:return checkSelected(this.form);">
                    Delete</a>
                <input type="hidden" runat="server" id="selectdIDs" /></td>
        </tr>
        <tr class="nav_header">
            <td width="50%">
                <div>
                    <table width="96%">
                        <tr>
                            <td width="20%">
                                FromDate</td>
                            <td width="20%">
                                ToDate</td>
                            <td width="20%" align="right">
                                Amount</td>
                            <td width="20%" align="right">
                                Days</td>
                            <td width="20%" align="right">
                                Interest</td>
                        </tr>
                    </table>
                </div>
                <div style="height: 150px; overflow-y: scroll; background-color: White;">
                    <table id="DetailsTable" runat="server" width="96%">
                    </table>
                </div>
            </td>
            <td width="50%" colspan="2">
                <div>
                    <table width="96%">
                        <tr>
                            <td width="20%">
                                PaidDate</td>
                            <td width="20%">
                                PaidUpto</td>
                            <td width="20%">
                                ReceiptNo</td>
                            <td width="20%" align="right">
                                Amount</td>
                            <td width="15%" align="center">
                                <input type="checkbox" id="SelectAll" onclick="javascript:selectAll(this, this.form.dcheck);" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="height: 150px; overflow-y: scroll; background-color: White;">
                    <table id="InterestTable" runat="server" width="96%">
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <table width="100%">
                    <tr>
                        <td width="25%">
                            Total Interest:</td>
                        <td width="25%">
                            <input type="text" id="TotalInterest" runat="Server" readonly /></td>
                        <td width="25%">
                            Interest Collected:</td>
                        <td width="25%">
                            <input type="text" id="InterestCollected" runat="Server" readonly /></td>
                        <%--<td>
                            Balance Interest :</td>
                        <td>
                            <input type="text" id="BalanceInterest" runat="Server" readonly />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="BalanceInterest"
                                ControlToValidate="InterestAmount" Display="Dynamic" Operator="LessThanEqual"
                                Type="Double" Enabled="false">Check the balance Interest</asp:CompareValidator>
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <input type="button" value="Close" id="CloseButton" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" class="nav_header">
                <input type="hidden" id="LastPaidDate" runat="server" />
                <input type="hidden" id="Rate" runat="server" />
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
