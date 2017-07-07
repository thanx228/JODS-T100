<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterailsCallIn.aspx.vb" Inherits="MIS_T100.MaterailsCallIn" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
             <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
   <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" >
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
           
        });

          function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 40,
                height: 850,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 0,
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
        .style3
        {
            width: 40px;
        }
        .style4
        {
            width: 40px;
        }
        .style5
        {
            width: 199px;
        }
        .style6
        {
            width: 88px;
        }
        .style7
        {
            width: 80px;
        }
        .style8
        {
            width: 199px;
        }
        .style9
        {
            width: 199px;
        }
        .style12
        {
            height: 22px;
        }
        .style13
        {
            width: 199px;
        }
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            width: 199px;
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width:75%; ">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" 
                            RepeatDirection="Horizontal" style="margin-left: 0px; margin-right: 0px" 
                            Width="250px">
                            <asp:ListItem Value="1">Materials</asp:ListItem>
                            <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                            <asp:ListItem Value="3">Semi FG</asp:ListItem>
                            <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="style8" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Call In</asp:ListItem>
                            <asp:ListItem Value="2">Shortage</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" Width="155px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDesc" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="149px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label12" runat="server" Text="Supplier"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:TextBox ID="tbSup" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style1">
                        <asp:Label ID="Label9" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="auto-style2" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BorderColor="Blue" BorderStyle="Solid" 
                            BorderWidth="1px" Width="149px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="End Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbEndDate" runat="server" Width="80px" BorderColor="Blue" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbEndDate_CalendarExtender" runat="server" 
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbEndDate">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF">
                        &nbsp;</td>
                    <td class="style13" style="background-color: #FFFFFF">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" ForeColor="Red" 
                            Text="Call In=Stock+PO Insp.-Mo Issue-SO "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" ForeColor="Red" 
                            Text="Shortage=Stock+PO Insp.+ PO+PO Manual+PO Forcast+PO MO+PR-MO Rcv-SO-MO Issue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
                <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
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

