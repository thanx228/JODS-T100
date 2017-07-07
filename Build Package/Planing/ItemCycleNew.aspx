<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="ItemCycleNew.aspx.vb" Inherits="MIS_T100.ItemCycleNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            width: 71%;
        }
        .style6
        {
        }
        .style13
        {
            width: 149px;
        }
        .style14
        {
            width: 149px;
            height: 26px;
        }
        .style15
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style4" 
                style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" ForeColor="#CC0000" 
                            Text="Item Cycle Count Card" Font-Bold="True" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 71%; background-color: #FFFFFF;">
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label3" runat="server" Text="Warehourse :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLWh" runat="server" AutoPostBack="True" 
                            DataSourceID="DataSourceWh" DataTextField="Column1" DataValueField="MC001">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style14">
                        <asp:Label ID="Label7" runat="server" Text="Period"></asp:Label>
                    </td>
                    <td class="style15">
                        <asp:DropDownList ID="ddlPreroid" runat="server" DataSourceID="SqlDataSource2" 
                            DataTextField="preriod" DataValueField="preriod">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label8" runat="server" Text="List For"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFor" runat="server">
                            <asp:ListItem Selected="True" Value="1">Check </asp:ListItem>
                            <asp:ListItem Value="2">Audit</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        <asp:Label ID="Label9" runat="server" Text="Print Label Condition"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="cbQty" runat="server" Text="Qty &gt;0" />
                    </td>
                </tr>
            </table>
            <asp:SqlDataSource ID="DataSourceWh" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                SelectCommand=" select MC001,MC001 ++ MC002 from [JINPAO80].[dbo].[CMSMC] where MC005 = 'Y' order by MC001">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DBMISConnectionString %>" 
                
                SelectCommand="SELECT SUBSTRING(RunNo, 1, 6) AS preriod FROM ItemCycle GROUP BY SUBSTRING(RunNo, 1, 6) ORDER BY preriod DESC">
            </asp:SqlDataSource>
            <table style="width: 72%;">
                <tr>
                    <td align="center" 
                        style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                        <asp:Button ID="Busave" runat="server" Text="Generate " />
                        &nbsp;
                        <asp:Button ID="BuSearch" runat="server" Text="Search" />
                        &nbsp;
                        <asp:Button ID="BuPrint" runat="server" style="margin-left: 0px" 
                            Text="Print Label" />
                        &nbsp;
                        <asp:Button ID="BuExcel" runat="server" Text="Print List" />
                        <asp:Button ID="BuEx" runat="server" Text="Excel" />
                    </td>
                </tr>
            </table>
            <table style="width: 72%;">
                <tr>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label4" runat="server" Text="Number Of Item"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="LCount" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                    </td>
                    <td align="center" style="background-color: #FFFFFF">
                        <asp:Label ID="Label6" runat="server" Text="Item"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvShow" runat="server" BackColor="White" AutoGenerateColumns="false" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="WH">
                        <ItemTemplate>
                            <asp:Label ID="lblWH" runat="server" Text='<%#Eval("WH")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bin-Store">
                        <ItemTemplate>
                            <asp:Label ID="lblBin" runat="server" Text='<%#Eval("Bin")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RunNo">
                        <ItemTemplate>
                            <asp:Label ID="lblRunNo" runat="server" Text='<%#Eval("RunNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("Desc")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec">
                        <ItemTemplate>
                            <asp:Label ID="lblSpec" runat="server" Text='<%#Eval("Spec")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit")%>' ></asp:Label>
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
            <asp:PostBackTrigger ControlID="BuEx" />
        </Triggers>

    </asp:UpdatePanel>
   
</asp:Content>
