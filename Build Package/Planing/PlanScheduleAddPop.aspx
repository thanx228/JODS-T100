<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanScheduleAddPop.aspx.vb" Inherits="MIS_T100.PlanScheduleAddPop" %>

<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>

<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            
        }
        .style3
        {
            width: 199px;
        }
        .style4
        {
            width: 137px;
        }
        .style5
        {
            width: 178px;
        }
        </style>
</head>
<script src="../Scripts/jsScript.js" type="text/javascript"></script>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMO" runat="server" Visible="false" ></asp:Label>
        <asp:Label ID="lblProItem" runat="server" Visible="false" ></asp:Label>
        <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
        <table style="width:95%;">
            <tr>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="Label2" runat="server" Text="MO DocNo."></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    Production ItemNo</td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    ProductionItemName</td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="Label3" runat="server" Text="SPEC"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="Label4" runat="server" Text="Production Qty"></asp:Label>
                </td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    Complete Qty</td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    Scarp Qty</td>
                <td align="center" 
                    style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="Label5" runat="server" Text="Customer"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbProductionItemNo" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbProductionItemNoName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbMoQty" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbMoCompleteQty" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbMoScarpQty" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbCust" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <uc2:CountRow ID="ucCountRow1" runat="server" />
<%--        <asp:GridView ID="gvMO" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px" AutoGenerateColumns="False">--%>
            <asp:GridView ID="gvMO" runat="server" BackColor="White" AutoGenerateColumns="False" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                     AllowPaging="True" AllowSorting="True" PageSize="50" PagerSettings-FirstPageText="First"
                     PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                Height="16px">
            <Columns>
<%--                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="sfbaseq" HeaderText="Item Seq." >
                  <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="sfba005" HeaderText="BOM_Item" />
                <asp:BoundField DataField="" HeaderText=" ItemName" />
                <asp:BoundField DataField="" HeaderText="Specifaction" />
                <asp:BoundField DataField="" DataFormatString="{0:N3}" 
                    HeaderText="StandardIssueQty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField=""  
                    HeaderText="Issued Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField=""  
                    HeaderText="UnIssuedQty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sfba014" HeaderText="Unit" />
                <asp:BoundField DataField="" HeaderText="Issue To Warehouse" />
                <asp:BoundField DataField=""  
                    HeaderText="Stock Qty">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="" HeaderText="Stock In Warehouse" />
            </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" Mode="NumericFirstLast" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    
        <uc2:CountRow ID="ucCountRow2" runat="server" />
        <asp:Label ID="lblStartDate" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblEndDate" runat="server" Visible="false" ></asp:Label>
        <asp:GridView ID="gvOperation" runat="server" 
            Height="16px" AutoGenerateColumns="true">
        </asp:GridView>
    
        <uc2:CountRow ID="ucCountRow3" runat="server" />
        <asp:GridView ID="gvStatus" runat="server" 
            Height="16px" AutoGenerateColumns="False">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
