<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HttpErrorClassT100.aspx.vb" Inherits="MIS_T100.HttpErrorClassT100" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<h2> Error  Class Function GetData : <span style="color:maroon;"><% Response.Write(Session("PathVB"))%></span></h2>
<p> Function GetData Error : <span style="color:maroon;"><% Response.Write(Session("FC"))%></span></p>
    <div>
  Error : Oracle statements comments  <span style="color:maroon;"> <%Response.Write(Session("Error"))%></span>
    </div>
<p> String Sql Error  :  <span style="color:maroon;"> <%Response.Write(Session("SQL"))%></span></p>
<br />

    </form>
</body>
</html>
