<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleUndeliveryStatusAmount.aspx.vb" Inherits="MIS_T100.SaleUndeliveryStatusAmount" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc4" %>
<%@ Register src="../UserControl/Multiple/UsingProductClassification.ascx" tagname="UsingProductClassification" tagprefix="uc5" %>
<%@ Register src="../UserControl/Multiple/UsingTypeSaleCheckList.ascx" tagname="UsingTypeSaleCheckList" tagprefix="uc6" %>
<%@ Register src="../UserControl/HeaderFormT100.ascx" tagname="HeaderFormT100" tagprefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvUndeliveryStusAmountScrollbar();

        });

        function GvUndeliveryStusAmountScrollbar() {
            gridView1 = $('#<%= GvUndeliveryStusAmount.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1300,
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <table style="width:100%; background-color:white">
                <tr>
                    <td colspan="6" style="background-image: url('../Images/btt.jpg');">
                        <uc7:HeaderFormT100 ID="HeaderFormT1001" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style28">SO Type :</td>
                    <td class="auto-style4" colspan="5">
                        <uc6:UsingTypeSaleCheckList ID="UsingTypeSaleCheckList1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style18">&nbsp;</td>
                    <td class="auto-style10">CustID :</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="Label11" runat="server" Text="Condition"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style8">
                        &nbsp;
                        <asp:DropDownList ID="ddlCondition" runat="server">
                            <asp:ListItem >ALL</asp:ListItem>
                            <asp:ListItem Value="1">Stock&lt;Undelivery</asp:ListItem>
                            <asp:ListItem Value="2">Supply &gt;= Undelivery</asp:ListItem>
                            <asp:ListItem Value="3">Supply &lt; Undelivery</asp:ListItem>
                            <asp:ListItem Value="4">Stock&gt;=Undelivery</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        </td>
                    <td class="auto-style2"></td>
                    <td class="auto-style2">
                        <asp:Label ID="Label3" runat="server" Text="SO No."></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSONo" runat="server" ValidateRequestMode="Enabled"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label14" runat="server" Text="Version"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtVersion" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        </td>
                    <td class="auto-style18"></td>
                    <td class="auto-style10">
                        <asp:Label ID="Label4" runat="server" Text="Item No"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style20">
                        <asp:TextBox ID="txtItemNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style21">
                        <asp:Label ID="Label15" runat="server" Text="Spec"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style22">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        </td>
                    <td class="auto-style18"></td>
                    <td class="auto-style10">
                        <asp:Label ID="Label12" runat="server" Text="From Due Date "></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style20">
                        <uc2:DateT100 ID="DateT1001" runat="server" />
                    </td>
                    <td class="auto-style21">
                        <asp:Label ID="Label5" runat="server" Text="End Due Date"></asp:Label>
                        &nbsp;:</td>
                    <td class="auto-style22">
                        <uc2:DateT100 ID="DateT1002" runat="server" />
                    </td>
                </tr>
                <tr >
                    <td class="auto-style4" colspan="6">
                    </td>
                </tr>
                <tr align="center">
                    <td class="auto-style4" colspan="6">
                        &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btnExcelExport" runat="server" Text="Export Excel " />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="6"></td>
                </tr>
                <tr>
                    <td class="auto-style4" colspan="6" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                </tr>
            </table>
             <div style="width:100%; background-color :white;">                      
                 <uc3:CountRow ID="CountRow1" runat="server" />                      
             </div>
            <div style="width:100%; background-color:#d9f4f7;">
                <asp:GridView ID="GvUndeliveryStusAmount" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                <br />
            </dvi>
            <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>     
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
