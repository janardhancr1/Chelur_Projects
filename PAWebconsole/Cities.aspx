<%@ Page Language="C#" MasterPageFile="~/PAMaster.master" AutoEventWireup="true"
    CodeFile="Cities.aspx.cs" Inherits="Cities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" class="nav_header">
        Search Cities</div>
    <table width="100%">
        <tr>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            Village Name</td>
                        <td>
                            <asp:TextBox ID="VillageName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            City Name</td>
                        <td>
                            <asp:TextBox ID="CityName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" width="50%">
                <table width="100%">
                    <tr>
                        <td>
                            State Name</td>
                        <td>
                            <asp:DropDownList ID="StateName" runat="Server" Width="153px">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="Karnataka">Karnataka</asp:ListItem>
                                <asp:ListItem Value="Andra Pradesh">Andra Pradesh</asp:ListItem>
                                <asp:ListItem Value="TamilName">Tamil Nadu</asp:ListItem>
                                <asp:ListItem Value="Kerala">Kerala</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Pincode</td>
                        <td>
                            <asp:TextBox ID="Pincode" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SearchButton" Text="Search" runat="server" OnClick="Search_Click" />
                <input type="button" value="Add New" onclick="window.location.href='AddCity.aspx';" />
                <input type="button" value="Close" onclick="window.location.href='HomePage.aspx';" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" width="100%">
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
                        <asp:HyperLinkField DataTextField="VillageName" HeaderText="VillageName" DataNavigateUrlFields="CityID"
                            DataNavigateUrlFormatString="AddCity.aspx?cityid={0}" SortExpression="VillageName">
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="CityName" DataField="CityName" SortExpression="CityName" />
                        <asp:BoundField HeaderText="StateName" DataField="State" SortExpression="State" />
                        <asp:BoundField HeaderText="Pincode" DataField="Pincode" SortExpression="Pincode" />
                        <asp:HyperLinkField Text="View" ControlStyle-ForeColor="Red" DataNavigateUrlFields="CityID"
                            DataNavigateUrlFormatString="ViewCity.aspx?cityid={0}" ItemStyle-Width="50px" />
                    </Columns>
                    <HeaderStyle CssClass="nav_header" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="Beige" />
                    <PagerStyle CssClass="nav_header" />
                    <PagerSettings FirstPageImageUrl="~/images/arrow-left-end.gif" LastPageImageUrl="~/images/arrow-right-end.gif"
                        NextPageImageUrl="~/images/arrow-right.gif" PreviousPageImageUrl="~/images/arrow-left.gif"
                        Mode="NextPreviousFirstLast" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="PALibrary.Library.Component.CityManager"
                    SelectMethod="SearchCityInfo" SelectCountMethod="SearchCityInfoCount" EnablePaging="true">
                    <SelectParameters>
                        <asp:Parameter Name="cityID" DefaultValue="0" Type="Int32" />
                        <asp:ControlParameter Name="villageName" PropertyName="Text" ControlID="VillageName">
                        </asp:ControlParameter>
                        <asp:ControlParameter Name="cityName" PropertyName="Text" ControlID="CityName" Type="String" />
                        <asp:ControlParameter Name="state" PropertyName="SelectedValue" ControlID="StateName"
                            Type="String" />
                        <asp:ControlParameter Name="pincode" PropertyName="Text" ControlID="Pincode" Type="String" />
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
