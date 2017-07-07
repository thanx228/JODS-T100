<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SqlJoinOracle.aspx.vb" Inherits="MIS_T100.SqlJoinOracle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/gridviewScroll.min.js" type="text/javascript"></script>
    <link href="../Styles/gridviewScroll.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gridviewScrollShow() {
            gridView1 = $('#<%= GridView1.ClientID %>').gridviewScroll({
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
          PlanDate :  <asp:Label ID="lblPlanDate" runat="server" ></asp:Label><br />
            W/C :  <asp:Label ID="lblWC" runat="server" ></asp:Label>
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <h1>SQL-Server</h1>
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                AllowPaging="True" PagerSettings-FirstPageText="First" PageSize="50" 
                PagerSettings-LastPageText="Last" PagerSettings-NextPageText="Next"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"
                Height="16px" PagerSettings-Position="Bottom">
                <Columns>
                    <asp:BoundField DataField="Workcenter" HeaderText="WC" />
                    <asp:BoundField DataField="PlanDate" HeaderText="PlanDate" />
                    <asp:BoundField DataField="MONo" HeaderText="MO" />
                    <asp:TemplateField HeaderText="WCT100">
                        <ItemTemplate>
                            <asp:Label ID="lblWCT100" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MO-T100">
                        <ItemTemplate>
                            <asp:Label ID="lblWOT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StdLaborHursT100">
                        <ItemTemplate>
                            <asp:Label ID="lblStdLaborHursT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="StdMcHursT100">
                        <ItemTemplate>
                            <asp:Label ID="lblStdMcHursT100" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <h1>OracleBase</h1>
            <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
