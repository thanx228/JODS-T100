<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master"
 EnableEventValidation = "false"  CodeBehind="PlanByMO.aspx.vb" Inherits="MIS_T100.PlanByMO" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc3" TagName="UsingMO_Type" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .style6
        {
            height: 21px;
        }
    .auto-style1 {
        width: 125px;
    }
    .auto-style2 {
        width: 168px;
    }
    .auto-style3 {
        width: 128px;
    }
    .auto-style4 {
        width: 146px;
    }
    .auto-style5 {
        width: 72px;
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
                height: 400,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 7,
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
          <uc1:HeaderForm ID="ucHeader" runat="server" />
            <asp:Label ID="lblSql" runat="server" ></asp:Label>
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label3" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <%--<uc3:docTypeD ID="ucDocType" runat="server" />--%>
                        <uc3:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbMO" runat="server"></asp:TextBox>
                        <asp:Label ID="lblReplace" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblCkCust" runat="server" Visible="false" ></asp:Label>
                        &nbsp;<asp:Label ID="lblSaleDocNo" runat="server" Text="" Visible="false"></asp:Label>
                         &nbsp;<asp:Label ID="lblStartDate" runat="server" Text=""  Visible="false" ></asp:Label>
                         &nbsp;<asp:Label ID="lblEndDate" runat="server" Text=""  Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btChk" runat="server" Text="Progress" />
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Action Plan" />
                        &nbsp;<asp:Button ID="btSave" runat="server" Text="Save" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server">
                <table style="width: 75%;">
                    <tr>
                        <td bgcolor="White" class="auto-style1">
                            <asp:Label ID="Label5" runat="server" Text="MO"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style2">
                            <asp:Label ID="lbMO" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style3">
                            <asp:Label ID="Label7" runat="server" Text="Product Item"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style4">
                            <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style5">&nbsp;</td>
                        <td bgcolor="White">&nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style1">
                            <asp:Label ID="Label9" runat="server" Text="Item Name"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style2">
                            <asp:Label ID="lbDesc" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style3">
                            <asp:Label ID="Label11" runat="server" Text="Specifaction"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style4">
                            <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style5">&nbsp;</td>
                        <td bgcolor="White">&nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style1">
                            <asp:Label ID="Label13" runat="server" Text="Cust Name"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style2">
                            <asp:Label ID="lbCustName" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style3">
                            <asp:Label ID="Label15" runat="server" Text="Batch No"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style4">
                            <asp:Label ID="lbBatch" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style5">&nbsp;</td>
                        <td bgcolor="White">&nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="White" class="auto-style1">
                            <asp:Label ID="Label17" runat="server" Text="Production Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style2">
                            <asp:Label ID="lbQty" runat="server" ForeColor="Blue"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblUnit" runat="server"  Text=""></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style3">
                            <asp:Label ID="lbl555" runat="server" Text="Complete Qty"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style4">
                            <asp:Label ID="lblCompleteQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                        <td bgcolor="White" class="auto-style5">
                            <asp:Label ID="lbl556" runat="server" Text="Scarp Qty"></asp:Label>
                        </td>
                        <td bgcolor="White">
                            <asp:Label ID="lblScarpQty" runat="server" ForeColor="Blue"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--<br />--%>
            <%--<hr style="color:white;" />--%>
            <uc2:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="sfcb011" HeaderText="W/C" />
                    <asp:BoundField DataField="sfcb002" HeaderText="LineNo" />
                    <asp:BoundField DataField="sfcb003" HeaderText="Operation" />
                    <asp:BoundField DataField="oocql004" HeaderText="opt Desc" />
                    <asp:BoundField DataField="sfcb044" HeaderText="Plan Start" DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField DataField="sfcb045" HeaderText="Plan Compete" DataFormatString="{0:yyyy/MM/dd}" />
                    <asp:BoundField DataField="" DataFormatString="{0:N0}" HeaderText="Plan Bal" />
                    <asp:TemplateField HeaderText="MC No./Batch No.">
                        <ItemTemplate>
                            <asp:TextBox ID="tbMC" runat="server" Width="120px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 1">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate1" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 2">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate2" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 3">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate3" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 4">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate4" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 5">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate5" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 6">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate6" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 7">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate7" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 8">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate8" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 9">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate9" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 10">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate10" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 11">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate11" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 12">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate12" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 13">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate13" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 14">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate14" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 15">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate15" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 16">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate16" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 17">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate17" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 18">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate18" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 19">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate19" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 20">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate20" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 21">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate21" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 22">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate22" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 23">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate23" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 24">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate24" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 25">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate25" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 26">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate26" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 27">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate27" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 28">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate28" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 29">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate29" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 30">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate30" runat="server" Width="50px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day 31">
                        <ItemTemplate>
                            <asp:TextBox ID="tbDate31" runat="server" Width="50px"></asp:TextBox>
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
    <%--        <asp:GridView ID="gvCheck" runat="server" BackColor="White" BorderColor="#3366CC" 
                 BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False">
                <Columns>
                     <asp:BoundField DataField="sfcb002" HeaderText="LineNo" >
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb003" HeaderText="Operation" >
                        <HeaderStyle Width="40" />
                        <ItemStyle Width="40" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb011" HeaderText="W/C" >
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb027" HeaderText="Standrad Output" >
                        <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb050" HeaderText="WIP Qty" >
                         <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb028" HeaderText="Good Transfer-in" >
                         <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb033" HeaderText="Good Transfer-Out" >
                         <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfca004" HeaderText="Complete Qty" >
                         <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb036" HeaderText="Direct Scap" >
                         <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="ReWork" >
                      </asp:BoundField>
                    <asp:BoundField DataField="sfcb029" HeaderText="Re-Work Trs-in" >
                       <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Bal Plan" />                    
                     <asp:BoundField DataField="sfcb044" HeaderText="PlanStart " DataFormatString="{0:dd-MM-yyyy}"   >                   
                     <HeaderStyle Width="70px" />
                     <ItemStyle Width="70px" />
                     </asp:BoundField> 
                    <asp:BoundField DataField="sfcb045" HeaderText="PlanComplete " DataFormatString="{0:dd-MM-yyyy}"   >                   
                     <HeaderStyle Width="70px" />
                     <ItemStyle Width="70px" />
                     </asp:BoundField>               
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>--%>
            <asp:GridView ID="gvCheck" runat="server"></asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
