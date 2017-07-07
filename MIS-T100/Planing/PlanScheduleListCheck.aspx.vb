Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class PlanScheduleListCheck
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'CreateTable.CreateMoBalanceTable()
            'CreateTable.CreatePlanScheduleTable()
            'Dim SQL As String = "select MD001,MD001+':'+MD002 as MD002 from CMSMD order by MD001 "
            'ControlForm.showCheckboxList(cblWorkCenter, SQL, "MD002", "MD001", 4, Conn_SQL.ERP_ConnectionString)

            'ddlMonth.Text = Now.Month
            'showGridview(True)
        End If
    End Sub
    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch.Click
        Call showDataBind()
    End Sub
    Private Sub showDataBind()
        Dim Sday As String = String.Empty
        Dim Tday As String = String.Empty
        Dim WHR As String = String.Empty
        Dim aSQL As String = "Select WC from UserPlanAuthority where Id='" & Session("UserId") & "' "
        'Dim aWC As String = UserAuthen.LevelWorkcenterT100(Session("UserId"))

        Dim dtWCT100 As New DataTable
        Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWorkstation.getObject)
        Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        If SelectWC > 0 Then
            dtWCT100 = GetDataWhereWrokstatinDataT100(SelectWhereWC)
        Else
            dtWCT100 = GetWrokstatinDataT100()
        End If
        If txtDateFrom.Text <> String.Empty AndAlso txtDateTo.Text <> String.Empty Then
            Dim pSdate As Date = CDate(txtDateFrom.Text)
            Dim pTdate As Date = CDate(txtDateTo.Text)
            Sday = pSdate.ToString("MM/dd/yyyy")
            Tday = pTdate.ToString("MM/dd/yyyy")
        Else
            Dim SqlGetDate As String = "Select to_char(TO_DATE(sysdate),'mm/dd/yyyy') as getDate From dual"
            Dim GetSysDate As DataTable = GetDataOracleDate(SqlGetDate)
            Sday = dtRowsFormat.FormatString(GetSysDate, "getDate")
            Tday = dtRowsFormat.FormatString(GetSysDate, "getDate")
        End If
        'Call getDatabindUpdateStdTime()
        Dim ds As DataSet = AddDateBetweenToDataTable(dtWCT100, Sday, Tday)
        gvShow.DataSource = ds
        gvShow.DataBind()
        Call CreateHeadGeridShow()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub
    Private Sub CreateHeadGeridShow()
        Dim i As Integer = 3
        While i < (gvShow.HeaderRow.Cells.Count - 3)
            Dim varliaName = gvShow.HeaderRow.Cells(i).Text.Split(" ")
            Dim Title As String = varliaName(0)
            Dim pSDay = varliaName(1).Split("()")
            Dim pDate As String = pSDay(0)
            Dim DayName = pSDay(1).Split(")")
            Dim CheckDayOfWeek As String = DayName(0)
            Dim pTitle As String = "<span style='color:Cyan;'>" & Title & "</span>"
            If CheckDayOfWeek = "Sun" Then
                gvShow.HeaderRow.Cells(i).Text = pDate & "<span style='color:Red;'>(" & CheckDayOfWeek & ")</span>" & " " & pTitle
            ElseIf CheckDayOfWeek = "Sat" Then
                gvShow.HeaderRow.Cells(i).Text = pDate & "<span style='color:Yellow;'>(" & CheckDayOfWeek & ")</span>" & " " & pTitle
            Else
                gvShow.HeaderRow.Cells(i).Text = pDate & "(" & CheckDayOfWeek & ")" & " " & pTitle
            End If
            For Each row As GridViewRow In gvShow.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim WC As String = row.Cells(0).Text
                    WC = WC.Replace("WC", "W")
                    Dim McLoading As String = String.Empty
                    Dim Std_LaborHours As String = String.Empty
                    Dim Std_McHours As String = String.Empty
                    Dim strTrsOute As String = String.Empty
                    Dim strPlanQty As String = String.Empty
                    Dim TrsOuteD As Double = 0
                    Dim PlanQtyD As Double = 0
                    Dim checkPlanQty As Double = 0
                    Dim SqlDataMC As String = "SELECT wc,capacity,mancapacity from MachineCapacity where  wc='" & WC & "'  "
                    Dim dtMCloading As DataTable = GetDataSQLserverMIS(SqlDataMC)
                    If dtMCloading.Rows.Count > 0 Then
                        McLoading = dtRowsFormat.FormatString(dtMCloading, "capacity")
                        row.Cells(2).Text = McLoading
                        row.Cells(2).Text = CalcHHMM(row.Cells(2).Text)
                        row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                    End If

                    Dim sppDate As String
                    If pDate <> String.Empty Then
                        Dim xDD = pDate.Split("-")
                        Dim xYY As String = xDD(2)
                        Dim xMM As String = xDD(1)
                        Dim xDay As String = xDD(0)
                        sppDate = xYY & xMM & xDay
                        Dim WhereWC As String = String.Empty
                        '### MC_T ***********************************************
                        If Title = "MC_T" Then
                            WhereWC = " TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
                            Dim dtT100laborStd As DataTable = GetdateMOprocessT100(WhereWC)
                            If dtT100laborStd.Rows.Count > 0 Then
                                Std_LaborHours = dtRowsFormat.FormatString(dtT100laborStd, "StdLaborTime")
                                Std_McHours = dtRowsFormat.FormatString(dtT100laborStd, "StdMcTime")
                            End If
                            If Std_McHours <> String.Empty Then
                                row.Cells(i).Text = Std_McHours
                                row.Cells(i).Text = CalcHHMM(row.Cells(i).Text)
                            End If
                        End If
                        '### PlanQty ***********************************************
                        If Title = "PlanQty" Then
                            Dim Sql As String = "  Select sum(CAST(round([PlanQty],2) As Decimal(18,0))) As SumPlanQty " &
    " from PlanSchedule where  Cancled='0' and TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
                            Dim dtT100PlanQty As DataTable = GetdateCustomMOprocessT100(Sql)
                            If dtT100PlanQty.Rows.Count > 0 Then
                                Dim PlanQty As String = dtRowsFormat.FormatString(dtT100PlanQty, "SumPlanQty")
                                If PlanQty <> String.Empty Then
                                    Dim dblNumberPlanQty As Double = CDbl(PlanQty)
                                    row.Cells(i).Text = String.Format("{0:n0}", dblNumberPlanQty)
                                    checkPlanQty = dblNumberPlanQty
                                    strPlanQty = String.Format("{0:n0}", dblNumberPlanQty)
                                End If
                            End If
                        End If
                        '### TrsOut ***********************************************
                        If Title = "TrsOut" Then
                            Dim WCT100 = WC.Replace("W", "WC")
                            Dim sppDateT100 As String = sppDate
                            Dim T100WC As String = SFCB.WorkStation & " = '" & [String].Join("','", WCT100) & "'"
                            Dim TrsT100Date As String = SFFB.DocumentDate & " = TO_DATE('" & [String].Join("','", sppDateT100) & "','yyyymmdd')"
                            Dim T100ProcessWhere As String = T100WC & " and " & TrsT100Date
                            Dim Sql As String = " select cast(sum(" & SFCB.GoodTransferOut & ") as integer) as GoodTransferOut " &
    " from " & SFCB.tblMOprocessItem_SFCB & " " &
    " left join " & SFFB.tblTransferHead & " on " & SFFB.WONo & "=" & SFCB.WONo & " and " & SFFB.RunCard & "=" & SFCB.RunCard & " " &
    " And " & SFFB.OperationNo & "=" & SFCB.OperationID & " and " & SFFB.OperationSequence & "=" & SFCB.OperationSeq & " " &
    " And " & SFFB.Workstation & "=" & SFCB.WorkStation & " " &
    " where  " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO'  And " & T100ProcessWhere & " "
                            Dim dtT100GoodTransferOut As DataTable = GetDataCustomOracle(Sql)
                            If dtT100GoodTransferOut.Rows.Count > 0 Then
                                Dim GoodTransferOut As String = dtRowsFormat.FormatString(dtT100GoodTransferOut, "GoodTransferOut")
                                If GoodTransferOut <> String.Empty Then
                                    Dim dblNumberGoodTransferOut As Double = CDbl(GoodTransferOut)
                                    row.Cells(i).Text = String.Format("{0:n3}", dblNumberGoodTransferOut)
                                    strTrsOute = String.Format("{0:n3}", dblNumberGoodTransferOut)
                                    If checkPlanQty < dblNumberGoodTransferOut Then
                                        row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                                    End If
                                End If
                            End If
                        End If
                        '### % ***********************************************
                        If Title = "%" Then
                            Dim Sql As String = "  Select sum(CAST(round([PlanQty],2) As Decimal(18,0))) As SumPlanQty " &
    " from PlanSchedule where  Cancled='0' and TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
                            Dim dtT100PlanQty As DataTable = GetdateCustomMOprocessT100(Sql)
                            If dtT100PlanQty.Rows.Count > 0 Then
                                Dim PlanQty As String = dtRowsFormat.FormatString(dtT100PlanQty, "SumPlanQty")
                                If PlanQty <> String.Empty Then
                                    Dim dblNumberPlanQty As Double = CDbl(PlanQty)
                                    row.Cells(i).Text = String.Format("{0:n0}", dblNumberPlanQty)
                                    PlanQtyD = dblNumberPlanQty
                                End If
                            End If
                            Dim WCT100 = WC.Replace("W", "WC")
                            Dim sppDateT100 As String = sppDate
                            Dim T100WC As String = SFCB.WorkStation & " = '" & [String].Join("','", WCT100) & "'"
                            Dim TrsT100Date As String = SFFB.DocumentDate & " = TO_DATE('" & [String].Join("','", sppDateT100) & "','yyyymmdd')"
                            Dim T100ProcessWhere As String = T100WC & " and " & TrsT100Date
                            Dim SqlT100 As String = " select cast(sum(" & SFCB.GoodTransferOut & ") as integer) as GoodTransferOut " &
        " from " & SFCB.tblMOprocessItem_SFCB & " " &
        " left join " & SFFB.tblTransferHead & " on " & SFFB.WONo & "=" & SFCB.WONo & " and " & SFFB.RunCard & "=" & SFCB.RunCard & " " &
        " And " & SFFB.OperationNo & "=" & SFCB.OperationID & " and " & SFFB.OperationSequence & "=" & SFCB.OperationSeq & " " &
        " And " & SFFB.Workstation & "=" & SFCB.WorkStation & " " &
        " where  " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO'  And " & T100ProcessWhere & " "
                            Dim dtT100GoodTransferOut As DataTable = GetDataCustomOracle(SqlT100)
                            If dtT100GoodTransferOut.Rows.Count > 0 Then
                                Dim GoodTransferOut As String = dtRowsFormat.FormatString(dtT100GoodTransferOut, "GoodTransferOut")
                                If GoodTransferOut <> String.Empty Then
                                    Dim dblNumberGoodTransferOut As Double = CDbl(GoodTransferOut)
                                    row.Cells(i).Text = String.Format("{0:n3}", dblNumberGoodTransferOut)
                                    TrsOuteD = dblNumberGoodTransferOut
                                End If
                            End If
                            If TrsOuteD <> 0 And PlanQtyD <> 0 Then
                                Dim Result As Double = (TrsOuteD / PlanQtyD) * 100
                                row.Cells(i).Text = String.Format("{0:n2}", Result)
                            Else
                                row.Cells(i).Text = String.Empty
                            End If
                            If PlanQtyD < TrsOuteD Then
                                row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                            End If
                        End If

                        If McLoading <> String.Empty AndAlso Std_McHours <> String.Empty Then
                            Dim iMcLoading As Integer = CInt(McLoading)
                            Dim iStd_McHours As Integer = CInt(Std_McHours)
                            If iStd_McHours > iMcLoading Then
                                row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                            End If
                        End If
                        If Title = "MC_T" Then
                            If row.Cells(i).Text <> String.Empty Then
                                Dim wcT100 As String = WC.Replace("W", "WC")
                                Dim PlanDate As String = xYY & "/" & xMM & "/" & xDay
                                row.Cells(i).Attributes.Add("style", "cursor:pointer;")
                                row.Cells(i).Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#FFFFFF'")
                                row.Cells(i).Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                                ' row.Cells(i).Attributes.Add("onclick", "location='PlanScheduleAdd.aspx?PlanDate=" & sendPalnDate & "&WC=" & row.Cells(0).Text & "'")
                                row.Cells(i).Attributes.Add("onclick", "NewWindow('PlanScheduleCheck.aspx?WCT100=" & wcT100 & "&plandate=" & PlanDate & "','PlanScheduleCheck',1000,750,'yes')")
                            End If
                        End If
                    End If


                    End If


            Next
            i += 1
        End While
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
    Private Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            'Dim Title As String = "<span style='color:Blue;'>" & yyyy & "</span>"
            HeaderCell2.Text = "Group. Workstaion"
            HeaderCell2.Font.Size = 12
            HeaderCell2.Font.Bold = True
            'HeaderCell2.ForeColor = System.Drawing.Color.Orange
            HeaderCell2.ForeColor = ColorTranslator.FromHtml("#DF7401")
            'HeaderCell2.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell2.ColumnSpan = 3
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell2)
            gvShow.Controls(0).Controls.AddAt(0, HeaderRow)

            Dim Fdate As String = String.Empty
            Dim Tdate As String = String.Empty
            Dim HeaderCell As New TableCell()
            If txtDateFrom.Text <> String.Empty AndAlso txtDateTo.Text <> String.Empty Then
                Dim pSdate As Date = CDate(txtDateFrom.Text)
                Dim pTdate As Date = CDate(txtDateTo.Text)
                Fdate = pSdate.ToString("yyyy/MM/dd")
                Tdate = pTdate.ToString("yyyy/MM/dd")
            Else
                Dim SqlGetDate As String = "Select to_char(TO_DATE(sysdate),'yyyy/mm/dd') as getDate From dual"
                Dim GetSysDate As DataTable = GetDataOracleDate(SqlGetDate)
                Fdate = dtRowsFormat.FormatString(GetSysDate, "getDate")
                Tdate = dtRowsFormat.FormatString(GetSysDate, "getDate")
            End If
            HeaderCell.Text = "Plan Schedule List  " & "<span style='color:#DF7401;'>" & "  Date: " & Fdate & "  ~ " & Tdate & "</span>"
            HeaderCell.ColumnSpan = (e.Row.Cells.Count)
            HeaderCell.Font.Size = 12
            HeaderCell.Font.Bold = True
            'HeaderCell.ForeColor = System.Drawing.Color.Yellow
            'HeaderCell.BackColor = ColorTranslator.FromHtml("#93979A")
            'HeaderCell.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell)
            gvShow.Controls(0).Controls.AddAt(1, HeaderRow)
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
        tbl.Columns.Add(New DataColumn("Machine_Load", GetType(String)))
        For offset = 0 To (EndDate - StartDate).Days
            tbl.Columns.Add(New DataColumn("MC_T " & StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
            tbl.Columns.Add(New DataColumn("PlanQty " & StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
            tbl.Columns.Add(New DataColumn("TrsOut " & StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
            tbl.Columns.Add(New DataColumn("% " & StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
        Next
        tbl.Columns.Add(New DataColumn("Sum_Plan", GetType(String)))
        tbl.Columns.Add(New DataColumn("Sum_Actual", GetType(String)))
        tbl.Columns.Add(New DataColumn("Sum_Actual%", GetType(String)))
        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
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
            GetPageError.GetPage(FilePage, "GetDataTableFind", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
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
    Private Shared Function GetdateCustomMOprocessT100(Sql As String) As DataTable
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
    Private Shared Function GetDataCustomOracle(Sql As String) As DataTable
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
            GetPageError.GetPage(FilePage, "GetDataTableFindMC", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "W/C"
            e.Row.Cells(1).Text = "Name"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim strDate As String = dateReturn(ucDateFrom.dateVal, "yyyyMMdd")

            'Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", CultureInfo.InvariantCulture)

            'Dim Syear As String,
            '    Smonth As String,
            '    Sdate As String
            'Dim wc As String = .Cells(0).Text.Trim.Substring(0, 3)
            'Dim wcName As String = .Cells(0).Text.Trim
            'Dim j As Integer = 0
            'Dim xx() As String = .Cells(1).Text.Split(":")
            'Dim minLoad As Decimal = CDec(xx(0) * 60) + CDec(xx(1))

            'Dim runStep As Integer = 1
            'If cbPercent.Checked Then
            '    runStep = 4
            'End If

            'For i As Decimal = 2 To gvShow.HeaderRow.Cells.Count - runStep Step runStep
            '    With .Cells(i)
            '        Dim NextDate As Date = beginDate.AddDays(j)
            '        Syear = NextDate.Year.ToString
            '        Smonth = NextDate.Month.ToString
            '        Sdate = NextDate.Day.ToString

            '        Dim yy() As String = .Text.Replace("&nbsp;", "0:0").Split(":")
            '        Dim AcMinLoad As Decimal = CDec(yy(0) * 60)
            '        AcMinLoad += If(yy.Length > 1, yy(1), 0)
            '        If AcMinLoad > minLoad Then
            '            .ForeColor = Drawing.Color.Red
            '        End If

            '        .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '        '.Attributes.Add("onclick", "NewWindow('PlanScheduleCheck.aspx?wc=" & wc & "&wcName=" & wcName & "&plandate=" & getDate(Sdate, Smonth, Syear) & "','PlanScheduleCheck',900,600,'yes')")
            '        'strDays = strDays + 1
            '        j = j + 1
            '    End With
            'Next
            '    .Cells(4).Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '    .Cells(4).Attributes("onclick") = "javascript:NewWindow('PlanScheduleCheck.aspx?wc=" & .Cells(0).Text.Replace(" ", "") & "&planno=" & .Cells(1).Text.Replace(" ", "") & "&plandate=" & .Cells(2).Text.Replace(" ", "") & "','CheckPlanSchedule','700','500','no');"

        End If
    End Sub

    'Protected Function getManWorkCenter() As Hashtable
    '    Dim XSQL As String = "select MD001 from CMSMD where CMSMD.UDF01='M'"
    '    Dim xdt As DataTable = Conn_SQL.Get_DataReader(XSQL, Conn_SQL.ERP_ConnectionString)
    '    Dim listWCMan As Hashtable = New Hashtable
    '    If xdt.Rows.Count > 0 Then
    '        For i As Integer = 0 To xdt.Rows.Count - 1
    '            Dim wc As String = Trim(xdt.Rows(i).Item(0))
    '            listWCMan.Add(wc, wc)
    '        Next
    '    End If
    '    Return listWCMan
    'End Function

    Protected Function strFldTime(fldName As String, Optional unitSec As Boolean = True) As String
        Dim strTime As String
        If unitSec Then 'second
            strTime = "cast(FLOOR(" & fldName & "/3600) as varchar(10))+':'+right('0'+cast(floor((" & fldName & " % 3600)/60)as varchar(10)),2)"
        Else 'minite
            strTime = "cast(FLOOR(" & fldName & "/60) as varchar(10))+':'+right('0'+cast((" & fldName & " % 60)as varchar(10)),2)"
        End If
        Return strTime
    End Function

    ' Private Sub showGridviewWithPer()
    'Dim tempTable As String = "tempWorkCenterPlan"
    'Dim strDate As String = dateReturn(ucDateFrom.dateVal, "yyyyMMdd"),
    '    endDate As String = dateReturn(ucDateTo.dateVal, "yyyyMMdd")
    'CreateTempTable.createTempWorkCenterPlan(tempTable, strDate, endDate)

    'Dim SQL As String,
    '    WHR As String

    ''#2
    'Dim strSQL As String = ""

    'Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", CultureInfo.InvariantCulture)
    'Dim endDate2 As Date = DateTime.ParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture)
    'Dim amtDay As Short = DateDiff(DateInterval.Day, beginDate, endDate2)
    'For i As Integer = 0 To amtDay
    '    Dim pDate As String = beginDate.AddDays(i).ToString("yyyyMMdd", CultureInfo.InvariantCulture)

    '    WHR = Conn_SQL.Where("P.TA006", cblWorkCenter)
    '    Dim tomorow As String = beginDate.AddDays(i + 1).ToString("yyyyMMdd")
    '    'get basic data
    '    Dim fldVal As Hashtable,
    '       whrVal As Hashtable

    '    Dim tt1 As String = "round(((PlanQty)*(case when MOCTA.TA015=0 then 0 else round(SFCTA.TA022/MOCTA.TA015,0) end)),0)"
    '    Dim tt2 As String = "round(((PlanQty)*(case when MOCTA.TA015=0 then 0 else round(SFCTA.TA035/MOCTA.TA015,0) end)),0)"

    '    SQL = " select P.TA006,count(*) planCnt,sum(cast(case isnull(SFCTA.TA022,0) when '0' then '0' else " & tt1 & "  end as decimal(15,0))) stdMan," & _
    '          " sum(cast(case isnull(SFCTA.TA035,0) when '0' then '0' else " & tt2 & "  end as decimal(15,0))) stdMch ," & _
    '          " sum(cast(case isnull(P.ap100,0) when '0' then '0' else P.ap100  end as decimal(15,0))) ap100 " & _
    '          " from DBMIS.dbo.PlanSchedule P " & _
    '          " left join SFCTA on SFCTA.TA001=P.TA001 and SFCTA.TA002=P.TA002 and SFCTA.TA003=P.TA003 and SFCTA.TA004=P.TA004 and SFCTA.TA006= P.TA006 " & _
    '          " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
    '          " where P.Cancled='0' and P.PlanDate='" & pDate & "'  " & WHR & _
    '          " group by P.TA006 " & _
    '          " order by P.TA006 "
    '    Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
    '    For j As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(j)
    '            whrVal = New Hashtable
    '            whrVal.Add("wc", Trim(.Item("TA006")))
    '            fldVal = New Hashtable
    '            fldVal.Add("plan" & pDate, .Item("planCnt"))
    '            fldVal.Add("planTimeMan" & pDate, .Item("stdMan"))
    '            fldVal.Add("planTimeMch" & pDate, .Item("stdMch"))
    '            fldVal.Add("planTimeAP100" & pDate, .Item("ap100"))
    '            strSQL &= Conn_SQL.GetSQL(tempTable, fldVal, whrVal)
    '        End With
    '    Next

    '    'get actual time
    '    WHR &= Conn_SQL.Where("substring(SFCTC.CREATE_DATE,1,12)", pDate & "0801", tomorow & "0800")
    '    SQL = " select P.TA006,P.TA001,P.TA002,P.TA003,AVG(isnull(PlanQty,0)) pqty,sum(TC036) tQty " & _
    '          " from DBMIS.dbo.PlanSchedule P " & _
    '          " left join SFCTA on SFCTA.TA001=P.TA001 and SFCTA.TA002=P.TA002 and SFCTA.TA003=P.TA003 and SFCTA.TA004=P.TA004 and SFCTA.TA006= P.TA006 " & _
    '          " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
    '          " left join SFCTC on SFCTC.TC004 = SFCTA.TA001 and SFCTC.TC005 = SFCTA.TA002 and SFCTC.TC006 = SFCTA.TA003 " & _
    '          " where P.Cancled='0' and P.PlanDate='" & pDate & "' " & WHR & _
    '          " group by P.TA006,P.TA001,P.TA002,P.TA003 " & _
    '          " order by P.TA006,P.TA001,P.TA002,P.TA003 "

    '    dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)

    '    Dim lastWc As String = "",
    '       wc As String = "",
    '       actualPlan As Decimal = 0

    '    For j As Integer = 0 To dt.Rows.Count - 1
    '        With dt.Rows(j)
    '            wc = Trim(.Item("TA006"))
    '            If lastWc <> wc Then
    '                If lastWc <> "" Then
    '                    fldVal = New Hashtable
    '                    fldVal.Add("planActual" & pDate, actualPlan)
    '                    strSQL &= Conn_SQL.GetSQL(tempTable, fldVal, whrVal)
    '                End If

    '                whrVal = New Hashtable
    '                whrVal.Add("wc", wc)
    '                actualPlan = 0

    '            End If

    '            If .Item("tQty") >= .Item("pQty") Then
    '                actualPlan += 1
    '            End If
    '            lastWc = wc
    '        End With
    '    Next
    '    If lastWc <> "" Then
    '        fldVal = New Hashtable
    '        fldVal.Add("planActual" & pDate, actualPlan)
    '        strSQL &= Conn_SQL.GetSQL(tempTable, fldVal, whrVal)
    '    End If
    'Next
    'If strSQL <> "" Then
    '    Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)
    'End If
    'Dim colName As ArrayList = New ArrayList,
    '    fld As String = ""

    'Dim fldPlan As String = "0",
    '    fldActual As String = "0"

    'colName.Add("Work Center:A")
    'fld &= "rtrim(T.wc)+':'+rtrim(MD002) A"
    'colName.Add("W/C Capacity:B")
    'fld &= "," & strFldTime("M.capacity", False) & " B "
    'Dim fldName As String

    'For i As Integer = 0 To amtDay
    '    Dim pDate As String = beginDate.AddDays(i).ToString("yyyyMMdd", CultureInfo.InvariantCulture)
    '    Dim sDate As String = configDate.dateShow2(pDate, "-").Substring(0, 5)

    '    fldName = "T" & pDate
    '    colName.Add("T " & sDate & ":" & fldName)
    '    fld &= " ,case when isnull(UDF01,'')='' and isnull(UDF02,'')='M' then " & strFldTime("planTimeAP100" & pDate, False) & " else case when isnull(UDF01,'')='M' and isnull(UDF02,'')='' then " & strFldTime("planTimeMan" & pDate) & " else " & strFldTime("planTimeMch" & pDate) & " end end  " & fldName

    '    fldName = "P" & pDate
    '    colName.Add("P " & sDate & ":" & fldName & ":0")
    '    fld &= ",plan" & pDate & " " & fldName
    '    fldPlan &= "+plan" & pDate

    '    fldName = "A" & pDate
    '    colName.Add("A " & sDate & ":" & fldName & ":0")
    '    fld &= ",planActual" & pDate & " " & fldName
    '    fldActual &= "+planActual" & pDate

    '    fldName = "C" & pDate
    '    colName.Add("% " & sDate & ":" & fldName)
    '    fld &= ",cast(case when plan" & pDate & "=0 then 0 else cast(round((planActual" & pDate & "/plan" & pDate & ")*100,2) as numeric(35,2)) end as varchar(20))+'%'" & fldName

    'Next
    'fldName = "sPlan"
    'colName.Add("Sum Plan:" & fldName & ":0")
    'fld &= "," & fldPlan & " " & fldName

    'fldName = "sActual"
    'colName.Add("Sum Actual:" & fldName & ":0")
    'fld &= "," & fldActual & " " & fldName

    'fldName = "sPercent"
    'colName.Add("Sum Actual:" & fldName & ":0")
    'fld &= ",cast(case when " & fldPlan & "=0 then 0 else cast(round(((" & fldActual & ")/(" & fldPlan & "))*100,2) as numeric(35,2)) end as varchar(20))+'%'" & fldName

    'ControlForm.GridviewColWithLinkFirst(gvShow, colName)

    'SQL = " select " & fld & " from " & tempTable & " T " & _
    '      " left join " & Conn_SQL.DBMain & "..CMSMD on MD001=T.wc left join MachineCapacity M on M.wc=T.wc order by T.wc"

    'ControlForm.ShowGridView(gvShow, SQL, Conn_SQL.MIS_ConnectionString)
    'System.Threading.Thread.Sleep(1000)

    'End Sub

    ' Private Sub showGridview(Optional wcDefault As Boolean = False)
    ''lbMonth.Text = ddlMonth.Text
    ''lbYear.Text = tbYear.Text
    'Dim WHR As String = ""
    'If wcDefault Then
    '    WHR = " and MD001 in('W01','W02','W03','W04','W05','W06','W07','W13','W14','W15','W25','W27','W16','W28')"
    'Else
    '    WHR = Conn_SQL.Where("MD001", cblWorkCenter)
    'End If
    'Dim listWCMan As Hashtable = getManWorkCenter()


    ''get wc first multi mo/mch
    'Dim NSQL As String = "select MD001 from CMSMD where UDF02='M' order by MD001 "
    'Dim ndt As DataTable = Conn_SQL.Get_DataReader(NSQL, Conn_SQL.ERP_ConnectionString)
    'Dim listFirt As Hashtable = New Hashtable
    'For i As Integer = 0 To ndt.Rows.Count - 1
    '    With ndt.Rows(i)
    '        listFirt.Add(Trim(.Item(0)), Trim(.Item(0)))
    '    End With
    'Next

    'Dim strDate As String = dateReturn(ucDateFrom.dateVal, "yyyyMMdd"),
    '    endDate As String = dateReturn(ucDateTo.dateVal, "yyyyMMdd")

    'Dim beginDate As Date = DateTime.ParseExact(strDate, "yyyyMMdd", CultureInfo.InvariantCulture)
    'Dim lastDate As Date = DateTime.ParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture)
    'Dim amtDay As Short = DateDiff(DateInterval.Day, beginDate, lastDate)

    'Dim SQL As String = "select MD001,rtrim(MD001)+':'+MD002 as MD002,capacity from CMSMD left join DBMIS.dbo.MachineCapacity on wc=MD001 where 1=1 " & WHR & " order by MD001 ",
    '    sSQL As String = ""
    'Dim Program As New DataTable
    'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
    'With Program
    '    Dim dt As Data.DataTable = New DataTable
    '    If .Rows.Count > 0 Then
    '        dt.Columns.Add(New DataColumn("Work Center"))
    '        dt.Columns.Add(New DataColumn("Machine Load"))
    '        For i As Integer = 0 To amtDay
    '            Dim j As String = beginDate.AddDays(i).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
    '            'Next
    '            'For j As Integer = 1 To dayOfMonth
    '            Dim aa As String = CStr(j)
    '            dt.Columns.Add(New DataColumn("Day " & CStr(j)))
    '        Next

    '        For i As Integer = 0 To .Rows.Count - 1
    '            With .Rows(i)
    '                Dim wc As String = .Item("MD001")
    '                Dim dr As DataRow = dt.NewRow()
    '                dr("Work Center") = .Item("MD002").ToString.Trim
    '                Dim hmin As String = "0" & (.Item("capacity") Mod 60).ToString
    '                dr("Machine Load") = (Math.Floor(.Item("capacity") / 60)).ToString & ":" & hmin.Substring(hmin.Count - 2, 2).ToString
    '                Dim Program1 As New DataTable

    '                Dim tt1 As String = "round(((PlanQty)*(case when MOCTA.TA015=0 then 0 else round(SFCTA.TA022/MOCTA.TA015,0) end))/60,0)"
    '                Dim tt2 As String = "round(((PlanQty)*(case when MOCTA.TA015=0 then 0 else round(SFCTA.TA035/MOCTA.TA015,0) end))/60,0)"
    '                sSQL = "select case when CMSMD.UDF01='M' then '1' else '0' end from CMSMD where MD001='" & wc & "' "
    '                Dim manWorker As String = Conn_SQL.Get_value(sSQL, Conn_SQL.ERP_ConnectionString)

    '                sSQL = " select P.PlanDate,sum(cast(case isnull(SFCTA.TA022,0) when '0' then '0' else " & tt1 & "  end as decimal(15,0))) as stdMan," & _
    '                       " sum(cast(case isnull(SFCTA.TA035,0) when '0' then '0' else " & tt2 & "  end as decimal(15,0))) as stdMch, " & _
    '                       " sum(cast(case isnull(ap100,0) when '0' then '0' else ap100  end as decimal(15,0))) as ap100 " & _
    '                       " from DBMIS.dbo.PlanSchedule P " & _
    '                       " left join SFCTA on SFCTA.TA001=P.TA001 and SFCTA.TA002=P.TA002 and SFCTA.TA003=P.TA003 and SFCTA.TA004=P.TA004 and SFCTA.TA006= P.TA006 " & _
    '                       " left join MOCTA on MOCTA.TA001 = SFCTA.TA001 and MOCTA.TA002 = SFCTA.TA002 " & _
    '                       " where P.TA006='" & wc & "' and P.Cancled='0' and P.PlanDate between '" & strDate & "' and '" & endDate & "'  " & _
    '                       " group by P.PlanDate " & _
    '                       " order by P.PlanDate "
    '                'and  P.PlanDate like '" & lbYear.Text.Trim & mm & "%' 
    '                Program1 = Conn_SQL.Get_DataReader(sSQL, Conn_SQL.ERP_ConnectionString)
    '                Dim val As Hashtable = New Hashtable
    '                For k As Integer = 0 To Program1.Rows.Count - 1
    '                    Dim planTime As Decimal = CDec(Program1.Rows(k).Item("stdMch").ToString.Trim)
    '                    If listFirt.ContainsKey(wc.Trim) Then
    '                        planTime = CDec(Program1.Rows(k).Item("ap100").ToString.Trim)
    '                    End If
    '                    If listWCMan.ContainsKey(wc.Trim) Then
    '                        planTime = CDec(Program1.Rows(k).Item("stdMan").ToString.Trim)
    '                    End If
    '                    Dim Pdate As String = Program1.Rows(k).Item("PlanDate").ToString.Trim
    '                    Dim mchTime As Decimal = planTime
    '                    val.Add(Pdate, mchTime)
    '                Next
    '                For j As Integer = 0 To amtDay
    '                    Dim txtPlan As String = ""
    '                    Dim k As String = beginDate.AddDays(j).ToString("yyyyMMdd", CultureInfo.InvariantCulture)
    '                    If val.ContainsKey(k) Then
    '                        Dim min As String = "0" & (val(k) Mod 60).ToString
    '                        txtPlan = (Math.Floor(val(k) / 60)).ToString & ":" & min.Substring(min.Count - 2, 2)
    '                    End If
    '                    k = beginDate.AddDays(j).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
    '                    dr("Day " & CStr(k)) = txtPlan
    '                Next
    '                dt.Rows.Add(dr)
    '            End With
    '        Next
    '    End If

    '    ControlForm.GridviewFormat(gvShow, True)
    '    gvShow.DataSource = dt
    '    gvShow.DataBind()

    'End With
    'End Sub
End Class
