<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PCConfSO.aspx.vb" Inherits="MIS_T100.PCConfSO" Debug = "true"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Multiple/UsingDocTypeSaleCheckList.ascx" TagPrefix="uc1" TagName="UsingDocTypeSaleCheckList" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>
<%--<%@ Register Src="~/UserControl/Date.ascx" TagPrefix="uc1" TagName="Date" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            width: 159px;
        }
        .style8
        {
            width: 155px;
        }
        .style18
        {
            width: 220px;
            height: 21px;
        }
        .style10
        {
            width: 148px;
        }
        .style9
        {
            width: 220px;
        }
        .style24
        {
            height: 21px;
            width: 105px;
        }
        .style25
        {
            width: 105px;
        }
        .style1
    {
        width: 282px;
    }
        </style>
<%--    <script src="../Scripts/jsMaktxt/jquery-1.5.js"></script>
    <script src="../Scripts/jsMaktxt/jquery.maskedinput-1.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[type=text][id*=tbSaleNo]").mask("JP9999-99999999999");
    });
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <table style="width:75%;">
                        <tr>
                            <td bgcolor="White" class="style7" style="vertical-align: top">
                                <asp:Label ID="Label4" runat="server" Text="SO Type"></asp:Label>
                            </td>
                            <td bgcolor="White" colspan="3">
                                <uc1:UsingDocTypeSaleCheckList runat="server" ID="UsingDocTypeSaleCheckList" />
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label15" runat="server" Text="Cust ID"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td bgcolor="White" class="style8">
                                PUR Confirm Date</td>
                            <td bgcolor="White">
                                <asp:CheckBox ID="cbPURYes" runat="server" Text="Yes" />
                                <asp:CheckBox ID="cbPURNo" runat="server" Text="No" />
                                </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label5" runat="server" Text="SO NO-Seq"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                JP<asp:TextBox ID="tbSaleNo" runat="server" ></asp:TextBox>
                                &nbsp;-&nbsp;
                                <asp:TextBox ID="tbSOSeq" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td bgcolor="White" class="style8">
                                <asp:Label ID="Label6" runat="server" Text="PC Confirm Date"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:CheckBox ID="cbPCYes" runat="server" Text="Yes" />
                                <asp:CheckBox ID="cbPCNo" runat="server" Text="No" />
                                </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label14" runat="server" Text="Condition"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:CheckBox ID="cbApp" runat="server" Text="Not Approved" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="cbNoBOM" runat="server" Text="NO BOM" />
                            </td>
                            <td bgcolor="White">
                                Sale Confirm</td>
                            <td bgcolor="White">
                                <asp:CheckBox ID="cbReject" runat="server" Text="Reject" />
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                Close Status</td>
                            <td bgcolor="White">
                                <asp:DropDownList ID="ddlClose" runat="server">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="N">Not Close</asp:ListItem>
                                    <asp:ListItem Value="Y">Close</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td bgcolor="White">
                                </td>
                            <td bgcolor="White">
                                
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label16" runat="server" Text="Property"></asp:Label>
                            </td>
                            <td bgcolor="White" colspan="3">
                                <asp:CheckBoxList ID="cblProperty" runat="server" RepeatColumns="3" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="A">A:COMBINED/PROCESSED PRODUCT</asp:ListItem>
                                    <asp:ListItem Value="E">E:COST/SOFTWARE</asp:ListItem>
                                    <asp:ListItem Value="F">F:OFFICE SUPPLIES</asp:ListItem>
                                    <asp:ListItem Value="M">M:MATERIAL/PART/PRODUCT</asp:ListItem>
                                    <asp:ListItem Value="T">T:TEMPLATE</asp:ListItem>
                                    <asp:ListItem Value="X">X:VIRTUAL PRODUCTS</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label7" runat="server" Text="Item"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                            </td>
                            <td bgcolor="White" class="style8">
                                <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                <asp:Label ID="Label9" runat="server" Text="Plan Delivery Date From"></asp:Label>
                            </td>
                            <td bgcolor="White">
