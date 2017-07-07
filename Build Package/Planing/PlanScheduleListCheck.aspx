<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanScheduleListCheck.aspx.vb" Inherits="MIS_T100.PlanScheduleListCheck" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="Date" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 524px;
        }
        .style5
        {
            height: 21px;
        }
        .style9
        {
            height: 21px;
        }
        .style10
        {
            width: 127px;
        }
        .style11
        {
            height: 21px;
            width: 127px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 75%;">
                <tr>
                    <td class="style4" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Plan Schedule List"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; background-color: #FFFFFF;">
                <tr>
                    <td style="vertical-align: top" class="style10">
                        <asp:Label ID="Label10" runat="server" Text="Workstaion"></asp:Label>
                    </td>
                    <td colspan="3">
                        <uc1:UsingWorkstationCheckList runat="server" ID="UsingWorkstation" />
               <%--         <asp:CheckBoxList ID="cblWorkCenter" runat="server">
                        </asp:CheckBoxList>--%>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;background-color:#FFFFFF;">
                   <tr>
                    <td class="style10" style="vertical-align: top">
                        <asp:Label ID="Label1" runat="server" Text="Date FM"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="txtDateFrom" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Date TO"></asp:Label>
                    </td>
                    <td>
                        <uc1:Date ID="txtDateTo" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" Width="100px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
