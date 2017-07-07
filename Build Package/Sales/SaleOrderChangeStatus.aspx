<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderChangeStatus.aspx.vb" Inherits="MIS_T100.SaleOrderChangeStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc3" %>
<%@ Register src="../UserControl/Multiple/UsingSectionSalesCheckList.ascx" tagname="UsingSectionSalesCheckList" tagprefix="uc4" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc5" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvReportHeadScrollbar();
            GvReportLineScrollbar();
            GvPrintHeadScrollbar();
            GvPrintLineScrollbar();

        });

        function GvReportHeadScrollbar() {
            gridView1 = $('#<%= GvReportHead.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: 1075,
                height: 300,
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
            function GvReportLineScrollbar() {
            gridView1 = $('#<%= GvReportLine.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 5,
                width: 1390,
                height: 300,
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
            function GvPrintHeadScrollbar() {
            gridView1 = $('#<%= GvPrintHead.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: 1075,
                height: 300,
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
            function GvPrintLineScrollbar() {
            gridView1 = $('#<%= GvPrintLine.ClientID %>').gridviewScroll({
                width: 1700,
                height: 300,
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
        .auto-style5 {
            width: 1044px;
        }
        .auto-style8 {
            height: 36px;
        }
        .auto-style11 {
            width: 397px;
        }
        .auto-style12 {
            width: 76px;
        }
        .auto-style13 {
            width: 397px;
            height: 38px;
        }
        .auto-style14 {
            width: 76px;
            height: 38px;
        }
        .auto-style15 {
            height: 38px;
        }
        .auto-style18 {
            height: 21px;
        }
        .auto-style20 {
            width: 9%;
        }
        .auto-style21 {
            width: 19%;
        }
        .auto-style23 {
            width: 8%;
        }
        .auto-style24 {
            width: 29%;
        }
        .auto-style25 {
            width: 68px;
        }
        .auto-style26 {
            width: 26%;
        }
        .auto-style27 {
            width: 100%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Height="29%" Width="100%" AutoPostBack="True">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Report
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table class="auto-style5" Height="312px">
                            <tr>
                                <td colspan="3"style="background-image: url('../Images/btt.jpg');">
                                    <uc1:HeaderForm ID="HeaderForm2" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style11"></td>
                                <td class="auto-style12">
                                    <asp:Label ID="Label1" runat="server" Text="SO Type :"></asp:Label>
                                </td>
                                <td class="auto-style29">
                                    &nbsp;<asp:DropDownList ID="ReptdllSOType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">&nbsp;</td>
                                <td class="auto-style12">
                                    <asp:Label ID="Label2" runat="server" Text="SO No. :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="RepttxtSONo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style13"></td>
                                <td class="auto-style14">
                                    <asp:Label ID="Label6" runat="server" Text="Chg. Ver :"></asp:Label>
                                </td>
                                <td class="auto-style15">
                                    <asp:TextBox ID="RepttxtChgVer" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">&nbsp;</td>
                                <td class="auto-style12">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr align="center">
                                <td colspan="3">
                                    <asp:Button ID="ReptbtnSearch" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                        </table>
                        <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Print
                    </HeaderTemplate>
                    <ContentTemplate>
                       <table style="width: 100%;">
                            <tr>
                                <td colspan="6"style="background-image: url('../Images/btt.jpg');">
                                    <uc1:HeaderForm ID="HeaderForm1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style20">
                                    <asp:Label ID="Label3" runat="server" Text="Section :"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <uc4:UsingSectionSalesCheckList ID="UsingSectionSalesCheckList" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" class="auto-style18"></td>
                            </tr>
                            <tr>
                                <td class="auto-style20">
                                    <asp:Label ID="Label4" runat="server" Text="SO Type :"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <uc6:UsingTypeSaleCheckList ID="UsingTypeSaleCheckList1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style20">
                                    &nbsp;</td>
                                <td class="auto-style21">&nbsp;</td>
                                <td class="auto-style25">
                                    <asp:Label ID="Label5" runat="server" Text="Customer :"></asp:Label>
                                </td>
                                <td class="auto-style26">
                                    <asp:TextBox ID="PrttxtCust" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style23">
                                    <asp:Label ID="Label8" runat="server" Text="Status :"></asp:Label>
                                </td>
                                <td class="auto-style24">&nbsp;<asp:DropDownList ID="PrtddlStus" runat="server">
                                        <asp:ListItem Value="U">U : Unapproved</asp:ListItem>
                                        <asp:ListItem Value="Y ">Y : Approved</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style20">
                                    &nbsp;</td>
                                <td class="auto-style21">&nbsp;</td>
                                <td class="auto-style25">
                                    <asp:Label ID="Label7" runat="server" Text="Date From :"></asp:Label>
                                </td>
                                <td class="auto-style26">
                                    <uc5:DateT100 ID="DateT1001" runat="server" />
                                </td>
                                <td class="auto-style23">
                                    <asp:Label ID="Label9" runat="server" Text="Date To :"></asp:Label>
                                </td>
                                <td class="auto-style24">
                                    <uc5:DateT100 ID="DateT1002" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" class="auto-style18"></td>
                            </tr>
                            <tr align="center">
                                <td colspan="6" class="auto-style8">
                                    <asp:Button ID="PrtbtnShearch" runat="server" Text="Search" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6"style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                            </tr>
                        </table>                                  
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <div style="width:100%; background-color :white;">
            <asp:Button ID="PrinttbtnExport" runat="server" Text="Excel Export" />
            </div>
            <div>
                
                    <asp:Panel ID="Panel2" runat="server">
                        <div style="background-color:#d9f4f7;">
                                <div style="width:100%; background-color :white;">
                                    <uc2:CountRow ID="CountRow5" runat="server" />
                                </div>
                        
                                    <asp:GridView ID="GvReportHead" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                        <br />
                    </asp:Panel>                   
                </div>
            <asp:Panel ID="Panel3" runat="server">
                        <div style="width:100%; background-color :white;">                           
                            <uc2:CountRow ID="CountRow6" runat="server" />                           
                        </div>
                        <div style="background-color:#e4d8f3; ">
                        <asp:GridView ID="GvReportLine" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                            <br />
                        </div>
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server">
                        <div style="background-color :white;" >
                            <uc2:CountRow ID="CountRow3" runat="server" />
                        </div>
                        <div style="background-color:#d9f4f7;">
                        <asp:GridView ID="GvPrintHead" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="True" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                            <br />                           
                        </div>
            </asp:Panel>                      
            <asp:Panel ID="Panel5" runat="server">
                        <div style="background-color :white;">
                            <uc2:CountRow ID="CountRow4" runat="server" />
                        <div style="width:100%; background-color:#e4d8f3; ">
                            <asp:GridView ID="GvPrintLine" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <br />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                            <asp:Button ID="btnPrint" runat="server" Text="Print" />
                            </asp:Panel>
                            <br />
                        </div>
           </asp:Panel>
                </div>
        </ContentTemplate>
       <Triggers>
            <asp:PostBackTrigger ControlID="PrinttbtnExport"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
