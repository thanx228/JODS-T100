Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDF
    '# Module: ASF
    Private Shared ASF As String = "ASF"
    '# Table: sfdf_t
    '# asft311 : Material Issue : Body Item 2 Mat. number summary 
    '''<reamrks># Table 
    ''' 1.Material Issue ##############
    ''' 2.Material Return fro Rework Header : Rework DocNo= JP205-xxxxxxxxx ##############
    '''  </reamrks>
    Public Shared tblMatIssue_summaryB2 As String = "sfdf_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "sfdfdocno"
    Public Shared ent As String = "sfdfent"
    Public Shared Site As String = "sfdfsite"
    Public Shared ItemOrder As String = "sfdfseq"
    Public Shared ProductionItem As String = "sfdf001"
    Public Shared UsageProb As String = "sfdf002"
    Public Shared WH As String = "sfdf003"
    Public Shared Location As String = "sfdf004"
    Public Shared LotNo As String = "sfdf005"
    Public Shared InventoryManagUnit As String = "sfdf010"
    Public Shared Unit As String = "sfdf006"
    Public Shared Ouantity As String = "sfdf007"


    Private Shared strqlIssueRow As String = "Select " & IssueDocNo & "," & ItemOrder & "," & ProductionItem & ", " &
    " " & UsageProb & "," & WH & "," & Location & "  from " & tblMatIssue_summaryB2 & " where " & IssueDocNo & "=@IssueDocNo "
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
            GetPageError.GetClassT100(ASF, "SFDF", "GetMatIssueHead", "Sql = strqlIssueRow", ex.Message)
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
            GetPageError.GetClassT100(ASF, "SFDF", "GetMatIssueHeadlDataSet", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
