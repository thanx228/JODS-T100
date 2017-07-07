<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="planIssueMatPopup.aspx.vb" Inherits="MIS_T100.planIssueMatPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Plan Request Stock</title>
    <style type="text/css">
        .style1
        {
            width: 37px;
        }
        .style4
        {
            width: 468px;
        }
        .style10
        {
            width: 579px;
        }
        .style11
        {
            width: 563px;
        }
        .style12
        {
            width: 651px;
        }
        .auto-style1 {
            width: 468px;
            height: 27px;
        }
        .auto-style2 {
            width: 651px;
            height: 27px;
        }
        .auto-style3 {
            width: 563px;
            height: 27px;
        }
        .auto-style4 {
            width: 579px;
            height: 27px;
        }
        .ShowTitleBom{
            border: thin solid #00CCFF; 
            background-color: #808080;
            box-shadow:5px 5px 5px 5px ;
            box-shadow: 0 0 50px  #808080 inset; 
        }
    </style>
     <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvIssue.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 500,
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
                barsize: 14
            });
        }
   </script>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:69%;">
            <tr>
                <td align="center" 
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Text="Material Shortage Detail" 
                        Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
<div >
        <table style="width: 65%;" >
            <tr>
                <td align="center" class="ShowTitleBom" >
                    BOM_Item</td>
                <td align="center" class="ShowTitleBom">
                    BOM_ItemName</td>
                <td align="center" class="ShowTitleBom">
                    <asp:Label ID="Label3" runat="server" Text="BOM_Specifiation"></asp:Label>
                </td>
                <td align="center" class="ShowTitleBom">
                    <asp:Label ID="Label4" runat="server" Text="Std_Issuance_Qty"></asp:Label>
                </td>
                <td align="center" class="ShowTitleBom">
                    <asp:Label ID="Label5" runat="server" Text="On Stock"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lblpBOM_Item" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbItemName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="left" style="border: thin solid #00CCFF; background-color: #FFFFFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbNotDel" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="right" style="border: thin solid #00CCFF; background-color: #FFFFFF" >
                    <asp:Label ID="lbStock" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
</div>
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label10" runat="server" Text="จำนวนรายการ Mat Issue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountIssue" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbl5555" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvIssue" runat="server" BackColor="White" AutoGenerateColumns="False" Width="2100px" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px">
            <Columns>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplLinkMO" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="sfbadocno" HeaderText="MO_DocNo."  />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status."  />
                    <asp:BoundField DataField="sfaa020" HeaderText="Plan_DueDate" DataFormatString="{0:yyyy/MM/dd}"  />
                 <asp:BoundField DataField="sfba001" HeaderText="Production_Item"  />
                <asp:TemplateField HeaderText="Item_Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_ItemName" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specifiation">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_Spec" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="sfca001" HeaderText="Runcard"  >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sfca005" HeaderText="RuncardDetail"  />
                <asp:TemplateField HeaderText="Production Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_Qty" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mat Request">
                    <ItemTemplate>
                        <asp:Label ID="lblMat_Request" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WIP">
                    <ItemTemplate>
                        <asp:Label ID="lblWIP" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GoodTransfer-In">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodTransferIn" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GoodTransfer-Out">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodTransferOut" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label15" runat="server" Text="จำนวนรายการ PO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label16" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC"  AutoGenerateColumns="False"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px">
            <Columns>
                    <asp:BoundField DataField="pmdndocno" HeaderText="PO_No."  />
                    <asp:BoundField DataField="pmdn023" HeaderText="Vender_No."  />
                    <asp:BoundField DataField="pmdn012" HeaderText="shipping_Date" DataFormatString="{0:yyyy/MM/dd}"  />
                    <asp:BoundField DataField="pmdn007" HeaderText="Purchase_Qty" DataFormatString="{0:N3}"  >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pmdn010" HeaderText="Unit"  />
                    <asp:BoundField DataField="pmdn013" HeaderText="Arrival Date" DataFormatString="{0:yyyy/MM/dd}"  />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label17" runat="server" Text="จำนวนรายการ Purchase Request"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label18" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPR" runat="server" BackColor="White" BorderColor="#3366CC" ShowHeaderWhenEmpty="True" AutoGenerateColumns="false" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px" EmptyDataText="No Data Found">
            <Columns>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplLinkMO" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="pmdbdocno" HeaderText="Purchase Request_No."  />
            <%--        <asp:BoundField DataField="" HeaderText="Vender_No."  />
                    <asp:BoundField DataField="" HeaderText="shipping_Date" DataFormatString="{0:MM/dd/yyyy}"  />
                    <asp:BoundField DataField="" HeaderText="Purchase_Qty"  />
                    <asp:BoundField DataField="" HeaderText="Unit"  />
                    <asp:BoundField DataField="" HeaderText="Arrival Date" DataFormatString="{0:MM/dd/yyyy}"  />--%>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
          <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ Purchase Receipt"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lblPR_Receipt" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" class="style1" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label7" runat="server" Text="รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPR_Receipt" runat="server" BackColor="White" BorderColor="#3366CC" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px" EmptyDataText="No Records Found">
            <Columns>
                    <asp:BoundField DataField="pmdtdocno" HeaderText="Purchase Receipt_No."  />
                     <asp:TemplateField HeaderText="Stock in Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStock_In_Date" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                   <asp:BoundField DataField="pmdtdocno" HeaderText="Receipt DocNo."   />
                   <asp:BoundField DataField="pmdt001" HeaderText="PO_No."   />   
                   <asp:BoundField DataField="pmdt020" HeaderText="Receipt_in_Qty" DataFormatString="{0:N3}"  />               
                   <asp:BoundField DataField="pmdt055" HeaderText="Reject Qty" DataFormatString="{0:N3}"  />
                    <asp:BoundField DataField="pmdt019" HeaderText="Unit"   />
                    <asp:BoundField DataField="pmdt016" HeaderText="Store_Location"   >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
