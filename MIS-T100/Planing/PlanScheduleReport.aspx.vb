Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class PlanScheduleReport
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Private Shared RowNumSelect As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            'reset()
        End If

    End Sub
    Private Sub getDataBind()
        Dim Sday As String = String.Empty
        Dim Tday As String = String.Empty
        Dim where As String = String.Empty
        Dim aSQL As String = "Select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        'Dim aWC As String = UserAuthen.LevelWorkcenterT100(Session("UserId"))
        Dim ssFdate As String = String.Empty
        Dim ssTdate As String = String.Empty
        Dim Fdate As String = String.Empty
        Dim Tdate As String = String.Empty
        If txtDateFrom.Text <> String.Empty AndAlso txtDateTo.Text <> String.Empty Then
            Fdate = txtDateFrom.Text
            Tdate = txtDateTo.Text
            Dim xxFdate As Date = CDate(Fdate)
            Dim xxTdate As Date = CDate(Tdate)
            ssFdate = xxFdate.ToString("yyyyMMdd")
            ssTdate = xxTdate.ToString("yyyyMMdd")
        Else
            Dim SqlGetDate As String = "Select to_char(TO_DATE(sysdate),'mm/dd/yyyy') as getDate,to_char(TO_DATE(sysdate),'yyyymmdd') as FindDate From dual"
            Dim GetSysDate As DataTable = GetDataOracleDate(SqlGetDate)
            Fdate = dtRowsFormat.FormatString(GetSysDate, "getDate")
            Tdate = dtRowsFormat.FormatString(GetSysDate, "getDate")
            ssFdate = dtRowsFormat.FormatString(GetSysDate, "FindDate")
            ssTdate = dtRowsFormat.FormatString(GetSysDate, "FindDate")
        End If
        Dim whereDate As String = " PlanDate BETWEEN '" & ssFdate & "' and '" & ssTdate & "' "
        Dim SelectWhereMOtype As String = MultipleSelect(UsingMOTypeCheckList.getObject)
        Dim SelectMOType As Integer = RowNumSelect
        Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWorkstationCheckList.getObject)
        Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        If (SelectMOType > 0) And (SelectWC <= 0) Then
            where = " and TA001 In(" & [String].Join("','", SelectWhereMOtype) & "')" & " and " & whereDate
        ElseIf (SelectWC > 0) And (SelectMOType <= 0) Then
            where = " and TA006 In(" & [String].Join("','", SelectWhereWC.Replace("WC", "W")) & "')" & " and " & whereDate
        ElseIf (SelectWC > 0) And (SelectMOType > 0) Then
            Dim whereType As String = " TA001 In(" & [String].Join("','", SelectWhereMOtype) & "')"
            where = " and TA006 In(" & [String].Join("','", SelectWhereWC.Replace("WC", "W")) & "')" & " and " & whereType & " and " & whereDate
        End If

        Dim SqlPlanSQL As String = "select TA006 as WC,'' as WcName,TA004 as Op_Id,' ' as Op_Name, " &
