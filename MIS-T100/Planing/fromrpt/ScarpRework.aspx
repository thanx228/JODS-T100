﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScarpRework.aspx.vb" Inherits="MIS_T100.ScarpRework" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<script language="javaScript" type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script>--%> 
<head runat="server">
    <title>Show Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
    
    </div>
    </form>
</body>
</html>
