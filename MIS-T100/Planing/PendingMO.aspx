<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PendingMO.aspx.vb" Inherits="MIS_T100.PendingMO" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingWorkstation.ascx" TagPrefix="uc1" TagName="UsingWorkstation" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
        }
        .style10
        {
            height: 30px;
        }
        .style13
        {
            width: 155px;
        }
        .style15
        {
            width: 105px;
        }
        .style16
        {
            width: 226px;
        }
    </style>
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
            <table style="width:75%;">
                <tr>
                    <td align="left" style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                            Text="Pending MO"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 793px">
                <tr>
                    <td style="background-color: #FFFFFF" class="style13">
                        <asp:Label ID="Label8" runat="server" Text="Property"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlProperty" runat="server">
                            <asp:ListItem Selected="True" Value="1">Internal(Work Center)</asp:ListItem>
                            <asp:ListItem Value="2">Outsource(Supplier)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="style15">
                        <asp:Label ID="Label9" runat="server" Text="Suplier"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbWC" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="5" Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style13">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingWorkstation runat="server" ID="UsingWorkstation" />
                    </td>
                    <td style="background-color: #FFFFFF" class="style15">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style13">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style15">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />
                    </td>
                </tr>
                <tr>
                    <td class="style13" style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="Sale No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        JP<asp:TextBox ID="tbSaleNo" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td class="style15" style="background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSaleSeq" runat="server" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px" MaxLength="5" 
                            Width="50px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style13">
                        <asp:Label ID="Label3" runat="server" Text="Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="style15">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF" 
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="style13">
                        <asp:DropDownList ID="ddlDateType" runat="server">
                            <asp:ListItem Value="0">--SelectDate--</asp:ListItem>
                            <asp:ListItem Value="1">Plan Start Date From</asp:ListItem>
                            <asp:ListItem Value="2">Plan Complete Date From</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateFrom" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>
                    </td>
                    <td style="background-color: #FFFFFF" class="style15">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style16" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px" BackColor="White" 
                            BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="tbDateTo" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" class="style10" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Text="Export Excel" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" PagerSettings-FirstPageText="First" PageSize="50" 
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="TopAndBottom" Width="2400px" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                <Columns>
                    <asp:TemplateField HeaderText="Workstation">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblWorkstation" runat="server" Text='<%#Eval("sfcb011")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkstationName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seq">
                        <ItemTemplate>
                            <asp:Label ID="lblSeq" runat="server" Text='<%#Eval("sfcb002")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Op_Id">
                        <ItemTemplate>
                            <asp:Label ID="lblOp_Id" runat="server" Text='<%#Eval("sfcb003")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Opreation">
                        <ItemTemplate>
                            <asp:Label ID="lblOpreation" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" runat="server" Text='<%#Eval("xmda004")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SaleOrder No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleOrderNo" runat="server" Text='<%#Eval("xmdcdocno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-DocNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblMO" runat="server" Text='<%#Eval("sfcbdocno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Production ItemNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemNo" runat="server" Text='<%#Eval("sfaa010")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec">
                        <ItemTemplate>
                            <asp:Label ID="lblSpec" runat="server" Text='<%#Eval("imaal004")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanStart">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanStart" runat="server" Text='<%#Eval("sfcb044", "{0:MM/dd/yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanComplete">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanComplete" runat="server" Text='<%#Eval("sfcb045", "{0:MM/dd/yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanQty">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPlanQty" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WIP">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblWIP" runat="server" Text='<%#Eval("sfcb050", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GoodTransfer-in">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblGoodTransferIn" runat="server" Text='<%#Eval("sfcb028", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GoodTransfer-Out">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblGoodTransferOut" runat="server" Text='<%#Eval("sfcb033", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rework.Trs-in">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblReworkTrsIn" runat="server" Text='<%#Eval("sfcb029", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rework.Trs-Out">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblReworkTrsOut" runat="server" Text='<%#Eval("sfcb034", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DirecScarp">
                          <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblDirecScarp" runat="server"  Text='<%#Eval("sfcb036", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUint" runat="server"  Text='<%#Eval("sfcb052")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="TopAndBottom" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle ForeColor="#003399" BackColor="White" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />

            </asp:GridView>
            
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
