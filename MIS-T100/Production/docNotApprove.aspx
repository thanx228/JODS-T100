<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="docNotApprove.aspx.vb" Inherits="MIS_T100.not_approve" MasterPageFile="~/Styles/MIS.Master" %>

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
             function toggle_schtype(source) {
                 checkboxes = document.getElementsByName('schtype');
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
<table cellpadding="0" cellspacing="0" border="1" width="1073">
<tr bgcolor="FFFFFF">
 <td>
 <div class="Menuhead">
     Production -> Not Approve & Not Post or Not Released (T100 ERP)
 </div>
 <table>
    <tr>
      <td>
          Record Type&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_schtype(this)" />Select All<br />
<%

    Dim doreport As String = Request.Form("doreport")
    Dim wc() As String = Request.Form.GetValues("wc")
    Dim schtype() As String = Request.Form.GetValues("schtype")
    Dim reptype As String = Request.Form("reptype")
    Dim recstat As String = Request.Form("recstat")

    Dim arrnumwc As Integer = 0
    Dim arrnumsch As Integer = 0
    Dim seqwc As String = ""
    Dim seqschtype As String = ""

    If doreport = "Show" Then

        If wc Is Nothing Then
            arrnumwc = 0
        Else
            arrnumwc = wc.Length
        End If
        If schtype Is Nothing Then
            arrnumsch = 0
        Else
            arrnumsch = schtype.Length
        End If

        If arrnumwc <> 0 Then
            For i = 0 To arrnumwc - 1
                If i = arrnumwc - 1 Then
                    seqwc = seqwc + wc(i)
                ElseIf i = 0 Then
                    seqwc = wc(i) + ","
                Else
                    seqwc = seqwc + wc(i) + ","
                End If
            Next
        End If
        If arrnumsch <> 0 Then
            For i = 0 To arrnumsch - 1
                If i = arrnumsch - 1 Then
                    seqschtype = seqschtype + schtype(i)
                ElseIf i = 0 Then
                    seqschtype = schtype(i) + ","
                Else
                    seqschtype = seqschtype + schtype(i) + ","
                End If
            Next
        End If

    End If

    Response.Write(showparameType("2", seqschtype))
%>    
      </td>   
    </tr>
<%--    <tr>
      <td>
          End Date&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="enddate" id="dateTo" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy")) %>" size="10" maxlength="10" />
      </td>   
    </tr>--%>
    <tr>
      <td>
          Work Center&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_wc(this)" />Select All<br />
<%
            Response.Write(showparameWC("5", seqwc))
%>    
      </td>   
    </tr>
    <tr>
      <td>
          Report Type&nbsp;&nbsp;
      </td>   
      <td>
        <select name="reptype">
<%
            Response.Write(showselReporttype(reptype))
%>  
        </select>
      </td>   
    </tr>
    <tr>
      <td>
          Status Type&nbsp;&nbsp;
      </td>   
      <td>
          <select name="recstat">
<%
    Response.Write(showselRecstatus(recstat))
%>  
        </select>
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
          <br /><br />
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

 <%--<div style=height="490px;width:1500px;position:absolute;overflow:auto;>--%>
    <div class="gridcontrol">
 <table>
    <tr>
      <td>
          <asp:GridView ID="GridViewResult" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True">
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
          </asp:GridView>       
      </td>
    </tr>
 </table>        
 </div>
 </form>

 
</ContentTemplate>
</asp:Content>

    


