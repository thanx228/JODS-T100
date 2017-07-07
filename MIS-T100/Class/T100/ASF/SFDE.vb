Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDE
    '# Module: ASF
    Private Shared ASF As String = "ASF"
    '# Table: sfde_t
    '# asft311 : Material Issue : Body Item 1 Mat. number summary 
    '''<reamrks># Table 
    ''' 1.Material Issue ##############
    ''' 2.Material Return fro Rework Header : Rework DocNo= JP205-xxxxxxxxx ##############</reamrks>
    Public Shared tblMatIssue_summaryB1 As String = "sfde_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "sfdedocno"
    Public Shared ent As String = "sfdeent"
    Public Shared Site As String = "sfdesite"
    Public Shared LineNo As String = "sfdeseq"
    Public Shared ProductionItem As String = "sfde001"
    Public Shared Consigned_Mat As String = "sfde009"
    Public Shared Unit As String = "sfde003"
    Public Shared AppliedQty As String = "sfde004"
    Public Shared ActualQty As String = "sfde005"

    Private Shared strqlIssueRow As String = "Select " & IssueDocNo & "," & LineNo & "," & ProductionItem & ", " &
    " " & Consigned_Mat & "," & Unit & "," & AppliedQty & "," & ActualQty & " " &
    " from " & tblMatIssue_summaryB1 & " where " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssueHead(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDE", "GetMatIssueHead", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueHeadlDataSet(IssueDoc_No As String) As DataSet
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDE", "GetMatIssueHeadlDataSet", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
