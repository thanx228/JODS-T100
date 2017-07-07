<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanScheduleListBackup.aspx.vb" Inherits="MIS_T100.PlanScheduleListBackup" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControl/MultipleAutentication/WorkstationAuten.ascx" TagPrefix="uc1" TagName="WorkstationAuten" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 524px;
        }
        .style5
        {
            
        }
        .style7
        {
            height: 19px;
        }
        .style8
        {
            width: 91px;
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
               // freezesize: 3,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 2,
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
            <table style="width: 75%;">
                <tr>
                    <td class="style4" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Label ID="Label5" runat="server" Font-Size="1.2em" ForeColor="Blue" 
                            Text="Plan Schedule List"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblSqlWhere" runat="server" ></asp:Label><br />
             <asp:Label ID="lblSqlWhere2" runat="server" ></asp:Label>
            <table style="width: 101%; background-color: #FFFFFF;">
                <tr>
                    <td class="style8" style="vertical-align: top">
                        <asp:Label ID="Label10" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td>
                        <uc1:UsingWorkstationCheckList runat="server" ID="UsingWC" />
                        <%--<uc1:WorkstationAuten runat="server" ID="WorkstationAuten" />--%>
                        <%--<asp:CheckBoxList ID="cbwc" runat="server"></asp:CheckBoxList>--%>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label6" runat="server" Text="Month"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" runat="server">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label7" runat="server" Text="Year"></asp:Label>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="tbYear" runat="server" Width="50px"></asp:TextBox>--%>
                        <asp:DropDownList ID="DLYear" runat="server"></asp:DropDownList>
                        **Ex. 2013</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" Width="100px" />
                    </td>
                </tr>
            </table>
<%--            <table style="width: 75%;">
                <tr>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Select Month"></asp:Label>
                    </td>
                    <td class="style7" style="background-color: #FFFFFF">
                        <asp:Label ID="lbMonth" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label9" runat="server" Text="Select Year"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="lbYear" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <asp:GridView ID="gvShow" runat="server" 
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4">
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
            <asp:Panel ID="Panel1" runat="server" >
     <%--            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" PagerSettings-FirstPageText="First" PageSize="50" 
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom">--%>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="Workcenter" HeaderText="WC" />
                    <asp:BoundField DataField="PlanDate" HeaderText="PlanDate" />
                    <asp:BoundField DataField="MONo" HeaderText="MO" />
                    <asp:TemplateField HeaderText="WCT100">
                        <ItemTemplate>
                            <asp:Label ID="lblWCT100" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-T100">
                        <ItemTemplate>
                            <asp:Label ID="lblWOT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StdLaborHursT100">
                        <ItemTemplate>
                            <asp:Label ID="lblStdLaborHursT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StdMcHursT100">
                        <ItemTemplate>
                            <asp:Label ID="lblStdMcHursT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
