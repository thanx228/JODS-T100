<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="EditTransferOrder.aspx.vb" Inherits="MIS_T100.EditTransferOrder" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
            width: 86px;
        }
        .style7
        {
            width: 57px;
        }
        .style8
        {
            width: 135px;
        }
        .style15
        {
            width: 106px;
        }
        .style16
        {
            width: 113px;
        }
        .style17
        {
            width: 113px;
            height: 10px;
        }
        .style18
        {
            width: 57px;
            height: 10px;
        }
        .style19
        {
            width: 86px;
            height: 10px;
        }
        .style20
        {
            width: 135px;
            height: 10px;
        }
        .style21
        {
            width: 106px;
            height: 10px;
        }
        .style22
        {
            height: 10px;
        }
        .style23
        {
            width: 113px;
            height: 30px;
        }
        .style24
        {
            width: 57px;
            height: 30px;
        }
        .style25
        {
            width: 86px;
            height: 30px;
        }
        .style26
        {
            width: 135px;
            height: 30px;
        }
        .style27
        {
            width: 106px;
            height: 30px;
        }
        .style28
        {
            height: 30px;
        }
        .auto-style1 {
            width: 141px;
        }
        .auto-style2 {
            width: 113px;
            height: 21px;
        }
        .auto-style3 {
            width: 57px;
            height: 21px;
        }
        .auto-style4 {
            width: 86px;
            height: 21px;
        }
        .auto-style5 {
            width: 135px;
            height: 21px;
        }
        .auto-style6 {
            width: 106px;
            height: 21px;
        }
        .auto-style7 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td align="left" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Large" 
                            ForeColor="Blue" Text="Edit Price Transfer Order"></asp:Label>
                    </td>
                </tr>
            </table>



            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Transfer Order Type :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="DDLType" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ListSearchExtender ID="DDLType_ListSearchExtender" runat="server" 
                            Enabled="True" TargetControlID="DDLType">
                        </asp:ListSearchExtender>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label2" runat="server" Text="Transfer Order No :"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtno" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="background-color: #FFFFFF" colspan="2">
                        <asp:Button ID="BuSearch" runat="server" Text="Search" Width="100px" />
                    </td>
                </tr>
            </table>



            <table class="style4" style="background-color: #FFFFFF; width: 804px;">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label3" runat="server" Text="Transfer No. :"></asp:Label>
                    </td>
                    <td class="auto-style7" colspan="3">
                        <asp:Label ID="Ltype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:Label ID="Label4" runat="server" Text="Seq :"></asp:Label>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Lseq" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style16">
                        <asp:Label ID="Label15" runat="server" Text="Currency"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Lcurrency" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label12" runat="server" Text="Exchange Rate"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Lrate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style15">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style17">
                        <asp:Label ID="Label13" runat="server" Text="Tax Type"></asp:Label>
                    </td>
                    <td class="style18">
                        <asp:Label ID="Ltaxtype" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style19">
                        <asp:Label ID="Label14" runat="server" Text="Tax rate "></asp:Label>
                    </td>
                    <td class="style20">
                        <asp:Label ID="Ltaxrate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style21">
                        &nbsp;</td>
                    <td class="style22">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style16">
                        <asp:Label ID="Label7" runat="server" Text="Item :"></asp:Label>
                    </td>
                    <td class="style7">
                        <asp:Label ID="Litem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:Label ID="Label8" runat="server" Text="Description :"></asp:Label>
                    </td>
                    <td class="style8">
                        <asp:Label ID="Ldesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style15">
                        <asp:Label ID="Label10" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Lspec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style23">
                        <asp:Label ID="Label5" runat="server" Text="Quantity :"></asp:Label>
                    </td>
                    <td class="style24">
                        <asp:Label ID="Lqty" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style25">
                        <asp:Label ID="Label9" runat="server" Text="Price :"></asp:Label>
                    </td>
                    <td class="style26">
                        <asp:TextBox ID="txtprice" runat="server" BorderStyle="Solid"></asp:TextBox>
                    </td>
                    <td class="style27">
                        </td>
                    <td class="style28">
                        <asp:Button ID="BuSave" runat="server" Text="Save" Width="100px" />
                    </td>
                </tr>
            </table>
            <br/>
            <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns >
                    <asp:ButtonField ButtonType="Image" CommandName="OnChange" HeaderText="Change" 
                        ImageUrl="~/Images/edit.gif">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
