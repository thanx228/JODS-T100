<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginT100.aspx.vb" Inherits="MIS_T100.LoginT100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>User Login</title>
    
    <style type="text/css">
        .style32
        {
            height: 93px;
        }
        .style33
        {
            height: 20px;
        }
        .style34
        {
            width: 633px;
        }
        .style35
        {
            height: 20px;
            width: 633px;
        }
        .style36
        {
            width: 371px;
        }
        .style37
        {
            height: 20px;
            width: 274px;
        }
        .style38
        {
            width: 274px;
        }
        .auto-style1 {
            width: 128px;
            height: 25px;
        }
        .auto-style2 {
            height: 25px;
        }
        .auto-style3 {
            width: 462px;
            height: 25px;
        }
        .auto-style4 {
            width: 57%;
            height: 100px;
        }
        .auto-style5 {
            width: 814px;
        }
        .auto-style6 {
            height: 20px;
            width: 462px;
        }
        .auto-style7 {
            width: 462px;
        }
        .auto-style8 {
            height: 20px;
            width: 128px;
        }
        .auto-style9 {
            width: 128px;
        }
        .PLbodyLogin{
        width:842px;
        }
    </style>
    
</head>
<body  background="/Images/bg.jpg" runat="server" >
    <form id="lgMember" runat="server">
    <div align="center">
    <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    <ContentTemplate>
 <asp:Panel ID="Panel1" runat="server" CssClass="PLbodyLogin"> 
  <table class="auto-style4" >
            <tr>
                <td style="text-align:center;" 
                    class="style32">
                    <img src="Images/hd.jpg" />
                    </td>
            </tr>
            <tr>
                <td class="style32">
                    <table class="auto-style5">
                        <tr>
                            <td class="auto-style1">
                                </td>
                            <td align="right" style="color: #0000FF" class="auto-style2">
                                User Name</td>
                            <td align="left" class="auto-style3">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style2">
                                </td>
                        </tr>
                        <tr>
                            <td class="auto-style8">
                            </td>
                            <td align="right" class="style33" style="color: #0000FF; font-size: small">
                                Password</td>
                            <td align="left" class="auto-style6">
                                <asp:TextBox ID="PassWord" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                            <td class="style33">
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style9">
                                &nbsp;</td>
                            <td align="right">
                                &nbsp;</td>
                            <td align="left" class="auto-style7">
                                <asp:Button ID="Btlogin" runat="server" CssClass="menu" Text="  Login" 
                                    Width="100px" />
                                <asp:Label ID="lbError" runat="server" ForeColor="#CC0066"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

     </asp:Panel>        
    </ContentTemplate> 
      </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
