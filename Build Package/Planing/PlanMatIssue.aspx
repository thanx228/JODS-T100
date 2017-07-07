<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanMatIssue.aspx.vb" Inherits="MIS_T100.PlanMatIssue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Date.ascx" TagPrefix="uc1" TagName="Date" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusMO_Normal.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal" %>
<%@ Register Src="~/UserControl/Normal/UsingMat_IssueType.ascx" TagPrefix="uc1" TagName="UsingMat_IssueType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

    .style1
    {
        width: 282px;
    }
        .auto-style2 {
            width: 112px;
        }
        .auto-style3 {
            width: 113px;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>      
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblSql" runat="server" ></asp:Label>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" style="width: 75%;background-color: #FFFFFF;">
            <table style="width: 75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />   
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Sale Order No."></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSaleNo" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="MO No. From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtWorkNoFrom" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Sale Order Seq"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSaleSeq" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="MO No. To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtWorkNoTo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Sale Date"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="txtSaleDate" />
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="MO Date From"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="MO_FormDate" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Cust Code"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="MO Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="MO_ToDate" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="BOM_Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text="MO Status"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingStatusMO_Normal runat="server" ID="UsingStatusMO_Normal" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Specifaction"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">

                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:CheckBox ID="CheckRecoilMat" runat="server" Text="Recoil Material" />
                         <asp:DropDownList ID="DLConsPurchase" runat="server">
                            <asp:ListItem Value="0">--Condition--</asp:ListItem>
                            <asp:ListItem Value="1">Cons.Purchase > 0</asp:ListItem>
                            <asp:ListItem Value="2">Cons.Purchase = 0</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                </tr>
            </table>
            <table style="width: 80%;background-color: #FFFFFF;">
                <tr>
                <td>
         <asp:Label ID="Label18" runat="server" Text="Issue DocNo."></asp:Label>
                    </td>
                    <td>
                        <uc1:UsingMat_IssueType ID="UsingMat_IssueType" runat="server" />
                       - <asp:TextBox ID="txtIssueDocNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    <asp:Label ID="Label9" runat="server" Text="Issue Status"></asp:Label>
                        <asp:DropDownList ID="DLIssueType" runat="server">
                            <asp:ListItem Value="0">--Select Status--</asp:ListItem>
                            <asp:ListItem Value="N">N : Unapproved</asp:ListItem>
                            <asp:ListItem Value="Y">Y : Approved</asp:ListItem>
                            <asp:ListItem Value="S">S : Posted</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
            </table>
          </asp:Panel>
            <table style="width: 60%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
              <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" Width="3800px"
                AllowPaging="True" PagerSettings-FirstPageText="First" PageSize ="50"
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <Columns>
                 <asp:TemplateField HeaderText="No."   >
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:BoundField DataField="sfdcdocno" HeaderText="Issue DocNo" />
                <asp:BoundField DataField="sfdadocdt" HeaderText="DocumentDate" DataFormatString="{0:yyyy/MM/dd}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                 <asp:BoundField DataField="sfda001" HeaderText="PostingDate" DataFormatString="{0:yyyy/MM/dd}">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                 <asp:BoundField DataField="sfda003" HeaderText="ProductionDept" />
                  <asp:BoundField DataField="sfda004" HeaderText="Applicant" >
                    </asp:BoundField>
                 <asp:BoundField DataField="sfdb006" HeaderText="Expected Sets" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                <asp:BoundField DataField="sfdb007" HeaderText="Actual Sets" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="AppliedQty">
                            <ItemTemplate>
                                <asp:Label ID="lblAppliedQty2" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="ActualQty ">
                            <ItemTemplate>
                                <asp:Label ID="lblActualQty2" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit ">
                            <ItemTemplate>
                                <asp:Label ID="lblUnit2" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="WH ">
                            <ItemTemplate>
                                <asp:Label ID="lblWH2" runat="server" ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     <asp:BoundField DataField="sfdastus" HeaderText="Issue Status" />
<%--                    <asp:BoundField DataField="xmdc007" HeaderText="SO Qty" />
                    <asp:BoundField DataField="xmdc006" HeaderText="SO Uint" />--%>
                    <%--<asp:BoundField DataField="xmdmdocno" HeaderText="SaleDelivery No." />--%>
                    <asp:BoundField DataField="xmdadocno" HeaderText="SaleOrder_No." />
                    <asp:BoundField DataField="xmda004" HeaderText="Customer" />
                    <asp:BoundField DataField="sfaadocno" HeaderText="MO_DocNo." />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status" />
                    <asp:BoundField DataField="sfba001" HeaderText="Production Item" />
                     <asp:BoundField DataField="imaal003" HeaderText="ProductionItemName" />
                    <asp:BoundField DataField="imaal004" HeaderText="Specifaction" /> 
                    <asp:BoundField DataField="sfaa019" HeaderText="PlanStartDate" DataFormatString="{0:yyyy/MM/dd}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfaa020" HeaderText="PlanCompleteDate" DataFormatString="{0:yyyy/MM/dd}" >  
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca001" HeaderText="Runcard" >   
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca005" HeaderText="RuncardDetail" />                                  
                    <asp:BoundField DataField="sfaa012" HeaderText="ProductionQty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfca004" HeaderText="CompleteQty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfaa056" HeaderText="ScarpQty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfaa013" HeaderText="Unit" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfbaseq" HeaderText="BOM LineNo" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba005" HeaderText="BOM Item" />
                    <asp:TemplateField HeaderText="Recoil Mat"   >
                            <ItemTemplate>
                                <asp:Label ID="lblRecoilMat" runat="server" Text='<%#Eval("sfba009")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="ChkRecoilMat" runat="server"  OnClick="return false;"  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    <asp:BoundField DataField="sfba015" HeaderText="Cons.Purchase" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba023" HeaderText="StandardIssuanceQty"  DataFormatString="{0:N3}"> 
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba016" HeaderText="IssueQty"  DataFormatString="{0:N3}">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UnIssuedQty" HeaderText="UnIssuedQty" DataFormatString="{0:N3}" > 
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UnIssuedQty" HeaderText="Required Qty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba014" HeaderText="BOM Unit" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
<%--                    <asp:BoundField DataField="sfcb002" HeaderText="LineNo" />
                    <asp:BoundField DataField="sfcb003" HeaderText="OperationID" />
                    <asp:BoundField DataField="sfcb011" HeaderText="Workstation" />
                     <asp:BoundField DataField="sfcb050" HeaderText="WIP" />--%>
                </Columns>
                  <FooterStyle BackColor="White" ForeColor="#000066" />
                  <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                  <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
                  <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                  <RowStyle ForeColor="#000066" />
                  <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                  <SortedAscendingHeaderStyle BackColor="#007DBB" />
                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                  <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
