Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization

Public Class moStatusNotClosed
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Private Shared iyyyy As Integer
    Private Shared iMM As Integer
    Private Shared Sday As Integer = 1
    Private Shared Tday As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            HeaderForm1.HeaderLable = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            Call S_year()
            Call S_Month()
        End If
    End Sub
    Sub S_year()
        Dim i As Integer
        For i = 2015 To 2050
            DL_Year.Items.Add(i)
        Next i
        DL_Year.Items.FindByValue(System.DateTime.Now.Year.ToString).Selected = True
    End Sub
    Sub S_Month()
        Dim i As Integer
        For i = 1 To 12
            DL_Year.Items.Add(i.ToString("00"))
        Next i
        DL_Month.Items.FindByValue(System.DateTime.Now.Month.ToString("00")).Selected = True
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim where As String = String.Empty
        Dim WC As String = UsingWorkstation.getObject.SelectedValue
        Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWorkstation.getObject)
        Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
        If WC <> "0" Then
            If (SelectWC > 0) Then
                Dim EndMonth As Integer = Date.DaysInMonth(DL_Year.SelectedItem.Text, DL_Month.SelectedValue)
                Dim Fdate As String = DL_Year.SelectedItem.Text & "/" & DL_Month.SelectedValue & "/01"
                Dim Tdate As String = DL_Year.SelectedItem.Text & "/" & DL_Month.SelectedValue & "/" & EndMonth.ToString("00")
                'MsgBox(Fdate & "**" & Tdate)
                Dim FromDate As String = " TO_DATE('" & [String].Join("','", Fdate) & "','yyyy/mm/dd')"
                Dim ToDate As String = " TO_DATE('" & [String].Join("','", Tdate) & "','yyyy/mm/dd')"
                Dim whereWC As String = " and " & SFCB.WorkStation & " In(" & [String].Join("','", SelectWhereWC) & "')"
                where = whereWC & " And " & SFCB.PlannedCompletionDate & " BETWEEN " & FromDate & " And " & ToDate
                ' where = whereWC & " And substr(" & SFCB.WONo & ",3,4 ) In(" & [String].Join("','", SelectWhereMOtype) & "')"
                'End If
                Dim SqlData As String = String.Empty
                SqlData = " select " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
            " " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WIP & "," & SFCB.PlanStartDate & ", " &
            " " & ECAA.Workcenter & "," & SFAA.Status & "," & "substr(" & SFCB.WONo & ",3,4)" & " as MoType,  " &
            " " & SFAA.PlanedCompletionDate & "," & SFCB.PlannedCompletionDate & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
            " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & "," & SFCB.GoodTransferIn & " ," & SFCB.GoodTransferOut & ",  " &
            " " & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "," & SFCB.TransferInUnit & ", " &
            " " & XMDC.SaleOrderNo & "," & XMDA.CustomerId & " " &
            " from " & SFCB.tblMOprocessItem_SFCB & " " &
            " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFCB.WONo & " " &
            " left join " & SFBA.tblManufactureOrder_Body & " On " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
            " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " and " & XMDA.OrderType & "<>'5' " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
        " LEFT OUTER JOIN  " & ECAA.tblWorkcenter & " On  " & ECAA.WorkcenterID & "=" & SFCB.WorkStation & "  " &
            " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' and " & SFAA.Status & " not in('C','c') " & where & "  " &
            " group by " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
            " " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WIP & "," & SFCB.PlanStartDate & ", " &
            " " & ECAA.Workcenter & "," & SFAA.Status & "," & SFAA.PlanedCompletionDate & "," & SFCB.PlannedCompletionDate & ", " &
            " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
            " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & "," & SFCB.GoodTransferIn & " ," & SFCB.GoodTransferOut & ",  " &
            " " & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "," & SFCB.TransferInUnit & ", " &
            " " & XMDC.SaleOrderNo & "," & XMDA.CustomerId & " " &
            " order by  " & SFAA.PlanedCompletionDate & "," & SFCB.PlannedCompletionDate & " "
                Dim dtData As DataTable = GetDataOracleDate(SqlData)
                If dtData.Rows.Count > 0 Then
                    gvDetail.DataSource = dtData
                    gvDetail.DataBind()
                    ucCountRowDetail.RowCount = dtData.Rows.Count.ToString
                    GridviewUtility.GrigOnmouseHandleAuto(gvDetail)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvDetail", "gridviewScrollgvDetail();", True)
                    Dim SqlDataSummary As String = " select " & "substr(" & SFCB.WONo & ",3,4)" & " as Motype " &
            " from " & SFCB.tblMOprocessItem_SFCB & " " &
            " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFCB.WONo & " " &
            " left join " & SFBA.tblManufactureOrder_Body & " On " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
            " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " And " & XMDA.OrderType & "<>'5' " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
        " LEFT OUTER JOIN  " & ECAA.tblWorkcenter & " On  " & ECAA.WorkcenterID & "=" & SFCB.WorkStation & "  " &
            " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' and " & SFAA.Status & " not in('C','c') " & where & "  " &
            " group by " & "substr(" & SFCB.WONo & ",3,4)" & " " &
            " order by  " & "substr(" & SFCB.WONo & ",3,4)" & " "
                    Dim dtSummaryData As DataTable = GetDataOracleDate(SqlDataSummary)
                    If dtSummaryData.Rows.Count > 0 Then
                        Dim mmm As String = DL_Month.SelectedValue
                        Dim MonthNam As String = GetSheardMonth(mmm)
                        Dim yy As String = DL_Year.Text
                        Dim FindDate As String = "01-" & MonthNam & "-" & yy
                        'MsgBox(FindDate)
                        Dim SqlGetDateFind As String = "Select to_char(TO_DATE('" & FindDate & "'),'yyyy') as yyyy, " &
