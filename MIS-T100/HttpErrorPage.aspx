<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HttpErrorPage.aspx.vb" Inherits="MIS_T100.HttpErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<h2> Page Error Url ASPX: <span style="color:maroon;"><% Response.Write(Session("Path"))%></span></h2>
<h2> Page Error Url VB : <span style="color:maroon;"><% Response.Write(Session("PathVB"))%></span></h2>
<p> Function GetData Error : <span style="color:maroon;"><% Response.Write(Session("FC"))%></span></p>
    <div>
  Message Error : <span style="color:maroon;"> <%Response.Write(Session("Error"))%></span>
    </div>
<p> String Sql Error  :  <span style="color:maroon;"> <%Response.Write(Session("SQL"))%></span></p>
<br />
<div>
</div>
    </form>
</body>
</html>
