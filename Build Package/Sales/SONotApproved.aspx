<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SONotApproved.aspx.vb" Inherits="MIS_T100.SONotApproved" %>
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
             function toggle_schtype(source) {
                 checkboxes = document.getElementsByName('stype');
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

<form action="not_approve.aspx" method="post">

 <div class="Menuhead">
     Sales -> SO Not Approve (T100 ERP)
 </div>
 <table>
    <tr>
      <td>
          SO Type&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_schtype(this)" /> Select All<br />
<%
        Response.Write(showparameSOType("7"))
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

 <br /><br />
 <%--<div style=height="490px;width:1500px;position:absolute;overflow:auto;>--%>
    <div class="gridcontrol">
 <table>
    <tr>
      <td>
<%  
        Dim str As String = ""
        Dim doreport As String = Request.Form("doreport")
        Dim stype_str() As String = Request.Form.GetValues("stype")
        Dim f_sTypeID As String = "xmdcdocno"
        Dim in_stypeStr As String = ""

        Dim st_num As Integer = 0
        If doreport = "Show" Then

            If stype_str Is Nothing Then
                st_num = 0
            Else
                st_num = stype_str.Length
            End If
            If st_num <> 0 Then
                For z = 0 To st_num - 1
                    If z = 0 Then
                        in_stypeStr = "AND substr(" & f_sTypeID & ",1,6) IN("
                        in_stypeStr = in_stypeStr + createStrSType(stype_str(z))
                        If stype_str(z) <> "on" Then
                            If st_num = 1 Then
                                in_stypeStr = in_stypeStr + ")"
                            Else
                                in_stypeStr = in_stypeStr + ","
                            End If
                        Else
                            If stype_str(z) <> "on" Then
                                in_stypeStr = in_stypeStr + createStrSType(stype_str(z)) + ","
                            Else
                            End If
                        End If
                    ElseIf z = st_num - 1 Then
                        in_stypeStr = in_stypeStr + createStrSType(stype_str(z))
                        in_stypeStr = in_stypeStr + ")"
                    Else
                        in_stypeStr = in_stypeStr + createStrSType(stype_str(z)) + ","
                    End If
                Next
            Else
            End If

            str = ShowNotApproveSO(in_stypeStr)
            Response.Write(str)

        End If


%>
      </td>
    </tr>
 </table>        
 </div>      
 </form>
 
</ContentTemplate>

</asp:Content>
