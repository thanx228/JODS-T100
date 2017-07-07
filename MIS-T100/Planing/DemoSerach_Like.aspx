<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DemoSerach_Like.aspx.vb" Inherits="MIS_T100.DemoSerach_Like" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <asp:GridView ID="gvEmp" runat="server"></asp:GridView>
    <asp:GridView ID="gvDept" runat="server"></asp:GridView>
    <asp:GridView ID="gvJoin" runat="server"></asp:GridView>
</div>
<div>
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
</div>
<hr />
    <div>
        <a href="Class/T100/HttpErrorClassT100.aspx">Class/T100/HttpErrorClassT100.aspx</a>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtSearch2" runat="server"></asp:TextBox>
        <asp:Button ID="btnSeacth" runat="server" Text="Search" />
        <br />
        <asp:Label ID="lblSql" runat="server" Text=""></asp:Label>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
