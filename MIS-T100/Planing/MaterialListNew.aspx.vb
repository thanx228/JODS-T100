Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class MaterialListNew
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If

            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

            'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            'ControlForm.showDDL(ddlWorkType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            'tbOrdQty.Text = 1
            TabContainer1.ActiveTabIndex = 0

        End If
    End Sub

    Protected Sub btShowItem_Click(sender As Object, e As EventArgs) Handles btShowItem.Click
        Dim where As String = String.Empty
        Dim spec As String = tbSpec.Text.Trim
        If tbItem.Text.Trim = "" And tbSpec.Text.Trim = "" And tbDesc.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Item ,Desc and Spec is null !!!", UpdatePanel1)
            tbItem.Focus()
            Exit Sub
        Else
            Dim wItemNo As String = ""
            Dim wItemName As String = ""
            Dim wItemSpec As String = ""
            If tbItem.Text <> "" Then
                wItemNo = " and " & SFAA.ProductItem & " = '" & tbItem.Text & "'"
            End If
            If tbDesc.Text <> "" Then
                wItemName = " and " & IMAAL.ProductName & " Like '" & tbDesc.Text & " %'"
            End If
            If tbSpec.Text <> "" Then
                wItemSpec = " and " & IMAAL.Specifaction & " Like '" & tbSpec.Text & " %'"
            End If
            where = wItemNo & wItemName & wItemSpec
            Dim dtItemFG As DataTable = GetDataOracleDate(where)
            If dtItemFG.Rows.Count > 0 Then
                gvShow.DataSource = dtItemFG
                gvShow.DataBind()
                lbCount.Text = dtItemFG.Rows.Count.ToString
                btExport.Visible = True
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            End If
        End If
    End Sub
    Protected Sub btShowSO_Click(sender As Object, e As EventArgs) Handles btShowSO.Click
        Dim where As String = String.Empty
        Dim wSaleOrderNo As String = String.Empty
        Dim wSaleNoSeq As String = String.Empty
        Dim wwSaleCust As String = String.Empty
        Dim wDate As String = String.Empty


        Dim SaleCust As String = tbSaleCust.Text
        Dim DocTypeSale As String = UsingDocTypeSale.getObject.SelectedValue
        Dim SaleNo As String = tbSaleNo.Text
        Dim SaleSeq As String = tbSaleSeq.Text
        Dim DateFm As String = FromDateT100.Text
        Dim DateTo As String = ToDateT100.Text

        Dim W_SaleCust As String = XMDA.CustomerId & " = '" & SaleCust & "'"
        Dim w_DocTypeSale As String = "substr(" & XMDC.SaleOrderNo & ", 3, 4) = '" & DocTypeSale & "'"
        Dim w_SaleNo As String = "substr(" & XMDC.SaleOrderNo & ", 8, 11) = '" & SaleNo & "'"
        Dim w_SaleSeq As String = XMDC.ItemSequence & " = '" & SaleSeq & " '"
        Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", DateFm) & "','yyyy/mm/dd')"
        Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", DateTo) & "','yyyy/mm/dd')"
        If DocTypeSale <> "0" Then
            If SaleNo <> String.Empty AndAlso SaleSeq <> String.Empty Then
                wSaleOrderNo = " and " & w_DocTypeSale & " and " & w_SaleNo & " and " & w_SaleSeq
            End If
            If SaleNo <> String.Empty AndAlso SaleSeq = String.Empty Then
                wSaleNoSeq = " and " & w_DocTypeSale & " and " & w_SaleNo
            End If
        Else
            If SaleCust <> String.Empty Then
                wwSaleCust = " and " & W_SaleCust
            End If
            If DateFm <> String.Empty AndAlso DateTo <> String.Empty Then
                wDate = " and " & XMDA.DocumentDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
            End If
        End If
        where = wSaleOrderNo & wSaleNoSeq & wSaleNoSeq & wwSaleCust & wDate

        Dim dtItemFG As DataTable = GetDataOracleDate(where)
        If dtItemFG.Rows.Count > 0 Then
            gvShow.DataSource = dtItemFG
            gvShow.DataBind()
            lbCount.Text = dtItemFG.Rows.Count.ToString
            btExport.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If

    End Sub
    Protected Sub btShowMO_Click(sender As Object, e As EventArgs) Handles btShowMO.Click
        Dim where As String = String.Empty
        Dim SaleCust As String = tbWorkCust.Text
        Dim MOType As String = UsingMO_Type.getObject.SelectedValue
        Dim MO_No As String = tbWorkNo.Text
        Dim SaleSeq As String = tbSaleSeq.Text
        Dim DateFm As String = fomDateT100.Text
        Dim DateTo As String = DateT100To.Text

        Dim W_SaleCust As String = XMDA.CustomerId & " = '" & SaleCust & "'"
        Dim w_MoType As String = "substr(" & SFBA.MODocNo & ", 3, 4) = '" & MOType & "'"
        Dim w_MO_No As String = "substr(" & SFBA.MODocNo & ", 8, 11) = '" & MO_No & "'"
        Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", DateFm) & "','yyyy/mm/dd')"
        Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", DateTo) & "','yyyy/mm/dd')"
        If MOType <> "0" Then
            If w_MO_No <> String.Empty Then
                where = " and " & w_MoType & " and " & w_MO_No
            Else
                where = " and " & w_MoType
            End If
        Else
            If W_SaleCust <> String.Empty Then
                where = " and " & W_SaleCust
            End If
            If DateFm <> String.Empty AndAlso DateTo <> String.Empty Then
                where = " and " & SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
            End If
        End If

        Dim dtItemFG As DataTable = GetDataOracleDate(where)
        If dtItemFG.Rows.Count > 0 Then
            gvShow.DataSource = dtItemFG
            gvShow.DataBind()
            lbCount.Text = dtItemFG.Rows.Count.ToString
            btExport.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub
    Protected Sub btShowCust_Click(sender As Object, e As EventArgs) Handles btShowCust.Click
        Dim where As String = String.Empty
        Dim SaleCust As String = tbCust.Text
        Dim saleStatus As String = ddlSourceStatus.SelectedValue

        Dim W_SaleCust As String = XMDA.CustomerId & " = '" & SaleCust & "'"
        Dim W_SaleStatus As String = XMDA.DocStatus & " not in('C','c')"
        If W_SaleCust <> String.Empty And saleStatus <> "1" Then
            where = " and " & W_SaleCust
        ElseIf W_SaleCust <> String.Empty And saleStatus = "1" Then
            where = " and " & W_SaleCust & " and " & W_SaleStatus
        End If
        If saleStatus = "1" And W_SaleCust = String.Empty Then
            where = " and " & W_SaleStatus
        End If
        Dim dtItemFG As DataTable = GetDataOracleDate(where)
        If dtItemFG.Rows.Count > 0 Then
            gvShow.DataSource = dtItemFG
            gvShow.DataBind()
            lbCount.Text = dtItemFG.Rows.Count.ToString
            btExport.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        End If
    End Sub
    Private Shared SqlDataOrcale As String = " select " & SFBA.MODocNo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFBA.ItemSequence & "," & SFBA.BOMitem & "," & SFBA.StandardIssuanceQuantity & "," & SFBA.IssuedQty & "," & SFBA.Unit & "  " &
    " from " & SFBA.tblManufactureOrder_Body & " " &
    " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFBA.MODocNo & " " &
    " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " and " & XMDA.OrderType & "<>'5' " &
    " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
    " where " & SFBA.wStandard & "  @WhereCustom" &
    " group by " & SFBA.MODocNo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFBA.ItemSequence & "," & SFBA.BOMitem & "," & SFBA.StandardIssuanceQuantity & "," & SFBA.IssuedQty & "," & SFBA.Unit & "  " &
    " Order by " & SFBA.MODocNo & "," & SFBA.ItemSequence & " ASC "
    Private Shared Function GetDataOracleDate(SqlWhere As String) As DataTable
        Dim strSQL As String = SqlDataOrcale
        strSQL = strSQL.Replace("@WhereCustom", "" & SqlWhere & "")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataOracleDate", "strSQL", ex.Message)
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
    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#F5C688'")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")

            Dim BOMitemNo As Label = CType(e.Row.FindControl("lblBOMitemNo"), Label)
            Dim lblBOMItemName As Label = CType(e.Row.FindControl("lblBOMItemName"), Label)
            Dim lblBOMspec As Label = CType(e.Row.FindControl("lblBOMspec"), Label)
            If BOMitemNo.Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(BOMitemNo.Text)
                If dtItem.Rows.Count > 0 Then
                    lblBOMItemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblBOMspec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
            End If
            Dim lblStdIssueQty As Label = CType(e.Row.FindControl("lblStdIssueQty"), Label)
            Dim lblIssueQty As Label = CType(e.Row.FindControl("lblIssueQty"), Label)
            Dim lblUnIssuedQty As Label = CType(e.Row.FindControl("lblUnIssuedQty"), Label)
            If lblStdIssueQty.Text <> String.Empty And lblIssueQty.Text <> String.Empty Then
                Dim istdIssue As Decimal = Convert.ToDecimal(lblStdIssueQty.Text)
                Dim iIssue As Decimal = Convert.ToDecimal(lblIssueQty.Text)
                Dim Result As Double = istdIssue - iIssue
                Dim strResult As String = Result
                Dim ddUnIssueQty As Double = CDbl(strResult)
                lblUnIssuedQty.Text = String.Format("{0:n3}", ddUnIssueQty)
            End If
        End If
    End Sub
    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("MaterialsListNew" & Session("UserName"), gvShow)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

End Class