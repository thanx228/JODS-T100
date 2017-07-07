<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductionRecordReportPop.aspx.vb" Inherits="MIS_T100.ProductionRecordReportPop" %>
<%@ Register src="../UserControl/HeaderForm.ascx" tagname="HeaderForm" tagprefix="uc1" %>
<%@ Register src="../UserControl/CountRow.ascx" tagname="CountRow" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detail Check Code Status</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScrollShow();
            gridviewScrollMO();
        });
        function gridviewScrollShow() {
            gridView1 = $('#<%= gvShowOper.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 300,
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
        function gridviewScrollMO() {
            gridView1 = $('#<%= gvMO_Process.ClientID %>').gridviewScroll({
                width: screen.availWidth - 30,
                height: 400,
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
</head>
<body style="background-image: url('../Images/bg.jpg')">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblpMO_Docno" runat="server" Visible="false"></asp:Label>
        <table style="width:61%;">
            </table>
        <uc1:HeaderForm ID="ucHeaderForm" runat="server" />
        <uc2:CountRow ID="ucCountRowOper" runat="server" />
        <asp:GridView ID="gvShowOper" runat="server" 
            Height="16px" AutoGenerateColumns="False">
        </asp:GridView>
    
        <asp:GridView ID="gvMODetail" runat="server" AutoGenerateColumns="False" >
            <Columns>
               <asp:TemplateField HeaderText="SaleOrder_No">
                    <ItemTemplate>
                        <asp:Label ID="lblSaleOrder_No" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="sfaadocno" HeaderText="MO_DocNo" />
                <asp:BoundField DataField="" HeaderText="Batch" />
                <asp:BoundField DataField="sfaa010" HeaderText="Production Item" />
               <asp:TemplateField HeaderText="ItemName">
                    <ItemTemplate>
                        <asp:Label ID="lblItemName" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Specifiation">
                    <ItemTemplate>
                        <asp:Label ID="lblSpecifiation" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="sfaa012" HeaderText="Prodution Qty" DataFormatString="{0:N3}" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Complete Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblComplete_Qty" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="sfaa056" HeaderText="Sacrp Qty" DataFormatString="{0:N3}" />
                <asp:BoundField DataField="sfaa019" HeaderText="PlanStart"  DataFormatString="{0:yyyy/MM/dd}" />
                <asp:BoundField DataField="sfaa020" HeaderText="PlanComplete" DataFormatString="{0:yyyy/MM/dd}"/>
            </Columns>
        </asp:GridView>
        <uc2:CountRow ID="ucCountRowMO" runat="server" />
        <asp:GridView ID="gvMO_Process" runat="server" AutoGenerateColumns="False" Width="1400px" >
             <Columns>
                <asp:BoundField DataField="sfcbdocno" HeaderText="MO_DocNo" />
                 <asp:BoundField DataField="sfca001" HeaderText="Runcard." />
                  <asp:BoundField DataField="sfca005" HeaderText="RuncardDetail" />
                <asp:BoundField DataField="sfcb002" HeaderText="Item Seq." />
                <asp:BoundField DataField="sfcb003" HeaderText="Operation" /> 
                 <asp:BoundField DataField="oocql004" HeaderText="Description" /> 
                <asp:BoundField DataField="sfcb011" HeaderText="Workstation" />
                 <asp:BoundField DataField="sfcb044" HeaderText="PlanStart"  DataFormatString="{0:yyyy/MM/dd}" />
                 <asp:BoundField DataField="sfcb045" HeaderText="PlanComplete"  DataFormatString="{0:yyyy/MM/dd}" />
                  <asp:BoundField DataField="sfcb050" HeaderText="WIP" DataFormatString="{0:N3}" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="sfcb028" HeaderText="GoodTransfer-in" DataFormatString="{0:N3}" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="sfcb033" HeaderText="GoodTransfer-Out" DataFormatString="{0:N3}" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="sfcb036" HeaderText="DirectSacrp" DataFormatString="{0:N3}" >
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
