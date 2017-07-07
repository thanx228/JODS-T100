<%@ Page Title="" EnableEventValidation="false" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanScheduleAdd.aspx.vb" Inherits="MIS_T100.PlanScheduleAdd" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 26px;
        }
        .style7
        {
            width: 207px;
            height: 26px;
        }
        .style8
        {
            width: 137px;
        }
        .style9
        {
            height: 26px;
            width: 137px;
        }
        .style10
        {
            width: 207px;
        }
        .style11
        {
        }
        .style13
        {
            width: 230px;
        }
        .style14
        {
            height: 39px;
        }
        .style15
        {
            width: 1236px;
        }
        .style16
        {
            width: 149px;
        }
        .style17
        {
            width: 223px;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            width: 400px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    
  <%--  <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollSelect();
        });

        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth ,
                height: 600,
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
        function gridviewScrollSelect() {
            gridView2 = $('#<%= gvSelect.ClientID %>').gridviewScroll({
                width: screen.availWidth,
                height: 600,
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
       function chkQty(elm) {
            //event.isDefaultPrevented();
            var row = elm.parentNode.parentNode;
            var planQty = Number(parseInt(elm.value.replace(/,/g, "")));
            var balQty = Number(parseInt(row.cells[20].innerHTML.replace(/,/g, "")));
            var planedQty = Number(parseInt(row.cells[18].innerHTML.replace(/,/g, "")));
            var stdMan = Number(parseInt(row.cells[7].innerHTML.replace(/,/g, "")));
            var stdMch = Number(parseInt(row.cells[8].innerHTML.replace(/,/g, "")));
            var chkQty = balQty + planedQty;
            if (planQty > Number(chkQty)) {
                alert("Plan Qty is over Planed Qty + Bal Qty!!");
                elm.value = chkQty;
                planQty = chkQty;
            }
            var sstdMan = Math.round(stdMan / balQty, 2);
            var sstdMch = Math.round(stdMch / balQty, 2);

           // alert(sstdMch);
            row.cells[5].innerHTML = Math.ceil(planQty * sstdMan);
            row.cells[6].innerHTML = Math.ceil(planQty * sstdMch);

            var sMan = 0;
            var sMch = 0;

            $("#<%=gvSelect.ClientID%> tr:has(td)").each(function () {
                var $tdElement18 = $(this).find("td:eq(5)"); //Cache Quantity column.
                var $tdElement19 = $(this).find("td:eq(6)"); //Cache Quantity column.

                sMan += parseInt($tdElement18.text());
                sMch += parseInt($tdElement19.text());
                //alert(sMan);
            });

            $("#lbUsageHourMch").text(sMan);
            $("#lbUsageHourMan").text(sMch);
       } 

        // onchange="ShowProcess();"
        function MouseEvents(objRef, evt) {

            if (objRef.getElementsByTagName("input").length != "0") {
                var checkbox = objRef.getElementsByTagName("input")[0];
                if (evt.type == "mouseover") {
                    objRef.style.backgroundColor = "grey";
                }
                else {
                    if (checkbox.checked) {
                        objRef.style.backgroundColor = "aqua";
                    }
                    else if (evt.type == "mouseout") {
                        objRef.style.backgroundColor = "white";
                    }
                }
             }
            
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
            <asp:Button ID="btBack" runat="server" Text="Back To Main" />
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="Plan Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbPlanDate" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Work Center*"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbWc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="1">
                <asp:View ID="View1" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td align="center" class="style15" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btSave" runat="server" Text="Save" UseSubmitBehavior="False" 
                                    Width="100px" />
                                &nbsp;&nbsp;<asp:Button ID="btUpdate" runat="server" style="margin-top: 2px" 
                                    Text="Update" Visible="False" />
                                &nbsp;<asp:Button ID="btSearch2" runat="server" Text="Veiw Search" />
                            </td>
                        </tr>
                    </table>
                     <asp:GridView ID="gvSelect" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                        <Columns>
                            <asp:TemplateField HeaderText="Detail">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplSelect" runat="server" Target="_blank">Detail</asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CancelNum" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblCkCencel" runat="server" Text='<%#Eval("Cancled")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cancel">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbCancel" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SetSeq">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSetSeq" runat="server" Width="30" Text='<%#Eval("PlanSeq")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UrgentNum" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCkUrgent" runat="server" Text='<%#Eval("Urgent")%>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Urgent">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbUrgent" runat="server"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mch">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMch" runat="server" Width="100" Text='<%#Eval("Mch")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Ap100">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblAp100" runat="server" Text='<%#Eval("ap100")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SaleOrderNo.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSaleOrderNo" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="MO-DocNo.">
                                <ItemTemplate>
                                    <asp:Label ID="lblMO_No" runat="server" Text='<%#Eval("MO_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Labor/Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblStdLaborTime" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mch/Hours">
                                <ItemTemplate>
                                    <asp:Label ID="lblStdMcTime" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="W/C">
                                <ItemTemplate>
                                    <asp:Label ID="lblWC" runat="server" Text='<%#Eval("WC")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PlanRecord">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlanDate" runat="server" Text='<%#Eval("PlanDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Seq.">
                                <ItemTemplate>
                                    <asp:Label ID="lblErpMO_Seq" runat="server" Text='<%#Eval("Seq")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="PlanQty">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("PlanQty")%>' Width="50" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomer" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Production ItemNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionItemNo" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Production ItemName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionItemName" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Spec">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecifaction" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ProductionQty">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblProductionQty" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="CompleteQty">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblCompleteQty" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ScrapQty">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblScrapQty" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Unit">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="GoodTrs-in">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodTransferIn" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="WIP">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblWIP" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="GoodTrs-Out">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodTransferOut" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ReworkTrs-in">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblRewrokTrsin" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ReworkTrs-Out">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblRewrokTrsOut" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="DirectScarp">
                                 <ItemStyle  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblDirectScarp" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" Wrap="False" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                    <div style="background-color: #FFFFFF">
                        <table class="auto-style2">
                            <tr>
                                <td class="auto-style3">
                                    <asp:Label ID="Label35" runat="server" Text=" Sum (Ap100,Labor/Hours,Mch/Hours) where <> Cancel" 
                                        ForeColor="#800000"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <table style="width: 66%;">
                        <tr>
                            <td align="center" style="background-color: #FFFFFF" class="style16">
                                <asp:Label ID="Label28" runat="server" Text="Unit Of Time = Minute"></asp:Label>
                            </td>
                            <td align="center" style="background-color: #FFFFFF">
                                <asp:Label ID="Label29" runat="server" Text="Standard Hour"></asp:Label>
                            </td>
                            <td align="center" style="background-color: #FFFFFF">
                                <asp:Label ID="Label30" runat="server" Text="Usage Hour"></asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #FFFFFF">
                                <asp:Label ID="Label31" runat="server" Text="Balance Hour"></asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #FFFFFF">
                                <asp:Label ID="Label34" runat="server" Text="AP100"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #CCCCCC" class="style16">
                                <asp:Label ID="Label32" runat="server" Text="Machine"></asp:Label>
                            </td>
                            <td align="center" style="background-color: #CCCCCC">
                                <asp:Label ID="lbStdHourMchSum" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" style="background-color: #CCCCCC">
                                <asp:Label ID="lbUsageHourMch" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #CCCCCC">
                                <asp:Label ID="lbBalHourMch" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #CCCCCC">
                                <asp:Label ID="lbAp100Mch" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: #CCCCCC" class="style16">
                                <asp:Label ID="Label33" runat="server" Text="Man"></asp:Label>
                            </td>
                            <td align="center" style="background-color: #CCCCCC">
                                <asp:Label ID="lbStdHourManSum" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" style="background-color: #CCCCCC">
                                <asp:Label ID="lbUsageHourMan" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #CCCCCC">
                                <asp:Label ID="lbBalHourMan" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                            <td align="center" class="style13" style="background-color: #CCCCCC">
                                <asp:Label ID="lbAp100Man" runat="server" ForeColor="Blue">0</asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                <asp:Label ID="Label24" runat="server" Text="Process*"></asp:Label>
                            </td>
                            <td class="style10" style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlProcess" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbDoc" runat="server" Text="Tran No (D201)"></asp:Label>
                            </td>
                            <td style="background-color: #FFFFFF" class="style11">
                                <asp:TextBox ID="tbTranNo" runat="server" MaxLength="11" Width="110px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" style="background-color: #FFFFFF">
                                <asp:Label ID="Label25" runat="server" Text="End Plan Start Date*"></asp:Label>
                            </td>
                            <td class="style7" style="background-color: #FFFFFF">
                                <uc2:Date ID="ucDateEnd" runat="server" />
                            </td>
                            <td class="style6" colspan="2" style="background-color: #FFFFFF">
                                <asp:CheckBox ID="cbMoWorking" runat="server" Text="MO in Process*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style9" style="background-color: #FFFFFF">
                                <asp:Label ID="Label26" runat="server" Text="Sort 1"></asp:Label>
                            </td>
                            <td class="style7" style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlSort1" runat="server">
                                    <asp:ListItem Selected="True" Value="MA002">Cust Name</asp:ListItem>
                                    <asp:ListItem Value="PlanStartDate"></asp:ListItem>
                                    <asp:ListItem Value="planQty">Plan Start DatePlan Qty</asp:ListItem>
                                    <asp:ListItem Value="stdMan">Standard Man</asp:ListItem>
                                    <asp:ListItem Value="stdMch">Standard Mch</asp:ListItem>
                                    <asp:ListItem Value="tranNo">Tranfer No.</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="style6" colspan="2" style="background-color: #FFFFFF">
                                <asp:CheckBox ID="cbSort1" runat="server" Text="Z to A" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style8" style="background-color: #FFFFFF">
                                <asp:Label ID="Label27" runat="server" Text="Sort 2"></asp:Label>
                            </td>
                            <td class="style10" style="background-color: #FFFFFF">
                                <asp:DropDownList ID="ddlSort2" runat="server">
                                    <asp:ListItem Value="MA002">Cust Name</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="PlanStartDate">Plan Start Date</asp:ListItem>
                                    <asp:ListItem Value="moQty">Plan Qty</asp:ListItem>
                                    <asp:ListItem Value="stdMan">Standard Man</asp:ListItem>
                                    <asp:ListItem Value="stdMch">Standard Mch</asp:ListItem>
                                    <asp:ListItem Value="tranNo">Transfer No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" style="background-color: #FFFFFF">
                                <asp:CheckBox ID="cbSort2" runat="server" Text="Z to A" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%;">
                        <tr>
                            <td align="center" class="style14" 
                                
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                                <asp:Button ID="btSelect" runat="server" Text="Select" 
                                    UseSubmitBehavior="false" Width="100px" />
                                &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" 
                                    UseSubmitBehavior="false" Width="100px" />
                                &nbsp;<asp:Button ID="btClear" runat="server" Text="Clear" UseSubmitBehavior="false" 
                                    Width="100px" />
                                &nbsp;<asp:Button ID="btSelect2" runat="server" Text="Veiw Select" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Detail">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TA008" HeaderText="MO Plan Start Date" />
                            <asp:BoundField DataField="TA022" HeaderText="Std Man" 
                                DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="stdMch" HeaderText="Std Mch" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="tranNo" HeaderText="Trans No." />
                            <asp:BoundField DataField="ap100" HeaderText="AP100(Min)" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="so" HeaderText="SO" />
                            <asp:BoundField DataField="TA001" HeaderText="MO" />
                            <asp:BoundField DataField="MW002" HeaderText="Opr Name" />
                            <asp:BoundField DataField="waitTrnQty" DataFormatString="{0:N}" 
                                HeaderText="Wait Transfer Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                            <asp:BoundField DataField="TA035" HeaderText="Spec" />
                            <asp:BoundField DataField="wc" HeaderText="WC" />
                            <asp:BoundField DataField="TA015" HeaderText="MO Qty" DataFormatString="{0:N0}">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TA011" DataFormatString="{0:N0}" 
                                HeaderText="Complete Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="scpQty" DataFormatString="{0:N0}" 
                                HeaderText="Scrap Qty" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WipQty" DataFormatString="{0:N0}" 
                                HeaderText="WIP Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="balPlan" DataFormatString="{0:N0}" 
                                HeaderText="Bal Plan" />
                            <asp:BoundField DataField="PlanedQty" DataFormatString="{0:N0}" 
                                HeaderText="Planed Qty">
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LastPlanDate1" HeaderText="Last TV Plan Date" />
                            <asp:BoundField DataField="balQty" HeaderText="MO Bal Qty" 
                                DataFormatString="{0:N0}" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TA006" HeaderText="Item" />
                            <asp:BoundField DataField="TA004" HeaderText="Operation" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                            CssClass="FrozenHeader" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlProcess" />
            <asp:PostBackTrigger ControlID="btSearch" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>
