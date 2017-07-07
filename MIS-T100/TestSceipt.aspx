<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TestSceipt.aspx.vb" Inherits="MIS_T100.TestSceipt" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../js/jquery-ui.css" rel="stylesheet" />
      <script src="../js/jquery-1.12.4.js"></script>
      <script src="../js/jquery-ui.js"></script>
   <%-- <link href="Styles/PopupLoad.css" rel="stylesheet" />--%>
    <style type="text/css">
         #divMsg {
           position: absolute;
           top: 0px;
           right: 0px;
           width: 100%;
           height: 100%;
           background-color: #000;
           background-repeat: no-repeat;
           background-position: center;
           z-index: 10000000;
           opacity: 0.7;
           filter: alpha(opacity=40);
           color: white;
       }
    </style>
     <script type="text/javascript"> 
           $(function(){
           $('#submitter').one('click', function(){  
           $('#divMsg').show();
              });
           });
        </script>
</head>
<body>
    <form id="form1" runat="server">
<input id="submitter" type="submit" name="doreport" value="Show" />

 <div id="divMsg" class="divMsg"  style="display:none;text-align:center" ondrag="return false; ">
 <div style="text-align:center;margin-top:150px;">
              <img src="../pic/loading2.gif" style="height:100px;" alt="Please wait.." /><br />
               Please Wait....processing the request.
     </div>
 </div>
    </form>
</body>
</html>
