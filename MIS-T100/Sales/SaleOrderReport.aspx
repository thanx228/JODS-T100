<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderReport.aspx.vb" Inherits="MIS_T100.SaleOrderReport" %>
<%@ Register Src="~/UserControl/Multiple/UsingSectionSalesCheckList.ascx" TagPrefix="uc1" TagName="UsingSectionSalesCheckList" %>
<%@ Register Src="~/UserControl/HeaderForm.ascx" TagPrefix="uc1" TagName="HeaderForm" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/MonthDateT100.ascx" tagname="MonthDateT100" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvSOReportScrollbar();

        });

        function GvSOReportScrollbar() {
            gridView1 = $('#<%= GvSOReport.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: screen.availWidth,
                width: 1190,
                height: 800,
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
        .auto-style3 {
            width: 151px;
        }
        .auto-style4 {
            width: 100%;
            height: 15px;
        }
        .auto-style5 {
            width: 233px;
        }
        .auto-style6 {
            width: 144px;
        }
        .auto-style7 {
            width: 93px;
        }
        .auto-style8 {
            height: 21px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width:100%; background-color:white;">
        <tr>
            <td colspan="2"style="background-image: url('../Images/btt.jpg');">
                <uc1:HeaderForm ID="HeaderForm1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Salec Section :</td>
            <td>
                <uc1:UsingSectionSalesCheckList ID="UsingSectionSalesCheckList1" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="auto-style3">Product Classification : </td>
            <td>
                        &nbsp;
                        <asp:DropDownList ID="ddlIndusType" runat="server">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Aero</asp:ListItem>
                            <asp:ListItem Value="2">General</asp:ListItem>
                        </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>          
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="85px" Width="1064px">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>Sale Order</HeaderTemplate>
                  <ContentTemplate>
                    <table class="auto-style4" style="background-color:white;">
                        <tr>
                            <td class="auto-style6" >End Date of Month :</td>
                            <td class="auto-style5" >
                                &nbsp;</td>
                            <td class="auto-style7" >Report Type :</td>
                            <td >
                                     <asp:DropDownList ID="ddlReportType" runat="server">
                                        <asp:ListItem Value="1">Receive Order</asp:ListItem>
                                        <asp:ListItem Value="2">SO Balance</asp:ListItem>
                                    </asp:DropDownList>                               
                            </td>
                        </tr>
                    </table>
                 </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>Plan Delivery</HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%; background-color:white;">
                            <tr>
                                <td class="auto-style6">Date Start :</td>
                                <td class="auto-style5">&nbsp;</td>
                                <td>Date End :</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <table style="width: 100%; background-color:white;">
                <tr align="center">
                    <td style="background-image: url('../Images/btt.jpg');">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btnExcelExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <div style="width:100%; background-color :white;">
                <uc3:CountRow ID="CountRow1" runat="server" />
            </div>
            <div style="background-color:#d9f4f7;">
                <asp:GridView ID="GvSOReport" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            </div>     
            <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>          
        </ContentTemplate>
                <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
</asp:Content>
