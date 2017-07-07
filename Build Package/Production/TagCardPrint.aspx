<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="TagCardPrint.aspx.vb" Inherits="MIS_T100.TagCardPrint" %>
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

      </style>

         <!-- Code by Can Berk Güder : Stackoverflow  -->

         <script type="text/javascript">            
             function toggle_motype(source) {
                 checkboxes = document.getElementsByName('motype');
                 for (var i = 0, n = checkboxes.length; i < n; i++) {
                     checkboxes[i].checked = source.checked;
                 }
             }

             function toggle_wc(source) {
                 checkboxes = document.getElementsByName('wc');
                 for (var i = 0, n = checkboxes.length; i < n; i++) {
                     checkboxes[i].checked = source.checked;
                 }
             }
         </script>

         <script type="text/javascript">

           $(function () {
               $("#dpdateTo").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

           $(function () {
               $("#dateFrom").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

           $(function () {
               $("#dateTo").datepicker({
                   dateFormat: 'dd/mm/yy'
               });
           });

           $(function () {
               $("#pcdateFrom").datepicker({
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
 <div class="Menuhead">
     Production -> Tag Card (T100 ERP)
</div>
<br />
<div>
<form action="TagCardPrint.aspx" method="post">
<table>
    <tr>
      <td>
          Module Type&nbsp;&nbsp;
      </td>   
      <td>
          <select name="module">
              <option value="TO">Transfer Order</option>
              <option value="MOR">MO Receipt</option>
              <option value="MO">MO</option>
          </select>
      </td>   
    </tr>
    <tr>
      <td>
          Date&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="fromdate" id="dateFrom" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
      </td>   
    </tr>
    <tr>
      <td>
          Item&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="item" size="20" maxlength="40" />
      </td>   
    </tr>
    <tr>
      <td>
          Workcenter&nbsp;&nbsp;
      </td>   
      <td>
<%
          response.Write(getWC_sel())
%>
      </td>   
    </tr>
    <tr>
      <td>
          MO Number&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="monum" size="20" maxlength="40" />
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
          <input type="button" name="pdfExport" value="Print" onclick="javascript: window.open('report/reportviewer.aspx')" />
          
          <div id="divMsg" style="display:none;text-align:center;" ondrag="return false;">
              <div style="text-align:center;margin-top:200px;">
                  <img src="../pic/loading.gif" alt="Please wait.." /><br />
                         Please Wait....processing the request.
              </div>
          </div>
      </td>   
    </tr>
</table>
</form>
</div>
<br />
<div class="gridcontrol">
<%  
    Dim str As String = ""
    Dim fdate As String = Request.Form("fromdate")
    Dim doreport As String = Request.Form("doreport")
    Dim appmodule As String = Request.Form("module")

    If doreport = "Show" Then

        If appmodule = "MOR" Then
            str = getMORSummary(fdate)
            Response.Write(str)
        ElseIf appmodule = "TO" Then
            str = getTOSummary(fdate)
            Response.Write(str)
        ElseIf appmodule = "MO" Then
            str = getMOSummary(fdate)
            Response.Write(str)
        Else
        End If

    Else
    End If

%>
</div> 

</ContentTemplate>
</asp:Content> 

   
