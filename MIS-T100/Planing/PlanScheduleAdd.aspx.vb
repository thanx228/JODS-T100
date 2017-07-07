Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Web.HttpException
Public Class PlanScheduleAdd
    Inherits System.Web.UI.Page
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim CreateTempTable As New CreateTempTable

    Dim dt As DataTable = New DataTable
    Dim table As String = "PlanSchedule"
    Dim table2 As String = "MoBalance"
    Dim dateToday As String = Date.Now.ToString("yyyyMMdd", New CultureInfo("en-US"))
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Call ShowPlanScheduleSQLserver()
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            lbWc.Text = Request.QueryString("WC").ToString.Trim
            Dim pDate As String = Request.QueryString("PlanDate").ToString.Trim
            Dim ddd As Date = CDate(pDate)
            lbPlanDate.Text = ddd.ToString("yyyy/MM/dd")
            '    ' Dim nameHead As String = ControlForm.nameHeader("/Planing/PlanScheduleList.aspx") & "(Record)"
            '    ' ucHeaderForm.HeaderLable = ControlForm.nameHeader("/Planing/PlanScheduleList.aspx") & "(Record)"
            '    'ucHeaderForm2.HeaderLable = nameHead
            '    'Page.Title = "T100 REPORT->" & nameHead
            Call ShowPlanScheduleSQLserver()
            Call showButton()
        End If
    End Sub
    Private Sub ShowPlanScheduleSQLserver()
        Dim pDate = lbPlanDate.Text.Split("/")
        Dim yy As String = pDate(0)
        Dim mm As String = pDate(1)
        Dim dd As String = pDate(2)
        Dim PlanDate As String = yy & mm & dd
        Dim WC As String = lbWc.Text.Replace("WC", "W")
        Dim wherePlanSchedule As String = String.Empty
        wherePlanSchedule = "PlanDate = '" & PlanDate & "' AND TA006 ='" & WC & "'"
        Dim SqlPlanSch As String = " select PlanDate,TA001,TA002,'JP'+TA001+'-'+TA002 as MO_No,ap100,TA006 as WC,Cancled, " &
    " PlanSeq,PlanQty,Mch,Urgent,TA003 as Seq  from PlanSchedule where " & wherePlanSchedule & " "
        Dim dtPlanSchedule As DataTable = GetDataSQLserver(SqlPlanSch)
        If dtPlanSchedule.Rows.Count > 0 Then
            gvSelect.DataSource = dtPlanSchedule
            gvSelect.DataBind()
            Call SumStdTimeT100()
            MultiView1.SetActiveView(View1)
        Else
            MultiView1.SetActiveView(View2)
        End If
    End Sub
    'Protected Sub chkboxCancel_CheckedChanged(sender As Object, e As EventArgs)
    '    'Dim ChkBoxHeader As CheckBox = CType(gvShow.HeaderRow.FindControl("chkAllSelect"), CheckBox)
    '    For Each row As GridViewRow In gvShow.Rows
    '        Dim lblCkCencel As Label = CType(row.FindControl("lblCkCencel"), Label)
    '        Dim cbCancel As CheckBox = CType(row.FindControl("cbCancel"), CheckBox)
    '        If cbCancel.Checked = True Then
    '            'cbCancel.Checked = True
    '            lblCkCencel.Text = "1"
    '            row.BackColor = System.Drawing.Color.Gray
    '        Else
    '            ' cbCancel.Checked = False
    '            lblCkCencel.Text = "0"
    '            row.BackColor = System.Drawing.Color.White
    '        End If
    '    Next
    'End Sub
    Private Sub gvSelect_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSelect.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim lblMO_No As Label = CType(e.Row.FindControl("lblMO_No"), Label)
            Dim lblErpMO_Seq As Label = CType(e.Row.FindControl("lblErpMO_Seq"), Label)
            Dim lblAp100 As Label = CType(e.Row.FindControl("lblAp100"), Label)
            Dim lblWC As Label = CType(e.Row.FindControl("lblWC"), Label)
            Dim lblPlanDate As Label = CType(e.Row.FindControl("lblPlanDate"), Label)
            Dim lblStdLaborTime As Label = CType(e.Row.FindControl("lblStdLaborTime"), Label)
            Dim lblStdMcTime As Label = CType(e.Row.FindControl("lblStdMcTime"), Label)
            Dim lblCustomer As Label = CType(e.Row.FindControl("lblCustomer"), Label)
            Dim lblProductionItemNo As Label = CType(e.Row.FindControl("lblProductionItemNo"), Label)
            Dim lblProductionItemName As Label = CType(e.Row.FindControl("lblProductionItemName"), Label)
            Dim lblSpecifaction As Label = CType(e.Row.FindControl("lblSpecifaction"), Label)
            Dim lblProductionQty As Label = CType(e.Row.FindControl("lblProductionQty"), Label)
            Dim lblCompleteQty As Label = CType(e.Row.FindControl("lblCompleteQty"), Label)
            Dim lblScrapQty As Label = CType(e.Row.FindControl("lblScrapQty"), Label)
            Dim lblUnit As Label = CType(e.Row.FindControl("lblUnit"), Label)
            Dim lblSaleOrderNo As Label = CType(e.Row.FindControl("lblSaleOrderNo"), Label)
            Dim lblGoodTransferIn As Label = CType(e.Row.FindControl("lblGoodTransferIn"), Label)
            Dim lblWIP As Label = CType(e.Row.FindControl("lblWIP"), Label)
            Dim lblGoodTransferOut As Label = CType(e.Row.FindControl("lblGoodTransferOut"), Label)
            Dim lblRewrokTrsin As Label = CType(e.Row.FindControl("lblRewrokTrsin"), Label)
            Dim lblRewrokTrsOut As Label = CType(e.Row.FindControl("lblRewrokTrsOut"), Label)
            Dim lblDirectScarp As Label = CType(e.Row.FindControl("lblDirectScarp"), Label)

            Dim lblCkCencel As Label = CType(e.Row.FindControl("lblCkCencel"), Label)
            Dim cbCancel As CheckBox = CType(e.Row.FindControl("cbCancel"), CheckBox)
            If lblCkCencel.Text = "1" Then
                cbCancel.Checked = True
                e.Row.BackColor = System.Drawing.Color.Gray
            Else
                cbCancel.Checked = False
            End If
            Dim lblCkUrgent As Label = CType(e.Row.FindControl("lblCkUrgent"), Label)
            Dim cbUrgent As CheckBox = CType(e.Row.FindControl("cbUrgent"), CheckBox)
            If lblCkUrgent.Text = "1" Then
                cbUrgent.Checked = True
            Else
                cbUrgent.Checked = False
            End If
            Dim iSeqPlanERP As Integer = 0
            If lblErpMO_Seq.Text <> String.Empty Then
                iSeqPlanERP = CInt(lblErpMO_Seq.Text)
            End If
            'Dim lblWC As Label = CType(e.Row.FindControl("lblWC"), Label)
            lblWC.Text = lblWC.Text.Replace("W", "WC")
            Dim WC As String = lblWC.Text
            Dim pWoNo = lblMO_No.Text.Split(" ")
            Dim pWC = WC.Split(" ")
            Dim WONO100 As String = SFCB.WONo & "= '" & [String].Join("','", pWoNo(0)) & "'"
            Dim WC100 As String = SFCB.WorkStation & "= '" & [String].Join("','", pWC(0)) & "'"
            Dim Seq100 As String = SFCB.LineNo & "= '" & [String].Join("','", iSeqPlanERP) & "'"
            Dim whereOracleProcess As String = String.Empty
            whereOracleProcess = WONO100 & " AND " & WC100 & " AND " & Seq100
            Dim SqlProcess As String = " select " & SFCB.LineNo & "," & SFCB.OperationID & ", " &
        " " & SFCB.StandradLaborHours2 & "," & SFCB.StandradMachineHours2 & "," & XMDA.CustomerId & "," & SFAA.ProductItem & ", " &
        " " & IMAAL.ProductName & "," & IMAAL.Specifaction & "," & SFAA.ProductionQty & ", " &
        " " & SFAA.ScarpQty & "," & SFAA.Unit & "," & SFCA.CompletedQty & "," & XMDA.SaleOrderNo & "," & SFCB.GoodTransferIn & ",  " &
        " " & SFCB.WIP & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & "," & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "  " &
        " from " & SFCB.tblMOprocessItem_SFCB & " " &
        " LEFT JOIN " & SFAA.tblMO & " ON " & SFAA.DocNo & "=" & SFCB.WONo & " " &
        " LEFT JOIN " & SFCA.tblMO_Detail & " ON " & SFCA.DocNo & "=" & SFCB.WONo & " " &
        " AND " & SFCA.RunCardNo & "=" & SFCB.RunCard & " " &
        " LEFT JOIN " & XMDC.tblSaleItem & " ON " & XMDC.Item & "=" & SFAA.ProductItem & " " &
        " LEFT JOIN " & XMDA.tblSaleHead & " ON " & XMDA.SaleOrderNo & "=" & XMDC.SaleOrderNo & " " &
        " LEFT JOIN  " & IMAAL.tblProductionDetail & " On " & IMAAL.ProductItem & "=" & SFAA.ProductItem & " " &
       "where  " & SFCB.RunCard & "='0' AND " & whereOracleProcess & " "

            Dim dtOracleProcess As DataTable = GetDataOracleBase(SqlProcess)
            If dtOracleProcess.Rows.Count > 0 Then
                lblStdLaborTime.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.StandradLaborHours2)
                lblStdMcTime.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.StandradMachineHours2)
                lblCustomer.Text = dtRowsFormat.FormatString(dtOracleProcess, XMDA.CustomerId)
                lblProductionItemNo.Text = dtRowsFormat.FormatString(dtOracleProcess, SFAA.ProductItem)
                lblProductionItemName.Text = dtRowsFormat.FormatString(dtOracleProcess, IMAAL.ProductName)
                lblSpecifaction.Text = dtRowsFormat.FormatString(dtOracleProcess, IMAAL.Specifaction)
                lblProductionQty.Text = dtRowsFormat.FormatString(dtOracleProcess, SFAA.ProductionQty)
                If lblProductionQty.Text <> String.Empty Then
                    Dim dblNumberProductionQty As Double = CDbl(lblProductionQty.Text)
                    lblProductionQty.Text = String.Format("{0:n3}", dblNumberProductionQty)
                End If
                lblCompleteQty.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCA.CompletedQty)
                If lblCompleteQty.Text <> String.Empty Then
                    Dim dblNumberCompleteQty As Double = CDbl(lblCompleteQty.Text)
                    lblCompleteQty.Text = String.Format("{0:n3}", dblNumberCompleteQty)
                End If
                lblScrapQty.Text = dtRowsFormat.FormatString(dtOracleProcess, SFAA.ScarpQty)
                If lblScrapQty.Text <> String.Empty Then
                    Dim dblNumberScrapQty As Double = CDbl(lblScrapQty.Text)
                    lblScrapQty.Text = String.Format("{0:n3}", dblNumberScrapQty)
                End If
                lblUnit.Text = dtRowsFormat.FormatString(dtOracleProcess, SFAA.Unit)
                lblSaleOrderNo.Text = dtRowsFormat.FormatString(dtOracleProcess, XMDA.SaleOrderNo)

                lblGoodTransferIn.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.GoodTransferIn)
                If lblGoodTransferIn.Text <> String.Empty Then
                    Dim dblNumberGoodTransferIn As Double = CDbl(lblGoodTransferIn.Text)
                    lblGoodTransferIn.Text = String.Format("{0:n3}", dblNumberGoodTransferIn)
                End If
                lblWIP.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.WIP)
                If lblWIP.Text <> String.Empty Then
                    Dim dblNumberWIP As Double = CDbl(lblWIP.Text)
                    lblWIP.Text = String.Format("{0:n3}", dblNumberWIP)
                End If
                lblGoodTransferOut.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.GoodTransferOut)
                If lblGoodTransferOut.Text <> String.Empty Then
                    Dim dblNumberGoodTransferOut As Double = CDbl(lblGoodTransferOut.Text)
                    lblGoodTransferOut.Text = String.Format("{0:n3}", dblNumberGoodTransferOut)
                End If
                lblRewrokTrsin.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.ReworkTrsIn)
                If lblRewrokTrsin.Text <> String.Empty Then
                    Dim dblNumberGoodTransferIn As Double = CDbl(lblRewrokTrsin.Text)
                    lblRewrokTrsin.Text = String.Format("{0:n3}", dblNumberGoodTransferIn)
                End If
                lblRewrokTrsOut.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.TransferOutforRework)
                If lblRewrokTrsOut.Text <> String.Empty Then
                    Dim dblNumberRewrokTrsOut As Double = CDbl(lblRewrokTrsOut.Text)
                    lblRewrokTrsOut.Text = String.Format("{0:n3}", dblNumberRewrokTrsOut)
                End If
                lblDirectScarp.Text = dtRowsFormat.FormatString(dtOracleProcess, SFCB.DirectScarp)
                If lblDirectScarp.Text <> String.Empty Then
                    Dim dblNumberDirectScarp As Double = CDbl(lblDirectScarp.Text)
                    lblDirectScarp.Text = String.Format("{0:n3}", dblNumberDirectScarp)
                End If
            End If
            Dim pMONo = lblMO_No.Text.Split(" ")
            Dim ImSendData As HyperLink = CType(e.Row.FindControl("hplSelect"), HyperLink)
            If Not IsNothing(ImSendData) Then
                Dim strUrl As String = "PlanScheduleAddPop.aspx?"
                Dim SendData As String = "MO=" & pMONo(0) & "&ProductionItem=" & lblProductionItemNo.Text & "  "
                ImSendData.NavigateUrl = "JavaScript:void(0);"
                ImSendData.NavigateUrl = strUrl & SendData
                ImSendData.Target = "_blank"
            End If
        End If
    End Sub
    Sub SumStdTimeT100()
        Dim tmpDecimalvalMan As Decimal = 0
        Dim tmpvalMC As Integer = 0
        Dim tmpvalAP As Integer = 0
        For i As Integer = 0 To gvSelect.Rows.Count - 1
            Dim lblCkCencel As Label = DirectCast(gvSelect.Rows(i).FindControl("lblCkCencel"), Label)
            If lblCkCencel.Text <> "1" Then
                Dim lblStdMcTime As Label = DirectCast(gvSelect.Rows(i).FindControl("lblStdMcTime"), Label)
                Dim lblStdLaborTime As Label = DirectCast(gvSelect.Rows(i).FindControl("lblStdLaborTime"), Label)
                Dim lblAp100 As Label = DirectCast(gvSelect.Rows(i).FindControl("lblAp100"), Label)
                If lblStdMcTime.Text <> String.Empty Then
                    tmpvalMC = tmpvalMC + Decimal.Parse(lblStdMcTime.Text)
                    lbUsageHourMch.Text = tmpvalMC.ToString()
                End If
                If lblAp100.Text <> String.Empty Then
                    tmpvalAP = tmpvalAP + Integer.Parse(lblAp100.Text)
                    lbAp100Mch.Text = tmpvalAP.ToString()
                    lbAp100Man.Text = tmpvalAP.ToString()
                End If
                If lblStdLaborTime.Text <> String.Empty Then
                    tmpDecimalvalMan = tmpDecimalvalMan + Decimal.Parse(lblStdLaborTime.Text)
                    lbUsageHourMan.Text = CInt(tmpDecimalvalMan.ToString())
                End If
            End If
        Next
        Dim WC As String = lbWc.Text.Replace("WC", "W")
        Dim SqlDataStdTimeT100 As String = " select sum(CAST(round([mancapacity],2) AS decimal(18,0))) as StdLaborTime, " &
    " sum(CAST(round([capacity],2) AS decimal(18,0))) as StdMcTime  " &
    " from MachineCapacity where  wc='" & WC & "'  "
        Dim dtProcessT100 As DataTable = GetDataDB100SQLserver(SqlDataStdTimeT100)
        If dtProcessT100.Rows.Count > 0 Then
            lbStdHourMchSum.Text = dtRowsFormat.FormatString(dtProcessT100, "StdMcTime")
            lbStdHourManSum.Text = dtRowsFormat.FormatString(dtProcessT100, "StdLaborTime")
        End If
        Dim dStdMcSum As Decimal = Decimal.Parse(lbStdHourMchSum.Text)
        Dim dStdLaborSum As Decimal = Decimal.Parse(lbStdHourManSum.Text)
        lbBalHourMch.Text = (dStdMcSum - CInt(lbUsageHourMch.Text)).ToString
        lbBalHourMan.Text = (dStdLaborSum - CInt(lbUsageHourMan.Text)).ToString
    End Sub
    Private Shared Function GetDataSQLserver(SqlSQLserver As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(SqlSQLserver, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataSQLserver", "SqlSQLserver", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared Function GetDataDB100SQLserver(SqlSQLserver As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(SqlSQLserver, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataSQLserver", "SqlSQLserver", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Private Shared Function GetDataOracleBase(SqlOracle As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(SqlOracle, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDatdOracleBase", "SqlOracle", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Sub showButton()
        Dim setVisible As Boolean = False
        setVisible = False
        If gvShow.Rows.Count > 0 Then
            setVisible = True
        End If
        btSelect.Visible = setVisible
        Dim planDate As String = lbPlanDate.Text.Trim
        '201510310800
        Dim dateToday1 As String = DateTime.Now.ToString("yyyyMMddHHmm", New CultureInfo("en-US"))
        '201508301540
        setVisible = False
        If planDate >= dateToday1 Then
            setVisible = True
        End If
        btSearch.Visible = setVisible
        btSave.Visible = setVisible
    End Sub
    Protected Sub btSearch2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSearch2.Click
        MultiView1.SetActiveView(View2)
    End Sub
    Protected Sub btSelect2_Click(sender As Object, e As EventArgs) Handles btSelect2.Click
        MultiView1.SetActiveView(View1)
    End Sub
    Protected Sub btBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBack.Click
        Response.Redirect("PlanScheduleList.aspx?")
    End Sub
    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Dim line As Integer = 0
        Dim whrHash As Hashtable = New Hashtable,
            fldInsHash As Hashtable = New Hashtable,
            planDate As String = configDate.dateFormat2(lbPlanDate.Text.Trim),
            wc As String = lbWc.Text.Trim,
            fldUpdHash As Hashtable = New Hashtable

        'whrHash.Add("PlanDate", planDate) 'Plan Date
        'whrHash.Add("TA006", wc) 'WC
        'delete all data before save it
        'Conn_SQL.Exec_Sql(Conn_SQL.getDeleteSql(table, whrHash), Conn_SQL.MIS_ConnectionString)

        For Each gvRow As GridViewRow In gvSelect.Rows
            With gvRow
                Dim cbCancled As CheckBox = .Cells(1).FindControl("cbCancled")
                Dim txtSet As TextBox = .Cells(2).FindControl("tbSetSeq")
                Dim cbUrgent As CheckBox = .Cells(3).FindControl("cbUrgent")
                Dim txtMch As TextBox = .Cells(4).FindControl("tbMch")
                Dim txtPlan As TextBox = .Cells(19).FindControl("tbPlanQty")
                Dim mo() As String = .Cells(10).Text.Trim.Split("-")
                line += 1
                whrHash = New Hashtable
                whrHash.Add("PlanDate", planDate) 'Plan Date
                whrHash.Add("TA006", wc) 'WC
                whrHash.Add("PlanSeq", line) 'PlanSeq
                'insert
                fldInsHash = New Hashtable
                Dim setSeq As Integer = 999
                If txtSet.Text.Trim <> "" And IsNumeric(txtSet.Text.Trim) Then
                    setSeq = CInt(txtSet.Text.Trim)
                End If
                fldInsHash.Add("PlanSeqSet", setSeq) 'PlanSeqSet
                fldInsHash.Add("TA001", mo(0)) 'MO Type
                fldInsHash.Add("TA002", mo(1)) 'MO No
                fldInsHash.Add("TA003", mo(2)) 'MO Seq
                fldInsHash.Add("TA004", .Cells(23).Text.Trim.Replace("&nbsp;", "")) 'Operation
                fldInsHash.Add("PlanedQty", CDec(.Cells(18).Text.Trim.Replace(",", ""))) 'PlanedQty
                Dim valUrgent As String = "0"
                If cbUrgent.Checked Then
                    valUrgent = "1"
                End If
                fldInsHash.Add("Urgent", valUrgent) 'urgent
                fldInsHash.Add("Mch", txtMch.Text.Trim) 'mch
                valUrgent = "0"
                If cbCancled.Checked Then
                    valUrgent = "1"
                End If
                fldInsHash.Add("Cancled", valUrgent) 'urgent
                Dim planQty As Decimal = CDec(If(.Cells(17).Text.Replace(",", "").Replace("&nbsp;", "") = "", 0, .Cells(17).Text.Replace(",", ""))) 'WIP Qty
                If txtPlan.Text.Trim <> "" And IsNumeric(txtPlan.Text.Trim) Then
                    planQty = CDec(txtPlan.Text.Trim)
                End If
                fldInsHash.Add("PlanQty", planQty) 'PlanQty
                fldInsHash.Add("tranNo", .Cells(5).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "").Replace("amp;", "").Replace("&", "").Replace("nbsp;", "")) 'Transfer Number  &amp;nbsp;
                fldInsHash.Add("ap100", CInt(.Cells(6).Text.Replace(",", ""))) 'AP100 Time

                fldInsHash.Add("lastProcessName", .Cells(25).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "").Replace("amp;", "").Replace("&", "").Replace("nbsp;", "")) 'last Process Name  &amp;nbsp;
                fldInsHash.Add("lastProcessPlanDate", configDate.dateFormat2(.Cells(26).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "").Replace("amp;", "").Replace("&", "").Replace("nbsp;", ""), "-")) 'last Process Plan Date &amp;nbsp;
                fldInsHash.Add("lastProcess", .Cells(27).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "").Replace("amp;", "").Replace("&", "").Replace("nbsp;", "")) 'last Process &amp;nbsp;

                Dim fldUser As String = "CreateBy",
                    fldDate As String = "CreateDate"
                Dim valChk As String = .Cells(28).Text.Trim.Replace("&amp;", "").Replace("&nbsp;", "").Replace("amp;", "").Replace("&", "").Replace("nbsp;", "")
                If valChk <> "" Then
                    fldUser = "ChangeBy"
                    fldDate = "ChangeDate"
                End If

                fldInsHash.Add(fldUser, Session("UserName"))
                fldInsHash.Add(fldDate, DateTime.Now.ToString("yyyyMMdd HHmmss"))

                Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table, fldInsHash, whrHash), Conn_SQL.MIS_ConnectionString)
                'End If

                'update plan qty
                'Dim SQL As String = ""
                'SQL = " select TA006,sum(PlanQty) as sQty from " & table & " where TA006='" & wc & "' " & _
                '      " and TA001='" & moType & "' and TA002='" & moNo & "' " & _
                '      " and TA003='" & moSeq & "' and TA004='" & operation & "'  " & _
                '      " and Cancled='0' group by TA006 "
                'Dim Program As New DataTable
                'Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                'whrHash = New Hashtable
                'whrHash.Add("TA001", moType) 'Plan Date
                'whrHash.Add("TA002", moNo) 'WC
                'whrHash.Add("TA003", moSeq) 'PlanSeq
                'whrHash.Add("TA004", operation) 'Plan Date
                'whrHash.Add("TA006", wc) 'WC
                'fldInsHash = New Hashtable
                'If Program.Rows.Count > 0 Then
                '    Dim pQty As Decimal = Program.Rows(0).Item("sQty")
                '    fldInsHash.Add("PlanedQty", pQty) 'Planed Qty
                'Else
                '    fldInsHash.Add("PlanedQty", 0) 'Planed Qty
                'End If
                'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(table2, fldInsHash, whrHash), Conn_SQL.MIS_ConnectionString)
            End With
        Next
        ' updateToSFCTA()
        show_message.ShowMessage(Page, "Update complete", UpdatePanel1)
        btBack_Click(sender, e)
    End Sub
End Class