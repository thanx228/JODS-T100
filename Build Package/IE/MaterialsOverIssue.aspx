﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsOverIssue.aspx.vb" Inherits="MIS_T100.MaterialsOverIssue" %>
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
<form action="WorkLoading.aspx" method="post">
<%--    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Button" />--%>
 <div class="Menuhead">
     Production -> Material Over Issue (T100 ERP)
     <br />
 </div>
 <table>
<%--    <tr>
      <td>
          MO Type&nbsp;&nbsp;
      </td>   
      <td>
         <input type="checkbox" onclick="toggle_motype(this)" name="motype" /> Select All<br />--%>
<%
'Response.Write(showMOType("5"))
%>    
<%--      </td>   
    </tr>--%>
<%--    <tr>
      <td>
          Customer ID&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="custid" size="20" maxlength="30" />
      </td>   
    </tr>--%>
<%--    <tr>
      <td>
          MO Codition&nbsp;&nbsp;
      </td>   
      <td>
        <select name="mocon">
          <option value="1">All Condition</option>
          <option value="2">In Progress</option>
          <option value="3">Pending</option>
        </select>
      </td>   
    </tr>--%>
    <tr>
      <td>
<%--        <select name="type">
          <option value="A">Plan start date</option>
          <option value="B">Daily plan start date</option>
        </select>--%>
          MO From date
      </td>
      <td>
        <input type="text" name="fromdate" id="dateFrom" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
          MO To date
        <input type="text" name="todate" id="dateTo" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
      </td>  
    </tr>
    <tr>
      <td>
          Over Issue          
      </td>
      <td>
        <select name="cond">
          <option value="aoi">All Over Issues</option>
          <option value="a">More than 0% to 5%</option>
          <option value="b">More than 5% to 10%</option>
          <option value="c">More than 10% to 15%</option>
          <option value="d">More than 15% to 20%</option>
          <option value="e">More than 20% to 25%</option>
          <option value="f">More than 25% to 30%</option>
          <option value="g">More than 30%</option>
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
 <%--<div style=height="490px;width:1500px;position:absolute;overflow:auto;>--%>
    <div class="gridcontrol">
 <table>
    <tr>
      <td>
<%--          <asp:GridView ID="GridViewResult" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True">
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
          </asp:GridView> --%>      

<%


    Dim str As String = ""
    Dim fdate As String = Request.Form("fromdate")
    Dim tdate As String = Request.Form("todate")
    Dim cond As String = Request.Form("cond")
    Dim doreport As String = Request.Form("doreport")

    Dim wc_num As Integer = 0
    If doreport = "Show" Then

        str = getMatOverIssue(fdate, tdate, cond)
        Response.Write(str)

    Else
    End If

    'str = getTORecordDetail(fdate, tdate, in_wcstr)
    'Response.Write(str)

%>

      </td>
    </tr>
 </table>        
 </div>      
 </form>


</asp:Content>
