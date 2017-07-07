Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class BMAA
    '# Module : ABM
    '# Table : bmaa_t
    Private Shared ABM As String = "ABM"
    '# abmm210 : BOM : Header
    '''<reamrks>## Table BOM MasterItemNo : Header  ##############</reamrks>
    Public Shared tblBOMheader As String = "bmaa_t"
    Public Shared MasterItemNo As String = "bmaa001"
    Public Shared Feature As String = "bmaa002"
    Public Shared Status As String = "bmaastus"
    Public Shared ProductionUnit As String = "bmaa004"
    Public Shared BacthQuantity As String = "bmaa003"
    Public Shared DataOwner As String = "bmaaownid"
    Public Shared DataOwnerDept As String = "bmaaowndp"
    Public Shared DataCreateBy As String = "bmaacrtid"
    Public Shared DataCreateByDept As String = "bmaacrtdp"
    Public Shared DataCreateDate As String = "bmaacrtdt"
    Public Shared DataModifyBy As String = "bmaamodid"
    Public Shared LastModifyDate As String = "bmaamoddt"
    Public Shared DataConfirmationPersonal As String = "bmaacnfid"
    Public Shared DateConfrimDate As String = "bmaacnfdt"

    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "bmaaent"
    Private Shared Site As String = "bmaasite"
    Public Shared WStandard As String = Site & "='JINPAO' and " & ent & "='3' "
    Public Shared WMasterItemNo As String = "bmaa001"
    Public Shared Approved As String = Status & "='Y'"

    '--Page CheckBOM PopUp 
    '--Shearch BOM Detail  where Item /Refresh DataTable
    Private Shared SelectBomDetl As String = "select " & BMBA.LineNo & "," & BMBA.ChildItemNo & "," & IMAAL.ProductName & "," & IMAAL.Specifaction & "," &
        " " & BMBA.QPA & "," & BMBA.IssueUnit & ",case " & IMAF.SupplyStrategy & " when '1' then 'Purchase' when '2' then 'Manufacture' end as Supply" &
        "  from " & tblBOMheader & "" &
        " left join " & BMBA.tblBOMdetail & " on " & MasterItemNo & "=" & BMBA.MasterItemNo & "" &
        " left join " & IMAAL.tblProductionDetail & " On " & BMBA.ChildItemNo & " = " & IMAAL.ProductItem & "" &
        " left join " & IMAF.tblSaleItemProperty & " on " & BMBA.ChildItemNo & " = " & IMAF.ItemNo & " where" &
        " " & WStandard & " And " & Approved & "And " &
        " " & BMBA.wStandard & " And " &
        " " & IMAAL.WStandard & " And " & IMAAL.enUS & "And " &
        " " & IMAF.wStandard & " And " &
        " " & MasterItemNo & "='@MasterItemNo' order by " & MasterItemNo & ""
    Public Shared Function BomDetl(ByVal ItemNo As String)
        Dim Oral As String = SelectBomDetl
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@MasterItemNo", ItemNo)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString, tempDataTable)
        Return tempDataTable
    End Function

    '''<remarks> Get BOM MasterItemNo where MasterItemNo. ='?' </remarks> 
    Private Shared StrMasterItemNo As String = "Select " & MasterItemNo & "," & Feature & "," & Status & ", " &
    " " & ProductionUnit & "," & BacthQuantity & "," & DataOwner & "," & DataOwnerDept & "," & DataCreateBy & ", " &
    " " & DataCreateByDept & "," & DataCreateDate & "," & DataModifyBy & "," & LastModifyDate & ", " &
    " " & DataConfirmationPersonal & "," & DateConfrimDate & "  " &
    " FROM " & tblBOMheader & " where " & WMasterItemNo & " =@pMasterItemNo And " & WStandard & " "
    Public Shared Function GetMasterItemNo(MasterItemNo As String) As DataTable
        Dim strSQL As String = StrMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & MasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMAA", "GetMasterItemNo", "Sql = StrMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetMasterItemNo_DataSet(MasterItemNo As String) As DataSet
        Dim strSQL As String = StrMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & MasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMAA", "GetMasterItemNo_DataSet", "Sql = StrMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

