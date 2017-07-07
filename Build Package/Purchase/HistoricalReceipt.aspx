<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="HistoricalReceipt.aspx.vb" Inherits="MIS_T100.HistoricalReceipt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style1
    {
        width: 282px;
    }
        .style6
        {
            width: 252px;
        }
        .style8
        {
            width: 795px;
        }
        .auto-style1 {
            width: 128px;
        }
        .auto-style2 {
            width: 197px;
        }
        .auto-style3 {
            width: 69px;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="7000">
            </asp:ScriptManager>
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White" class="auto-style1">
                        Item :</td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="auto-style3">
                        SO Type :</td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlSO" runat="server">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                            <asp:ListItem>2213</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style1">
                        Approved Status</td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="Y">Y:Comfirmed</asp:ListItem>
                            <asp:ListItem Value="N">N:UnComfirmed</asp:ListItem>
                            <asp:ListItem Value="S">S:Posted</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White" class="auto-style3">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style1">
                        Accept Date From :</td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="tbDateFrom" Format="yyyy/MM/dd">
                    </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="auto-style3">
                        <asp:Label ID="Label14" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="tbDateTo" Format="yyyy/MM/dd">
                    </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%; background-image: url('http://localhost:57600/Images/btt.jpg');">
                <tr>
                    <td align="center" class="style8" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btSearch" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label1" runat="server" Text="Amount Of rows "></asp:Label>&nbsp;<asp:Label ID="lbCount" runat="server" ForeColor="#0000CC"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="False" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
<br />
            <br />
            <br />
            <br />
            <br />
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

