<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WIPStatusReport.aspx.vb" Inherits="MIS_T100.WIPStatusReport" %>
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
              width:1500px;
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
               $("#dpdateFrom").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

           $(function () {
               $("#pcdateTo").datepicker({
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
 <div class="Menuhead">
     Production -> WIP Status Report (T100 ERP)
 </div>
 <table>
    <tr>
      <td>
          Specify Workcenter(s)
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_wc(this)" name="wc" /> Select All<br />
<%
    Response.Write(showparameWC("5"))
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
          <input type="button" name="genReport" value="Export To Excel" onclick="javascript: window.open('report/reportviewer.aspx')" />
          
          <div id="divMsg" style="display:none;text-align:center;" ondrag="return false;">
              <div style="text-align:center;margin-top:200px;">
                  <img src="../pic/loading.gif" alt="Please wait.." /><br />
                         Please Wait....processing the request.
              </div>
          </div>
      </td>   
    </tr>
    </table>
    <br /><br />
<%

    Dim str As String = ""
    Dim fdate As String = Request.Form("fromdate")
    Dim tdate As String = Request.Form("todate")
    Dim doreport As String = Request.Form("doreport")
    Dim wc_str() As String = Request.Form.GetValues("wc")
    Dim in_wcstr As String = ""
    Dim f_workcenterID As String = "ecaa001"
    Dim wc_num As Integer = 0

    If doreport = "Show" Then

        If wc_str Is Nothing Then
            wc_num = 0
        Else
            wc_num = wc_str.Length
        End If
        If wc_num <> 0 Then
            For z = 0 To wc_num - 1
                If z = 0 Then
                    in_wcstr = "AND " & f_workcenterID & " IN("
                    in_wcstr = in_wcstr + createStrWC(wc_str(z))
                    If wc_str(z) <> "on" Then
                        If wc_num = 1 Then
                            in_wcstr = in_wcstr + ")"
                        Else
                            in_wcstr = in_wcstr + ","
                        End If
                    Else
                        If wc_str(z) <> "on" Then
                            in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                        Else
                        End If
                    End If
                ElseIf z = wc_num - 1 Then
                    in_wcstr = in_wcstr + createStrWC(wc_str(z))
                    in_wcstr = in_wcstr + ")"
                Else
                    in_wcstr = in_wcstr + createStrWC(wc_str(z)) + ","
                End If
            Next
        Else
        End If

        Response.Write(doGenerateReport(in_wcstr))

    Else
    End If


%>
          
    <br /><br />
 <%--<div style=height="490px;width:1500px;position:absolute;overflow:auto;>--%>
    <div class="gridcontrol">
      
   </div>      




</asp:Content>
