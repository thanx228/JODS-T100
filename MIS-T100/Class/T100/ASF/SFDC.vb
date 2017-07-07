Imports System.Configuration
Imports System.Data.OracleClient
Public Class SFDC
    '# Module: ASF
    Private Shared ASF As String = "ASF"
    '# Table: sfdc_t
    '# asft311 : Material Issue : Body Item
    '''<reamrks># Table Material Issue(Body Mat. Distribution Requirement and Details Body1) ##############</reamrks>
    Public Shared tblMatIssueDistribution As String = "sfdc_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "sfdcdocno"
    Public Shared ent As String = "sfdcent"
    Public Shared Site As String = "sfdcsite"
    Public Shared WONo As String = "sfdc001"
    Public Shared WOLineNo As String = "sfdc002"
    Public Shared WOLineNoSN As String = "sfdc003"
    Public Shared RequirementItem_No As String = "sfdc004"
    Public Shared Unit As String = "sfdc006"
    Public Shared AppliciedQty As String = "sfdc007"
    Public Shared ActualQty As String = "sfdc008"
    Public Shared SpecifiesWH As String = "sfdc012"
    Public Shared SpecifiesLocation As String = "sfdc013"
    Public Shared AssignNo As String = "sfdc014"
    Public Shared InventoryManagCharac As String = "sfdc016"
    Public Shared ReasonCode As String = "sfdc015"

    Private Shared strqlIssueRow As String = "Select " & IssueDocNo & "," & WONo & "," & WOLineNo & ", " &
    " " & RequirementItem_No & "," & Unit & "," & AppliciedQty & "," & ActualQty & "," & SpecifiesWH & ", " &
    " " & SpecifiesLocation & "," & AssignNo & "," & AssignNo & "," & InventoryManagCharac & "," & ReasonCode & " " &
    " from " & tblMatIssueDistribution & " where " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssue(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDC", "GetMatIssue", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueDataSet(IssueDoc_No As String) As DataSet
        Dim Sql As String = strqlIssueRow.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDC", "GetMatIssueDataSet", "Sql = strqlIssueRow", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    '# where MO_No
    Private Shared strqlIssueRowWhereMO_No As String = "Select " & IssueDocNo & "," & WONo & "," & WOLineNo & ", " &
   " " & RequirementItem_No & "," & Unit & "," & AppliciedQty & "," & ActualQty & "," & SpecifiesWH & ", " &
   " " & SpecifiesLocation & "," & AssignNo & "," & AssignNo & "," & InventoryManagCharac & "," & ReasonCode & " " &
   " from " & tblMatIssueDistribution & " where " & WONo & "=@MO_NO "
    Public Shared Function GetMatIssueBy_MONo(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueRowWhereMO_No.Replace("@MO_NO", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDC", "GetMatIssueBy_MONo", "Sql = strqlIssueRowWhereMO_No", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '# where Custom Using Parameter
    Private Shared strqlIssueRowWhereMulti As String = "Select " & IssueDocNo & "," & WONo & "," & WOLineNo & ", " &
   " " & RequirementItem_No & "," & Unit & "," & AppliciedQty & "," & ActualQty & "," & SpecifiesWH & ", " &
   " " & SpecifiesLocation & "," & AssignNo & "," & AssignNo & "," & InventoryManagCharac & "," & ReasonCode & " " &
   " from " & tblMatIssueDistribution & " where @pWhereCustomUsing "
    Public Shared Function GetMatIssueBy_Multi(IssueWhereMutil As String) As DataTable
        Dim Sql As String = strqlIssueRowWhereMulti
        Dim pWhereCustomUsing As String = IssueWhereMutil
        Sql = Sql.Replace("@pWhereCustomUsing", pWhereCustomUsing)
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ASF, "SFDC", "GetMatIssueBy_Multi", "Sql = strqlIssueRowWhereMulti", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
