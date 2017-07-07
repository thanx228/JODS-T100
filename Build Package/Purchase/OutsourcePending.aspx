<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="OutsourcePending.aspx.vb" Inherits="MIS_T100.OutsourcePending" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 26px;
        }
    .style1
    {
        width: 282px;
    }
        .auto-style1 {
            width: 124px;
        }
        .auto-style2 {
            height: 29px;
            width: 124px;
        }
        .auto-style3 {
            width: 105px;
        }
        .auto-style4 {
            height: 29px;
            width: 105px;
        }
    </style>

     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td style="background-color: #FFFFFF" class="auto-style1">
                    <asp:Label ID="Label4" runat="server" Text="MO Type"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:DropDownList ID="ddlMoType" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="background-color: #FFFFFF" class="auto-style3">
                    <asp:Label ID="Label9" runat="server" Text="MO No."></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF" class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Item"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                </td>
                <td style="background-color: #FFFFFF" class="auto-style3">
                    <asp:Label ID="Label10" runat="server" Text="Spec"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF" class="auto-style1">
                    <asp:Label ID="Label11" runat="server" Text="Supplier Code"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbSup" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td style="background-color: #FFFFFF" class="auto-style3">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="background-color: #FFFFFF">
                    <asp:Label ID="Label6" runat="server" Text="Transfer Type"></asp:Label>
                </td>
                <td colspan="3" style="background-color: #FFFFFF">
                    <asp:CheckBoxList ID="cblTrnType" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" style="background-color: #FFFFFF">
                    <asp:Label ID="Label7" runat="server" Text="Transfer Date From"></asp:Label>
                </td>
                <td class="style4" style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                    <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="tbDateFrom" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td class="auto-style4" style="background-color: #FFFFFF">
                    <asp:Label ID="Label12" runat="server" Text="Transfer Date To"></asp:Label>
                </td>
                <td class="style4" style="background-color: #FFFFFF">
                    <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                    <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="tbDateTo" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF" class="auto-style1">
                    <asp:Label ID="Label8" runat="server" Text="Transfer Status"></asp:Label>
                </td>
                <td style="background-color: #FFFFFF">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Selected="True" Value="A">ALL Status</asp:ListItem>
                        <asp:ListItem Value="Y">Aproved</asp:ListItem>
                        <asp:ListItem Value="N">Opened</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="background-color: #FFFFFF" class="auto-style3">
                    &nbsp;</td>
                <td style="background-color: #FFFFFF">
                    &nbsp;</td>
            </tr>
        </table>
        <table style="width: 69%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Button ID="btShow" runat="server" Text="Show Report" />
                    &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Text=" Amount Of Rows " ForeColor="Blue"></asp:Label>&nbsp;<asp:Label ID="lbCount" runat="server" Text=""></asp:Label>
        <asp:GridView ID="gvShow" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Width="190px">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                Wrap="False" />
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

