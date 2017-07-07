<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkCenterSumPopup.aspx.vb" Inherits="MIS_T100.WorkCenterSumPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>WSS in Detail -
<% 
    Dim wc As String = Request.QueryString("wc")
    Response.Write(" Workcenter[" & wc & "] ")
%>     
</title>
    <link href="../Styles/Site.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            margin-right: 0px;
        }
    </style>
</head>
<body>

    <div>

<%

    Dim fdate As String = Request.QueryString("fdate")
    Dim tdate As String = Request.QueryString("tdate")
    Dim mow As String = Request.QueryString("mow")
    Dim wc As String = Request.QueryString("wc")
    Dim doctype As String = Request.QueryString("doctype")

    Response.Write(ShowWCSDetail(fdate, tdate, wc, mow, doctype))

%>
      
    </div>      
</body>
</html>
