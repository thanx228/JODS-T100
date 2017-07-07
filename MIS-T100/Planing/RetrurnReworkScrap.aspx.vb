Imports System.Data.OracleClient
Imports System.Drawing

Public Class RetrurnReworkScrap
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            btExportExcel.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub
    Private Sub DataTransferReturnFroReworkBind()
        Dim where As String = String.Empty
        Dim wWC As String = String.Empty
        Dim wSaleOrderNo As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wCust As String = String.Empty
        Dim wItemNo As String = String.Empty
        Dim wPlanStartDate As String = String.Empty
        If UsingWorkstation.getObject.Text <> "0" Then
            Dim WC As String = UsingWorkstation.getObject.Text
            where = " and " & SFCB.WorkStation & " = '" & [String].Join("','", WC) & "'"
        End If
        If UsingMO_Type.getObject.Text <> "0" Then
            Dim MO_DocNo As String = UsingMO_Type.getObject.Text
            wWC = " and substr(" & SFIA.WONo & ",3,4) = '" & [String].Join("','", MO_DocNo) & "'"
        End If
        If UsingDocTypeSale.getObject.Text <> "0" Then
            Dim SO_No As String = UsingDocTypeSale.getObject.Text
            wSaleOrderNo = " and substr(" & XMDA.SaleOrderNo & ",3,4) = '" & [String].Join("','", SO_No) & "'"
        End If
        If tbSpec.Text <> String.Empty Then
            wSpec = " and " & IMAAL.Specifaction & " Like '" & [String].Join("','", tbSpec.Text) & "%'"
        End If
        If tbCust.Text <> String.Empty Then
            wCust = " and " & XMDA.CustomerId & " ='" & [String].Join("','", tbCust.Text) & "'"
        End If
        If tbCode.Text <> String.Empty Then
            wItemNo = " and " & SFAA.ProductItem & " Like '" & [String].Join("','", tbCode.Text) & "%'"
        End If
        If txtFormDate.Text <> String.Empty And txtToDate.Text <> String.Empty Then
            Dim PlanStartDate As String = SFCB.PlanStartDate & " BETWEEN TO_DATE('" & [String].Join("','", txtFormDate.Text) & "','yyyy/mm/dd') AND TO_DATE('" & [String].Join("','", txtToDate.Text) & "','yyyy/mm/dd')"
            wPlanStartDate = " and " & PlanStartDate
        End If
        where = wWC & wSaleOrderNo & wSaleOrderNo & wSpec & wCust & wItemNo & wPlanStartDate

        'lblsql.Text = GetRetrunForReworkT100(where)
        Dim dtRework As DataTable = GetRetrunForReworkT100(where)
        If dtRework.Rows.Count > 0 Then
            gvShow.DataSource = dtRework
            gvShow.DataBind()
            CountRow1.RowCount = dtRework.Rows.Count.ToString()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            btExportExcel.Visible = True
        Else
            MessageAlert.Show(Me, "Not Data ")
        End If
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Call DataTransferReturnFroReworkBind()
    End Sub
    '############# function DataReturn for rework T100 ############################################################
    Private Shared SqlDataRetrunForReworkT100 As String = "select " & SFIA.DocNo & "," & SFIA.WONo & "," & SFIA.DocumentDate & "," & SFIB.ItemSequence & ", " &
    "  " & SFIB.OperationID & "," & SFAA.ProductItem & "," & SFIA.TransferinRuncard & "," & SFIA.TransferOutOpOrder & "," & SFIA.TransferOutOpSerailNo & ", " &
    "  " & SFIA.ReworkTransferOutQty & ", " & SFIA.Status & "," & SFIA.Applicant & "," & SFCB.WorkStation & "," & SFCA.RunCardDetail & "," & SFCB.LineNo & "," & SFCB.WorkStation & ", " &
    " " & XMDA.SaleOrderNo & "," & XMDA.CustomerId & "," & XMDC.ItemSequence & "," & IMAAL.Specifaction & "," & SFAA.ProductionQty & "," & SFIB.PreviousOperation & ", " &
    " " & SFIB.OperationSeq & "," & SFIB.OperationID & "," & SFIB.Workstation & "," & SFIA.TransferOutOpSerailNo & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & " " &
