Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.util.collections
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class PendingMO
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD order by MD001 "
            'ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", True, Conn_SQL.ERP_ConnectionString)

            'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            'ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            'SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            'ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)

            btExportExcel.Visible = False
        End If
    End Sub
    Private Sub DataBindShow()
        Dim WC As String = UsingWorkstation.getObject.SelectedValue
        Dim MOtype As String = UsingMO_Type.getObject.SelectedValue
        Dim Cust As String = tbCust.Text
        Dim SaleOrderType As String = UsingDocTypeSale.getObject.SelectedValue
        Dim SaleOrderNo As String = tbSaleNo.Text
        Dim SaleSeq As String = tbSaleSeq.Text
        Dim ProdutItemNo As String = tbCode.Text
        Dim Spec As String = tbSpec.Text
        Dim SelectDateType As String = ddlDateType.SelectedValue
        Dim fromDate As String = tbDateFrom.Text
        Dim toDate As String = tbDateTo.Text

        Dim where As String = String.Empty
        Dim WhereWC As String = String.Empty
        Dim wMOtype As String = String.Empty
        Dim wSaleOrderType As String = String.Empty
        Dim wSaleOrderNo As String = String.Empty
        Dim wCust As String = String.Empty
        Dim wProdutItemNo As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wMOPlanStartDate As String = String.Empty
        Dim wMOPlannedCompletionDate As String = String.Empty
        If WC = "0" Then
            ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Select Workstation!!');", True)
        Else
            WhereWC = " and " & SFCB.WorkStation & " In('" & [String].Join("','", WC) & "')"
            If MOtype <> "0" Then
                wMOtype = " and substr(" & SFCB.WONo & ",3,4) = '" & MOtype & "' "
            End If
            If SaleOrderType <> "0" Then
                wSaleOrderType = " and substr(" & XMDC.SaleOrderNo & ",3,4) = '" & SaleOrderType & "'"
            End If
            If SaleOrderNo <> "" And SaleSeq <> "" Then
                Dim WheeSaleNo As String = XMDA.CustomerId & " = '" & SaleOrderNo & "' "
                Dim WheeSaleSeq As String = XMDC.ItemSequence & " = '" & SaleSeq & "' "
                wSaleOrderNo = " and " & WheeSaleNo & " and " & WheeSaleSeq
            End If
            If Cust <> "" Then
                wCust = " and " & XMDA.CustomerId & " = '" & Cust & "'"
            End If
            If ProdutItemNo <> "" Then
                wProdutItemNo = " and " & SFAA.ProductItem & " = '" & ProdutItemNo & "'"
            End If
            If Spec <> "" Then
                wSpec = " and " & IMAAL.Specifaction & " Like '" & Spec & "%'"
            End If
            If fromDate <> "" And toDate <> "" Then
                If SelectDateType = "1" Then
                    Dim pFDate As String = " TO_DATE('" & [String].Join("','", fromDate) & "','yyyy/mm/dd')"
                    Dim pTDate As String = " TO_DATE('" & [String].Join("','", toDate) & "','yyyy/mm/dd')"
                    wMOPlanStartDate = " And " & SFCB.PlanStartDate & " BETWEEN " & fromDate & " And " & toDate
                ElseIf SelectDateType = "2" Then
                    Dim pFDate As String = " TO_DATE('" & [String].Join("','", fromDate) & "','yyyy/mm/dd')"
                    Dim pTDate As String = " TO_DATE('" & [String].Join("','", toDate) & "','yyyy/mm/dd')"
                    wMOPlannedCompletionDate = " And " & SFCB.PlannedCompletionDate & " BETWEEN " & fromDate & " And " & toDate
                End If
            End If
            where = WhereWC & wMOtype & wSaleOrderType & wSaleOrderNo & wCust & wProdutItemNo & wSpec & wMOPlanStartDate & wMOPlannedCompletionDate

            Dim SqlData As String = String.Empty
            SqlData = " select " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WIP & "," & SFCB.PlanStartDate & ", " &
    " " & ECAA.Workcenter & "," & SFAA.Status & ", " &
    " " & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
    " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & "," & SFCB.GoodTransferIn & " ," & SFCB.GoodTransferOut & ",  " &
    " " & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "," & SFCB.TransferInUnit & ", " &
    " " & XMDC.SaleOrderNo & "," & XMDA.CustomerId & " " &
    " from " & SFCB.tblMOprocessItem_SFCB & " " &
    " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFCB.WONo & " " &
    " left join " & SFBA.tblManufactureOrder_Body & " On " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
    " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " and " & XMDA.OrderType & "<>'5' " &
    " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
    " LEFT OUTER JOIN  " & ECAA.tblWorkcenter & " On  " & ECAA.WorkcenterID & "=" & SFCB.WorkStation & "  " &
    " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' " & where & "  " &
    " group by " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFCB.LineNo & "," & SFCB.OperationID & "," & SFCB.WIP & "," & SFCB.PlanStartDate & ", " &
    " " & ECAA.Workcenter & "," & SFAA.Status & ", " &
    " " & SFCB.PlanStartDate & "," & SFCB.PlannedCompletionDate & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
    " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & "," & SFCB.GoodTransferIn & " ," & SFCB.GoodTransferOut & ",  " &
    " " & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "," & SFCB.TransferInUnit & ", " &
    " " & XMDC.SaleOrderNo & "," & XMDA.CustomerId & " "
            Dim dtData As DataTable = GetDataOracleDate(SqlData)
            If dtData.Rows.Count > 0 Then
                gvShow.DataSource = dtData
                gvShow.DataBind()
                ucCountRow.RowCount = dtData.Rows.Count.ToString
            End If
        End If
    End Sub
    Private Shared Function GetDataOracleDate(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataOracleDate", "Sql", ex.Message)
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
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Call DataBindShow()
        'ucCountRow.RowCount = ControlForm.rowGridview(gvShow)
        ''lbCount.Text = ControlForm.rowGridview(gvShow)
        'btExportExcel.Visible = True
        'System.Threading.Thread.Sleep(1000)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
    End Sub

    Function returnFld(ByVal fldName As String, ByVal fldCall As String) As String
        Return ",CONVERT(varchar, floor(" & fldName & "/60)) as " & fldCall
    End Function

    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        ExportsUtility.ExportGridviewToMsExcel("WorkCenterStatus" & Session("UserName"), gvShow)
    End Sub

    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            'Dim lblMOstatus As Label = CType(e.Row.FindControl("lblMOstatus"), Label)
            '    If lblMOstatus.Text <> String.Empty Then
            '        lblMOstatus.Text = StatusT100.MO_Normal(lblMOstatus.Text)
            '    End If
            Dim lblWorkstation As Label = CType(e.Row.FindControl("lblWorkstation"), Label)
            Dim lblWorkstationName As Label = CType(e.Row.FindControl("lblWorkstationName"), Label)
            If lblWorkstation.Text <> String.Empty Then
                Dim dtWC As DataTable = ECAA.GetFindWorkcenterDetail_Table(lblWorkstation.Text)
                If dtWC.Rows.Count > 0 Then
                    lblWorkstationName.Text = dtRowsFormat.FormatString(dtWC, ECAA.Workcenter)
                End If
            End If
            Dim lblOp_Id As Label = CType(e.Row.FindControl("lblOp_Id"), Label)
            Dim lblOpreation As Label = CType(e.Row.FindControl("lblOpreation"), Label)
            If lblOp_Id.Text <> String.Empty Then
                Dim dtOp As DataTable = OOCQL.GetDataOperation(lblOp_Id.Text)
                If dtOp.Rows.Count > 0 Then
                    lblOpreation.Text = dtRowsFormat.FormatString(dtOp, OOCQL.Operation)
                End If
            End If
            Dim lblCustomer As Label = CType(e.Row.FindControl("lblCustomer"), Label)
            Dim lblCustomerName As Label = CType(e.Row.FindControl("lblCustomerName"), Label)
            Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(lblCustomer.Text)
            If dtCustName.Rows.Count > 0 Then
                lblCustomerName.Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerName)
            End If

            Dim lblMO As Label = CType(e.Row.FindControl("lblMO"), Label)
            Dim lblSeq As Label = CType(e.Row.FindControl("lblSeq"), Label)
            Dim lblPlanQty As Label = CType(e.Row.FindControl("lblPlanQty"), Label)
            Dim wherePlan As String = String.Empty
            Dim whereWC As String = lblWorkstation.Text.Replace("WC", "W")
            Dim sMO As String = lblMO.Text.Replace("JP", "")
            Dim xMO = sMO.Split("-")
            Dim whereMo As String = xMO(1)
            Dim whereMoType As String = xMO(0)
            Dim whereOp As String = lblOp_Id.Text
            Dim whereSeq As Integer = Integer.Parse(lblSeq.Text)
            wherePlan = " TA001='" & whereMoType & "' and TA002='" & whereMo & "' and TA006='" & whereWC & "' " &
                " And TA003='" & whereSeq.ToString("0000") & "' and TA004='" & whereOp & "' "
            Dim SqlDataMIS As String = " select PlanQty from PlanSchedule " &
                " where " & wherePlan & " "
            Dim dtPlanQty As DataTable = GetDataSQLserverMIS(SqlDataMIS)
            If dtPlanQty.Rows.Count > 0 Then
                lblPlanQty.Text = dtRowsFormat.FormatString(dtPlanQty, "PlanQty")
            End If
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call DataBindShow()
    End Sub
End Class