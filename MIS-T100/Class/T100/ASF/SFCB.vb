Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFCB
    ''' <remarks>
    '''# Module T100 : ASF 
    ''' # Table : sfcb_t
    '''# asft301 : Maintain MO Operation : Body Item
    ''' # asft004 : Workstation WIP Status  >> Tab1:  Waork Station
    ''' # asft004 : Workstation WIP Status  >> Tab2:  Operation No.
    ''' </remarks>
    Private Shared ASF As String = "ASF"
    '''<reamrks>##########Table MO Process Item ##############</reamrks>
    Public Shared tblMOprocessItem_SFCB As String = "sfcb_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> # Tab Progess Schedule </reamrks>
    Public Shared WONo As String = "sfcbdocno"
    Public Shared ent As String = "sfcbent"
    Public Shared Site As String = "sfcbsite"
    Public Shared RunCard As String = "sfcb001"
    Public Shared LineNo As String = "sfcb002"
    Public Shared OperationID As String = "sfcb003"
    'Public Shared OpDesc As String = "OpDesc"
    Public Shared OperationSeq As String = "sfcb004"
    Public Shared WorkStation As String = "sfcb011"
    Public Shared StardardOutput As String = "sfcb027"
    Public Shared WIP As String = "sfcb050"
    Public Shared GoodTransferIn As String = "sfcb028"
    Public Shared GoodTransferOut As String = "sfcb033"
    Public Shared RecylingTransferIn As String = "sfcb030"
    Public Shared ReworkTrsIn As String = "sfcb029"
    Public Shared SplitTransferIn As String = "sfcb031"
    Public Shared CombineTransferIn As String = "sfcb032"
    Public Shared TransferOutforRework As String = "sfcb034"
    Public Shared RecylingTransferOut As String = "sfcb035"
    Public Shared DirectScarp As String = "sfcb036"
    Public Shared DirectSuspend As String = "sfcb037"
    Public Shared SplitTransferOut As String = "sfcb038"
    Public Shared CombineTransferOut As String = "sfcb039"
    Public Shared SubcontactFinishQty As String = "sfcb041"
    Public Shared CountQty As String = "sfcb043"
    Public Shared PendingMoveInQty As String = "sfcb046"
    Public Shared PendingMoveOutQty As String = "sfcb049"
    Public Shared PendingPQCQty As String = "sfcb051"
    '''<reamrks> # Tab Progess Project </reamrks>
    Public Shared Groups As String = "sfcb006"
    Public Shared PerviousOperation As String = "sfcb007"
    Public Shared LastStationOpSeq As String = "sfcb008"
    Public Shared NextOperation As String = "sfcb009"
    Public Shared NextStationOpSeq As String = "sfcb010"
    Public Shared WorkstationTo As String = "sfcb011"
    Public Shared FixedLaborHours As String = "sfcbua001"
    Public Shared FixedLaborHours2 As String = "sfcb023"
    Public Shared StandradLaborHours As String = "sfcbua002"
    Public Shared StandradLaborHoursh As String = "sfcbua005"
    Public Shared StandradLaborHoursm As String = "sfcbua006"
    Public Shared StandradLaborHours2 As String = "sfcb024"
    Public Shared MachineHours As String = "sfcbua003"
    Public Shared FixMachineHours As String = "sfcb025"
    Public Shared StandradMachineHours As String = "sfcbua004"
    Public Shared StandradMachineHoursh As String = "sfcbua007"
    Public Shared StandradMachineHourss As String = "sfcbua008"
    Public Shared StandradMachineHours2 As String = "sfcb026"
    Public Shared PlanStartDate As String = "sfcb044"
    Public Shared PlannedCompletionDate As String = "sfcb045"
    Public Shared AllowOutsourcing As String = "sfcb012"
    Public Shared MainProcessPlant As String = "sfcb013"
    ' Public Shared TradingPartner As String = Jion Table Now
    Public Shared MoveIn As String = "sfcb014" ' Checked
    Public Shared CheckIn As String = "sfcb015" ' Checked
    Public Shared WorkReportStation As String = "sfcb016" ' Checked
    Public Shared PQC As String = "sfcb017" 'Checked
    Public Shared CheckOut As String = "sfcb018" 'Checked
    Public Shared MoveOut As String = "sfcb019" ' Checked
    Public Shared TransferInUnit As String = "sfcb052"
    Public Shared TransferUnitConversionRateNemberator As String = "sfcb053"
    Public Shared TransferUnitConversionRateDemominator As String = "sfcb054"
    Public Shared TransferOutUnit As String = "sfcb020"
    Public Shared UnitConversionRateNemberator As String = "sfcb021"
    Public Shared UnitConversionRateNemberator2 As String = "sfcb022"
    Public Shared RecylingStation As String = "sfcb055" ' Checked
    '' Public Shared MemoDescription As String = Join Table Now

    '''<reamrks> Condition Field </reamrks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '--Page SalesOrderChangeStatus
    '--SelectDocNoMOOperatLine where DocNo
    Private Shared SelectDocNoMOOperatLine As String = "select " & WONo & "," & WorkStation & "," & OperationID & "  from " & tblMOprocessItem_SFCB & " " &
        " where " & wStandard & " and  " & WONo & " ='@WONo'"
    Public Shared Function GetDocNoMOOperatHead(ByVal DocNoMOOperatHead As String)
        Dim Oral As String = SelectDocNoMOOperatLine
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@WONo", DocNoMOOperatHead)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function






















    '##################### MO Item Process For all Feild.   Where WO No. = ? ) #############################################################
    Private Shared strSqlRowProcessItemAll As String = "SELECT " & LineNo & "," & OperationID & "," & OOCQL.Operation & "," &