" from " & SFIA.tblTransferReworkHead & "  " &
        " LEFT OUTER JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & "=" & SFIA.tblTransferReworkHead & "." & SFIA.WONo & "  " &
        " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & "=" & SFIA.tblTransferReworkHead & "." & SFIA.RunCard & "  " &
        " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.OperationSeq & "=" & SFIA.tblTransferReworkHead & "." & SFIA.TransferOutOpOrder & "  " &
        " AND " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.OperationID & "=" & SFIA.tblTransferReworkHead & "." & SFIA.TransferOutOpSerailNo & "  " &
        " LEFT OUTER JOIN  " & SFAA.tblMO & " On " & SFAA.tblMO & "." & SFAA.DocNo & " = " & SFIA.tblTransferReworkHead & "." & SFIA.WONo & " " &
        " LEFT OUTER JOIN  " & SFIB.tblTransferReworkBody & " On " & SFIB.tblTransferReworkBody & "." & SFIB.DocNo & "=" & SFIA.tblTransferReworkHead & "." & SFIA.DocNo & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & "=" & SFAA.tblMO & "." & SFAA.ProductItem & "  " &
        " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " " &
        " AND " & SFCA.RunCardNo & "=" & SFCB.RunCard & "  " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.tblSaleItem & "." & XMDC.Item & "=" & SFAA.tblMO & "." & SFAA.ProductItem & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.tblSaleItem & "." & XMDC.SaleOrderNo & "=" & XMDA.tblSaleHead & "." & XMDA.SaleOrderNo & " " &
" where " & SFIA.wStandard & " @pWhereCustomUsing " &
" Group by " & SFIA.DocNo & "," & SFIA.WONo & "," & SFIA.DocumentDate & "," & SFIB.ItemSequence & ", " &
    "  " & SFIB.OperationID & "," & SFAA.ProductItem & "," & SFIA.TransferinRuncard & "," & SFIA.TransferOutOpOrder & "," & SFIA.TransferOutOpSerailNo & ", " &
    "  " & SFIA.ReworkTransferOutQty & ", " & SFIA.Status & "," & SFIA.Applicant & "," & SFCB.WorkStation & "," & SFCA.RunCardDetail & "," & SFCB.LineNo & "," & SFCB.WorkStation & ", " &
    " " & XMDA.SaleOrderNo & "," & XMDA.CustomerId & "," & XMDC.ItemSequence & "," & IMAAL.Specifaction & "," & SFAA.ProductionQty & "," & SFIB.PreviousOperation & ", " &
    " " & SFIB.OperationSeq & "," & SFIB.OperationID & "," & SFIB.Workstation & "," & SFIA.TransferOutOpSerailNo & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & " " &
