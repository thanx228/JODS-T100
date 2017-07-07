Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Imports System.Exception
Public Class SFAA
    '# Module T100 : ASF
    Private Shared ASF As String = "ASF"
    '# Table : sfaa_t
    '# asft300 : Maintain Work Order :Header
    '# asft004 : Workstation WIP Status : Tab1: Product Item No.
    '# asfp500 : Close MO : Header
    '# select rows Top 100 (Example)
    ''' <remarks> Structure Table MO Header </remarks>
    Public Shared tblMO As String = "sfaa_t"
    ''' <remarks> Field </remarks>
    Public Shared DocNo As String = "sfaadocno"
    Public Shared VersionDocNo As String = "sfaa002"
    Public Shared VersionItem As String = "sfaa002"
    Public Shared DocumentDate As String = "sfaadocdt"
    Public Shared ManufacManagmentPersonal As String = "sfaa002"
    Public Shared WOType As String = "sfaa003"
    Public Shared IssuanceSystem As String = "sfaa004"
    Public Shared Factory As String = "sfaa057"
    Public Shared Status As String = "sfaastus"
    Public Shared Company As String = "sfaasite"
    ''' <remarks> BasicData </remarks>
    Public Shared WorkOrderSource As String = "sfaa005"
    Public Shared SourceDocNo As String = "sfaa006"
    Public Shared ReferanceSuctomer As String = "sfaa009"
    Public Shared ReferanceCustomer As String = "sfaa009"
    Public Shared OldRefereanceDocNo As String = "sfaa022"

    Public Shared OldRefereanceDocLineNo As String = "sfaa023" 'sale order line ## Old reference line No.
    Public Shared OldRefereanceDocLineSeq As String = "sfaa024" 'sale order line on tab delivery detial##Old reference line sequence

    Public Shared ParentWorkOrderNumber As String = "sfaa021"
    Public Shared PerviousWorkOrderNo As String = "sfaa025"
    Public Shared ProductItem As String = "sfaa010"
    Public Shared ProductionQty As String = "sfaa012"
    Public Shared Unit As String = "sfaa013"
    Public Shared RoutingCode As String = "sfaa061"
    Public Shared CheckReoutingCode As String = "sfaa016"
    Public Shared DeptVendor As String = "sfaa017"
    Public Shared CooperativeLocation As String = "sfaa018"
    Public Shared PlanStartDate As String = "sfaa019"
    Public Shared PlanedCompletionDate As String = "sfaa020"
    Public Shared CostCenter As String = "sfaa068"
    ''' <remarks> WO Data </remarks>
    Public Shared BOMVersion As String = "sfaa014"
    Public Shared BOMEffectiveDate As String = "sfaa015"
    Public Shared ItemListLotNoPBI As String = "sfaa026"
    Public Shared ProjectNo As String = "sfaa028"
    Public Shared WBS As String = "sfaa029"
    Public Shared Activity As String = "sfaa030"
    Public Shared ReasonCode As String = "sfaa031"
    Public Shared CriticalRatio As String = "sfaa032"
    Public Shared EstimatedStorgeWH As String = "sfaa034"
    Public Shared EstimatedStorgeLocation As String = "sfaa035"
    Public Shared EstimatedStorgeBacthNo As String = "sfaa059"
    Public Shared ManualNo As String = "sfaa036"
    Public Shared BondedApprovalNo As String = "sfaa037"
    ''' <remarks> Relevant Information </remarks>
    Public Shared MaterialPerparationGen As String = "sfaa039"
    Public Shared ManualfacturingDistanceConfrim As String = "sfaa040"
    Public Shared Frozen As String = "sfaa041"
    Public Shared Rework As String = "sfaa042"
    Public Shared Allocate As String = "sfaa043"
    Public Shared SuppliableVolume As String = "sfaa069"
    Public Shared LodEstCompletionDate As String = "sfaa070"
    Public Shared ActualStartIssuanceDate As String = "sfaa045" '  DateTime
    Public Shared FinalStorgeDate As String = "sfaa046" '  DateTime
    Public Shared ManufacturingManagClosingDate As String = "sfaa047" '  DateTime
    Public Shared CostClosingDate As String = "sfaa048" '  DateTime
    Public Shared ManufManagSetStatus As String = "sfaa065"
    Public Shared IssuedSets As String = "sfaa049" ' Numberice
    Public Shared StoredPassQuantity As String = "sfaa050" 'Numberice
    Public Shared StoredFailureQuantity As String = "sfaa051" ' Numberice
    Public Shared SuspenQty As String = "sfaa056" 'Numberice
    Public Shared ScarpQty As String = "sfaa056" 'Numberice
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "sfaaownid"
    Public Shared DataOwnerDept As String = "sfaaowndp"
    Public Shared DataCreateBy As String = "sfaacrtid"
    Public Shared DataCreateByDept As String = "sfaacrtdp"
    Public Shared DataCreateDate As String = "sfaacrtdt"

    Public Shared DataModifyBy As String = "sfaamodid"
    Public Shared LastModifyDate As String = "sfaamoddt"
    Public Shared DataConfrimationPersonal As String = "sfaacnfid"
    Public Shared DataConfrmedDate As String = "sfaacnfdt"
    Public Shared ProgromCode As String = "sfaaent"
    Public Shared ent As String = "sfaaent"
    Public Shared Site As String = "sfaasite"
    Public Shared VersionMOChange As String = "sfaa001"
    Public Shared LineNo As String = "sfaa007"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"
    Public Shared ApprovedtypeA As String = Status & "='A'"
    Public Shared Closed As String = "C"
    Public Shared Withdraw As String = "D"
    Public Shared Appoved As String = "Y"
    Public Shared Released As String = Status & "='F'"
    Public Shared CostClose As String = "M"
    Public Shared UnApproved As String = "N"
    Public Shared Rejected As String = "R"
    Public Shared Approving As String = "W"
    Public Shared Voided As String = "X"
    Public Shared Approved As String = Status & "='Y'"
    Public Shared Termination As String = "E"

    '--Page SaleOrderChangeStus
    '--SelectMOSOChgLine
    Private Shared SelectMOSOChgLine As String = "select case " & Status & " " &
        " when 'A' then 'Approved' " &
        " when 'C' then 'Closed' " &
        " when 'D' then 'Withdraw' " &
        " when 'F' then 'Released' " &
        " when 'M' then 'CostClose' " &
        " when 'N' then 'UnApproved' " &
        " when 'R' then 'Rejected' " &
        " when 'W' then 'Approving' " &
        " when 'X' then 'Voided' " &
        " when 'Y' then 'Approved' " &
        " when 'E' then 'Termination' end as Status," &
        " " & IssuedSets & "," & StoredPassQuantity & "," & ScarpQty & ", " &
        " " & DocNo & "," & ProductItem & "," & VersionMOChange & "," & CheckReoutingCode & "," & SourceDocNo & "," & LineNo & " from " & tblMO & " " &
        " where " & wStandard & "and  " & SourceDocNo & " ='@OrderNoLine' "
    Private Shared SelectMOSOChg As String = "select " & Status & ", " &
        " " & IssuedSets & "," & StoredPassQuantity & "," & ScarpQty & ", " &
        " " & DocNo & "," & ProductItem & "," & VersionMOChange & "," & CheckReoutingCode & "," & SourceDocNo & "," & LineNo & " from " & tblMO & " " &
        " where " & wStandard & " and  " & SourceDocNo & " ='@OrderNoLine'"
    Public Shared Function GetOrderNoSOChgLine(ByVal OrderNoLine As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SelectMOSOChgLine,
            Oracle As String = SelectMOSOChg,
            test As String = "JP2205-20170125003"
        OrderNoLine = test
        Oral = Oral.Replace("@OrderNoLine", OrderNoLine)
        Dim dt As New DataTable
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, dt)
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim MOQty As Integer = dt.Rows(0).Item("sfaa049"),
                MOReceiptsQty As Integer = dt.Rows(0).Item("sfaa050"),
                MOScarpQty As Integer = dt.Rows(0).Item("sfaa056"),
                SOChgLine As String = dt.Rows(0).Item("sfaa006"),
                satus As String = dt.Rows(0).Item("Status")

            If MOScarpQty = 0 Then
                MOScarpQty = Nothing
            End If

            Dim SumQty As Integer = MOReceiptsQty + MOScarpQty

            If dt.Rows.Count > 0 Then
                If satus = "Closed" Then
                    If SumQty = MOQty Then
                        satus = "Finished"
                    ElseIf SumQty < MOQty Then
                        satus = "Material Not Issue"
                    ElseIf SumQty = 0 Then
                        satus = "Manual Close"
                    End If
                Else
                    If satus = "Released" Then
                        If SumQty = MOQty Then
                            satus = "Finished"
                        ElseIf SumQty < MOQty Then
                            satus = "Manufacturing"
                        ElseIf SumQty = 0 Then
                            satus = "Have not Manufactured"
                        End If
                    End If
                End If
            End If
            Oracle = Oracle.Replace("@OrderNoLine", SOChgLine)
            'Oracle = Oracle.Replace("@Status", satus)
            GetData.Get_DataReaderOracle(Oracle, clsDBConnect.strT100ConnectionString, tempDataTable)
        Next
    End Function

    '--Search MO Where ItemNo from No Rrfesh DataTable
    Private Shared SearchMO As String = "select " & DocNo & "," & VersionMOChange & "," & ProductItem & "," & ProductionQty & "," & StoredPassQuantity & "," & ScarpQty & " from " & tblMO & " " &
        " where " & wStandard & " and  " & ProductItem & " ='@ItemNo' and " & Released & " order by " & ProductItem & ""
    Public Shared Function GetItem(ByVal ItemNo As String, ByRef tempDataTable As Data.DataTable)
        Dim Oral As String = SearchMO
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
    End Function

    '--Page Sales Undelivery Status PopUp and Page SLUndelStusAmountPopUp
    '--Search MO Where ItemNo from Rrfesh DataTable
    Private Shared SearchMORefresh As String = "select " & DocNo & ",substr(" & DataConfrmedDate & ",0,10) as ConfrmedDate," & ProductItem & "," & ProductionQty & "," & StoredPassQuantity & "," &
        " " & ScarpQty & "," & PlanStartDate & "," & PlanedCompletionDate & " from " & tblMO & "" &
        " where " & wStandard & "and  " & ProductItem & "='@ItemNo' and " & Released & " order by " & DocNo & "," & ProductItem & ""
    Public Shared Function GetItemRefresh(ByVal ItemNo As String)
        Dim Oral As String = SearchMORefresh
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page Sales Undelivery Status and Page UndeliveryStusAmount and Page SaleUndelivery Status Period 
    '--Search Sum MO Where ItemNo / Rrfesh DataTable
    Private Shared SearchSumMO As String = "select " & ProductItem & ",sum(" & ProductionQty & "-" & StoredPassQuantity & "-" & ScarpQty & ") as MOQty from " & tblMO & "" &
        " where " & wStandard & "and  " & ProductItem & "='@ItemNo' and " & Released & " Group by " & ProductItem & " order by " & ProductItem & ""
    Public Shared Function SumMO(ByVal ItemNo As String)
        Dim Oral As String = SearchSumMO
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@ItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function






























    '####################### Where MO Doc No. ( Url = 1. /Planing/PlanByMO.aspx ) ###############################################
    Private Shared strWH_MO_No As String = "Select " & DocNo & " ," & VersionDocNo & "," & VersionItem & ", " & DocumentDate & ", " &
        " " & ManufacManagmentPersonal & " ," & WOType & "," & IssuanceSystem & "," & Factory & "," & Status & "," & WorkOrderSource & ", " &
        " " & SourceDocNo & "," & ReferanceSuctomer & "," & OldRefereanceDocNo & "," & ParentWorkOrderNumber & "," & PerviousWorkOrderNo & ", " &
        " " & ProductItem & "," & ProductionQty & "," & Unit & "," & RoutingCode & "," & CheckReoutingCode & "," & DeptVendor & ", " &
        " " & CooperativeLocation & "," & PlanStartDate & "," & PlanedCompletionDate & "," & CostCenter & "," & BOMVersion & "," & BOMEffectiveDate & ", " &
        " " & ItemListLotNoPBI & "," & ProjectNo & "," & WBS & "," & Activity & "," & ReasonCode & "," & CriticalRatio & "," & EstimatedStorgeWH & ", " &
        " " & EstimatedStorgeLocation & "," & EstimatedStorgeBacthNo & "," & ManualNo & "," & BondedApprovalNo & "," & MaterialPerparationGen & ", " &
        " " & ProjectNo & "," & WBS & "," & Activity & "," & ReasonCode & "," & CriticalRatio & "," & EstimatedStorgeWH & "," & MaterialPerparationGen & ", " &
        " " & ManualfacturingDistanceConfrim & "," & Frozen & "," & Rework & "," & Allocate & "," & SuppliableVolume & "," & LodEstCompletionDate & ", " &
        " " & ActualStartIssuanceDate & "," & FinalStorgeDate & "," & ManufacturingManagClosingDate & "," & CostClosingDate & "," & ManufManagSetStatus & ", " &
        " " & IssuedSets & "," & StoredPassQuantity & "," & StoredFailureQuantity & "," & SuspenQty & "," & ScarpQty & ", " &
        " " & DataModifyBy & "," & LastModifyDate & "," & DataConfrimationPersonal & "," & DataConfrmedDate & "," & Company & " " &
        " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & " " &
        " FROM " & tblMO & "  " &
        " where " & Company & " Like 'JINPAO' AND " & DocNo & " =@MO_No "
    Public Shared Function GetMO_HeaderDeatil(strMoNo As String) As DataTable
        Dim Sql As String = strWH_MO_No.Replace("@MO_No", "'" & strMoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFAA", "GetMO_HeaderDeatil", "Sql = strWH_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMO_HeaderDeatilDataSet(strMoNo As String) As DataSet
        Dim Sql As String = strWH_MO_No.Replace("@MO_No", "'" & strMoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFAA", "GetMO_HeaderDeatilDataSet", "Sql = strWH_MO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '###################### where MO Status ##############################################
    Private Shared strMOno_WH_Status As String = "Select " & DocNo & " ," & VersionDocNo & "," & VersionItem & ", " & DocumentDate & ", " &
        " " & ManufacManagmentPersonal & " ," & WOType & "," & IssuanceSystem & "," & Factory & "," & Status & "," & WorkOrderSource & ", " &
        " " & SourceDocNo & "," & ReferanceSuctomer & "," & OldRefereanceDocNo & "," & ParentWorkOrderNumber & "," & PerviousWorkOrderNo & ", " &
        " " & ProductItem & "," & ProductionQty & "," & Unit & "," & RoutingCode & "," & CheckReoutingCode & "," & DeptVendor & ", " &
        " " & CooperativeLocation & "," & PlanStartDate & "," & PlanedCompletionDate & "," & CostCenter & "," & BOMVersion & "," & BOMEffectiveDate & ", " &
        " " & ItemListLotNoPBI & "," & ProjectNo & "," & WBS & "," & Activity & "," & ReasonCode & "," & CriticalRatio & "," & EstimatedStorgeWH & ", " &
        " " & EstimatedStorgeLocation & "," & EstimatedStorgeBacthNo & "," & ManualNo & "," & BondedApprovalNo & "," & MaterialPerparationGen & ", " &
        " " & ProjectNo & "," & WBS & "," & Activity & "," & ReasonCode & "," & CriticalRatio & "," & EstimatedStorgeWH & "," & MaterialPerparationGen & ", " &
        " " & ManualfacturingDistanceConfrim & "," & Frozen & "," & Rework & "," & Allocate & "," & SuppliableVolume & "," & LodEstCompletionDate & ", " &
        " " & ActualStartIssuanceDate & "," & FinalStorgeDate & "," & ManufacturingManagClosingDate & "," & CostClosingDate & "," & ManufManagSetStatus & ", " &
        " " & IssuedSets & "," & StoredPassQuantity & "," & StoredFailureQuantity & "," & SuspenQty & "," & ScarpQty & ", " &
        " " & DataModifyBy & "," & LastModifyDate & "," & DataConfrimationPersonal & "," & DataConfrmedDate & "," & Company & " " &
        " " & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & "," & DataCreateByDept & "," & DataCreateDate & " " &
        " FROM " & tblMO & "  " &
        " where " & Company & " Like 'JINPAO' AND " & Status & " =@pStatus "
    Public Shared Function GetMO_HeaderDeatilStatus(strStatus As String) As DataTable
        Dim Sql As String = strMOno_WH_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFAA", "GetMO_HeaderDeatilStatus", "Sql = strMOno_WH_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMO_HeaderDeatilStatusDataSet(strStatus As String) As DataSet
        Dim Sql As String = strMOno_WH_Status.Replace("@pStatus", "'" & strStatus & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFAA", "GetMO_HeaderDeatilStatusDataSet", "Sql = strMOno_WH_Status", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class