Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class OOCQ
    '# Module : AOO
    Private Shared AOO As String = "AOO"
    '# Table : oocq_t
    '# aeci004 : Process / Operation : (oocq_t  >> Deatil All , oocql_t >> Operation Name )
    '''<reamrks>##########Table Operation Process Detail ##############</reamrks>
    Public Shared tblOperationSummary As String = "oocq_t"
    '''<reamrks> # Field </reamrks>
    Public Shared AppClass As String = "oocql001"
    Public Shared OperationID As String = "oocql002"
    Public Shared Operation As String = "oocql004"
    Public Shared Workstation As String = "oocqua001"
    Public Shared Status As String = "oocqstus"
    Public Shared Fixed_Housr As String = "oocqud001"
    Public Shared FixedMachine_Hours As String = " oocqud002"
    Public Shared Subcontacting As String = "oocqud003"
    Public Shared Mnemonic As String = "oocqud005"
    Public Shared ReferenceField_6 As String = "oocqud009"
    Public Shared DataOwner As String = "oocqownid"
    Public Shared DataOwner_Dept As String = "oocqowndp"
    Public Shared DataCreate_By As String = "oocqcrtid"
    Public Shared DataCreate_Date As String = "oocqcrtdt"
    Public Shared DataModify_By As String = "oocqmodid"
    'Public Shared xxx As String = ""
    'Public Shared xxx As String = ""
    'Public Shared xxx As String = ""

    '''<reamrks> Condition Field </reamrks>
    Private Shared cqent As String = "oocqent"
    Private Shared cqlent As String = "oocqlent"
    Private Shared Language As String = "oocql003"


    Private Shared strSqlOperationFindAll As String = " select " & OperationID & "," & Operation & "," & Workstation & "," & Status & ", " &
    " " & Fixed_Housr & "," & FixedMachine_Hours & "," & Subcontacting & "," & Mnemonic & "," & ReferenceField_6 & ", " & DataOwner & ", " &
    " " & DataOwner_Dept & "," & DataCreate_By & "," & DataCreate_Date & "," & DataModify_By & " " &
    " from " & tblOperationSummary & " INNER JOIN oocql_t On oocql_t." & OperationID & " = oocq_t.oocq002 " &
    " where " & Language & " ='en_US' and " & cqent & "='3'and " & cqlent & "='3' and " & AppClass & "='221' " &
    " And " & Status & " ='Y' and (" & Workstation & " IS NOT NULL) order by " & OperationID & " ASC "
    Public Shared Function GetDataOperationAll() As DataTable
        ' Dim strSQL = strSqlOperation.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlOperationFindAll, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQ", "GetDataOperationAll", "strSqlOperationFindAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataOperationAll_DataSet() As DataSet
        ' Dim strSQL = strSqlOperation.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSqlOperationFindAll, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQ", "GetDataOperationAll_DataSet", "strSqlOperationFindAll", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    Private Shared strSqlOperationFind As String = " select " & OperationID & "," & Operation & "," & Workstation & "," & Status & ", " &
   " " & Fixed_Housr & "," & FixedMachine_Hours & "," & Subcontacting & "," & Mnemonic & "," & ReferenceField_6 & ", " & DataOwner & ", " &
   " " & DataOwner_Dept & "," & DataCreate_By & "," & DataCreate_Date & "," & DataModify_By & " " &
   " from " & tblOperationSummary & " INNER JOIN oocql_t On oocql_t." & OperationID & " = oocq_t.oocq002 " &
   " where " & Language & " ='en_US' and " & cqent & "='3'and " & cqlent & "='3' and " & AppClass & "='221' and " & OperationID & "=@OpID " &
   " And " & Status & " ='Y' and (" & Workstation & " IS NOT NULL) order by " & OperationID & " ASC "
    Public Shared Function GetDataOperationFind(strWH_OpID As String) As DataTable
        Dim strSQL = strSqlOperationFind.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQ", "GetDataOperationFind", "strSQL = strSqlOperationFind", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetDataOperationFind_DataSet(strWH_OpID As String) As DataSet
        Dim strSQL = strSqlOperationFind.Replace("@OpID", "'" & strWH_OpID & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AOO, "OOCQ", "GetDataOperationFind_DataSet", "strSQL = strSqlOperationFind", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
