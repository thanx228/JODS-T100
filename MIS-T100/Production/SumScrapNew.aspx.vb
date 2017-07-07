Imports System.IO
Imports System.Data
Imports System.Drawing

Public Class SumScrapNew
    Inherits System.Web.UI.Page

    'Declare Class
    Dim ECAA As New ECAA   'WC List
    Dim SFFB As New SFFB   'TO Table
    Dim SFAA As New SFAA   'MO Table
    Dim SFCA As New SFCA   'MO Header
    Dim IMAAL As New IMAAL 'Item Master
    Dim clstemp As New clsJODST100_temp_Scraplist   'temp for create report
    Dim clsconnect As New clsDBConnect

    '------------------- Production Module - Scrap Summary -------------------
    '                      Original Code Module - JODS
    '                 Version 2 by Pattavee Narumonchavalit
    '-----------------------------------------------------------------------

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session.Timeout = 120
        If Session("UserId") Is Nothing Or Session("UserId") = "" Then
            Response.Redirect("../Login.aspx")
        Else
            Dim usrnamelogged As String = Session("UserName")
            clstemp.ClearTempRec(usrnamelogged)
            Dim show As String = Request.Form("doreport")

            Dim in_wcstr As String = "" '
            Dim f_workcenterID As String = "sffb009"  'wc field in TO

            If show = "Show" Then
                Dim reptype As String = Request.Form("reptype")
                Dim datefrom As String = Request.Form("FromDate")
                Dim dateto As String = Request.Form("EndDate")
                Dim item As String = Request.Form("item")
                Dim spec As String = Request.Form("spec")

                Dim wc_str() As String = Request.Form.GetValues("wc")
                Dim r As Integer = 0
                Dim ds As DataSet
                Dim wc_num As String = ""
                If wc_str Is Nothing Then
                Else
                    wc_num = wc_str.Length
                End If

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

                    If reptype = "0" Or reptype = "1" Then     'Summary (General Process -> RC != 0)

                    ds = getScrapSummary(reptype, datefrom, dateto, item, spec, in_wcstr)
                    GridViewResult.DataSource = ds
                    GridViewResult.DataBind()
                    GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")

                Else 'Detail

                    ds = getScrapRecDetail(datefrom, dateto, item, spec, in_wcstr)
                    GridViewResult.DataSource = ds
                    GridViewResult.DataBind()
                    GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")
                End If
            End If
        End If

    End Sub

    Public Function getScrapSummary(ByVal reptype As String, ByVal datefrom As String, ByVal dateto As String, Optional ByVal item As String = "", Optional ByVal spec As String = "", Optional ByVal in_wcstr As String = "") As DataSet

        Dim ds As DataSet
        Dim rc As String = ""
        Dim language As String = "imaal002"
        If reptype = "0" Then     'General Item
            rc = "AND sffb006='0'"
        ElseIf reptype = "1" Then 'Rework Item
            rc = "AND sffb006<>'0'"
        Else
        End If

        Dim searchitem As String = ""
        Dim searchspec As String = ""

        If item <> "" Then
            searchitem = "AND sfaa010 LIKE '%" & item & "%'"
        Else
        End If
        If spec <> "" Then
            searchspec = "AND imaal004 LIKE '%" & spec & "%'"
        Else
        End If

        Dim lblfield_wc As String = "|Work Center|"
        lblfield_wc = lblfield_wc.Replace("|", Chr(34))
        Dim lblfield_wcname As String = "|Work Center Name|"
        lblfield_wcname = lblfield_wcname.Replace("|", Chr(34))
        Dim lblfield_mo As String = "|TTL MO Item|"
        lblfield_mo = lblfield_mo.Replace("|", Chr(34))
        Dim lblfield_moqty As String = "|TTL MO Qty|"
        lblfield_moqty = lblfield_moqty.Replace("|", Chr(34))
        Dim lblfield_sumscrap As String = "|Sum Scrap Qty|"
        lblfield_sumscrap = lblfield_sumscrap.Replace("|", Chr(34))
        Dim lblfield_scrapperc As String = "|Scrap%|"
        lblfield_scrapperc = lblfield_scrapperc.Replace("|", Chr(34))
        Dim lblfield_apprstatus As String = "|Sum Approve|"
        lblfield_apprstatus = lblfield_apprstatus.Replace("|", Chr(34))
        Dim lblfield_sumnotappr As String = "|Sum Not Appr|"
        lblfield_sumnotappr = lblfield_sumnotappr.Replace("|", Chr(34))
        Dim lblfield_datefrom As String = "|Date From|"
        lblfield_datefrom = lblfield_datefrom.Replace("|", Chr(34))
        Dim lblfield_dateto As String = "|Date To|"
        lblfield_dateto = lblfield_dateto.Replace("|", Chr(34))

        Dim sql As String = "SELECT " & SFFB.Workstation & " AS " & lblfield_wc & "," & ECAA.Workcenter & " AS " & lblfield_wcname & "," &
                            "COUNT(" & SFAA.ProductItem & ") AS " & lblfield_mo & ",SUM(" & SFCA.ProductionQty & ") AS " & lblfield_moqty & "," &
                            "SUM(" & SFFB.ScarpQty & ") AS " & lblfield_sumscrap & ",ROUND((SUM(" & SFFB.ScarpQty & ")/SUM(" & SFCA.ProductionQty & "))*100,2) AS " & lblfield_scrapperc & "," &
                            "SUM(CASE " & SFFB.Status & " WHEN 'Y' THEN 1 ELSE 0 END) AS " & lblfield_apprstatus & "," &
                            "SUM(CASE " & SFFB.Status & " WHEN 'N' THEN 1 ELSE 0 END) AS " & lblfield_sumnotappr & "," &
                            "TO_CHAR(MIN(" & SFFB.DocumentDate & "),'dd/MM/yyyy') AS " & lblfield_datefrom & "," &
                            "TO_CHAR(MAX(" & SFFB.DocumentDate & "),'dd/MM/yyyy') AS " & lblfield_dateto & " " &
                            "FROM " & SFFB.tblTransferHead & " " &
                            "LEFT JOIN " & SFAA.tblMO & " " &
                            "ON " & SFFB.WONo & "=" & SFAA.DocNo & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " " &
                            "ON " & SFFB.WONo & "=" & SFCA.DocNo & " AND " & SFFB.RunCard & "=" & SFCA.RunCardNo & " " &
                            "LEFT JOIN " & ECAA.tblWorkcenter & " " &
                            "ON " & SFFB.Workstation & "=" & ECAA.WorkcenterID & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " " &
                            "ON " & SFAA.ProductItem & "=" & IMAAL.W_ProductItem & " AND " & language & "='en_US' " &
                            "WHERE " & SFFB.ent & "='3' AND " & SFAA.ProgromCode & "='3' AND " & SFCA.ent & "='3' AND " & ECAA.ent & "='3' " &
                            "AND " & SFFB.DocumentDate & " BETWEEN TO_DATE('" & datefrom & "','dd/mm/yyyy') AND TO_DATE('" & dateto & "','dd/mm/yyyy') " &
                            "AND " & SFFB.ScarpQty & "<>0 " & rc & " " & searchitem & " " & searchspec & " " & in_wcstr & " " &
                            "GROUP BY " & SFFB.Workstation & "," & ECAA.Workcenter & " ORDER BY " & SFFB.Workstation & ""
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        Return ds

    End Function

    Public Function getScrapRecDetail(ByVal datefrom As String, ByVal dateto As String, Optional ByVal item As String = "", Optional ByVal spec As String = "", Optional ByVal in_wcstr As String = "") As DataSet

        Dim ds As DataSet
        Dim language As String = "imaal002"
        Dim searchitem As String = ""
        Dim searchspec As String = ""

        If item <> "" Then
            searchitem = "AND sfaa010 LIKE '%" & item & "%'"
        Else
        End If
        If spec <> "" Then
            searchspec = "AND imaal004 LIKE '%" & spec & "%'"
        Else
        End If

        Dim lblfield_wc As String = "|Work Center|"
        lblfield_wc = lblfield_wc.Replace("|", Chr(34))
        Dim lblfield_wcname As String = "|Work Center Name|"
        lblfield_wcname = lblfield_wcname.Replace("|", Chr(34))
        Dim lblfield_trfno As String = "|Transfer. No|"
        lblfield_trfno = lblfield_trfno.Replace("|", Chr(34))
        Dim lblfield_scrapno As String = "|Scrap. No|"
        lblfield_scrapno = lblfield_scrapno.Replace("|", Chr(34))
        Dim lblfield_docdate As String = "|Doc.Date|"
        lblfield_docdate = lblfield_docdate.Replace("|", Chr(34))
        Dim lblfield_MO As String = "|MO|"
        lblfield_MO = lblfield_MO.Replace("|", Chr(34))
        Dim lblfield_runcard As String = "|Runcard|"
        lblfield_runcard = lblfield_runcard.Replace("|", Chr(34))
        Dim lblfield_item As String = "|Item|"
        lblfield_item = lblfield_item.Replace("|", Chr(34))
        Dim lblfield_spec As String = "|Spec|"
        lblfield_spec = lblfield_spec.Replace("|", Chr(34))
        Dim lblfield_moqty As String = "|MO Qty|"
        lblfield_moqty = lblfield_moqty.Replace("|", Chr(34))
        Dim lblfield_scrap As String = "|Scrap Qty|"
        lblfield_scrap = lblfield_scrap.Replace("|", Chr(34))
        Dim lblfield_Scrapperc As String = "|Scrap% |"
        lblfield_Scrapperc = lblfield_Scrapperc.Replace("|", Chr(34))
        Dim lblfield_redoqty As String = "|Redo Qty|"
        lblfield_redoqty = lblfield_redoqty.Replace("|", Chr(34))
        Dim lblfield_apprstatus As String = "|Appr.Status|"
        lblfield_apprstatus = lblfield_apprstatus.Replace("|", Chr(34))
        Dim lblfield_department As String = "|Department|"
        lblfield_department = lblfield_department.Replace("|", Chr(34))

        Dim sql As String = "Select " & SFFB.Workstation & " As " & lblfield_wc & "," & ECAA.Workcenter & " As " & lblfield_wcname & "," &
                            "" & SFFB.DocNo & " As " & lblfield_trfno & ",TO_CHAR(" & SFFB.DocumentDate & ",'dd/MM/yyyy') As " & lblfield_docdate & "," & SFAA.DocNo & " As " & lblfield_MO & "," &
                            "" & SFFB.RunCard & " As " & lblfield_runcard & "," & SFAA.ProductItem & " As " & lblfield_item & "," & IMAAL.Specifaction & " As " & lblfield_spec & "," & SFCA.ProductionQty & " As " & lblfield_moqty & "," &
                            "" & SFFB.ScarpQty & " As " & lblfield_scrap & ", ROUND((" & SFFB.ScarpQty & " / " & SFCA.ProductionQty & ") * 100, 2) As " & lblfield_Scrapperc & "," &
                            "CASE " & SFFB.Status & " When 'Y' THEN 'Y:Approved' ELSE 'N:Not Approved' END AS " & lblfield_apprstatus & "," & ECAA.CostCenter & " AS " & lblfield_department & " " &
                            "FROM " & SFFB.tblTransferHead & " " &
                            "LEFT JOIN " & SFAA.tblMO & " " &
                            "ON " & SFFB.WONo & "=" & SFAA.DocNo & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " " &
                            "ON " & SFFB.WONo & "=" & SFCA.DocNo & " AND " & SFFB.RunCard & "=" & SFCA.RunCardNo & " " &
                            "LEFT JOIN " & ECAA.tblWorkcenter & " " &
                            "ON " & SFFB.Workstation & "=" & ECAA.WorkcenterID & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " " &
                            "ON " & SFAA.ProductItem & "=" & IMAAL.W_ProductItem & " AND " & language & "='en_US' " &
                            "WHERE " & SFFB.ent & "='3' AND " & SFAA.ProgromCode & "='3' AND " & SFCA.ent & "='3' AND " & ECAA.ent & "='3' " &
                            "AND " & IMAAL.ent & "='3' AND " & SFFB.DocumentDate & " BETWEEN TO_DATE('" & datefrom & "','dd/mm/yyyy') AND " &
                            "TO_DATE('" & dateto & "','dd/mm/yyyy') AND " & SFFB.ScarpQty & " <> 0 " & searchitem & " " & searchspec & " " & in_wcstr & " " &
                            "ORDER BY " & SFFB.Workstation & "," & SFFB.DocumentDate & ""
        ds = clsconnect.QueryDataSet(sql, clsconnect.T100)
        clsconnect.Close(clsconnect.T100)
        Return ds

    End Function

    Public Function showparameWC(Optional ByVal rowsplit As Integer = 4, Optional mtwc As String = "") As String

        Dim drawstr As String = ""
        Dim linecut As Integer = 1
        Dim rowcount As Integer = 0
        Dim ds As DataSet
        Dim wccode As String = ""
        Dim wcname As String = ""
        Dim ischecked As String = ""
        Dim mtwcarr() As String = mtwc.Split(",")

        ds = ECAA.GetWorkcenter_DataSet()
        rowcount = ds.Tables(0).Rows.Count
        For i = 0 To rowcount - 1
            'wccode = ds.Tables("DATASET")(i)("WC")
            'wcname = ds.Tables("DATASET")(i)("WCNAME")
            wccode = ds.Tables(0)(i)(0)
            wcname = ds.Tables(0)(i)(1)

            If mtwc.Length <> 0 Then
                If mtwcarr.Contains(wccode) = True Then
                    ischecked = " checked"
                Else
                End If
            End If

            drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & " " & ischecked & "> " & wcname & " "
            If i Mod rowsplit = 0 And i <> 0 Then
                drawstr = drawstr + "<br />"
            Else
            End If
            linecut = linecut + 1
            ischecked = ""
        Next
        Return drawstr

    End Function

    Private Function getStatus(ByVal stat As String) As String

        Dim result As String = ""
        If stat = "N" Then
            result = "N:Not Approved"
        Else
            result = "Y:Approved"
        End If
        Return result
    End Function

    Public Function showselReporttype(Optional value As String = "") As String

        Dim drawstring As String = ""
        Dim v1 As String = ""
        Dim v2 As String = ""
        Dim v3 As String = ""

        If value = 0 Then
            v1 = " selected"
        End If
        If value = 1 Then
            v2 = " selected"
        End If
        If value = 2 Then
            v3 = " selected"
        End If

        drawstring = "<option value=0 " & v1 & ">Summary (Runcard = 0)</option><option value=1" & v2 & ">Summary (Runcard is not 0)</option><option value=2" & v3 & ">Detail</option>"

        Return drawstring

    End Function

    Public Function createStrWC(ByVal wcstring As String) As String
        Dim statement As String = ""
        If wcstring <> "on" Then
            statement = "'" + wcstring + "'"
        Else
        End If
        Return statement
    End Function

