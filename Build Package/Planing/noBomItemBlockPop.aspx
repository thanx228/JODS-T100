<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="noBomItemBlockPop.aspx.vb" Inherits="MIS_T100.noBomItemBlockPop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>No Bom & Item Block</title>
    <link href="../Styles/Site.css" rel="stylesheet" />
</head>
<body>
    <div>
<%
    Dim MasterItemId As String = Request.QueryString("mitemid")
    Response.Write(getItemInfo(MasterItemId))
%>      
    </div>
    <div>
    <br />
<%
    Response.Write(getSODeliveryLine(MasterItemId))
%>        
    </div>
</body>
</html>
