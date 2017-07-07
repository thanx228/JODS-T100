<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="BillingReceipts.aspx.vb" Inherits="MIS_T100.BillingReceipts" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style3 {
            width: 155px;
        }
        .Calendar {
            background-color: gainsboro;
            color: blue;
            font-family: monospace;
            font-size: 20px;
            border-bottom-color : blue ;
            /*font-weight: bold;*/
        }
        .fontChar {
            /*background-color: gainsboro;*/
            color: black ;
            font-family:  monospace;
            font-size: 14px;
            /*font-weight: bold;*/
        }
        .auto-style10 {
            width: 111px;
        }
        .auto-style11 {
            width: 84px;
        }
        .auto-style12 {
            width: 111px;
            height: 24px;
        }
        .auto-style15 {
            height: 24px;
        }
        .auto-style22 {
            width: 59px;
        }
        .auto-style30 {
            width: 103px;
        }
        .auto-style39 {
            width: 89px;
        }
        .auto-style41 {
            width: 66px;
        }
        .auto-style42 {
            width: 86px;
        }
        .auto-style44 {
            width: 70px;
        }
        .auto-style46 {
            width: 151px;
        }
        .auto-style47 {
            width: 45px;
        }
        .auto-style56 {
            width: 72px;
        }
        .auto-style57 {
            width: 82px;
        }
        .auto-style58 {
            width: 109px;
        }
    </style>

   <%--   <link href="../js/jquery-ui.css" rel="stylesheet" />
      <script src="../js/jquery-1.12.4.js"></script>
      <script src="../js/jquery-ui.js"></script>--%><%--<script type="text/javascript">
 $().ready(
 function(){      
     $('#<%=FromDate.ClientID%>').datepicker({ dateFormat: 'd-M-yy' });   
     $('#<%=ToDate.ClientID%>').datepicker({ dateFormat: 'd-M-yy' }); 
    });
</script>--%>
            <script type="text/javascript">
        function GridSelectAllColumn(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0]; xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++) {
                if (elm[i].type === 'checkbox' && elm[i].checked != xState)
                    elm[i].click();
            }
        }
      