End Class


'-------------------  Backup Version 1 -------------------


'------------------- Production Module - Scrap Summary -------------------
'                      Original Code Module - JODS
'                 Version 1 by Pattavee Narumonchavalit
'-----------------------------------------------------------------------

''Declare Class
'Dim ECAA As New ECAA   'WC List
'Dim INBI As New INBI   'Scrap Header
'Dim INBJ As New INBJ   'Scrap Line
'Dim clstemp As New clsJODST100_temp_Scraplist   'temp for create report
'Dim clsconnect As New clsDBConnect

'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'    Session.Timeout = 120
'    If Session("UserId") Is Nothing Or Session("UserId") = "" Then
'        Response.Redirect("../Login.aspx")
'    Else
'        Dim usrnamelogged As String = Session("UserName")
'        clstemp.ClearTempRec(usrnamelogged)
'        Dim show As String = Request.Form("doreport")
'        If show = "Show" Then
'            Dim reptype As String = Request.Form("reptype")
'            Dim wc_str() As String = Request.Form.GetValues("wc")
'            Dim r As Integer = 0
'            Dim ds As DataSet
'            Dim wc_num As String = ""
'            If wc_str Is Nothing Then
'            Else
'                wc_num = wc_str.Length
'            End If
'            If reptype = 0 Then     'Summary
'                If wc_num <> 0 Then
'                    For i = 0 To wc_num - 1

