<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="CustomsNew.aspx.vb" Inherits="MIS_T100.CustomsNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/HeaderForm.ascx" TagPrefix="uc1" TagName="HeaderForm" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="DateT100" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
          .auto-style11 {
              width: 114px;
          }
          .auto-style12 {
              width: 90px;
          }
          .auto-style13 {
              width: 152px;
          }
          .auto-style14 {
              width: 163px;
          }
          .auto-style15 {
              width: 251px;
          }
          .auto-style16 {
              width: 251px;
              height: 27px;
          }
          .auto-style17 {
              width: 114px;
              height: 27px;
          }
          .auto-style18 {
              width: 152px;
              height: 27px;
          }
          .auto-style19 {
              width: 90px;
              height: 27px;
          }
          .auto-style20 {
              width: 163px;
              height: 27px;
          }
          .auto-style21 {
              height: 27px;
          }
         
          .auto-style23 {
              height: 21px;
          }
          </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" />
        <script type="text/javascript">
        $(document).ready(function () {
            GvInvNoScrollbar();
            GvATMScrollbar();
        });
        function GvInvNoScrollbar() {
            gridView1 = $('#<%= GvInvNo.ClientID %>').gridviewScroll({
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

        function GvATMScrollbar() {
            gridView1 = $('#<%= GvATM.ClientID %>').gridviewScroll({
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
        </script>


      </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%; background-color :white;">
                <tr>
                    <td colspan="6" style="background-image: url('../Images/btt.jpg');">
                        <uc1:HeaderForm ID="HeaderForm1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="auto-style23">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style16">&nbsp;</td>
                    <td class="auto-style17">
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer :"></asp:Label>
                    </td>
                    <td class="auto-style18">
                        <asp:TextBox ID="tbCust" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style19">
                        <asp:Label ID="lblDateFrom" runat="server" Text="Date From :"></asp:Label>
                    </td>
                    <td class="auto-style20">
                        <uc2:DateT100 ID="DateT1001" runat="server" />
                    </td>
                    <td class="auto-style21"></td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblInvoice" runat="server" Text="Invoice Type :"></asp:Label>
                    </td>
                    <td class="auto-style13">
                        &nbsp;
                        <asp:DropDownList ID="ddlInvType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style12">
                        <asp:Label ID="lblDateTo" runat="server" Text="Date To :"></asp:Label>
                    </td>
                    <td class="auto-style14">

                        <uc2:DateT100 ID="DateT1002" runat="server" />

                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style15">&nbsp;</td>
                    <td class="auto-style11">
                        <asp:Label ID="lblRemarkType" runat="server" Text="Remark Type :"></asp:Label>
                    </td>
                    <td class="auto-style13">
                        &nbsp;
                        <asp:DropDownList ID="ddlRarkType" runat="server">
                            <asp:ListItem Selected="True">Computer</asp:ListItem>
                            <asp:ListItem>Appliances</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style12">
                        &nbsp;</td>
                    <td class="auto-style14">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr align="center">
                    <td colspan="6" style="background-image: url('../Images/btt.jpg');" >
                        <asp:Button ID="btnSearch" runat="server" Text="Search" />

                    </td>

                </tr>
                <tr>
                    <td  colspan="6" >      
                        <uc1:CountRow ID="CountRow1" runat="server" />
         
                    </td>
                </tr>
            </table>
        <div style="background-color:#d9f4f7; width:100%; ">
                <asp:GridView ID="GvInvNo" runat="server" CellPadding="4" GridLines="None"  ForeColor="#333333" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkbSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" Wrap="False" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>  
            <br />                   
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                <asp:Button ID="btnSelectATMUS" runat="server" Text="Select ATM US" />
                &nbsp; <asp:Button ID="btnSelectATMTH" runat="server" Text="Select ATM TH" />
            </asp:Panel>
     </div>
            <div style="width:100%; background-color :white;">

                    <uc1:CountRow ID="CountRow2" runat="server" />

                    </div>
            <div style="background-color:#e4d8f3; width:100%;">           
                <asp:GridView ID="GvATM" runat="server" CellPadding="4" GridLines="None" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
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
                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center">
                    <asp:Button ID="btnPrintReport" runat="server" Text="Print Report" />
                    &nbsp;&nbsp;<asp:Button ID="btnPrintDelta" runat="server" Text="Print Delta" />
                    &nbsp;&nbsp;<asp:Button ID="btnPrintChinI" runat="server" Text="Print Chin i" />
                    &nbsp;&nbsp;<asp:Button ID="btnPrintFisher" runat="server" Text="Print Fisher" />
                    &nbsp;&nbsp;<asp:Button ID="btnExcel" runat="server" Text="Excel" />
                </asp:Panel>                                    
               <asp:TextBox runat="server" ID="NowPageIndex" style="display:none;" Text="0"></asp:TextBox>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