<%--                                <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateFrom">
                                </asp:CalendarExtender>--%>
                                <uc1:DateT100 runat="server" ID="tbFromDelivDate" />
                            </td>
                            <td bgcolor="White" class="style8">
                                <asp:Label ID="Label10" runat="server" Text="Plan Delivery Date To"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                 <uc1:DateT100 runat="server" ID="tbToDelivDate" />
                          <%--      <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbDateTo">
                                </asp:CalendarExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White" class="style7">
                                SO Date From</td>
                            <td bgcolor="White">
                                <%--<asp:TextBox ID="tbSODateFrom" runat="server" Width="80px"></asp:TextBox>--%>
                              <%--  <uc1:Date runat="server" ID="tbSODateDate" />--%>
                                <uc1:DateT100 runat="server" ID="txtsFromDate" />
                            </td>
                            <td bgcolor="White" class="style8">
                                SO Date To</td>
                            <td bgcolor="White">
                                <%--<asp:TextBox ID="tbSODateTo" runat="server" Width="80px"></asp:TextBox>--%>
                              <%--  <uc1:Date runat="server" ID="tbSOToDate" />--%>
                                <uc1:DateT100 runat="server" ID="txtsToDate" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%; background-image: url('../Images/btt.jpg');">
                        <tr>
                            <td align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                                &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                                &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table bgcolor="White" style="width: 75%;">
                        <tr>
                            <td class="style8">
                                SO Type-No</td>
                            <td class="style18">
                                <asp:Label ID="lbSO" runat="server" BackColor="#FFCC99" BorderColor="#C4C4C4" 
                                    BorderWidth="1px" Font-Bold="True"></asp:Label>
                                &nbsp;</td>
                            <td class="style24" style="background-color: #FFFFFF">
                                Item</td>
                            <td class="style10">
                                <asp:Label ID="lbItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                Customer</td>
                            <td class="style10">
                                <asp:Label ID="lbCust" runat="server"></asp:Label>
                            </td>
                            <td class="style10">
                                Item Spec</td>
                            <td bgcolor="White">
                                <asp:Label ID="lbSpec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style8">
                                Qty</td>
                            <td class="style18">
                                <asp:Label ID="lbQty" runat="server"></asp:Label>
                            </td>
                            <td class="style24" style="background-color: #FFFFFF">
                                Sale Order Date</td>
                            <td class="style10">
                                <asp:Label ID="lbSODate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Sale Request DueDate</td>
                            <td class="style9">
                                <asp:Label ID="lbSaleReqDate" runat="server"></asp:Label>
                            </td>
                            <td class="style25" style="background-color: #FFFFFF">
                                Plan Delivery Date</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbDelDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                PUR Remark</td>
                            <td class="style9" colspan="3">
                                <asp:Label ID="lbRemark" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                PUR Confirm Date</td>
                            <td class="style9">
                                <asp:Label ID="lbPURCon" runat="server"></asp:Label>
                            </td>
                            <td class="style25" style="background-color: #FFFFFF">
                                PUR Confirm Date 1</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbPURCon1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                Sale Confirm Date</td>
                            <td class="style9">
                                <asp:Label ID="lbSaleCon" runat="server"></asp:Label>
                            </td>
                            <td class="style25" style="background-color: #FFFFFF">
                                &nbsp;</td>
                            <td style="background-color: #FFFFFF">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style7">
                                PC Confirm Date</td>
                            <td class="style9">
                                <asp:Label ID="lbPCCon" runat="server"></asp:Label>
                            </td>
                            <td class="style25" style="background-color: #FFFFFF">
                                &nbsp;</td>
                            <td style="background-color: #FFFFFF">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style7">
                                PC Confirm Date</td>
                            <td class="style9">
                    <%--            <asp:TextBox ID="tbPCDate" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender ID="tbPCDate_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tbPCDate">
                                </asp:CalendarExtender>--%>
                                <uc1:DateT100 runat="server" ID="txtPCDate" />
                            </td>
                            <td class="style25" style="background-color: #FFFFFF">
                                &nbsp;</td>
                            <td style="background-color: #FFFFFF">
                                <asp:Label ID="lbDateRec" runat="server" BackColor="#FFCC99" 
                                    BorderColor="#C4C4C4" BorderWidth="1px" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%; background-image: url('../Images/btt.jpg');">
                        <tr>
                            <td align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                                <asp:Button ID="btSave" runat="server" Text="Save" />
                                &nbsp;<asp:Button ID="btCancel" runat="server" Text="Cancel" />
                                &nbsp;&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </asp:View>
                <br />
            </asp:MultiView>
            <uc1:CountRow ID="CountRow1" runat="server" />
              <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" Width="3200"
                AllowPaging="True" PagerSettings-FirstPageText="First" PageSize="50" 
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom">
                <Columns>
                      <asp:ButtonField ButtonType="Image" CommandName="OnEdit" HeaderText="Edit" 
                        ImageUrl="~/Images/edit.gif" Text="Edit" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                    <asp:HyperLink ID="hplShow" runat="server" Target="_blank" ImageUrl="~/Images/imagesview.jpg" HorizontalAlign="Center"></asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BOM">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlBOM" runat="server" Target="_blank">BOM</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SaleOrderNo - Seq.">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleOrderNo" runat="server" Text='<%#Eval("SaleOrderSeq")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="ItemSeq.">
                        <ItemTemplate>
                            <asp:Label ID="lblItemSeq" runat="server" Text='<%#Eval("xmdcseq")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                      <asp:TemplateField HeaderText="OrderType">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderType" runat="server" Text='<%#Eval("xmda005")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductItemNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblProductItemNo" runat="server" Text='<%#Eval("xmdc001")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="ProductItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblProductItemName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Specifaction">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecifaction" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="ItemCategory">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCategory" runat="server" Text='<%#Eval("imaa004")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("xmdastus") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SaleOrder Date">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleOrderDate" runat="server" Text='<%#Eval("xmdadocdt", "{0:yyyy/MM/dd}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SO Qty" >
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSOqty" runat="server"  Text='<%#Eval("xmdc007", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SaleDeliveryNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleDeliveryNo" runat="server" Text='<%#Eval("xmdmdocno") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SaleDeliveryQty.">
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSaleDeliveryQty" runat="server" Text='<%#Eval("xmdm009", "{0:N3}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Balacne Qty.">
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBalacneQty" runat="server" Text='<%#Eval("BalacneQty", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" runat="server" Text='<%#Eval("xmda004")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="DeliveryDate">
                           <HeaderStyle Width="50" />
                          <ItemStyle Width="50" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPlanDeliveryDate" runat="server" Text='<%#Eval("xmdkdocdt", "{0:yyyy/MM/dd}") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sale Request DueDate">
                          <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSaleRequestDueDate" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="BOM">
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOM" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Last BOM Update">
                        <ItemTemplate>
                            <asp:Label ID="lblLastBOMUpdate" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Production LeadTime">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionLeadTime" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Plan Batch No.">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanBatchNo" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PUR Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblPURRemark" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PUR Confirm Date">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPURConfirmDate" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PC Confirm Date">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPCConfirmDate" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sale Confirm">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSaleConfirm" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PUR Confirm Date 1">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPURConfirmDate1" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PC Confirm Date 1">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPCConfirmDate1" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sale Confirm 1">
                           <HeaderStyle Width="70" />
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSaleConfirm1" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SOconfrimLogNo." Visible="false">
                           <HeaderStyle Width="70" />
                        <ItemTemplate>
                            <asp:Label ID="lblSOconfrimLogNO" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

<%--            <asp:GridView ID="gvPrint" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                Visible="False">
                <Columns>
                   
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>--%>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
