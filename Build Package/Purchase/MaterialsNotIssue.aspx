<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsNotIssue.aspx.vb" Inherits="MIS_T100.MaterialsNotIssue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

    .style1
    {
        width: 282px;
    }
        .auto-style1 {
            width: 125px;
        }
        .auto-style2 {
            width: 169px;
        }
        .auto-style3 {
            width: 122px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="Materials Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style2">
                        <asp:DropDownList ID="ddlCodeType" runat="server">
                            <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        &nbsp;</td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="auto-style1">
                        <asp:Label ID="Label4" runat="server" Text="MO type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF; vertical-align: top;" colspan="3">
                        <asp:CheckBoxList ID="clWorkType" runat="server" ForeColor="Blue">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label9" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style2">
                        <asp:TextBox ID="tbWorkNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label7" runat="server" Text="Codition"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Not Issue</asp:ListItem>
                            <asp:ListItem Value="2">Issue &lt; Require</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label5" runat="server" Text="MatItem"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style2">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label10" runat="server" Text="MatSpec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label6" runat="server" Text="Plan Start Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style2">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Plan Start Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label1" runat="server" Text="Label">Amount Of Rows</asp:Label>&nbsp;&nbsp;<asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

