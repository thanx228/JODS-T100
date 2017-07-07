<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialListNew.aspx.vb" Inherits="MIS_T100.MaterialListNew" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
        }
        .style7
        {
        }
        .style8
        {
            width: 100px;
        }
        .style9
        {
            height: 21px;
            width: 303px;
        }
        .style10
        {
            width: 102px;
        }
        .style11
        {
            width: 161px;
        }
        .style12
        {
            width: 102px;
            height: 24px;
        }
        .style13
        {
            height: 24px;
        }
        .auto-style3 {
            width: 98px;
        }
        .auto-style4 {
            width: 79px;
        }
        .auto-style9 {
            width: 210px;
        }
        .auto-style11 {
            width: 126px;
        }
        .auto-style12 {
            width: 149px;
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
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label25" runat="server" Text="BOM Materials List" 
                            Font-Size="1.1em" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" 
                Height="25%" Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Item FG
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="width: 75%;">
                            <tr>
                                <td class="auto-style12" >
                                    <asp:Label ID="Label3" runat="server" Text="Master ItemNo."></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style12" >
                                    <asp:Label ID="Label4" runat="server" Text="Master ItemName"></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style12" >
                                    <asp:Label ID="Label5" runat="server" Text="Master Specifaction"></asp:Label>
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                                </td>
                                <td class="auto-style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style12">
                                    &nbsp;</td>
                                <td class="auto-style9">
                                    <asp:Label ID="Label24" runat="server" ForeColor="Red" Text="* Property is manufactor only"></asp:Label>
                                </td>
                                <td class="auto-style11">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:75%;">
                            <tr>
                                <td align="center" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                                    <asp:Button ID="btShowItem" runat="server" Text="Show Report" />
                                </td>
                            </tr>
                        </table>
                        <br>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <HeaderTemplate>
                        Sale order<br />
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="">
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label21" runat="server" Text="Cust Code"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="tbSaleCust" runat="server" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label6" runat="server" Text="SO Type"></asp:Label>
                                </td>
                                <td class="style11">
                                    &nbsp;
                                    <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label7" runat="server" Text="SO Number"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="tbSaleNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label8" runat="server" Text="SO Seq"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="tbSaleSeq" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label9" runat="server" Text="Date From"></asp:Label>
                                </td>
                                <td class="style11">
                                    <uc1:DateT100 runat="server" ID="FromDateT100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label10" runat="server" Text="Date To"></asp:Label>
                                </td>
                                <td class="style11">
                                    <uc1:DateT100 runat="server" ID="ToDateT100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:DropDownList ID="ddlSaleStatus" runat="server">
                                        <asp:ListItem Value="y,Y,N">ALL</asp:ListItem>
                                        <asp:ListItem Value="y,Y">Closed</asp:ListItem>
                                        <asp:ListItem Value="N">Not Close</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <table style="width:75%;">
                            <tr>
                                <td align="center" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                                    <asp:Button ID="btShowSO" runat="server" Text="Show Report" />
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        Manf Order
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="">
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                                </td>
                                <td>
                                    <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />   
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label13" runat="server" Text="MO No"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbWorkNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label23" runat="server" Text="Cust"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbWorkCust" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label27" runat="server" Text="Date From"></asp:Label>
                                </td>
                                <td>
                                    <uc1:DateT100 runat="server" ID="fomDateT100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    <asp:Label ID="Label28" runat="server" Text="Date To"></asp:Label>
                                </td>
                                <td>
                                    <uc1:DateT100 runat="server" ID="DateT100To" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table style="width:75%;">
                            <tr>
                                <td align="center" 
                                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                                    <asp:Button ID="btShowMO" runat="server" Text="Show Report" />
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                    <HeaderTemplate>
                        Customer
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="">
                            <tr>
                                <td class="style10">
                                    <asp:Label ID="Label22" runat="server" Text="Cust Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCust" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label30" runat="server" Text="Source By"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:DropDownList ID="ddlSource" runat="server">
                                        <asp:ListItem Value="1">Sale Forcast</asp:ListItem>
                                        <asp:ListItem Value="2">Sale Order</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style12">
                                    <asp:Label ID="Label31" runat="server" Text="Source Status"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:DropDownList ID="ddlSourceStatus" runat="server">
                                        <asp:ListItem Value="2">ALL</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">Not Closed</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    &nbsp;</td>
                                <td>
                                    <asp:CheckBox ID="cbShowSub" runat="server" Text="Sup Part + Mat" />
                                </td>
                            </tr>
                        </table>
                        <table style="width:75%;">
                            <tr>
                                <td align="center" 
                                    style="background-image: url('http://localhost:54341/Images/btt.jpg'); background-repeat: no-repeat">
                                    <asp:Button ID="btShowCust" runat="server" Text="Show Report " />
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
<br />
<br />
            <table style="width: 75%; " bgcolor="White">
                <tr>
                    <td class="style9" align="center">
                        <asp:Label ID="Label18" runat="server" Text="Amount Of Rows"></asp:Label>
                    </td>
                    <td class="style6" align="center">
                        <asp:Label ID="lbCount" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="style6" align="center">
                        <asp:Label ID="Label20" runat="server" Text="Rows"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btExport" runat="server" Text="Export Excel" />
            <br>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="MO-DocNo">
                        <ItemTemplate>
                            <asp:Label ID="lblMOdocno" runat="server" Text='<%#Eval("sfbadocno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemNo">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemNo" runat="server" Text='<%#Eval("sfaa010")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Production Qty">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblMOqty" runat="server" Text='<%#Eval("sfaa012", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Seq.">
                         <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblSeqItemNo" runat="server" Text='<%#Eval("sfbaseq")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BOM ItemNo">
                        <ItemTemplate>
                            <asp:Label ID="lblBOMitemNo" runat="server" Text='<%#Eval("sfba005")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BOM ItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblBOMItemName" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BOM Spec">
                        <ItemTemplate>
                            <asp:Label ID="lblBOMspec" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std.Issue Qty">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblStdIssueQty" runat="server" Text='<%#Eval("sfba023", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Qty">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblIssueQty" runat="server" Text='<%#Eval("sfba016", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UnIssuedQty">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnIssuedQty" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("sfba014")%>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanIssue">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblPlanIssue" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemCategory">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCategory" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stock-in">
                        <ItemTemplate>
                            <asp:Label ID="lblStckIn" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PR">
                        <ItemTemplate>
                            <asp:Label ID="lblPO" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO">
                        <ItemTemplate>
                            <asp:Label ID="lblPR" runat="server"  ></asp:Label>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
