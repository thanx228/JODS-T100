<%@ Page Title="SaleOrderDelay" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="saleOrderDelay.aspx.vb" Inherits="MIS_T100.saleOrderDelay" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/Date.ascx" TagPrefix="uc1" TagName="Date" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 109px;
        }
        .auto-style3 {
            width: 35px;
        }
        .auto-style4 {
            width: 177px;
        }
        .auto-style5 {
            width: 29px;
        }
        .auto-style6 {
            width: 1024px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <br />
                <table bgcolor="White" class="auto-style6">
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label6" runat="server" Text="Delivery Date"></asp:Label>
                        </td>
                        <td class="auto-style3">                         
                            <asp:Label ID="lblTitleFrom" runat="server" Text="From"></asp:Label>
                        </td>
                        <td class="auto-style4">
                            <uc1:Date ID="startDate" runat="server" />
                        </td>
                        <td class="auto-style5">
                            <asp:Label ID="lblTitleTo" runat="server" Text="To"></asp:Label>
                        </td>
                        <td>
                         <uc1:Date ID="ToDate" runat="server" />
                        </td>
                    </tr>
                </table><hr />
            </div>
            <table style="width: 71%;" bgcolor="White">
                <tr>
                    <td align="center">
                        <asp:Button ID="btDelayMO" runat="server" Text="Delay MO" />
                    </td>
                    <td align="center">
                        &nbsp;<asp:Button ID="btNotIssueSO" runat="server" Text="Not Issue MO" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbShowText" runat="server"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <asp:Panel ID="Panel1" runat="server" Height="27px">
                <asp:DropDownList ID="ddlCondition" runat="server" Height="23px" Width="110px">
                </asp:DropDownList>
                <asp:Button ID="btDetailDelay" runat="server" Text="Detail Delay MO" />
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Height="21px">
                <asp:Button ID="btNotIssueDetail" runat="server" Text="Detail Not Issue SO" />
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server">
                <table style="width: 69%; height: 34px;">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="มีรายการจำนวน"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="lbCountRow" runat="server" ForeColor="Red"></asp:Label>
                            &nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="    รายการ"></asp:Label>
                            <asp:Button ID="btExcel" runat="server" Text="Excel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:GridView ID="GridView2" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </ContentTemplate>
         <Triggers>
               <asp:PostBackTrigger ControlID ="btExcel" />
               <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
               </Triggers>
    </asp:UpdatePanel>
</asp:Content>
