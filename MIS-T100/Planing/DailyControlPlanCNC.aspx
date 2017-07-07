<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="DailyControlPlanCNC.aspx.vb" Inherits="MIS_T100.DailyControlPlanCNC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/Normal/UsingWorkstation.ascx" TagPrefix="uc1" TagName="UsingWorkstation" %>
<%@ Register Src="~/UserControl/Multiple/UsingMOTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingMOTypeCheckList" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="Date" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3 {
        }

        .style6 {
            height: 23px;
            width: 104px;
        }

        .style7 {
            height: 23px;
            width: 32px;
        }

        .style8 {
            height: 31px;
            width: 544px;
        }

        .style9 {
            height: 23px;
            width: 92px;
        }

        .style10 {
            height: 30px;
        }

        .style11 {
            width: 126px;
        }

        .auto-style3 {
            width: 158px;
        }

        .auto-style5 {
            width: 85px;
        }

        .auto-style6 {
            width: 313px;
        }

        .auto-style9 {
            width: 101px;
        }

        .auto-style10 {
            width: 84px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td align="left"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue"
                            Text="Daily Control Plan CNC"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 1200px; background-color: #FFFFFF;">
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label2" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td>
                        <uc1:UsingWorkstation runat="server" ID="UsingWorkstation" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF; vertical-align: top;" class="auto-style10">
                        <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td>
                        <uc1:UsingMOTypeCheckList runat="server" ID="UsingMOTypeCheckList" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label16" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <asp:TextBox ID="tbCust" runat="server" BackColor="White" BorderColor="#00CCFF"
                            BorderStyle="Solid" BorderWidth="1px" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label14" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Text="Production ItemNo."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <asp:TextBox ID="tbCode" runat="server" BackColor="White" BorderColor="#00CCFF"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server" BackColor="White" BorderColor="#00CCFF"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="0">--Select Date--</asp:ListItem>
                            <asp:ListItem Value="1">Plan Start Date From</asp:ListItem>
                            <asp:ListItem Value="2">Trn Date From</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <uc1:Date runat="server" ID="fromDate" />
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">
                        <asp:Label ID="Label11" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td class="style11" style="background-color: #FFFFFF">
                        <uc1:Date runat="server" ID="ToDate" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF" class="auto-style3">
                        <asp:Label ID="Label18" runat="server" Text="Plan Date Start"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style6">
                        <uc1:Date runat="server" ID="PlanDate" />
                    </td>
                    <td style="background-color: #FFFFFF" class="auto-style5">&nbsp;</td>
                    <td class="style11" style="background-color: #FFFFFF">&nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style10"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" Width="100px" />
                        &nbsp;
                        <asp:Button ID="btExportExcel" runat="server" Height="26px" Text="Export Excel"
                            Width="100px" />
                        &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 75%; border: thin solid #00CCFF; background-color: #FFFFFF;"
                class="style8">
                <tr>
                    <td align="center" class="style9">
                        <asp:Label ID="Label5" runat="server" Text="จำนวนรายการ"></asp:Label>
                    </td>
                    <td align="center" class="style6">
                        <asp:Label ID="lbCount" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    </td>
                    <td align="center" class="style7">
                        <asp:Label ID="Label7" runat="server" Text="รายการ"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label17" runat="server" Text="Capacity="></asp:Label>
            <asp:Label ID="lbCapa" runat="server" Font-Size="Medium" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" AutoGenerateColumns="false"
                 AllowPaging="True" PageSize="50" PagerSettings-FirstPageText="First"
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom"
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <Columns>
                    <asp:TemplateField HeaderText="WorkStation">
                        <ItemTemplate>
                            <asp:Label ID="lblWC" runat="server" Text='<%#Eval("sfcb011") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Operation">
                        <ItemTemplate>
                            <asp:Label ID="lblOperation" runat="server" Text='<%#Eval("sfcb003") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Id">
                        <ItemTemplate>
                            <asp:Label ID="lblCustId" runat="server" Text='<%#Eval("xmda004") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CustomerName">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SaleOrderNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblSO" runat="server" Text='<%#Eval("xmdadocno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sale-OrderType">
                        <ItemTemplate>
                            <asp:Label ID="lblSO_OrderType" runat="server" Text='<%#Eval("xmda005") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-DocNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblMO" runat="server" Text='<%#Eval("sfcbdocno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProdcitionItemNo">
                        <ItemTemplate>
                            <asp:Label ID="lblProdcitionItemNo" runat="server" Text='<%#Eval("sfaa010") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec">
                        <ItemTemplate>
                            <asp:Label ID="lblSpec" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanStart">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanStart" runat="server" Text='<%#Eval("sfcb044", "{0:yyyy/MM/dd}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionQty">
                        <ItemTemplate>
                            <asp:Label ID="lblMOQty" runat="server" Text='<%#Eval("sfaa012", "{0:N3}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemSeq.">
                        <ItemTemplate>
                            <asp:Label ID="lblItemSeq" runat="server" Text='<%#Eval("sfcb002") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanDate">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanQty">
                        <ItemTemplate>
                            <asp:Label ID="lblPlanQty" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WIP">
                        <ItemTemplate>
                            <asp:Label ID="lblWIP" runat="server" Text='<%#Eval("sfcb050", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transfer Qty.">
                        <ItemTemplate>
                            <asp:Label ID="lblTransferQty" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ReturnRework Qty.">
                        <ItemTemplate>
                            <asp:Label ID="lblReturnReworkQty" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-Receipt Qty.">
                        <ItemTemplate>
                            <asp:Label ID="lblMoreceiptQty" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
