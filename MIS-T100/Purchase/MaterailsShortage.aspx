<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterailsShortage.aspx.vb" Inherits="MIS_T100.MaterailsShortage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollgvShow();

        });
        function gridviewScrollgvShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: 900,
                height: 200,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 2,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 8
            });
        }
        </script>
    <style type="text/css">
     .fontChar {
            /*background-color: gainsboro;*/
            color: black ;
            font-family:  monospace;
            font-size: 14px;
            /*font-weight: bold;*/
        }
        .auto-style1 {
            width: 342px;
        }
        .auto-style2 {
            width: 82px;
        }
        .auto-style3 {
            width: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width:75%;" bgcolor="White">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label3" runat="server" Text="Code Type" CssClass ="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style1"><asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" 
                            RepeatDirection="Horizontal" style="margin-left: 0px; margin-right: 0px" 
                            Width="318px" CssClass ="fontChar">
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi FG</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                            <asp:ListItem Value="5">Suply Product</asp:ListItem>
                            <asp:ListItem Value="6">Suply Other</asp:ListItem>
                        </asp:CheckBoxList></td>
                    
                    <td class="auto-style3"><asp:Label ID="Label1" runat="server" Text="Condition" CssClass ="fontChar"></asp:Label></td>
                    <td><asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Shortage</asp:ListItem>
                            <asp:ListItem Value="2">Call In</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style2"><asp:Label ID="Label2" runat="server" Text="Code" CssClass ="fontChar"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="tbCode" runat="server" CssClass ="fontChar"></asp:TextBox>
                    </td>
                    <td class="auto-style3"><asp:Label ID="Label5" runat="server" Text="Spec" CssClass ="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbSpec" runat="server" CssClass ="fontChar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2"><asp:Label ID="Label4" runat="server" Text="From Date" CssClass ="fontChar"></asp:Label></td>
                    <td class="auto-style1">
                        <asp:TextBox ID="tbDateFrom" runat="server" CssClass ="fontChar"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td class="auto-style3"><asp:Label ID="Label6" runat="server" Text="To Date" CssClass ="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbDateTo" runat="server" CssClass ="fontChar"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd"  TargetControlID="tbDateTo">
                     </asp:CalendarExtender>
                       
                    </td>
                </tr>
            </table>
 <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btPlanCallIn" runat="server" Text="Plan Call In" />
                        &nbsp;<asp:Button ID="btPOCallIn" runat="server" Text="PO Call In" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Report" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server"  BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="False" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" Wrap="False" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
            <asp:GridView ID="gvPlanCallIn" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="False" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" Wrap="False" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
            <asp:GridView ID="gvPoCallIn" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="False" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" Wrap="False" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
