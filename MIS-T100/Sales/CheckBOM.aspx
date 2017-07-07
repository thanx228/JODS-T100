﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CheckBOM.aspx.vb" Inherits="MIS_T100.CheckBOM" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc4" %>
<%@ Register src="../UserControl/HeaderFormT100.ascx" tagname="HeaderFormT100" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvChkBomScrollbar();

        });

        function GvChkBomScrollbar() {
            gridView1 = $('#<%= GvChkBom.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1900,
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
        .auto-style4 {
            height: 18px;
        }
        .auto-style6 {
            width: 23%;
        }
        .auto-style8 {
            width: 57%;
        }
        .auto-style10 {
            width: 12%;
            height: 18px;
        }
        .auto-style11 {
            width: 10%;
        }
        .auto-style20 {
            width: 23%;
            height: 18px;
        }
        .auto-style21 {
            width: 10%;
            height: 18px;
        }
        .auto-style22 {
            width: 57%;
            height: 18px;
        }
        .auto-style23 {
            width: 12%;
            height: 26px;
        }
        .auto-style24 {
            width: 9%;
            height: 26px;
        }
        .auto-style25 {
            width: 23%;
            height: 26px;
        }
        .auto-style26 {
            width: 10%;
            height: 26px;
        }
        .auto-style27 {
            width: 57%;
            height: 26px;
        }
        .auto-style28 {
            width: 12%;
            height: 18px;
            text-align: left;
        }
        .auto-style30 {
            width: 9%;
            height: 18px;
        }
        .auto-style31 {
            height: 33px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%; background-color:white">
                <tr>
                    <td colspan="10" style="background-image: url('../Images/btt.jpg');">
                        <uc5:HeaderFormT100 ID="HeaderFormT1001" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style28" colspan="2">SO Type :</td>
                    <td class="auto-style4" colspan="8">
                        <uc4:UsingTypeSaleCheckList ID="UsingTypeSaleCheckList1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">&nbsp;</td>
                    <td class="auto-style30">&nbsp;</td>
                    <td class="auto-style10" colspan="2">CustID :</td>
                    <td class="auto-style6" colspan="2">
                        <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        Condition :</td>
                    <td class="auto-style8" colspan="2">
                        &nbsp;
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem Value="Y">Approved</asp:ListItem>
                            <asp:ListItem Value="N">No Bom</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style23" colspan="2">
                        </td>
                    <td class="auto-style24"></td>
                    <td class="auto-style23" colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="SO No."></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style25" colspan="2">
                        <asp:TextBox ID="txtSONo" runat="server" ValidateRequestMode="Enabled"></asp:TextBox>
                    </td>
                    <td class="auto-style26">
                        <asp:Label ID="Label14" runat="server" Text="Version"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style27" colspan="2">
                        <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">
                        </td>
                    <td class="auto-style30"></td>
                    <td class="auto-style10" colspan="2">
                        Item No :</td>
                    <td class="auto-style20" colspan="2">
                        <asp:TextBox ID="txtItemNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style21">
                        Spec :</td>
                    <td class="auto-style22" colspan="2">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">
                        </td>
                    <td class="auto-style30"></td>
                    <td class="auto-style10" colspan="2">
                        <asp:Label ID="Label12" runat="server" Text="From Due Date "></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style20" colspan="2">
                        <uc2:DateT100 ID="DateT1001" runat="server" />
                    </td>
                    <td class="auto-style21">
                        <asp:Label ID="Label5" runat="server" Text="End Due Date"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style22" colspan="2">
                        <uc2:DateT100 ID="DateT1002" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style4" colspan="3">&nbsp;</td>
                    <td class="auto-style4">
                        &nbsp;</td>
                    <td class="auto-style4">
                        &nbsp;
                        </td>
                    <td class="auto-style4" colspan="3">&nbsp;</td>
                    <td class="auto-style4">&nbsp;</td>
                </tr>
                <tr align="center">
                    <td class="auto-style4" colspan="10">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btnExcelExport" runat="server" Text="Export Excel " />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="10">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="10" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                </tr>
            </table>
             <div style="width:100%; background-color :white;">                      
                 <uc3:CountRow ID="CountRow1" runat="server" />                      
             </div>
            <div style="width:100%; background-color:#d9f4f7;">
                <asp:GridView ID="GvChkBom" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <asp:HyperLink ID="ShowDetail" runat="server">Detail</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" Wrap="False" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" Wrap="False" />
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
</asp:Content>
