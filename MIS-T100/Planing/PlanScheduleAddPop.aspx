<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PlanScheduleAddPop.aspx.vb" Inherits="MIS_T100.PlanScheduleAddPop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPS in Detail [
<%
    Dim mo As String = Request.QueryString("mo")
    Response.Write(mo)
%>
]
    </title>
    <link href="../Styles/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<%
    Dim mo As String = Request.QueryString("mo")
    Response.Write(showMODetail(mo))
    Response.Write("<br />")
    Response.Write(getRelateOperationMO(mo))
%>               
    </form>
</body>
</html>
