<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master"
    EnableEventValidation="false" CodeBehind="WorkCenterStatus.aspx.vb" Inherits="MIS_T100.WorkCenterStatus" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/Normal/UsingWorkstation.ascx" TagPrefix="uc1" TagName="UsingWorkstation" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style10
        {
        
        }
        .style11
        {
            width: 126px;
        }
        .style14
        {
            width: 148px;
        }
        .style16
        {
            width: 162px;
        }
        .style17
        {
            width: 199px;
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
    <asp:Label ID="lblSql" runat="server" ></asp:Label>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table  style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <uc1:UsingWorkstation runat="server" ID="UsingWorkstation" />
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label17" runat="server" Text="Sale No"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label18" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleSeq" runat="server" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label10" runat="server" Text="MO PlanStart FromDate"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                 <%--       <asp:TextBox ID="tbDateFrom" runat="server" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>--%>
                        <uc1:DateT100 runat="server" ID="DateFrom" />
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label11" runat="server" Text="MO PlanStart ToDate"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                      <%--  <asp:TextBox ID="tbDateTo" runat="server" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>--%>
                        <uc1:DateT100 runat="server" ID="DateTo" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style14">
                        <asp:Label ID="Label13" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="style17">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="C,M,F">All</asp:ListItem>
                            <asp:ListItem Selected="True" Value="F">Not Finished</asp:ListItem>
                            <asp:ListItem Value="C,M">Closed</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="style16">
                        <asp:Label ID="Label15" runat="server" Text="Print For"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlFor" runat="server">
                            <asp:ListItem Value="2">Audit</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Check</asp:ListItem>
                        </asp:DropDownList>
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
                        &nbsp;
                        <asp:Button ID="btLabel" runat="server" Text="Print Label" />
                        &nbsp;
                        <asp:Button ID="btList" runat="server" Text="Print List" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
           <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" PageSize="50"
            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next">
                <Columns>
                    <asp:TemplateField HeaderText="MO Deatil">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplMO" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                   <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                     <asp:Label ID="lblNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="xmdadocno" HeaderText="SaleOrderNo." />
                    <asp:BoundField DataField="xmda004" HeaderText="Cust ID" />
                   <%-- <asp:BoundField DataField="" HeaderText="Customer" />--%>
                    <asp:BoundField DataField="sfaadocno" HeaderText="MO DocNo." />
                    <asp:BoundField DataField="sfaa010" HeaderText="Production ItemNo." />
                    <asp:BoundField DataField="imaal004" HeaderText="Specifaction" />
                     <asp:BoundField DataField="sfcb044" HeaderText="PlanStart" DataFormatString="{0:yyyy/MM/dd}" />
                     <asp:BoundField DataField="sfcb045" HeaderText="PlanComplete" DataFormatString="{0:yyyy/MM/dd}" />
                     <asp:BoundField DataField="sfaa012" HeaderText="Production Qty" DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField DataField="sfca004" HeaderText="CompletedQty Qty" DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField DataField="sfaa056" HeaderText="Scarp Qty" DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca001" HeaderText="RunCardNo." >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca005" HeaderText="RunCardDeatil." />
                    <asp:TemplateField HeaderText="ItemSeq.">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItemSeq" runat="server" Text='<%#Eval("sfcb002")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:BoundField DataField="sfcb002" HeaderText="ItemSeq." >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="sfcb003" HeaderText="Op_Id." >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Description" />
                    <asp:BoundField DataField="sfcb011" HeaderText="Workstation" >
                     <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Name" />
                    <asp:BoundField DataField="sfcb027" HeaderText="StardardOutput"  DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb028" HeaderText="GoodTransfer-In"  DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                     <asp:BoundField DataField="sfcb050" HeaderText="WIP"  DataFormatString="{0:#,#}" >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb033" HeaderText="GoodTransfer-Out" DataFormatString="{0:#,#}"  >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb029" HeaderText="ReworkTrs-In" DataFormatString="{0:#,#}"  />
                    <asp:BoundField DataField="sfcb034" HeaderText="TransferOutforRework" DataFormatString="{0:#,#}"  >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb036" HeaderText="DirectScarp" DataFormatString="{0:#,#}"  >
                     <ItemStyle HorizontalAlign="right" />
                    </asp:BoundField>
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
 <%--           <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="MO">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplMO" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ZZ" HeaderText="Last Transfer(Date Trans No)" />
                    <asp:BoundField DataField="A" HeaderText="WC" />
                    <asp:BoundField DataField="B" HeaderText="Process Name" />
                    <asp:BoundField DataField="C" HeaderText="Customer" />
                    <asp:BoundField DataField="D" HeaderText="Customer Name" />
                    <asp:BoundField DataField="E" HeaderText="SO" />
                    <asp:BoundField DataField="F" HeaderText="MO" />
                    <asp:BoundField DataField="H" HeaderText="Item Spec" />
                    <asp:BoundField DataField="I" HeaderText="Plan Start Date" />
                    <asp:BoundField DataField="J" DataFormatString="{0:#,#}" HeaderText="Plan Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="K" DataFormatString="{0:#,#}" HeaderText="WIP Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="L" DataFormatString="{0:#,#}" 
                        HeaderText="Reciept Qty(+)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="M" DataFormatString="{0:#,#}" 
                        HeaderText="Finish Qty(-)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="N" DataFormatString="{0:#,#}" 
                        HeaderText="Return Qty(+)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="O" DataFormatString="{0:#,#}" 
                        HeaderText="Finish Return Qty(-)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="P" DataFormatString="{0:#,#}" 
                        HeaderText="Scrap Qty(-)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Q" HeaderText="Plan Schedule Date" />
                    <asp:BoundField DataField="R" HeaderText="Status" />
                    <asp:BoundField DataField="S" HeaderText="Item Code" />
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
