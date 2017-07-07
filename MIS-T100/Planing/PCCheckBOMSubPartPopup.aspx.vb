Imports System.Data.OracleClient

Public Class PCCheckBOMSubPartPopup
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
                Dim SqlBOM As String = "select " & BMBA.LineNo & "," & BMBA.ChildItemNo & "," & BMBA.QPA & "," & BMBA.Denominator & ", " &
    " " & BMBA.IssueUnit & "," & BMBA.EffectiveDateTime & " from " & BMBA.tblBOMdetail & " " &
    " where " & BMBA.wStandard & "  And " & BMBA.MasterItemNo & "='" & lblMasterItemNo.Text & "'  Order By " & BMBA.LineNo & " ASC "
                Dim dtBOM As DataTable = GetDataOrcaleBase(SqlBOM)
                If dtBOM.Rows.Count > 0 Then
                    gvBOMDetail.DataSource = dtBOM
                    gvBOMDetail.DataBind()
                    lbCountBOMsubpart.Text = dtBOM.Rows.Count.ToString
                End If
            End If
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
            If BOMItemNo <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(BOMItemNo)
                If dtItem.Rows.Count > 0 Then
                    lblBOMItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblBOMSpecifaction.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
                Dim dtItemDetail As DataTable = IMAA.GetDataProducItem(BOMItemNo)
                If dtItemDetail.Rows.Count > 0 Then
                    lblBOMItemCategory.Text = dtRowsFormat.FormatString(dtItemDetail, IMAA.ItemCategory)
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
                        hlBOMSubPart.NavigateUrl = "PCCheckBOMSubPartPopup.aspx?height=150&width=350" & link
                        hlBOMSubPart.Attributes.Add("title", BOMItemNo)
                    End If
                Else
                    hlBOMSubPart.Text = ""
                End If
            End If

        End If
    End Sub

End Class