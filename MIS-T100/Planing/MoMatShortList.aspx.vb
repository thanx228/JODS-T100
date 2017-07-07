Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class MOMaterialShort
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate
    Dim WHtypeSP As String = "('2205','2206','2210')"
    Dim WHtypeRM As String = "('2201','2202','2203','2204','2209','2601','7777')"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Dim SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            'ControlForm.showDDL(ddlMOType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Call DataBindShow()
        'CountRow1.RowCount = ControlForm.rowGridview(gvShow)
        'btExport.Visible = True
        'System.Threading.Thread.Sleep(1000)

    End Sub
    Private Sub DataBindShow()
        Dim WH_type As String = ddlTypeMat.SelectedValue
        Dim MO_type As String = UsingMO_Type.getObject.SelectedValue
        Dim ProductItem As String = tbItem.Text
        Dim ProductSpec As String = tbSpec.Text
        Dim MatItem As String = tbMatItem.Text
        Dim MatSpec As String = tbMatSpec.Text
        Dim MO_DateFrom As String = DateFrom.Text
        Dim MO_ToDate As String = ToDate.Text

        Dim Where As String = String.Empty
        Dim WhereWH_type As String = String.Empty
        Dim WhereMO_type As String = String.Empty
        Dim WhereProductItem As String = String.Empty
        Dim WhereProductSpec As String = String.Empty
        Dim WhereMatItem As String = String.Empty
        Dim WhereMatSpec As String = String.Empty
        Dim WhereMO_DateBetween As String = String.Empty
        If WH_type = "1" Then
            WhereWH_type = " and " & INBC.WH & " in" & WHtypeRM & ""
        ElseIf WH_type = "4" Then
            WhereWH_type = " and " & INBC.WH & " in" & WHtypeSP & ""
        Else 'All
            WhereWH_type = String.Empty
        End If
        If MO_type <> "0" Then
            WhereMO_type = " and substr(" & SFBA.MODocNo & ", 3, 4) = '" & MO_type & "'"
        Else
            WhereMO_type = String.Empty
        End If
        If ProductItem <> String.Empty Then
            WhereProductItem = " and " & SFAA.ProductItem & " ='" & ProductItem & "'"
        Else
            WhereProductItem = String.Empty
        End If
        If ProductSpec <> String.Empty Then
            WhereProductSpec = " and ProductDetail." & IMAAL.Specifaction & " Like '" & ProductSpec & "%'"
        Else
            WhereProductSpec = String.Empty
        End If
        If MatItem <> String.Empty Then
            WhereMatItem = " and inventory." & INBC.ItemNo & " ='" & MatItem & "'"
        Else
            WhereMatItem = String.Empty
        End If
        If MatSpec <> String.Empty Then
            WhereMatSpec = " and inventoryDetail." & IMAAL.Specifaction & " Like '" & MatSpec & "%'"
        Else
            WhereMatSpec = String.Empty
        End If
        If MO_DateFrom <> String.Empty And MO_ToDate <> String.Empty Then
            Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", MO_DateFrom) & "','yyyy/mm/dd')"
            Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", MO_ToDate) & "','yyyy/mm/dd')"
            WhereMO_DateBetween = " and " & SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
        Else
            DateFrom.Text = Date.Today.ToString("yyyy/MM/dd")
            ToDate.Text = Date.Today.ToString("yyyy/MM/dd")
            Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", Date.Today.ToString("yyyy/MM/dd")) & "','yyyy/mm/dd')"
            Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", Date.Today.ToString("yyyy/MM/dd")) & "','yyyy/mm/dd')"
            WhereMO_DateBetween = " and " & SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
        End If
        Where = WhereWH_type & WhereMO_type & WhereProductItem & WhereProductSpec & WhereMatItem & WhereMatSpec & WhereMO_DateBetween
        If Where <> String.Empty Then
            Dim dtData As DataTable = GetDataOracleDate(Where)
            If dtData.Rows.Count > 0 Then
                gvShow.DataSource = dtData
                gvShow.DataBind()
                CountRow1.RowCount = dtData.Rows.Count.ToString
                btExport.Visible = True
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
                'GridviewUtility.MergeCells(gvShow)
            End If
        End If
    End Sub
    Private Shared SqlDataOrcale As String = " select " & SFBA.BOMitem & ",( select sum(" & INBC.Quantity & ") from " & INBC.tblStockInDetail & " " &
    " where " & INBC.ItemNo & "=" & SFBA.BOMitem & " And " & INBC.WH & "=inventory." & INBC.WH & ") as Stock_in," &
    " " & INBC.TradingUnit & "," & INBC.WH & "  " &
    " from " & SFBA.tblManufactureOrder_Body & " " &
    " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFBA.MODocNo & " " &
    " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " And " & XMDA.OrderType & "<>'5' " &
    " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " ProductDetail On  ProductDetail." & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
    " left join " & INBC.tblStockInDetail & " inventory On inventory." & INBC.ItemNo & "=" & SFBA.BOMitem & " " &
   "  left join  " & IMAAL.tblProductionDetail & " inventoryDetail On  inventoryDetail." & IMAAL.ProductItem & "=inventory." & INBC.ItemNo & " " &
    " where " & SFBA.wStandard & "  @WhereCustom" &
    " group by " & SFBA.BOMitem & "," & INBC.TradingUnit & "," & INBC.WH & " " &
    " order by " & SFBA.BOMitem & " "
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
    Private Shared Function GetDataOracleCustom(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
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
    Private Sub btExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("MoMatShotList" & Session("UserName"), gvShow)
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim hplDetail As HyperLink = CType(e.Row.FindControl("hplShow"), HyperLink)
            Dim lblBOMitemNo As Label = CType(e.Row.FindControl("lblBOMitemNo"), Label)
            Dim lblBOMitemName As Label = CType(e.Row.FindControl("lblBOMitemName"), Label)
            Dim lblBOMitemSpec As Label = CType(e.Row.FindControl("lblBOMitemSpec"), Label)
            Dim lblWH As Label = CType(e.Row.FindControl("lblWH"), Label)
            Dim lblStock As Label = CType(e.Row.FindControl("lblStock"), Label)
            Dim lblStcokinUint As Label = CType(e.Row.FindControl("lblStcokinUint"), Label)

            Dim lblMONo As Label = CType(e.Row.FindControl("lblMONo"), Label)
            Dim lblMOqty As Label = CType(e.Row.FindControl("lblMOqty"), Label)
            Dim lblStdIssueqty As Label = CType(e.Row.FindControl("lblStdIssueqty"), Label)
            Dim lblIssueqty As Label = CType(e.Row.FindControl("lblIssueqty"), Label)
            Dim lblUnIssueqty As Label = CType(e.Row.FindControl("lblUnIssueqty"), Label)
            Dim lblPRqty As Label = CType(e.Row.FindControl("lblPRqty"), Label)
            Dim lblPOqty As Label = CType(e.Row.FindControl("lblPOqty"), Label)
            Dim MO_type As String = UsingMO_Type.getObject.SelectedValue
            Dim ProductItem As String = tbItem.Text
            Dim MatItem As String = lblBOMitemNo.Text
            Dim MO_DateFrom As String = DateFrom.Text
            Dim MO_ToDate As String = ToDate.Text
            Dim Where As String = String.Empty
            Dim WhereWH_type As String = String.Empty
            Dim WhereMO_type As String = String.Empty
            Dim WhereProductItem As String = String.Empty
            Dim WhereMatItem As String = String.Empty
            Dim WhereMO_DateBetween As String = String.Empty
            If lblBOMitemNo.Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(lblBOMitemNo.Text)
                If dtItem.Rows.Count > 0 Then
                    lblBOMitemName.Text = dtRowsFormat.FormatString(dtItem, IMAAL.ProductName)
                    lblBOMitemSpec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If

                If MO_type <> "0" Then
                    WhereMO_type = " and substr(" & SFBA.MODocNo & ", 3, 4) = '" & MO_type & "'"
                Else
                    WhereMO_type = String.Empty
                End If
                If ProductItem <> String.Empty Then
                    WhereProductItem = " and " & SFAA.ProductItem & " ='" & ProductItem & "'"
                Else
                    WhereProductItem = String.Empty
                End If
                If MatItem <> String.Empty Then
                    WhereMatItem = " and " & SFBA.BOMitem & " ='" & MatItem & "'"
                Else
                    WhereMatItem = String.Empty
                End If
                If MO_DateFrom <> String.Empty And MO_ToDate <> String.Empty Then
                    Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", MO_DateFrom) & "','yyyy/mm/dd')"
                    Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", MO_ToDate) & "','yyyy/mm/dd')"
                    WhereMO_DateBetween = " and " & SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
                Else
                    Dim w_DateFm As String = " TO_DATE('" & [String].Join("','", Date.Today.ToString("yyyy/mm/dd")) & "','yyyy/mm/dd')"
                    Dim w_DateTo As String = " TO_DATE('" & [String].Join("','", Date.Today.ToString("yyyy/mm/dd")) & "','yyyy/mm/dd')"
                    WhereMO_DateBetween = " and " & SFAA.PlanStartDate & " BETWEEN " & w_DateFm & " and " & w_DateTo
                End If
                Where = WhereWH_type & WhereMO_type & WhereProductItem & WhereMatItem & WhereMO_DateBetween
                Dim SqlMO As String = " select  " & SFAA.DocNo & "," & SFAA.ProductionQty & "," & SFBA.StandardIssuanceQuantity & "," & SFBA.IssuedQty & ", " &
                    "  " & SFBA.StandardIssuanceQuantity & "-" & SFBA.IssuedQty & " as UnIssueQty  " &
    " from " & SFBA.tblManufactureOrder_Body & " " &
    " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFBA.MODocNo & " " &
    " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " And " & XMDA.OrderType & "<>'5' " &
    " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " ProductDetail On  ProductDetail." & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
    " left join " & INBC.tblStockInDetail & " inventory On inventory." & INBC.ItemNo & "=" & SFBA.BOMitem & " " &
   "  left join  " & IMAAL.tblProductionDetail & " inventoryDetail On  inventoryDetail." & IMAAL.ProductItem & "=inventory." & INBC.ItemNo & " " &
    " where " & SFBA.wStandard & "  " & Where & " "
                Dim dtMO As DataTable = GetDataOracleCustom(SqlMO)
                If dtMO.Rows.Count > 0 Then
                    lblMONo.Text = dtRowsFormat.FormatString(dtMO, SFAA.DocNo)
                    lblMOqty.Text = dtRowsFormat.FormatString(dtMO, SFAA.ProductionQty)
                    Dim ddMOQty As Double = CDbl(lblMOqty.Text)
                    lblMOqty.Text = String.Format("{0:n3}", ddMOQty)
                    lblStdIssueqty.Text = dtRowsFormat.FormatString(dtMO, SFBA.StandardIssuanceQuantity)
                    Dim ddStdQty As Double = CDbl(lblStdIssueqty.Text)
                    lblStdIssueqty.Text = String.Format("{0:n3}", ddStdQty)
                    lblIssueqty.Text = dtRowsFormat.FormatString(dtMO, SFBA.IssuedQty)
                    Dim ddIssueQty As Double = CDbl(lblIssueqty.Text)
                    lblIssueqty.Text = String.Format("{0:n3}", ddIssueQty)
                    lblUnIssueqty.Text = dtRowsFormat.FormatString(dtMO, "UnIssueQty")
                    Dim ddUnIssueQty As Double = CDbl(lblUnIssueqty.Text)
                    lblUnIssueqty.Text = String.Format("{0:n3}", ddUnIssueQty)
                End If

                Dim dtPOtoStrock As DataTable = PMDO.GetPO_Body_SumStockInQty_ByItemNo_Delivery(lblBOMitemNo.Text)
                If dtPOtoStrock.Rows.Count > 0 Then
                    lblPRqty.Text = dtRowsFormat.FormatString(dtPOtoStrock, PMDO.SumReceivedVolume)
                    If lblPRqty.Text <> String.Empty Then
                        Dim dblNumberPRqty As Double = CDbl(lblPRqty.Text)
                        lblPRqty.Text = String.Format("{0:n3}", dblNumberPRqty)
                    End If
                End If

                Dim dtPO As DataTable = PMDN.GetPO_Body_SumStockInQty_ByItemNo_Delivery(lblBOMitemNo.Text)
                If dtPO.Rows.Count > 0 Then
                    lblPOqty.Text = dtRowsFormat.FormatString(dtPO, PMDN.SumPurchaseQty)
                    If lblPOqty.Text <> String.Empty Then
                        Dim dblNumberPOqty As Double = CDbl(lblPOqty.Text)
                        lblPOqty.Text = String.Format("{0:n3}", dblNumberPOqty)
                    End If
                End If
            End If
            If lblWH.Text <> String.Empty Then
                Dim dtWH As DataTable = INAA.GetWarehouseFind_Table(lblWH.Text)
                If dtWH.Rows.Count > 0 Then
                    lblWH.Text = dtRowsFormat.FormatSumString(dtWH, INAA.WharehouseID, INAA.Warehouse)
                End If
            End If
            If MO_DateFrom = String.Empty Then
                MO_DateFrom = Date.Today.ToString("yyyy/mm/dd")
            End If
            If MO_ToDate = String.Empty Then
                MO_ToDate = Date.Today.ToString("yyyy/mm/dd")
            End If
            If Not IsNothing(hplDetail) And Not IsDBNull(lblBOMitemNo.Text) Then
                Dim link As String = ""
                Dim Item As String = lblBOMitemNo.Text
                link = link & "&Item= " & Item
                link = link & "&Spec= " & lblBOMitemSpec.Text
                link = link & "&Desc= " & lblBOMitemName.Text
                link = link & "&WH= " & lblWH.Text
                link = link & "&Unit= " & lblStcokinUint.Text
                link = link & "&Stock= " & lblStock.Text
                link = link & "&DateFrom= " & MO_DateFrom
                link = link & "&DateTo= " & MO_ToDate
                hplDetail.NavigateUrl = "MoMatShortListPopup.aspx?height=150&width=350" & link
                hplDetail.Attributes.Add("title", Item)
            End If
        End If
    End Sub
End Class