" 'JP'+TA001+'-'+TA002 as Mo_No,TA003 as Seq," &
" '' as ItemNo,'' as Spec,'' as PlanStart, " &
" '' as PlanComplete,Mch,'' as MO_Qty,PlanQty,'' as BalPlanQty " &
" from PlanSchedule where SUBSTRING(TA002,1,8 ) > '20151231' " & where & " " &
" group by  TA006,TA004,TA001,TA002,TA003,Mch,PlanQty "

        Dim dtPlan As DataTable = GetDataSQLserverMIS(SqlPlanSQL)
        If dtPlan.Rows.Count > 0 Then
            Dim dtPlanReport As New DataTable("PlanScheduleReport")
            Dim dr As DataRow = Nothing
            dtPlanReport.Columns.Add(New DataColumn("WC", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("WCName", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Op_Id", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Op_Name", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Mo_No", GetType(String)))
            'dtPlanReport.Columns.Add(New DataColumn("Mo_Status", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Seq", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("ItemNo", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Spec", GetType(String)))
            ' dtPlanReport.Columns.Add(New DataColumn("PlanStart", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("PlanComplete", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("Mch", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("MO_Qty", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("PlanQty", GetType(String)))
            dtPlanReport.Columns.Add(New DataColumn("BalPlanQty", GetType(String)))
            If dtPlan.Rows.Count > 0 Then
                For Each row As DataRow In dtPlan.Rows
                    dr = dtPlanReport.NewRow()
                    Dim WC_Erp As String = row.Item("WC").Replace(" ", "")
                    Dim strWC As String = WC_Erp.Replace("W", "WC")
                    dr("WC") = strWC
                    Dim dtWCname As DataTable = ECAA.GetFindWorkcenterDetail_Table(strWC)
                    Dim strWCname As String = String.Empty
                    If dtWCname.Rows.Count > 0 Then
                        strWCname = dtRowsFormat.FormatString(dtWCname, ECAA.Workcenter)
                    End If
                    dr("WCName") = strWCname
                    dr("Op_Id") = row.Item("Op_Id")
                    dr("Mo_No") = row.Item("Mo_No").Replace(" ", "")
                    dr("Seq") = row.Item("Seq")
                    dr("Mch") = row.Item("Mch")
                    Dim OpERP As String = dr("Op_Id")
                    Dim dtOpName As DataTable = OOCQL.GetDataOperation(OpERP)
                    If dtOpName.Rows.Count > 0 Then
                        dr("Op_Name") = dtRowsFormat.FormatString(dtOpName, OOCQL.Operation)
                    End If
                    Dim MO_ERP As String = dr("Mo_No")
                    Dim Seq_ERP As Integer = CInt(dr("Seq"))
                    Dim whereT100 As String = String.Empty
                    Dim pWC As String = SFCB.WorkStation & " ='" & dr("WC") & "'"
                    Dim MOt100 As String = SFCB.WONo & " ='" & MO_ERP & "'"
                    Dim Seqt100 As String = SFCB.LineNo & " ='" & Seq_ERP & "'"
                    whereT100 = pWC & " and " & MOt100 & " and " & Seqt100
                    Dim SqlDataT100 As String = " select " & SFAA.ProductItem & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & ", " &
                        " " & SFAA.ProductionQty & " " &
                        " from " & SFCB.tblMOprocessItem_SFCB & " " &
                        " LEFT JOIN " & SFAA.tblMO & " ON " & SFAA.DocNo & "=" & SFCB.WONo & " " &
                        " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' and " & whereT100 & "  "
                    Dim dtProcessT100 As DataTable = GetDataOracleDate(SqlDataT100)
                    Dim sItemNo As String = String.Empty
                    Dim sPlanStart As String = String.Empty
                    Dim sPlanComplete As String = String.Empty
                    Dim sMO_Qty As String = String.Empty
                    Dim sMO_Status As String = String.Empty
                    If dtProcessT100.Rows.Count > 0 Then
                        sItemNo = dtRowsFormat.FormatString(dtProcessT100, SFAA.ProductItem)
                        ' sPlanStart = dtRowsFormat.FormatString(dtProcessT100, SFCB.PlanStartDate)
                        sPlanComplete = dtRowsFormat.FormatString(dtProcessT100, SFCB.PlannedCompletionDate)
                        sMO_Qty = dtRowsFormat.FormatString(dtProcessT100, SFAA.ProductionQty)
                        'sMO_Status = StatusT100.MO_Normal(dtRowsFormat.FormatString(dtProcessT100, SFAA.Status))
                        ' sMO_Status = dtRowsFormat.FormatString(dtProcessT100, SFAA.Status)
                    End If
                    If sMO_Qty <> String.Empty Then
                        Dim MOQty As Double = CDbl(sMO_Qty)
                        sMO_Qty = String.Format("{0:n3}", MOQty)
                    End If
                    dr("ItemNo") = sItemNo
                    ' dr("PlanStart") = sPlanStart
                    dr("PlanComplete") = sPlanComplete
                    Dim PLanQty As String = row.Item("PlanQty")
                    If PLanQty <> String.Empty Then
                        Dim ddPLanQty As Double = CDbl(PLanQty)
                        PLanQty = String.Format("{0:n3}", ddPLanQty)
                    End If
                    dr("PlanQty") = PLanQty
                    dr("MO_Qty") = sMO_Qty
                    'dr("Mo_Status") = sMO_Status
                    Dim dtItem As DataTable = IMAAL.GetDataProducItem(sItemNo)
                    Dim pSpec As String = String.Empty
                    If dtItem.Rows.Count > 0 Then
                        pSpec = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    End If
                    dr("Spec") = pSpec
                    If sMO_Qty <> String.Empty And PLanQty <> String.Empty Then
                        Dim BalPlanQty As Double = CDbl(sMO_Qty) - CDbl(PLanQty)
                        dr("BalPlanQty") = String.Format("{0:n3}", BalPlanQty)
                    Else
                        dr("BalPlanQty") = ""
                    End If

                    dtPlanReport.Rows.Add(dr)
                Next row
                Dim dsSet As DataSet = AddDateBetweenToDataTable(dtPlanReport, Fdate, Tdate)
                gvShow.DataSource = dsSet
                gvShow.DataBind()
                btExport.Visible = True
                ucCountRow.RowCount = dtPlan.Rows.Count.ToString
                'GridviewUtility.GridStyleTemplate_Std(gvShow)
                'GridviewUtility.GrigOnmouseHandleAuto(gvShow)
                Call CreateHeadGeridShow()
            End If
        End If
    End Sub
    Private Shared Function AddDateBetweenToDataTable(dt As DataTable, SDate As String, EDate As String) As DataSet
        Dim StartDate As Date
        Dim EndDate As Date
        StartDate = ConvertUtility.StringToDate(SDate)
        EndDate = ConvertUtility.StringToDate(EDate)
        Dim tbl As New DataTable
        Dim dr As DataRow = Nothing
        Dim columns As DataColumnCollection = dt.Columns
        Dim column As DataColumn
        For Each column In columns
            tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
        Next
        Dim eRow As DataRowCollection = dt.Rows
        For Each drs As DataRow In eRow
            tbl.ImportRow(drs)
        Next
        For offset = 0 To (EndDate - StartDate).Days
            tbl.Columns.Add(New DataColumn(StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
        Next
        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
    End Function
    Private Sub CreateHeadGeridShow()
        Dim i As Integer = 14
        While i < gvShow.HeaderRow.Cells.Count
            Dim pDate As String = gvShow.HeaderRow.Cells(i).Text
            Dim sxx = pDate.Split("()")
            Dim aaa As String = sxx(0)
            Dim xStr = aaa.Split("-")
            Dim yy As Integer = CInt(xStr(2))
            Dim mm As Integer = CInt(xStr(1))
            Dim dddd As Integer = CInt(xStr(0))
            Dim Chdd As New Date(yy, mm, dddd)
            Dim CheckDay As String = Chdd.ToString("ddd")
            'MsgBox(aaa)
            'gvCheck.HeaderRow.Cells(i).Text = Chdd.ToString("yyyMMdd")
            If CheckDay = "Sat" Then
                Dim TitleDate As String = Chdd.ToString("dd-MM-yyyy")
                Dim sTitle As String = "<span style='color:Yellow;'>" & CheckDay & "</span>"
                gvShow.HeaderRow.Cells(i).Text = TitleDate & "(" & sTitle & ")"
            End If
            If CheckDay = "Sun" Then
                Dim TitleDate As String = Chdd.ToString("dd-MM-yyyy")
                Dim sTitle As String = "<span style='color:Red;'>" & CheckDay & "</span>"
                gvShow.HeaderRow.Cells(i).Text = TitleDate & "(" & sTitle & ")"
            End If
            For Each row As GridViewRow In gvShow.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim Operp As String = row.Cells(2).Text
                    Dim iSeq As Integer = CInt(row.Cells(5).Text)
                    Dim Seq_Erp As String = iSeq.ToString("0000")

                    Dim sWONO = row.Cells(4).Text.Replace("JP", "")
                    Dim ssMONO = sWONO.Split(" ")
                    Dim sWOxx = ssMONO(0).Split("-")
                    Dim MO_NO As String = sWOxx(1)
                    Dim WOType As String = sWOxx(0)
                    Dim SqlData As String = "select PlanQty,Cancled from PlanSchedule " &
                               " where TA001='" & WOType & "' and TA002 = '" & MO_NO & "' and TA004 ='" & Operp & "' " &
                               " and TA003 ='" & Seq_Erp & "' and PlanDate='" & Chdd.ToString("yyyyMMdd") & "' "
                    Dim strPlanQty As String = String.Empty
                    Dim Cancled As String = String.Empty
                    Dim dtDataPlan As DataTable = GetDataSQLserverMIS(SqlData)
                    If dtDataPlan.Rows.Count > 0 Then
                        strPlanQty = dtRowsFormat.FormatString(dtDataPlan, "PlanQty")
                        Cancled = dtRowsFormat.FormatString(dtDataPlan, "Cancled")
                    End If
                    If strPlanQty <> String.Empty Then
                        If Cancled <> "0" Then
                            ' row.Cells(i).Text = "<span style='color:Red;'>" & "Cancel<br></span>" & CInt(strPlanQty)
                            row.Cells(i).Text = CInt(strPlanQty)
                            row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                            row.Cells(i).BackColor = System.Drawing.Color.Gray
                        Else
                            row.Cells(i).Text = CInt(strPlanQty)
                        End If
                        Dim dblNumberCells As Double = CDbl(row.Cells(i).Text)
                        row.Cells(i).Text = String.Format("{0:n3}", dblNumberCells)
                    Else
                        row.Cells(i).Text = String.Empty
                    End If
                    row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                    ' End If
                    ' Dim tmpval As Integer = 0
                    'If row.Cells(i).Text <> String.Empty Then
                    '    tmpval = tmpval + Decimal.Parse(row.Cells(i).Text)
                    '    'MsgBox(tmpval.ToString)
                    'End If
                End If
            Next
            i += 1
        End While
    End Sub
    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then

        'End If
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call getDataBind()
    End Sub
    Private Shared Function MultipleSelect(CheckboxList As CheckBoxList) As String
        Dim iRow As Integer = 0
        Dim AaryList As List(Of [String]) = New List(Of String)()
        For Each item As ListItem In CheckboxList.Items
            If item.Selected Then
                AaryList.Add(item.Value)
                iRow = +1
                MultipleSelectNum(iRow)
            End If
        Next
        Dim Where As String = String.Empty
        Dim StrSelect As String = String.Empty
        If iRow > 0 Then
            StrSelect = " '" & [String].Join("' , '", AaryList.ToArray())
        End If
        Where = StrSelect
        Return Where
    End Function
    Private Shared Function MultipleSelectNum(RowNum As Integer) As Integer
        RowNumSelect = RowNum
        Return RowNumSelect
    End Function
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Call getDataBind()
        'ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        'System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvShow", "gridviewScrollgvShow();", True)
    End Sub
    Private Shared Function GetDataSQLserverMIS(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataSQLserverMIS", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared Function GetDataOracleDate(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataOracleDate", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Protected Sub btReset_Click(sender As Object, e As EventArgs) Handles btReset.Click
        reset()
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("DailyPlanSchuduleReport" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Sub reset()
        'ucMoType.typeSet = "51,52"
        'ucDateFrom.dateVal = ""
        'ucDateTo.dateVal = ""
        'ucWC.setObject = ""

        'btExport.Visible = False

        'gvShow.DataSource = ""
        'gvShow.DataBind()
        'ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
    End Sub

End Class