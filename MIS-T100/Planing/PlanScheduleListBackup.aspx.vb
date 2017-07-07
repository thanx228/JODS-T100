Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class PlanScheduleListBackup
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Private Shared iyyyy As Integer
    Private Shared iMM As Integer
    Private Shared Sday As Integer = 1
    Private Shared Tday As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                ' Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Page.Title = ""
            'CreateTable.CreateMoBalanceTable()
            'CreateTable.CreatePlanScheduleTable()
            'UserAuthen.LevelWorkcenterT100(Session("UserId"))
            'UserAuthen.LevelWorkcenterERP(Session("UserId"))
            'WorkstationAuten.WC_Auten = WC
            Call S_year()
            Call S_Month()
            Call showGridview()
            GridView1.Visible = False
        End If
    End Sub
    Private Shared Function CalcHHMM(Min As String) As String
        Dim showHHMM As String = String.Empty
        Dim iRowMc As Integer = CInt(Int(Min))
        If iRowMc > 0 Then
            Dim hours As Integer = iRowMc \ 60
            Dim minutes As Integer = iRowMc - (hours * 60)
            Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes.ToString("00"), String)
            showHHMM = timeElapsed
        Else
            showHHMM = "00:00"
        End If
        Return showHHMM
    End Function
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "W/C"
            e.Row.Cells(1).Text = "Name"
        End If
    End Sub
    Private Shared Function AddDaysBetweenMonth(dt As DataTable, SDay As Integer, EDay As Integer) As DataSet
        Dim tbl As New DataTable
        Dim dr As DataRow = Nothing
        Dim columns As DataColumnCollection = dt.Columns
        Dim column As DataColumn
        For Each column In columns
            tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
        Next
        tbl.Columns.Add(New DataColumn("Machine_Load", GetType(String)))
        Dim eRow As DataRowCollection = dt.Rows
        For Each drs As DataRow In eRow
            tbl.ImportRow(drs)
        Next
        For offset = SDay To EDay
            Dim d As New DateTime(iyyyy, iMM, offset)
            tbl.Columns.Add(New DataColumn("(" & d.ToString("ddd") & ")" & offset.ToString("00")))
        Next
        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
    End Function
    Private Shared SqlDataWrokstatinDataT100 As String = " select " & ECAA.WorkcenterID & " ," & ECAA.Workcenter & " " &
    " from " & ECAA.tblWorkcenter & " where " & ECAA.wStandard & "  "
    Public Shared Function GetWrokstatinDataT100() As DataTable
        Dim Sql As String = SqlDataWrokstatinDataT100
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetWrokstatinDataT100", "Sql = SqlDataWrokstatinDataT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared SqlDataWhereWrokstatinDataT100 As String = " select " & ECAA.WorkcenterID & " ," & ECAA.Workcenter & " " &
    " from " & ECAA.tblWorkcenter & " where " & ECAA.wStandard & "  "
    Public Shared Function GetDataWhereWrokstatinDataT100(MultiWorkcenter As String) As DataTable
        Dim Sql As String = SqlDataWhereWrokstatinDataT100
        Sql = Sql & " and " & ECAA.WorkcenterID & " In(" & [String].Join("','", MultiWorkcenter) & "')"
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataWhereWrokstatinDataT100", "Sql = SqlDataWrokstatinDataT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Public Shared Function GetDataTable(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataTable", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Public Shared Function GetDataTableFind(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataTableFind", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Public Shared Function GetDataTableFindMC(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataTableFindMC", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Call showGridview()
    End Sub
    Private Sub S_year()
        Dim i As Integer
        For i = 2013 To 2050
            DLYear.Items.Add(i)
        Next i
        Dim SqlGetDate As String = "Select to_char(TO_DATE(sysdate),'yyyy') as yyyy, " &
" to_char(TO_DATE(sysdate),'mm') as mm, " &
" to_char(LAST_DAY(sysdate),'dd') as EndDay From dual "
        Dim GetSysDate As DataTable = GetDataTable(SqlGetDate)
        DLYear.Items.FindByValue(dtRowsFormat.FormatString(GetSysDate, "yyyy")).Selected = True
    End Sub
    Private Sub S_Month()
        Dim SqlGetDate As String = "Select to_char(TO_DATE(sysdate),'yyyy') as yyyy, " &
" to_char(TO_DATE(sysdate),'mm') as mm, " &
" to_char(LAST_DAY(sysdate),'dd') as EndDay From dual "
        Dim GetSysDate As DataTable = GetDataTable(SqlGetDate)
        ddlMonth.Items.FindByValue(dtRowsFormat.FormatString(GetSysDate, "mm")).Selected = True
    End Sub
    Private Sub showGridview()
        Dim mmm As String = ddlMonth.SelectedValue
        Dim MonthNam As String = GetSheardMonth(mmm)
        Dim yy As String = DLYear.Text
        Dim FindDate As String = "01-" & MonthNam & "-" & yy
        Dim WHR As String = String.Empty
        Dim aSQL As String = "Select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        'Dim aWC As String = UserAuthen.LevelWorkcenterT100(Session("UserId"))

        Dim dtWCT100 As New DataTable
        Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWC.getObject)
        Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        If SelectWC > 0 Then
            dtWCT100 = GetDataWhereWrokstatinDataT100(SelectWhereWC)
        Else
            dtWCT100 = GetWrokstatinDataT100()
        End If
        Dim SqlGetDateFind As String = "Select to_char(TO_DATE('" & FindDate & "'),'yyyy') as yyyy, " &
" to_char(TO_DATE('" & FindDate & "'),'mm') as mm, " &
" to_char(LAST_DAY('" & FindDate & "'),'dd') as EndDay From dual "
        Dim GetSysDate As DataTable = GetDataTableFind(SqlGetDateFind)
        iyyyy = CInt(dtRowsFormat.FormatString(GetSysDate, "yyyy"))
        iMM = CInt(dtRowsFormat.FormatString(GetSysDate, "mm"))
        Tday = CInt(dtRowsFormat.FormatString(GetSysDate, "EndDay"))
        If dtWCT100.Rows.Count > 0 Then
            Call getDatabindUpdateStdTime()
            Dim ds As DataSet = AddDaysBetweenMonth(dtWCT100, Sday, Tday)
            gvShow.DataSource = ds
            gvShow.DataBind()
            Call CreateHeadGeridShow()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub
    Private Sub getDatabindUpdateStdTime()
        'Dim sppDate As String = iyyyy & iMM.ToString("00")
        'Dim WC As String = String.Empty
        'Dim WhereWC As String = String.Empty
        'Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWC.getObject)
        'Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        'If SelectWC > 0 Then
        '    WC = SelectWhereWC.Replace("WC", "W")
        '    WhereWC = "TA006 In(" & [String].Join("','", WC) & "') AND PlanDate Like '" & sppDate & "%'"
        'Else
        '    WhereWC = "PlanDate Like '" & sppDate & "%'"
        'End If
        'Dim dtSQLserver As DataTable = GetdatePlanScheduleSQLserver(WhereWC)
        'Dim dtPlanReport As New DataTable("ScheduleInToTime")
        'Dim dr As DataRow = Nothing
        'dtPlanReport.Columns.Add(New DataColumn("WorkStation", GetType(String)))
        'dtPlanReport.Columns.Add(New DataColumn("PlanDate", GetType(String)))
        'dtPlanReport.Columns.Add(New DataColumn("MO_No", GetType(String)))
        'dtPlanReport.Columns.Add(New DataColumn("StdLaborHursT100", GetType(String)))
        'dtPlanReport.Columns.Add(New DataColumn("StdMcHursT100", GetType(String)))
        'If dtSQLserver.Rows.Count > 0 Then
        '    For Each rowERP As DataRow In dtSQLserver.Rows
        '        dr = dtPlanReport.NewRow()
        '        dr("WorkStation") = rowERP.Item("Workcenter")
        '        dr("PlanDate") = rowERP.Item("PlanDate")
        '        dr("Mo_No") = rowERP.Item("MONo").Replace(" ", "")
        '        Dim MO As String = rowERP.Item("MONo").Replace(" ", "")
        '        Dim WCT100 As String = rowERP.Item("Workcenter")
        '        WCT100 = WC.Replace("W", "WC")
        '        Dim pWC = WC.Split(" ")
        '        Dim sWC As String = pWC(0)
        '        Dim Where As String = SFCB.WONo & " ='JP" & MO & "' AND " & SFCB.WorkStation & " ='" & sWC & "'"
        '        Dim dtT100 As DataTable = GetdateOracleBaase(Where)
        '        If dtT100.Rows.Count > 0 Then
        '            dr("StdLaborHursT100") = dtRowsFormat.FormatString(dtT100, SFCB.StandradLaborHours2)
        '            dr("StdMcHursT100") = dtRowsFormat.FormatString(dtT100, SFCB.StandradMachineHours2)
        '        End If
        '        dtPlanReport.Rows.Add(dr)
        '    Next rowERP
        '    If dtPlanReport.Rows.Count > 0 Then
        '        For Each rowData As DataRow In dtPlanReport.Rows
        '            Dim ERP_WC As String = rowData.Item("Workcenter")
        '            Dim ERP_PlanDate As String = rowData.Item("PlanDate")
        '            Dim ERP_MO_No As String = rowData.Item("MO_No")
        '            Dim ERP_StdManTime As String = rowData.Item("StdLaborHursT100")
        '            Dim ERP_StdMCTime As String = rowData.Item("StdMcHursT100")
        '            Dim MO As String = ERP_MO_No
        '            Dim pMO = MO.Split(" ")
        '            Dim sMO As String = pMO(0)
        '            Dim aasMO = sMO.Split("-")
        '            Dim objConn As New SqlConnection
        '            Dim objCm As New SqlCommand
        '            Dim ddtAdapter As New SqlDataAdapter
        '            objConn.ConnectionString = clsDBConnect.strMISConnectionString
        '            objConn.Open()
        '            Dim strSql As String = "UPDATE  PlanSchedule Set " &
        '         "MO_T100=@MO_T100" &
        '         ",WC_T100=@WC_T100 " &
        '         ",StdManT100=@StdManT100 " &
        '         ",StdMCT100=@StdMCT100 " &
        '          " WHERE PlanDate = '" & ERP_PlanDate & "' " &
        '          " And TA006 = @TA006 " &
        '          " And TA001 = @TA001  " &
        '          " And TA002 = @TA002  "
        '            objCm = New System.Data.SqlClient.SqlCommand(strSql, objConn)
        '            With objCm
        '                .Connection = objConn
        '                .CommandText = strSql
        '                .CommandType = CommandType.Text
        '                .Parameters.AddWithValue("@TA006", ERP_WC)
        '                .Parameters.AddWithValue("@TA001", aasMO(0))
        '                .Parameters.AddWithValue("@TA002", aasMO(1))
        '                .Parameters.AddWithValue("@MO_T100", ERP_MO_No)
        '                .Parameters.AddWithValue("@WC_T100", ERP_WC)
        '                .Parameters.AddWithValue("@StdManT100", ERP_StdManTime)
        '                .Parameters.AddWithValue("@StdMCT100", ERP_StdMCTime)
        '            End With
        '            Try
        '                objCm.ExecuteNonQuery()
        '            Catch ex As Exception
        '                MsgBox(ex.Message.ToString)
        '            End Try
        '        Next rowData
        '    End If
        'End If

        Dim sppDate As String = iyyyy & iMM.ToString("00")
        'MsgBox(Left(sppDate, 6))
        Dim WC As String = String.Empty
        Dim WhereWC As String = String.Empty
        Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWC.getObject)
        Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        If SelectWC > 0 Then
            WC = SelectWhereWC.Replace("WC", "W")
            WhereWC = "TA006 In(" & [String].Join("','", WC) & "') AND PlanDate Like '" & sppDate & "%'"
            '   Sql = Sql & " and " & ECAA.WorkcenterID & " In(" & [String].Join("','", MultiWorkcenter) & "')"
        Else
            WhereWC = "PlanDate Like '" & sppDate & "%'"
        End If
        Dim dtSQLserver As DataTable = GetdatePlanScheduleSQLserver(WhereWC)
        If dtSQLserver.Rows.Count > 0 Then
            GridView1.DataSource = dtSQLserver
            GridView1.DataBind()
            Call GenerateMOT100stLaborHours()
            Call updateStdHursT100ToPlanschedule()
        Else
            GridView1.DataSource = New List(Of String)
            GridView1.DataBind()
        End If
    End Sub
    Sub GenerateMOT100stLaborHours()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim MO As String = row.Cells(2).Text
                Dim pMO = MO.Split(" ")
                Dim WC As String = row.Cells(0).Text
                Dim sMO As String = pMO(0)
                WC = WC.Replace("W", "WC")
                Dim pWC = WC.Split(" ")
                Dim sWC As String = pWC(0)
                Dim Where As String = SFCB.WONo & " ='JP" & sMO & "' AND " & SFCB.WorkStation & " ='" & sWC & "'"
                Dim dtT100 As DataTable = GetdateOracleBaase(Where)
                If dtT100.Rows.Count > 0 Then
                    Dim lblWCT100 As Label = CType((row.FindControl("lblWCT100")), Label)
                    Dim lblWOT100 As Label = CType((row.FindControl("lblWOT100")), Label)
                    Dim lblStdLaborHursT100 As Label = CType((row.FindControl("lblStdLaborHursT100")), Label)
                    Dim lblStdMcHursT100 As Label = CType((row.FindControl("lblStdMcHursT100")), Label)
                    lblWCT100.Text = dtRowsFormat.FormatString(dtT100, SFCB.WorkStation)
                    lblWOT100.Text = dtRowsFormat.FormatString(dtT100, SFCB.WONo)
                    lblStdLaborHursT100.Text = dtRowsFormat.FormatString(dtT100, SFCB.StandradLaborHours2)
                    lblStdMcHursT100.Text = dtRowsFormat.FormatString(dtT100, SFCB.StandradMachineHours2)
                End If
            End If
        Next
    End Sub

    'Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
    '    GridView1.PageIndex = e.NewPageIndex
    '    Call getDatabindUpdateStdTime()
    'End Sub
    Private Sub updateStdHursT100ToPlanschedule()
        Dim sppDate As String = iyyyy & iMM.ToString("00")
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim MO As String = row.Cells(2).Text
                Dim pMO = MO.Split(" ")
                Dim WC As String = row.Cells(0).Text
                Dim sMO As String = pMO(0)
                WC = WC.Replace("W", "WC")
                Dim pWC = row.Cells(0).Text.Split(" ")
                Dim sWC As String = pWC(0)
                Dim Where As String = SFCB.WONo & " ='JP" & sMO & "' AND " & SFCB.WorkStation & " ='" & sWC & "'"
                Dim lblWCT100 As Label = CType((row.FindControl("lblWCT100")), Label)
                Dim lblWOT100 As Label = CType((row.FindControl("lblWOT100")), Label)
                Dim lblStdLaborHursT100 As Label = CType((row.FindControl("lblStdLaborHursT100")), Label)
                Dim lblStdMcHursT100 As Label = CType((row.FindControl("lblStdMcHursT100")), Label)
                Dim sPlanDate As String = sppDate
                Dim aasMO = sMO.Split("-")
                Dim objConn As New SqlConnection
                Dim objCm As New SqlCommand
                Dim ddtAdapter As New SqlDataAdapter
                objConn.ConnectionString = clsDBConnect.strMISConnectionString
                objConn.Open()
                Dim strSql As String = "UPDATE  PlanSchedule Set " &
             "MO_T100=@MO_T100" &
             ",WC_T100=@WC_T100 " &
             ",StdManT100=@StdManT100 " &
             ",StdMCT100=@StdMCT100 " &
              " WHERE PlanDate Like '" & sPlanDate & "%' " &
              " And TA006 = @TA006 " &
              " And TA001 = @TA001  " &
              " And TA002 = @TA002  "
                objCm = New System.Data.SqlClient.SqlCommand(strSql, objConn)
                With objCm
                    .Connection = objConn
                    .CommandText = strSql
                    .CommandType = CommandType.Text
                    '.Parameters.AddWithValue("@PlanDate", " Like '" & Left(lblPlanDate.Text, 6) & "%'")
                    .Parameters.AddWithValue("@TA006", sWC)
                    .Parameters.AddWithValue("@TA001", aasMO(0))
                    .Parameters.AddWithValue("@TA002", aasMO(1))
                    .Parameters.AddWithValue("@MO_T100", lblWCT100.Text)
                    .Parameters.AddWithValue("@WC_T100", lblWOT100.Text)
                    .Parameters.AddWithValue("@StdManT100", lblStdLaborHursT100.Text)
                    .Parameters.AddWithValue("@StdMCT100", lblStdMcHursT100.Text)
                End With
                Try
                    objCm.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message.ToString)
                End Try
            End If
        Next
        GridView1.Visible = False
    End Sub
    Private Shared SqlDataMOstdTimeT100 As String = " select " & SFCB.StandradLaborHours2 & " , " & SFCB.StandradMachineHours2 & ", " &
    " " & SFCB.WONo & "," & SFCB.WorkStation & "  " &
