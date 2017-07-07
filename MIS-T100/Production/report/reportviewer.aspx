<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportviewer.aspx.vb" Inherits="MIS_T100.reportviewer" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
    <script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
          </div>
        <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" />
    </form>
</body>
</html>
