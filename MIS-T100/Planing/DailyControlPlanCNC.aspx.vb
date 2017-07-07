Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class DailyControlPlanCNC
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTempTable As New CreateTempTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            '    Dim SQL As String = "select MD001,MD001+' : '+MD002 as MD002 from CMSMD where MD001 in (" & wcList & ") order by MD001 "
            '    ControlForm.showDDL(ddlWC, SQL, "MD002", "MD001", False, Conn_SQL.ERP_ConnectionString)
            '    SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52') order by MQ002"
            '    'ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            '    ControlForm.showCheckboxList(cblMoType, SQL, "MQ002", "MQ001", 4, Conn_SQL.ERP_ConnectionString)
            '    SQL = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003='22' order by MQ002 "
            '    ControlForm.showDDL(ddlSaleType, SQL, "MQ002", "MQ001", True, Conn_SQL.ERP_ConnectionString)
            btExportExcel.Visible = False
        End If
    End Sub
    Private Sub DataBindShow()
        Dim WC As String = UsingWorkstation.getObject.SelectedValue
        Dim SaleType As String = UsingDocTypeSale.getObject.SelectedValue
        Dim where As String = String.Empty
        Dim wMOtype As String = String.Empty
        Dim wCust As String = String.Empty
        Dim wItemCode As String = String.Empty
        Dim wSpec As String = String.Empty
        Dim wPlanStartDate As String = String.Empty
        Dim wTransferDate As String = String.Empty
        Dim wPlanDate As String = String.Empty
        Dim wSaleType As String = String.Empty

        Dim SelectWhereMOtype As String = SelectCheckBoxList.MultipleSelect(UsingMOTypeCheckList.getObject)
        Dim SelectMOType As Integer = SelectCheckBoxList.RowNumSelect
        If WC <> "0" Then
            Dim Wc_erp As String = WC.Replace("WC", "W")
            Dim SqlDataMC As String = "SELECT wc,capacity,mancapacity from MachineCapacity where  wc='" & Wc_erp & "'  "
            Dim dtMcLoading As DataTable = GetDataSQLserverMIS(SqlDataMC)
            If dtMcLoading.Rows.Count > 0 Then
                lbCapa.Text = dtRowsFormat.FormatString(dtMcLoading, "capacity")
            End If

            Dim whereWC As String = " and " & SFCB.WorkStation & "='" & [String].Join("','", WC) & "'"
            If (SelectMOType > 0) Then
                wMOtype = " and substr(" & SFCB.WONo & ",3,4 ) in(" & [String].Join("','", SelectWhereMOtype) & "')"
            Else
                wMOtype = String.Empty
            End If
            If tbCust.Text <> String.Empty Then
                wCust = " and " & XMDA.CustomerId & "='" & [String].Join("','", tbCust.Text) & "'"
            Else
                wCust = String.Empty
            End If
            If tbCode.Text <> String.Empty Then
                wItemCode = " and " & SFAA.ProductItem & " like '" & [String].Join("','", tbCode.Text) & "%'"
            Else
                wItemCode = String.Empty
            End If
            If tbSpec.Text <> String.Empty Then
                wSpec = " and " & IMAAL.Specifaction & " like '" & [String].Join("','", tbSpec.Text) & "%'"
            Else
                wSpec = String.Empty
            End If
            If fromDate.Text <> String.Empty AndAlso ToDate.Text <> String.Empty Then
                If ddlDate.SelectedValue = "1" Then
                    wPlanStartDate = " and " & SFAA.PlanStartDate & " BETWEEN TO_DATE('" & [String].Join("','", fromDate.Text) & "','yyyy/mm/dd') and  TO_DATE('" & [String].Join("','", ToDate.Text) & "','yyyy/mm/dd')"
                ElseIf ddlDate.SelectedValue = "2" Then
                    wTransferDate = " and " & SFFB.DocumentDate & " BETWEEN TO_DATE('" & [String].Join("','", fromDate.Text) & "','yyyy/mm/dd') and  TO_DATE('" & [String].Join("','", ToDate.Text) & "','yyyy/mm/dd')"
                End If
                wPlanStartDate = String.Empty
                wTransferDate = String.Empty
            End If
            If PlanDate.Text <> String.Empty Then
                wPlanDate = " and " & SFAA.PlanStartDate & "= TO_DATE('" & [String].Join("','", PlanDate.Text) & "','yyyy/mm/dd')"
            Else
                wPlanDate = String.Empty
            End If
            If SaleType <> "0" Then
                wSaleType = " and substr(" & XMDA.SaleOrderNo & ",3,4 ) ='" & [String].Join("','", SaleType) & "'"
            Else
                wSaleType = String.Empty
            End If

            where = whereWC & wMOtype & wCust & wItemCode & wSpec & wPlanStartDate & wTransferDate & wPlanDate & wSaleType

            Dim SqlData As String = String.Empty
                SqlData = " select " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
            " " & SFCB.LineNo & "," & SFCB.OperationID & "," & XMDA.SaleOrderNo & "," & XMDA.CustomerId & "," & XMDA.OrderType & ", " &
            " " & SFCB.WIP & "," & SFCB.PlanStartDate & "  " &
            " from " & SFCB.tblMOprocessItem_SFCB & " " &
            " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFCB.WONo & " " &
            " left join " & SFBA.tblManufactureOrder_Body & " On " & SFBA.MODocNo & "=" & SFAA.DocNo & " " &
        " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
        " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " and " & XMDA.OrderType & "<>'5' " &
        " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
        " LEFT JOIN " & SFFB.tblTransferHead & "  On " & SFFB.WONo & "=" & SFCB.WONo & " " &
