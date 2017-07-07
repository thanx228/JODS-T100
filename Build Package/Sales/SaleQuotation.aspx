<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleQuotation.aspx.vb" Inherits="MIS_T100.SaleQuotation" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc4" %>
<%@ Register src="../UserControl/Multiple/UsingProductClassification.ascx" tagname="UsingProductClassification" tagprefix="uc5" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvSaleQuotationScrollbar();

        });

        function GvSaleQuotationScrollbar() {
            gridView1 = $('#<%= GvSaleQuotation.ClientID %>').gridviewScroll({
                width: screen.availWidth - 10,
                //width: screen.availWidth,
                width: 1500,
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
        .auto-style83 {
            height: 23px;
        }
        .auto-style84 {
            width: 67px;
        }
        .auto-style85 {
            height: 23px;
            width: 67px;
        }
        .auto-style89 {
            height: 23px;
            width: 252px;
        }
        .auto-style91 {
            height: 23px;
            width: 97px;
        }
        .auto-style93 {
            width: 97px;
        }
        .auto-style95 {
            width: 108px;
        }
        .auto-style96 {
            height: 23px;
            width: 108px;
        }
        .auto-style97 {
            width: 140px;
        }
        .auto-style98 {
            height: 23px;
            width: 140px;
        }
        .auto-style99 {
            width: 252px;
        }
        .auto-style100 {
            width: 67px;
            height: 24px;
        }
        .auto-style101 {
            width: 140px;
            height: 24px;
        }
        .auto-style102 {
            width: 108px;
            height: 24px;
        }
        .auto-style103 {
            height: 24px;
        }
        .auto-style104 {
            margin-top: 1px;
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
                     <td style="background-image: url('../Images/btt.jpg');" colspan="9">
                         <uc1:HeaderForm ID="HeaderForm1" runat="server" />
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style84">&nbsp;</td>
                     <td class="auto-style97">&nbsp;</td>
                     <td class="auto-style95">&nbsp;</td>
                     <td class="auto-style99">&nbsp;</td>
                     <td class="auto-style93">&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style84">SO Type :</td>
                     <td colspan="8">
                         <uc6:UsingTypeSaleCheckList ID="UsingTypeSaleCheckList1" runat="server" />
                     </td>
                 </tr>
                 <tr>
                     <td class="auto-style85"></td>
                     <td class="auto-style98">&nbsp;</td>
                     <td class="auto-style96">CustID :</td>
                     <td class="auto-style89">
                         <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                     </td>
                     <td class="auto-style91">
                         <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                         &nbsp;:</td>
                     <td class="auto-style83">
                         &nbsp;
                         <asp:DropDownList ID="ddlStatus" runat="server" CssClass="auto-style104">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                            <asp:ListItem Value="1">Not Close</asp:ListItem>
                            <asp:ListItem Value="2">Auto Close</asp:ListItem>
                            <asp:ListItem Value="3">Close Manual</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                     <td class="auto-style83"></td>
                     <td class="auto-style83"></td>
                     <td class="auto-style83"></td>
                 </tr>
                 <tr>
                     <td class="auto-style84">&nbsp;</td>
                     <td class="auto-style97">&nbsp;</td>
                     <td class="auto-style95">SO No. :</td>
                     <td class="auto-style99">
                         <asp:TextBox ID="txtSONo" runat="server"></asp:TextBox>
                     </td>
                     <td class="auto-style93">
                         Version&nbsp;:</td>
                     <td>
                         <asp:TextBox ID="txtSOSeq" runat="server"></asp:TextBox>
                     </td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style84">&nbsp;</td>
                     <td class="auto-style97">&nbsp;</td>
                     <td class="auto-style95">Item No :</td>
                     <td class="auto-style99">
                         <asp:TextBox ID="txtPartNo" runat="server"></asp:TextBox>
                     </td>
                     <td class="auto-style93">
                         <asp:Label ID="Label15" runat="server" Text="Spec"></asp:Label>
                         &nbsp;:</td>
                     <td>
                         <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                     </td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style84">&nbsp;</td>
                     <td class="auto-style97">&nbsp;</td>
                     <td class="auto-style95">From Date :</td>
                     <td class="auto-style99">
                         <uc2:DateT100 ID="DateT1001" runat="server" />
                     </td>
                     <td class="auto-style93">
                         End Date&nbsp;:</td>
                     <td>
                         <uc2:DateT100 ID="DateT1002" runat="server" />
                     </td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="auto-style100"></td>
                     <td class="auto-style101"></td>
                     <td class="auto-style102">Condition :</td>
                     <td colspan="6" class="auto-style103">
                         <asp:CheckBox ID="cbNotQuon" runat="server" Text="SO Not Quotation" />
                         <asp:CheckBox ID="cbPrice" runat="server" Text="SO Price less then Qout. Price" />
                     </td>
                 </tr>
                 <tr align="center">
                     <td colspan="9">
                         &nbsp;</td>
                 </tr>
                 <tr align="center">
                     <td colspan="9">
                         <asp:Button ID="btnSearch" runat="server" Text="Search" />
                         &nbsp;<asp:Button ID="btnExcelExport" runat="server" Text="Export Excel " />
                     </td>
                 </tr>
                 <tr>
                     <td colspan="9">&nbsp;</td>
                 </tr>
                 <tr>
                     <td colspan="9" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                 </tr>
             </table>
             <div style="width:100%; background-color :white;">                      
                 <uc3:CountRow ID="CountRow1" runat="server" />                      
             </div>
            <div style="width:100%; background-color:#d9f4f7;">
                <asp:GridView ID="GvSaleQuotation" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB"  Wrap="False" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Center" Wrap="False" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br /> 
            </div>
            <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>     
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
