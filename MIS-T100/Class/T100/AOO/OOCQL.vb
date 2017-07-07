Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOCQL
    '# Module : AOO
    Private Shared AOO As String = "AOO"
    '# Table : oocql_t
    '# 1. aimi001 : Item Property Master Group
    '# 2. aeci001 : Process / Operation (oocql_t >> Operation Name,  oocq_t  >> Deatil All)
    '''<remarks>##########Table  ##############</reamrks>
    Public Shared tblOperation As String = "oocql_t"
    Public Shared ent As String = "oocqlent"
    Public Shared IssueSite As String = "oocql001"
    Public Shared OperationID As String = "oocql002"
    Public Shared Operation As String = "oocql004"
    Public Shared ShowData As String = "ShowData"
    Public Shared Language As String = "oocql003"
    Public Shared Description As String = "oocql004"

    '''<remark> Condition Field </remark>
    Public Shared wStandard As String = ent & " ='3' "

    '--Page SalesOrderChangeStatus
    '--SelectOpreationMOOperatLine where DocNo Refesh Data Table
    Private Shared SelectOpreationRefesh As String = "select " & OperationID & "," & Description & " from " & tblOperation & " " &
        " where " & wStandard & "and  " & OperationID & " ='@Opreation' "
    Public Shared Function GetOpreationMOOperatHeadRefesh(ByVal Opreation As String)
        Dim Oral As String = SelectOpreationRefesh
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@Opreation", Opreation)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '--Page SalesOrderChangeStatus
    '--SelectOpreationMOOperatLine where DocNo
    Private Shared SelectOpreation As String = "select " & OperationID & "," & Description & " from " & tblOperation & " " &
        " where " & wStandard & "and  " & OperationID & " ='@Opreation' "
    Public Shared Function GetOpreationMOOperatHead(ByVal Opreation As String)
        Dim Oral As String = SelectOpreation
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@Opreation", Opreation)
        tempDataTable = GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function
















    Private Shared strSqlOperation As String = " select " & OperationID & "," & Operation & " from " & tblOperation & "  " &
" where " & Language & " ='en_US' and " & ent & "='3' and " & IssueSite & "='221' and " & OperationID & " = @OpID  "
    Public Shared Function GetDataOperation(ByVal strWH_OpID As String) As DataTable
        Dim strSQL = strSqlOperation.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataOperation", "strSQL = strSqlOperation", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataOperationDataSet(ByVal strWH_OpID As String) As DataSet
        Dim strSQL = strSqlOperation.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataOperationDataSet", "strSQL = strSqlOperation", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strSqlOperationAll As String = " select " & OperationID & "," & Operation & " from " & tblOperation & "  " &
" where " & Language & " ='en_US' and  " & IssueSite & "='221' and " & ent & "='3'  "
    Public Shared Function GetDataOperationAll() As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlOperationAll, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataOperationAll", "strSqlOperationAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataOperationDataSetAll() As DataSet
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlOperationAll, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataOperationDataSetAll", "strSqlOperationAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '### For UsingControl Item Master Group : Item Property #################################################################
    Private Shared strSqlItemMasterGroup As String = " select " & OperationID & "," & Operation & ", " &
    " " & OperationID & " || ' : ' || " & Operation & " as " & ShowData & " from " & tblOperation & "  " &
    " where " & Language & " ='en_US' and  " & IssueSite & "='200' and " & ent & "='3'  "
    Public Shared Function GetDataItemMasterGroup() As DataTable
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlItemMasterGroup, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataItemMasterGroup", "strSqlItemMasterGroup", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '### Search where Item Master Group : Item Property #################################################################
    Private Shared strSqlWhereItemMasterGroup As String = " select " & OperationID & "," & Operation & ", " &
    " " & OperationID & " || ' : ' || " & Operation & " as " & ShowData & " from " & tblOperation & "  " &
    " where " & Language & " ='en_US' and  " & IssueSite & "='200' and " & ent & "='3'  " &
    " and " & OperationID & "=@pItemMasterCode "
    Public Shared Function GetDataFindItemMasterGroup(ItemMasterCode As String) As DataTable
        Dim strSQL As String = strSqlWhereItemMasterGroup
        strSQL = strSQL.Replace("@pItemMasterCode", "'" & ItemMasterCode & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataFindItemMasterGroup", "strSQL= strSqlWhereItemMasterGroup", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '##################### Trade Term Type ########################################################################
    Private Shared strSqlTradeTermType As String = " select " & OperationID & "," & Operation & " from " & tblOperation & "  " &
" where " & Language & " ='en_US' and  " & IssueSite & "='238' and " & ent & "='3' and " & OperationID & "=@pTradeTermType "
    Public Shared Function GetDataTradeTermType(TradeTermType As String) As DataTable
        Dim strSQL As String = strSqlTradeTermType
        strSQL = strSQL.Replace("@pTradeTermType", "'" & TradeTermType & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQL", "GetDataTradeTermType", "strSQL", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
