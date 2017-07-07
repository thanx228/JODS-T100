Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class PlanByMO
    Inherits System.Web.UI.Page
    Dim clsDB As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Const colStart As Integer = 8
    Const table As String = "PlanSchedule"
    Const dayOfMon As Integer = 31
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            reset()
            ' Call getFunctionGrid
        End If
    End Sub
    Private Sub getFunctionGrid()
        GridviewUtility.ShowHead(gvShow, colStart, dayOfMon)
        ' GridviewUtility.ShowHeadReport(gvCheck, 12, dayOfMon)
        GridviewUtility.GrigOnmouseHandleCustomer(gvShow, "#FAAC58")
        ' GridviewUtility.GrigOnmouseHandleAuto(gvCheck)
    End Sub
    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Call getdataMO_Header()
        Call getdataMO_Header_CompleQty()
        Call getdataProductionItem_Name()
        Call getdataSaleOrderDocNo()
        Call getdataTableXMDA()
        Call getdataCustomerDeatil()
        Call getdataShowActionPlan()
        System.Threading.Thread.Sleep(400)
        Call getFunctionGrid()
    End Sub
    Private Sub btChk_Click(sender As Object, e As EventArgs) Handles btChk.Click
        Call getdataMO_Header()
        Call getdataMO_Header_CompleQty()
        Call getdataProductionItem_Name()
        Call getdataSaleOrderDocNo()
        Call getdataTableXMDA()
        Call getdataCustomerDeatil()
        Call getdataShowActionPlan()
        Call getdataShowProgressMO()
        System.Threading.Thread.Sleep(400)
        Call getFunctionGrid()
    End Sub

    Private Sub getdataMO_Header()
        Dim JP As String = ReplaceString.ReplaceMO(UsingMO_Type.getObject.Text, tbMO.Text)
        Dim dt As DataTable = SFAA.GetMO_HeaderDeatil(JP)
        If dt.Rows.Count > 0 Then
            lbMO.Text = dtRowsFormat.FormatString(dt, SFAA.DocNo)
            lbItem.Text = dtRowsFormat.FormatString(dt, SFAA.ProductItem)
            lbQty.Text = dtRowsFormat.FormatDecimal(dt, SFAA.ProductionQty)
            lblScarpQty.Text = dtRowsFormat.FormatDecimal(dt, SFAA.ScarpQty)
            lblUnit.Text = dtRowsFormat.FormatString(dt, SFAA.Unit)
            'lblStartDate.Text = dtRowsFormat.FormatString(dt, SFAA.PlanStartDate)
            'lblEndDate.Text = dtRowsFormat.FormatString(dt, SFAA.PlanedCompletionDate)
        Else
            Call reset()
        End If
    End Sub
    Private Sub getdataMO_Header_CompleQty()
        Dim dt As DataTable = SFCA.GetDataMoProcessHeader(lbMO.Text)
        If dt.Rows.Count > 0 Then
            lblCompleteQty.Text = dtRowsFormat.FormatDecimal(dt, SFCA.CompletedQty)
        End If
    End Sub
    Private Sub getdataProductionItem_Name()
        Dim dtItem As DataTable = IMAAL.GetDataProducItem(lbItem.Text)
        If dtItem.Rows.Count > 0 Then
            lbSpec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
            lbDesc.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
        End If
    End Sub
    Private Sub getdataSaleOrderDocNo()
        Dim dtSaleItem As DataTable = XMDC.GetDataMoProcessHeader(lbItem.Text)
        If dtSaleItem.Rows.Count > 0 Then
            lblSaleDocNo.Text = dtRowsFormat.FormatString(dtSaleItem, XMDC.SaleOrderNo)

        End If
    End Sub
    Private Sub getdataTableXMDA()
        Dim dtSaleH As DataTable = XMDA.GetCustomerItemSale(lblSaleDocNo.Text)
        If dtSaleH.Rows.Count > 0 Then
            lblCkCust.Text = dtRowsFormat.FormatString(dtSaleH, XMDA.CustomerId)
        End If
    End Sub
    Private Sub getdataCustomerDeatil()
        Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(lblCkCust.Text)
        If dtCustName.Rows.Count > 0 Then
            If lblCkCust.Text <> "" Then
                lbCustName.Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerID) & " : " & dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerFullName)
            Else
                lbCustName.Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerName)
            End If
        End If
    End Sub
    Private Sub getdataShowActionPlan()
        Dim dtProcess As DataTable = SFCB.GetDataRowsProcessItem(lbMO.Text)
        If dtProcess.Rows.Count > 0 Then
            gvShow.DataSource = dtProcess
            gvShow.DataBind()
            gvCheck.DataSource = ""
            gvCheck.DataBind()
            ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            btSave.Visible = True
        End If
    End Sub
    Private Shared Function GetPlanSchedule(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetPlanSchedule", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '###################### Function for Add DateBetween Join To DataTable #############################################
    Private Shared Function AddDateBetweenToDataTable(dt As DataTable, SDate As String, EDate As String) As DataSet
        Dim StartDate As Date
        Dim EndDate As Date
        'If SDate <> String.Empty AndAlso EDate <> String.Empty Then
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
        'tbl.Columns.Add(New DataColumn("CompleteDate ", GetType(String)))
        For offset = 0 To (EndDate - StartDate).Days
            tbl.Columns.Add(New DataColumn(StartDate.AddDays(offset).ToString("dd-MM-yyyy(ddd)")))
        Next

        Dim dsA As New DataSet()
        dsA.Tables.Add(tbl)
        Return dsA
        ' End If
    End Function
    Private Sub getdataShowProgressMO()
        Dim dtCkProcess As DataTable = SFCB.GetDataRowsProcessItem(lbMO.Text)
        If dtCkProcess.Rows.Count > 0 Then
            Dim SqlActionPlan As String = "select min(convert(date, PlanDate,113)) as Sdate,max(convert(date, PlanDate,113)) as Edate  " &
               " from PlanSchedule where TA001='" & UsingMO_Type.getObject.Text & "' and TA002 = '" & tbMO.Text & "' "
            Dim dtPlan As DataTable = GetPlanSchedule(SqlActionPlan)
            If dtPlan.Rows.Count > 0 Then
                Dim strSdate As String = dtRowsFormat.FormatString(dtPlan, "Sdate")
                Dim strEdate As String = dtRowsFormat.FormatString(dtPlan, "Edate")
                'Dim aa As String = DateSerial(Left(strSdate, 4), Mid(strSdate, 6, 2), Right(strSdate, 2))
                'Dim bb As String = DateSerial(Left(strEdate, 4), Mid(strEdate, 6, 2), Right(strEdate, 2))
                If strSdate <> String.Empty Then
                    Dim Sdd As Date = CDate(strSdate)
                    lblStartDate.Text = Sdd.ToString("MM/dd/yyyy")
                End If
                If strEdate <> String.Empty Then
                    Dim Edd As Date = CDate(strEdate)
                    lblEndDate.Text = Edd.ToString("MM/dd/yyyy")
                End If
            End If

                If lblStartDate.Text <> "" AndAlso lblEndDate.Text <> "" Then
                Dim ds As DataSet = DataTableMultiple.AddDateBetweenToDataTable(dtCkProcess, lblStartDate.Text, lblEndDate.Text)
                gvCheck.DataSource = ds
                gvCheck.DataBind()
                GridviewUtility.GrigOnmouseHandleCustomer(gvCheck, "#008000")
                GridviewUtility.GridStyleTemplate_Std(gvCheck)
                Dim i As Integer = 13
                While i < gvCheck.HeaderRow.Cells.Count
                    Dim xxx As String = gvCheck.HeaderRow.Cells(i).Text
                    Dim sxx = xxx.Split("()")
                    Dim aaa As String = sxx(0)
                    Dim xStr = sxx(0).Split("-")
                    Dim yy As Integer = CInt(xStr(2))
                    Dim mm As Integer = CInt(xStr(1))
                    Dim dddd As Integer = CInt(xStr(0))
                    Dim Chdd As New Date(yy, mm, dddd)
                    Dim CheckDay As String = Chdd.ToString("ddd")
                    'gvCheck.HeaderRow.Cells(i).Text = Chdd.ToString("yyyMMdd")
                    If CheckDay = "Sat" Then
                        Dim TitleDate As String = Chdd.ToString("dd-MM-yyyy")
                        Dim sTitle As String = "<span style='color:Yellow;'>" & CheckDay & "</span>"
                        gvCheck.HeaderRow.Cells(i).Text = TitleDate & "(" & sTitle & ")"
                        'gvCheck.HeaderRow.Cells(i).ForeColor = System.Drawing.Color.Yellow
                    End If
                    If CheckDay = "Sun" Then
                        Dim TitleDate As String = Chdd.ToString("dd-MM-yyyy")
                        Dim sTitle As String = "<span style='color:Red;'>" & CheckDay & "</span>"
                        gvCheck.HeaderRow.Cells(i).Text = TitleDate & "(" & sTitle & ")"
                        ' gvCheck.HeaderRow.Cells(i).ForeColor = System.Drawing.Color.Red
                    End If
                    For Each row As GridViewRow In gvCheck.Rows
                        If row.RowType = DataControlRowType.DataRow Then
                            row.Cells(i).Text = Chdd.ToString("yyyyMMdd")
                            If row.Cells(i).Text <> String.Empty Then
                                Dim iSeq As Integer = CInt(row.Cells(0).Text)
                                Dim Mo_Type As String = UsingMO_Type.getObject.Text
                                Dim Mo_No As String = tbMO.Text
                                Dim Seq_Erp As String = iSeq.ToString("0000")

                                Dim SqlData As String = "select PlanQty,Cancled from PlanSchedule " &
                           " where TA001='" & UsingMO_Type.getObject.Text & "' and TA002 = '" & tbMO.Text & "' " &
                           " and TA003 ='" & Seq_Erp & "' and PlanDate='" & row.Cells(i).Text & "' "
                                ' lblSql.Text = SqlData
                                Dim strPlanQty As String = String.Empty
                                Dim Cancled As String = String.Empty
                                Dim dtDataPlan As DataTable = GetPlanSchedule(SqlData)
                                If dtDataPlan.Rows.Count > 0 Then
                                    strPlanQty = dtRowsFormat.FormatString(dtDataPlan, "PlanQty")
                                    Cancled = dtRowsFormat.FormatString(dtDataPlan, "Cancled")
                                End If
                                If strPlanQty <> String.Empty Then
                                    Dim dblNumberCells As Double = CDbl(strPlanQty)
                                    Dim PlanQtyDecimal = String.Format("{0:n3}", dblNumberCells)
                                    If Cancled <> "0" Then
                                        row.Cells(i).Text = PlanQtyDecimal
                                        row.Cells(i).ForeColor = System.Drawing.Color.Maroon
                                        row.Cells(i).BackColor = System.Drawing.Color.Gray
                                    Else
                                        row.Cells(i).Text = PlanQtyDecimal
                                    End If
                                Else
                                    row.Cells(i).Text = String.Empty
                                End If
                            End If
                        End If
                    Next
                    i += 1
                End While
            End If
            gvShow.DataSource = ""
            gvShow.DataBind()
            ucCountRow.RowCount = ControlForm.rowGridview(gvCheck)
            ' ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "gridviewScroll();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub
    Sub reset()
        UsingMO_Type.getObject.ClearSelection()
        tbMO.Text = ""
        lbMO.Text = ""
        lbBatch.Text = ""
        lbCustName.Text = ""
        lbDesc.Text = ""
        lbQty.Text = ""
        lbSpec.Text = ""
        gvShow.DataSource = ""
        gvShow.DataBind()
        gvCheck.DataSource = ""
        gvCheck.DataBind()
        btSave.Visible = False
        ' btExport.Visible = False
    End Sub

    Protected Sub gvCheck_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCheck.RowDataBound
        Dim StartDate = ConvertUtility.StToDate(lblStartDate.Text).Date
        Dim EndDate = ConvertUtility.StToDate(lblEndDate.Text).Date
        If e.Row.RowType = DataControlRowType.Header Then
            Dim colStart As Integer = 13
            Dim lcolumncount As Integer
            lcolumncount = (e.Row.Cells.Count) - colStart
            e.Row.Cells(0).Text = "Line_No"
            e.Row.Cells(1).Text = "Op_ID"
            e.Row.Cells(2).Text = "Op_Description"
            e.Row.Cells(3).Text = "Workcenter_:_Name"
            e.Row.Cells(4).Text = "PlanStartDate"
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Text = "PlanCompleteDate"
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Text = "Std.Output"
            e.Row.Cells(7).Text = "WIP"
            e.Row.Cells(8).Text = "GoodTrs.In"
            e.Row.Cells(9).Text = "GoodTrs.Out"
            e.Row.Cells(10).Text = "ReworkTrsIn"
            e.Row.Cells(11).Text = "ReworkTrsOut"
            e.Row.Cells(12).Text = "Direct_Scarp"
            'e.Row.Cells(13).Text = "Plan.CompleteDate "
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            If e.Row.Cells(4).Text <> String.Empty Then
                e.Row.Cells(4).Text = ConvertUtility.StringDateTimeToDate(e.Row.Cells(4).Text).ToString("yyyy/MM/dd")
            End If
            If e.Row.Cells(5).Text <> String.Empty Then
                e.Row.Cells(5).Text = ConvertUtility.StringDateTimeToDate(e.Row.Cells(5).Text).ToString("yyyy/MM/dd")
            End If
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            If e.Row.Cells(3).Text <> String.Empty Then
                Dim WCname As DataTable = ECAA.GetFindWorkcenterDetail_Table(e.Row.Cells(3).Text)
                e.Row.Cells(3).Text = dtRowsFormat.FormatSumString(WCname, ECAA.WorkcenterID, ECAA.Workcenter)
            End If
            'e.Row.Cells(2).Width = 350
            'e.Row.Cells(3).Width = 250
            Dim i As Integer = 6
            While i < e.Row.Cells.Count
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                If e.Row.Cells(i).Text <> "&nbsp;" Then
                    Dim dblNumberCells As Double = CDbl(e.Row.Cells(i).Text)
                    e.Row.Cells(i).Text = String.Format("{0:n3}", dblNumberCells)
                End If
                i += 1
            End While
        End If
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.Cells(0).Text = "Not Data Found"
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.Font.Bold = True
        End If

        'Dim rowIndex As Integer = gvCheck.Rows.Count - 2
        'While rowIndex >= 0
        '    Dim gvRow As GridViewRow = gvCheck.Rows(rowIndex)
        '    Dim gvPreviousRow As GridViewRow = gvCheck.Rows(rowIndex + 1)
        '    Dim cellCount As Integer = 0
        '    Dim CellSpan As Integer = (e.Row.Cells.Count) - (13 - 7)
        '    While cellCount <= (gvRow.Cells.Count - CellSpan)
        '        If gvRow.Cells(cellCount).Text = gvPreviousRow.Cells(cellCount).Text Then
        '            If gvPreviousRow.Cells(cellCount).RowSpan < 2 Then
        '                gvRow.Cells(cellCount).RowSpan = 2
        '            Else
        '                gvRow.Cells(cellCount).RowSpan = gvPreviousRow.Cells(cellCount).RowSpan + 1
        '            End If
        '            gvPreviousRow.Cells(cellCount).Visible = False
        '        End If
        '        cellCount += 1
        '    End While
        '    rowIndex -= 1
        'End While
    End Sub
    Private Sub gvCheck_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvCheck.RowCreated
        Dim StartDate As Date = ConvertUtility.StToDate(lblStartDate.Text).Date
        Dim EndDate As Date = ConvertUtility.StToDate(lblEndDate.Text).Date
        Dim colStart As Integer = 13
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell2 As New TableCell()
            Dim FindeMO As String = lbMO.Text
            Dim StrMO_No As String = "<span style='color:Blue;'>" & FindeMO & "</span>"
            HeaderCell2.Text = vbTab & vbTab & "Schecdule  MO-No. " & StrMO_No
            HeaderCell2.Font.Bold = True
            HeaderCell2.ForeColor = System.Drawing.Color.Black
            HeaderCell2.BackColor = ColorTranslator.FromHtml("#93979A")
            HeaderCell2.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell2.ColumnSpan = colStart - 6
            HeaderCell2.HorizontalAlign = HorizontalAlign.Left
            HeaderRow.Cells.Add(HeaderCell2)
            gvCheck.Controls(0).Controls.AddAt(0, HeaderRow)
            ' Dim HeaderRow1 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim HeaderCell As New TableCell()
            Dim StrStartDate As String = "<span style='color:Blue;'>" & StartDate.ToString("yyyy/MM/dd") & "</span>"
            Dim StrEndDate As String = "<span style='color:Blue;'>" & EndDate.ToString("yyyy/MM/dd") & "</span>"
            HeaderCell.Text = vbTab & vbTab & "MO-PlanStarDate : " & StrStartDate & " - " & "MO-PlanCompleteDate : " & StrEndDate
            HeaderCell.ColumnSpan = (e.Row.Cells.Count)
            HeaderCell.Font.Bold = True
            HeaderCell.ForeColor = System.Drawing.Color.Black
            HeaderCell.BackColor = ColorTranslator.FromHtml("#93979A")
            HeaderCell.BorderColor = ColorTranslator.FromHtml("#5E73DD")
            HeaderCell.HorizontalAlign = HorizontalAlign.Left
            HeaderRow.Cells.Add(HeaderCell)
            gvCheck.Controls(0).Controls.AddAt(1, HeaderRow)

        End If
        Dim gvr As GridViewRow = e.Row
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim gvRowCount As String = "<span style='color:Blue;'>" & gvCheck.Rows.Count.ToString() & "</span>"
            Dim stTotla As String = "Process Total: " + gvRowCount + ""
            Dim iFoot As Integer = CInt(gvCheck.Columns.Count)
            'e.Row.Cells(0).Text = stTotla.ToString()
            'e.Row.Cells(0).ColumnSpan = CInt(e.Row.Cells.Count)
            'e.Row.Cells.RemoveAt(1)
            Dim index As Integer = gvCheck.Rows.Count
            Dim row As New GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal)
            Dim cell As New TableCell()
            ' Dim txt As New TextBox()
            'txt.Width = 350
            ' cell.Controls.Add(txt)
            Dim cell2 As New TableCell()
            'row.Cells.Add(cell)
            cell2.Text = stTotla
            cell2.ColumnSpan = colStart
            cell2.BorderColor = System.Drawing.Color.Blue
            cell2.BackColor = ColorTranslator.FromHtml("#E3E9EA")
            row.Cells.Add(cell2)
            gvCheck.Controls(0).Controls.Add(row)
            Dim cell3 As New TableCell()
            Dim DayCount As Integer = e.Row.Cells.Count - 13
            Dim StrDayCount As String = "<span style='color:Blue;'>" & DayCount & "</span>"
            cell3.Text = "Plan Count : " & StrDayCount & " days"
            Dim iDay As Integer = (e.Row.Cells.Count - 13)
            cell3.ColumnSpan = iDay
            cell3.BorderColor = System.Drawing.Color.Blue
            cell3.BackColor = ColorTranslator.FromHtml("#E3E9EA")
            row.Cells.Add(cell3)
            gvCheck.Controls(0).Controls.Add(row)
        End If
    End Sub

    Private Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        getdataShowProgressMO()
        If gvCheck.Rows.Count <= 0 Then
            MessageAlert.Show(Me, "Not data found")
        Else
            '# Style1 Ok Excel
            ExportsUtility.ExportGridviewToMsExcel("PlanByMO", gvCheck)
            'Have NameSpace  >>> Export.ExportsUtility.ExportGridviewToMsExcel("PlanByMO", gvCheck)

            '# Style2 ok Excel
            'Export.ExportsUtility.Export("FileName", GridviewID)
            'Have NameSpace  >>> Export.ExportsUtility.Export("FileName", GridviewID)

            '# Style3 Ok Excel
            'Export.ExportsUtility.Export("FileName", GridviewID)
            ' Have NameSpace  >>> Export.ExportsUtility.ExportToExcel("FileName", GridviewID)

            '# Style4 Ok  CSV
            'Export.ExportsUtility.Export("FileName", GridviewID)
            ' Have NameSpace  >>> Export.ExportsUtility.ExportCVS("FileName", GridviewID)


        End If
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        With gvShow
            'check befor save
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim ss As Integer = 1
                    Dim planQty As Decimal = 0
                    For j As Integer = colStart To gvShow.Columns.Count - 1
                        Dim tbDate As TextBox = gvShow.Rows(i).FindControl("tbDate" & ss.ToString)
                        planQty += Conn_SQL.checkNumberic(tbDate)
                        ss += 1
                    Next
                    Dim moQty As Decimal = Conn_SQL.checkNumberic(lbQty.Text.Trim)
                    If planQty > moQty Then
                        show_message.ShowMessage(Page, "Plan qty is over mo qty on seq=" & Trim(.Cells(1).Text), UpdatePanel1)
                        Dim tbDate As TextBox = gvShow.Rows(i).FindControl("tbDate1")
                        tbDate.Focus()
                        Exit Sub
                    End If
                End With
            Next
            'go to save
            Dim SSxx = lbMO.Text.Trim.Split("JP")
            Dim aa() As String = SSxx(0).Split("-")
            Dim whrHash As Hashtable,
        fldHash As Hashtable,
        moType As String = aa(0),
        moNo As String = aa(1)
            For i As Integer = 0 To .Rows.Count - 1
                With .Rows(i)
                    Dim wc As String = .Cells(0).Text.Trim,
        moSeq As String = .Cells(1).Text.Trim,
        op As String = .Cells(2).Text.Trim

                    Dim strSQL As String = ""
                    Dim ss As Integer = 1
                    For j As Integer = colStart To gvShow.Columns.Count - 1
                        Dim planDate As String = DateTime.Now.AddDays(ss - 1).ToString("yyyyMMdd"),
        tbDate As TextBox = gvShow.Rows(i).FindControl("tbDate" & ss.ToString),
        tbMC As TextBox = gvShow.Rows(i).FindControl("tbMC")
                        Dim planQty As Decimal = Conn_SQL.checkNumberic(tbDate),
                        mc As String = tbMC.Text.Trim
                        whrHash = New Hashtable
                        fldHash = New Hashtable
                        whrHash.Add("PlanDate", planDate)
                        whrHash.Add("TA006", wc)
                        fldHash.Add("PlanQty", planQty)
                        fldHash.Add("Mch", mc)
                        fldHash.Add("PlanSeqSet", "999")
                        fldHash.Add("CreateBy", Session("UserName"))
                        If planQty > 0 Then
                            whrHash.Add("PlanSeq", chkRecPlan(moType, moNo, moSeq, wc, planDate))
                            fldHash.Add("TA001", moType)
                            fldHash.Add("TA002", moNo)
                            fldHash.Add("TA003", moSeq)
                            fldHash.Add("TA004", op)
                            fldHash.Add("Cancled", "0")
                            strSQL &= Conn_SQL.GetSQL(table, fldHash, whrHash)
                        Else
                            Dim SQL As String = "select count(*),sum(PlanQty) from PlanSchedule where Cancled='0' and PlanDate='" & planDate & "' and TA006='" & wc & "' and TA001='" & moType & "' and TA002='" & moNo & "' and TA003='" & moSeq & "' "
                            Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                            If dt.Rows(0).Item(0) > 0 Then
                                whrHash.Add("TA001", moType)
                                whrHash.Add("TA002", moNo)
                                whrHash.Add("TA003", moSeq)
                                fldHash.Add("Cancled", "1")
                                strSQL &= Conn_SQL.GetSQL(table, fldHash, whrHash, "U")
                            End If
                        End If
                        ss += 1
                    Next
                    If strSQL <> "" Then
                        Conn_SQL.Exec_Sql(strSQL, Conn_SQL.MIS_ConnectionString)
                    End If
                End With
            Next

        End With
        show_message.ShowMessage(Page, "Save Plan Complete!!", UpdatePanel1)
        btShow_Click(sender, e)
    End Sub
    Function chkRecPlan(mtype As String, mo As String, seq As String, wc As String, pDate As String) As Integer
        Dim SQL As String = "select top 1 PlanSeq from PlanSchedule where Cancled='0' and PlanDate='" & pDate & "' and TA006='" & wc & "' and TA001='" & mtype & "' and TA002='" & mo & "' and TA003='" & seq & "' "
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Dim seqPlan As Integer = 0
        If dt.Rows.Count = 0 Then
            SQL = "select top 1 PlanSeq from PlanSchedule where PlanDate='" & pDate & "' and TA006='" & wc & "' order by  PlanSeq desc "
            dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
            If dt.Rows.Count > 0 Then
                seqPlan = CInt(dt.Rows(0).Item(0)) + 1
            Else
                seqPlan = 1
            End If
        Else
            seqPlan = dt.Rows(0).Item(0)
        End If
        Return seq
    End Function


End Class