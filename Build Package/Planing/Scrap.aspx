<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" 
   EnableEventValidation = "false" CodeBehind="Scrap.aspx.vb" Inherits="MIS_T100.Scrap" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/HeaderForm.ascx" TagName="HeaderForm" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc3" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusMO_Normal_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal_checkList" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="DateT100" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3 {
            width: 148%;
        }

        .style1 {
            width: 282px;
        }

        .auto-style2 {
            width: 268px;
        }

        .auto-style3 {
            width: 98px;
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
            <asp:Panel ID="PanelTabHead" runat="server" style="background-color: White; width: 75%;">
            <table style="background-color: White;">
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="So No."></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSoNo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="SaleOrder Date :"></asp:Label>
                    </td>
                    <td>
                        <uc1:DateT100 runat="server" ID="SaleOrderDate" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Work Order Type :"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <uc3:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Work Order No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMO_WO_No" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="BOM Item :"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtitem" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Spec :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtspec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlDate" runat="server">
                            <asp:ListItem Value="1">--Condition------</asp:ListItem>
                            <asp:ListItem Value="T">Return Rework Date</asp:ListItem>
                            <asp:ListItem Value="S">Scrap Date</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <uc1:DateT100 runat="server" ID="FromDate" />
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="To Date :"></asp:Label>
                    </td>
                    <td>
                        <uc1:DateT100 runat="server" ID="ToDate" />
                    </td>
                </tr>
            </table>
                </asp:Panel>
            <div>
                <table style="background-color: #f7ecec; width: 100%;">
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Label ID="Label21" runat="server" Text="Status :"></asp:Label>
                        </td>
                        <td>
                            <uc1:UsingStatusMO_Normal_checkList runat="server" ID="MO_NormalStatus" />
                        </td>
                    </tr>
                </table>
                <table style="background-color: #ffffff; width: 100%">
                    <tr>
                        <td style="vertical-align: top;" class="auto-style3">
                            <asp:Label ID="Label20" runat="server" Text="Work Center :"></asp:Label>
                        </td>
                        <td>
                            <uc1:UsingWorkstationCheckList runat="server" ID="WC_List" />
                        </td>
                    </tr>
                </table>

            </div>
            <table style="width: 75%;">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="Busearch" runat="server" Text="Search" Width="100px" />
                        &nbsp;<asp:Button ID="btSelect" runat="server" Text="Select Export" />
                        &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print Report" />
                        &nbsp;<asp:Button ID="btPrintAMP" runat="server" Text="AMP Format" />
                        &nbsp;<asp:Button ID="BuExcel" runat="server" Text="Excel" />
                        &nbsp;</td>
                </tr>
            </table>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">
      $(document).ready(function () {
	        gridviewShow();
	    });
	
      function gridviewShow() {
	        gridView1 = $('#<%=gvShow.ClientID%>').gridviewScroll({
	            //width: 1024,
	            width: screen.availWidth - 30,
                height: 620,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 5,
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
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" Width="3200px"
                AllowPaging="True" PageSize="50" PagerSettings-FirstPageText="First"
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" 
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                        <asp:CheckBox ID="chkAllSelect" runat="server" OnCheckedChanged="chkboxSelectAll_CheckedChanged" AutoPostBack="true"   />
                            <%--<asp:CheckBox ID="chkAllSelect" runat="server" AutoPostBack="true"   />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--   <asp:TemplateField HeaderText="No."   >
                            <ItemTemplate>
                                <asp:Label ID="lblNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="inbidocdt" HeaderText="ScarpDate." />--%>
                    <asp:BoundField DataField="xmdadocno" HeaderText="SaleOrder_No." />
<%--                    <asp:BoundField DataField="xmdc007" HeaderText="SO Qty" />
                    <asp:BoundField DataField="xmdc006" HeaderText="SO Uint" />--%>
                    <asp:BoundField DataField="sfcbdocno" HeaderText="MO_DocNo." />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba001" HeaderText="Production Item" />
                    <asp:BoundField DataField="sfbaseq" HeaderText="LineSeq" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfba005" HeaderText="BOM_Item" />
                    <asp:BoundField DataField="imaal003" HeaderText="BOM_ItemName" />
                    <asp:BoundField DataField="imaal004" HeaderText="BOM_Specifaction" />
                    <asp:BoundField DataField="sfcb001" HeaderText="Runcard" >
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
                    <asp:BoundField DataField="sfcb002" HeaderText="LineNo" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb003" HeaderText="OperationID" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfcb011" HeaderText="Workstation" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfiadocno" HeaderText="Return Rework No." />
                    <asp:BoundField DataField="sfia004" HeaderText="Runcard" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfia005" HeaderText="Trs.OutOpSerailNo" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sfia007" HeaderText="Rework-Trs-Out-Qty" DataFormatString="{0:N3}" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inbidocno" HeaderText="Applicatin No." />
                    <asp:BoundField DataField="inbidocdt" HeaderText="Entry Date." DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField DataField="inbi001" HeaderText="Applicant" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inbi002" HeaderText="Applied Dept." />
                    <asp:BoundField DataField="inbi003" HeaderText="ScarpReason" />
                    <asp:BoundField DataField="inbistus" HeaderText="Status" />
                    <asp:BoundField DataField="inbjseq" HeaderText="LineNo." >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inbj001" HeaderText="ItemNo." />
                    <asp:BoundField DataField="inbj005" HeaderText="WH" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inbj009" HeaderText="Applied Qty" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="inbj008" HeaderText="Unit" />
                </Columns>
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
            </asp:GridView>
            <asp:Panel ID="PanelTabReport" runat="server" style="background-color: White;width: 100%; ">
            <table style="width: 75%;">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btSelect0" runat="server" Text="Select Export" />
                        &nbsp;<asp:Button ID="btPrint0" runat="server" Text="Print Report" />
                        &nbsp;<asp:Button ID="btPrintAMP0" runat="server" Text="AMP Format" />
                        &nbsp;<asp:Button ID="BuExcel0" runat="server" Text="Excel" Width="100px" />
                    </td>
                </tr>
            </table>
         </asp:Panel>   
              <asp:GridView ID="gvSel" runat="server" AutoGenerateColumns="false" Width="1600">
                  <Columns>
                      <asp:TemplateField HeaderText="Workstation">
                          <ItemTemplate>
                              <asp:Label ID="lblWorkstation" runat="server" Text='<%#Eval("Workstation")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Scarp_ApplicatinNo">
                          <ItemTemplate>
                              <asp:Label ID="lblScarp_ApplicatinNo" runat="server" Text='<%#Eval("Scarp_ApplicatinNo")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Entry_Date">
                          <ItemTemplate>
                              <asp:Label ID="lblEntry_Date" runat="server" Text='<%#Eval("Entry_Date")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="MO_DocNo">
                          <ItemTemplate>
                              <asp:Label ID="lblMO_DocNo" runat="server" Text='<%#Eval("MO_DocNo")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Production_Item">
                          <ItemTemplate>
                              <asp:Label ID="lblProduction_Item" runat="server" Text='<%#Eval("Production_Item")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Scarp_Item">
                          <ItemTemplate>
                              <asp:Label ID="lblScarp_Item" runat="server" Text='<%#Eval("Scarp_Item")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Scarp_Spec">
                          <ItemTemplate>
                              <asp:Label ID="lblScarp_Spec" runat="server" Text='<%#Eval("Scarp_Spec")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Runcard">
                          <ItemTemplate>
                              <asp:Label ID="lblRuncard" runat="server" Text='<%#Eval("Runcard")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="RuncardDetail">
                          <ItemTemplate>
                              <asp:Label ID="lblRuncardDetail" runat="server" Text='<%#Eval("RuncardDetail")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Production_Qty">
                          <ItemTemplate>
                              <asp:Label ID="lblProduction_Qty" runat="server" Text='<%#Eval("Production_Qty")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="ScarpAppliedQty">
                          <ItemTemplate>
                              <asp:Label ID="lblScarpAppliedQty" runat="server" Text='<%#Eval("ScarpAppliedQty")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Scarp_Uint">
                          <ItemTemplate>
                              <asp:Label ID="lblScarp_Uint" runat="server" Text='<%#Eval("Scarp_Uint")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Defect">
                          <ItemTemplate>
                              <asp:Label ID="lblDefect" runat="server" Text='<%#Eval("Defect")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="RootCause">
                          <ItemTemplate>
                              <asp:Label ID="lblRootCause" runat="server" Text='<%#Eval("RootCause")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="Applicant">
                          <ItemTemplate>
                              <asp:Label ID="lblApplicant" runat="server" Text='<%#Eval("Applicant")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                     <asp:TemplateField HeaderText="EmpWorkDefect">
                          <ItemTemplate>
                              <asp:Label ID="lblEmpWorkDefect" runat="server" Text='<%#Eval("EmpWorkDefect")%>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
            </asp:GridView>   
        </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BuExcel" />
            <asp:PostBackTrigger ControlID="BuExcel0" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
