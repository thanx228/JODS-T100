<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MoMatShortListPopup.aspx.vb" Inherits="MIS_T100.MoMatShortListPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left" 
                    
                    style="background-image: url('../Images/btt.jpg'); background-repeat: no-repeat">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" ForeColor="Blue" 
                        Text="Inventory Status Detail"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label19" runat="server" Text="Mat'l Item"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label21" runat="server" Text="ItemName"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label20" runat="server" Text="Spec"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label22" runat="server" Text="WH"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label24" runat="server" Text="Stock"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label23" runat="server" Text="Unit"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    Plan Start From</td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    Plan Start To</td>
            </tr>
            <tr>
                
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbItem" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbDesc" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbSpec" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbWH" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbStock" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                 <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbUnit" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbDateFrom" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
                <td align="left" style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbDateTo" runat="server" ForeColor="Blue"></asp:Label>
                </td>
               
            </tr>
        </table>


        <br />


        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label2" runat="server" Text="จำนวนรายการ   MO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountMO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label4" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMo" runat="server" BackColor="White" AutoGenerateColumns="False" Width="1900px" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            Height="16px">
            <Columns>
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="hplLinkMO" runat="server" Target="_blank">Detail</asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                    <asp:BoundField DataField="sfbadocno" HeaderText="MO_DocNo."  />
                    <asp:BoundField DataField="sfaastus" HeaderText="Status."  />
                    <asp:BoundField DataField="sfaa020" HeaderText="Plan_DueDate" DataFormatString="{0:yyyy/MM/dd}"  />
                 <asp:BoundField DataField="sfba001" HeaderText="Production_Item"  />
                <asp:TemplateField HeaderText="Item_Name">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_ItemName" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specifiation">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_Spec" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField DataField="sfca001" HeaderText="Runcard"  >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sfca005" HeaderText="RuncardDetail"  />
                <asp:TemplateField HeaderText="Production Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblProduct_Qty" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mat Request">
                    <ItemTemplate>
                        <asp:Label ID="lblMat_Request" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WIP">
                    <ItemTemplate>
                        <asp:Label ID="lblWIP" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GoodTransfer-In">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodTransferIn" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GoodTransfer-Out">
                    <ItemTemplate>
                        <asp:Label ID="lblGoodTransferOut" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
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






        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label25" runat="server" Text="จำนวนรายการ   PO"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPO" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label26" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
 <asp:GridView ID="gvPO" runat="server" BackColor="White" BorderColor="#3366CC"  AutoGenerateColumns="False"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px">
            <Columns>
                    <asp:TemplateField HeaderText="PO no.">
                    <ItemTemplate>
                        <asp:Label ID="lblPOno" runat="server" text='<%#Eval("pmdndocno")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                     <asp:TemplateField HeaderText="PO Status">
                          <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:Label ID="lblPOStatus" runat="server" Text='<%#Eval("pmdlstus")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                   <asp:TemplateField HeaderText="Mat' ItemNo.">
                    <ItemTemplate>
                        <asp:Label ID="lblMatItemNo" runat="server" Text='<%#Eval("pmdn001")%>' ></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vender_No.">
                    <ItemTemplate>
                        <asp:Label ID="lblVender_No" runat="server" text='<%#Eval("pmdn023")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shipping_Date.">
                         <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:Label ID="lblshipping_Date" runat="server" text='<%#Eval("pmdn012", "{0:MM/dd/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                    <asp:TemplateField HeaderText="Purchase_Qty.">
                        <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblPurchase_Qty" runat="server" text='<%#Eval("pmdn007", "{0:N3}")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                     <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("pmdn010")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arrival Date">
                     <ItemStyle HorizontalAlign="center" />
                    <ItemTemplate>
                        <asp:Label ID="lblArrivalDate" runat="server" Text='<%#Eval("pmdn013", "{0:MM/dd/yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Term">
                    <ItemTemplate>
                        <asp:Label ID="lblPaymentTerm" runat="server" Text='<%#Eval("pmdl009")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trande Term">
                    <ItemTemplate>
                        <asp:Label ID="lblTrandeTerm" runat="server" Text='<%#Eval("pmdl010")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Type">
                     <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTaxType" runat="server" Text='<%#Eval("pmdn016")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TaxRate">
                     <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblTaxRate" runat="server" Text='<%#Eval("pmdo024", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Currency">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblCurrency" runat="server" Text='<%#Eval("pmdl015")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exch.Rate">
                     <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="lblExchRate" runat="server"  Text='<%#Eval("pmdl016", "{0:N3}")%>'></asp:Label>
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






        <br />
        <table>
            <tr>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label27" runat="server" Text="จำนวนรายการ   PR Receipt "></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="lbCountPR" runat="server" ForeColor="Blue"></asp:Label>
                </td>
                <td align="center" 
                    style="background-color: #FFFFFF; border: thin solid #00CCFF">
                    <asp:Label ID="Label28" runat="server" Text="     รายการ"></asp:Label>
                </td>
            </tr>
        </table>
    <asp:GridView ID="gvPR_Receipt" runat="server" BackColor="White" BorderColor="#3366CC" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Height="16px" EmptyDataText="No Records Found">
            <Columns>
                    <asp:BoundField DataField="pmdtdocno" HeaderText="Purchase Receipt_No."  />
                     <asp:TemplateField HeaderText="Stock in Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStock_In_Date" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receipt DocNo.">
                    <ItemTemplate>
                         <asp:Label ID="lblReceiptDocNo" runat="server" Text='<%#Eval("pmdtdocno")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO_No.">
                    <ItemTemplate>
                         <asp:Label ID="lblPO_No" runat="server" Text='<%#Eval("pmdt001")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receipt_in_Qt">
                     <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                         <asp:Label ID="lblReceipt_in_Qty" runat="server" Text='<%#Eval("pmdt020", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Reject Qty">
                      <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                         <asp:Label ID="lblRejectQty" runat="server" Text='<%#Eval("pmdt055", "{0:N3}")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                         <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("pmdt019")%>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Store_Location">
                    <ItemTemplate>
                         <asp:Label ID="lblStore_Location" runat="server" Text='<%#Eval("pmdt016")%>' ></asp:Label>
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






        <br />
    
    </div>
    </form>
</body>
</html>
