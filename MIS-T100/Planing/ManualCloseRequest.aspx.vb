Imports System.Globalization
Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Data.OracleClient
Imports System.Data.SqlClient

Public Class ManualCloseRequest
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            'Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('51','52')  order by MQ002"
            'ControlForm.showDDL(ddlMoType, SQL, "MQ002", "MQ001", False, Conn_SQL.ERP_ConnectionString)

            btExport.Visible = False
            HeaderForm1.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
        End If
    End Sub

    Protected Sub btShow_Click(sender As Object, e As EventArgs) Handles btShow.Click
        Dim where As String = String.Empty
        If tbMoNo.Text = "" Then
            show_message.ShowMessage(Page, "MO No is null!!", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        Else
            If UsingMO_Type.getObject.SelectedValue <> "0" Then
                Dim pMOtype As String = UsingMO_Type.getObject.SelectedValue
                Dim MoType As String = "substr(" & SFBA.MODocNo & ",3,4) ='" & [String].Join("','", pMOtype) & "' "
                Dim Mo As String = "substr(" & SFBA.MODocNo & ",8,11) ='" & [String].Join("','", tbMoNo.Text) & "' "
                where = MoType & " and " & Mo
                Dim SqlData As String = String.Empty
                SqlData = " select " & SFBA.MODocNo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFAA.Status & "," & SFCA.ProductionQty & ", " &
    " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
    " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & ",  " &
    " " & "substr(" & SFBA.MODocNo & ",3,4)" & " as Motype," & "substr(" & SFBA.MODocNo & ",8,11)" & " as MO, " &
    " " & SFBA.ItemSequence & "," & SFBA.BOMitem & " " &
    " from " & SFBA.tblManufactureOrder_Body & " " &
    " left join " & SFAA.tblMO & " On " & SFAA.DocNo & "=" & SFBA.MODocNo & " " &
    " left join " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & "=" & SFAA.DocNo & " " &
    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " and " & XMDA.OrderType & "<>'5' " &
    " LEFT OUTER JOIN  " & IMAAL.tblProductionDetail & " On  " & IMAAL.ProductItem & "=" & SFAA.ProductItem & "  " &
    " where " & SFBA.wStandard & " and " & where & "  " &
    " group by " & SFBA.MODocNo & "," & SFAA.ProductItem & "," & SFAA.ProductionQty & ", " &
    " " & SFAA.Status & "," & SFCA.ProductionQty & ", " &
    " " & IMAAL.ProductName & "," & IMAAL.Specifaction & ",  " &
    " " & SFAA.ProductionQty & "," & SFCA.CompletedQty & "," & SFAA.ScarpQty & ", " &
    " " & SFBA.ItemSequence & "," & SFBA.BOMitem & " "
                Dim dtData As DataTable = GetDataOracleDate(SqlData)
                If dtData.Rows.Count > 0 Then
                    gvShow.DataSource = dtData
                    gvShow.DataBind()
                    CountRow1.RowCount = dtData.Rows.Count.ToString
                    btExport.Visible = True
                End If

            End If
        End If
        'System.Threading.Thread.Sleep(1000)
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
    Sub genTemp(tempTable As String)
        'CreateTempTable.createTempMOCheckMatReturn(tempTable)
        ''generate temp
        'Dim SQL As String = "",
        '    WHR As String = "",
        '    USQL As String = "",
        '    moType As String = ddlMoType.Text.Trim,
        '    moNo As String = tbMoNo.Text.Trim

        ''get mo data & BOM
        'SQL = "select TA001,TA002,TB003,MD006 from MOCTB left join MOCTA on TA001=TB001 and TA002=TB002 left join BOMMD on MD001=TA006 and MD003=TB003 where TB001='" & moType & "' and TB002='" & moNo & "' "
        'SQL = "insert into " & Conn_SQL.DBReport & ".." & tempTable & "(moType,moNo,item,qpaQty)" & SQL
        'Conn_SQL.Exec_Sql(SQL, Conn_SQL.ERP_ConnectionString)

        'SQL = "select * from " & tempTable
        'Dim Program As New DataTable
        'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        'If Program.Rows.Count > 0 Then
        '    'get data from mat issue & return
        '    SQL = "select TE004,sum(case when MQ003='54' then TE005 else 0 end) mat_issue,sum(case when MQ003='56' then TE005 else 0 end) mat_return from MOCTE left join CMSMQ on MQ001=TE001 where TE011='" & moType & "' and TE012='" & moNo & "' group by TE004"
        '    Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
        '    For i As Integer = 0 To Program.Rows.Count - 1
        '        With Program.Rows(i)
        '            USQL = " update " & tempTable & " set issueQty=" & .Item("mat_issue") & ",returnQty=" & .Item("mat_return") & " where moType='" & moType & "'and moNo='" & moNo & "' and item='" & .Item("TE004") & "'"
        '            Conn_SQL.Exec_Sql(USQL, Conn_SQL.MIS_ConnectionString)
        '        End With
        '    Next
        'Else
        '    SQL = "select TA001,TA002 from MOCTA where TA001='" & moType & "' and TA002='" & moNo & "' "
        '    SQL = "insert into " & Conn_SQL.DBReport & ".." & tempTable & "(moType,moNo)" & SQL
        '    Conn_SQL.Exec_Sql(SQL, Conn_SQL.ERP_ConnectionString)
        'End If

    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        If tbMoNo.Text = "" Then
            show_message.ShowMessage(Page, "MO No is null!!", UpdatePanel1)
            tbMoNo.Focus()
            Exit Sub
        End If
        If ddlReason.Text = "3" And tbReason.Text.Trim = "" Then
            show_message.ShowMessage(Page, "Reason for other is empty", UpdatePanel1)
            tbReason.Focus()
            Exit Sub
        End If
        Dim reason As String = ""
        Select Case ddlReason.Text.Trim
            Case "1"
                reason = "Order Cancle"
            Case "2"
                reason = "ECN Change"
            Case Else
                reason = tbReason.Text.Trim.Replace(",", "")
        End Select
        Dim tempTable As String = "tempManulaClose" & Session("UserName")
        genTemp(tempTable)

        Dim filePrint As String = "ManualCloseRequest.rpt",
           moType As String = UsingMO_Type.getObject.SelectedValue,
           moNo As String = tbMoNo.Text.Trim
        If ddlPage.Text = "A5" Then
            filePrint = "ManualCloseRequestHalf.rpt"
        End If
        Dim paraName As String = "table:" & tempTable & ",moType:" & moType & ",moNo:" & moNo & ",Reason:" & reason
        Dim rnd As New Random
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('fromrpt/ShowCrystalReportPlan.aspx?dbName=ERP&ReportName=" & filePrint & "&paraName=" & Server.UrlEncode(paraName) & "&encode=1&RID=" & rnd.Next(1, 100) & "');", True)
    End Sub

    Protected Sub btExport_Click(sender As Object, e As EventArgs) Handles btExport.Click
        ExportsUtility.ExportGridviewToMsExcel("ManualCloseRequest" & Session("UserName"), gvShow)
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File 
    End Sub

    Private Sub gvShow_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvShow.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblChildItemNo As Label = CType(e.Row.FindControl("lblChildItemNo"), Label)
            Dim lblChildItemSpec As Label = CType(e.Row.FindControl("lblChildItemSpec"), Label)
            If lblChildItemNo.Text <> String.Empty Then
                Dim dtItem As DataTable = IMAAL.GetDataProducItem(lblChildItemNo.Text)
                If dtItem.Rows.Count > 0 Then
                    lblChildItemSpec.Text = dtRowsFormat.FormatString(dtItem, IMAAL.Specifaction)
                End If
            End If

            Dim lblMOtype As Label = CType(e.Row.FindControl("lblMOtype"), Label)
            Dim lblMOno As Label = CType(e.Row.FindControl("lblMOno"), Label)
            Dim lblPlanQty As Label = CType(e.Row.FindControl("lblPlanQty"), Label)
            Dim wherePlan As String = String.Empty
            Dim whereMo As String = lblMOno.Text
            Dim whereMoType As String = lblMOtype.Text
            wherePlan = " TA001='" & whereMoType & "' and TA002='" & whereMo & "' "
            Dim SqlDataMIS As String = " select PlanQty from PlanSchedule  where " & wherePlan & " "
            Dim dtPlanQty As DataTable = GetDataSQLserverMIS(SqlDataMIS)
            If dtPlanQty.Rows.Count > 0 Then
                lblPlanQty.Text = dtRowsFormat.FormatString(dtPlanQty, "PlanQty")
            End If
        End If
    End Sub
End Class