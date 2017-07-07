Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECBC
    '# Module : AEC
    Private Shared AEC As String = "AEC"
    '# Table : ecbc_t
    '# aecm200 : Item Routing : Item-Body Left : Material Consumption draffs
    '''<reamrks>## Table Item Routing : Header  ##############</reamrks>
    Public Shared tblItemRoutingBodyMaterial As String = "ecbc_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ProcessPartNo As String = "ecbc001"
    Public Shared LineNo As String = "ecbc004"
    Public Shared ChildItemNo As String = "ecbc005"
    Public Shared Position As String = "ecbc006"
    Public Shared QPA As String = "ecbc007"
    Public Shared Demoninator As String = "ecbc008"
    Public Shared UsangeVolumeUnit As String = "ecbc009"

    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "ecbcent"
    Private Shared Site As String = "ecbcsite"
    Private Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "

    '''<remarks> Get ItemRouting Body where ProcessPartNo. ='?' </remarks>  
    Private Shared StrItemRoutingItemNo As String = "Select " & LineNo & "," & ChildItemNo & "," & Position & ", " &
    " " & QPA & "," & Demoninator & "," & UsangeVolumeUnit & " " &
    " FROM " & tblItemRoutingBodyMaterial & " where " & ProcessPartNo & " =@ProcessPartNo And " & wStandard & " "
    Public Shared Function GetItemRoutingItemNo(ItemRotingNo As String) As DataTable
        Dim strSQL As String = StrItemRoutingItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBC", "GetItemRoutingItemNo", "Sql = StrItemRoutingItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetItemRoutingItemNo_DataSet(ItemRotingNo As String) As DataSet
        Dim strSQL As String = StrItemRoutingItemNo
        strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(AEC, "ECBC", "GetItemRoutingItemNo_DataSet", "Sql = StrItemRoutingItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