" to_char(TO_DATE('" & FindDate & "'),'mm') as mm, " &
" to_char(LAST_DAY('" & FindDate & "'),'dd') as EndDay From dual "
                        Dim GetSysDate As DataTable = GetDataTableFind(SqlGetDateFind)
                        iyyyy = CInt(dtRowsFormat.FormatString(GetSysDate, "yyyy"))
                        iMM = CInt(dtRowsFormat.FormatString(GetSysDate, "mm"))
                        Tday = CInt(dtRowsFormat.FormatString(GetSysDate, "EndDay"))
                        Dim ds As DataSet = AddDaysBetweenMonth(dtSummaryData, Sday, Tday)
                        gvSum.DataSource = ds
                        gvSum.DataBind()
                        Call CreateHeadGeridShow()
                    End If
                Else
                    gvDetail.DataSource = New List(Of String)
                    gvDetail.DataBind()
                End If
            End If
        End If

        'ControlForm.ShowGridView(gvDetail, SQL, Conn_SQL.ERP_ConnectionString)
        'ucCountRowDetail.RowCount = ControlForm.rowGridview(gvDetail)
        'System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollgvDetail", "gridviewScrollgvDetail();", True)
    End Sub
    Private Shared Function GetSheardMonth(thisMonth As String) As String
        Dim name As String
        name = MonthName(thisMonth, True)
        Return name
    End Function
    Private Sub CreateHeadGeridShow()
        Dim i As Integer = 2
        While i < gvSum.HeaderRow.Cells.Count
            Dim CheckDay = gvSum.HeaderRow.Cells(i).Text.Split("()")
            Dim xxx = CheckDay(1).Split(")")
            Dim CheckDayOfWeek As String = xxx(0)
            Dim CheckDayNumOfMonth As String = xxx(1)
            If CheckDayOfWeek = "Sun" Then
                gvSum.HeaderRow.Cells(i).Text = "<span style='color:Red;'>(" & CheckDayOfWeek & ")</span>" & CheckDayNumOfMonth
            ElseIf CheckDayOfWeek = "Sat" Then
                gvSum.HeaderRow.Cells(i).Text = "<span style='color:Yellow;'>(" & CheckDayOfWeek & ")</span>" & CheckDayNumOfMonth
            Else
                gvSum.HeaderRow.Cells(i).Text = "(" & CheckDayOfWeek & ")" & CheckDayNumOfMonth
            End If
            For Each row As GridViewRow In gvSum.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim MOtype As String = row.Cells(0).Text
                    If MOtype <> String.Empty Then
                        Dim dtMOtype As DataTable = OOBXL.GetMOTypeWhere_Table(MOtype)
                        If dtMOtype.Rows.Count > 0 Then
                            row.Cells(1).Text = dtRowsFormat.FormatString(dtMOtype, OOBXL.DocType)
                        End If
                    End If
                    Dim sppDate As String = iyyyy & iMM.ToString("00") & CheckDayNumOfMonth
                    Dim WC As String = UsingWorkstation.getObject.SelectedValue
                    Dim SelectWhereWC As String = SelectCheckBoxList.MultipleSelect(UsingWorkstation.getObject)
                    Dim SelectWC As Integer = SelectCheckBoxList.RowNumSelect
                    Dim WhereWC As String = SFCB.WorkStation & " In(" & [String].Join("','", SelectWhereWC) & "')"
                    Dim WhereDate As String = SFCB.PlannedCompletionDate & "= TO_DATE('" & sppDate & "','yyyymmdd') "
                    Dim where As String = WhereWC & " and " & WhereDate & " and substr(" & SFCB.WONo & ",3,4)='" & MOtype & "'"
                    Dim SqlGetMo As String = "select count(substr(" & SFCB.WONo & ",3,4)) as MOcount from " & SFCB.tblMOprocessItem_SFCB & " " &
                        " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' and " & where & " "
                    Dim dtMOProcess As DataTable = GetDataOracleDate(SqlGetMo)
                    If dtMOProcess.Rows.Count > 0 Then
                        Dim MoCount As String = dtRowsFormat.FormatString(dtMOProcess, "MOcount")
                        If MoCount <> "0" Then
                            row.Cells(i).Text = MoCount
                        End If
                    End If
                    row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                End If

            Next
            i += 1
        End While
    End Sub
    Private Shared Function AddDaysBetweenMonth(dt As DataTable, SDay As Integer, EDay As Integer) As DataSet
        Dim tbl As New DataTable
        Dim dr As DataRow = Nothing
        Dim columns As DataColumnCollection = dt.Columns
        Dim column As DataColumn
        For Each column In columns
            tbl.Columns.Add(New DataColumn(column.ColumnName, column.DataType))
        Next
        tbl.Columns.Add(New DataColumn("MO_Type_Name", GetType(String)))
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
    Private Shared Function GetDataTableFind(Sql As String) As DataTable
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
    Private Sub gvSum_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvSum.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            Dim yyyy As String = iyyyy
            Dim Title As String = "<span style='color:Blue;'>" & yyyy & "</span>"
            HeaderCell2.Text = vbTab & vbTab & "MO Type"
            HeaderCell2.Font.Bold = True
            'HeaderCell2.ForeColor = System.Drawing.Color.Orange
            HeaderCell2.ForeColor = ColorTranslator.FromHtml("#DF7401")
            'HeaderCell2.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell2.ColumnSpan = 2
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell2)
            gvSum.Controls(0).Controls.AddAt(0, HeaderRow)

            Dim HeaderCell As New TableCell()
            Dim nameMonth As String = ShowMonth(iMM.ToString("00"))
            HeaderCell.Text = "MO Type. Not Close " & "<span style='color:#DF7401;'>" & " Year: " & yyyy & " Month : " & nameMonth & "</span>"
            HeaderCell.ColumnSpan = (e.Row.Cells.Count)
            HeaderCell.Font.Bold = True
            'HeaderCell.ForeColor = System.Drawing.Color.Yellow
            'HeaderCell.BackColor = ColorTranslator.FromHtml("#93979A")
            'HeaderCell.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderRow.Cells.Add(HeaderCell)
            gvSum.Controls(0).Controls.AddAt(1, HeaderRow)
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim gvRowCount As String = "<span style='color:Blue;'>" & gvSum.Rows.Count.ToString() & "</span>"
            Dim stTotla As String = "MO Type Total: " + gvRowCount + ""
            Dim iFoot As Integer = CInt(gvSum.Columns.Count)
            'e.Row.Cells(0).Text = stTotla.ToString()
            'e.Row.Cells(0).ColumnSpan = CInt(e.Row.Cells.Count)
            'e.Row.Cells.RemoveAt(1)
            Dim index As Integer = gvSum.Rows.Count
            Dim row As New GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal)
            Dim cell As New TableCell()
            ' Dim txt As New TextBox()
            'txt.Width = 350
            ' cell.Controls.Add(txt)
            Dim cell2 As New TableCell()
            'row.Cells.Add(cell)
            cell2.Text = stTotla
            cell2.ColumnSpan = 2
            cell2.BorderColor = System.Drawing.Color.Blue
            cell2.BackColor = ColorTranslator.FromHtml("#E3E9EA")
            row.Cells.Add(cell2)
            gvSum.Controls(0).Controls.Add(row)
        End If
    End Sub
    Private Shared Function ShowMonth(mm As String) As String
        Return DateTime.ParseExact(mm, "MM", CultureInfo.CurrentCulture).ToString("MMMM")
    End Function
    Private Sub gvSum_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSum.RowDataBound
        'With e.Row
        '    If .RowType = DataControlRowType.DataRow Then
        '        Dim moType As String = .Cells(0).Text.Trim
        '        For i As Decimal = 2 To gvSum.HeaderRow.Cells.Count - 2
        '            With .Cells(i)
        '                .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
        '                Dim mm() As String = gvSum.HeaderRow.Cells(i).Text.Trim.Split("-")
        '                .Attributes.Add("onclick", "NewWindow('moStatusNotClosedPop.aspx?tempTable=tempMOnotClose" & Session("UserName") & "&motype=" & moType & "&selmonth=" & mm(1) & mm(0) & "','_blank',800,500,'yes')")
        '            End With
        '        Next
        '    End If
        '    If .RowType = DataControlRowType.Footer Then
        '        gvSum.ShowFooter = True
        '        .Cells(1).Text = "Sum"
        '        For i As Decimal = 2 To gvSum.HeaderRow.Cells.Count - 1
        '            Dim val As Decimal = 0
        '            For j As Decimal = 0 To gvSum.Rows.Count - 1
        '                val += Conn_SQL.checkNumberic(gvSum.Rows(j).Cells(i).Text)
        '            Next
        '            .Cells(i).Text = val
        '            .Cells(i).HorizontalAlign = HorizontalAlign.Right
        '        Next
        '    End If
        'End With
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("MoStatusNotClosed" & Session("UserName"), gvDetail)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Private Sub gvDetail_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDetail.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblMOstatus As Label = CType(e.Row.FindControl("lblMOstatus"), Label)
            If lblMOstatus.Text <> String.Empty Then
                lblMOstatus.Text = StatusT100.MO_Normal(lblMOstatus.Text)
            End If
            Dim lblMOtype As Label = CType(e.Row.FindControl("lblMOtype"), Label)
            If lblMOtype.Text <> String.Empty Then
                Dim dtMOtype As DataTable = OOBXL.GetMOTypeWhere_Table(lblMOtype.Text)
                If dtMOtype.Rows.Count > 0 Then
                    lblMOtype.Text = dtRowsFormat.FormatSumString(dtMOtype, OOBXL.DocTypeId, OOBXL.DocType)
                End If
            End If
            Dim lblOpId As Label = CType(e.Row.FindControl("lblOpId"), Label)
            Dim lblOperation As Label = CType(e.Row.FindControl("lblOperation"), Label)
            If lblOpId.Text <> String.Empty Then
                Dim dtOp As DataTable = OOCQL.GetDataOperation(lblOpId.Text)
                If dtOp.Rows.Count > 0 Then
                    lblOperation.Text = dtRowsFormat.FormatString(dtOp, OOCQL.Operation)
                End If
            End If
        End If
    End Sub
End Class