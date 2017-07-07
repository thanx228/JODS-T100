<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" 
   EnableEventValidation = "false" CodeBehind="RetrurnReworkScrap.aspx.vb" Inherits="MIS_T100.RetrurnReworkScrap" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingWorkstation.ascx" TagPrefix="uc1" TagName="UsingWorkstation" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
        }
        .style10
        {
            height: 30px;
        }
        .style11
        {
            width: 126px;
        }
        .style1
    {
        width: 282px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblsql" runat="server" ></asp:Label>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingWorkstation runat="server" ID="UsingWorkstation" />  
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />  
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Production ItemNo."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Specifaction"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Plan Start Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="txtFormDate" />
                      <%--  <uc1:Date runat="server" ID="fromDate" />--%>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label11" runat="server" Text="Plan Start Date To"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="txtToDate" />
                      <%--  <uc1:Date runat="server" ID="ToDate" />--%>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Height="30px" 
                            Text="Export Excel" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />

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
                //freezesize: 7,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 2,
                railsize: 16,
                barsize: 14
            });
        }
   </script>
       <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False"  AllowPaging="True" AllowSorting="True" PageSize="50"
            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                     <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="xmda004" HeaderText="Customer" />
                    <asp:BoundField DataField="xmdadocno" HeaderText="SaleOrderNo." />
                    <asp:BoundField DataField="xmdcseq" HeaderText="SaleSeq." >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfia003" HeaderText="MO-DocNo" />
                    <asp:BoundField DataField="sfaa010" HeaderText="ProductionItemNo" />
                    <asp:BoundField DataField="imaal004" HeaderText="Specifaction" />
                    <asp:BoundField DataField="sfia008" HeaderText="RuncardNo" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca005" HeaderText="RuncardDetail" />
                    <asp:BoundField DataField="sfaa012" HeaderText="Production Qty" DataFormatString="{0:N3}" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField DataField="sfcb044" HeaderText="PlanStartDate" DataFormatString="{0:yyyy/MM/dd}" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                     <asp:BoundField DataField="sfcb045" HeaderText="PlanComplete" DataFormatString="{0:yyyy/MM/dd}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfiadocno" HeaderText="Return-DocNo" />
                    <asp:BoundField DataField="sfcb002" HeaderText="Seq." >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfia005" HeaderText="OP_Id." >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Description" />
                    <asp:BoundField DataField="sfcb011" HeaderText="Workstation" >
                    </asp:BoundField>
                    <asp:BoundField DataField="sfia007" HeaderText="Rework Qty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfibseq" HeaderText="To.Seq" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfib002" HeaderText="To.OP_Seq" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfib001" HeaderText="To.OP_Id" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="To.Description" />
                    <asp:BoundField DataField="sfib009" HeaderText="To.Workstation" >
                    </asp:BoundField>
                    <asp:BoundField DataField="sfib005" HeaderText="Prev.Operation" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Prev.Description" />
                  
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" Mode="NextPrevious" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="False" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
          <%--  <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="F01" HeaderText="W/C" />
                    <asp:BoundField DataField="F02" HeaderText="Cust" />
                    <asp:BoundField DataField="F03" HeaderText="Cust Name" />
                    <asp:BoundField DataField="F04" HeaderText="S/O" />
                    <asp:BoundField DataField="F05" HeaderText="S/O Seq" />
                    <asp:BoundField DataField="F06" HeaderText="M/O" />
                    <asp:BoundField DataField="F07" HeaderText="M/O Seq" />
                    <asp:BoundField DataField="F08" HeaderText="Transfer No." />
                    <asp:BoundField DataField="F09" HeaderText="Item Spec" />
                    <asp:BoundField DataField="F10" DataFormatString="{0:N2}" HeaderText="Plan Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F11" DataFormatString="{0:N2}" HeaderText="Rework">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="F12" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="F13" HeaderText="Next W/C" />
                    <asp:BoundField DataField="F14" HeaderText="Item Code" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" 
                    Wrap="False" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