" And " & SFFB.OperationSequence & "=" & SFCB.OperationSeq & " And  " & SFFB.Workstation & "=" & SFCB.WorkStation & " " &
" And " & SFFB.RunCard & "=" & SFCB.RunCard & "  " &
            " where " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' " & where & "  " &
            " group by " & SFCB.WorkStation & " ," & SFCB.WONo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
            " " & SFCB.LineNo & "," & SFCB.OperationID & "," & XMDA.SaleOrderNo & "," & XMDA.CustomerId & "," & XMDA.OrderType & "," &
            " " & SFCB.WIP & "," & SFCB.PlanStartDate & "  "
                Dim dtData As DataTable = GetDataOracleDate(SqlData)
                If dtData.Rows.Count > 0 Then
                    gvShow.DataSource = dtData
                    gvShow.DataBind()
                    lbCount.Text = dtData.Rows.Count.ToString
                Else
                    gvShow.DataSource = New List(Of String)
                    gvShow.DataBind()
                End If
            End If
    End Sub
    Protected Sub btShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShow.Click
        Call DataBindShow()
        'lbCount.Text = ControlForm.rowGridview(gvShow)
        'btExportExcel.Visible = True
        'System.Threading.Thread.Sleep(1000)
    End Sub
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
    Protected Sub btExportExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExportExcel.Click
        ExportsUtility.ExportGridviewToMsExcel("DailyControlPlan" & Session("UserName"), gvShow)
    End Sub
    Protected Sub gvShow_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvShow.PageIndexChanging
        gvShow.PageIndex = e.NewPageIndex
        Call DataBindShow()
    End Sub
    Private Sub gvShow_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
            Dim lblWC As Label = CType(e.Row.FindControl("lblWC"), Label)
            Dim lblMO As Label = CType(e.Row.FindControl("lblMO"), Label)
            Dim lblItemSeq As Label = CType(e.Row.FindControl("lblItemSeq"), Label)
            Dim lblProdcitionItemNo As Label = CType(e.Row.FindControl("lblProdcitionItemNo"), Label)
            Dim lblSpec As Label = CType(e.Row.FindControl("lblSpec"), Label)
            Dim lblOperation As Label = CType(e.Row.FindControl("lblOperation"), Label)
            Dim lblCustId As Label = CType(e.Row.FindControl("lblCustId"), Label)
            Dim lblCustomerName As Label = CType(e.Row.FindControl("lblCustomerName"), Label)
            Dim lblSO_OrderType As Label = CType(e.Row.FindControl("lblSO_OrderType"), Label)
            Dim lblPlanDate As Label = CType(e.Row.FindControl("lblPlanDate"), Label)
            Dim lblPlanQty As Label = CType(e.Row.FindControl("lblPlanQty"), Label)

            Dim wherePlan As String = String.Empty
            Dim sMO As String = lblMO.Text.Replace("JP", "")
            Dim xMO = sMO.Split("-")
            Dim MoType As String = xMO(0)
            Dim MoNo As String = xMO(1)
            Dim sOP As String = lblOperation.Text
            Dim Seq As Integer = CInt(lblItemSeq.Text)
            Dim sWC As String = lblWC.Text.Replace("WC", "W")
            'Dim sDate As Date = CDate()
            wherePlan = " TA006='" & sWC & "' and TA004='" & sOP & "' and TA003='" & Seq.ToString("0000") & "' " &
                " and TA001='" & MoType & "' and TA002='" & MoNo & "' "
            Dim SqlPlanQty As String = String.Empty
            SqlPlanQty = "Select PlanDate,substring(PlanDate,1,4) as yy,substring(PlanDate,5,2) as mm, " &
                "substring(PlanDate,7,2) as dd,PlanQty from PlanSchedule where " & wherePlan & " "
            Dim dtPlan As DataTable = GetDataSQLserverMIS(SqlPlanQty)
            If dtPlan.Rows.Count > 0 Then
                Dim yy As String = dtRowsFormat.FormatString(dtPlan, "yy")
                Dim mm As String = dtRowsFormat.FormatString(dtPlan, "mm")
                Dim dd As String = dtRowsFormat.FormatString(dtPlan, "dd")
                lblPlanDate.Text = yy & "/" & mm & "/" & dd
                lblPlanQty.Text = dtRowsFormat.FormatString(dtPlan, "PlanQty")
            End If
            If lblWC.Text <> String.Empty Then
                Dim dtWcName As DataTable = ECAA.GetFindWorkcenterDetail_Table(lblWC.Text)
                If dtWcName.Rows.Count > 0 Then
                    lblWC.Text = dtRowsFormat.FormatSumString(dtWcName, ECAA.WorkcenterID, ECAA.Workcenter)
                End If
            End If
            If lblOperation.Text <> String.Empty Then
                Dim dtOpName As DataTable = OOCQL.GetDataOperation(lblOperation.Text)
                If dtOpName.Rows.Count > 0 Then
                    lblOperation.Text = dtRowsFormat.FormatSumString(dtOpName, OOCQL.OperationID, OOCQL.Operation)
                End If
            End If
            If lblCustId.Text <> String.Empty Then
                Dim dtCustName As DataTable = PMAAL.GetDataCustomerDetail(lblCustId.Text)
                If dtCustName.Rows.Count > 0 Then
                    lblCustomerName.Text = dtRowsFormat.FormatString(dtCustName, PMAAL.CustomerName)
                End If
            End If
            If lblSO_OrderType.Text <> String.Empty Then

            End If
            If lblProdcitionItemNo.Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(lblProdcitionItemNo.Text)
                If dtItem.Rows.Count > 0 Then
                    lblSpec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
            End If
            If lblSO_OrderType.Text <> String.Empty Then
                If lblSO_OrderType.Text = "1" Then
                    lblSO_OrderType.Text = "1:GENERAL SO"
                ElseIf lblSO_OrderType.Text = "2" Then
                    lblSO_OrderType.Text = "2:EXCHANGE SO"
                ElseIf lblSO_OrderType.Text = "3" Then
                    lblSO_OrderType.Text = "3:RECEPTION ORDER"
                ElseIf lblSO_OrderType.Text = "4" Then
                    lblSO_OrderType.Text = "4:TRANSFER ORDER"
                ElseIf lblSO_OrderType.Text = "5" Then
                    lblSO_OrderType.Text = "5:ADVANCED ORDER"
                End If
            End If
            ' Dim lblCustomerName As Label = CType(e.Row.FindControl(""), Label)


            '        Dim mchTime As Integer = 0
            '        If Integer.TryParse(.DataItem("AP100(Minute)").ToString.Trim, mchTime) Then
            '            mchTime = mchTime
            '        End If
            '        If line = 0 Then
            '            bCapa = CInt(lbCapa.Text.Trim)
            '            If tbPlanDate.Text <> "" Then
            '                planStard = DateTime.ParseExact(configDate.dateFormat2(tbPlanDate.Text.Trim), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
            '            End If
            '        End If
            '        bCapa = bCapa - mchTime
            '        line = line + 1
            '        e.Row.Cells(14).Text = planStard.ToString("dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture) 'bCapa & "," &
            '        If bCapa < 0 Then
            '            Dim xCapa As Integer = CInt(lbCapa.Text.Trim)
            '            Dim lastCapa As Integer = Math.Abs(bCapa)
            '            Dim modDate As Integer = lastCapa Mod xCapa
            '            Dim cntDate As Integer = (lastCapa - modDate) / xCapa
            '            If modDate > 0 Then
            '                cntDate = cntDate + 1
            '            End If
            '            planStard = planStard.AddDays(cntDate)
            '            bCapa = CInt(lbCapa.Text.Trim) - modDate
            '        End If
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub
End Class