<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="moStatusNotClosed.aspx.vb" Inherits="MIS_T100.moStatusNotClosed" %>

<%@ Register Src="../UserControl/HeaderForm.ascx" TagName="HeaderForm" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CountRow.ascx" TagName="CountRow" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Date.ascx" TagName="Date" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/Multiple/UsingWorkstationCheckList.ascx" TagPrefix="uc1" TagName="UsingWorkstationCheckList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style6 {
            height: 20px;
        }

        .style7 {
            height: 20px;
            width: 145px;
        }

        .style8 {
            width: 145px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollgvDetail() {
            gridView1 = $('#<%= gvDetail.ClientID %>').gridviewScroll({
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
                barsize: 12
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:HeaderForm ID="HeaderForm1" runat="server" />
            <table style="width: 101%;">
                <tr>
                    <td bgcolor="White" class="style7">
                        <asp:Label ID="Label3" runat="server" Text="Plan Comp Date"></asp:Label>
                    </td>
                    <td bgcolor="White" class="style6">
                        <asp:DropDownList ID="DL_Month" runat="server" Width="70">
                            <asp:ListItem Value="01">JAN</asp:ListItem>
                            <asp:ListItem Value="02">FEB</asp:ListItem>
                            <asp:ListItem Value="03">MAR</asp:ListItem>
                            <asp:ListItem Value="04">APR</asp:ListItem>
                            <asp:ListItem Value="05">MAY</asp:ListItem>
                            <asp:ListItem Value="06">JUN</asp:ListItem>
                            <asp:ListItem Value="07">JUL</asp:ListItem>
                            <asp:ListItem Value="08">AUG</asp:ListItem>
                            <asp:ListItem Value="09">SEP</asp:ListItem>
                            <asp:ListItem Value="10">OCT</asp:ListItem>
                            <asp:ListItem Value="11">NOV</asp:ListItem>
                            <asp:ListItem Value="12">DEC</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <asp:DropDownList ID="DL_Year" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td bgcolor="White" class="style6"></td>
                </tr>
                <tr>
                    <td bgcolor="White" style="vertical-align: top;">
                        <asp:Label ID="Label4" runat="server" Text="Work Center"></asp:Label>
                    </td>
                    <td bgcolor="White" colspan="2">
                        <uc1:UsingWorkstationCheckList runat="server" ID="UsingWorkstation" />
                    </td>
                </tr>
            </table>
            <table style="width: 75%;">
                <tr>
                    <td align="center"
                        style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">
                        <asp:Button ID="btShow" runat="server" Text="Show Report" />
                        &nbsp;<asp:Button ID="btExport" runat="server" Text="Excel Export" />
                    </td>
                </tr>
            </table>
            <uc2:CountRow ID="ucCountRowSum" runat="server" />
            <asp:GridView ID="gvSum" runat="server" BackColor="White" BorderColor="#3366CC"
                 BorderStyle="None" BorderWidth="1px" CellPadding="4" AlternatingRowStyle-Wrap="false">
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
            <uc2:CountRow ID="ucCountRowDetail" runat="server" />
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" BackColor="White" 
               Width="3000"  BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:TemplateField HeaderText="MO PlanComplete">
                        <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblMOplancomplete" runat="server" Text='<%#Eval("sfaa020", "{0:yyyy/MM/dd}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Process PlanComplete">
                        <ItemStyle  HorizontalAlign="center"/>
                        <ItemTemplate>
                            <asp:Label ID="lblProcessPlancomplete" runat="server" Text='<%#Eval("sfcb045", "{0:yyyy/MM/dd}")%>'  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Status">
                        <ItemTemplate>
                            <asp:Label ID="lblMOstatus" runat="server" Text='<%#Eval("sfaastus")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Type">
                        <ItemTemplate>
                            <asp:Label ID="lblMOtype" runat="server"  Text='<%#Eval("MoType")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO DocNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblMoDocno" runat="server" Text='<%#Eval("sfcbdocno")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemNo.">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemNo" runat="server" Text='<%#Eval("sfaa010")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ProductionItemName">
                        <ItemTemplate>
                            <asp:Label ID="lblProductionItemName" runat="server" Text='<%#Eval("imaal003")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec.">
                        <ItemTemplate>
                            <asp:Label ID="lblSpec" runat="server" Text='<%#Eval("imaal004")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Workstation">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblWorkstation" runat="server" Text='<%#Eval("sfcb011")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkstationName" runat="server" Text='<%#Eval("ecaa002")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemSeq.">
                         <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblItemSeq" runat="server" Text='<%#Eval("sfcb002")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Op_Id">
                         <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblOpId" runat="server" Text='<%#Eval("sfcb003")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Operation">
                        <ItemTemplate>
                            <asp:Label ID="lblOperation" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plan Qty">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblPlanqty" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO Qty">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblMOqty" runat="server" Text='<%#Eval("sfaa012", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CompleteQty.">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblCompleteQty" runat="server" Text='<%#Eval("sfca004", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ScarpQty.">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblScarpQty" runat="server" Text='<%#Eval("sfaa056", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WIP">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblWIP" runat="server" Text='<%#Eval("sfcb050", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Good.Trs-in">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblGoodTrsIn" runat="server" Text='<%#Eval("sfcb028", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Good.Trs-Out">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblGoodTrsOut" runat="server" Text='<%#Eval("sfcb033", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rework.Trs-in">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblReworkTrsIn" runat="server" Text='<%#Eval("sfcb029", "{0:N3}")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rework.Trs-Out">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblReworkTrsOut" runat="server" Text='<%#Eval("sfcb034", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DirecScarp">
                         <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="lblDirecScarp" runat="server"  Text='<%#Eval("sfcb036", "{0:N3}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                         <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblUint" runat="server"  Text='<%#Eval("sfcb052")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SaleOrder No.">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleOrderNo" runat="server"  Text='<%#Eval("xmdcdocno")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomer" runat="server"  Text='<%#Eval("xmda004")%>'></asp:Label>
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
            <br /><br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
