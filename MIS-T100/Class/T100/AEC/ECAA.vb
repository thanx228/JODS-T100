Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECAA

    Private Shared AEC As String = "AEC"
    ''' <remarks>
    ''' # Module : AEC
    ''' # Table : ecaa_t
    ''' # aeci001 : Workcenter or Workstation
    ''' </remarks>
    '''<reamrks>##########Table Workstation Workcenter ##############</reamrks>
    Public Shared tblWorkcenter As String = "ecaa_t"
    '''<reamrks> # Field </reamrks>
    Public Shared Site As String = "ecaasite"
    Public Shared ent As String = "ecaaent"
    Public Shared WorkcenterID As String = "ecaa001"
    Public Shared Workcenter As String = "ecaa002"
    Public Shared ShowWorkstation As String = "Workstation"
    Public Shared Status As String = "ecaastus"
    Public Shared CostCenter As String = "ecaa003"
    Public Shared Capacity_Type As String = "ecaa004"
    Public Shared WorkingCalendar As String = "ecaa005"
    Public Shared DailyLaborCapacity As String = "ecaa006"
    Public Shared DailyMachineCapacity As String = "ecaa007"
    Public Shared StdLaborEfficiency As String = "ecaa008"
    Public Shared StdMachineLoading As String = "ecaa009"
    Public Shared StdLaborCost As String = "ecaa010"
    Public Shared StdManufacturingCost As String = "ecaa011"
    '''<reamrks> Condition Field </reamrks>
    Public Shared wStandard As String = Site & " ='JINPAO' and " & Status & " ='Y' and " & ent & "='3' "

    '--Page SalesOrderChangeStatus
    '--SelectWorkcenter where Workcenter
    Private Shared SelectWorkcenter As String = "select " & WorkcenterID & "," & Workcenter & " from " & tblWorkcenter & " " &
        " where " & wStandard & "and  " & WorkcenterID & " ='@Workcenter'"
    Public Shared Function GetWorkcenter(ByVal WorkStation As String)
        Dim Oral As String = SelectWorkcenter
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@Workcenter", WorkStation)
        tempDataTable = GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function



    '####################### Workstation All One Work Center ########################################
    Private Shared strSqlWorkcenterDetailAll As String = "select " & WorkcenterID & "," & Workcenter & "," & CostCenter & ", " &
    " " & Capacity_Type & "," & WorkingCalendar & "," & DailyLaborCapacity & "," & DailyMachineCapacity & "," & StdLaborEfficiency & ", " &
    " " & StdMachineLoading & "," & StdLaborCost & "," & StdManufacturingCost & " " &
    " from " & tblWorkcenter & " where " & wStandard & "  "
    '''<remarks># Workstation Datail all DataTable </remarks>
    Public Shared Function GetWorkcenterDetailAll_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWorkcenterDetailAll, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenterDetailAll_Table", "strSqlWorkcenterDetailAll", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Workstation Detail all DataSet </remarks>
    Public Shared Function GetWorkcenterDetailAll_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWorkcenterDetailAll, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenterDetailAll_DataSet", "strSqlWorkcenterDetailAll", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '####################### Search Find One Workstation ########################################
    Private Shared strSqlFindWorkcenterDetail As String = "select " & WorkcenterID & "," & Workcenter & "," & CostCenter & ", " &
    " " & Capacity_Type & "," & WorkingCalendar & "," & DailyLaborCapacity & "," & DailyMachineCapacity & "," & StdLaborEfficiency & ", " &
    " " & StdMachineLoading & "," & StdLaborCost & "," & StdManufacturingCost & " " &
    " from " & tblWorkcenter & " where " & wStandard & " and " & WorkcenterID & "=@pWorkcenter "
    '''<remarks>#  Search Find Workstation DataTable </remarks>
    Public Shared Function GetFindWorkcenterDetail_Table(Workcenter As String) As DataTable
        Dim Sql As String = strSqlFindWorkcenterDetail.Replace("@pWorkcenter", "'" & Workcenter & "'")
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetFindWorkcenterDetail_Table", "Sql = strSqlFindWorkcenterDetail", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks>#  Search Find Workstation DataSet </remarks>
    Public Shared Function GetFindWorkcenterDetail_DataSet(Workcenter As String) As DataSet
        Dim Sql As String = strSqlFindWorkcenterDetail.Replace("@pWorkcenter", "'" & Workcenter & "'")
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetFindWorkcenterDetail_DataSet", "Sql = strSqlFindWorkcenterDetail", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '####################### Multi Select for CheckBoxList ########################################
    Private Shared strSqlWorkcenterDetailMultiAll As String = "select " & WorkcenterID & "," & Workcenter & "," & CostCenter & ", " &
    " " & Capacity_Type & "," & WorkingCalendar & "," & DailyLaborCapacity & "," & DailyMachineCapacity & "," & StdLaborEfficiency & ", " &
    " " & StdMachineLoading & "," & StdLaborCost & "," & StdManufacturingCost & " " &
    " from " & tblWorkcenter & " where " & wStandard & "  "
    '''<remarks># Multi Select Workstation Datail all DataTable </remarks>
    Public Shared Function GetWorkcenterDetailMultiAll_Table(MultiWorkcenter As String) As DataTable
        Dim Sql As String = strSqlWorkcenterDetailMultiAll
        Sql = Sql & " and " & WorkcenterID & " In(" & [String].Join("','", MultiWorkcenter) & "')"
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenterDetailMultiAll_Table", "Sql = strSqlWorkcenterDetailMultiAll", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
        'Return Sql
    End Function
    '''<remarks># Multi Select for CheckBoxList Detail all DataSet </remarks>
    Public Shared Function GetWorkcenterDetailMultiAll_DataSet(MultiWorkcenter As String) As DataSet
        Dim Sql As String = strSqlWorkcenterDetailMultiAll
        Sql = Sql & " and " & WorkcenterID & " In(" & [String].Join("','", MultiWorkcenter) & "')"
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenterDetailMultiAll_DataSet", "Sql = strSqlWorkcenterDetailMultiAll", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '####################### for UsingControl Select Workstation ########################################
    Private Shared strSqlWorkcenter As String = "select " & WorkcenterID & "," & WorkcenterID & " || ' : ' || " & Workcenter & " as " & ShowWorkstation & " " &
    " from " & tblWorkcenter & " where " & wStandard & "  order by  " & WorkcenterID
    '''<remarks># Select Workstation  DataTable </remarks>
    Public Shared Function GetWorkcenter_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWorkcenter, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenter_Table", "strSqlWorkcenter", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Select Workstation DataSet </remarks>
    Public Shared Function GetWorkcenter_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWorkcenter, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenter_DataSet", "strSqlWorkcenter", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '####################### for UsingControl Select Workstation Where ########################################
    Private Shared strSqlWorkcenterWH As String = "select " & WorkcenterID & "," & WorkcenterID & " || ' : ' || " & Workcenter & " as " & ShowWorkstation & "," & Workcenter & " " &
    " from " & tblWorkcenter & " where " & wStandard & "  "
    '''<remarks># Select Workstation  DataTable </remarks>
    Public Shared Function GetWorkcenterWhere_Table(MultiWorkcenter As String) As DataTable
        Dim Sql As String = strSqlWorkcenterWH
        Sql = Sql & " and " & WorkcenterID & " In(" & "'" & [String].Join("','", MultiWorkcenter) & "')"
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECAA", "GetWorkcenterWhere_Table", "Sql = strSqlWorkcenterWH", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
        'Return Sql
    End Function


    Private Shared dtAdapter As OracleDataAdapter
    Private Shared objConn As OracleConnection
    Private Shared Sub T100Close()
        If objConn.State = ConnectionState.Open Then objConn.Close()
        dtAdapter = Nothing
        objConn = Nothing
    End Sub

End Class
