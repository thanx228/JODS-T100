<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="moGenBatch.aspx.vb" Inherits="MIS_T100.moGenBatch" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc3" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_TypeGenBatch.ascx" TagPrefix="uc1" TagName="UsingMO_TypeGenBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 37px;
        }
        .style7
        {
            width: 125px;
        }
        .style8
        {
            width: 125px;
            height: 30px;
        }
        .style9
        {
            height: 30px;
        }
        .style10
        {
            height: 30px;
            width: 255px;
        }
        .style11
        {
            width: 255px;
        }
        .auto-style1 {
            height: 30px;
            width: 237px;
        }
        .auto-style2 {
            width: 237px;
        }
        .auto-style3 {
            height: 22px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
        function gridviewScrollShow() {
                gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                   // width: 100%,
                    //height: 50%,
                    width: screen.availWidth - 30,
                    height: 400,
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
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White" class="style8">
                        <asp:Label ID="Label3" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style10">
                        <uc2:docTypeD ID="ucDocType" runat="server" />
                    </td>
                    <td bgcolor="White" class="style9">
                        <asp:Label ID="Label4" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style1">
                        <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label21" runat="server" Text="Condition"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style11">
                        <asp:CheckBox ID="cbFA" runat="server" AutoPostBack="True" Text="FA" />
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label20" runat="server" Text="Item Old"></asp:Label>
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:TextBox ID="tbItemOld" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:CheckBox ID="cbRedo" runat="server" AutoPostBack="True" Text="Redo" />
                        <asp:Label ID="lbRedo" runat="server" Visible="False"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style11">
                        <asp:TextBox ID="tbBatchNo" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td bgcolor="White">
                        <asp:CheckBox ID="cbHardTool" runat="server" Text="Hard Tooling" />
                    </td>
                    <td bgcolor="White" class="auto-style2">
                        <asp:Label ID="lbHardTool" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lbItemOld" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x" 
                        class="style6">
                        <asp:Button ID="btShow" runat="server" Text="Show " />
                        &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update PO/SO Seq" Visible="False" />
                        &nbsp;<asp:Button ID="btGen" runat="server" Text="Generate Batch" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                        &nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                    </td>
                </tr>
            </table>
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="SO"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="lbSO" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label13" runat="server" Text="Item"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Desc"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbDesc" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="Batch No"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbBatch" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="MO Status"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbMoStatus" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width:100%;">
                <tr>
                    <td align="center" style="vertical-align: top; background-color: #FFFFFF">
                        <asp:Label ID="Label18" runat="server" Font-Size="1.5em" ForeColor="Blue" 
                            Text="SO"></asp:Label>
                    </td>
                    <td align="center" style="vertical-align: top; background-color: #FFFFFF">
                        <asp:Label ID="Label19" runat="server" Font-Size="1.5em" ForeColor="Blue" 
                            Text="MO"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="vertical-align: top; background-color: #FFFFFF">
                        <uc3:CountRow ID="ucCountRowSO" runat="server" />
                    </td>
                    <td align="center" style="vertical-align: top; background-color: #FFFFFF">
                        <uc3:CountRow ID="ucCountRowShow" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; background-color: #FFFFFF">
                        <asp:GridView ID="gvSO" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4">
                            <Columns>
                                <asp:TemplateField HeaderText="Sel">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbChk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="A" HeaderText="So Seq" />
                                <asp:BoundField DataField="B" HeaderText="Cust PO" />
                                <asp:BoundField DataField="C" DataFormatString="{0:N0}" HeaderText="SO Qty">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="D" HeaderText="Del Date" />
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
                    </td>
                    <td style="vertical-align: top; background-color: #FFFFFF">
                        <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" 
                            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4">
                            <Columns>
                                <asp:BoundField DataField="A" HeaderText="MO" />
                                <asp:BoundField DataField="B" HeaderText="Item" />
                                <asp:BoundField DataField="C" HeaderText="Desc" />
                                <asp:BoundField DataField="D" HeaderText="Spec" />
                                <asp:BoundField DataField="E" DataFormatString="{0:N0}" HeaderText="Plan Qty" />
                                <asp:BoundField DataField="F" HeaderText="Batch No" />
                                <asp:BoundField DataField="G" HeaderText="Cust PO" />
                                <asp:BoundField DataField="H" HeaderText="SO Seq" />
                                <asp:BoundField DataField="I" HeaderText="Lot Control" />
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
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
