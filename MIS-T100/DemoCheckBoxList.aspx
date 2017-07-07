<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DemoCheckBoxList.aspx.vb" Inherits="MIS_T100.DemoCheckBoxList" %>

<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>
<%@ Register Src="~/UserControl/Multiple/UsingStatusMO_Normal_checkList.ascx" TagPrefix="uc1" TagName="UsingStatusMO_Normal_checkList" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <asp:Label ID="lblsql" runat="server" ></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
</div>
    <div>
        <uc1:UsingWorkstationCheckList runat="server" ID="UsingWorkstationCheckList" />
        <hr />
        <asp:CheckBoxList ID="cblCodeType" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
             style="margin-left: 0px; margin-right: 0px" Width="250px">
                                <asp:ListItem Value="1">Materials</asp:ListItem>
                                <asp:ListItem Value="2">Finished Goods</asp:ListItem>
                                <asp:ListItem Value="3">Semi FG</asp:ListItem>
                                <asp:ListItem Value="4">Spare Part and Another</asp:ListItem>
         </asp:CheckBoxList>
        <uc1:UsingStatusMO_Normal_checkList runat="server" ID="UsingStatusMO_Normal_checkList" />
        <asp:Label ID="lblselect" runat="server" ></asp:Label>
        <hr />

        <asp:Button ID="btnCheckAll" runat="server" Text="Check All" />
        <asp:Button ID="btnUncheckAll" runat="server" Text=" UnCheck All" />
         <asp:Button ID="btnCallData" runat="server" Text=" ShowData" />
        <asp:Label ID="lblParameter" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
