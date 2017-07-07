Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class BMAB
    '# Module : ABM
    Private Shared ABM As String = "ABM"
    '# Table : bmab_t
    '# abmm210 : BOM : Item Body : Tab Join Product,Project
    '''<reamrks>## Table BOM  (Tab Join Product , Project)  ##############</reamrks>
    Public Shared tblBOMdetail As String = "bmab_t"
    '''<reamrks> # Field </reamrks>
    Public Shared MasterItemNo As String = "bmab001"
    Public Shared Feature As String = "bmab002"
    Public Shared JointProductNo As String = "bmab003"
    Public Shared EstimateRatio As String = "bmab004"
    Public Shared EffectiveDate As String = "bmab005"
    Public Shared ExpiredDate As String = "bmab006"


    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "bmabent"
    Private Shared Site As String = "bmabsite"
    Private Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "
    Public Shared wMasterItemNo As String = "bmab001"

    '''<remarks> (Tab Joint Product )Get BOM Deatil where MasterItemNo. ='?' </remarks> 
    Private Shared StrJoinProductMasterItemNo As String = "Select " & MasterItemNo & "," & Feature & ", " &
    " " & JointProductNo & "," & EstimateRatio & "," & EffectiveDate & "," & ExpiredDate & " " &
    " FROM " & tblBOMdetail & " where " & wStandard & " AND" & wMasterItemNo & " =@pMasterItemNo "
    Public Shared Function GetJoinProductMasterItemNo(pMasterItemNo As String) As DataTable
        Dim strSQL As String = StrJoinProductMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & pMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMAB", "GetJoinProductMasterItemNo", "strSQL = StrJoinProductMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetJoinProductMasterItemNo_DataSet(pMasterItemNo As String) As DataSet
        Dim strSQL As String = StrJoinProductMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & pMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMAB", "GetJoinProductMasterItemNo_DataSet", "strSQL = StrJoinProductMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class
