<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BillInvoice.aspx.vb" Inherits="MIS_T100.BillInvoice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc1" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc2" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <style type="text/css">
        .auto-style1 {
            height: 9px;
        }
        .auto-style9 {
            height: 18px;
            width: 252px;
        }
        .auto-style15 {
            height: 18px;
            width: 125px;
        }
        .auto-style22 {
            height: 18px;
        }
        .auto-style25 {
            height: 18px;
            width: 170px;
        }
        .auto-style27 {
            height: 18px;
            width: 334px;
        }
        .auto-style28 {
            height: 17px;
            width: 244px;
        }
        .auto-style29 {
            height: 17px;
            width: 114px;
        }
        .auto-style30 {
            height: 17px;
            }
        .auto-style35 {
            height: 18px;
            width: 395px;
        }
        .auto-style41 {
            height: 18px;
            width: 185px;
        }
        .auto-style45 {
            height: 18px;
            width: 210px;
        }
        .auto-style48 {
            height: 18px;
            width: 116px;
        }
        .auto-style54 {
            width: 100%;
        }
        .auto-style63 {
            width: 204px;
            height: 18px;
        }
        .auto-style65 {
            height: 18px;
            width: 4px;
        }
        .auto-style69 {
            height: 18px;
            width: 176px;
        }
        .auto-style76 {
            width: 100%;
            height: 357px;
        }
        .auto-style79 {
            height: 18px;
            width: 93px;
        }
        .auto-style82 {
            height: 18px;
            width: 114px;
        }
        .auto-style84 {
            width: 244px;
            height: 18px;
        }
        .auto-style85 {
            height: 18px;
            width: 161px;
        }
        .auto-style86 {
            height: 18px;
            width: 149px;
        }
        .auto-style87 {
            height: 18px;
            width: 20px;
        }
        .auto-style88 {
            height: 22px;
        }
        .auto-style93 {
            width: 89px;
        }
        .auto-style94 {
            height: 22px;
            width: 89px;
        }
        .auto-style95 {
            width: 349px;
        }
        .auto-style96 {
            height: 22px;
            width: 349px;
        }
        .style1
    {
        width: 282px;
    }
        .auto-style98 {
            width: 105px;
        }
        .auto-style99 {
            height: 21px;
        }
        .auto-style101 {
            width: 105px;
            height: 21px;
        }
        .auto-style106 {
            width: 215px;
        }
        .auto-style107 {
            height: 21px;
            width: 215px;
        }
        .auto-style113 {
            width: 146px;
        }
        .auto-style114 {
            height: 21px;
            width: 146px;
        }
        .auto-style117 {
            width: 100%;
            height: 764px;
        }
        .auto-style118 {
            height: 32px;
        }
        .auto-style119 {
            width: 338px;
            height: 32px;
        }
        .auto-style120 {
            width: 93px;
            height: 32px;
        }
        .auto-style122 {
            height: 34px;
        }
        .auto-style123 {
            width: 94px;
        }
        .auto-style125 {
            height: 22px;
            width: 93px;
        }
        .auto-style126 {
            width: 93px;
        }
        .auto-style127 {
            width: 338px;
        }
        .auto-style128 {
            height: 22px;
            width: 338px;
        }
        .auto-style129 {
            width: 192px;
            height: 21px;
        }
        .auto-style130 {
            width: 192px;
        }
        .auto-style131 {
            width: 103px;
        }
        .auto-style132 {
            width: 150px;
        }
        .auto-style134 {
            width: 151px;
        }
        .auto-style135 {
            width: 211px;
        }
        .auto-style136 {
            width: 211px;
            height: 28px;
        }
        .auto-style137 {
            width: 103px;
            height: 28px;
        }
        .auto-style138 {
            width: 150px;
            height: 28px;
        }
        .auto-style139 {
            width: 94px;
            height: 28px;
        }
        .auto-style140 {
            width: 151px;
            height: 28px;
        }
        .auto-style141 {
            height: 28px;
        }
        </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvAddDataScrollbar();
            GvDeleteScrollbar();
            GvEditScrollbar();
            GvEdit1Scrollbar();
            GvReprotScrollbar();
        });

        function GvAddDataScrollbar() {
            gridView1 = $('#<%= GvAddData.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: screen.availWidth,
                width: 1000,
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
            function GvDeleteScrollbar() {
            gridView1 = $('#<%= GvDelete.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: screen.availWidth,
                width: 1000,
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
            function GvEditScrollbar() {
            gridView1 = $('#<%= GvEdit.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: screen.availWidth,
                width: 1000,
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
            function GvEdit1Scrollbar() {
            gridView1 = $('#<%= GvEdit1.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: screen.availWidth,
                width: 1000,
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
            function GvReprotScrollbar() {
            gridView1 = $('#<%= GvReprot.ClientID %>').gridviewScroll({
                //width: screen.availWidth -30,
                width: screen.availWidth,
                width: 1000,
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
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="1378px" Width="1063px">
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        Add Data
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="10"style="background-image: url('http://localhost:8080/Images/btt.jpg');" >
                                    <uc2:HeaderForm ID="HeaderForm1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style25"></td>
                                <td class="auto-style35"></td>
                                <td class="auto-style41">
                                    <asp:Label ID="Label3" runat="server" Text="Custommer ID :"></asp:Label>
                                </td>
                                <td class="auto-style63">
                                    <asp:TextBox ID="txtCustID" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style65"></td>
                                <td class="auto-style69">
                                    <asp:Label ID="Label19" runat="server" Text="Date Form :"></asp:Label>
                                </td>
                                <td class="auto-style22">
                                    <uc1:DateT100 ID="DateT1001" runat="server" />
                                </td>
                                <td class="auto-style15"></td>
                                <td class="auto-style27"></td>
                                <td class="auto-style22"></td>
                            </tr>
                            <tr>
                                <td class="auto-style25"></td>
                                <td class="auto-style35"></td>
                                <td class="auto-style41">
                                    <asp:Label ID="Label4" runat="server" Text="Bill BY :"></asp:Label>
                                </td>
                                <td class="auto-style22">
                                    &nbsp;<asp:DropDownList ID="ddlEmp" runat="server" Height="23px">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style65">&nbsp;</td>
                                <td class="auto-style69">
                                    <asp:Label ID="Label20" runat="server" Text="Date To :"></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <uc1:DateT100 ID="DateT1002" runat="server" />
                                </td>
                                <td class="auto-style15">
                                </td>
                                <td class="auto-style27"></td>
                                <td class="auto-style1"></td>
                            </tr>
                            </table>
                            <table style="width:100%;">
                            <tr>
                                <td class="auto-style22" colspan="8">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style84">
                                    <asp:Label ID="lblBill" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style82">
                                    <asp:Label ID="Label5" runat="server" Text="Custommer ID :"></asp:Label>
                                </td>
                                <td class="auto-style85">
                                    <asp:Label ID="lblCust" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style86">
                                    <asp:Label ID="Label6" runat="server" Text="Custommer Name :"></asp:Label>
                                </td>
                                <td class="auto-style22" colspan="4">
                                    <asp:Label ID="lblCustName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style84">
                                    <asp:Label ID="lblBeDate" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style82">
                                    <asp:Label ID="Label7" runat="server" Text="Bill No. :"></asp:Label>
                                </td>
                                <td class="auto-style85">
                                    <asp:Label ID="lblBillNo" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style86">
                                    <asp:Label ID="Label8" runat="server" Text="Date :"></asp:Label>
                                    &nbsp;<asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style79">
                                    <asp:Label ID="Label13" runat="server" Text="Payment :"></asp:Label>
                                </td>
                                <td class="auto-style48">
                                    <asp:Label ID="lblPayment" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style45">
                                    </td>
                                <td class="auto-style87"></td>
                            </tr>
                            <tr>
                                <td class="auto-style84">
                                    &nbsp;</td>
                                <td class="auto-style82">
                                    <asp:Label ID="Label14" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td class="auto-style22" colspan="6">
                                    <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style28"></td>
                                <td class="auto-style29">
                                    <asp:Label ID="Label15" runat="server" Text="Address :"></asp:Label>
                                </td>
                                <td class="auto-style30" colspan="6">
                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr >
                                <td class="auto-style22" colspan="8">
                                    <asp:Label ID="lblEnglishBaht" runat="server"></asp:Label>
                                </td>
                            </tr>
                                <tr align ="center">
                                    <td class="auto-style22" colspan="8">
                                        &nbsp;<asp:Button ID="btnShearh" runat="server" Text="Search" />
                                    </td>
                                </tr>
                            <tr>
                                <td class="auto-style30" colspan="8"></td>
                            </tr>
                            <tr>
                                <td class="auto-style22" colspan="8" style="background-image: url('../Images/btt.jpg');" >&nbsp;</td>
                            </tr>
                        </table>
                        <div style="background-color :white;" class="auto-style54">
                            <uc3:CountRow ID="CountRow1" runat="server" />
                        </div>
                        <div style="background-color:#d9f4f7; " class="auto-style76">
                        <asp:GridView ID="GvAddData" runat="server" CellPadding="4" GridLines="None" ForeColor="#333333">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkB" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Wrap="False" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" />
                            </asp:Panel>
                        </div>
                
                        <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Delete Data
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="6"style="background-image: url('http://localhost:8080/Images/btt.jpg');" >
                                    <uc2:HeaderForm ID="HeaderForm2" runat="server" />
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style95">&nbsp;</td>
                                <td class="auto-style93">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style95">&nbsp;</td>
                                <td class="auto-style93">
                                    <asp:Label ID="DletlblBillNo" runat="server" Text="Billing No. :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="DlettxtBill" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style88"></td>
                                <td class="auto-style96"></td>
                                <td class="auto-style94">
                                    <asp:Label ID="DletlblInvNo" runat="server" Text="Invoice No. :"></asp:Label>
                                </td>
                                <td class="auto-style88">
                                    <asp:TextBox ID="DlettxtInv" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                            </tr>
                            <tr>
                                <td class="auto-style88"></td>
                                <td class="auto-style96">
                                    &nbsp;</td>
                                <td class="auto-style94">&nbsp;</td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                            </tr>
                            <tr align ="center">
                                <td colspan="6" >
                                    <asp:Button ID="DletbtnSearch" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style95">&nbsp;</td>
                                <td class="auto-style93">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-image: url('../Images/btt.jpg');" >&nbsp;</td>
                            </tr>
                        </table>
                        <div style="background-color :white;" class="auto-style54">
                            <uc3:CountRow ID="CountRow2" runat="server" />
                        </div>
                        <div style="background-color:#d9f4f7; " class="auto-style76">
                            <asp:GridView ID="GvDelete" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="DletChkb" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                                <asp:Button ID="DletbtnSave" runat="server" Text="Delete" />
                            </asp:Panel>
                            </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Edit Data
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td colspan="6" style="background-image: url('http://localhost:8080/Images/btt.jpg');">
                                    <uc2:HeaderForm ID="HeaderForm3" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style127">&nbsp;</td>
                                <td class="auto-style126">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style118"></td>
                                <td class="auto-style119"></td>
                                <td class="auto-style120">
                                    <asp:Label ID="EdtlblCustomer" runat="server" Text="Customer :"></asp:Label>
                                </td>
                                <td class="auto-style118">
                                    <asp:TextBox ID="EdttxtCust" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style118"></td>
                                <td class="auto-style118"></td>
                            </tr>
                            <tr>
                                <td class="auto-style88"></td>
                                <td class="auto-style128"></td>
                                <td class="auto-style125">
                                    <asp:Label ID="EdtlblMonth" runat="server" Text="Month :"></asp:Label>
                                </td>
                                <td class="auto-style88">
                                    <asp:TextBox ID="EdttxtMonth" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="EdttxtMonth_CalendarExtender" runat="server" 
                                        Enabled="True" Format="MM/yyyy" TargetControlID="EdttxtMonth">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                            </tr>
                            <tr>
                                <td class="auto-style88"></td>
                                <td class="auto-style128"></td>
                                <td class="auto-style125"></td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                                <td class="auto-style88"></td>
                            </tr>
                            <tr align="center">
                                <td colspan="6">
                                    <asp:Button ID="EdtbtnSearch" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td class="auto-style127">&nbsp;</td>
                                <td class="auto-style126">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                            </tr>
                        </table>
                        <div style="background-color :white;" class="auto-style54">
                            <uc3:CountRow ID="CountRow3" runat="server" />
                        </div>
                        <div style="background-color:#d9f4f7; " class="auto-style76">
                            <asp:GridView ID="GvEdit" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Wrap="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                                      ImageUrl="~/Images/Addnew.gif" HeaderText="Select" />
                                </Columns>
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
                            <br />
                            <br />
                            <br />
                            <div style="background-color:#FFF0F5; " class="auto-style117">
                            <table class="nav-justified">
                                <tr>
                                    <td colspan="8" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:Label ID="Edtlblbathedit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style99">
                                        &nbsp;</td>
                                    <td class="auto-style129"></td>
                                    <td class="auto-style101">
                                        <asp:Label ID="Lable22" runat="server" Text="Billing No. :"></asp:Label>
                                    </td>
                                    <td class="auto-style107">
                                        <asp:Label ID="EdtlblBill" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style114">
                                        <asp:Label ID="Label25" runat="server" Text="Custommer ID :"></asp:Label>
                                    </td>
                                    <td class="auto-style99">
                                        <asp:Label ID="EdtlblCustID" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style99"></td>
                                    <td class="auto-style99"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style130">&nbsp;</td>
                                    <td class="auto-style98">
                                        <asp:Label ID="Lable24" runat="server" Text="Date :"></asp:Label>
                                    </td>
                                    <td class="auto-style106">
                                        <asp:Label ID="EdtlblDate" runat="server"></asp:Label>
                                    </td>
                                    <td class="auto-style113">
                                        <asp:Label ID="Label27" runat="server" Text="Custommer Name :"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="EdtCustName" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="auto-style99"></td>
                                    <td class="auto-style129">&nbsp;</td>
                                    <td class="auto-style101">
                                        <asp:Label ID="Label30" runat="server" Text="Modify By :"></asp:Label>
                                    </td>
                                    <td class="auto-style107">
                                        &nbsp;
                                        <asp:DropDownList ID="EdtDDLModfy" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style114">
                                        <asp:Label ID="Label28" runat="server" Text="Date From :"></asp:Label>
                                    </td>
                                    <td class="auto-style99">
                                        <uc1:DateT100 ID="DateT1003" runat="server" />
                                    </td>
                                    <td class="auto-style99"></td>
                                    <td class="auto-style99"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style99"></td>
                                    <td class="auto-style129">&nbsp;</td>
                                    <td class="auto-style101">
                                        <asp:Label ID="Label31" runat="server" Text="PO :"></asp:Label>
                                    </td>
                                    <td class="auto-style107">
                                        <asp:TextBox ID="EdttxtPO" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style114">
                                        <asp:Label ID="Label29" runat="server" Text="Date To :"></asp:Label>
                                    </td>
                                    <td class="auto-style99">
                                        <uc1:DateT100 ID="DateT1004" runat="server" />
                                    </td>
                                    <td class="auto-style99"></td>
                                    <td class="auto-style99"></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style130">&nbsp;</td>
                                    <td class="auto-style98">&nbsp;</td>
                                    <td class="auto-style106"> &nbsp;</td>
                                    <td class="auto-style113">&nbsp;</td>
                                    <td>
                                        <asp:Label ID="Edtlblbillno" runat="server"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="auto-style130">&nbsp;</td>
                                    <td class="auto-style98">&nbsp;</td>
                                    <td class="auto-style106">&nbsp;</td>
                                    <td class="auto-style113">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr align="center">
                                    <td colspan="8">
                                        <asp:Button ID="EdtbtnSearch1" runat="server" Text="Search" />
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="8">&nbsp;</td>
                                </tr>
                                <tr align="center">
                                    <td colspan="8" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                                </tr>
                            </table>
                        <div style="background-color :white;" class="auto-style54">

                            <uc3:CountRow ID="CountRow5" runat="server" />

                        </div>
                            <asp:GridView ID="GvEdit1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="EdtChkb" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Wrap="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center">
                                    <asp:Button ID="EdtbtnSave" runat="server" Text="Save" />
                                </asp:Panel>

                    </div>
                        
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                    <HeaderTemplate>
                        Print Report
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table class="auto-style54">
                            <tr>
                                <td colspan="7" style="background-image: url('http://localhost:8080/Images/btt.jpg');">
                                    <uc2:HeaderForm ID="HeaderForm4" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style135">&nbsp;</td>
                                <td class="auto-style131">&nbsp;</td>
                                <td class="auto-style132">&nbsp;</td>
                                <td class="auto-style123">&nbsp;</td>
                                <td class="auto-style134">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style136"></td>
                                <td class="auto-style137">
                                    <asp:Label ID="Label32" runat="server" Text="Custommer :"></asp:Label>
                                </td>
                                <td class="auto-style138">
                                    <asp:TextBox ID="RepttxtCustID" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style139">
                                    <asp:Label ID="Label33" runat="server" Text="Month :"></asp:Label>
                                </td>
                                <td class="auto-style140">
                                    <asp:TextBox ID="RepttxtMonth" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="RepttxtMonth_CalendarExtender1" runat="server" 
                                        Enabled="True" Format="MM/yyyy" TargetControlID="RepttxtMonth">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="auto-style141">
                                    <asp:Button ID="ReptbtnSearchCust" runat="server" CssClass="col-xs-offset-0" Text="Search" />
                                </td>
                                <td class="auto-style141"></td>
                            </tr>
                            <tr>
                                <td class="auto-style135">&nbsp;</td>
                                <td class="auto-style131">&nbsp;</td>
                                <td class="auto-style132">&nbsp;</td>
                                <td class="auto-style123">
                                    &nbsp;</td>
                                <td class="auto-style134">
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style135">&nbsp;</td>
                                <td class="auto-style131">
                                    <asp:Label ID="Label34" runat="server" Text="Bill BY :"></asp:Label>
                                </td>
                                <td class="auto-style132">
                                    <asp:DropDownList ID="ReptddlBillBy" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style123">
                                    <asp:Button ID="ReptbtnSearchBiilBy" runat="server" Text="Search" />
                                </td>
                                <td class="auto-style134">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr align="center">
                                <td colspan="7" class="auto-style122">
                                    <asp:Label ID="ReptlblBillShow" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style135">&nbsp;</td>
                                <td class="auto-style131">&nbsp;</td>
                                <td class="auto-style132">&nbsp;</td>
                                <td class="auto-style123">&nbsp;</td>
                                <td class="auto-style134">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="7" style="background-image: url('../Images/btt.jpg');">&nbsp;</td>
                            </tr>
                        </table>
                        <div style="background-color :white;" class="auto-style54">

                            <uc3:CountRow ID="CountRow4" runat="server" />

                        </div>
                        <div style="background-color:#d9f4f7; " class="auto-style76">
                            <asp:GridView ID="GvReprot" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Wrap="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                <Columns>
                                  <asp:ButtonField ButtonType="Image" CommandName="OnClick" 
                                      ImageUrl="~/Images/icon_print.png" HeaderText="Print" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
