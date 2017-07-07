Imports System.Data.OracleClient
Imports System.Globalization

Public Class MoMatShortListPopup
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'lbItem.Text = "81101GG00292"
            'lbDateFrom.Text = "05/02/2017"
            'lbDateTo.Text = "05/02/2017"
            lbItem.Text = Request.QueryString("Item").ToString.Trim
            lbSpec.Text = Request.QueryString("Spec").ToString.Trim
            lbDesc.Text = Request.QueryString("Desc").ToString.Trim
            lbWH.Text = Request.QueryString("WH").ToString.Trim
            lbUnit.Text = Request.QueryString("Unit").ToString.Trim
            lbStock.Text = Request.QueryString("Stock").ToString.Trim
            lbDateFrom.Text = Request.QueryString("DateFrom").ToString.Trim
            lbDateTo.Text = Request.QueryString("DateTo").ToString.Trim
            Call ShowMO_WhereBy_BOMitem()
            Call ShowPO_WhereBy_BOMitem()
            Call PR_Receipt_WkereBy_ItemNo()
        End If
    End Sub
    Private Sub ShowPO_WhereBy_BOMitem()
        Dim dt As DataTable = GetPO_Body_By_ItemNo(lbItem.Text)
        If dt.Rows.Count > 0 Then
            gvPO.DataSource = dt
            gvPO.DataBind()
            lbCountPO.Text = gvPO.Rows.Count.ToString
            GridviewUtility.GrigOnmouseHandleCustomer(gvPO, "#ADA9A9")
        End If
    End Sub
    Private Sub PR_Receipt_WkereBy_ItemNo()
        Dim dt As DataTable = PMDT.GetPR_Receipt_By_ItemNo_BodyReceiptDetail(lbItem.Text)
        If dt.Rows.Count > 0 Then
            gvPR_Receipt.DataSource = dt
            gvPR_Receipt.DataBind()
        Else
            gvPR_Receipt.DataSource = New List(Of String)
            gvPR_Receipt.DataBind()
        End If
        lbCountPR.Text = gvPR_Receipt.Rows.Count.ToString()
    End Sub
    Private Sub ShowMO_WhereBy_BOMitem()
        Dim Where As String = String.Empty
        Dim wBOMitemNo As String = SFBA.BOMitem & "='" & lbItem.Text & "'"
        Dim WhereMO_DateBetween As String = String.Empty
        Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", lbDateFrom.Text) & "','yyyy/mm/dd')"
        Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", lbDateTo.Text) & "','yyyy/mm/dd')"
        WhereMO_DateBetween = SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
        Where = wBOMitemNo & " And " & WhereMO_DateBetween
        Dim dtMO As DataTable = GetManufactureOrder(Where)
        If dtMO.Rows.Count > 0 Then
            gvMo.DataSource = dtMO
            gvMo.DataBind()
            lbCountMO.Text = dtMO.Rows.Count.ToString
            GridviewUtility.GrigOnmouseHandleAuto(gvMo)
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
        " And  " & SFCB.tblMOprocessItem_SFCB & "." & SFCB.RunCard & " = " & SFCA.tblMO_Detail & "." & SFCA.RunCardNo & " " &
        " where " & SFBA.wStandard & " And " & SFBA.ItemSequence & "<>'999' AND " & IMAAL.ent & "='3' and @pWhereCustomUsing " &
        " Group by  " & SFBA.MODocNo & " ," & SFBA.ItemSequence & "," & SFBA.LineSequence & ", " &
        " " & SFBA.MasterItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ", " &
        " " & SFBA.OperationNo & "," & SFBA.OperationSeq & "," & SFBA.BOMitem & "," & SFBA.IssueItem & ", " &
        " " & SFBA.RequiredQty & "," & SFBA.Unit & "," & SFBA.ConsPurchase & "," & SFBA.IssuedQty & "," & SFBA.ScrapQty & ", " &
        " " & SFAA.PlanStartDate & "," & SFAA.PlanedCompletionDate & "," & SFAA.Status & "," & SFCA.RunCardNo & "," & SFCA.RunCardDetail & " " &
        " Order By " & SFBA.MODocNo & "," & SFBA.ItemSequence & "," & SFCA.RunCardNo & " "
    Private Shared Function GetManufactureOrder(strWwhere As String) As DataTable
        Dim Sql As String = strWH_BOM_Item
        Dim pWhereCustomUsing As String = strWwhere
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
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
    Private Sub gvMo_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMo.RowDataBound
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
                If Not IsDBNull(e.Row.DataItem(SFBA.MODocNo)) Then
                    'hplDetail.NavigateUrl = "../Production/ProductionRecordReportPop.aspx?height=150&width=350&docno=&mo=" & .DataItem(SFBA.MO_DocNo).ToString.Trim
                    hplDetail.NavigateUrl = "ProductionRecordReportPop.aspx?height=150&width=350&MO_DocNo=" & e.Row.DataItem(SFBA.MODocNo).ToString.Trim
                    hplDetail.Attributes.Add("title", e.Row.DataItem(SFBA.MODocNo))
                End If
            End If
        End If
    End Sub
    Private Sub gvPO_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPO.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblPOno As Label = CType(e.Row.FindControl("lblPOno"), Label)
            Dim lblItemNo As Label = CType(e.Row.FindControl("lblMatItemNo"), Label)
            Dim lblPOStatus As Label = CType(e.Row.FindControl("lblPOStatus"), Label)
            Dim lblVender_No As Label = CType(e.Row.FindControl("lblVender_No"), Label)
            Dim lblPaymentTerm As Label = CType(e.Row.FindControl("lblPaymentTerm"), Label)
            Dim lblTrandeTerm As Label = CType(e.Row.FindControl("lblTrandeTerm"), Label)
            Dim lblTaxType As Label = CType(e.Row.FindControl("lblTaxType"), Label)
            Dim lblTaxRate As Label = CType(e.Row.FindControl("lblTaxRate"), Label)
            Dim lblCurrency As Label = CType(e.Row.FindControl("lblCurrency"), Label)
            Dim lblExchRate As Label = CType(e.Row.FindControl("lblExchRate"), Label)
            If lblPOStatus.Text <> "" Then
                If lblPOStatus.Text = "Y" Then
                    lblPOStatus.ForeColor = System.Drawing.Color.Green
                End If
                lblPOStatus.Text = StatusT100.Purchase(lblPOStatus.Text)
            End If
            If lblTaxRate.Text <> "" Then
                lblTaxRate.Text = lblTaxRate.Text & " %"
            End If
            If lblPaymentTerm.Text = "001" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " Cash"
            ElseIf lblPaymentTerm.Text = "002" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " 30 DAYS"
            ElseIf lblPaymentTerm.Text = "003" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " 60 DAYS"
            ElseIf lblPaymentTerm.Text = "004" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " 45 DAYS"
            ElseIf lblPaymentTerm.Text = "005" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " 90 DAYS"
            ElseIf lblPaymentTerm.Text = "006" Then
                lblPaymentTerm.Text = lblPaymentTerm.Text & " : " & " 15 DAYS"
            End If
            lblPaymentTerm.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008ae6")
            If lblTrandeTerm.Text <> "" Or lblTrandeTerm.Text <> "DAF" Or lblTrandeTerm.Text <> "DDU" Then
                Dim dtTrandeTermType As DataTable = OOCQL.GetDataTradeTermType(lblTrandeTerm.Text)
                If dtTrandeTermType.Rows.Count > 0 Then
                    lblTrandeTerm.Text = dtRowsFormat.FormatSumString(dtTrandeTermType, OOCQL.OperationID, OOCQL.Operation)
                End If
            End If
            If lblTaxType.Text <> "" Then
                Dim SqlTaxType As String = " select " & OODBL.TaxCode & "," & OODBL.Description & " from " & OODBL.tblTaxTypeDetail & " " &
            " where " & OODBL.wStandrad & " and  " & OODBL.TaxCode & "='" & lblTaxType.Text & "' "
                Dim dtTaxType As DataTable = GetBaseOracleCustom(SqlTaxType)
                If dtTaxType.Rows.Count > 0 Then
                    lblTaxType.Text = dtRowsFormat.FormatSumString(dtTaxType, OODBL.TaxCode, OODBL.Description)
                End If
            End If
            lblTaxType.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008ae6")

            If lblVender_No.Text <> "" Then
                Dim dtVenderNo As DataTable = PMAAL.GetDataCustomerDetail(lblVender_No.Text)
                If dtVenderNo.Rows.Count > 0 Then
                    lblVender_No.Text = dtRowsFormat.FormatSumString(dtVenderNo, PMAAL.CustomerID, PMAAL.CustomerFullName)
                End If
            End If

        End If
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
            Dim lblStore_Location As Label = CType(e.Row.FindControl("lblStore_Location"), Label)
            If lblStore_Location.Text <> "" Then
                Dim dtWH As DataTable = INAA.GetWarehouseFind_Table(lblStore_Location.Text)
                If dtWH.Rows.Count > 0 Then
                    lblStore_Location.Text = dtRowsFormat.FormatSumString(dtWH, INAA.WharehouseID, INAA.Warehouse)
                End If
            End If
        End If
        If e.Row.RowType = DataControlRowType.EmptyDataRow Then
            e.Row.ForeColor = System.Drawing.Color.Maroon
            e.Row.Font.Bold = True
        End If
    End Sub
    Private Shared strWH_ItemNO As String = "Select " & PMDN.PurchaseNo & "," & PMDN.ItemNo & ", " &
        " " & PMDN.ShippingVendor & "," & PMDN.ShippingDate & "," & PMDN.PurchaseQty & "," & PMDN.PricingUnit & "," & PMDN.ArrivalDate & ", " &
        " " & PMDN.TaxType & "," & PMDO.TaxRate & "," & PMDL.Status & "," & PMDL.PaymentTerm & "," & PMDL.TradeTerms & ", " &
        " " & PMDL.Currency & "," & PMDL.ExchRate & " " &
        " FROM " & PMDN.tblPObody & "  " &
        " LEFT JOIN " & PMDO.tblPObodyDelivery & " On " & PMDO.PurchaseNo & "=" & PMDN.PurchaseNo & " And " & PMDO.ItemNo & "=" & PMDN.ItemNo & "  " &
        " LEFT JOIN " & PMDL.tblPOHeader & " On " & PMDL.PONo & "=" & PMDN.PurchaseNo & " " &
        " where " & PMDN.wStandard & " And  " & PMDN.ItemNo & " = @pItem_No  "
    Private Shared Function GetPO_Body_By_ItemNo(StrItemNO As String) As DataTable
        Dim Sql As String = strWH_ItemNO
        Sql = Sql.Replace("@pItem_No", "'" & StrItemNO & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetPO_Body_By_ItemNo", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared Function GetBaseOracleCustom(sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetBaseOracleCustom", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class