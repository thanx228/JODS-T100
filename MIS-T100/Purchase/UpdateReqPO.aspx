<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="UpdateReqPO.aspx.vb" Inherits="MIS_T100.UpdateReqPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-bottom: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>          
            <table style="width:75%;">
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label3" runat="server" Text="P/R Location"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:DropDownList ID="ddlPrLoc" runat="server" AutoPostBack="True" CssClass="auto-style1">
                            <asp:ListItem Value="1">Fix Asset PR</asp:ListItem>
                            <asp:ListItem Value="2">Purchase Req.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label5" runat="server" Text="P/R NO"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbPrNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label4" runat="server" Text="P/R Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                       
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                       
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="Label11" runat="server" Text="PO Seq"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:TextBox ID="tbPrSeq" runat="server" Width="50px"></asp:TextBox>
                        &nbsp;<asp:Button ID="btCheck" runat="server" Text="Check" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label6" runat="server" Text="Require 1"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:TextBox ID="tbRequire1" runat="server" TextMode="MultiLine" Width="445px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label7" runat="server" Text="Require 2"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:TextBox ID="tbRequire2" runat="server" TextMode="MultiLine" Width="445px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label8" runat="server" Text="Require 3"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="3">
                        <asp:TextBox ID="tbRequire3" runat="server" TextMode="MultiLine" Width="445px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="White">
                        <asp:Label ID="Label9" runat="server" Text="PO Type"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        <asp:Label ID="lbPoTypeNo" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                    <td bgcolor="White">
                        &nbsp;</td>
                    <td bgcolor="White">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        &nbsp;<asp:Button ID="btUpdate" runat="server" Text="Update" />
                        &nbsp;<asp:Button ID="btReset" runat="server" Text="Reset" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