'                    Next
'                Else
'                End If
'            Else 'Detail
'                'Dim wcdescription As String = ""
'                'Dim transferNo As String = ""
'                'Dim mo As String = ""
'                'Dim pec_scrap As Double
'                'Dim moqty As Integer = 0
'                'Dim scrapqty As Integer = 0
'                'Dim reworkqty As Integer = 0
'                Dim scrapdocNo As String = ""
'                Dim docDate As String = ""
'                Dim item As String = ""
'                Dim spec As String = ""
'                Dim approvestat As String = ""
'                Dim linenum As Integer = 1
'                ds = getScrapRecDetail(reptype)
'                r = ds.Tables(0).Rows.Count
'                For i = 0 To r - 1
'                    scrapdocNo = ds.Tables(0)(i)(1)
'                    docDate = ds.Tables(0)(i)(22)
'                    item = ds.Tables(0)(i)(19)
'                    spec = ds.Tables(0)(i)(20)
'                    approvestat = ds.Tables(0)(i)(23)
'                    approvestat = getStatus(approvestat)
'                    clstemp.InsertTempRecord(linenum, reptype, scrapdocNo, docDate, item, spec, approvestat, usrnamelogged)
'                    linenum = linenum + 1
'                Next
'                Call ShowScrapSummary_Detail()
'            End If
'        End If
'    End If
'End Sub

