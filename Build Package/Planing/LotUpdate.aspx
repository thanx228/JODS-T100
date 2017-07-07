<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="LotUpdate.aspx.vb" Inherits="MIS_T100.LotUpdate" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc3" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc4" %>
<%@ Register Src="~/UserControl/Normal/UsingDocTypeSale.ascx" TagPrefix="uc1" TagName="UsingDocTypeSale" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            height: 21px;
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
            <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
            <table style="width: 75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="SO Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc1:UsingDocTypeSale runat="server" ID="UsingDocTypeSale" />   
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />   
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label4" runat="server" Text="SO No"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbSO" runat="server"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label7" runat="server" Text="MO No"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label5" runat="server" Text="SO Seq"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:TextBox ID="tbSoSeq" runat="server" MaxLength="4" Width="50px"></asp:TextBox>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label8" runat="server" Text="Document Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <uc4:Date ID="ucDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White" class="style6">
                        <asp:Label ID="Label9" runat="server" Text="Update Include"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:CheckBox ID="cbQC" runat="server" Text="QC" />
                    </td>
                    <td bgcolor="White" class="style6">
                        &nbsp;</td>
                    <td bgcolor="White" class="style6">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btSearch" runat="server" Text="Search" />
                        &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCountRow" runat="server" />
            <asp:GridView ID="gvShow" runat="server">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
