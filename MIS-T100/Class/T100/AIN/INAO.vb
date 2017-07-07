Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class INAO
    '# Module : AIN
    Private Shared AIN As String = "AIN"
    '# Table : inao_t
    '# aqct300 : Maintain QC Inspection Record : Item - Body Tab4 >> Manufacturing bacth number
    '# asft311 : Material Issue  : Body Item : Apply Monaufacturing Lot/ S/N Detail
    '''<reamrks>### Table for Material Issue
    '''1 .Apply Monaufacturing Lot/ S/N Detail ##############
    '''2 .Details of the actual manufacturing bacth number ##############</reamrks>
    Public Shared tblMatIssueItemApplyLot As String = "inao_t"
    '''<reamrks> # Field </reamrks>
    Public Shared IssueDocNo As String = "inaodocno"
    Public Shared ent As String = "inaoent"
    Public Shared LineNo As String = "inaoseq"
    Public Shared ItemNo As String = "inao001"
    Public Shared ManufacturingLotNo As String = "inao008"
    Public Shared ManufacturingSerialNo As String = "inao009"
    Public Shared Quantity As String = "inao012"
    Public Shared ManufacturingDate As String = "inao010"
    Public Shared Valid_Date As String = "inao011"

    Private Shared strqlIssueItemApplyLot As String = "Select " & IssueDocNo & "," & LineNo & "," & ItemNo & ",  " &
" " & ManufacturingLotNo & "," & ManufacturingSerialNo & "," & Quantity & "," & ManufacturingDate & ", " &
" " & Valid_Date & "  from " & tblMatIssueItemApplyLot & " where " & IssueDocNo & "=@IssueDocNo "
    Public Shared Function GetMatIssueApplyLot(IssueDoc_No As String) As DataTable
        Dim Sql As String = strqlIssueItemApplyLot.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAO", "GetMatIssueApplyLot", "Sql = strqlIssueItemApplyLot", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMatIssueApplyLotDataSet(IssueDoc_No As String) As DataSet
        Dim Sql As String = strqlIssueItemApplyLot.Replace("@IssueDocNo", "'" & IssueDoc_No & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AIN, "INAO", "GetMatIssueApplyLotDataSet", "Sql = strqlIssueItemApplyLot", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function


End Class

