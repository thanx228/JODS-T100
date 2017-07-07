<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Sample.aspx.vb" Inherits="MIS_T100.Sample" %>

<!DOCTYPE html>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pivot Data Samples</title>
    <style type="text/css">
    body
    {
    	font-family:Verdana, Tahoma;
    	font-size:9pt;
    }
    h1
    {
    	font-family:Verdana, Tahoma;
    	font-size:14pt;
    }
	h2
	{
		color:#667755;
		font-family:Verdana, Tahoma;
	    font-size:12pt;
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label ID="lblsql" runat="server" ></asp:Label>
    <h1>Summary Report: Employee Salary (Annual)</h1>
    <%--<cc1:TabContainer ID="TabContainer1" runat="server">
        <cc1:TabPanel ID="Panel1" runat="server" HeaderText="Raw Data">
            <ContentTemplate>--%>
                    <h2>Raw Data</h2>
                    <asp:GridView ID="grdRawData" runat="server" BackColor="White" 
                        BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black">
                        <RowStyle BackColor="#F7F7DE" />
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView><br />
  <%--          </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="First Pivot">
            <ContentTemplate>--%>
                <h2>No. of Employees: Comapany vs Year</h2>
                <asp:GridView ID="grdCompanyYear" runat="server" BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <h2>Least Salary: Comapany vs Year</h2>
                <asp:GridView ID="grdLeastCompanyYear" runat="server" BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView><br />
   <%--         </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Second Pivot">
            <ContentTemplate>--%>
                <h2>Highest Salary: Designation vs Comapany and Year</h2>
                <asp:GridView ID="grdDesignationCompanyYear" runat="server" BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" onrowcreated="grdPivot2_RowCreated">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <h2>Average Salary: Designation vs Comapany and Year</h2>
                <asp:GridView ID="grdDesignationCompanyYearAvg" runat="server" BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" onrowcreated="grdPivot2_RowCreated">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView><br />
<%--            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="Panel2" runat="server" HeaderText="Third Pivot">
            <ContentTemplate>--%>
                <h2>Average Salary: Designation vs Comapany, Department and Year</h2>
                <asp:GridView ID="grdPivot" runat="server" BackColor="White" 
                    BorderColor="#cEcFcE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" onrowcreated="grdPivot3_RowCreated">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
      <%--      </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>--%>
    </form>
</body>
</html>
