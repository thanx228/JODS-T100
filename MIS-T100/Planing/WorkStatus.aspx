<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master"
    EnableEventValidation="false" CodeBehind="WorkStatus.aspx.vb" Inherits="MIS_T100.WorkStatus" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>
<%@ Register Src="~/UserControl/Normal/UsingStatusMO_Normal.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            height: 27px;
            width: 99px;
        }
        .style34
        {
            height: 27px;
            width: 78px;
        }
        .style38
        {
            height: 27px;
            width: 53px;
        }
        .style40
        {
            height: 21px;
            width: 99px;
        }
        .style41
        {
            height: 27px;
            width: 38px;
        }
        .style42
        {
            height: 21px;
            width: 38px;
        }
        .style43
        {
            height: 21px;
            width: 53px;
        }
        .style44
        {
            height: 21px;
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
            <asp:Label ID="lblSql" runat="server" ></asp:Label>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="height: 96px;width: 75%;">
                <tr>
                    <td class="style34" style="background-color: #FFFFFF">
                        <asp:Label ID="Label1" runat="server" Text="Sale Type"></asp:Label>
                    </td>
                    <td class="style41" style="background-color: #FFFFFF">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />   
                    </td>
                    <td class="style38" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Sale No"></asp:Label>
                    </td>
                    <td class="style4" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSaleNo" runat="server" BorderColor="Black" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label3" runat="server" Text="Sale Seq"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSaleSeq" runat="server" BorderColor="Black" 
                            Width="34px"></asp:TextBox>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Customer"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtCust" runat="server" BorderColor="Black" 
                            Width="52px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label2" runat="server" Text="Work Type"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Work No"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtWorkNo" runat="server" BorderColor="Black" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label10" runat="server" Text="Production Item"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtProductionItem" runat="server" BorderColor="Black"></asp:TextBox>
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Specifaction"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:TextBox ID="txtSpec" runat="server" BorderColor="Black"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="0">--Select Date--</asp:ListItem>
                            <asp:ListItem Value="DS">SaleDelivery Date</asp:ListItem>
                            <asp:ListItem Value="SD">SaleOrder Date</asp:ListItem>
                            <asp:ListItem Value="MOD">MO Date</asp:ListItem>
                            <asp:ListItem Value="PSD">Plan Start Date</asp:ListItem>
                            <asp:ListItem Value="PFD">Plan Finish Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="FormDate" />
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <uc1:DateT100 runat="server" ID="ToDate" />
                    </td>
                </tr>
                <tr>
                    <td class="style44" style="background-color: #FFFFFF">
                        <asp:Label ID="Label13" runat="server" Text="Status Code"></asp:Label>
                    </td>
                    <td class="style42" style="background-color: #FFFFFF">
                        <uc1:UsingStatusMO_Normal runat="server" ID="UsingStatusMO_Normal" />
                    </td>
                    <td class="style43" style="background-color: #FFFFFF">
                        <asp:Label ID="Label15" runat="server" Text="Process"></asp:Label>
                    </td>
                    <td class="style40" style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlProcess" runat="server">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Not Complete</asp:ListItem>
                            <asp:ListItem Value="2">WIP&gt;0</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style44" colspan="4" style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="Remark"></asp:Label>
                        &nbsp;
                        <asp:Label ID="Label17" runat="server" ForeColor="Blue" 
                            Text="Qty=WIP(Complete)"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btnReport" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
           <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" Width="2200px" Visible="false"
                AllowPaging="True" PageSize="50" PagerSettings-FirstPageText="First"
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom">
                <Columns>
                       <asp:TemplateField HeaderText="No."   >
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <%--<asp:BoundField DataField="xmdmdocno" HeaderText="SaleDelivery No." />--%>
                    <asp:BoundField DataField="" HeaderText="SaleOrder_No." />
<%--                    <asp:BoundField DataField="xmdc007" HeaderText="SO Qty" />
                    <asp:BoundField DataField="xmdc006" HeaderText="SO Uint" />--%>
                    <asp:BoundField DataField="" HeaderText="Customer" />
                    <asp:BoundField DataField="sfaadocno" HeaderText="MO_DocNo." />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status" />
                    <asp:BoundField DataField="sfaa010" HeaderText="Production Item" />
                     <asp:BoundField DataField="imaal003" HeaderText="ProductionItemName" Visible="false" />
                    <asp:BoundField DataField="imaal004" HeaderText="Specifaction" />                  
                    <asp:BoundField DataField="sfca001" HeaderText="Runcard" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfca005" HeaderText="Runcard Deatil" />
                    <asp:BoundField DataField="sfaa012" HeaderText="ProductionQty" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfca004" HeaderText="CompleteQty" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfaa056" HeaderText="ScarpQty" DataFormatString="{0:N3}" />
                    <asp:BoundField DataField="sfaa019" HeaderText="PlanStartDate" DataFormatString="{0:yyyy/MM/dd}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfaa020" HeaderText="PlanCompleteDate" DataFormatString="{0:yyyy/MM/dd}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfcb002" HeaderText="LineNo" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfcb003" HeaderText="OperationID" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfcb011" HeaderText="Workstation" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                     <asp:BoundField DataField="sfcb050" HeaderText="WIP" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                     <asp:BoundField DataField="sfcb028" HeaderText="GoodTrs-In" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                     <asp:BoundField DataField="sfcb033" HeaderText="GoodTrs-Out" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfcb029" HeaderText="Rework-Trs-In" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                    <asp:BoundField DataField="sfcb034" HeaderText="Rework-Trs-Out" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                     <asp:BoundField DataField="sfcb036" HeaderText="DirectScarp" DataFormatString="{0:N3}" >
                       <ItemStyle HorizontalAlign="Center" />
                       </asp:BoundField>
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
            </asp:GridView>
            <%--<asp:GridView ID="GridView1" runat="server"
                BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black">
                <Columns>
                   <asp:TemplateField HeaderText="No."   >
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderStyle-Width="150" ItemStyle-Width="150" HeaderText="SaleOrderNo."  >
                            <ItemTemplate>
                                <asp:Label ID="lblSaleOrderNo" runat="server" ></asp:Label>
                            </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Sale ItemNo."   >
                            <ItemTemplate>
                                <asp:Label ID="lblSaleItemNo" runat="server" ></asp:Label>
                            </ItemTemplate>
                   </asp:TemplateField>
                </Columns>
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
            </asp:GridView>--%>

    <asp:GridView ID="gvShowPiVot" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-Wrap="false" 
            ShowHeaderWhenEmpty="True" EmptyDataText="Not have Data Record" BackColor="White" BorderColor="#3366CC"
         BorderStyle="None" BorderWidth="1px" CellPadding="4"
        ShowFooter="True"  AllowPaging="True"  PagerSettings-FirstPageText="First" 
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"                  
                Height="16px" PagerSettings-Position="Bottom">
                <Columns>
                       <asp:TemplateField HeaderText="No."   >
                            <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="xmdadocno" HeaderText="SaleOrder_No."  >
                       <HeaderStyle Width="350px" />
                       <ItemStyle Width="350px" />
                       </asp:BoundField>
                    <asp:BoundField DataField="xmda004" HeaderText="Customer" />
                    <asp:BoundField DataField="sfaadocno" HeaderText="MO_DocNo." />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status" />
                    <asp:BoundField DataField="sfaa010" HeaderText="Production Item" />
                     <asp:BoundField DataField="imaal003" HeaderText="ProductionItemName"  />
                    <asp:BoundField DataField="imaal004" HeaderText="Specifaction" />                  
                    <asp:BoundField DataField="sfca001" HeaderText="Runcard" />
                    <asp:BoundField DataField="sfca005" HeaderText="Runcard Deatil"  />
                    <asp:BoundField DataField="sfaa019" HeaderText="PlanStartDate" DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField DataField="sfaa020" HeaderText="PlanCompleteDate" DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField DataField="sfaa012" HeaderText="ProductionQty" />
                    <asp:BoundField DataField="sfca004" HeaderText="CompleteQty" />
                    <asp:BoundField DataField="sfaa056" HeaderText="ScarpQty" />
               <asp:TemplateField HeaderText="Process 1" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep1" runat="server" Text='<%#Eval("StartProcess")%>' ></asp:Label>
                         <asp:Label ID="lblStep1_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep1" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep1" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep1" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep1" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep1" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 2" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep2" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep2_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep2" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep2" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep2" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep2" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep2" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 3" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep3" runat="server"  ></asp:Label>
                        <asp:Label ID="lblStep3_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep3" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep3" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep3" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep3" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep3" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 4" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep4" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep4_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep4" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep4" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep4" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep4" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep4" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 5" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep5" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep5_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep5" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep5" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblTrsInStep5" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWIPStep5" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblTrsOutStep5" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 6" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep6" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep6_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep6" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep6" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep6" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep6" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep6" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 7" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep7" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep7_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep7" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep7" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep7" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep7" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep7" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 8" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep8" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep8_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep8" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep8" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep8" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep8" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep8" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 9" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep9" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep9_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep9" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep9" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep9" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep9" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep9" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 10"  >
                    <ItemTemplate>
                        <asp:Label ID="lblStep10" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep10_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep10" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep10" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep10" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep10" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep10" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 11" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep11" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep11_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep11" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep11" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep11" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep11" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep11" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 12" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep12" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep12_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep12" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep12" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep12" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep12" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep12" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 13" > 
                    <ItemTemplate>
                        <asp:Label ID="lblStep13" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep13_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep13" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep13" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep13" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep13" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep13" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 14" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep14" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep14_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep14" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep14" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep14" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep14" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep14" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 15" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep15" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep15_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep15" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep15" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep15" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep15" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep15" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 16" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep16" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep16_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep16" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep16" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep16" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep16" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep16" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 17" >
                    <ItemTemplate>
                        <asp:Label ID="lblStep17" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep17_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep17" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep17" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep17" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep17" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep17" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 18">
                    <ItemTemplate>
                        <asp:Label ID="lblStep18" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep18_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep18" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep18" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep18" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep18" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep18" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 19">
                    <ItemTemplate>
                        <asp:Label ID="lblStep19" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep19_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep19" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep19" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep19" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep19" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep19" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 20">
                    <ItemTemplate>
                        <asp:Label ID="lblStep20" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep20_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep20" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep20" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep20" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep20" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep20" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 22">
                    <ItemTemplate>
                        <asp:Label ID="lblStep21" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep21_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep21" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep21" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep21" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep21" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep21" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 22">
                    <ItemTemplate>
                        <asp:Label ID="lblStep22" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep22_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep22" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep22" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep22" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep22" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep22" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 23">
                    <ItemTemplate>
                        <asp:Label ID="lblStep23" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep23_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep23" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep23" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep23" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep23" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep23" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 24">
                    <ItemTemplate>
                        <asp:Label ID="lblStep24" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep24_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep24" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep24" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep24" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep24" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep24" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 25">
                    <ItemTemplate>
                        <asp:Label ID="lblStep25" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep25_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep25" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep25" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep25" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep25" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep25" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 26">
                    <ItemTemplate>
                        <asp:Label ID="lblStep26" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep26_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep26" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep26" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep26" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep26" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep26" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 27">
                    <ItemTemplate>
                        <asp:Label ID="lblStep27" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep27_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep27" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep27" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep27" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep27" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep27" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 28">
                    <ItemTemplate>
                        <asp:Label ID="lblStep28" runat="server" ></asp:Label>
                         <asp:Label ID="lblStep28_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep28" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep28" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep28" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep28" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep28" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 29">
                    <ItemTemplate>
                        <asp:Label ID="lblStep29" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep29_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep29" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep29" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep29" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep29" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep29" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Process 30">
                    <ItemTemplate>
                        <asp:Label ID="lblStep30" runat="server"  ></asp:Label>
                         <asp:Label ID="lblStep30_C" runat="server"  Visible="false" ></asp:Label><br />
                        <asp:Label ID="lblOPStep30" runat="server"  ></asp:Label><br />
                        <asp:Label ID="lblWCStep30" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsInStep30" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblWIPStep30" runat="server"  ></asp:Label><br />
                         <asp:Label ID="lblTrsOutStep30" runat="server"  ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="350px" />
                    <ItemStyle Width="350px" />
                </asp:TemplateField>
<%--                <asp:TemplateField HeaderText="StartProcess">
                    <ItemTemplate>
                        <asp:Label ID="lblStepStart" runat="server"  Text='<%#Eval("StartProcess")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EndProcess">
                    <ItemTemplate>
                        <asp:Label ID="lblStepEnd" runat="server"  Text='<%#Eval("EndProcess")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Process Count">
                    <ItemTemplate>
                        <asp:Label ID="lblStepCount" runat="server"  Text='<%#Eval("CountProcess")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
     <%--               <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />--%>
                       <RowStyle BackColor="White" ForeColor="#003399" />
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <AlternatingRowStyle />
    <%--                <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="#444444" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="#e0f8fc" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />--%>
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