" Order By " & SFIA.DocNo & "," & SFIA.WONo & "," & SFIA.TransferinRuncard & "," & XMDA.SaleOrderNo & "," & XMDC.ItemSequence & "  ASC "
    Public Shared Function GetRetrunForReworkT100(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataRetrunForReworkT100
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
            GetPageError.GetPage(FilePage, "GetRetrunForReworkT100", "Sql = SqlDataRetrunForReworkT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    End Function

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        Call DataTransferReturnFroReworkBind()
        ExportsUtility.ExportGridviewToMsExcel("ReturnForRework" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call DataTransferReturnFroReworkBind()
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim Customer As String = e.Row.Cells(1).Text
            If Customer <> String.Empty Then
                Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(Customer)
                If dtCustName.Rows.Count > 0 Then
                    e.Row.Cells(1).Text = dtRowsFormat.FormatSumString(dtCustName, PMAAL.CustomerID, PMAAL.CustomerFullName)
                End If
            End If
            Dim RecardDetail As String = e.Row.Cells(8).Text
            If RecardDetail <> String.Empty Then
                If RecardDetail = "1" Then
                    e.Row.Cells(8).Text = "1 : GENERAL"
                ElseIf RecardDetail = "2" Then
                    e.Row.Cells(8).Text = "2 : REWORK"
                End If
            End If
            Dim Formworkcenter As String = e.Row.Cells(16).Text
            If Formworkcenter <> String.Empty Then
                Dim dtWc As DataTable = ECAA.GetFindWorkcenterDetail_Table(Formworkcenter)
                If dtWc.Rows.Count > 0 Then
                    e.Row.Cells(16).Text = dtRowsFormat.FormatSumString(dtWc, ECAA.WorkcenterID, ECAA.Workcenter)
                End If
            End If

            Dim FrmOP As String = e.Row.Cells(14).Text
            If FrmOP <> String.Empty Then
                Dim dtOp As DataTable = OOCQL.GetDataOperation(FrmOP)
                If dtOp.Rows.Count > 0 Then
                    e.Row.Cells(15).Text = dtRowsFormat.FormatString(dtOp, OOCQL.Operation)
                End If
            End If

            Dim ReturnToOP As String = e.Row.Cells(20).Text
            If ReturnToOP <> String.Empty Then
                Dim dtReturnOp As DataTable = OOCQL.GetDataOperation(ReturnToOP)
                If dtReturnOp.Rows.Count > 0 Then
                    e.Row.Cells(21).Text = dtRowsFormat.FormatString(dtReturnOp, OOCQL.Operation)
                End If
                Dim ToWorkcenter As String = e.Row.Cells(22).Text
                If ToWorkcenter <> String.Empty Then
                    Dim dtToWc As DataTable = ECAA.GetFindWorkcenterDetail_Table(ToWorkcenter)
                    If dtToWc.Rows.Count > 0 Then
                        e.Row.Cells(22).Text = dtRowsFormat.FormatSumString(dtToWc, ECAA.WorkcenterID, ECAA.Workcenter)
                    End If
                End If
                Dim PrevOP As String = e.Row.Cells(23).Text
                If PrevOP <> String.Empty Then
                    Dim dtPrevOp As DataTable = OOCQL.GetDataOperation(PrevOP)
                    If dtPrevOp.Rows.Count > 0 Then
                        e.Row.Cells(24).Text = dtRowsFormat.FormatString(dtPrevOp, OOCQL.Operation)
                    Else
                        e.Row.Cells(24).Text = "First Process"
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gvShow_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowCreated
        'If e.Row.RowType = DataControlRowType.Header Then
        '    Dim colCount As Integer = e.Row.Cells.Count
        '    Dim HeaderRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
        '    Dim Header0 As New TableCell()
        '    ' Dim FindeMO As String = lbMO.Text
        '    'Dim StrMO_No As String = "<span style='color:Blue;'>" & FindeMO & "</span>"
        '    Header0.Text = ""
        '    Header0.Font.Bold = True
        '    Header0.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
        '    Header0.BackColor = ColorTranslator.FromHtml("#99CCCC")
        '    Header0.BorderColor = ColorTranslator.FromHtml("#5E73DD")
        '    Header0.ColumnSpan = 1
        '    Header0.HorizontalAlign = HorizontalAlign.Left
        '    HeaderRow.Cells.Add(Header0)
        '    gvShow.Controls(0).Controls.AddAt(0, HeaderRow)
        '    Dim HeaderSaleOrder As New TableCell()
        '    ' Dim FindeMO As String = lbMO.Text
        '    Dim StrSOo As String = "<span style='color:#3F3E3E;'>SaleOrder & SaleOrder Forecast (axmt500 )</span>"
        '    HeaderSaleOrder.Text = vbTab & vbTab & StrSOo
        '    HeaderSaleOrder.Font.Bold = True
        '    HeaderSaleOrder.ForeColor = ColorTranslator.FromHtml("#3F3E3E")
        '    HeaderSaleOrder.BackColor = ColorTranslator.FromHtml("#99CCCC")
        '    HeaderSaleOrder.BorderColor = ColorTranslator.FromHtml("#5E73DD")
        '    HeaderSaleOrder.ColumnSpan = 3
        '    HeaderSaleOrder.HorizontalAlign = HorizontalAlign.Left
        '    HeaderRow.Cells.Add(HeaderSaleOrder)
        '    gvShow.Controls(0).Controls.AddAt(0, HeaderRow)
        'End If
    End Sub
End Class