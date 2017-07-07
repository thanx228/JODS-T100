<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Styles/MIS.Master" CodeBehind="MatUsageReport.aspx.vb" Inherits="MIS_T100.MatUsageReport" %>
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

<form action="not_approve.aspx" method="post">

 <div class="Menuhead">
     Production -> AMP Mat'l Usage Report (T100 ERP)
 </div>
 <br />
<%
    Dim checkmosign As String = Request.Form("checkmo")
    Dim motype As String = Request.Form("motype")
    Dim docno As String = Request.Form("docno")
    Dim Savedata As String = Request.Form("Savedata")
    Dim fieldmatqty() As String = Request.Form.GetValues("matuseqty")
    Dim fieldlotname() As String = Request.Form.GetValues("matlot")
    Dim viewreport As String = Request.Form("viewreport")
    Dim viewrecord As String = Request.Form("viewrecord")
    Dim SearchReport As String = Request.Form("SearchReport")
    '
    Dim matlot As String = Request.Form("matlot")
    Dim datefrom As String = Request.Form("datefrom")
    Dim dateto As String = Request.Form("dateto")
    Dim result As Boolean = False
    If viewreport = "ViewReport" Or SearchReport = "Search" Then

        '---------------------------------------------------- View Report ----------------------------------------------------

        Dim amp_DoctypeArr() As String = {"JP5104", "JP5194", "JP5199", "JP5205", "JP5209", "JP5210", "JP5211", "JP5212", "JP5301"}
        Dim disp_array As String = ""
        disp_array = "MO (Type-DocNo) <select name=motype>"
        For i = 0 To amp_DoctypeArr.Length - 1
            If motype = amp_DoctypeArr(i) Then
                disp_array = disp_array + "<option value=" & amp_DoctypeArr(i) & " Selected>" & amp_DoctypeArr(i) & "</option>"
            Else
                disp_array = disp_array + "<option value=" & amp_DoctypeArr(i) & ">" & amp_DoctypeArr(i) & "</option>"
            End If
        Next
        If docno <> "" Then
            disp_array = disp_array + "</select>&nbsp;<input type=text maxlength=11 name=docno value=" & docno & ">"
        Else
            disp_array = disp_array + "</select>&nbsp;<input type=text maxlength=11 name=docno>"
        End If
        disp_array = disp_array + "<br />Mat Lot <input type=text maxlength=11 name=matlot value=" & matlot & ">"
        disp_array = disp_array + "<br />Date From<input type=text id=dateFrom maxlength=10 name=datefrom value=" & datefrom & ">&nbsp;"
        disp_array = disp_array + "Date To&nbsp;<input id=dateTo type=text maxlength=10 name=dateto value=" & dateto & ">"
        disp_array = disp_array + "&nbsp;<input type=submit name=SearchReport value=Search>"
        disp_array = disp_array + "&nbsp;<input type=submit name=viewrecord value=ViewRecord>"
        Response.Write(disp_array)
        If SearchReport = "Search" Then
            Response.Write("<br /><hr />")
            Response.Write(ShowReport(motype, docno))
        End If

        '---------------------------------------------------- View Report ----------------------------------------------------

    Else

        '---------------------------------------------------- View Record (DF) ----------------------------------------------------

        Dim amp_DoctypeArr() As String = {"JP5104", "JP5194", "JP5199", "JP5205", "JP5209", "JP5210", "JP5211", "JP5212", "JP5301"}
        Dim disp_array As String = ""
        disp_array = "MO (Type-DocNo) <select name=motype>"
        For i = 0 To amp_DoctypeArr.Length - 1
            If motype = amp_DoctypeArr(i) Then
                disp_array = disp_array + "<option value=" & amp_DoctypeArr(i) & " Selected>" & amp_DoctypeArr(i) & "</option>"
            Else
                disp_array = disp_array + "<option value=" & amp_DoctypeArr(i) & ">" & amp_DoctypeArr(i) & "</option>"
            End If
        Next
        If docno <> "" Then
            disp_array = disp_array + "</select>&nbsp;<input type=text maxlength=11 name=docno value=" & docno & ">"
        Else
            disp_array = disp_array + "</select>&nbsp;<input type=text maxlength=11 name=docno>"
        End If
        disp_array = disp_array + "&nbsp;<input type=submit name=checkmo value=Check>"
        disp_array = disp_array + "&nbsp;<input type=submit name=viewreport value=ViewReport>"
        Response.Write(disp_array)

        If checkmosign = "Check" Then
            Response.Write("<br /><hr />")
            Response.Write(getMODetail(motype, docno))
            Response.Write(getItemBOMList(motype, docno))
        End If
        If Savedata = "Save" Then
            Dim arrcount_matqty As Integer = 0
            Dim arrcount_lotname As Integer = 0
            arrcount_matqty = fieldmatqty.Length
            arrcount_lotname = fieldlotname.Length
            If arrcount_lotname = arrcount_matqty Then
                result = isExist(motype, docno)
                If result = True Then
                    For i = 0 To arrcount_matqty - 1
                        UpdateData(motype, docno, fieldmatqty, fieldlotname)
                    Next
                Else
                    For i = 0 To arrcount_matqty - 1
                        WriteData(motype, docno, fieldmatqty, fieldlotname)
                    Next
                End If
            Else
            End If
        End If

        '---------------------------------------------------- View Record (DF) ----------------------------------------------------

    End If
%>  
    <table>
    <tr>
      <td>
          <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<%--          <input id="submitter" type="submit" name="doreport" value="Show" />--%>
<%--          <input type="button" name="viewReport" value="Report"" />--%>
          
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
