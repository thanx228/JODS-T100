<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="FGLabel.aspx.vb" Inherits="MIS_T100.FGLabel" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/Date.ascx" tagname="Date" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<%@ Register src="../UserControl/Normal/UsingMO_Type.ascx" tagname="UsingMO_Type" tagprefix="uc3" %>
<%@ Register src="../UserControl/Normal/UsingWorkstation.ascx" tagname="UsingWorkstation" tagprefix="uc4" %>
<%@ Register src="../UserControl/docTypeD.ascx" tagname="docTypeD" tagprefix="uc5" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style8
        {
            height: 21px;
            width: 141px;
        }
        .style33
        {
            width: 237px;
            height: 21px;
        }
        .style35
        {
            height: 21px;
            width: 160px;
        }
        .style10
        {
            height: 21px;
            }
        .style14
        {
            width: 141px;
            height: 26px;
        }
        .style15
        {
            width: 237px;
            height: 26px;
        }
        .style36
        {
            width: 160px;
            height: 26px;
        }
        .style17
        {
            height: 26px;
        }
        .style34
        {
            width: 237px;
        }
        .style37
        {
            width: 160px;
        }
        .auto-style8 {
            height: 21px;
            width: 29%;
        }
        .auto-style9 {
            height: 21px;
            width: 21%;
        }
        .auto-style12 {
            height: 26px;
            width: 29%;
        }
        .auto-style15 {
            width: 29%;
        }
        .auto-style16 {
            height: 21px;
            width: 120px;
        }
        .auto-style17 {
            width: 32%;
            height: 21px;
        }
        .auto-style18 {
            width: 120px;
            height: 26px;
        }
        .auto-style19 {
            width: 32%;
            height: 26px;
        }
        .auto-style20 {
            width: 120px;
        }
        .auto-style21 {
            width: 32%;
        }
        </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShowNew.ClientID %>').gridviewScroll({
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
        function gridviewScrollShowDel() {
            gridView1 = $('#<%= gvShowDel.ClientID %>').gridviewScroll({
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
            <uc1:HeaderForm ID="ucHeader" runat="server" />
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td bgcolor="White">
                                <asp:Label ID="Label3" runat="server" Text="Item"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbItem" runat="server"></asp:TextBox>
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="Label4" runat="server" Text="Spec"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbSpec" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="White">
                                <asp:Label ID="Label5" runat="server" Text="Cust Code"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:TextBox ID="tbCustCode" runat="server"></asp:TextBox>
                            </td>
                            <td bgcolor="White">&nbsp;</td>
                            <td bgcolor="White">&nbsp;</td>
                        </tr>
                        <tr>
                            <td bgcolor="White">
                                <asp:Label ID="lbDate" runat="server"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <uc2:Date ID="ucDate" runat="server" />
                            </td>
                            <td bgcolor="White">
                                <asp:Label ID="Label8" runat="server" Text="App. Status"></asp:Label>
                            </td>
                            <td bgcolor="White">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="N,Y,S">ALL</asp:ListItem>
                                    <asp:ListItem Value="N">Un Confirm</asp:ListItem>
                                    <asp:ListItem Value="Y" Selected="True">Confirmed</asp:ListItem>
                                    <asp:ListItem Value="S">Post</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Height="200px" Width="75%">
                        <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                            <HeaderTemplate>
                                Mo Receipt
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Process"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProcessMO" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="asft335">Transfer Order</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="asft340">MO Receipt</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label10" runat="server" Text="WC From"></asp:Label>
                                        </td>
                                        <td>
                                            <uc4:UsingWorkstation ID="ucWC" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbDocType" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMoRType" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="MO Type"></asp:Label>
                                        </td>
                                        <td>
                                            <uc3:UsingMO_Type ID="ddlMoType" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbDocNo" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMoRNo" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="MO No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMoNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbDocSeq" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMoRSeq" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="MO Seq"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbMOSeq" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                            <HeaderTemplate>
                                Sale Delivery
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Sale Delivery Type"></asp:Label>
                                        </td>
                                        <td>
                                            <uc5:docTypeD ID="ucSaleDelType" runat="server" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Sale Delivery No"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbSaleDelNo" runat="server"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Sale Delivery Seq"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbSaleDelSeq" runat="server"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    <table style="width: 75%;">
                        <tr>
                            <td align="center" style="background-image: url('../Images/btt.jpg'); background-repeat: repeat-x">&nbsp;<asp:Button ID="btSearch" runat="server" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <table style="width: 75%;">
                        <tr>
                            <td class="auto-style16" style="background-color: #FFFFFF">Doc. Record</td>
                            <td class="auto-style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbDateRec" runat="server" BackColor="#FFCC99" BorderColor="#C4C4C4" BorderWidth="1px" Font-Bold="True"></asp:Label>
                            </td>
                            <td class="style35" colspan="2" style="background-color: #FFFFFF">Item</td>
                            <td class="auto-style8" style="background-color: #FFFFFF">
                                <asp:Label ID="lbItem" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style16" style="background-color: #FFFFFF">
                                <asp:Label ID="lbRef" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbMoR" runat="server" BackColor="#FFCC99" BorderColor="#C4C4C4" BorderWidth="1px" Font-Bold="True"></asp:Label>
                                &nbsp;</td>
                            <td class="style35" colspan="2" style="background-color: #FFFFFF">Item Desc.</td>
                            <td class="auto-style8" style="background-color: #FFFFFF">
                                <asp:Label ID="lbDesc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style16" style="background-color: #FFFFFF">MO Type-No</td>
                            <td class="auto-style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbMo" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style9" style="background-color: #FFFFFF">Item Spec</td>
                            <td class="style10" colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="lbSpec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style18" style="background-color: #FFFFFF">SO Type-No</td>
                            <td class="auto-style19" style="background-color: #FFFFFF">
                                <asp:Label ID="lbSo" runat="server"></asp:Label>
                            </td>
                            <td class="style36" colspan="2" style="background-color: #FFFFFF">&nbsp;</td>
                            <td class="auto-style12" style="background-color: #FFFFFF">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style18" style="background-color: #FFFFFF">
                                <asp:Label ID="Label21" runat="server" Text="PO"></asp:Label>
                            </td>
                            <td class="auto-style19" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbPO" runat="server"></asp:TextBox>
                                <asp:Label ID="lbPO" runat="server"></asp:Label>
                            </td>
                            <td class="style36" colspan="2" style="background-color: #FFFFFF">
                                <asp:Label ID="Label20" runat="server" Text="Serial No(AMP)."></asp:Label>
                            </td>
                            <td class="auto-style12" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbSerialNo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style16" style="background-color: #FFFFFF">Qty</td>
                            <td class="auto-style17" style="background-color: #FFFFFF">
                                <asp:Label ID="lbQty" runat="server"></asp:Label>
                            </td>
                            <td class="style35" colspan="2" style="background-color: #FFFFFF">Item Weight</td>
                            <td class="auto-style8" style="background-color: #FFFFFF">
                                <asp:Label ID="lbItemWgh" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Qty/Carton</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbQtyCtn" runat="server" CssClass="numberOnly" Width="50px"></asp:TextBox>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Carton Wight</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:TextBox ID="tbCtnWgh" runat="server" CssClass="numberOnly" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btCal1" runat="server" Text="Cal" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Carton No.</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:TextBox ID="tbCtnNo" runat="server"></asp:TextBox>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Carton Spec</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:Label ID="lbCtnSpec" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Full Box</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFull" runat="server"></asp:Label>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Not Full Box</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:Label ID="lbNotFull" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Full Box Net Wight</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFullN" runat="server"></asp:Label>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Not Full Box Net Wight</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:Label ID="lbNotFullN" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Full Box Gross Wight</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:Label ID="lbFullG" runat="server"></asp:Label>
                                <br />
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Not Full Box Gross Wight</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:Label ID="lbNotFullG" runat="server"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20" style="background-color: #FFFFFF">Batch No</td>
                            <td class="auto-style21" style="background-color: #FFFFFF">
                                <asp:Label ID="lbBatch" runat="server"></asp:Label>
                            </td>
                            <td class="style37" colspan="2" style="background-color: #FFFFFF">Pack By(Emp Code(,) )</td>
                            <td style="background-color: #FFFFFF" class="auto-style15">
                                <asp:TextBox ID="tbPack" runat="server" Width="208px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" colspan="5" style="background-color: #FFFFFF">
                                <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 75%; background-image: url('../Images/btt.jpg'); background-repeat: repeat-x;">
                        <tr>
                            <td align="center" style="background-image: url('http://localhost:49931/Images/btt.jpg'); background-repeat: no-repeat">&nbsp;&nbsp;&nbsp;<asp:Button ID="btSave" runat="server" Autopostback="false" Text="Save &amp; Preview" UseSubmitBehavior="true" />
                                &nbsp;<asp:Button ID="btPrint" runat="server" Text="Preview" />
                                &nbsp;<asp:Button ID="btCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <uc6:CountRow ID="ucRowCount" runat="server" />
            <asp:GridView ID="gvShowNew" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="OnView2" HeaderText="View" ImageUrl="~/Images/imagesview.jpg" Text="View">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="A" HeaderText="Doc No" />
                    <asp:BoundField DataField="B" HeaderText="MO Reipt Type/No/Seq" />
                    <asp:BoundField DataField="C" HeaderText="MO Type/No" />
                    <asp:BoundField DataField="D" HeaderText="SO Type/No/Seq" />
                    <asp:BoundField DataField="E" HeaderText="Cust PO" />
                    <asp:BoundField DataField="F" HeaderText="Item" />
                    <asp:BoundField DataField="G" HeaderText="Desc" />
                    <asp:BoundField DataField="H" HeaderText="Cust Spec" />
                    <asp:BoundField DataField="I" DataFormatString="{0:#,#}" HeaderText="MO Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="J" DataFormatString="{0:#,#}" HeaderText="Comp. Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="K" DataFormatString="{0:#,#}" HeaderText="Scarp Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="L" HeaderText="Customer" />
                    <asp:BoundField DataField="M" HeaderText="MO Status" />
                    <asp:BoundField DataField="N" DataFormatString="{0:#,#.###}" HeaderText="Item Wght">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="O" DataFormatString="{0:#,#}" HeaderText="MO Receipt Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="P" DataFormatString="{0:#,#}" HeaderText="Qty/Carton">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Q" DataFormatString="{0:#,#.###}" HeaderText="Carton  Wght">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="R" HeaderText="Carton Item" />
                    <asp:BoundField DataField="S" HeaderText="Carton Spec" />
                    <asp:BoundField DataField="T" HeaderText="Batch No" />
                    <asp:BoundField DataField="Z" HeaderText="Pack By" />
                    <asp:BoundField DataField="SERAIL" HeaderText="Serail No" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:GridView ID="gvShowDel" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:ButtonField ButtonType="Image" CommandName="OnView" HeaderText="View" ImageUrl="~/Images/imagesview.jpg" />
                    <asp:BoundField DataField="DocNo" HeaderText="Doc No" />
                    <asp:BoundField DataField="TH00123" HeaderText="Sale Del Type-No-Seq" />
                    <asp:BoundField DataField="TH01456" HeaderText="SO Type-No-Seq" />
                    <asp:BoundField DataField="TH030" HeaderText="Cust PO" />
                    <asp:BoundField DataField="TH004" HeaderText="Item" />
                    <asp:BoundField DataField="TH005" HeaderText="Desc" />
                    <asp:BoundField DataField="TH019" HeaderText="Cust Spec" />
                    <asp:BoundField DataField="TH008" DataFormatString="{0:N0}" HeaderText="Ship Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TG004" HeaderText="Customer" />
                    <asp:BoundField DataField="MB014" DataFormatString="{0:N3}" HeaderText="Item Wgh">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qtyCtn" DataFormatString="{0:N0}" HeaderText="Qty/Carton">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CtnWgh" DataFormatString="{0:N3}" HeaderText="Carton Wgh">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CtnNo" HeaderText="Carton Item" />
                    <asp:BoundField DataField="CtnSpec" HeaderText="Carton Spec" />
                    <asp:BoundField DataField="TH017" HeaderText="Batch No" />
                    <asp:BoundField DataField="PackBy" HeaderText="Pack By" />
                    <asp:BoundField DataField="SERAIL" HeaderText="Serail" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" HorizontalAlign="Center" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
