Imports System.Web.Services
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Configuration
Imports System.Data.OracleClient
Public Class PMAO
    '# Module : APM
    Private Shared APM As String = "APM"
    '# Table : pmao_t
    '# Counterparty Item Mapping File : Item Customer
    '# ไฟล์การทำรายการสินค้า counterparty : Item Customer
    Public Shared tbl_PO_CounterpartyItemMappingFile As String = "pmao_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmaoent"
    Public Shared StatusCode As String = "pmaostus"
    Public Shared CounterpartyNo As String = "pmao001"
    Public Shared CompanyItemsNo As String = "pmao002"
    Public Shared ProductFeature As String = "pmao003"
    Public Shared TradingPartnerItemNo As String = "pmao004"
    Public Shared BriefDescription As String = "pmao005"
    Public Shared ReferencePriceApprovalNo As String = "pmao006"   'เลขที่ Price ApprovalNo.
    Public Shared MainMappedItemNo As String = "pmao007"
    Public Shared NoUse As String = "pmao008"
    Public Shared TradingPartnerItemName As String = "pmao009"
    Public Shared Type As String = "pmao000"
    Public Shared PurchaseConfrimDateSaleOrder As String = "pmaoud010"
    ''' <remarks> Adjustment Information  </remarks>
    Public Shared DataOwner As String = "pmaoownid"
    Public Shared DataOwnerDep As String = "pmaoowndp"
    Public Shared DataCreatedBy As String = "pmaocrtid"
    Public Shared DataCreatedByDept As String = "pmaocrtdp"
    Public Shared DataCreatedDate As String = "pmaocrtdt"
    Public Shared ModifiedBy As String = "pmaomodid"
    Public Shared LastModifiedDate As String = "pmaomoddt"
    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = ent & "='3' "

    ''####### ไฟล์การทำรายการสินค้า CompanyItemsNo  = ? AND CutomerNo. ####################################################################
    Private Shared strCounterpartyItemMappingFile As String = "Select " & StatusCode & " ," & CounterpartyNo & "," & CompanyItemsNo & ", " & ProductFeature & ", " &
      " " & TradingPartnerItemNo & " ," & BriefDescription & "," & ReferencePriceApprovalNo & "," & MainMappedItemNo & "," & NoUse & ", " &
      " " & TradingPartnerItemName & "," & Type & "," & PurchaseConfrimDateSaleOrder & "," & DataOwner & "," & DataOwnerDep & ", " &
      " " & DataCreatedByDept & "," & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "  " &
      " FROM " & PMAO.tbl_PO_CounterpartyItemMappingFile & "  " &
      " where " & wStandard & " AND " & StatusCode & " in('Y','y')  " &
      " And " & CompanyItemsNo & " =@pItemsNo And " & CounterpartyNo & " =@pCounterpartyNo "
    Public Shared Function GetCounterpartyItemMappingFile(strCompanyItemsNo As String, strCounterpartyNo As String) As DataTable
        Dim Sql As String = strCounterpartyItemMappingFile
        Sql = Sql.Replace("@pItemsNo", "'" & strCompanyItemsNo & "'")
        Sql = Sql.Replace("@pCounterpartyNo", "'" & strCounterpartyNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMAO", "GetCounterpartyItemMappingFile", "Sql = strCounterpartyItemMappingFile", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

End Class

