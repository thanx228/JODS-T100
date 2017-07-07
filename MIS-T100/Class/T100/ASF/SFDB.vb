Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDB
    '# Module: ASF
    Private Shared ASF As String = "ASF"
    '# Table: sfdb_t
    '# asft311 : Material Issue SET : Body Item Set
    '''<reamrks>### Table
    '''1.Material Issue  ##############
    '''2.Material Return fro Rework Header : Rework DocNo= JP205-xxxxxxxxx ##############</reamrks>
    Public Shared tblMatIssueSet As String = "sfdb_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "sfdbdocno"
    Public Shared ent As String = "sfdbent"
    Public Shared Site As String = "sfdbsite"
    Public Shared WONo As String = "sfdb001"
    Public Shared RunCard As String = "sfdb002"
    Public Shared Position As String = "sfdb003"
    Public Shared OperationID As String = "sfdb004"
    Public Shared Op_Seq As String = "sfdb005"
    Public Shared Expectes_Sets As String = "sfdb006"
    Public Shared Actual_Sets As String = "sfdb007"
    Public Shared Positive_Negative As String = "sfdb008"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    Private Shared strqlIssueRow As String = "Select " & IssueDocNo & "," & RunCard & "," & WONo & "," & OperationID & ",  " &
    " " & Op_Seq & "," & Expectes_Sets & "," & Actual_Sets & "," & Positive_Negative & " " &
    " from " & tblMatIssueSet & " where " & wStandard & " AND " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssueBodySet(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDB", "GetMatIssueBodySet", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueBodySetDataSet(IssueDoc_No As String) As DataSet
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDB", "GetMatIssueBodySetDataSet", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '###### Where MO_WO_No = ? #########################################################################
    Private Shared strqlIssueWhereMO_No As String = "Select " & IssueDocNo & "," & RunCard & "," & WONo & "," & OperationID & ",  " &
" " & Op_Seq & "," & Expectes_Sets & "," & Actual_Sets & "," & Positive_Negative & " " &
" from " & tblMatIssueSet & " where  " & wStandard & " AND " & WONo & "=@pWO_No "
    Public Shared Function GetMatIssueSet_WhereMO(strWO_No As String) As DataTable
        Dim Sql As String = strqlIssueWhereMO_No.Replace("@pWO_No", "'" & strWO_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDB", "GetMatIssueSet_WhereMO", "Sql = strqlIssueWhereMO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

