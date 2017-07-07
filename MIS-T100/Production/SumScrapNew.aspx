<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="SumScrapNew.aspx.vb" Inherits="MIS_T100.SumScrapNew" %>
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
              width:2500px;
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
<ContentTemplate>


<form action="SumScrapNew.aspx" method="post">

<%

    Dim doreport As String = Request.Form("doreport")
    Dim wc() As String = Request.Form.GetValues("wc")
    Dim reptype As String = Request.Form("reptype")
    Dim FromDate As String = Request.Form("FromDate")
    Dim EndDate As String = Request.Form("EndDate")
    Dim item As String = Request.Form("item")
    Dim spec As String = Request.Form("spec")

    Dim arrnumwc As Integer = 0
    Dim seqwc As String = ""

    If doreport = "Show" Then

        If wc Is Nothing Then
            arrnumwc = 0
        Else
            arrnumwc = wc.Length
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
        If FromDate = "" Then
            FromDate = Date.Now.ToString("dd/MM/yyyy")
        Else
        End If
        If EndDate = "" Then
            EndDate = Date.Now.ToString("dd/MM/yyyy")
        Else
        End If

    End If

%>

<table cellpadding="0" cellspacing="0" border="1" width="1073">
<tr bgcolor="FFFFFF">
 <td>
 <div class="Menuhead">
     Production -> Scrap Summary (T100 ERP)
 </div>
 <table>
    <tr>
      <td>
          Document Date (From)&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="FromDate" id="dateFrom" value="<% Response.Write(FromDate)  %>"  size="10" maxlength="10" />
      </td>   
    </tr>
    <tr>
      <td>
          Document Date (To)&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="EndDate" id="dateTo" value="<% Response.Write(EndDate)  %>"  size="10" maxlength="10" />
      </td>   
    </tr>

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
          Item&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="item" size="20" value="<% Response.Write(item)  %>"  maxlength="30" />
      </td>   
    </tr>
    <tr>
      <td>
          Spec&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="spec" size="20" value="<% Response.Write(spec)  %>"  maxlength="30" />
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
      <td colspan="2">
          <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input id="submitter" type="submit" name="doreport" value="Show" />
          <asp:Button ID="exp" runat="server" Text="Export To Excel"></asp:Button>
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
