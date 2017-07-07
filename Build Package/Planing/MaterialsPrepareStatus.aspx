<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MaterialsPrepareStatus.aspx.vb" Inherits="MIS_T100.MaterialsPrepareStatus" %>
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
<form action="MaterialsPrepareStatus.aspx" method="post">
<%--    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Button" />--%>
 <div class="Menuhead">
     Production -> WIP Prepare Status (T100 ERP)
     <br /><br />
 </div>
 <table>
    <tr>
      <td>
          Work Center&nbsp;&nbsp;
      </td>   
      <td>
        <select name="wc">
          <option value="WC04">WC04 : Spot Welding</option>
          <option value="WC05">WC05 : Reprocess</option>
          <option value="WC09">WC09 : Aerospace</option>
          <option value="WC12">WC12 : Stamping</option>
          <option value="WC13">WC13 : Assembly</option>
          <option value="WC14">WC14 : Welding</option>
          <option value="WC15">WC15 : Welding (Robot)</option>
          <option value="WC56">WC56 : AMP Welding</option>
        </select>
      </td>   
    </tr>
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
          MO Number&nbsp;&nbsp;
      </td>   
      <td>
         <input type="text" name="monum" size="20" maxlength="30" />&nbsp;Part/Item No.&nbsp;<input type="text" name="itemnum" size="20" maxlength="30" />
      </td>   
    </tr>
    <tr>
      <td>
          Spec
      </td>   
      <td>
         <input type="text" name="spec" size="20" maxlength="30" />
      </td>   
    </tr>
    <tr>
      <td>
          Condition&nbsp;&nbsp;
      </td>   
      <td>
        <select name="condition">
          <option value="1">All</option>
          <option value="2">Not Issue</option>
          <option value="3">Issue < Required</option>
        </select>
      </td>   
    </tr>
    <tr>
      <td>
         Plan Complete Date (From)
      </td>
      <td>
        <input type="text" name="pcfromdate" id="pcdateFrom" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />&nbsp;&nbsp;Plan Complete Date (To)<input type="text" name="pcdateTo" id="pcdateTo" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
      </td>  
    </tr>
<%--    <tr>
      <td>
         Daily Plan Start (From)
      </td>
      <td>
        <input type="text" name="dpfromdate" id="dpdateFrom" value="<% Response.Write(Date.Now.ToString("dd/MM/yyyy"))  %>"  size="10" maxlength="10" />
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
    Dim pcfdate As String = Request.Form("pcfromdate")
    Dim pctdate As String = Request.Form("pcdateTo")
    Dim condition As String = Request.Form("condition")
    Dim doreport As String = Request.Form("doreport")
    Dim wc As String = Request.Form("wc")
    Dim doctype() As String = Request.Form.GetValues("motype")
    Dim doctype_num As Integer = 0
    Dim str_indoctype As String = ""
    Dim doctypestrSQL As String = "SUBSTR(sfcbdocno,1,6)"
    If doreport = "Show" Then
        If doctype Is Nothing Then
            doctype_num = 0
        Else
            doctype_num = doctype.Length
        End If
        If doctype_num <> 0 Then
            For z = 0 To doctype_num - 1
                If z = 0 Then
                    str_indoctype = "AND " & doctypestrSQL & " IN("
                    str_indoctype = str_indoctype + createStrDoctype(doctype(z))
                    If doctype(z) <> "on" Then
                        If doctype_num = 1 Then
                            str_indoctype = str_indoctype + ")"
                        Else
                            str_indoctype = str_indoctype + ","
                        End If
                    Else
                        If doctype(z) <> "on" Then
                            str_indoctype = str_indoctype + createStrDoctype(doctype(z)) + ","
                        Else
                        End If
                    End If
                ElseIf z = doctype_num - 1 Then
                    str_indoctype = str_indoctype + createStrDoctype(doctype(z))
                    str_indoctype = str_indoctype + ")"
                Else
                    str_indoctype = str_indoctype + createStrDoctype(doctype(z)) + ","
                End If
            Next
        Else
        End If

        str = getWIPDataProcess(pcfdate, pctdate, wc, condition, str_indoctype)
        Response.Write(str)
    End If


    'Dim str As String = ""
    'Dim pcfdate As String = Request.Form("pcfromdate")
    'Dim doreport As String = Request.Form("doreport")
    'Dim wc As String = Request.Form("wc")
    'Dim doctype() As String = Request.Form.GetValues("motype")
    'Dim doctype_num As Integer = 0
    'Dim str_indoctype As String = ""
    'Dim orsyntax = " OR sfcbdocno LIKE "
    'Dim andsyntax = " AND sfcbdocno LIKE "
    'If doreport = "Show" Then

    '    If doctype Is Nothing Then
    '        doctype_num = 0
    '    Else
    '        doctype_num = doctype.Length
    '    End If

    '    If doctype_num <> 0 Then
    '        For z = 0 To doctype_num - 1
    '            If z = 0 Then
    '                If doctype(z) = "on" Then
    '                Else
    '                    str_indoctype = str_indoctype + andsyntax + createStrDoctype(doctype(z))
    '                End If
    '            Else
    '                If str_indoctype = "" Then
    '                    str_indoctype = str_indoctype + andsyntax + createStrDoctype(doctype(z))
    '                Else
    '                    str_indoctype = str_indoctype + orsyntax + createStrDoctype(doctype(z))
    '                End If
    '            End If
    '        Next
    '    Else
    '    End If
    '    str = getWIPDataProcess(pcfdate, wc, str_indoctype)
    '    Response.Write(str)
    'End If



%>
      </td>
    </tr>
 </table>        
 </div>      
 </form>

</asp:Content>
