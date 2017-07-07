Imports System.Data.OracleClient

Public Class INAA
    ''' <remarks>
    '''  # Module : AIN
    '''  # Table : inaa
    '''  # aeci001 : Workcenter
    '''  # Function for Warehourse/Sotre Location
    ''' </remarks>>
    Private Shared AIN As String = "AIN"
    '''<reamrks>##########Table Warehouse/Store Location ##############</reamrks>
    Public Shared tblWarehouse As String = "inaa_t"
    '''<reamrks> # Field </reamrks>
    '''<reamrks> Rename Field </reamrks>
    Public Shared WharehouseID As String = "inaa001"
    Public Shared Warehouse As String = "inayl003"
    Public Shared ShowpWarehouse As String = "ShowpWarehouse"
    '''<reamrks> Condition Where and Field</reamrks>
    Public Shared Site As String = "inaasite"
    Public Shared ent As String = "inaylent"
    Public Shared Status As String = "inaastus"

    '###################### Show Warehouse ###################################################################
    Private Shared strSqlWarehosue As String = " Select " & WharehouseID & ",inayl_t." & Warehouse & ", " &
            " " & WharehouseID & " || ' : ' || " & Warehouse & " as " & ShowpWarehouse & " from " & tblWarehouse & " LEFT JOIN inayl_t " &
            " ON " & tblWarehouse & "." & WharehouseID & " = inayl_t.inayl001 " &
            " where (" & Site & " ='JINPAO' and " & Status & " ='Y' and inayl_t.inayl002 ='en_US') " &
            " and (" & WharehouseID & " <>'TEST' and " & WharehouseID & " <>'TEST01') and " & ent & " ='3' " &
            " group by " & WharehouseID & ",inayl_t." & Warehouse & " " &
            " Order by inaa_t." & WharehouseID & " "
    '''<remarks># Warehouse DataTable </remarks>
    Public Shared Function GetWarehouse_Table() As DataTable
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWarehosue, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAA", "GetWarehouse_Table", "strSqlWarehosue", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Warehouse DataSet </remarks>
    Public Shared Function GetWarehouse_DataSet() As DataSet
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlWarehosue, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAA", "GetWarehouse_DataSet", "strSqlWarehosue", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function

    '###################### Search Find Warehouse where WH_ID = ?  ###################################################################
    Private Shared strSqlWarehosueFind As String = " Select " & WharehouseID & ",inayl_t." & Warehouse & ", " &
            " " & WharehouseID & " || ' : ' || " & Warehouse & " as " & ShowpWarehouse & " from " & tblWarehouse & " LEFT JOIN inayl_t " &
            " ON " & tblWarehouse & "." & WharehouseID & " = inayl_t.inayl001 " &
            " where (" & Site & " ='JINPAO' and " & Status & " ='Y' and inayl_t.inayl002 ='en_US') " &
            " and (" & WharehouseID & " <>'TEST' and " & WharehouseID & " <>'TEST01') and " & ent & " ='3' and " & WharehouseID & " =@pWH " &
            " group by " & WharehouseID & ",inayl_t." & Warehouse & " " &
            " Order by inaa_t." & WharehouseID & " "
    '''<remarks># Warehouse DataTable </remarks>
    Public Shared Function GetWarehouseFind_Table(StrWarehose As String) As DataTable
        Dim Sql As String = strSqlWarehosueFind.Replace("@pWH", "'" & StrWarehose & "'")
        Dim dt As New DataTable
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAA", "GetWarehouseFind_Table", "Sql = strSqlWarehosueFind", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function
    '''<remarks># Warehouse DataSet </remarks>
    Public Shared Function GetWarehouseFind_DataSet(StrWarehose As String) As DataSet
        Dim Sql As String = strSqlWarehosueFind.Replace("@pWH", "'" & StrWarehose & "'")
        Dim ds As New DataSet
        objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***'
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAA", "GetWarehouseFind_DataSet", "Sql = strSqlWarehosueFind", ex.Message)
            Return Nothing
        Finally
            T100Close()
        End Try
    End Function


    Private Shared dtAdapter As OracleDataAdapter
    Private Shared objConn As OracleConnection
    Private Shared Sub T100Close()
        If objConn.State = ConnectionState.Open Then objConn.Close()
        dtAdapter = Nothing
        objConn = Nothing
    End Sub


End Class
