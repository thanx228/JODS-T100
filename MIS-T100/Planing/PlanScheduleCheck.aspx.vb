Imports System.Data.OracleClient
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class PlanScheduleCheck
    Inherits System.Web.UI.Page

    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    ' Dim ControlForm As New controld
    Dim CreateTempTable As New CreateTempTable
    Dim configDate As New ConfigDate

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.AppendHeader("Refresh", "300")
        If Not Page.IsPostBack() Then
            'Dim tempTable As String = "TempPlanSchedule" & Session("UserName")
            'CreateTempTable.createTempPlanSchedule(tempTable)
            lbWc.Text = Request.QueryString("WCT100").ToString
            lbPlanDate.Text = Request.QueryString("plandate").ToString
            'lbWc.Text = "WC01"
            'lbPlanDate.Text = "04/03/2017"
            Call getWorkstationName()
            Call getHeaderData()
            Call getBodyData()
        End If
    End Sub
    Private Sub getWorkstationName()
        If lbWc.Text <> String.Empty Then
            Dim dtWC As DataTable = ECAA.GetFindWorkcenterDetail_Table(lbWc.Text)
            If dtWC.Rows.Count > 0 Then
                lbWcName.Text = dtRowsFormat.FormatSumString(dtWC, ECAA.WorkcenterID, ECAA.Workcenter)
            End If
        End If
    End Sub
    Private Sub getBodyData()
        Dim sPlanDate As String = String.Empty
        If lbPlanDate.Text <> String.Empty Then
            Dim ppDate = lbPlanDate.Text.Split("/")
            sPlanDate = ppDate(0) & ppDate(1) & ppDate(2)
        End If
        Dim WC_ERP As String = lbWc.Text.Replace("WC", "W")
        Dim SqlPalnList As String = "select TA001,TA002,PlanSeq,PlanSeqSet,Mch,PlanDate,'JP'+TA001+'-'+TA002 as MO_No," &
" TA003,PlanSeq,PlanQty,ap100 from PlanSchedule " &
"  where TA006='" & WC_ERP & "' and PlanDate='" & sPlanDate & "' Order By PlanSeqSet,TA001,TA002 ASC"
        Dim dtPlan As DataTable = GetdateCustomMOprocessT100(SqlPalnList)
        If dtPlan.Rows.Count > 0 Then
            gvPlan.DataSource = dtPlan
            gvPlan.DataBind()
        End If
    End Sub
    Private Sub getHeaderData()
        Dim MOqtyD As Double = 0
        Dim PlanQtyD As Double = 0
        Dim TrsOuteD As Double = 0
        Dim WC As String = lbWc.Text.Replace("WC", "W")
        Dim sppDate As String = String.Empty
        If lbPlanDate.Text <> "" Then
            Dim pDD = lbPlanDate.Text.Split("/")
            sppDate = pDD(0) & pDD(1) & pDD(2)
        End If
        Dim Sql As String = "  Select sum(CAST(round([PlanQty],2) As Decimal(18,0))) As SumPlanQty " &
" from PlanSchedule where  Cancled='0' and TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
        Dim dtT100PlanQty As DataTable = GetdateCustomMOprocessT100(Sql)
        If dtT100PlanQty.Rows.Count > 0 Then
            Dim PlanQty As String = dtRowsFormat.FormatString(dtT100PlanQty, "SumPlanQty")
            Dim dblNumberPlanQty As Double = CDbl(PlanQty)
            PlanQtyD = dblNumberPlanQty
            lbActPlan.Text = String.Format("{0:n0}", dblNumberPlanQty)
        End If

        Dim WCT100 = WC.Replace("W", "WC")
        Dim sppDateT100 As String = sppDate
        Dim T100WC As String = SFCB.WorkStation & " = '" & [String].Join("','", WCT100) & "'"
        Dim TrsT100Date As String = SFFB.DocumentDate & " = TO_DATE('" & [String].Join("','", sppDateT100) & "','yyyymmdd')"
        Dim T100ProcessWhere As String = T100WC & " and " & TrsT100Date
        Dim SqlT100 As String = " select cast(sum(" & SFAA.ProductionQty & ") as integer) as ProductionQty, " &
            " cast(sum(" & SFCB.GoodTransferOut & ") as integer) as GoodTransferOut " &
