<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test.aspx.vb" Inherits="MIS_T100.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server"   >
         </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="Button" />
        &nbsp;
        <asp:Button ID="Button2" runat="server" Text="send" />
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
