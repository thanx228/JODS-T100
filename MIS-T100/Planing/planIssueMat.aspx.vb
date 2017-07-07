Imports System.Data.Linq
Imports System.Data.OracleClient

Public Class planIssueMat
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'btExportGrid.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblStd_Issuance_Qty As Label = CType(e.Row.FindControl("lblStd_Issuance_Qty"), Label)
            Dim lblIssue_Qty As Label = CType(e.Row.FindControl("lblIssue_Qty"), Label)
            Dim lblIssueOver_Qty As Label = CType(e.Row.FindControl("lblIssueOver_Qty"), Label)
            Dim lblStockInQty As Label = CType(e.Row.FindControl("lblStockInQty"), Label)
            Dim lblMO_qty As Label = CType(e.Row.FindControl("lblMO_qty"), Label)
            Dim lblPO_qty As Label = CType(e.Row.FindControl("lblPO_qty"), Label)
            Dim lblPR_qty As Label = CType(e.Row.FindControl("lblPR_qty"), Label)

            Dim lblBOM_ItemName As Label = CType(e.Row.FindControl("lblBOM_ItemName"), Label)
            Dim lblBOM_Specifiation As Label = CType(e.Row.FindControl("lblBOM_Specifiation"), Label)
            Dim BOM_Item As String = e.Row.Cells(1).Text
            If BOM_Item <> String.Empty Then
                Dim dtSpec As DataTable = IMAAL.GetDataProducItem(BOM_Item)
                If dtSpec.Rows.Count > 0 Then
                    lblBOM_ItemName.Text = dtRowsFormat.FormatString(dtSpec, IMAAL.ProductName)
                    lblBOM_Specifiation.Text = dtRowsFormat.FormatString(dtSpec, IMAAL.Specifaction)
                End If
                Dim dtPO As DataTable = PMDN.GetPO_Body_SumStockInQty_ByItemNo_Delivery(BOM_Item)
                If dtPO.Rows.Count > 0 Then
                    If Not IsNothing(lblPO_qty) Then
                        lblPO_qty.Text = dtRowsFormat.FormatString(dtPO, PMDN.SumPurchaseQty)
                        If lblPO_qty.Text <> String.Empty Then
                            Dim dblNumberPOqty As Double = CDbl(lblPO_qty.Text)
                            lblPO_qty.Text = String.Format("{0:n3}", dblNumberPOqty)
                        End If
                    End If
                Else
                    lblPO_qty.Text = "0.000"
                End If
                Dim dtPOtoStrock As DataTable = PMDO.GetPO_Body_SumStockInQty_ByItemNo_Delivery(BOM_Item)
                If dtPOtoStrock.Rows.Count > 0 Then
                    If Not IsNothing(lblStockInQty) Then
                        lblStockInQty.Text = dtRowsFormat.FormatString(dtPOtoStrock, PMDO.SumStockInQty)
                        If lblStockInQty.Text <> String.Empty Then
                            Dim dblNumberStockQty As Double = CDbl(lblStockInQty.Text)
                            lblStockInQty.Text = String.Format("{0:n3}", dblNumberStockQty)
                        End If
                    End If
                    If Not IsNothing(lblPR_qty) Then
                        lblPR_qty.Text = dtRowsFormat.FormatString(dtPOtoStrock, PMDO.SumReceivedVolume)
                        If lblPR_qty.Text <> String.Empty Then
                            Dim dblNumberPRqty As Double = CDbl(lblPR_qty.Text)
                            lblPR_qty.Text = String.Format("{0:n3}", dblNumberPRqty)
                        End If
                    End If
                Else
                    lblStockInQty.Text = "0.000"
                    lblPR_qty.Text = "0.000"
                End If
                Dim dtIssuQty As DataTable = SFBA.GetManufactureOrder_BOMitemNo_SumStdIssuanceSumIssueQty_Body(BOM_Item)
                If dtIssuQty.Rows.Count > 0 Then
                    If Not IsNothing(lblStd_Issuance_Qty) Then
                        lblStd_Issuance_Qty.Text = dtRowsFormat.FormatString(dtIssuQty, SFBA.SUMStandardIssuanceQuantity)
                        If lblStd_Issuance_Qty.Text <> String.Empty Then
                            Dim dblNumberStd_Issuance_Qty As Double = CDbl(lblStd_Issuance_Qty.Text)
                            lblStd_Issuance_Qty.Text = String.Format("{0:n3}", dblNumberStd_Issuance_Qty)
                        End If
                    End If
                    If Not IsNothing(lblIssue_Qty) Then
                        lblIssue_Qty.Text = dtRowsFormat.FormatString(dtIssuQty, SFBA.SUMIssuedQty)
                        If lblIssue_Qty.Text <> String.Empty Then
                            Dim dblNumberIssueQty As Double = CDbl(lblIssue_Qty.Text)
                            lblIssue_Qty.Text = String.Format("{0:n3}", dblNumberIssueQty)
                        End If
                    End If
                    If Not IsNothing(lblIssueOver_Qty) Then
                        lblIssueOver_Qty.Text = dtRowsFormat.FormatString(dtIssuQty, SFBA.SUMUnplannedIssued)
                        If lblIssueOver_Qty.Text <> String.Empty Then
                            Dim dblNumberIssueOverQty As Double = CDbl(lblIssueOver_Qty.Text)
                            lblIssueOver_Qty.Text = String.Format("{0:n3}", dblNumberIssueOverQty)
                        End If
                    End If
                Else
                    lblStd_Issuance_Qty.Text = "0.000"
                    lblIssue_Qty.Text = "0.000"
                    lblIssueOver_Qty.Text = "0.000"
                End If
                Dim lblIQC_Source_Qty As Label = CType(e.Row.FindControl("lblIQC_Source_Qty"), Label)
                Dim lblIQC_Inspection_Qty As Label = CType(e.Row.FindControl("lblIQC_Inspection_Qty"), Label)
                Dim lblIQC_Qty_Passed As Label = CType(e.Row.FindControl("lblIQC_Qty_Passed"), Label)
                Dim lblIQC_Qty_Defected As Label = CType(e.Row.FindControl("lblIQC_Qty_Defected"), Label)

                Dim dtIQC As DataTable = QCBA.GetIQC_SUMqty_inSpectionHeader(BOM_Item)
                ' If dtIQC.Rows.Count > 0 Then
                'If Not IsNothing(lblIQC_Source_Qty) Then
                lblIQC_Source_Qty.Text = dtRowsFormat.FormatString(dtIQC, QCBA.SUMSourceQty)
                If lblIQC_Source_Qty.Text <> String.Empty Then
                    Dim dblNumberIQC_Source_Qty As Double = CDbl(lblIQC_Source_Qty.Text)
                    lblIQC_Source_Qty.Text = String.Format("{0:n3}", dblNumberIQC_Source_Qty)
                End If
                'End If
                'If Not IsNothing(lblIQC_Inspection_Qty) Then
                lblIQC_Inspection_Qty.Text = dtRowsFormat.FormatString(dtIQC, QCBA.SUMInspectionQty)
                If lblIQC_Inspection_Qty.Text <> String.Empty Then
                    Dim dblNumberIQC_Inspection_Qty As Double = CDbl(lblIQC_Inspection_Qty.Text)
                    lblIQC_Inspection_Qty.Text = String.Format("{0:n3}", dblNumberIQC_Inspection_Qty)
                End If
                'End If
                'If Not IsNothing(lblIQC_Qty_Passed) Then
                lblIQC_Qty_Passed.Text = dtRowsFormat.FormatString(dtIQC, QCBA.SUMQtyPassed)
                If lblIQC_Qty_Passed.Text <> String.Empty Then
                    Dim dblNumberIQC_Qty_Passed As Double = CDbl(lblIQC_Qty_Passed.Text)
                    lblIQC_Qty_Passed.Text = String.Format("{0:n3}", dblNumberIQC_Qty_Passed)
                End If
                'End If
                'If Not IsNothing(lblIQC_Qty_Defected) Then
                lblIQC_Qty_Defected.Text = dtRowsFormat.FormatString(dtIQC, QCBA.SUMQtyDefected)
                If lblIQC_Qty_Defected.Text <> String.Empty Then
                    Dim dblNumberIQC_Qty_Defected As Double = CDbl(lblIQC_Qty_Defected.Text)
                    lblIQC_Qty_Defected.Text = String.Format("{0:n3}", dblNumberIQC_Qty_Defected)
                End If
                'End If
                'Else
                If lblIQC_Source_Qty.Text = String.Empty Then
                    lblIQC_Source_Qty.Text = "0.000"
                End If
                If lblIQC_Inspection_Qty.Text = String.Empty Then
                    lblIQC_Inspection_Qty.Text = "0.000"
                End If
                If lblIQC_Qty_Passed.Text = String.Empty Then
                    lblIQC_Qty_Passed.Text = "0.000"
                End If
                If lblIQC_Qty_Defected.Text = String.Empty Then
                    lblIQC_Qty_Defected.Text = "0.000"
                End If
                If lblStockInQty.Text = String.Empty Then
                    lblStockInQty.Text = "0.000"
                End If
                If lblPO_qty.Text = String.Empty Then
                    lblPO_qty.Text = "0.000"
                End If
                If lblPR_qty.Text = String.Empty Then
                    lblPR_qty.Text = "0.000"
                End If
                'End If
            End If
            If lblStd_Issuance_Qty.Text <> String.Empty Then
                lblStd_Issuance_Qty.Text = CInt(dtRowsFormat.FormatStringUnit(lblStd_Issuance_Qty.Text))
                If lblStd_Issuance_Qty.Text <> String.Empty Then
                    Dim dblNumberStd_Issuance_Qty As Double = CDbl(lblStd_Issuance_Qty.Text)
                    lblStd_Issuance_Qty.Text = String.Format("{0:n3}", dblNumberStd_Issuance_Qty)
                End If
            End If
            If lblStd_Issuance_Qty.Text <> String.Empty And lblIssue_Qty.Text <> String.Empty Then
                If Not IsNothing(lblMO_qty) Then
                    lblMO_qty.Text = CInt(lblStd_Issuance_Qty.Text) - CInt(lblIssue_Qty.Text)
                    If lblMO_qty.Text <> String.Empty Then
                        Dim dblNumberMO_qty As Double = CDbl(lblMO_qty.Text)
                        lblMO_qty.Text = String.Format("{0:n3}", dblNumberMO_qty)
                    End If
                End If
            End If
            'e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            'e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            If Not IsNothing(hplDetail) And Not IsDBNull(e.Row.DataItem(SFBA.BOMitem)) Then
                Dim link As String = ""
                link = link & "&BOM_Item= " & e.Row.DataItem(SFBA.BOMitem).ToString.Replace("-", "")
                Dim spec As String = ""
                If Not IsDBNull(lblBOM_Specifiation.Text) Then
                    spec = lblBOM_Specifiation.Text
                End If
                link = link & "&ItemName= " & lblBOM_ItemName.Text
                link = link & "&ItemSpec= " & Server.UrlEncode(spec)
                link = link & "&stock= " & lblStockInQty.Text
                link = link & "&issueQty= " & lblStd_Issuance_Qty.Text
                link = link & "&poQty= " & lblPO_qty.Text
                link = link & "&prQty= " & lblPR_qty.Text
                link = link & "&moQty= " & lblMO_qty.Text
                hplDetail.NavigateUrl = "planIssueMatPopup.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", e.Row.DataItem(SFBA.BOMitem))
            End If
        End If
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call GetDataBind()
    End Sub

    Private Shared strSqlData As String = "Select  " & SFBA.BOMitem & " " &
        " FROM " & SFBA.tblManufactureOrder_Body & "  " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFBA.BOMitem & "  " &
        " LEFT JOIN  " & SFAA.tblMO & " On " & SFAA.DocNo & " = " & SFBA.MODocNo & " " &
        " LEFT JOIN  " & SFDB.tblMatIssueSet & " On " & SFDB.WONo & " = " & SFBA.MODocNo & " " &
        " LEFT JOIN  " & SFDC.tblMatIssueDistribution & " On " & SFDC.WONo & " =" & SFDB.WONo & " " &
        " AND " & SFDC.IssueDocNo & " =" & SFDB.IssueDocNo & " " &
        " LEFT JOIN  " & SFDA.tblMatIssueHead & " On " & SFDA.IssueDocNo & "=" & SFDB.IssueDocNo & " " &
        " LEFT JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
        " LEFT JOIN  " & XMDA.tblSaleHead & " On " & XMDA.SaleOrderNo & "=" & XMDC.SaleOrderNo & " " &
        " where " & SFBA.wStandard & " And " & IMAAL.ent & "='3'  @pWhereCustomUsing  " &
       " Group by  " & SFBA.BOMitem & " "
    Public Shared Function GetManufactureOrderPlanIssueMat(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = strSqlData
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
            GetPageError.GetPage(FilePage, "GetManufactureOrderPlanIssueMat", "Sql = strSqlData", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub GetDataBind()
        Dim BOM_Item As String = tbCode.Text,
          spec As String = tbSpec.Text,
          TypeMO As String = UsingMO_Type.getObject.SelectedValue,
          MoFrom As String = ReplaceString.ReplaceMO(UsingMO_Type.getObject.SelectedValue, tbMoFrom.Text),
          MoTo As String = ReplaceString.ReplaceMO(UsingMO_Type.getObject.SelectedValue, tbMoTo.Text),
          SaleNumber As String = ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.SelectedValue, tbSoNo.Text),
          SaleSeq As String = tbSoSeq.Text,
          dateFrom As String = FormDate.dateText,
          dateTo As String = EndDate.dateText,
          cust As String = tbCust.Text.Trim,
          item As String = "",
          Qty As Integer = 0
        Dim where As String = String.Empty
        Dim wBetweenMO As String = String.Empty
        Dim wBOM_Item As String = String.Empty
        Dim wspec As String = String.Empty
        Dim wPlanStartDate As String = String.Empty
        Dim wIssueDate As String = String.Empty
        Dim wMODate As String = String.Empty
        Dim wSaleOrderNo As String = String.Empty
        Dim wTypSaleOrderNoSeq As String = String.Empty
        Dim wCodeType As String = String.Empty
        If TypeMO <> "0" AndAlso (MoFrom <> "" Or MoTo <> "") Then
            Dim strFromMO_No As String = " '" & [String].Join("','", MoFrom) & "'"
            Dim strFromMO_To As String = " '" & [String].Join("','", MoTo) & "'"
            wBetweenMO = " And " & SFBA.MODocNo & " BETWEEN " & strFromMO_No & " AND " & strFromMO_To
        Else
            wBetweenMO = String.Empty
        End If
        If BOM_Item <> String.Empty Then
            wBOM_Item = " And " & SFBA.BOMitem & "= '" & [String].Join("','", BOM_Item) & "'"
        Else
            wBOM_Item = String.Empty
        End If
        If spec <> "" Then
            wspec = " And " & IMAAL.Specifaction & " Like '%" & [String].Join("','", spec) & "%'"
        Else
            spec = String.Empty
        End If
        If cust <> "" Or cust <> String.Empty Then
            where = " And " & XMDA.CustomerId & "= '" & [String].Join("','", cust) & "'"
        End If
        If dateFrom <> "" And dateTo <> "" Then
            If ddlDate.SelectedValue <> "0" Then
                If ddlDate.SelectedValue = "1" Then
                    Dim PlanStartDate As String = SFAA.PlanStartDate & " BETWEEN TO_DATE('" & [String].Join("','", dateFrom) & "','yyyy/mm/dd') AND  TO_DATE('" & [String].Join("','", dateTo) & "','yyyy/mm/dd')"
                    wPlanStartDate = " And " & PlanStartDate
                Else
                    wPlanStartDate = String.Empty
                End If
                If ddlDate.SelectedValue = "2" Then
                    Dim IssueDate As String = SFDA.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", dateFrom) & "','yyyy/mm/dd') AND  TO_DATE('" & [String].Join("','", dateTo) & "','yyyy/mm/dd')"
                    wIssueDate = " And " & IssueDate
                Else
                    wIssueDate = String.Empty
                End If
                If ddlDate.SelectedValue = "3" Then
                    Dim MODate As String = SFAA.PlanStartDate & " = TO_DATE('" & [String].Join("','", dateFrom) & "','yyyy/mm/dd') AND " & SFAA.PlanedCompletionDate & " = TO_DATE('" & [String].Join("','", dateTo) & "','yyyy/mm/dd')"
                    wMODate = " And " & MODate
                Else
                    wMODate = String.Empty
                End If
            End If
        End If
        If UsingDocTypeSale.getObject.SelectedValue <> "0" And tbSoNo.Text <> "" Then
            Dim SaleOrderNo As String = XMDA.SaleOrderNo & "= '" & [String].Join("','", ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, tbSoNo.Text)) & "'"
            wSaleOrderNo = " And " & SaleOrderNo
        Else
            wSaleOrderNo = String.Empty
        End If
        If UsingDocTypeSale.getObject.SelectedValue <> "0" And (tbSoNo.Text <> "" And tbSoSeq.Text <> "") Then
            Dim SaleOrderNo As String = XMDA.SaleOrderNo & "= '" & [String].Join("','", ReplaceString.ReplaceJP(UsingDocTypeSale.getObject.Text, tbSoNo.Text)) & "'"
            Dim SaleSeqNo As String = XMDC.ItemSequence & "= '" & [String].Join("','", tbSoSeq.Text) & "'"
            wTypSaleOrderNoSeq = " And " & SaleOrderNo & " AND " & SaleSeqNo
        Else
            wTypSaleOrderNoSeq = String.Empty
        End If
        Dim iRow As Integer = 0
        Dim BOM_Str As String = String.Empty
        Dim YrStrList As List(Of [String]) = New List(Of String)()
        For Each sitem As ListItem In cblCodeType.Items
            If sitem.Selected Then
                YrStrList.Add(sitem.Value)
                iRow = +1
            End If
        Next
        If iRow > 0 Then
            BOM_Str = " '" & [String].Join("' , '", YrStrList.ToArray())
            wCodeType = " And  SUBSTR(" & SFBA.BOMitem & ", 3, 1) In(" & [String].Join("','", BOM_Str) & "')"
        Else
            wCodeType = String.Empty
        End If

        where = wBetweenMO & wBOM_Item & wspec & wPlanStartDate & wIssueDate & wMODate & wSaleOrderNo & wTypSaleOrderNoSeq & wCodeType
        If where <> String.Empty Then
            'lblSql.Text = GetManufactureOrderPlanIssueMat(where)
            Dim dt As DataTable = GetManufactureOrderPlanIssueMat(where)
            If dt.Rows.Count > 0 Then
                gvShow.DataSource = dt
                gvShow.DataBind()
                ucCount.RowCount = dt.Rows.Count.ToString
                btExportGrid.Visible = True
                GridviewUtility.GrigOnmouseHandleCustomer(gvShow, "#ADA9A9")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            Else
                gvShow.DataSource = New List(Of String)
                gvShow.DataBind()
                btExportGrid.Visible = False
                GridviewUtility.GridStyleTemplateStyleTooling(gvShow)
            End If
        End If

    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Call GetDataBind()
        ''System.Threading.Thread.Sleep(1000)
        ''GridviewUtility.GrigOnmouseHandleCustomer(gvShow, "#ADA9A9")
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub
    Protected Sub btExportGrid_Click(sender As Object, e As EventArgs) Handles btExportGrid.Click
        ExportsUtility.ExportGridviewToMsExcel("DailyMchPlan" & Session("UserName"), gvShow)
    End Sub



End Class