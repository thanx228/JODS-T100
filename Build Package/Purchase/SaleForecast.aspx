<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleForecast.aspx.vb" Inherits="MIS_T100.SaleForecast" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style7
        {
            width: 159px;
        }
        .style8
        {
            width: 155px;
        }
        .style1
    {
        height: 23px;
    }
    .style1
    {
        width: 282px;
    }
        .style27
        {
            width: 791px;
        }
        .auto-style1 {
            width: 159px;
            height: 30px;
        }
        .auto-style2 {
            height: 30px;
        }
        .auto-style3 {
            width: 155px;
            height: 30px;
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
             <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White" class="style7" style="vertical-align: top">
                        <asp:Label ID="Label4" runat="server" Text="Forecast Type (Channel)"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblForeType" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label15" runat="server" Text="Cust ID"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbCust" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style8">
                        Plan Batch No.</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbPlanBatch" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style1">
                        Forecast Type</td>
                    <td bgcolor="White" class="auto-style2" colspan="3">
                        <asp:CheckBoxList ID="cblSoTypePO" runat="server">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">Forecast Type</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbForNo" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White">Forecast Seq</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbForSeq" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        Forecast Status</td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlForeStatus" runat="server">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem Value="1">Not Close</asp:ListItem>
                            <asp:ListItem Value="2">Close</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label14" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:CheckBox ID="cbNoBOM" runat="server" Text="NO BOM" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label16" runat="server" Text="Property"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:CheckBoxList ID="cblProperty" runat="server" RepeatColumns="5" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">1:PURCHASE</asp:ListItem>
                            <asp:ListItem Value="2">2:MANUFACTURING</asp:ListItem>
                            <asp:ListItem Value="3">3:SUBCONTRACTING</asp:ListItem>
                            <asp:ListItem Value="4">4:NONE</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="auto-style1">
                        <asp:Label ID="Label7" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="auto-style3">
                        <asp:Label ID="Label8" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label9" runat="server" Text="Plan Delivery Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label10" runat="server" Text="Plan Delivery Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        Sale Forecast Date From</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbFCDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbFCDateFrom_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbFCDateFrom">
                        </asp:CalendarExtender>
                    </td>
                    <td bgcolor="White" class="style8">
                        Sale Forecast Date To</td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbFCDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="tbFCDateTo_CalendarExtender" runat="server" 
                            Enabled="True" Format="yyyy/MM/dd" TargetControlID="tbFCDateTo">
                        </asp:CalendarExtender>
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" class="style27" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;<asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

