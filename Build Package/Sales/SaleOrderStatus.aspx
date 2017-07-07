<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SaleOrderStatus.aspx.vb" Inherits="MIS_T100.SaleOrderStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <link href="../js/jquery-ui.css" rel="stylesheet" />
      <script src="../js/jquery-1.12.4.js"></script>
      <script src="../js/jquery-ui.js"></script>
    
      <style>

          #divMsg {
              position: absolute;
              top:0px;    
              right:0px;   
              width:100%;   /*100%*/
              height:100%;  /*100%*/
              background-color:#000;
              /*background-image:url('pic/loading.gif');*/
              background-repeat:no-repeat;
              background-position:center;
              z-index:10000000;
              opacity: 0.7;
              filter: alpha(opacity=40);
              color: white;
          }

          .Menuhead{
              font-family: Tahoma;
              font-size: 25px;
              color: black;
          }

          .gridcontrol{
              height: auto;
              width:2000px;
              position:absolute;
              overflow:auto;
          }

          .inner{
              text-align:center;
          }

          .auto-style1 {
              height: 38px;
          }

      </style>

         <!-- Code by Can Berk Güder : Stackoverflow  -->

         <script type="text/javascript">            
             function toggle_schtype(source) {
                 checkboxes = document.getElementsByName('stype');
                 for (var i = 0, n = checkboxes.length; i < n; i++) {
                     checkboxes[i].checked = source.checked;
                 }
             }

         </script>

         <script type="text/javascript">

           $(function () {
           $("#dateFrom").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

        </script>

         <script type="text/javascript">

           $(function () {
           $("#dateTo").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

        </script>

<%--        <script type="text/javascript"> 
           $(function(){
           $('#submitter').one('click', function(){  
           $(this).attr('disabled','disabled');
           $('#divMsg').show();
              });
           });
        </script>--%>

        <script type="text/javascript"> 
           $(function(){
           $('#submitter').one('click', function(){  
           $('#divMsg').show();
              });
           });
        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<ContentTemplate>

<form action="SaleOrderStatus.aspx" method="post">
 <div class="Menuhead">
     Sales -> Sale Status (T100 ERP)
 </div>
 <br />
 <div class="gridcontrol">
 <table>
 <tr>
   <td>
     <table border="0">
        <tr bgcolor="#FFFFFF">
            <td width="150">
              SO Type
            </td>
            <td>
         <input type="checkbox" onclick="toggle_schtype(this)" /> Select All<br />
<%
    Response.Write(showparameSOType("6"))
%>
            </td>
        </tr>
        <tr bgcolor="#FFFFFF">
            <td height="25">
              SO Number
            </td>
            <td height="25">
             <input type="text" name="SONum" size="15" maxlength="20" />&nbsp;&nbsp;&nbsp;&nbsp;CustomerID <input type="text" name="CustID" size="15" maxlength="20" />
            </td>                        
        </tr>
        <tr bgcolor="#FFFFFF">
            <td height="25">
              Item
            </td>
            <td height="25">
             <input type="text" name="Item" size="15" maxlength="20" />&nbsp;&nbsp;&nbsp;&nbsp;Spec<input type="text" name="Spec" size="15" maxlength="20" />
            </td>                        
        </tr>
        <tr bgcolor="#FFFFFF">
            <td height="25">
              Begin Due Delivery Date
            </td>
            <td height="25">
             <input type="text" id="dateFrom" name="BDDate" size="10" maxlength="10" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>" />&nbsp;&nbsp;&nbsp;&nbsp;End Due Delivery Date<input type="text" id="dateTo" name="EDDate" size="10" maxlength="10"value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>" />
            </td>                        
        </tr>
        <tr bgcolor="#FFFFFF">
            <td height="25">
              Approve Status
            </td>
            <td height="25">
<%
    Response.Write(showparamNotApproveOption("1"))
    Response.Write(showparamSOOption("1"))
%>
            </td>                        
        </tr>
    <tr>
      <td colspan="2">
          <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input id="submitter" type="submit" name="doreport" value="Show" />
          <input type="button" name="genReport" value="Report" onclick="javascript: window.open('report/reportviewer.aspx')" />

          <div id="divMsg" style="display:none;text-align:center;" ondrag="return false;">
              <div style="text-align:center;margin-top:200px;">
                  <img src="../pic/loading.gif" alt="Please wait.." /><br />
                         Please Wait....processing the request.
              </div>
          </div>
      </td>   
    </tr>
     </table>          
   </td>
 </tr>
 </table>  
 </div>                  
 </form>
<div class="gridcontrol">
<%  
    Dim str As String = ""
    Dim fdate As String = Request.Form("BDDate")
    Dim tdate As String = Request.Form("EDDate")
    Dim SONum As String = Request.Form("SONum")
    Dim CustID As String = Request.Form("CustID")
    Dim Item As String = Request.Form("Item")
    Dim Spec As String = Request.Form("Spec")
    Dim doreport As String = Request.Form("doreport")
    'Dim appmodule As String = Request.Form("module")
    If doreport = "Show" Then
        Response.Write(getSOStatusDisp(fdate, tdate, SONum, CustID, Item, Spec))
    Else
    End If

%>
</div> 

 
</ContentTemplate>
</asp:Content>
