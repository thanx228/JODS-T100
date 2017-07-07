<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanScheduleCheck.aspx.vb" Inherits="MIS_T100.PlanScheduleCheck" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Check Plan Schedule</title>
    <style type="text/css">
        .style4
        {
            width: 196px;
        }
        .style6
        {
            width: 22%;
        }
        .style8
        {
            width: 203px;
        }
        .style9
        {
            width: 213px;
        }
        .style10
        {
            height: 23px;
        }
        .style11
        {
            height: 23px;
            width: 35px;
        }
        .style12
        {
            width: 35px;
        }
    </style>
    <script src="../Scripts/jsScript.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Scripts/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            gridView1 = $('#gvPlan').gridviewScroll({
                width: screen.availWidth-30,
                height: screen.availHeight - (screen.availHeight * 0.2) ,
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
                barsize: 12
            });
        }
	</script>


</head>
<body style="background-image: url('../Images/bg.jpg')" >
    <form id="form1" runat="server">
    <table style="width: 75%;" >
        <tr ">
            <td style="background-color: #FFFFFF;width:80px" >
                <asp:Label ID="Label5" runat="server" Text="Plan Date"></asp:Label>
            </td>
            <td style="background-color: #FFFFFF;width:150px" >
                <asp:Label ID="lbPlanDate" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td style="background-color: #FFFFFF;width:100px" >
                <asp:Label ID="Label6" runat="server" Text="Work Center"></asp:Label>
            </td>
            <td style="background-color: #FFFFFF;width:250px;" >
                <asp:Label ID="lbWc" runat="server" ForeColor="Blue" Visible="false" ></asp:Label>
                <asp:Label ID="lbWcName" runat="server" ForeColor="Blue" ></asp:Label>
            </td>
            <td style="background-color: #FFFFFF" >
                <asp:Button ID="btBefore" runat="server" Text="&lt;&lt;" Width="80px" />
                </td>
            <td style="background-color: #FFFFFF" >
                <asp:Button ID="btAfter" runat="server" Text="&gt;&gt;" Width="80px" />
            </td>
        </tr>
        </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="Label7" runat="server" Text="Summary" Font-Size="1em" 
        ForeColor="Blue"></asp:Label>
    <br />
    <table style="width:60%;">
        <tr>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label9" runat="server" Text="MO ProductionQty"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label10" runat="server" Text="Good.Transfer-Out"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label11" runat="server" Text="Action Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label12" runat="server" Text="Action Plan %"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label13" runat="server" Text="No Action Plan"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label15" runat="server" Text="No Action Plan %"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label20" runat="server" Text="WC Load"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style10">
                <asp:Label ID="Label16" runat="server" Text="Man Time"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style11">
                <asp:Label ID="Label17" runat="server" Text="Mch Time"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style11">
                <asp:Label ID="Label21" runat="server" Text="Canceled"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style11">
                <asp:Label ID="Label22" runat="server" Text="Canceled Rate"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style11">
                <asp:Label ID="Label23" runat="server" Text="AP100 Time"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbMOPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbActTran" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbActPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbPer" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbNoPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbPerNoPlan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbWcLoad" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White">
                <asp:Label ID="lbManTime" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style12">
                <asp:Label ID="lbMchTime" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style12">
                <asp:Label ID="lbCan" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style12">
                <asp:Label ID="lbCanRate" runat="server" ForeColor="Blue"></asp:Label>
            </td>
            <td align="center" bgcolor="White" class="style12">
                <asp:Label ID="lbAp100" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        </table>
            <table style="width:75%; background-image: url('../Images/btt.jpg'); background-repeat: no-repeat;">
                <tr>
                    <td align="center">
                         <asp:Button 
                            ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
    <asp:Label ID="Label8" runat="server" Text="Detail" Font-Size="1em" 
        ForeColor="Blue"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" Width="443px">
                <Columns>
                    <asp:BoundField DataField="PlanSeqSet" HeaderText="Piority" />
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate >
                            <asp:Label ID="lblCustomer" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PlanDate" HeaderText="PlanDate" />
                     <asp:BoundField DataField="Mch" HeaderText="M/C" />
                    <asp:BoundField DataField="MO_No" HeaderText="MO_No" />
                    <asp:TemplateField HeaderText="MO Status">
                        <ItemTemplate >
                            <asp:Label ID="lblMOStatus" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItem">
                        <ItemTemplate >
                            <asp:Label ID="lblProductionItem" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemName" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblProductionItemName" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specifaction">
                        <ItemTemplate >
                            <asp:Label ID="lblSpec" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionQty">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblProductionQty" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CompleteQty">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblCompleteQty" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ScrapQty">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblScrapQty" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mo-Seq">
                        <ItemTemplate >
                            <asp:Label ID="lblMO_Seq" runat="server" Text='<%#Eval("TA003") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Op_Id">
                        <ItemTemplate >
                            <asp:Label ID="lblOp_Id" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Operation">
                        <ItemTemplate >
                            <asp:Label ID="lblOperation" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StartTime M/C">
                        <ItemTemplate >
                            <asp:Label ID="lblStartTimeMC" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanSeq">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblPlanSeq" runat="server" Text='<%#Eval("PlanSeq") %>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanQty">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblPlanQty" runat="server" Text='<%#Eval("PlanQty", "{0:N0}") %>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std.OutPut">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblStdOutPut" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WIP">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblWIP" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Good.Trs.In">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblGoodTrsIn" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Good.Trs.Out">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblGoodTrsOut" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rework.Trs.In">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblReworkTrsIn" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Rework.Trs.Out">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblReworkTrsOut" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DirectScarp">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblDirectScarp" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DirectSuspend">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblDirectSuspend" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PerviousOperation">
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate >
                            <asp:Label ID="lblPerviousOperation" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NextOperation">
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate >
                            <asp:Label ID="lblNextOperation" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Next.Workstation">
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate >
                            <asp:Label ID="lblWorkstationNext" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std.LaborHours">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblStdLaborHours" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std.McHours">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblStdMcHours" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="atis304 Machine.StartTime">
                        <ItemTemplate >
                            <asp:Label ID="lblKOISmcStart" runat="server"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AP100">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate >
                            <asp:Label ID="lblAP100" runat="server" Text='<%#Eval("ap100", "{0:N0}") %>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
           <%-- <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" Width="443px">
                <Columns>
                    <asp:BoundField DataField="PlanSeqSet" HeaderText="Piority" />
                    <asp:BoundField DataField="MA002" HeaderText="Cust Name" />
                    <asp:BoundField DataField="Mch" HeaderText="M/C" />
                    <asp:BoundField DataField="PlanDate" HeaderText="Plan Date" />
                    <asp:BoundField DataField="TA001" HeaderText="MO Type/No/Seq" />
                    <asp:BoundField DataField="MW002" HeaderText="Operation" />
                    <asp:BoundField DataField="TA035" HeaderText="Spec" />
                    <asp:BoundField DataField="MC" HeaderText="Start Time M/C" />
                    <asp:BoundField DataField="TA015" DataFormatString=" {0:#,#.##} " 
                        HeaderText="MO Q'ty" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wipQty" HeaderText="WIP Q'ty" 
                        DataFormatString=" {0:#,#.##} " />
                    <asp:BoundField DataField="PlanQty" HeaderText="Plan Qty" 
                        DataFormatString=" {0:#,#.##} ">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA018" DataFormatString=" {0:#,#.##} " 
                        HeaderText="MO Scrap Q'ty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SFCTA011" DataFormatString=" {0:#,#.##} " 
                        HeaderText="Process Comp Q'ty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TA012" HeaderText="Process Scrap Q'ty" 
                        DataFormatString=" {0:#,#.##} " />
                    <asp:BoundField DataField="TA013" DataFormatString=" {0:#,#.##} " 
                        HeaderText="Re-MO Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ActualQty" HeaderText="Plan Comp Qty" 
                        DataFormatString=" {0:#,#.##} ">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UnAppQty" DataFormatString=" {0:#,#.##} " 
                        HeaderText="Q'ty for Trans(Not App.)" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PD" DataFormatString=" {0:#,#.##} " 
                        HeaderText="PD Daily Qty" />
                    <asp:BoundField DataField="lastWC" HeaderText="Last Process" />
                    <asp:BoundField DataField="nextProcess" HeaderText="Next Process" />
                    <asp:BoundField DataField="nextPlanDate" HeaderText="Next Plan Date" />
                    <asp:BoundField DataField="ActualDate" HeaderText="Actual Date" />
                    <asp:BoundField DataField="TranAcc" HeaderText="Create Date Time" />
                    <asp:BoundField DataField="LastTranAcc" HeaderText="Last Input Date" />
                    <asp:BoundField DataField="LastActualDate" HeaderText="Last App Date" />
                    <asp:BoundField DataField="MOCTA011" HeaderText="MO Status" />
                    <asp:BoundField DataField="A" DataFormatString=" {0:#,#} " 
                        HeaderText="Man Usage(Min)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="B" DataFormatString=" {0:#,#} " 
                        HeaderText="Mch Usage(Min)">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="C" DataFormatString=" {0:#,#} " 
                        HeaderText="Man Std(Min)" />
                    <asp:BoundField DataField="D" DataFormatString=" {0:#,#} " 
                        HeaderText="Mch Std(Min)" />
                    <asp:BoundField DataField="AP100" HeaderText="AP100" />
                    <asp:BoundField DataField="TA006" 
                        HeaderText="Part Item">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>--%>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Timer ID="Timer1" runat="server" Interval="120000">
    </asp:Timer>
    </form>
</body>
</html>