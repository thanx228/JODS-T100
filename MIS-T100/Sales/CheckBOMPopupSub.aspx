<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CheckBOMPopupSub.aspx.vb" Inherits="MIS_T100.CheckBOMPopupSub" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvSOScrollbar();
            GvMatIssueScrollbar;
            GvMOScrollbar();
            GvPOScrollbar();
            GvPRScrollbar;
        });
        
        function GvSOScrollbar() {
            gridView1 = $('#<%= GvSO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
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
            function GvMatIssueScrollbar() {
            gridView1 = $('#<%= GvMatIssue.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
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

        function GvMOScrollbar() {
            gridView1 = $('#<%= GvMO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
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

        function GvPOScrollbar() {
            gridView1 = $('#<%= GvPO.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
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
                    function GvPRScrollbar() {
            gridView1 = $('#<%= GvPR.ClientID %>').gridviewScroll({
                //width: screen.availWidth - 10,
                width: 1075,
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
        .auto-style1 {
            width: 100%;
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
                    <td align="center"colspan="8"style="background-image: url('../Images/btt.jpg');">
                    <asp:Label ID="Label18" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text=" Bom Detail "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label5" runat="server" Text="Mat IssueQty"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label6" runat="server" Text="On StockQty"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label7" runat="server" Text="SOQty"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label8" runat="server" Text="MOQty"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="Label9" runat="server" Text="POQty"></asp:Label>
                    </td>
                    <td align="center">PRQty&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblItem" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblSpec" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblIssueQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblSTQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblSOQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblMOQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblPOQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblPRQty" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table> 
            <div style="width:100%; background-color:#d9f4f7;"> 
            <div style="background-color :white;" class="auto-style1">     
                <asp:Label runat="server" Text="SO List" Font-Size="Medium" ForeColor="OrangeRed" Font-Bold="True"></asp:Label>
                &nbsp;<uc1:CountRow ID="CountRow1" runat="server" />
            </div>
            <asp:GridView ID="GvSO" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <div style="width:100%; background-color:#e4d8f3;">
            <div style="width:100%; background-color :white;">
                <asp:Label runat="server" Text="Mat Issue List" Font-Size="Medium" ForeColor="OrangeRed" Font-Bold="True"></asp:Label>
                &nbsp;<uc1:CountRow ID="CountRow2" runat="server" />          
            </div>
            <asp:GridView ID="GvMatIssue" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <div style="width:100%; background-color:#d9f4f7;"> 
            <div style="width:100%; background-color :white;">
                 <asp:Label runat="server" Text="MO List" Font-Size="Medium" ForeColor="OrangeRed" Font-Bold="True"></asp:Label>
                 &nbsp;<uc1:CountRow ID="CountRow3" runat="server" />         
            </div>
            <asp:GridView ID="GvMO" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <div style="width:100%; background-color:#e4d8f3;"> 
            <div style="width:100%; background-color :white;">   
                  <asp:Label runat="server" Text="PO List" Font-Size="Medium" ForeColor="OrangeRed" Font-Bold="True"></asp:Label>
                 &nbsp;<uc1:CountRow ID="CountRow4" runat="server" />
           
            </div>
                        <asp:GridView ID="GvPO" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <div style="width:100%; background-color:#d9f4f7;">
            <div style="width:100%; background-color :white;">
                <asp:Label runat="server" Text="PR List" Font-Size="Medium" ForeColor="OrangeRed" Font-Bold="True"></asp:Label>
                 &nbsp;<uc1:CountRow ID="CountRow5" runat="server" />
            </div>
            <asp:GridView ID="GvPR" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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
            <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
