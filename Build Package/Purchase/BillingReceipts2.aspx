﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BillingReceipts2.aspx.vb" Inherits="MIS_T100.BillingReceipts2" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<%--    <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true"  Visible="true"></CR:CrystalReportViewer>
    </div>
    </form>
</body>
</html>