'Private Sub ShowScrapSummary_Summary()
'    Dim usrnamelogged As String = Session("UserName")
'    Dim ds As DataSet = clstemp.GetLogDataShow_Summary_Dataset(usrnamelogged)
'    GridViewResult.DataSource = ds
'    GridViewResult.DataBind()
'    'GridviewUtility.GrigOnmouseHandleAuto(GridViewResult)
'    'GridviewUtility.MergeCells(GridViewResult)
'    GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")
'End Sub

'Private Sub ShowScrapSummary_Detail()
'    Dim usrnamelogged As String = Session("UserName")
'    Dim ds As DataSet = clstemp.GetLogDataShow_Detail_Dataset(usrnamelogged)
'    GridViewResult.DataSource = ds
'    GridViewResult.DataBind()
'    'GridviewUtility.GrigOnmouseHandleAuto(GridViewResult)
'    'GridviewUtility.MergeCells(GridViewResult)
'    GridviewUtility.GrigOnmouseHandleCustomer(GridViewResult, "#C0C0C0")
'End Sub

'Private Function getStatus(ByVal stat As String) As String
'    Dim result As String = ""
'    If stat = "N" Then
'        result = "N:Not Approved"
'    Else
'        result = "Y:Approved"
'    End If
'    Return result
'End Function