" from " & SFCB.tblMOprocessItem_SFCB & " " &
" left join " & SFAA.tblMO & " on " & SFAA.DocNo & "=" & SFCB.WONo & "  " &
" left join " & SFFB.tblTransferHead & " on " & SFFB.WONo & "=" & SFCB.WONo & " and " & SFFB.RunCard & "=" & SFCB.RunCard & " " &
" And " & SFFB.OperationNo & "=" & SFCB.OperationID & " and " & SFFB.OperationSequence & "=" & SFCB.OperationSeq & " " &
" And " & SFFB.Workstation & "=" & SFCB.WorkStation & " " &
" where  " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO'  And " & T100ProcessWhere & " "
        Dim dtT100GoodTransferOut As DataTable = GetDataCustomOracle(SqlT100)
        If dtT100GoodTransferOut.Rows.Count > 0 Then
            Dim GoodTransferOut As String = dtRowsFormat.FormatString(dtT100GoodTransferOut, "GoodTransferOut")
            Dim MOplan As String = dtRowsFormat.FormatString(dtT100GoodTransferOut, "ProductionQty")
            If GoodTransferOut <> String.Empty Then
                Dim dblNumberGoodTransferOut As Double = CDbl(GoodTransferOut)
                TrsOuteD = dblNumberGoodTransferOut
                lbActTran.Text = String.Format("{0:n3}", dblNumberGoodTransferOut)
                Dim dblNumberMOplan As Double = CDbl(MOplan)
                MOqtyD = dblNumberMOplan
                lbMOPlan.Text = String.Format("{0:n0}", dblNumberMOplan)
            End If
        End If

        If TrsOuteD <> 0 And PlanQtyD <> 0 Then
            Dim Result As Double = (TrsOuteD / PlanQtyD) * 100
            lbPer.Text = String.Format("{0:n2}", Result)

            Dim noActionPlan As Double = PlanQtyD - TrsOuteD
            lbNoPlan.Text = String.Format("{0:n3}", noActionPlan)

            Dim noActionPlanPercent As Double = (noActionPlan / PlanQtyD) * 100
            lbPerNoPlan.Text = String.Format("{0:n2}", noActionPlanPercent)
        End If
        Dim SqlDataMC As String = "SELECT wc,capacity,mancapacity from MachineCapacity where  wc='" & WC & "'  "
        Dim dtMCloading As DataTable = GetDataSQLserverMIS(SqlDataMC)
        If dtMCloading.Rows.Count > 0 Then
            Dim WcLoading As String = dtRowsFormat.FormatString(dtMCloading, "capacity")
            lbWcLoad.Text = WcLoading
            If lbWcLoad.Text <> String.Empty Then
                lbWcLoad.Text = CalcHHMM(lbWcLoad.Text)
            End If
        End If

        Dim WhereWC As String = " TA006 ='" & WC & "' AND PlanDate ='" & sppDate & "'"
        Dim dtT100laborStd As DataTable = GetdateMOprocessT100(WhereWC)
        If dtT100laborStd.Rows.Count > 0 Then
            Dim Std_LaborHours As String = dtRowsFormat.FormatString(dtT100laborStd, "StdLaborTime")
            Dim Std_McHours As String = dtRowsFormat.FormatString(dtT100laborStd, "StdMcTime")
            lbManTime.Text = Std_LaborHours
            If lbManTime.Text <> String.Empty Then
                lbManTime.Text = CalcHHMM(lbManTime.Text)
            End If
            lbMchTime.Text = Std_McHours
            If lbMchTime.Text <> String.Empty Then
                lbMchTime.Text = CalcHHMM(lbMchTime.Text)
            End If
        End If

        Dim SqlMIS As String = "select count(*) as cnt from PlanSchedule where  PlanDate='" & sppDate & "' and TA006='" & WC & "' and Cancled='1' " 'and PlanDate<='" & planDate & "'
        Dim Program As DataTable = GetDataSQLserverMIS(SqlMIS)
        With Program
            If .Rows.Count > 0 Then
                With .Rows(0)
                    Dim xper As Decimal = 0
                    lbCan.Text = .Item("cnt").ToString
                    If .Item("cnt") + CDec(lbActPlan.Text) > 0 Then
                        xper = .Item("cnt") * 100 / (.Item("cnt") + CDec(lbActPlan.Text))
                        lbCanRate.Text = CStr(xper.ToString("#,##0.000"))
                    Else
                        lbCanRate.Text = "0.00"
                    End If
                End With
            End If
        End With

        Dim SqlAP100 As String = "select sum(ap100) ap100 from PlanSchedule where  PlanDate='" & sppDate & "' and TA006='" & WC & "' and Cancled='0' " 'and PlanDate<='" & planDate & "'
        Dim dtAP100 As DataTable = GetDataSQLserverMIS(SqlAP100)
        With dtAP100
            If .Rows.Count > 0 Then
                With .Rows(0)
                    Dim hmin As String = "0" & (.Item("ap100") Mod 60).ToString
                    lbAp100.Text = (Math.Floor(.Item("ap100") / 60)).ToString & ":" & hmin.Substring(hmin.Count - 2, 2).ToString
                    'lbAp100.Text = ConvertUtility.CalcHHMM(.Item("ap100"))
                End With
            End If
        End With
    End Sub
    Private Shared Function CalcHHMM(Min As String) As String
        Dim showHHMM As String = String.Empty
        Dim iRowMc As Integer = CInt(Int(Min))
        If iRowMc > 0 Then
            Dim hours As Integer = iRowMc \ 60
            Dim minutes As Integer = iRowMc - (hours * 60)
            Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes.ToString("00"), String)
            showHHMM = timeElapsed
        Else
            showHHMM = "00:00"
        End If
        Return showHHMM
    End Function
    Protected Sub btBefore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBefore.Click
        'getDate()
        'Response.Redirect(Server.UrlPathEncode("PlanScheduleCheck.aspx?wc=" & lbWc.Text.Trim & "&wcName=" & lbWcName.Text.Trim & "&plandate=" & getDate()))
    End Sub

    Protected Sub btAfter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAfter.Click
        'getDate(False)
        ' Response.Redirect(Server.UrlPathEncode("PlanScheduleCheck.aspx?wc=" & lbWc.Text.Trim & "&wcName=" & lbWcName.Text.Trim & "&plandate=" & getDate(False)))
        ' Response.Redirect(Server.UrlPathEncode("PlanScheduleList.aspx.aspx?"))
    End Sub

    'Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    '    Timer1.Interval = 300000
    '    Timer1.Enabled = True
    '    gvPlan.DataBind()
    '    gvOutPlan.DataBind()
    'End Sub

    Private Sub gvPlan_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim PlanDate As String = e.Row.Cells(2).Text
            Dim ERP_MO As String = e.Row.Cells(4).Text
            Dim lblCustomer As Label = CType(e.Row.FindControl("lblCustomer"), Label)
            Dim lblMO_Seq As Label = CType(e.Row.FindControl("lblMO_Seq"), Label)
            Dim lblOp_Id As Label = CType(e.Row.FindControl("lblOp_Id"), Label)
            Dim lblPlanQty As Label = CType(e.Row.FindControl("lblPlanQty"), Label)
            Dim lblPlanSeq As Label = CType(e.Row.FindControl("lblPlanSeq"), Label)
            Dim lblAP100 As Label = CType(e.Row.FindControl("lblAP100"), Label)

            Dim lblMOStatus As Label = CType(e.Row.FindControl("lblMOStatus"), Label)
            Dim lblProductionItem As Label = CType(e.Row.FindControl("lblProductionItem"), Label)
            Dim lblProductionItemName As Label = CType(e.Row.FindControl("lblProductionItemName"), Label)
            Dim lblSpec As Label = CType(e.Row.FindControl("lblSpec"), Label)
            Dim lblProductionQty As Label = CType(e.Row.FindControl("lblProductionQty"), Label)
            Dim lblCompleteQty As Label = CType(e.Row.FindControl("lblCompleteQty"), Label)
            Dim lblScrapQty As Label = CType(e.Row.FindControl("lblScrapQty"), Label)
            Dim lblOperation As Label = CType(e.Row.FindControl("lblOperation"), Label)
            Dim lblStartTimeMC As Label = CType(e.Row.FindControl("lblStartTimeMC"), Label)

            Dim lblStdOutPut As Label = CType(e.Row.FindControl("lblStdOutPut"), Label)
            Dim lblWIP As Label = CType(e.Row.FindControl("lblWIP"), Label)
            Dim lblGoodTrsIn As Label = CType(e.Row.FindControl("lblGoodTrsIn"), Label)
            Dim lblGoodTrsOut As Label = CType(e.Row.FindControl("lblGoodTrsOut"), Label)
            Dim lblReworkTrsIn As Label = CType(e.Row.FindControl("lblReworkTrsIn"), Label)
            Dim lblReworkTrsOut As Label = CType(e.Row.FindControl("lblReworkTrsOut"), Label)
            Dim lblDirectScarp As Label = CType(e.Row.FindControl("lblDirectScarp"), Label)
            Dim lblDirectSuspend As Label = CType(e.Row.FindControl("lblDirectSuspend"), Label)

            Dim lblPerviousOperation As Label = CType(e.Row.FindControl("lblPerviousOperation"), Label)
            Dim lblNextOperation As Label = CType(e.Row.FindControl("lblNextOperation"), Label)
            Dim lblWorkstationNext As Label = CType(e.Row.FindControl("lblWorkstationNext"), Label)
            Dim lblStdLaborHours As Label = CType(e.Row.FindControl("lblStdLaborHours"), Label)
            Dim lblStdMcHours As Label = CType(e.Row.FindControl("lblStdMcHours"), Label)

            If ERP_MO <> String.Empty AndAlso lblMO_Seq.Text <> String.Empty Then
                Dim xxMO = ERP_MO.Split(" ")
                Dim iSeq As Integer = CInt(lblMO_Seq.Text)
                lblMO_Seq.Text = iSeq
                Dim SqlProcessT100 As String = " select " & SFCB.WONo & "," & SFCB.OperationID & "," & SFAA.Status & "," & SFAA.ProductItem & ", " &
                    " " & SFAA.ProductionQty & "," & SFAA.ScarpQty & "," & XMDA.CustomerId & "," & SFCA.CompletedQty & "," & SFCB.WIP & ", " &
                    " " & SFCB.StardardOutput & "," & SFCB.GoodTransferIn & "," & SFCB.GoodTransferOut & "," & SFCB.ReworkTrsIn & ", " &
                    " " & SFCB.TransferOutforRework & "," & SFCB.DirectScarp & "," & SFCB.DirectSuspend & "," & SFCB.PerviousOperation & ", " &
                    " " & SFCB.NextOperation & "," & SFCB.NextStationOpSeq & "," & SFCB.StandradLaborHours2 & "," & SFCB.StandradMachineHours2 & " " &
                    " from " & SFCB.tblMOprocessItem_SFCB & " " &
                    " LEFT OUTER JOIN " & SFAA.tblMO & " on " & SFAA.DocNo & "=" & SFCB.WONo & " " &
                    " LEFT OUTER JOIN  " & SFCA.tblMO_Detail & " On " & SFCA.DocNo & " = " & SFAA.DocNo & " " &
                    " LEFT OUTER JOIN  " & SFBA.tblManufactureOrder_Body & " On " & SFBA.MODocNo & "=" & SFCB.WONo & "  " &
                    " LEFT OUTER JOIN  " & XMDC.tblSaleItem & " On " & XMDC.Item & "=" & SFBA.MasterItemNo & " " &
                    " LEFT OUTER JOIN  " & XMDA.tblSaleHead & " On " & XMDC.SaleOrderNo & "=" & XMDA.SaleOrderNo & " " &
                    " where " & SFCB.WONo & "='" & xxMO(0) & "' and " & SFCB.LineNo & "='" & iSeq & "' " &
                    " and " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' " &
                    " and " & SFCB.WorkStation & "='" & lbWc.Text & "'  "
                Dim dtProcessT100 As DataTable = GetDataCustomOracle(SqlProcessT100)
                If dtProcessT100.Rows.Count > 0 Then
                    lblOp_Id.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.OperationID)
                    If lblOp_Id.Text <> String.Empty Then
                        Dim dtOP As DataTable = OOCQL.GetDataOperation(lblOp_Id.Text)
                        If dtOP.Rows.Count > 0 Then
                            lblOperation.Text = dtRowsFormat.FormatString(dtOP, OOCQL.Operation)
                        End If
                    End If
                    lblMOStatus.Text = dtRowsFormat.FormatString(dtProcessT100, SFAA.Status)
                    If lblMOStatus.Text <> String.Empty Then
                        lblMOStatus.Text = StatusT100.MO_Normal(lblMOStatus.Text)
                    End If
                    lblProductionItem.Text = dtRowsFormat.FormatString(dtProcessT100, SFAA.ProductItem)
                    If lblProductionItem.Text <> String.Empty Then
                        Dim dtProductionItemName As DataTable = IMAAL.GetDataProducItem(lblProductionItem.Text)
                        If dtProductionItemName.Rows.Count > 0 Then
                            lblProductionItemName.Text = dtRowsFormat.FormatString(dtProductionItemName, IMAAL.ProductName)
                            lblSpec.Text = dtRowsFormat.FormatString(dtProductionItemName, IMAAL.Specifaction)
                        End If
                    End If

                    lblProductionQty.Text = dtRowsFormat.FormatString(dtProcessT100, SFAA.ProductionQty)
                    If lblProductionQty.Text <> String.Empty Then
                        Dim dblNumberProductionQty As Double = CDbl(lblProductionQty.Text)
                        lblProductionQty.Text = String.Format("{0:n3}", dblNumberProductionQty)
                    End If
                    lblScrapQty.Text = dtRowsFormat.FormatString(dtProcessT100, SFAA.ScarpQty)
                    If lblScrapQty.Text <> String.Empty Then
                        Dim dblNumberScrapQty As Double = CDbl(lblScrapQty.Text)
                        lblScrapQty.Text = String.Format("{0:n3}", dblNumberScrapQty)
                    End If
                    lblCustomer.Text = dtRowsFormat.FormatString(dtProcessT100, XMDA.CustomerId)
                    'If lblCustomer.Text <> String.Empty Then
                    '    Dim dtCustomerName As DataTable = PMAAL.GetDataCustomerDetail(lblCustomer.Text)
                    '    If dtCustomerName.Rows.Count > 0 Then
                    '        'lblCustomer.Text = dtRowsFormat.FormatSumString(dtCustomerName, PMAAL.CustomerID, PMAAL.CustomerName)
                    '    End If
                    'End If
                    lblCompleteQty.Text = dtRowsFormat.FormatString(dtProcessT100, SFCA.CompletedQty)
                    If lblCompleteQty.Text <> String.Empty Then
                        Dim dblNumberCompleteQty As Double = CDbl(lblCompleteQty.Text)
                        lblCompleteQty.Text = String.Format("{0:n3}", dblNumberCompleteQty)
                    End If
                    lblStdOutPut.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.StardardOutput)
                    If lblStdOutPut.Text <> String.Empty Then
                        Dim dblNumberStdOutPut As Double = CDbl(lblStdOutPut.Text)
                        lblStdOutPut.Text = String.Format("{0:n3}", dblNumberStdOutPut)
                    End If
                    lblWIP.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.WIP)
                    If lblWIP.Text <> String.Empty Then
                        Dim dblNumberWIP As Double = CDbl(lblWIP.Text)
                        lblWIP.Text = String.Format("{0:n3}", dblNumberWIP)
                    End If
                    lblGoodTrsIn.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.GoodTransferIn)
                    If lblGoodTrsIn.Text <> String.Empty Then
                        Dim dblNumberGoodTrsIn As Double = CDbl(lblGoodTrsIn.Text)
                        lblGoodTrsIn.Text = String.Format("{0:n3}", dblNumberGoodTrsIn)
                    End If
                    lblGoodTrsOut.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.GoodTransferOut)
                    If lblGoodTrsOut.Text <> String.Empty Then
                        Dim dblNumberGoodTrsOut As Double = CDbl(lblGoodTrsOut.Text)
                        lblGoodTrsOut.Text = String.Format("{0:n3}", dblNumberGoodTrsOut)
                    End If
                    lblReworkTrsIn.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.ReworkTrsIn)
                    If lblReworkTrsIn.Text <> String.Empty Then
                        Dim dblNumberReworkTrsIn As Double = CDbl(lblReworkTrsIn.Text)
                        lblReworkTrsIn.Text = String.Format("{0:n3}", dblNumberReworkTrsIn)
                    End If
                    lblReworkTrsOut.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.TransferOutforRework)
                    If lblReworkTrsOut.Text <> String.Empty Then
                        Dim dblNumberReworkTrsOut As Double = CDbl(lblReworkTrsOut.Text)
                        lblReworkTrsOut.Text = String.Format("{0:n3}", dblNumberReworkTrsOut)
                    End If
                    lblDirectScarp.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.DirectScarp)
                    If lblDirectScarp.Text <> String.Empty Then
                        Dim dblNumberDirectScarp As Double = CDbl(lblDirectScarp.Text)
                        lblDirectScarp.Text = String.Format("{0:n3}", dblNumberDirectScarp)
                    End If
                    lblDirectSuspend.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.DirectSuspend)
                    If lblDirectSuspend.Text <> String.Empty Then
                        Dim dblNumberDirectSuspend As Double = CDbl(lblDirectSuspend.Text)
                        lblDirectSuspend.Text = String.Format("{0:n3}", dblNumberDirectSuspend)
                    End If
                    lblPerviousOperation.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.PerviousOperation)
                    If lblPerviousOperation.Text <> String.Empty Then
                        Dim dtOPprev As DataTable = OOCQL.GetDataOperation(lblPerviousOperation.Text)
                        If dtOPprev.Rows.Count > 0 Then
                            lblPerviousOperation.Text = dtRowsFormat.FormatSumString(dtOPprev, OOCQL.OperationID, OOCQL.Operation)
                        End If
                    End If
                    lblNextOperation.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.NextOperation)
                    Dim NextOperation As String = dtRowsFormat.FormatString(dtProcessT100, SFCB.NextOperation)
                    Dim NextStationOpSeq As String = dtRowsFormat.FormatString(dtProcessT100, SFCB.NextStationOpSeq)
                    If lblNextOperation.Text <> String.Empty Then
                        Dim dtOPnext As DataTable = OOCQL.GetDataOperation(lblNextOperation.Text)
                        If dtOPnext.Rows.Count > 0 Then
                            lblNextOperation.Text = dtRowsFormat.FormatSumString(dtOPnext, OOCQL.OperationID, OOCQL.Operation)
                        End If
                    End If
                    If NextOperation <> String.Empty And NextStationOpSeq <> String.Empty Then
                        Dim SqlNextWorkstation As String = " select " & SFCB.WorkStation & " from " & SFCB.tblMOprocessItem_SFCB & " " &
                         " where " & SFCB.OperationID & "='" & NextOperation & "' and " & SFCB.OperationSeq & "='" & NextStationOpSeq & "' " &
                         " and " & SFCB.ent & "='3' and " & SFCB.Site & "='JINPAO' "
                        Dim dtNextWC As DataTable = GetDataCustomOracle(SqlNextWorkstation)
                        If dtNextWC.Rows.Count > 0 Then
                            lblWorkstationNext.Text = dtRowsFormat.FormatString(dtNextWC, SFCB.WorkStation)
                        End If
                    End If
                    If lblWorkstationNext.Text <> String.Empty Then
                        Dim dtToWC As DataTable = ECAA.GetFindWorkcenterDetail_Table(lblWorkstationNext.Text)
                        If dtToWC.Rows.Count > 0 Then
                            lblWorkstationNext.Text = dtRowsFormat.FormatSumString(dtToWC, ECAA.WorkcenterID, ECAA.Workcenter)
                        End If
                    End If
                    lblStdLaborHours.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.StandradLaborHours2)
                    If lblStdLaborHours.Text <> String.Empty Then
                        Dim dblNumberStdLaborHours As Double = CDbl(lblStdLaborHours.Text)
                        lblStdLaborHours.Text = String.Format("{0:n3}", dblNumberStdLaborHours)
                    End If
                    lblStdMcHours.Text = dtRowsFormat.FormatString(dtProcessT100, SFCB.StandradMachineHours2)
                    If lblStdMcHours.Text <> String.Empty Then
                        Dim dblNumberStdMcHours As Double = CDbl(lblStdMcHours.Text)
                        lblStdMcHours.Text = String.Format("{0:n3}", dblNumberStdMcHours)
                    End If

                    Dim WC_atis340 As String = lbWc.Text.Replace("WC", "w")
                    Dim atis340iSeq As Integer = CInt(lblMO_Seq.Text)
                    Dim jpMO = e.Row.Cells(4).Text.Replace("JP", "")
                    Dim atisMO = jpMO.Split("-")
                    Dim atis430MOtype As String = atisMO(0)
                    Dim atis430MO As String = atisMO(1)
                    Dim atisSdateTime As Date = CDate(lbPlanDate.Text)
                    Dim lblKOISmcStart As Label = CType(e.Row.FindControl("lblKOISmcStart"), Label)
                    Dim sqMySqlatis340 As String = " SELECT CAST(start_time AS DATE) as Sdate,CAST(start_time AS time) as STime,machines.mid as McName,start_time,end_time " &
                    " FROM process_updates LEFT OUTER JOIN machines ON machines.id=process_updates.machine " &
                    " WHERE process_workcenter='" & WC_atis340 & "' And seq='" & atis340iSeq.ToString("0000") & "' " &
                    " And mo_type='" & atis430MOtype & "' And mo_number='" & atis430MO & "' And CAST(start_time AS DATE) = '" & atisSdateTime.ToString("yyyy-MM-dd") & "' "
                    Dim dtAtis340 As DataTable = GetdateCustomMOprocessKIOS(sqMySqlatis340)
                    If dtAtis340.Rows.Count > 0 Then
                        Dim McName As String = dtRowsFormat.FormatString(dtAtis340, "McName")
                        Dim sDateTime As String = dtRowsFormat.FormatString(dtAtis340, "start_time")
                        lblKOISmcStart.Text = McName & " " & sDateTime
                    End If
                    Dim MO As String = e.Row.Cells(4).Text
                    Dim ProductionItem As String = lblProductionItem.Text
                    e.Row.Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
                    'e.Row.Attributes.Add("onclick", "ChangeRowColor(this,'','');")
                    e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#00CC00'")
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;")
                    e.Row.Attributes.Add("onclick", "NewWindow('PlanScheduleAddPop.aspx?item=&mo=" & MO & "&ProductionItem=" & ProductionItem & "','PlanScheduleAddPop',900,600,'yes')")
                End If

            End If

            '        'Dim PlanDate As String = configDate.dateFormat2(.DataItem("PlanDate"))
            '        Dim PlanDateToday As String = configDate.dateFormat2(lbPlanDate.Text.Trim)
            '        Dim PlanDate As String = .DataItem("PlanDate").ToString.Trim
            '        'UnAppQty
            '        If PlanDateToday = PlanDate Then
            '            Dim MOQty As Decimal = CDec(.DataItem("TA015").ToString.Trim)
            '            Dim PQty As Decimal = CDec(.DataItem("PlanQty").ToString.Trim)
            '            Dim AQty As Decimal = 0
            '            Dim SQty As Decimal = 0
            '            If .DataItem("ActualQty").ToString.Trim <> "" And IsNumeric(.DataItem("ActualQty").ToString.Trim) Then
            '                AQty = CDec(.DataItem("ActualQty").ToString.Trim)
            '            End If
            '            If .DataItem("UnAppQty").ToString.Trim <> "" And IsNumeric(.DataItem("UnAppQty").ToString.Trim) Then
            '                SQty = CDec(.DataItem("UnAppQty").ToString.Trim)
            '            End If

            '            'UnAppQty()
            '            If PQty > MOQty Then
            '                .ForeColor = Drawing.Color.Gray
            '            ElseIf AQty + SQty < PQty Then
            '                .ForeColor = Drawing.Color.Red
            '            Else
            '                .ForeColor = Drawing.Color.Blue
            '            End If
            '        ElseIf PlanDate > PlanDateToday Then
            '            .ForeColor = Drawing.Color.Black
            '        Else
            '            .ForeColor = Drawing.Color.Red
            '        End If
            '        Dim mo As String = .Cells(4).Text.Trim
            '        With .Cells(7)
            '            .Style.Add(HtmlTextWriterStyle.Cursor, "pointer")
            '            .Attributes.Add("onclick", "NewWindow('PlanScheduleAddPop.aspx?item=&mo=" & mo & "','PlanScheduleAddPop',900,600,'yes')")
            '        End With
        End If
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        'Save To Excel File
    End Sub

    Protected Sub btExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btExport.Click
        ControlForm.ExportGridViewToExcel("PlanScheduleCheck" & Session("UserName"), gvPlan)
    End Sub
    Private Shared Function GetdateCustomMOprocessKIOS(Sql As String) As DataTable
        Dim dtAdapter As MySqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New MySqlConnection(clsDBConnect.strKioskConnectionString)
        Try
            dtAdapter = New MySqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql = SqlDataMOprocessT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared Function GetdateCustomMOprocessT100(Sql As String) As DataTable
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql = SqlDataMOprocessT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared Function GetDataCustomOracle(Sql As String) As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetDataWhereWrokstatinDataT100", "Sql = SqlDataWrokstatinDataT100", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
    Private Shared SqlDataMOprocessT100 As String = " select sum(CAST(round([StdManT100],2) AS decimal(18,0))) as StdLaborTime, " &
" sum(CAST(round([StdMCT100],2) AS decimal(18,0))) as StdMcTime  " &
" from PlanSchedule where  @pWhereCustomUsing and Cancled='0' "
    Private Shared Function GetdateMOprocessT100(strWhereCustomUsing As String) As DataTable
        Dim Sql As String = SqlDataMOprocessT100
        Dim pWhereCustomUsing As String = strWhereCustomUsing
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As SqlDataAdapter
        Dim dt As New DataTable
        Dim objConn = New SqlConnection(clsDBConnect.strMISConnectionString)
        Try
            dtAdapter = New SqlDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            Dim FilePage As String = HttpContext.Current.Request.CurrentExecutionFilePath.ToString
            GetPageError.GetPage(FilePage, "GetdateMOprocessT100", "Sql = SqlDataMOprocessT100", ex.Message)
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
            GetPageError.GetPage(FilePage, "GetDataTableFindMC", "Sql", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
        ' Return Sql
    End Function
End Class