" " & OperationSeq & "," & WorkStation & "," & StardardOutput & "," & WIP & "," & GoodTransferIn & "," & GoodTransferOut & "," & RecylingTransferIn & ", " &
" " & ReworkTrsIn & "," & SplitTransferIn & "," & CombineTransferIn & "," & TransferOutforRework & "," & RecylingTransferOut & "," & DirectScarp & ", " &
" " & DirectSuspend & "," & SplitTransferOut & "," & CombineTransferOut & "," & SubcontactFinishQty & "," & CountQty & "," & PendingMoveInQty & "," & PendingMoveOutQty & ", " &
" " & PendingPQCQty & "," & Groups & "," & PerviousOperation & "," & LastStationOpSeq & "," & NextOperation & "," & NextStationOpSeq & "," & WorkstationTo & "," & FixedLaborHours & ", " &
" " & FixedLaborHours2 & "," & StandradLaborHours & "," & StandradLaborHoursh & "," & StandradLaborHoursm & "," & StandradLaborHours2 & "," & MachineHours & ", " &
" " & FixMachineHours & "," & StandradMachineHours & "," & StandradMachineHoursh & "," & StandradMachineHourss & "," & StandradMachineHours2 & ", " &
" " & PlanStartDate & "," & PlannedCompletionDate & "," & AllowOutsourcing & "," & MainProcessPlant & "," & MoveIn & "," & CheckIn & "," & WorkReportStation & ", " &
" " & PQC & "," & CheckOut & "," & MoveOut & "," & TransferInUnit & "," & TransferUnitConversionRateNemberator & "," & TransferUnitConversionRateDemominator & ", " &
" " & TransferOutUnit & "," & UnitConversionRateNemberator & "," & UnitConversionRateNemberator2 & "," & RecylingStation & "," & SFCA.CompletedQty & "  " &
" FROM  " & SFCB.tblMOprocessItem_SFCB & "  " &
" LEFT JOIN  " & SFCA.tblMO_Detail & " On " & tblMOprocessItem_SFCB & "." & WONo & " = " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " " &
" LEFT JOIN  " & OOCQL.tblOperation & " On " & tblMOprocessItem_SFCB & "." & OperationID & " = " & OOCQL.tblOperation & "." & OOCQL.OperationID & " " &
" where " & tblMOprocessItem_SFCB & "." & WONo & " =@pMoNO " &
" And " & OOCQL.tblOperation & "." & OOCQL.Language & " ='en_US' And " & OOCQL.IssueSite & "='221' " &
" And " & OOCQL.tblOperation & "." & OOCQL.ent & "='3' Order by " & LineNo & " "

    Public Shared Function GetDataRowsProcessItemAll(ByVal strWH_MoNo As String) As DataTable
        Dim strSQL As String = strSqlRowProcessItemAll
        strSQL = strSqlRowProcessItemAll.Replace("@pMoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataRowsProcessItemAll", "strSQL = strSqlRowProcessItemAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataRowsProcessItemAllDataSet(ByVal strWH_MoNo As String) As DataSet
        Dim strSQL As String = strSqlRowProcessItemAll
        strSQL = strSqlRowProcessItemAll.Replace("@pMoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataRowsProcessItemAllDataSet", "strSQL = strSqlRowProcessItemAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '##################### MO Item Process Where Wo No. = ?  #############################################################
    Private Shared strSqlRowProcessBy_MO_DocNO As String = "SELECT " & WONo & "," & LineNo & "," & OperationID & "," & OOCQL.Operation & ", " &
" " & WorkStation & "," & PlanStartDate & "," & PlannedCompletionDate & "," & StardardOutput & ", " &
" " & WIP & "," & GoodTransferIn & "," & GoodTransferOut & ", " &
" " & ReworkTrsIn & " ," & DirectScarp & " " &
" FROM  " & tblMOprocessItem_SFCB & "  " &
" LEFT JOIN  " & SFCA.tblMO_Detail & " On " & tblMOprocessItem_SFCB & "." & WONo & " = " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " " &
" LEFT JOIN  " & OOCQL.tblOperation & " On " & tblMOprocessItem_SFCB & "." & OperationID & " = " & OOCQL.tblOperation & "." & OOCQL.OperationID & " " &
" where " & tblMOprocessItem_SFCB & "." & WONo & " =@MoNO " &
" And " & OOCQL.tblOperation & "." & OOCQL.Language & " ='en_US' And " & OOCQL.IssueSite & "='221' " &
" And " & OOCQL.tblOperation & "." & OOCQL.ent & "='3' Order by " & LineNo & " "
    Public Shared Function GetDataMOProcess_By_MO_DocNo(ByVal strWH_MoNo As String) As DataTable
        Dim strSQL = strSqlRowProcessBy_MO_DocNO.Replace("@MoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataMOProcess_By_MO_DocNo", "strSQL = strSqlRowProcessBy_MO_DocNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataMOProcess_By_MO_DocNo_DataSet(ByVal strWH_MoNo As String) As DataSet
        Dim strSQL = strSqlRowProcessBy_MO_DocNO.Replace("@MoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataMOProcess_By_MO_DocNo_DataSet", "strSQL = strSqlRowProcessBy_MO_DocNO", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '##################### MO Item Process Where Wo No. = ? ( for = Url = 1. /Planing/PlanByMO.aspx ) #############################################################
    Private Shared strSqlRowProcessItem As String = "SELECT " & LineNo & "," & OperationID & "," & OOCQL.Operation & ", " &
" " & WorkStation & "," & PlanStartDate & "," & PlannedCompletionDate & "," & StardardOutput & ", " &
" " & WIP & "," & GoodTransferIn & "," & GoodTransferOut & ", " &
" " & ReworkTrsIn & "," & TransferOutforRework & " ," & DirectScarp & " " &
" FROM  " & tblMOprocessItem_SFCB & "  " &
" LEFT JOIN  " & SFCA.tblMO_Detail & " On " & tblMOprocessItem_SFCB & "." & WONo & " = " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " " &
" LEFT JOIN  " & OOCQL.tblOperation & " On " & tblMOprocessItem_SFCB & "." & OperationID & " = " & OOCQL.tblOperation & "." & OOCQL.OperationID & " " &
" where " & tblMOprocessItem_SFCB & "." & WONo & " =@MoNO " &
" And " & OOCQL.tblOperation & "." & OOCQL.Language & " ='en_US' And " & OOCQL.IssueSite & "='221' " &
" And " & OOCQL.tblOperation & "." & OOCQL.ent & "='3' Order by " & LineNo & " "
    Public Shared Function GetDataRowsProcessItem(ByVal strWH_MoNo As String) As DataTable
        Dim strSQL = strSqlRowProcessItem.Replace("@MoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataRowsProcessItem", "strSQL = strSqlRowProcessItem", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataRowsProcessItemDataSet(ByVal strWH_MoNo As String) As DataSet
        Dim strSQL = strSqlRowProcessItem.Replace("@MoNO", "'" & strWH_MoNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataRowsProcessItemDataSet", "strSQL = strSqlRowProcessItem", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


    '##################### Custome Where Select Alone DataTable Where Wo No. = ?  #############################################################
    Private Shared strSqlRowProcessItemWhereCustom As String = "SELECT " & LineNo & "," & OperationID & ", " &
" " & WorkStation & "," & PlannedCompletionDate & "," & StardardOutput & ", " &
" " & WIP & "," & GoodTransferIn & "," & GoodTransferOut & ", " &
" " & PlanStartDate & "," & ReworkTrsIn & ", " &
" " & DirectScarp & "  FROM  " & tblMOprocessItem_SFCB & "  " &
" LEFT JOIN  " & SFCA.tblMO_Detail & " On " & tblMOprocessItem_SFCB & "." & WONo & " = " & SFCA.tblMO_Detail & "." & SFCA.DocNo & " " &
" where @pWhereCustom " &
" And " & Site & " ='JINPAO' " &
" And " & ent & "='3' Order by " & LineNo & " "
    Public Shared Function GetDataRowsProcessItemWhereCustom(ByVal strWH_MoNo As String) As DataTable
        Dim Sql As String = strSqlRowProcessItemWhereCustom
        Dim pWhereCustom As String = strWH_MoNo
        Sql = Sql.Replace("@pWhereCustom", pWhereCustom)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFCB", "GetDataRowsProcessItemWhereCustom", "strSQL = strSqlRowProcessItemWhereCustom", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try

    End Function
End Class