</script>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
 
 <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollgvSelEdit();
            gridviewScrollgvShowEdit1();
            gridviewScrollgvShowDel();
            gridviewScrollgvShowPrint();
        });

        function gridviewScrollgvSelEdit() {
            gridView1 = $('#<%= gvSelEdit.ClientID %>').gridviewScroll({
                width: 800,
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
                barsize: 16
            });
        }
       
     function gridviewScrollgvShowEdit1() {
            gridView1 = $('#<%= gvShowEdit1.ClientID %>').gridviewScroll({
                width:800,
                height: 230,
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
                barsize: 16
            });
     }
     function gridviewScrollgvShowDel() {
            gridView1 = $('#<%= gvShowDel.ClientID %>').gridviewScroll({
                width:800,
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
                barsize: 16
            });
     }

     function gridviewScrollgvShowPrint() {
            gridView1 = $('#<%= gvShowPrint.ClientID %>').gridviewScroll({
                width:800,
                height: 200,
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
                barsize: 16
            });
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
<uc3:HeaderForm ID="HeaderForm1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
 <asp:TabContainer ID="TabContainer1" runat="server" Height="100%"  Width="100%" 
                ActiveTabIndex="2">
     <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="ADD DATA" Height="800">
        <ContentTemplate >
            <asp:Panel ID="Panel7" runat="server" Height="150px"> 
            <table style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666666" >
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label2" runat="server" Text="From Date :" CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style3"><asp:TextBox ID="FromDate" runat="server" CssClass="Calendar" ></asp:TextBox>
                        <asp:CalendarExtender ID="FromDate_CalendarExtender" runat="server" 
                                        Enabled="True" Format="yyyy/MM/dd" TargetControlID="FromDate">
                                    </asp:CalendarExtender></td>
                    <td class="auto-style11"><asp:Label ID="Label1" runat="server" Text="From To :"  CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style3"><asp:TextBox ID="ToDate" runat="server" CssClass="Calendar"></asp:TextBox>
                        <asp:CalendarExtender ID="ToDate_CalendarExtender1" runat="server" 
                                        Enabled="True" Format="yyyy/MM/dd" TargetControlID="ToDate">
                                    </asp:CalendarExtender></td>
                    <td class="auto-style11">
                        <asp:Label ID="Label3" runat="server" Text="SupplierID :"  CssClass="fontChar"></asp:Label></td>
                    <td> <asp:TextBox ID="tbSup" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label5" runat="server" Text="Billing No :"  CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style3">&nbsp;<asp:Label ID="txtbilling" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style11">
                        <asp:Label ID="Label12" runat="server" Text="SupplierID :" CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style3">&nbsp;<asp:Label ID="txtsupid" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label> </td>
                    <td class="auto-style11"><asp:Label ID="Label7" runat="server" Text="Date :" CssClass="fontChar"></asp:Label></td>
                    <td>&nbsp;<asp:Label ID="txtdate" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label></td>
                </tr>
                <tr>
                    <td class="auto-style10">
                        <asp:Label ID="Label8" runat="server" Text="Supp Name :"  CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style3">&nbsp;<asp:Label ID="txtname" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style11"><asp:Label ID="Label9" runat="server" Text="Payment :" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style3"> &nbsp;<asp:Label ID="txtpayment" runat="server" CssClass="fontChar" ForeColor="Blue"></asp:Label></td>
                    <td class="auto-style11">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style12">
                        <asp:Label ID="Label11" runat="server" Text="Address :"  CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style15" colspan="5">&nbsp;<asp:Label ID="txtaddress1" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label></td>
                </tr>
                <tr>
                    <td class="auto-style10"><asp:Label ID="Label4" runat="server" Text="Address :"  CssClass="fontChar"></asp:Label></td>
                    <td colspan="5">&nbsp;<asp:Label ID="txtaddress2" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;
                         <asp:Button ID="Button1" runat="server" Text="Search"  />
                              <asp:Button ID="btSave" runat="server" Text="Save" />
                    </td>
                </tr>
            </table>
            </asp:Panel> 
             <uc2:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShowInv" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server"  AutoPostBack="true"  onclick="GridSelectAllColumn(this);"  />
                            </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate >
                            <asp:CheckBox ID="chkSelect" runat="server"  AutoPostBack="True"/>
                        </ItemTemplate>
                    </asp:TemplateField>  
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
            </asp:GridView>
        </ContentTemplate>
     </asp:TabPanel>  
     <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="EDIT DATA">
        <ContentTemplate >
              <asp:Panel ID="Panel3" runat="server" Height="130px"> 
            <table style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666666" width="800">
                <tr>
                    <td class="auto-style44"> 
                        <asp:Label ID="Label6" runat="server" CssClass="fontChar" Text="SupplierID"></asp:Label>
                    </td>
                    <td class="auto-style30">
                        <asp:TextBox ID="tbSupIDEdit" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="auto-style47"> 
                        <asp:Label ID="Label19" runat="server" CssClass="fontChar" Text="SupplierName"></asp:Label>
                    </td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbSupNameEdit" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style30">
                        &nbsp;</td>
                    <td class="auto-style22">
                        &nbsp;</td>
                </tr>
               
                <tr>
                    <td class="auto-style44">
                        <asp:Label ID="Label10" runat="server" CssClass="fontChar" Text="DateFrom"></asp:Label>
                    </td>
                    <td class="auto-style30">
                        <asp:TextBox ID="tbDateFromEdit" runat="server" CssClass="Calendar" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFromEdit_CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFromEdit">
                        </asp:CalendarExtender>
                    </td>
                    <td class="auto-style47"> 
                        <asp:Label ID="Label13" runat="server" CssClass="fontChar" Text="DateTo"></asp:Label>
                    </td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbDateToEdit" runat="server" CssClass="Calendar" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateToEdit_CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateToEdit">
                        </asp:CalendarExtender>
                          
                    </td>
                    <td class="auto-style30">
                        
                    </td>
                    <td class="auto-style22">&nbsp;</td>
                </tr>              
                <tr>
                    <td class="auto-style44">
                      <asp:Label ID="Label18" runat="server" CssClass="fontChar" Text="BillNo"></asp:Label>
                    </td>
                    <td class="auto-style30">
                        <asp:TextBox ID="tbBillNoEdit" runat="server" Width="100px"></asp:TextBox></td>
                    <td class="auto-style47">
                        &nbsp;</td>
                    <td class="auto-style46">&nbsp;</td>
                    <td class="auto-style30">&nbsp;</td>
                    <td class="auto-style22">&nbsp;</td>
                </tr>
                <tr>
                 <td colspan="6" align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;
                        <asp:Button ID="btSearchEdit_V1" runat="server" Text="Search" />
                    </td>
                    </tr>
            </table>
            </asp:Panel> 
            <asp:Panel ID="Panel1" runat="server" Height="280px"> 
                <asp:Label ID="Label21" runat="server" CssClass="fontChar" Text="amount of rows"></asp:Label> &nbsp;<asp:Label ID="lbCountEdit" runat="server" ForeColor="Blue" CssClass="fontChar" ></asp:Label> 
              <asp:GridView ID="gvShowEdit1" runat="server" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" >
                            <Columns >  
                                <asp:ButtonField  CommandName="Add" ImageUrl="~/Images/edit.gif" Text="Select" ></asp:ButtonField> 
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
            </asp:Panel> 
           <asp:Panel ID="Panel2" runat="server" Height="100px" Visible="False"> 
            <table style="border-top-style: solid; border-top-width: thin; border-top-color: #666666" width="800">
                <tr>
                    <td class="auto-style41">
                        <asp:Label ID="Label14" runat="server" Text="BillNo" CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style42">
                       <asp:Label ID="lbBillNoEdit" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label> </td>
                    <td class="auto-style39">
                        <asp:Label ID="Label16" runat="server" CssClass="fontChar" Text="Date"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbDateEdit" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label> </td>
                </tr>
                <tr>
                    <td class="auto-style41">
                        <asp:Label ID="Label15" runat="server" CssClass="fontChar" Text="SupplierID"></asp:Label>
                    </td>
                    <td class="auto-style42">
                       <asp:Label ID="lbSupEdit" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label> </td>
                    <td class="auto-style39">
                        <asp:Label ID="Label17" runat="server" Text="SupplierName" CssClass="fontChar"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbSupNameEdit" runat="server" ForeColor="Blue" CssClass="fontChar"></asp:Label> </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;
                        <asp:Button ID="btSaveEdit" runat="server" Text="Save" />
                    </td>
                </tr>
            </table>
      
            <asp:Label ID="Label22" runat="server" CssClass="fontChar" Text="amount of rows"></asp:Label>  
            <asp:Label ID="RowsCount" runat="server" ForeColor="Blue" CssClass="fontChar" ></asp:Label> 
                    </asp:Panel> 
            <asp:GridView ID="gvSelEdit" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns >
                     <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate >
                            <asp:CheckBox ID="chkSelectEdit" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>  
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
            </asp:GridView>           
        </ContentTemplate>
     </asp:TabPanel>  
       <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="DELETE DATA">
        <ContentTemplate >

            <table style=" border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666666;" width="800">
                <tr>
                    <td class="auto-style57">
                        <asp:Label ID="Label25" runat="server" Text="SupplierID" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbSupplierIDDel" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                    <td class="auto-style58">
                       <asp:Label ID="Label26" runat="server" Text="SupplierName" CssClass="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbSupplierNameDel" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style57"><asp:Label ID="Label29" runat="server" Text="DateFrom" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbDateFromDel" runat="server" CssClass="fontChar"></asp:TextBox>
                         <asp:CalendarExtender ID="tbDateFromDel_CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFromDel">
                        </asp:CalendarExtender>
                    </td>
                    <td class="auto-style58"><asp:Label ID="Label28" runat="server" Text="DateTo" CssClass="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbDateToDel" runat="server" CssClass="fontChar"></asp:TextBox>
                         <asp:CalendarExtender ID="tbDateToDel_CalendarExtender1" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateToDel">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style57">
                        <asp:Label ID="Label23" runat="server" Text="BillNo" CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbBillNoDel" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                    <td class="auto-style58"> 
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                  <td colspan="4" align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;
                        <asp:Button ID="btSearchDel" runat="server" Text="Search" />
                    </td>
                    </tr>
            </table>    
                       
               <asp:Panel ID="Panel4" runat="server"> 
            <asp:Label ID="Label30" runat="server" Text="amount of rows" CssClass="fontChar"></asp:Label>&nbsp;<asp:Label ID="lbCountShowDel" runat="server" CssClass="fontChar" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvShowDel" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns >  
                                <asp:ButtonField  CommandName="SelDel" Text="Select">
                             

                             
                                </asp:ButtonField> 
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
                </asp:Panel> 
            <br />
            <br />
              <asp:Panel ID="Panel5" runat="server" Visible="False" width="800px"> 
            <table style="width:100%; border-top-style: solid; border-top-width: thin; border-top-color: #666666; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666666;">               
             <tr>
                    <td class="auto-style56">
                        <asp:Label ID="Label24" runat="server" Text="BillNo" CssClass="fontChar"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbBillNoDel" runat="server" CssClass="fontChar" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style56">
                        <asp:Label ID="Label27" runat="server" CssClass="fontChar" Text="Supplier"></asp:Label>
                    </td>
                    <td >
                        <asp:Label ID="lbSupplierDel" runat="server" CssClass="fontChar" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" 
                                style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;
                                 
            <asp:Button ID="btSaveDel" runat="server" Text="Save"/>
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label20" runat="server" Text="amount of rows" CssClass="fontChar"></asp:Label>&nbsp;
            <asp:Label ID="lbCountDel" runat="server" CssClass="fontChar" ForeColor="Blue"></asp:Label>
            <asp:GridView ID="gvDelete" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns >
                     <asp:TemplateField HeaderText="Delete">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate >
                            <asp:CheckBox ID="chkSelectDel" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>  
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle ForeColor="#003399" BackColor="White" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
           </asp:Panel> 
        </ContentTemplate>
       </asp:TabPanel>
       <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="PRINT REPORT">
           <ContentTemplate >
               <table style="width:100%; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #666666;">
                <tr>
                    <td class="auto-style57">
                        <asp:Label ID="Label31" runat="server" Text="SupplierID" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbSupplierIDPrint" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                    <td class="auto-style58">
                       <asp:Label ID="Label32" runat="server" Text="SupplierName" CssClass="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbSupplierNamePrint" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style57"><asp:Label ID="Label33" runat="server" Text="DateFrom" CssClass="fontChar"></asp:Label></td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbDateFromPrint" runat="server" CssClass="fontChar"></asp:TextBox>
                         <asp:CalendarExtender ID="tbDateFromPrint_CalendarExtender" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFromPrint">
                        </asp:CalendarExtender>
                    </td>
                    <td class="auto-style58"><asp:Label ID="Label34" runat="server" Text="DateTo" CssClass="fontChar"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbDateToPrint" runat="server" CssClass="fontChar"></asp:TextBox>
                         <asp:CalendarExtender ID="tbDateToPrint_CalendarExtender" runat="server" Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateToPrint">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style57">
                        <asp:Label ID="Label35" runat="server" Text="BillNo" CssClass="fontChar"></asp:Label>
                    </td>
                    <td class="auto-style46">
                        <asp:TextBox ID="tbBillNoPrint" runat="server" CssClass="fontChar"></asp:TextBox>
                    </td>
                    <td class="auto-style58"> 
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                   <tr>
                       <td class="auto-style57">
                           <asp:Button ID="btSearchPrint" runat="server" Text="Search" />
                       </td>
                       <td colspan="3">
                           <asp:Button ID="Button2" runat="server" Text="Report Pur Month" />
                       </td>
                   </tr>
            </table>
               <asp:Label ID="Label36" runat="server" Text="amount of rows" CssClass ="fontChar"></asp:Label>&nbsp;<asp:Label ID="lbCountPrint" runat="server" ForeColor="Blue" CssClass ="fontChar"></asp:Label>
               <asp:GridView ID="gvShowPrint" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                   <Columns>
                       <asp:ButtonField ButtonType="Image" CommandName="OnClick" HeaderText="Print" 
                                     ImageUrl="~/Images/icon_print.png">
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:ButtonField>
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
               </asp:GridView>   
           </ContentTemplate>
       </asp:TabPanel> 


</asp:TabContainer>             
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