" from " & SFCB.tblMOprocessItem_SFCB & " where  @pWhereCustomUsing "
    Private Shared Function GetdateOracleBaase(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataMOstdTimeT100
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql = SqlDataMOprocessT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            Dim yyyy As String = iyyyy
            Dim Title As String = "<span style='color:Blue;'>" & yyyy & "</span>"
            HeaderCell2.Text = vbTab & vbTab & "Group. Workstaion"
            HeaderCell2.Font.Bold = True
            'HeaderCell2.ForeColor = System.Drawing.Color.Orange
            HeaderCell2.ForeColor = ColorTranslator.FromHtml("#DF7401")
            'HeaderCell2.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell2.ColumnSpan = 3
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell2)
            gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

            Dim HeaderCell As New TableCell()
            Dim nameMonth As String = ShowMonth(iMM.ToString("00"))
            HeaderCell.Text = "PlanSchedule Machine_Loading  " & "<span style='color:#DF7401;'>" & " Year: " & yyyy & " Month : " & nameMonth & "</span>"
            HeaderCell.ColumnSpan = (e.Row.Cells.Count)
            HeaderCell.Font.Bold = True
            'HeaderCell.ForeColor = System.Drawing.Color.Yellow
            'HeaderCell.BackColor = ColorTranslator.FromHtml("#93979A")
            'HeaderCell.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell)
            gvShow.Controls(0).Controls.AddAt(1, HeaderRow)
        End If
    End Sub
    Private Shared Function ShowMonth(mm As String) As String
        Return DateTime.ParseExact(mm, "MM", CultureInfo.CurrentCulture).ToString("MMMM")
    End Function
    Private Shared Function GetSheardMonth(thisMonth As String) As String
        Dim name As String
        name = MonthName(thisMonth, True)
        Return name
    End Function
    Private Sub CreateHeadGeridShow()
        Dim i As Integer = 3
        While i < gvShow.HeaderRow.Cells.Count
            Dim CheckDay = gvShow.HeaderRow.Cells(i).Text.Split("()")
            Dim xxx = CheckDay(1).Split(")")
            Dim CheckDayOfWeek As String = xxx(0)
            Dim CheckDayNumOfMonth As String = xxx(1)
            If CheckDayOfWeek = "Sun" Then
                gvShow.HeaderRow.Cells(i).Text = "<span style='color:Red;'>(" & CheckDayOfWeek & ")</span>" & CheckDayNumOfMonth
            ElseIf CheckDayOfWeek = "Sat" Then
                gvShow.HeaderRow.Cells(i).Text = "<span style='color:Yellow;'>(" & CheckDayOfWeek & ")</span>" & CheckDayNumOfMonth
            Else
                gvShow.HeaderRow.Cells(i).Text = "(" & CheckDayOfWeek & ")" & CheckDayNumOfMonth
            End If
            For Each row As GridViewRow In gvShow.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim WC As String = row.Cells(0).Text
                    WC = WC.Replace("WC", "W")
                    Dim McLoading As String = String.Empty
                    Dim SqlDataMC As String = "SELECT wc,capacity,mancapacity from MachineCapacity where  wc='" & WC & "'  "
                    Dim dtMCloading As DataTable = GetDataTableFindMC(SqlDataMC)
                    If dtMCloading.Rows.Count > 0 Then
                        McLoading = dtRowsFormat.FormatString(dtMCloading, "capacity")
                        row.Cells(2).Text = McLoading
                        row.Cells(2).Text = CalcHHMM(row.Cells(2).Text)
                    End If
                    'If row.Cells(i).Text <> String.Empty Then
                    Dim sppDate As String = iyyyy & iMM.ToString("00") & CheckDayNumOfMonth
                    Dim WhereWC As String = String.Empty
                    WhereWC = " TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
                    Dim dtT100laborStd As DataTable = GetdateMOprocessT100(WhereWC)
                    If dtT100laborStd.Rows.Count > 0 Then
                        Dim Std_LaborHours As String = dtRowsFormat.FormatString(dtT100laborStd, "StdLaborTime")
                        Dim Std_McHours As String = dtRowsFormat.FormatString(dtT100laborStd, "StdMcTime")
                        If Std_McHours <> String.Empty Then
                            Dim iMC As Integer = CInt(McLoading)
                            Dim iMcDays As Integer = CInt(Std_McHours)
                            If iMcDays > iMC Then
                                row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                            End If
                            row.Cells(i).Text = Std_McHours
                            row.Cells(i).Text = CalcHHMM(row.Cells(i).Text)
                            row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                            'Dim sendPalnDate As String = iMM.ToString("00") & "/" & CheckDayNumOfMonth & "/" & iyyyy
                            'row.Cells(i).Attributes.Add("style", "cursor:pointer;")
                            'row.Cells(i).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                            'row.Cells(i).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                            'row.Cells(i).Attributes.Add("onclick", "location='PlanScheduleAdd.aspx?PlanDate=" & sendPalnDate & "&WC=" & row.Cells(0).Text & "'")
                        End If
                        Dim sendPalnDate As String = iyyyy & "/" & iMM.ToString("00") & "/" & CheckDayNumOfMonth
                        row.Cells(i).Attributes.Add("style", "cursor:pointer;")
                        row.Cells(i).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                        row.Cells(i).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                        row.Cells(i).Attributes.Add("onclick", "location='PlanScheduleAdd.aspx?PlanDate=" & sendPalnDate & "&WC=" & row.Cells(0).Text & "'")

                    Else
                        row.Cells(i).Text = "Not data"
                    End If
                    row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                End If
            Next
            i += 1
        End While
    End Sub
    Private Shared SqlDataMOprocessT100 As String = " select sum(CAST(round([StdManT100],2) AS decimal(18,0))) as StdLaborTime, " &
    " sum(CAST(round([StdMCT100],2) AS decimal(18,0))) as StdMcTime  " &
    " from PlanSchedule where  @pWhereCustomUsing and Cancled='0' "
    Private Shared Function GetdateMOprocessT100(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataMOprocessT100
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql = SqlDataMOprocessT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared SqlDataPlanScheduleSQLserver As String = " select TA006 as Workcenter,PlanDate,TA001+'-'+TA002 as MONo " &
" from PlanSchedule where  @pWhereCustomUsing "
    Private Shared Function GetdatePlanScheduleSQLserver(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataPlanScheduleSQLserver
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdatePlanScheduleSQLserver", "Sql = SqlDataPlanScheduleSQLserver", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class