Imports System.Data.OracleClient

Public Class ProductionRecordReportPop
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            lblpMO_Docno.Text = Request.QueryString("MO_DocNo").ToString.Trim
            Call MO_SaleDetail()
            Call MO_Process()
            ucCountRowOper.RowCount = ControlForm.rowGridview(gvMODetail)
            ucCountRowMO.RowCount = ControlForm.rowGridview(gvMO_Process)
            GridviewUtility.GridStyleTemplate_Std(gvMODetail)
            GridviewUtility.GridStyleTemplate_Std(gvMO_Process)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollSMO", "gridviewScrollMO();", True)
        End If
    End Sub
    Private Sub gvMODetail_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMODetail.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblItemName As Label = CType((e.Row.FindControl("lblItemName")), Label)
            Dim lblSpecifiation As Label = CType((e.Row.FindControl("lblSpecifiation")), Label)
            Dim lblComplete_Qty As Label = CType((e.Row.FindControl("lblComplete_Qty")), Label)
            Dim lblSaleOrder_No As Label = CType((e.Row.FindControl("lblSaleOrder_No")), Label)
            Dim lblCustomer As Label = CType((e.Row.FindControl("lblCustomer")), Label)
            If e.Row.Cells(4).Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(e.Row.Cells(4).Text)
                If dtItem.Rows.Count > 0 Then
                    lblItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblSpecifiation.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
            End If
            Dim dtCompleteQty As DataTable = SFCA.GetDataMoProcessHeader(e.Row.Cells(2).Text)
            If dtCompleteQty.Rows.Count > 0 Then
                lblComplete_Qty.Text = dtRowsFormat.FormatDecimal(dtCompleteQty, SFCA.CompletedQty)
                If lblComplete_Qty.Text <> String.Empty Then
                    Dim dblNumberComplete_Qty As Double = CDbl(lblComplete_Qty.Text)
                    lblComplete_Qty.Text = String.Format("{0:n3}", dblNumberComplete_Qty)
                End If
            End If
            Dim dtSaleItem As DataTable = XMDC.GetDataMoProcessHeader(e.Row.Cells(4).Text)
            If dtSaleItem.Rows.Count > 0 Then
                lblSaleOrder_No.Text = dtRowsFormat.FormatString(dtSaleItem, XMDC.SaleOrderNo)
            End If
            Dim dtSaleH As DataTable = XMDA.GetCustomerItemSale(lblSaleOrder_No.Text)
            If dtSaleH.Rows.Count > 0 Then
                lblCustomer.Text = dtRowsFormat.FormatString(dtSaleH, XMDA.CustomerId)
                Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(lblCustomer.Text)
                If dtCustName.Rows.Count > 0 Then
                    lblCustomer.Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerID) & " : " & dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerFullName)
                End If
            End If
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
        End If
    End Sub
    Private Sub MO_SaleDetail()
        Dim dt As DataTable = SFAA.GetMO_HeaderDeatil(lblpMO_Docno.Text)
        If dt.Rows.Count > 0 Then
            gvMODetail.DataSource = dt
            gvMODetail.DataBind()
        Else
            gvMODetail.DataSource = New List(Of String)
            gvMODetail.DataBind()
        End If
    End Sub
    Private Sub MO_Process()
        Dim dtMOpreocess As DataTable = GetDataMOProcessByMODocNo(lblpMO_Docno.Text)
        If dtMOpreocess.Rows.Count > 0 Then
            gvMO_Process.DataSource = dtMOpreocess
            gvMO_Process.DataBind()
        Else
            gvMO_Process.DataSource = New List(Of String)
            gvMO_Process.DataBind()
        End If
    End Sub
    Private Shared strProcessByMO As String = "SELECT " & SFCB.WONo & "," & SFCB.LineNo & "," & SFCB.OperationID & "," & OOCQL.Operation & ", " &
" " & SFCB.WorkStation & "," & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & "," & SFCB.StardardOutput & ", " &
" " & SFCB.WIP & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & ", " &
" " & SFCB.ReworkTrsIn & " ," & SFCB.DirectScarp & " " &
" FROM  " & SFCB.tblMOprocessItem_SFCB & "  " &
" LEFT JOIN  " & SFCA.tblMO_Detail & " On  " & SFCA.tblMO_Detail & "." & SFCA.DocNo & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " " &
" AND " & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & " " &
" LEFT JOIN  " & OOCQL.tblOperation & " On " & OOCQL.tblOperation & "." & OOCQL.OperationID & "=" & SFCB.tblMOprocessItem_SFCB & "." & SFCB.OperationID & "  " &
" where " & SFCB.WONo & " =@MoNO " &
" And " & OOCQL.tblOperation & "." & OOCQL.Language & " ='en_US' And " & OOCQL.IssueSite & "='221' " &
" And " & OOCQL.tblOperation & "." & OOCQL.ent & "='3' Order by " & SFCA.RunCardNo & "," & SFCB.LineNo & " ASC "
    Public Shared Function GetDataMOProcessByMODocNo(ByVal strWH_MoNo As String) As DataTable
        Dim strSQL = strProcessByMO.Replace("@MoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            'GetPageError.GetClassT100(ASF, "SFCB", "GetDataMOProcess_By_MO_DocNo", "strSQL = strSqlRowProcessBy_MO_DocNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub gvMO_Process_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvMO_Process.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(6).Text <> String.Empty Then
                Dim WCname As DataTable = ECAA.GetFindWorkcenterDetail_Table(e.Row.Cells(6).Text)
                e.Row.Cells(6).Text = dtRowsFormat.FormatSumString(WCname, ECAA.WorkcenterID, ECAA.Workcenter)
            End If
            If e.Row.Cells(9).Text <> String.Empty Then
                Dim WIP As Integer = CInt(e.Row.Cells(9).Text)
                If WIP < 0 Then
                    e.Row.Cells(9).ForeColor = System.Drawing.Color.Maroon
                End If
            End If
            Dim RecardNo As String = e.Row.Cells(1).Text
            If RecardNo <> String.Empty Then
                If RecardNo <> "0" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFE0")
                End If
            End If
            Dim RecardDetail As String = e.Row.Cells(2).Text
                If RecardDetail <> String.Empty Then
                    If RecardDetail = "1" Then
                        e.Row.Cells(2).Text = "1 : GENERAL"
                    ElseIf RecardDetail = "2" Then
                        e.Row.Cells(2).Text = "2 : REWORK"
                    End If

                End If
                e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Center
            End If
    End Sub
End Class