Imports System.Data.OracleClient

Public Class PCCheckBOMPopup
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            Dim tempBOM As String = "tempBOM" & Session("UserName")
            Dim tempBOMList As String = "tempBOMList" & Session("UserName")
            'CreateTempTable.createTempBom(tempBOM)
            'CreateTempTable.createTempBOMList(tempBOMList)
            lblMasterItemNo.Text = Request.QueryString("item").ToString.Trim
            If lblMasterItemNo.Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(lblMasterItemNo.Text)
                If dtItem.Rows.Count > 0 Then
                    lbItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblSpec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
                Dim dtItemProperty As DataTable = IMAA.GetDataProducItem(lblMasterItemNo.Text)
                If dtItemProperty.Rows.Count > 0 Then
                    lblProductClass.Text = dtRowsFormat.FormatString(dtItemProperty, IMAA.ProductClassification)
                    lblItemCategory.Text = dtRowsFormat.FormatString(dtItemProperty, IMAA.ItemCategory)
                    If lblItemCategory.Text <> String.Empty Then
                        If lblItemCategory.Text = "A" Then
                            lblItemCategory.Text = "A:COMBINED/PROCESSED PRODUCT"
                            lblSupplyStartegy.Text = "1:Purchase"
                        ElseIf lblItemCategory.Text = "E" Then
                            lblItemCategory.Text = "E:COST/SOFTWARE"
                        ElseIf lblItemCategory.Text = "F" Then
                            lblItemCategory.Text = "F:OFFICE SUPPLIES"
                        ElseIf lblItemCategory.Text = "M" Then
                            lblSupplyStartegy.Text = "M:MATERIAL/PART/PRODUCT"
                            lblItemCategory.Text = "2:Manufacturing"
                        ElseIf lblItemCategory.Text = "T" Then
                            lblItemCategory.Text = "T:TEMPLATE"
                        ElseIf lblItemCategory.Text = "X" Then
                            lblItemCategory.Text = "X:VIRTUAL PRODUCTS"
                        End If
                    End If
                    lblProductUnit.Text = dtRowsFormat.FormatString(dtItemProperty, IMAA.BasicUnit)
                    Dim itemMasterGroup As String = dtRowsFormat.FormatString(dtItemProperty, IMAA.PrimaryGroupCode)
                    If itemMasterGroup <> String.Empty Then
                        Dim dtItemMasterGroup As DataTable = OOCQL.GetDataFindItemMasterGroup(itemMasterGroup)
                        If dtItemMasterGroup.Rows.Count > 0 Then
                            lblMainGroupCode.Text = dtRowsFormat.FormatSumString(dtItemMasterGroup, OOCQL.OperationID, OOCQL.Operation)
                        End If
                    End If
                End If
                Dim SqlBOM As String = "select " & BMBA.LineNo & "," & BMBA.ChildItemNo & "," & BMBA.QPA & "," & BMBA.Denominator & ", " &
    " " & BMBA.IssueUnit & "," & BMBA.EffectiveDateTime & " from " & BMBA.tblBOMdetail & " " &
    " where " & BMBA.wStandard & "  And " & BMBA.MasterItemNo & "='" & lblMasterItemNo.Text & "'  Order By " & BMBA.LineNo & " ASC "
                Dim dtBOM As DataTable = GetDataOrcaleBase(SqlBOM)
                If dtBOM.Rows.Count > 0 Then
                    gvBOMDetail.DataSource = dtBOM
                    gvBOMDetail.DataBind()
                    lbCountBOMitem.Text = dtBOM.Rows.Count.ToString
                End If
            End If
            'ControlForm.ShowGridView(gvBOMList, SQL, Conn_SQL.ERP_ConnectionString)
            'lbCountMO.Text = gvBOMList.Rows.Count

        End If
    End Sub
    Private Function GetDataOrcaleBase(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            ex.Message.ToString()
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Sub gvBOMDetail_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvBOMDetail.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim BOMItemNo As String = e.Row.Cells(2).Text
            Dim lblBOMItemName As Label = CType(e.Row.FindControl("lblBOMItemName"), Label)
            Dim lblBOMSpecifaction As Label = CType(e.Row.FindControl("lblBOMSpecifaction"), Label)
            Dim lblBOMItemCategory As Label = CType(e.Row.FindControl("lblBOMItemCategory"), Label)
            Dim lblBOMItemSupplyStatregy As Label = CType(e.Row.FindControl("lblBOMItemSupplyStatregy"), Label)

            Dim lblBOMinStock As Label = CType(e.Row.FindControl("lblBOMinStock"), Label)
            Dim lblBOMIssue_Qty As Label = CType(e.Row.FindControl("lblBOMIssue_Qty"), Label)
            Dim lblBOMUnIssue_Qty As Label = CType(e.Row.FindControl("lblBOMUnIssue_Qty"), Label)
            Dim lblBOMMO_Qty As Label = CType(e.Row.FindControl("lblBOMMO_Qty"), Label)
            Dim lblBOMPO_Qty As Label = CType(e.Row.FindControl("lblBOMPO_Qty"), Label)
            Dim lblBOMPR_Qty As Label = CType(e.Row.FindControl("lblBOMPR_Qty"), Label)
            Dim lblBOMSO_Qty As Label = CType(e.Row.FindControl("lblBOMSO_Qty"), Label)
            Dim lblBOMCapacity As Label = CType(e.Row.FindControl("lblBOMCapacity"), Label)

            If BOMItemNo <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(BOMItemNo)
                If dtItem.Rows.Count > 0 Then
                    lblBOMItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblBOMSpecifaction.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
                Dim dtItemDetail As DataTable = IMAA.GetDataProducItem(BOMItemNo)
                If dtItemDetail.Rows.Count > 0 Then
                    lblBOMItemCategory.Text = dtRowsFormat.FormatString(dtItemDetail, IMAA.ItemCategory)
                    lblBOMCapacity.Text = dtRowsFormat.FormatString(dtItemDetail, IMAA.Capacity)
                    If lblBOMCapacity.Text <> String.Empty Then
                        Dim dblNumber As Double = CDbl(lblBOMCapacity.Text)
                        lblBOMCapacity.Text = String.Format("{0:n3}", dblNumber)
                    End If
                    If lblBOMItemCategory.Text <> String.Empty Then
                        If lblBOMItemCategory.Text = "A" Then
                            lblBOMItemCategory.Text = "A:COMBINED/PROCESSED PRODUCT"
                            lblBOMItemSupplyStatregy.Text = "1:Purchase"
                        ElseIf lblBOMItemCategory.Text = "E" Then
                            lblBOMItemCategory.Text = "E:COST/SOFTWARE"
                        ElseIf lblBOMItemCategory.Text = "F" Then
                            lblBOMItemCategory.Text = "F:OFFICE SUPPLIES"
                        ElseIf lblBOMItemCategory.Text = "M" Then
                            lblBOMItemCategory.Text = "M:MATERIAL/PART/PRODUCT"
                            lblBOMItemSupplyStatregy.Text = "2:Manufacturing"
                        ElseIf lblBOMItemCategory.Text = "T" Then
                            lblBOMItemCategory.Text = "T:TEMPLATE"
                        ElseIf lblBOMItemCategory.Text = "X" Then
                            lblBOMItemCategory.Text = "X:VIRTUAL PRODUCTS"
                        End If
                    End If
                End If
                Dim hlBOMSubPart As HyperLink = CType(e.Row.FindControl("hlBOMSubPart"), HyperLink)
                Dim iCheckSubpart As String = String.Empty
                Dim SqlBOM As String = "select count(" & BMBA.ChildItemNo & ") as cuntBom from " & BMBA.tblBOMdetail & " " &
                " where " & BMBA.wStandard & "  and " & BMBA.MasterItemNo & "='" & BOMItemNo & "'"
                Dim dtBOMSubpart As DataTable = GetDataOrcaleBase(SqlBOM)
                If dtBOMSubpart.Rows.Count > 0 Then
                    iCheckSubpart = dtRowsFormat.FormatString(dtBOMSubpart, "cuntBom")
                End If
                If iCheckSubpart <> "0" Then
                    If Not IsNothing(hlBOMSubPart) Then
                        Dim link As String = "&item= " & BOMItemNo
                        hlBOMSubPart.NavigateUrl = "PCCheckBOMPopup.aspx?height=150&width=350" & link
                        hlBOMSubPart.Attributes.Add("title", BOMItemNo)
                    End If
                    lblBOMinStock.Text = " "
                    lblBOMIssue_Qty.Text = " "
                    lblBOMUnIssue_Qty.Text = " "
                    lblBOMMO_Qty.Text = " "
                    lblBOMPO_Qty.Text = " "
                    lblBOMPR_Qty.Text = " "
                    lblBOMSO_Qty.Text = " "

                Else
                    hlBOMSubPart.Text = ""
                    Dim dtIssueSum As DataTable = SFBA.GetManufactureOrder_BOMitemNo_SumStdIssuanceSumIssueQty_Body(BOMItemNo)
                    If dtItem.Rows.Count > 0 Then
                        lblBOMIssue_Qty.Text = dtRowsFormat.FormatString(dtIssueSum, SFBA.SUMIssuedQty)
                        lblBOMUnIssue_Qty.Text = dtRowsFormat.FormatString(dtIssueSum, SFBA.SUMUnIssuedQty)
                        If lblBOMIssue_Qty.Text <> String.Empty Then
                            Dim dblNumberIssue As Double = CDbl(lblBOMIssue_Qty.Text)
                            lblBOMIssue_Qty.Text = String.Format("{0:n3}", dblNumberIssue)
                        Else
                            lblBOMIssue_Qty.Text = " "
                        End If
                        If lblBOMUnIssue_Qty.Text <> String.Empty Then
                            Dim dblNumberUniIssue As Double = CDbl(lblBOMUnIssue_Qty.Text)
                            lblBOMUnIssue_Qty.Text = String.Format("{0:n3}", dblNumberUniIssue)
                        Else
                            lblBOMUnIssue_Qty.Text = " "
                        End If
                    End If
                    Dim whereBOM As String = INBC.ItemNo & "='" & BOMItemNo & "'"
                    Dim SqlDataMatInStockToWH100 As String = "select sum(" & INBC.Quantity & ") as SumStockActualQty " &
                            " from " & INBC.tblStockInDetail & "  where " & INBC.WStandard & " AND " & whereBOM & " "
                    Dim dtStrock As DataTable = GetDataOrcaleBase(SqlDataMatInStockToWH100)
                    If dtStrock.Rows.Count > 0 Then
                        lblBOMinStock.Text = dtRowsFormat.FormatString(dtStrock, "SumStockActualQty")
                        If lblBOMinStock.Text <> String.Empty Then
                            Dim dblNumberInStock As Double = CDbl(lblBOMinStock.Text)
                            lblBOMinStock.Text = String.Format("{0:n3}", dblNumberInStock)
                        Else
                            lblBOMinStock.Text = " "
                        End If
                    End If

                    Dim whereBOM_Moqty As String = SFBA.BOMitem & "='" & BOMItemNo & "'"
                    Dim SqlDataBOM_MOqt6y As String = "select sum(" & SFAA.ProductionQty & ") as sumMOQty " &
                    " from " & SFBA.tblManufactureOrder_Body & " left join " & SFAA.tblMO & " on " & SFAA.DocNo & "=" & SFBA.MODocNo & " " &
                    " where " & SFBA.wStandard & " And " & whereBOM_Moqty & " "
                    Dim dtBOM_MOqty As DataTable = GetDataOrcaleBase(SqlDataBOM_MOqt6y)
                    If dtBOM_MOqty.Rows.Count > 0 Then
                        lblBOMMO_Qty.Text = dtRowsFormat.FormatString(dtBOM_MOqty, "sumMOQty")
                        If lblBOMMO_Qty.Text <> String.Empty Then
                            Dim dblMOqty As Double = CDbl(lblBOMMO_Qty.Text)
                            lblBOMMO_Qty.Text = String.Format("{0:n3}", dblMOqty)
                        Else
                            lblBOMMO_Qty.Text = " "
                        End If
                    End If
                End If
                Dim whereBOM_SOqty As String = XMDC.Item & "='" & BOMItemNo & "'"
                Dim SqlDataBOM_SOqty As String = "select sum(" & XMDC.SalesQty & ") as sumSOQty " &
                    " from " & XMDC.tblSaleItem & " left join " & XMDA.tblSaleHead & " on " & XMDA.SaleOrderNo & "=" & XMDC.SaleOrderNo & " " &
                    " where " & XMDC.wStandard & " and " & XMDA.OrderType & " in('1','2') And " & whereBOM_SOqty & " "
                Dim dtBOM_SOqty As DataTable = GetDataOrcaleBase(SqlDataBOM_SOqty)
                If dtBOM_SOqty.Rows.Count > 0 Then
                    lblBOMSO_Qty.Text = dtRowsFormat.FormatString(dtBOM_SOqty, "sumSOQty")
                    If lblBOMSO_Qty.Text <> String.Empty Then
                        Dim dblSOqty As Double = CDbl(lblBOMSO_Qty.Text)
                        lblBOMSO_Qty.Text = String.Format("{0:n3}", dblSOqty)
                    Else
                        lblBOMSO_Qty.Text = " "
                    End If
                End If

                Dim dtPOtoStrock As DataTable = PMDO.GetPO_Body_SumStockInQty_ByItemNo_Delivery(BOMItemNo)
                If dtPOtoStrock.Rows.Count > 0 Then
                    lblBOMPO_Qty.Text = dtRowsFormat.FormatString(dtPOtoStrock, PMDO.SumStockInQty)
                    If lblBOMPO_Qty.Text <> String.Empty Then
                        Dim dblPOStockQty As Double = CDbl(lblBOMPO_Qty.Text)
                        lblBOMPO_Qty.Text = String.Format("{0:n3}", dblPOStockQty)
                    End If
                End If
                lblBOMPR_Qty.Text = dtRowsFormat.FormatString(dtPOtoStrock, PMDO.SumReceivedVolume)
                If lblBOMPR_Qty.Text <> String.Empty Then
                    Dim dblPRqty As Double = CDbl(lblBOMPR_Qty.Text)
                    lblBOMPR_Qty.Text = String.Format("{0:n3}", dblPRqty)
                End If

            End If
        End If
    End Sub
End Class