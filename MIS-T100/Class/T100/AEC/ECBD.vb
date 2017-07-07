Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class ECBD
    '# Module : AEC
    Private Shared AEC As String = "AEC"
    '# Table : ecbd_t
    '# aecm200 : Item Routing : Item-Body Right
    '# aecm200 : Item Routing : Item-Body Left : Material Consumption draffs
    '''<reamrks>## Table Item Routing : Header  ##############</reamrks>
    Public Shared tblItemRoutingBodyRight As String = "ecbd_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ProcessPartNo As String = "ecbd001"
    Public Shared InitialQty As String = "ecbd005"
    Public Shared EndQty As String = "ecbd006"
    Public Shared ChangLossRate As String = "ecbd007"
    Public Shared FixedAssets As String = "ecbd008"

    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "ecbdent"
    Private Shared Site As String = "ecbdsite"
    Private Shared W_Standard As String = Site & "='JINPAO' and " & ent & "='3' "
    Public Shared W_ProcessPartNo As String = "ecbd001"

    '''<remarks> Get ItemRouting Body Right where ProcessPartNo. ='?' </remarks> 
    '''
    'Private Shared StrItemRoutingItemNo As String = "Select " & Initial_Qty & "," & End_Qty & " " &
    '" " & ChangLossRate & "," & FixedAssets & " " &
    '" FROM " & tblItemRoutingBodyRight & " where " & W_ProcessPartNo & " =@ProcessPartNo And " & W_Standard & " "
    'Public Shared Function GetItemRoutingItemNo(ItemRotingNo As String) As DataTable
    '    Dim strSQL As String = StrItemRoutingItemNo
    '    strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim dt As New DataTable
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSQL, objConn)
    '        dtAdapter.Fill(dt)
    '        Return dt '*** Return DataTable ***
    '    Catch ex As Exception
    '        GetPageError.GetClassT100(AEC, "ECBC", "GetItemRoutingItemNo", "Sql = StrItemRoutingItemNo", ex.Message)
    '        Return Nothing
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function
    'Public Shared Function GetItemRoutingItemNo_DataSet(ItemRotingNo As String) As DataSet
    '    Dim strSQL As String = StrItemRoutingItemNo
    '    strSQL = strSQL.Replace("@ProcessPartNo", "'" & ItemRotingNo & "'")
    '    Dim dtAdapter As OracleDataAdapter
    '    Dim ds As New DataSet
    '    Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
    '    Try
    '        dtAdapter = New OracleDataAdapter(strSQL, objConn)
    '        dtAdapter.Fill(ds)
    '        Return ds '*** Return DataSet ***
    '    Catch ex As Exception
    '        ex.Message.ToString()
    '        Return Nothing
    '    Finally
    '        objConn.Close()
    '        objConn = Nothing
    '    End Try
    'End Function
End Class