'Public Function getScrapRecSummary(ByVal reptype As String, Optional ByVal datefrom As String = "", Optional ByVal dateto As String = "", Optional ByVal item As String = "", Optional ByVal spec As String = "")
'    Return Nothing
'End Function

'Public Function getScrapRecDetail(ByVal reptype As String, Optional ByVal datefrom As String = "", Optional ByVal dateto As String = "", Optional ByVal item As String = "", Optional ByVal spec As String = "") As DataSet
'    Dim ds As DataSet
'    'ds = INBJ.GetScrapDetail_DataSet()
'    Dim sql As String = "SELECT " & INBJ.Site & "," & INBJ.DocNo & "," & INBJ.LineNo & "," &
'        " " & INBJ.ItemNo & "," & INBJ.InventoryManagementCharacteristics & "," & INBJ.TransferOutWH & "," & INBJ.TransferOutLocation & "," & INBJ.LotNo & "," & INBJ.InventoryUnit & "," & INBJ.AppliedQty & "," & INBJ.ActualTransactionQty & "," &
'        " " & INBJ.IncomingWasteStore & "," & INBJ.ScrapReason & "," & INBJ.Department & "," & INBJ.DocumentNo & "," & INBJ.ProjectNo & "," &
'        " " & INBJ.WBS & "," & INBJ.ActivityNo & "," & INBJ.Memo & "," & IMAAL.ProductItem & "," & IMAAL.Specifaction & "," & INBI.DocNo & "," & INBI.EntryDate & "," & INBI.Status &
'        " FROM " & INBJ.tblScarpDestoryBody & " LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & INBJ.ItemNo & "=" & IMAAL.ProductItem & "" &
'        " LEFT JOIN " & INBI.tblScarpDestoryHeader & " ON " & INBJ.DocNo & "=" & INBI.DocNo & " WHERE " & INBJ.ent & "='3' AND " & IMAAL.ent & "='3' AND " & INBI.ent & "=3"
'    ds = clsconnect.QueryDataSet(sql, clsconnect.T100)

