Imports System.Data.OracleClient

Public Class planIssueMatPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            Dim pBOM_Item As String = Request.QueryString("BOM_Item").ToString.Trim
            lblpBOM_Item.Text = pBOM_Item
            lbSpec.Text = Server.UrlDecode(Request.QueryString("ItemSpec").ToString.Trim)
            lbItemName.Text = Request.QueryString("ItemName").ToString.Trim
            lbStock.Text = Request.QueryString("stock").ToString.Trim
            If lbStock.Text <> String.Empty Then
                Dim dblNumberStock As Double = CDbl(lbStock.Text)
                lbStock.Text = String.Format("{0:n3}", dblNumberStock)
            End If
            lbNotDel.Text = Request.QueryString("issueQty").ToString.Trim
            If lbNotDel.Text <> String.Empty Then
                Dim dblNumberNotDel As Double = CDbl(lbNotDel.Text)
                lbNotDel.Text = String.Format("{0:n3}", dblNumberNotDel)
            End If
            Call ShowMO_WhereBy_BOMitem()
            Call ShowPO_WhereBy_BOMitem()
            Call ShowPR_Request_WhereBy_BOMitem()
            Call PR_Receipt_WkereBy_ItemNo()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub
    Private Sub ShowMO_WhereBy_BOMitem()
        Dim dt As DataTable = GetManufactureOrder(lblpBOM_Item.Text)
        If dt.Rows.Count > 0 Then
            gvIssue.DataSource = dt
            gvIssue.DataBind()
            lbCountIssue.Text = gvIssue.Rows.Count.ToString
            GridviewUtility.GrigOnmouseHandleAuto(gvIssue)
        End If
    End Sub
    Private Shared strWH_BOM_Item As String = "Select " & SFBA.MODocNo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " &
        " " & SFBA.MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & SFBA.OperationNo & "," & SFBA.OperationSeq & "," & SFBA.BOMitem & "," & SFBA.IssueItem & ", " &
        " " & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & " " &
        " FROM " & SFBA.tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.BOMitem & " = " & IMAAL.tblProductionDetail & "." & IMAAL.ProductItem & " " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFBA.tblManufactureOrder_Body & "." & SFBA.MODocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " = " & SFAA.tblMO & "." & SFAA.DocNo & " " &
        " LEFT JOIN  " & SFCB.tblMOprocessItem_SFCB & " On " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.WONo & " = " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " " &
        " AND  " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & " = " & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & " " &
        " where " & SFBA.wStandard & " And " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' AND " & SFBA.BOMitem & " =@pBOM_Item " &
        " Group by  " & SFBA.MODocNo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " &
        " " & SFBA.MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & SFBA.OperationNo & "," & SFBA.OperationSeq & "," & SFBA.BOMitem & "," & SFBA.IssueItem & ", " &
        " " & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & " " &
        " Order By " & SFBA.MODocNo & "," & SFBA.ItemSequence & "," & SFCA.RunCardNo & " "
    Private Shared Function GetManufactureOrder(strBOM_Item As String) As DataTable
        Dim Sql As String = strWH_BOM_Item.Replace("@pBOM_Item", "'" & strBOM_Item & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            'GetPageError.GetClassT100(ASF, "SFBA", "GetManufactureOrder_BOM_Item_Body", "Sql = strWH_BOM_Item", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub ShowPO_WhereBy_BOMitem()
        Dim dt As DataTable = PMDN.GetPO_Body_By_ItemNo(lblpBOM_Item.Text)
        If dt.Rows.Count > 0 Then
            gvPO.DataSource = dt
            gvPO.DataBind()
            lbCountPO.Text = gvPO.Rows.Count.ToString
            GridviewUtility.GrigOnmouseHandleCustomer(gvPO, "#ADA9A9")
        End If
    End Sub
    Private Sub ShowPR_Request_WhereBy_BOMitem()
        Dim dt As DataTable = PMDB.GetPR_Request_ByItemNo_Body(lblpBOM_Item.Text)
        If dt.Rows.Count > 0 Then
            gvPR.DataSource = dt
            gvPR.DataBind()
            lbCountPR.Text = gvPR.Rows.Count.ToString
        Else
            gvPR.DataSource = New List(Of String)
            gvPR.DataBind()
        End If
        lbCountPR.Text = gvPR.Rows.Count.ToString()
    End Sub
    Private Sub PR_Receipt_WkereBy_ItemNo()
        Dim dt As DataTable = PMDT.GetPR_Receipt_By_ItemNo_BodyReceiptDetail(lblpBOM_Item.Text)
        If dt.Rows.Count > 0 Then
            gvPR_Receipt.DataSource = dt
            gvPR_Receipt.DataBind()
        Else
            gvPR_Receipt.DataSource = New List(Of String)
            gvPR_Receipt.DataBind()
        End If
        lblPR_Receipt.Text = gvPR_Receipt.Rows.Count.ToString()
    End Sub
    'Private Sub gvShow_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
    '    With e.Row
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplDetail"), HyperLink)
    '            If Not IsNothing(hplDetail) Then
    '                If Not IsDBNull(.DataItem("Z")) Then
    '                    hplDetail.NavigateUrl = "ProductionRecordReportPop.aspx?height=150&width=350&docno=" & .DataItem("Z").ToString.Trim & "&mo=" & .DataItem("D").ToString.Trim
    '                    hplDetail.Attributes.Add("title", .DataItem("D"))
    '                    '.Attributes.Add("onmouseover", "MouseEvents(this, event)")
    '                    '.Attributes.Add("onmouseout", "MouseEvents(this, event)")
    '                End If
    '            End If
    '        End If
    '    End With
    'End Sub


    'Private Sub gvMO_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMO.RowDataBound
    '    With e.Row
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplLinkMO"), HyperLink)
    '            If Not IsNothing(hplDetail) Then
    '                If Not IsDBNull(.DataItem("LotNo")) Then
    '                    hplDetail.NavigateUrl = "../Production/ProductionRecordReportPop.aspx?height=150&width=350&docno=&mo=" & .DataItem("LotNo").ToString.Trim
    '                    hplDetail.Attributes.Add("title", .DataItem("LotNo"))
    '                End If
    '            End If
    '        End If
    '    End With
    'End Sub

    Private Sub gvIssue_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIssue.RowDataBound
        With e.Row
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.Cells(2).Text <> String.Empty Then
                    If e.Row.Cells(2).Text = "C" Then
                        e.Row.ForeColor = System.Drawing.Color.Green
                    ElseIf e.Row.Cells(2).Text = "M" Then
                        e.Row.ForeColor = System.Drawing.Color.YellowGreen
                    ElseIf e.Row.Cells(2).Text = "N" Then
                        e.Row.Cells(2).ForeColor = System.Drawing.Color.Maroon
                    ElseIf e.Row.Cells(2).Text = "F" Then
                        e.Row.Cells(2).ForeColor = System.Drawing.ColorTranslator.FromHtml("#008ae6")
                    ElseIf e.Row.Cells(2).Text = "Y" Then
                        e.Row.Cells(2).ForeColor = System.Drawing.ColorTranslator.FromHtml("#b35900")
                    End If
                    e.Row.Cells(2).Text = StatusT100.MO_Normal(e.Row.Cells(2).Text)
                End If
                Dim RecardNo As String = e.Row.Cells(7).Text
                If RecardNo <> String.Empty Then
                    If RecardNo <> "0" Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFE0")
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
                Dim lblProduct_ItemName As Label = CType(e.Row.FindControl("lblProduct_ItemName"), Label)
                Dim lblProduct_Spec As Label = CType(e.Row.FindControl("lblProduct_Spec"), Label)
                Dim lblProduct_Qty As Label = CType(e.Row.FindControl("lblProduct_Qty"), Label)
                Dim lblWIP As Label = CType(e.Row.FindControl("lblWIP"), Label)
                Dim lblGoodTransferIn As Label = CType(e.Row.FindControl("lblGoodTransferIn"), Label)
                Dim lblGoodTransferOut As Label = CType(e.Row.FindControl("lblGoodTransferOut"), Label)
                Dim lblMat_Request As Label = CType(e.Row.FindControl("lblMat_Request"), Label) '
                'Dim lblDueDate As Label = CType(e.Row.FindControl("lblDueDate"), Label)
                If e.Row.Cells(4).Text <> String.Empty Then
                    Dim dtItem As DataTable = IMAAL.GetDataProducItem(e.Row.Cells(4).Text)
                    If dtItem.Rows.Count > 0 Then
                        lblProduct_ItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                        lblProduct_Spec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                    End If
                    Dim dtHeadDetail As DataTable = SFAA.GetMO_HeaderDeatil(e.Row.Cells(1).Text)
                    If dtHeadDetail.Rows.Count > 0 Then
                        lblProduct_Qty.Text = dtRowsFormat.FormatDecimal(dtHeadDetail, SFAA.ProductionQty)
                        If lblProduct_Qty.Text <> String.Empty Then
                            Dim dblNumberProduct_Qty As Double = CDbl(lblProduct_Qty.Text)
                            lblProduct_Qty.Text = String.Format("{0:n3}", dblNumberProduct_Qty)
                        End If
                    End If
                    Dim dtHeadProcess As DataTable = SFCB.GetDataRowsProcessItem(e.Row.Cells(1).Text)
                    If dtHeadProcess.Rows.Count > 0 Then
                        lblWIP.Text = dtRowsFormat.FormatDecimal(dtHeadProcess, SFCB.WIP)
                        If lblWIP.Text <> String.Empty Then
                            Dim dblNumberWIP As Double = CDbl(lblWIP.Text)
                            lblWIP.Text = String.Format("{0:n3}", dblNumberWIP)
                            Dim WIP As Integer = CInt(lblWIP.Text)
                            If WIP < 0 Then
                                lblWIP.ForeColor = System.Drawing.Color.Maroon
                            End If
                        End If
                        lblGoodTransferIn.Text = dtRowsFormat.FormatDecimal(dtHeadProcess, SFCB.GoodTransferIn)
                        If lblGoodTransferIn.Text <> String.Empty Then
                            Dim dblNumberGoodTransferIn As Double = CDbl(lblGoodTransferIn.Text)
                            lblGoodTransferIn.Text = String.Format("{0:n3}", dblNumberGoodTransferIn)
                        End If
                        lblGoodTransferOut.Text = dtRowsFormat.FormatDecimal(dtHeadProcess, SFCB.GoodTransferOut)
                        If lblGoodTransferOut.Text <> String.Empty Then
                            Dim dblNumberGoodTransferOut As Double = CDbl(lblGoodTransferOut.Text)
                            lblGoodTransferOut.Text = String.Format("{0:n3}", dblNumberGoodTransferOut)
                        End If
                    End If
                    If lblGoodTransferIn.Text <> String.Empty Then
                        Dim iStdQty As Decimal = Convert.ToDecimal(lblProduct_Qty.Text)
                        Dim iMat_Issue As Decimal = Convert.ToDecimal(lblGoodTransferIn.Text)
                        Dim iMat_Request As Decimal = iStdQty - iMat_Issue
                        lblMat_Request.Text = iMat_Request
                        If lblMat_Request.Text <> String.Empty Then
                            Dim dblNumberMat_Request As Double = CDbl(lblMat_Request.Text)
                            lblMat_Request.Text = String.Format("{0:n3}", dblNumberMat_Request)
                        End If
                    End If
                End If
                Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplLinkMO"), HyperLink)
                If Not IsNothing(hplDetail) Then
                    If Not IsDBNull(.DataItem(SFBA.MODocNo)) Then
                        'hplDetail.NavigateUrl = "../Production/ProductionRecordReportPop.aspx?height=150&width=350&docno=&mo=" & .DataItem(SFBA.MO_DocNo).ToString.Trim
                        hplDetail.NavigateUrl = "ProductionRecordReportPop.aspx?height=150&width=350&MO_DocNo=" & .DataItem(SFBA.MODocNo).ToString.Trim
                        hplDetail.Attributes.Add("title", .DataItem(SFBA.MODocNo))
                    End If
                End If
            End If
        End With
    End Sub
    Private Sub gvPR_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPR.RowDataBound
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.Font.Bold = True
        End If
    End Sub
    Private Sub gvPR_Receipt_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPR_Receipt.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'If e.Row.Cells(5).Text <> String.Empty Then
            ' e.Row.Cells(2).Text = Convert.ToDecimal(e.Row.Cells(2).Text)
            'e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            'End If
            Dim lblStock_In_Date As Label = CType(e.Row.FindControl("lblStock_In_Date"), Label)
            Dim dt_Head As DataTable = PMDS.GetPR_Receipt_By_DocNo_Header(e.Row.Cells(0).Text)
            If Not IsDBNull(lblStock_In_Date) Then
                lblStock_In_Date.Text = dtRowsFormat.FormatDateMMddyyyy(dt_Head, PMDS.DocumentDate)
            End If
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
        End If
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.Font.Bold = True
        End If
    End Sub

    Private Sub gvPO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPO.RowDataBound
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            If e.Row.Cells(3).Text <> String.Empty Then
                e.Row.Cells(3).Text = Convert.ToDecimal(e.Row.Cells(3).Text)
                e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center
            End If
        End If
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.Font.Bold = True
        End If
    End Sub
End Class