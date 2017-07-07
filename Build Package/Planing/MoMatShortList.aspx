<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MoMatShortList.aspx.vb" Inherits="MIS_T100.MOMaterialShort" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>
<%@ Register Src="~/UserControl/DateT100.ascx" TagPrefix="uc1" TagName="Date" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
    {
        width: 282px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        &nbsp;</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label16" runat="server" Text="WH Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:DropDownList ID="ddlTypeMat" runat="server">
                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                            <asp:ListItem Value="4">SP</asp:ListItem>
                            <asp:ListItem Value="1">RM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label17" runat="server" Text=" MO Type"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Produce Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label7" runat="server" Text="Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label12" runat="server" Text="Mat Item"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbMatItem" runat="server"></asp:TextBox>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label14" runat="server" Text="Mat Spec"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:TextBox ID="tbMatSpec" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label5" runat="server" Text="Plan Start Date FM"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                    <%--    <asp:TextBox ID="tbDateFrom" runat="server" Width="80px"></asp:TextBox>--%>
                        <uc1:Date runat="server" ID="DateFrom" />                    
                    </td>
                    <td style="background-color: #FFFFFF">
                        <asp:Label ID="Label8" runat="server" Text="Date To"></asp:Label>
                    </td>
                    <td style="background-color: #FFFFFF">
                        <%--<asp:TextBox ID="tbDateTo" runat="server" Width="80px"></asp:TextBox>--%>
                        <uc1:Date runat="server" ID="ToDate" />
                    </td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Export Excel" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White"  AutoGenerateColumns="false"
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="1800">
                <Columns>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplShow" runat="server" Target="_blank">Detail</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mat' ItemNo">
                        <ItemTemplate>
                         <asp:Label ID="lblBOMitemNo" runat="server" Text='<%#Eval("sfba005")%>'></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mat' Name">
                        <ItemTemplate>
                         <asp:Label ID="lblBOMitemName" runat="server"  ></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mat' Spec">
                        <ItemTemplate>
                         <asp:Label ID="lblBOMitemSpec" runat="server"  ></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stock-in">
                        <ItemStyle  HorizontalAlign="Right"/>
                        <ItemTemplate>
                         <asp:Label ID="lblStock" runat="server" Text='<%#Eval("Stock_in", "{0:N3}")%>' ></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WH">
                        <ItemTemplate>
                         <asp:Label ID="lblWH" runat="server" Text='<%#Eval("inbc005")%>'></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PR-Qty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblPRqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PO-Qty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblPOqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uint">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblStcokinUint" runat="server" Text='<%#Eval("inbc009")%>'></asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-DocNo.">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblMONo" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Qty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblMOqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std-IssueQty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblStdIssueqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IssueQty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblIssueqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UnIssueQty">
                         <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                         <asp:Label ID="lblUnIssueqty" runat="server"> </asp:Label>               
                        </ItemTemplate>
                    </asp:TemplateField>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
