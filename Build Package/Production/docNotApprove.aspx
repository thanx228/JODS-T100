﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="docNotApprove.aspx.vb" Inherits="MIS_T100.not_approve" MasterPageFile="~/Styles/MIS.Master" %>

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

 <div class="Menuhead">
     Production -> Not Approve & Not Post or Not Released (T100 ERP)
 </div>
 <table>
    <tr>
      <td>
          Record Type&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_schtype(this)" /> Select All<br />
<%
    Response.Write(showparameType("2"))

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
         <input type="checkbox" onclick="toggle_wc(this)" name="wc" /> Select All<br />
<%
    Response.Write(showparameWC("5"))
%>    
      </td>   
    </tr>
    <tr>
      <td>
          Report Type&nbsp;&nbsp;
      </td>   
      <td>
        <select name="reptype">
          <option value="0">Summary</option>
          <option value="1">Detail</option>
        </select>
      </td>   
    </tr>
    <tr>
      <td>
          Status Type&nbsp;&nbsp;
      </td>   
      <td>
          <select name="recstat">
           <option value="N">Not Approve</option>
           <option value="Y">Approved (Not Posting, Or Not Released)</option>
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

    


