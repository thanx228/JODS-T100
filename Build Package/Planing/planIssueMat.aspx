<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation="false"
     MasterPageFile="~/Styles/MIS.Master" CodeBehind="planIssueMat.aspx.vb" Inherits="MIS_T100.planIssueMat" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/HeaderForm.ascx" TagPrefix="uc1" TagName="HeaderForm" %>
<%@ Register Src="~/UserControl/CountRow.ascx" TagPrefix="uc1" TagName="CountRow" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc2" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .style3
        {
            width: 110px;
        }
        .style4
        {
           width:100%;
           background-color: #FFFFFF;
        }
        .style5
        {
            width: 139px;
        }
        .style6
        {
            width: 139px;
        }
        .style7
        {
            height: 29px;
        }
        .style8
        {
            width: 40px;
            height: 38px;
        }
        .style9
        {
            height: 24px;
        }
        .style19
        {
           
        }
        .style20
        {
            
        }
        .style22
        {
            height: 42px;
        }
        .style23
        {
            height: 38px;
        }
         .auto-style3 {
             height: 24px;
             width: 212px;
         }
         .auto-style4 {
             width: 75%;
             background-color: #FFFFFF;
         }
         .auto-style5 {
             width: 98px;
         }
         .auto-style6 {
             width: 190px;
         }
         .auto-style7 {
             width: 116px;
         }
         .auto-style9 {
             width: 158px;
         }
         .auto-style11 {
             width: 37px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblSql" runat="server" ></asp:Label>
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" ForeColor="#3333FF" 
                            Text="Plan Request (Stock)" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
      
                <table class="auto-style4">
                    <tr>
                        <td class="auto-style5">
                            <asp:Label ID="Label13" runat="server" Text="SO Type"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <uc2:UsingDocTypeSale ID="UsingDocTypeSale" runat="server" />
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label2" runat="server" Text="MO Type"></asp:Label>
                        </td>
                        <td>
                            <uc1:UsingMO_Type ID="UsingMO_Type" runat="server" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:Label ID="Label15" runat="server" Text="SO No"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="tbSoNo" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label3" runat="server" Text="MO No. From"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbMoFrom" runat="server" Width="130px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:Label ID="Label14" runat="server" Text="SO Seq"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="tbSoSeq" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label12" runat="server" Text="MO No To"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbMoTo" runat="server" Width="130px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:Label ID="Label4" runat="server" Text="BOM_Item"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label5" runat="server" Text="Spec"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:Label ID="Label17" runat="server" Text="Customer"></asp:Label>
                        </td>
                        <td class="auto-style6">
                            <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td class="auto-style7">
                            <asp:Label ID="Label10" runat="server" Text="Code Type"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCodecheck" runat="server" ></asp:Label>
                            <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" style="margin-left: 0px; margin-right: 0px" Width="250px">
                                <asp:ListItem Value="1">Materials</asp:ListItem>
                                <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                                <asp:ListItem Value="3">Semi FG</asp:ListItem>
                                <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table> 
            </div>

            <table class="auto-style4">
                <tr>
                    <td class="auto-style9">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="0">--Condition Date--</asp:ListItem>
                            <asp:ListItem Value="1">Plan MO Start</asp:ListItem>
                            <asp:ListItem Value="2">Mat Issue Date</asp:ListItem>
                            <asp:ListItem Value="3">MO Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style11">
                        <asp:Label ID="lblTitleDateFrom" runat="server" Text="From"></asp:Label>
                    </td>
                    <td>
                        <uc1:DateT100 runat="server" ID="FormDate" />
                    </td>
                    <td>
                        <asp:Label ID="lblTitleDateTo" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                        <uc1:DateT100 runat="server" ID="EndDate" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <table style="">
                <tr>
                    <td class="style9" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td class="auto-style3" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlCondition" runat="server" Width="152px">
                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Stock &gt;= Issue Req</asp:ListItem>
                            <asp:ListItem Value="2">Issue Req Over Stock</asp:ListItem>
                            <asp:ListItem Value="3">Issue Req =0 ,Stock&gt;0</asp:ListItem>
                            <asp:ListItem Value="4">Stock=0,Issue Req&gt;0</asp:ListItem>
                            <asp:ListItem Value="5">Stock &lt; Issue Req</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style9" style="background-color: #FFFFFF">
                        </td>
                    <td class="style6" style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" ForeColor="Red" 
                            Text="Stock=Stock+Support"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        class="style7">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportGrid" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow runat="server" ID="ucCount" />
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                //width: 1024,
                height: 450,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                //freezesize: 0,
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
            <asp:GridView ID="gvShow" runat="server" BackColor="White" AutoGenerateColumns="False" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                     AllowPaging="True" AllowSorting="True" PageSize="10" PagerSettings-FirstPageText="First"
                     PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                Height="16px">
                <Columns>
                   <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
<%--                    <asp:BoundField DataField="sfbadocno" HeaderText="MO_DocNo." Visible="false"  />
                    <asp:BoundField DataField="sfaastus"  HeaderText="Status." Visible="false" />
                     <asp:BoundField DataField="sfba001" HeaderText="Master_Item_No" Visible="false" />--%>
                    <asp:BoundField DataField="sfba005"  HeaderText="BOM_Item" />
                     <asp:TemplateField HeaderText="BOM_ItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblBOM_ItemName" runat="server"  ></asp:Label>                          
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="BOM_Specifiation">
                        <ItemTemplate>
                            <asp:Label ID="lblBOM_Specifiation" runat="server"  ></asp:Label>                          
                        </ItemTemplate>
                    </asp:TemplateField>
          <%--          <asp:BoundField DataField="imaal003"  HeaderText="BOM_ItemName" />
                    <asp:BoundField DataField="imaal004" HeaderText="BOM_Specifiation" />--%>
                   <asp:TemplateField HeaderText="Std_Issuance_Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblStd_Issuance_Qty" runat="server"  ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Issue_Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblIssue_Qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="UnIssued Qty.">
                        <ItemTemplate>
                         <asp:Label ID="lblMO_qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="IssueOver Qty.">
                        <ItemTemplate>
                         <asp:Label ID="lblIssueOver_Qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Stock">
                        <ItemTemplate>
                            <asp:Label ID="lblStockInQty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="PO Qty">
                        <ItemTemplate>
                         <asp:Label ID="lblPO_qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="IQC Source Qty">
                        <ItemTemplate>
                         <asp:Label ID="lblIQC_Source_Qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="IQC Inspection Qty">
                        <ItemTemplate>
                         <asp:Label ID="lblIQC_Inspection_Qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="IQC_Qty_Passed">
                        <ItemTemplate>
                         <asp:Label ID="lblIQC_Qty_Passed" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="IQC Qty_Defected">
                        <ItemTemplate>
                         <asp:Label ID="lblIQC_Qty_Defected" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="PR_Receipt Qty">
                        <ItemTemplate>
                         <asp:Label ID="lblPR_qty" runat="server" ></asp:Label>                          
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
<%--                      <asp:BoundField DataField="sfba014" HeaderText="Unit" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportGrid" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
