<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ManualCloseRequest.aspx.vb" Inherits="MIS_T100.ManualCloseRequest" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc3" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>
<%@ Register Src="~/UserControl/Normal/UsingMO_Type.ascx" TagPrefix="uc1" TagName="UsingMO_Type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6
        {
            width: 108px;
        }
        .style8
        {
            width: 108px;
            height: 26px;
        }
        .style9
        {
            height: 26px;
        }
    .style1
    {
        width: 282px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc3:HeaderForm ID="HeaderForm1" runat="server" />
            <table bgcolor="White" style="width: 75%;">
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label4" runat="server" Text="MO Type"></asp:Label>
                    </td>
                    <td>
                        <uc1:UsingMO_Type runat="server" ID="UsingMO_Type" />
                        <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label5" runat="server" Text="MO No."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                        <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label6" runat="server" Text="Reason"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReason" runat="server">
                            <asp:ListItem Value="1">Order Cancle</asp:ListItem>
                            <asp:ListItem Value="3">ECN Change</asp:ListItem>
                            <asp:ListItem Value="3">Other</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label12" runat="server" Text="Page size"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:DropDownList ID="ddlPage" runat="server">
                            <asp:ListItem>A4</asp:ListItem>
                            <asp:ListItem Selected="True" Value="A5">A5(Half A4)</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style6">
                        &nbsp;</td>
                    <td style="color: #FF0000">
                        *** MO Status Not Completed&nbsp; Only ***</td>
                </tr>
            </table>
            <table style="width:75%;">
                <tr>
                    <td style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat" 
                        align="center">
                        <asp:Button ID="btShow" runat="server" 
                            Text="Show Report" Height="30px" />
                        &nbsp;<asp:Button ID="btPrint" runat="server" Text="Print Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc1:CountRow ID="CountRow1" runat="server" />
            <asp:GridView ID="gvShow" runat="server" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="MO-Type" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblMOtype" runat="server" Text='<%#Eval("Motype")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblMOno" runat="server" Text='<%#Eval("MO")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-DocNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblMOdocno" runat="server" Text='<%#Eval("sfbadocno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemNo" runat="server" Text='<%#Eval("sfaa010")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemName" runat="server" Text='<%#Eval("imaal003")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Specifaction">
                        <ItemTemplate>
                            <asp:Label ID="lblSpecifaction" runat="server" Text='<%#Eval("imaal004")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PlanQty">
                        <ItemStyle  HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblPlanQty" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Qty">
                         <ItemStyle  HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblMOqty" runat="server" Text='<%#Eval("sfaa012", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Complete Qty">
                         <ItemStyle  HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblCompleteqty" runat="server" Text='<%#Eval("sfca003", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Scarp Qty">
                         <ItemStyle  HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblScarpQty" runat="server" Text='<%#Eval("sfaa056", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Child LineNo.">
                         <ItemStyle  HorizontalAlign="Center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblChildLineNo" runat="server" Text='<%#Eval("sfbaseq")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Child ItemNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblChildItemNo" runat="server" Text='<%#Eval("sfba005")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Child Item Spec">
                        <ItemTemplate>
                            <asp:Label ID="lblChildItemSpec" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
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
