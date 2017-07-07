<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DetailSOChange.aspx.vb" Inherits="MIS_T100.DetailSOChange" %>

<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SO,SaleForcast Change</title>
        <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
        });
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShow.ClientID %>').gridviewScroll({
                width: screen.availWidth - 50,
                height: 500,
                railcolor: "#F0F0F0",
                barcolor: "#CDCDCD",
                barhovercolor: "#606060",
                bgcolor: "#F0F0F0",
                freezesize: 1,
                arrowsize: 30,
                varrowtopimg: "../Images/arrowvt.png",
                varrowbottomimg: "../Images/arrowvb.png",
                harrowleftimg: "../Images/arrowhl.png",
                harrowrightimg: "../Images/arrowhr.png",
                headerrowcount: 1,
                railsize: 16,
                barsize: 14
            });
        }          
   </script>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
<asp:Label ID="lblSOtype" runat="server" ></asp:Label>
<asp:Label ID="lblSONo" runat="server" ></asp:Label>
<asp:Label ID="lblSOseq" runat="server" ></asp:Label>
<asp:Label ID="lblItemNo" runat="server" ></asp:Label>
        </asp:Panel>
       
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail SO,SaleForcast Change"></asp:Label>
                </td>
            </tr>
        </table>

        <uc1:CountRow ID="CountRow1" runat="server" />
        <asp:GridView ID="gvShow" runat="server" BackColor="White" AutoGenerateColumns="false" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"  ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"  
            CellPadding="4">
            <Columns>
                <asp:TemplateField HeaderText="SaleOrderNo. Change">
                    <ItemTemplate>
                        <asp:Label ID="lblSONochange" runat="server" Text='<%#Eval("xmehdocno")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SO OrderType">
                    <ItemTemplate>
                        <asp:Label ID="lblSOorderType" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Change S/N">
                     <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSOVersion" runat="server" Text='<%#Eval("xmeh900")%>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChangeDate">
                    <ItemTemplate>
                        <asp:Label ID="lblSOChangeDate" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SO Line">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSOline" runat="server" Text='<%#Eval("xmehseq")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Line Seq.">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSOlineSeq" runat="server" Text='<%#Eval("xmehseq1")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemNo">
                    <ItemTemplate>
                        <asp:Label ID="lblItemNo" runat="server" Text='<%#Eval("xmeh001")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Totla Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQty" runat="server" Text='<%#Eval("xmeh005", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("xmeh004")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Old agreed delivery">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblOldAgreedDelivery" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Appointed Delivery Date">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAppointedDeliveryDate" runat="server" Text='<%#Eval("xmeh011", "{0:yyyy/MM/dd}")%>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shipped Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblShippedQty" runat="server" Text='<%#Eval("xmeh014", "{0:N3}")%>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sale Return Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblSaleReturnQty" runat="server" Text='<%#Eval("xmeh015", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return Good Exch.">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblReturnGoodExch" runat="server" Text='<%#Eval("xmeh016", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:TemplateField HeaderText="Shipping Status">
                    <ItemTemplate>
                        <asp:Label ID="lblShippingStatus" runat="server" Text='<%#Eval("xmeh017")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Reference U/P">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceUP" runat="server" Text='<%#Eval("xmeh018", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Type">
                    <ItemTemplate>
                        <asp:Label ID="lblTaxType" runat="server" Text='<%#Eval("xmeh019")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Rate">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTaxRate" runat="server" Text='<%#Eval("xmeh020", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Amt Excl. Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblAmtExclTax" runat="server" Text='<%#Eval("xmeh028", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amt Incl. Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblAmtInclTax" runat="server" Text='<%#Eval("xmeh029", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblTax" runat="server" Text='<%#Eval("xmeh030", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>






        <br />
        <br />
    
   <%--     <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">--%>
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Detail Forecast Change" Visible="false"></asp:Label>
     <%--           </td>
            </tr>
        </table>--%>


        <uc1:CountRow ID="CountRow2" runat="server" Visible="false" />
        <asp:GridView ID="gvShowFore" runat="server" BackColor="White" AutoGenerateColumns="false" Visible="false" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" 
            CellPadding="4">
            <Columns>
                <asp:TemplateField HeaderText="SaleForecast. Change">
                    <ItemTemplate>
                        <asp:Label ID="lblSONochange" runat="server" Text='<%#Eval("xmehdocno")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SO OrderType">
                    <ItemTemplate>
                        <asp:Label ID="lblSOorderType" runat="server"  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SO Line">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSOline" runat="server" Text='<%#Eval("xmehseq")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Line Seq.">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblSOlineSeq" runat="server" Text='<%#Eval("xmehseq1")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemNo">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("xmeh001")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Totla Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTotalQty" runat="server" Text='<%#Eval("xmeh005", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("xmeh004")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Old agreed delivery">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblOldAgreedDelivery" runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Appointed Delivery Date">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblAppointedDeliveryDate" runat="server" Text='<%#Eval("xmeh011", "{0:MM/dd/yyyy}")%>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Shipped Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblShippedQty" runat="server" Text='<%#Eval("xmeh014", "{0:N3}")%>'  ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sale Return Qty">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblSaleReturnQty" runat="server" Text='<%#Eval("xmeh015", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Return Good Exch.">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblReturnGoodExch" runat="server" Text='<%#Eval("xmeh016", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:TemplateField HeaderText="Shipping Status">
                    <ItemTemplate>
                        <asp:Label ID="lblShippingStatus" runat="server" Text='<%#Eval("xmeh017")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Reference U/P">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceUP" runat="server" Text='<%#Eval("xmeh018", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Type">
                    <ItemTemplate>
                        <asp:Label ID="lblTaxType" runat="server" Text='<%#Eval("xmeh019")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Rate">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTaxRate" runat="server" Text='<%#Eval("xmeh020", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Amt Excl. Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblAmtExclTax" runat="server" Text='<%#Eval("xmeh028", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amt Incl. Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblAmtInclTax" runat="server" Text='<%#Eval("xmeh029", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax">
                    <ItemTemplate>
                        <asp:Label ID="lblTax" runat="server" Text='<%#Eval("xmeh030", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                    Wrap="False" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" Wrap="False" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>






        <br />






        <br />
    
    </div>
    </form>
</body>
</html>