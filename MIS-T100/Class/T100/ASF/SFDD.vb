Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDD
    '# Module: ASF
    Private Shared ASF As String = "ASF"
    '# Table: sfdd_t
    '# asft311 : Material Issue : Body at. Distribution Requirement Details Body2 
    '''<reamrks># Table
    ''' 1.Material Issue ##############
    ''' 2.Material Return fro Rework Header : Rework DocNo= JP205-xxxxxxxxx ##############</reamrks>
    Public Shared tblMatIssue_DisDetailsB2 As String = "sfdd_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "sfdddocno"
    Public Shared ent As String = "sfddent"
    Public Shared Site As String = "sfddsite"
    Public Shared ItemOrder As String = "sfddseq"
    Public Shared ProductionItem As String = "sfdd001"
    Public Shared UsageProb As String = "sfdd002"
    Public Shared WH As String = "sfdd003"
    Public Shared Location As String = "sfdd004"
    Public Shared LotNo As String = "sfdd005"
    Public Shared InventoryManagUint As String = "sfdd010"
    Public Shared Unit As String = "sfdd006"
    Public Shared Quantity As String = "sfdd007"


    Private Shared strqlIssueDisDetailB2 As String = "Select " & IssueDocNo & "," & ItemOrder & ", " &
    " " & ProductionItem & "," & UsageProb & "," & WH & "," & Location & "," & LotNo & ", " &
    " " & InventoryManagUint & "," & Unit & " " &
    " from " & tblMatIssue_DisDetailsB2 & " where " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssueDisDetailB2(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueDisDetailB2.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDD", "GetMatIssueDisDetailB2", "Sql = strqlIssueDisDetailB2", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueDisDetailB2_DataSet(IssueDoc_No As String) As DataSet
        Dim Sql As String = strqlIssueDisDetailB2.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDD", "GetMatIssueDisDetailB2_DataSet", "Sql = strqlIssueDisDetailB2", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
