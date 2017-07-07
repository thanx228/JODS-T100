Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class BMBA
    '# Module : ABM
    Private Shared ABM As String = "ABM"
    '# Table : bmba_t
    '# abmm210 : BOM : Item Body : Tab Join Product
    '''<reamrks>## Table BOM  (Tab  Project)  ##############</reamrks>
    Public Shared tblBOMdetail As String = "bmba_t"
    '''<reamrks> # Field </reamrks>
    Public Shared MasterItemNo As String = "bmba001"
    Public Shared Feature As String = "bmba002"
    Public Shared ChildItemNo As String = "bmba003"
    Public Shared PartCode As String = "bmba004"
    Public Shared EffectiveDateTime As String = "bmba005"
    Public Shared ExpiredDateTime As String = "bmba006"
    Public Shared OperationNo As String = "bmba007"
    Public Shared OperationSequence As String = "bmba008"
    Public Shared LineNo As String = "bmba009"
    Public Shared IssueUnit As String = "bmba010"
    Public Shared QPA As String = "bmba011"
    Public Shared Denominator As String = "bmba012"
    Public Shared Required As String = "bmba013"
    Public Shared CharacteristicsManagement As String = "bmba014"
    Public Shared DesignatedIssuanceStore As String = "bmba015"
    Public Shared DesignatedIssuanceLocation As String = "bmba016"
    Public Shared FASselectedGroup As String = "bmba017"
    Public Shared PlugPosition As String = "bmba018"
    Public Shared ReferenceResearchAandDevelopmentCenter As String = "bmba019"
    Public Shared Optionalitems As String = "bmba020"
    Public Shared WoExpansionOption As String = "bmba021"
    Public Shared CorrespondentPurchasedMaterials As String = "bmba022"
    Public Shared TimeBucketOfMaterialInput As String = "bmba023"
    Public Shared MainSubstituteMaterial As String = "bmba024"
    Public Shared Accessories As String = "bmba025"
    Public Shared ECNDocNo As String = "bmba026"
    Public Shared UseformulaforUsageVolume As String = "bmba027"
    Public Shared UsageVolumeforMula As String = "bmba028"
    Public Shared LossRateType As String = "bmba029"
    Public Shared RecoilMaterial As String = "bmba030"
    Public Shared ConsignedMaterial As String = "bmba031"
    Public Shared UseformulaForUsageRate As String = "bmba033"
    Public Shared LossRateFormula As String = "bmba034"
    Public Shared Bonded As String = "bmba035"
    '''<reamrks> Condition Field </reamrks>
    Private Shared ent As String = "bmbaent"
    Private Shared Site As String = "bmbasite"
    Public Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "
    Public Shared wMasterItemNo As String = "bmba001"
    Public Shared wChildItem_No As String = "bmba003"


    '''<remarks> (Tab Project )Get BOM Deatil where MasterItemNo. ='?' </remarks> 
    Private Shared StrJoinProjectMasterItemNo As String = "Select " & LineNo & "," & ChildItemNo & "," &
    " " & PartCode & "," & OperationNo & "," & OperationSequence & "," & QPA & "," & Denominator & "," & IssueUnit & ", " &
    " " & EffectiveDateTime & "," & ExpiredDateTime & "," & UseformulaforUsageVolume & "," & CharacteristicsManagement & ", " &
    " " & Optionalitems & "," & FASselectedGroup & "," & Accessories & "," & PlugPosition & "," & ECNDocNo & ", " &
    " " & ReferenceResearchAandDevelopmentCenter & "," & UseformulaForUsageRate & "," & LossRateFormula & " " &
    " FROM " & tblBOMdetail & " where " & wMasterItemNo & " =@pMasterItemNo And " & wStandard & " "
    Public Shared Function GetProjectMasterItemNo(pMasterItemNo As String) As DataTable
        Dim strSQL As String = StrJoinProjectMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & pMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMBA", "GetProjectMasterItemNo", "strSQL = StrJoinProjectMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetProjectMasterItemNo_DataSet(pMasterItemNo As String) As DataSet
        Dim strSQL As String = StrJoinProjectMasterItemNo
        strSQL = strSQL.Replace("@pMasterItemNo", "'" & pMasterItemNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(strSQL, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(ABM, "BMBA", "GetProjectMasterItemNo", "strSQL = StrJoinProjectMasterItemNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class

