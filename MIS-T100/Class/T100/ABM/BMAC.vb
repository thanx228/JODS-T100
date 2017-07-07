Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class BMAC
    '# Module : ABM
    Private Shared ABM As String = "ABM"
    '# Table : bmac_t
    '# abmm210 : BOM : Item Body : Tab Sub Product
    '''<reamrks>## Table BOM  (Tab Join Product )  ##############</reamrks>
    Public Shared tblBOMdetail As String = "bmac_t"
    '''<reamrks> # Field </reamrks>
    Public Shared MasterItemNo As String = "bmac001"
    Public Shared Feature As String = "bmac002"
    Public Shared ByProductItenNo As String = "bmac003"
    Public Shared Unit As String = "bmac004"
    Public Shared Quantity As String = "bmac005"
    Public Shared Denominator As String = "bmac006"
    Public Shared EffectiveDate As String = "bmac007"
    Public Shared ExpiredDate As String = "bmac008"

    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "bmacent"
    Private Shared Site As String = "bmacsite"
    Private Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "
    Public Shared wMasterItemNo As String = "bmac001"


    '''<remarks> (Tab Sub Product )Get BOM Deatil where MasterItemNo. ='?' </remarks> 
    Private Shared StrJoinProductMasterItemNo As String = "Select " & MasterItemNo & "," & Feature & ", " &
    " " & ByProductItenNo & "," & Unit & "," & Quantity & "," & Denominator & "," & EffectiveDate & "," & ExpiredDate & " " &
    " FROM " & tblBOMdetail & " where " & wStandard & " AND " & wMasterItemNo & " =@pMasterItemNo "
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
            GetPageError.GetClassT100(ABM, "BMAC", "GetJoinProductMasterItemNo", "strSQL = StrJoinProductMasterItemNo", ex.Message)
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
            GetPageError.GetClassT100(ABM, "BMAC", "GetJoinProductMasterItemNo_DataSet", "strSQL = StrJoinProductMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