'    Return ds
'End Function

'Public Function showparameWC(Optional ByVal rowsplit As Integer = 4) As String

'    Dim drawstr As String = ""
'    Dim linecut As Integer = 1
'    Dim rowcount As Integer = 0
'    Dim ds As DataSet
'    Dim wccode As String = ""
'    Dim wcname As String = ""
'    ds = ECAA.GetWorkcenter_DataSet()
'    rowcount = ds.Tables(0).Rows.Count
'    For i = 0 To rowcount - 1
'        'wccode = ds.Tables("DATASET")(i)("WC")
'        'wcname = ds.Tables("DATASET")(i)("WCNAME")
'        wccode = ds.Tables(0)(i)(0)
'        wcname = ds.Tables(0)(i)(1)
'        drawstr = drawstr + "<input type=checkbox name=wc value=" & wccode & "> " & wcname & " "
'        If i Mod rowsplit = 0 And i <> 0 Then
'            drawstr = drawstr + "<br />"
'        Else
'        End If
'        linecut = linecut + 1
'    Next
'    Return drawstr

'End Function

'Protected Sub exp_Click(sender As Object, e As EventArgs) Handles exp.Click
'    Call ExportToExcel(sender, e)
'End Sub

'Protected Sub ExportToExcel(sender As Object, e As EventArgs)

'    Response.Clear()
'    Response.Buffer = True
'    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
'    Response.Charset = ""
'    Response.ContentType = "application/vnd.ms-excel"
'    Using sw As New StringWriter()
'        Dim hw As New HtmlTextWriter(sw)

'        'To Export all pages
'        'GridViewResult.AllowPaging = False
'        'Me.BindGrid()

'        GridViewResult.HeaderRow.BackColor = Color.White
'        For Each cell As TableCell In GridViewResult.HeaderRow.Cells
'            cell.BackColor = GridViewResult.HeaderStyle.BackColor
'        Next
'        For Each row As GridViewRow In GridViewResult.Rows
'            row.BackColor = Color.White
'            For Each cell As TableCell In row.Cells
'                If row.RowIndex Mod 2 = 0 Then
'                    cell.BackColor = GridViewResult.AlternatingRowStyle.BackColor
'                Else
'                    cell.BackColor = GridViewResult.RowStyle.BackColor
'                End If
'                cell.CssClass = "textmode"
'            Next
'        Next

'        GridViewResult.RenderControl(hw)
'        'style to format numbers to string
'        Dim style As String = "<style> .textmode { } </style>"
'        Response.Write(style)
'        Response.Output.Write(sw.ToString())
'        Response.Flush()
'        Response.[End]()

'    End Using

'End Sub