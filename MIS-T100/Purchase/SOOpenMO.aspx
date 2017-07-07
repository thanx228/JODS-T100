<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SOOpenMO.aspx.vb" Inherits="MIS_T100.SOOpenMO" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register src="../UserControl/docTypeC.ascx" tagname="docTypeC" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
            width: 141px;
        }
        .style8
        {
            height: 21px;
            width: 141px;
        }
        .style9
        {
        }
        .style10
        {
            height: 21px;
            }
        .style1
    {
        width: 282px;
    }
        .auto-style1 {
            width: 94px;
        }
        .auto-style2 {
            height: 21px;
            width: 94px;
        }
        .auto-style3 {
            width: 218px;
        }
        .auto-style4 {
            height: 21px;
            width: 218px;
        }
        .auto-style5 {
            width: 70px;
        }
        .auto-style6 {
            height: 21px;
            width: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td class="auto-style1" style="background-color: #FFFFFF">
                        Item (FG)</td>
                    <td class="auto-style3" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbFGItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        Item (Pur)</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbPurItem" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" style="background-color: #FFFFFF">
                        Spec (FG)</td>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbFGSpec" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        Spec (Pur)</td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbPurSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" style="background-color: #FFFFFF">
                        SO Type</td>
                    <td class="style10" colspan="3" style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblSOType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" style="background-color: #FFFFFF">
                        SO NO.</td>
                    <td class="auto-style4" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSoNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style6" style="background-color: #FFFFFF">
                        SO Seq</td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSoSeq" runat="server" Width="78px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1" style="background-color: #FFFFFF">
                        MO Type</td>
                    <td class="style9" style="background-color: #FFFFFF" colspan="3">
                        <asp:CheckBoxList ID="cblMOType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style2">
                        MO No.</td>
                    <td style="background-color: #FFFFFF" class="auto-style4">
                        <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                    </td>
                    <td style="background-color: #FFFFFF" class="style6">
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        MO From</td>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                       <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        To</td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Height="30px" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Report" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <br />
            <asp:GridView ID="gvShow" runat="server" CellPadding="4" ForeColor="#333333" >
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

