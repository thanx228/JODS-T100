<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="PlanScheduleReport.aspx.vb" Inherits="MIS_T100.PlanScheduleReport" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/DateT100.ascx" tagname="Date" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/Multiple/UsingMOTypeCheckList.ascx" TagPrefix="uc1" TagName="UsingMOTypeCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvShow() {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <table style="width: 1200px;">
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label6" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <uc1:UsingMOTypeCheckList runat="server" ID="UsingMOTypeCheckList" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="Date From"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc3:Date ID="txtDateFrom" runat="server" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc3:Date ID="txtDateTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" style="vertical-align: top">
                        <asp:Label ID="Label5" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <uc1:UsingWorkstationCheckList runat="server" ID="UsingWorkstationCheckList" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCountRow" runat="server" />
            <%--<div style="background-color:white; width:1500 px ">--%>
            <asp:GridView ID="gvShow" runat="server" AllowPaging="True" PageSize="50" PagerSettings-FirstPageText="First" AlternatingRowStyle-Wrap="false"
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" >
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
                <%--</div>--%>
              <%--<asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="true" ></asp:GridView>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>
