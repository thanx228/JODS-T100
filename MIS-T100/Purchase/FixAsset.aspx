<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FixAsset.aspx.vb" Inherits="MIS_T100.FixAsset" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 520px;
        }
        .style5
        {
        }
        .style6
        {
            width: 252px;
        }
        .style8
        {
            width: 795px;
        }
        .style1
    {
        width: 282px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 <uc3:HeaderForm ID="HeaderForm1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label11" runat="server" Text="Asset Po Type :"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label2" runat="server" Text="Asset Po No :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="Po" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        Buyer :</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbBuyer" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Supp ID :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbSup" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label12" runat="server" Text="Condition :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="DDLCondition" runat="server">
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem Selected="True">Incomplete</asp:ListItem>
                            <asp:ListItem>Complete</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Asset :"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="Asset" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="Asset Spec :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="Spec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="TO_CHAR(pmdldocdt,'YYYYMMDD')">Doc Date</asp:ListItem>
                            <asp:ListItem Value="TO_CHAR(pmdn012,'YYYYMMDD')">Due Date</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label17" runat="server" Text="From"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="FDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="FDate_CalendarExtender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="FDate">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label14" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="TDate" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="TDate_CalendarExtender" runat="server" Enabled="True" 
                            Format="dd/MM/yyyy" TargetControlID="TDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%; background-image: url('../Images/btt.jpg');">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('http://localhost:50381/Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style8">
                        <asp:Button ID="BuSearch" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            Amount of rows <asp:Label ID="lbrowsCount" runat="server" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="POAsset" HeaderText="PO Asset" />
                    <asp:BoundField DataField="DocDate" HeaderText="Doc Date" />
                    <asp:BoundField DataField="PlanDelDate" HeaderText="Plan Del Date" />
                    <asp:BoundField DataField="Days" HeaderText="Days" />
                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                    <asp:BoundField DataField="Currency" HeaderText="Currency" />
                    <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />
                    <asp:BoundField DataField="AssetSpec" HeaderText="Asset Spec" />
                    <asp:BoundField DataField="Unit" HeaderText="Unit" />
                    <asp:BoundField DataField="Qty" DataFormatString="{0:N}" HeaderText="Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DelQty" DataFormatString="{0:N}" 
                        HeaderText="Del Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DelBal" DataFormatString="{0:N}" HeaderText="Del Bal">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRDEPT" HeaderText="P/R Dept." />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="Buyer" HeaderText="Buyer" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" Wrap="False" />
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
            <asp:PostBackTrigger  ControlID="btExport"  />
        </Triggers>
    </asp:UpdatePanel>
 
</asp:Content>
