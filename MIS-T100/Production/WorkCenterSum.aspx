<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="WorkCenterSum.aspx.vb" Inherits="MIS_T100.WorkCenterSum" %>

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
              height:400px;
              width:1600px;
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
           $("#dateTo").datepicker({
              dateFormat: 'dd/mm/yy'
           });
           });

           $(function () {
           $("#dateFrom").datepicker({
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
     Production -> Work Center Sum (T100 ERP)
 </div>
 <form action="WorkCenterSum.aspx" method="post">
 <table>
    <tr>
      <td>
          MO Type&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_motype(this)" name="motype" /> Select All<br />
<%
    Response.Write(getDocType("4"))
%>
      </td>   
    </tr>
    <tr>
      <td>
          Workcenter(s)
      </td>   
      <td>
<%
    Response.Write(showparameWCType_sel())
%>    
      </td>   
    </tr>
<%--    <tr>
      <td>
        Select From
      </td>   
      <td>
        <select name="rectype">
          <option value="i">Operation Issue</option>
          <option value="r">Operation Receipt</option>
        </select>
      </td>   
    </tr>    --%> 
    <tr>
      <td>
          Plan Start From Date&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="fromdate" id="dateFrom" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
      </td>   
    </tr>
    <tr>
      <td>
          Plan Start To Date&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="todate" id="dateTo"value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
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
      <td>
          Item&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="item" size="20" maxlength="40" />
      </td>   
    </tr>
    <tr>
      <td>
          Spec&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="spec" size="20" maxlength="40" />
      </td>   
    </tr>
<%--    <tr>
      <td>
          Report Type&nbsp;&nbsp;
      </td>   
      <td>
        <select name="reptype">
          <option value="0">Summary</option>
          <option value="1">Detail</option>
        </select>
      </td>   
    </tr>--%>
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
    </form>
 <br /><br />
 <%--<div style=height="490px;width:1500px;position:absolute;overflow:auto;>--%>
 <div class="gridcontrol">
 <table>
    <tr>
      <td>

<%

    Dim fdate As String = Request.Form("fromdate")
    Dim tdate As String = Request.Form("todate")
    Dim doreport As String = Request.Form("doreport")

    Dim wc As String = Request.Form("wc")
    Dim qrywc As String = ""
    Dim monum As String = Request.Form("monum")
    Dim qrymonum As String = ""
    Dim item As String = Request.Form("item")
    Dim qryitem As String = ""
    Dim spec As String = Request.Form("spec")
    Dim qryspec As String = ""

    Dim motypestr() As String = Request.Form.GetValues("motype")
    Dim motype_num As Integer = 0
    Dim in_motypestr As String = ""
    Dim f_motypeDoc As String = "sfcadocno"
    Dim qrystringdoctype As String = ""
    Dim totalparam As String = ""

    If doreport = "Show" Then

        If wc = "" Then
            qrywc = " AND sfcb011 LIKE '%%' "
        Else
            qrywc = " AND sfcb011='" & wc & "' "
        End If
        If monum <> "" Then
            qrymonum = "AND sfcadocno='" & monum & "' "
        Else
        End If

        If item <> "" Then
            qryitem = "AND imaal001='" & qryitem & "' "
        Else
        End If

        If spec <> "" Then
            qryspec = "AND imaal004='" & spec & "' "
        Else
        End If
        totalparam = qrymonum + qryitem + qryspec

        If motypestr Is Nothing Then
            motype_num = 0
        Else
            motype_num = motypestr.Length
        End If

        If motype_num <> 0 Then
            For z = 0 To motype_num - 1
                If z = 0 Then
                    If motype_num = 1 Then
                        qrystringdoctype = qrystringdoctype + motypestr(z)
                    Else
                        qrystringdoctype = qrystringdoctype + motypestr(z) + "-"
                    End If
                    in_motypestr = "AND SUBSTR(" & f_motypeDoc & ",1,6) IN("
                    in_motypestr = in_motypestr + createStrMotype(motypestr(z))
                    If motypestr(z) <> "on" Then
                        If motype_num = 1 Then
                            in_motypestr = in_motypestr + ")"
                        Else
                            in_motypestr = in_motypestr + ","
                        End If
                    Else
                        If motypestr(z) <> "on" Then
                            in_motypestr = in_motypestr + createStrMotype(motypestr(z)) + ","
                        Else
                        End If
                    End If
                ElseIf z = motype_num - 1 Then
                    qrystringdoctype = qrystringdoctype + motypestr(z)
                    in_motypestr = in_motypestr + createStrMotype(motypestr(z))
                    in_motypestr = in_motypestr + ")"
                Else
                    qrystringdoctype = qrystringdoctype + motypestr(z) + "-"
                    in_motypestr = in_motypestr + createStrMotype(motypestr(z)) + ","
                End If
            Next
        Else
            in_motypestr = "AND SUBSTR(" & f_motypeDoc & ",1,6)=''"
        End If
        Response.Write(ShowDataWCSumTable(wc, qrywc, fdate, tdate, in_motypestr, qrystringdoctype, totalparam))
    Else
    End If

%>

      </td>
    </tr>
 </table>        
 </div>      
 

</asp:Content>
