Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDS
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdt_t
    '''# apmt520 :Purchaes PR Receipt : Header 
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PR Receipt : Header  ##############</reamrks>
    Public Shared tblPRreceiptHeader As String = "pmds_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdsent"
    Public Shared Site As String = "pmdssite"
    Public Shared Status As String = "pmdsstus"
    Public Shared DocNo As String = "pmdsdocno"
    Public Shared DocumentDate As String = "pmdsdocdt"
    Public Shared DocType As String = "pmds000"
    Public Shared DebitDate As String = "pmds001"
    Public Shared Applicant As String = "pmds002"
    Public Shared RequestedDepartment As String = "pmds003"
    Public Shared SourceDocNo As String = "pmds006"
    Public Shared PurchaseVendor As String = "pmds007"
    Public Shared AccountVendor As String = "pmds008"
    Public Shared ShippingVendor As String = "pmds009"
    Public Shared VendorDeliveryNoteNo As String = "pmds010"
    Public Shared PurchaseProperty As String = "pmds011"
    Public Shared PurchaseChannel As String = "pmds012"
    Public Shared PurchaseCategory As String = "pmds013"
    Public Shared InterCompany As String = "pmds014"
    Public Shared LCImport As String = "pmds021"
    Public Shared ImportDate As String = "pmds022"
    Public Shared ImportDeclarationDoc As String = "pmds023"
    Public Shared ImportNo As String = "pmds024"
    Public Shared OneTimeCounterpartyID As String = "pmds028"
    Public Shared PaymentTerm As String = "pmds031"
    Public Shared TradeTerm As String = "pmds032"
    Public Shared TaxType As String = "pmds033"
    Public Shared TaxRate As String = "pmds034"
    Public Shared TaxIncludedInUP As String = "pmds035"
    Public Shared TransactionType As String = "pmds036"
    Public Shared Currency As String = "pmds037"
    Public Shared ExchRate As String = "pmds038"
    Public Shared PricingMethod As String = "pmds039"
    Public Shared PaymentPreferences As String = "pmds040"
    Public Shared IntercompanyTradeSN As String = "pmds041"
    Public Shared IntegrationOrderNo As String = "pmds042"
    Public Shared TotalAmtExclTaxForPurchasing As String = "pmds043"
    Public Shared TotalAmtInclTaxForPurchasing As String = "pmds044"
    Public Shared Notes As String = "pmds045"
    Public Shared OverallPurchaseTax As String = "pmds046"
    Public Shared MultilateralTradeBreakpoint As String = "pmds047"
    Public Shared InternalExternalPurchase As String = "pmds048"
    Public Shared ExchangeCalcBasis As String = "pmds049"
    Public Shared InterCompanyTradeTransferred As String = "pmds050"
    Public Shared ShippingOrderNo As String = "pmds051"
    Public Shared SupplierShippingDay As String = "pmds052"
    Public Shared InterCompanyProcessNo As String = "pmds053"
    Public Shared WithdrawalDate As String = "pmds081"
    Public Shared StoreReturnMethod As String = "pmds100"
    Public Shared AllowanceDate As String = "pmds101"
    Public Shared AllowanceOriginalInvoice As String = "pmds102"
    Public Shared ReceiptReservationNo As String = "pmds200"
    Public Shared Source As String = "pmds054"
    Public Shared SourceDocNo2 As String = "pmds055"
    Public Shared SourceDocNo3 As String = "pmds201"
    Public Shared SourceType As String = "pmds202"
    Public Shared ApplicationExecutionOrganizationObject As String = "pmdsunit"
    Public Shared NoUse As String = "pmds056"
    Public Shared IntegrateSource As String = "pmds057"
    Public Shared RecoiledMaterialPickingNo As String = "pmds058"
    Public Shared WayToOpenDiscountOfProof As String = "pmds103"
    '''<reamrks> # Adjustment information </reamrks>
    Public Shared DataOwner As String = "pmdsownid"
    Public Shared DataOwnerDept As String = "pmdsowndp"
    Public Shared DataCreatedBy As String = "pmdscrtid"
    Public Shared DataCreatedByDept As String = "pmdscrtdp"
    Public Shared DataCreatedDate As String = "pmdscrtdt"
    Public Shared ModifiedBy As String = "pmdsmodid"
    Public Shared LastModifiedDate As String = "pmdsmoddt"
    Public Shared DataConfirmedBy As String = "pmdscnfid"
    Public Shared DataConfirmedDate As String = "pmdscnfdt"
    Public Shared DataPostedBy As String = "pmdspstid"
    Public Shared DataPostedDate As String = "pmdspstdt"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  PR_Receipt By DocNo = ?  Purchaes PR Receipt : Header  ##############################################################
    Private Shared strWH_PR_Receipt_DocNo As String = "Select " & DocNo & " ," & DocumentDate & "," & DocType & ", " & DebitDate & "," & Status & "," &
        " " & Applicant & ", " & RequestedDepartment & "," & SourceDocNo & "," & PurchaseVendor & "," & AccountVendor & "," & ShippingVendor & ", " &
        " " & VendorDeliveryNoteNo & ", " & PurchaseProperty & "," & PurchaseChannel & "," & PurchaseCategory & "," & InterCompany & "," & LCImport & ", " &
        " " & ImportDate & "," & ImportDeclarationDoc & "," & ImportNo & "," & OneTimeCounterpartyID & "," & PaymentTerm & "," & TradeTerm & ", " &
        " " & TaxType & ", " & TaxRate & "," & TaxIncludedInUP & "," & TransactionType & "," & Currency & "," & ExchRate & "," & PricingMethod & ", " &
        " " & PaymentPreferences & "," & IntercompanyTradeSN & "," & IntegrationOrderNo & "," & TotalAmtExclTaxForPurchasing & "," & TotalAmtInclTaxForPurchasing & ", " &
        " " & Notes & ", " & OverallPurchaseTax & "," & MultilateralTradeBreakpoint & "," & InternalExternalPurchase & "," & ExchangeCalcBasis & ", " &
        " " & InterCompanyTradeTransferred & "," & ShippingOrderNo & "," & SupplierShippingDay & "," & InterCompanyProcessNo & "," & WithdrawalDate & ", " &
        " " & StoreReturnMethod & "," & AllowanceDate & "," & AllowanceOriginalInvoice & "," & ReceiptReservationNo & "," & Source & "," & SourceDocNo2 & ", " &
        " " & SourceDocNo3 & "," & SourceType & "," & ApplicationExecutionOrganizationObject & "," & NoUse & "," & IntegrateSource & "," & RecoiledMaterialPickingNo & ", " &
        " " & WayToOpenDiscountOfProof & "," & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DataCreatedByDept & ", " &
        " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmedBy & "," & DataConfirmedDate & ", " &
        " " & DataPostedBy & "," & DataPostedDate & " " &
        " FROM " & PMDS.tblPRreceiptHeader & "  " &
        " where " & wStandard & " And  " & DocNo & " = @pPR_Receipt_DocNo  "
    Public Shared Function GetPR_Receipt_By_DocNo_Header(strPR_Receipt_DocNo As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDS", "GetPR_Receipt_By_DocNo_Header", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_By_DocNo_Header_DataSet(strPR_Receipt_DocNo As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_DocNo
        Sql = Sql.Replace("@pPR_Receipt_DocNo", "'" & strPR_Receipt_DocNo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDS", "GetPR_Receipt_By_DocNo_Header_DataSet", "Sql = strWH_PR_Receipt_DocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  PR_Receipt By DucumentDate BETWEEN  Start = ?  To = ? Purchaes PR Receipt : Header  ########################################
    Private Shared strWH_PR_Receipt_DucumentDateBETWEEN As String = "Select " & DocNo & " ," & DocumentDate & "," & DocType & ", " & DebitDate & "," & Status & "," &
        " " & Applicant & ", " & RequestedDepartment & "," & SourceDocNo & "," & PurchaseVendor & "," & AccountVendor & "," & ShippingVendor & ", " &
        " " & VendorDeliveryNoteNo & ", " & PurchaseProperty & "," & PurchaseChannel & "," & PurchaseCategory & "," & InterCompany & "," & LCImport & ", " &
        " " & ImportDate & "," & ImportDeclarationDoc & "," & ImportNo & "," & OneTimeCounterpartyID & "," & PaymentTerm & "," & TradeTerm & ", " &
        " " & TaxType & ", " & TaxRate & "," & TaxIncludedInUP & "," & TransactionType & "," & Currency & "," & ExchRate & "," & PricingMethod & ", " &
        " " & PaymentPreferences & "," & IntercompanyTradeSN & "," & IntegrationOrderNo & "," & TotalAmtExclTaxForPurchasing & "," & TotalAmtInclTaxForPurchasing & ", " &
        " " & Notes & ", " & OverallPurchaseTax & "," & MultilateralTradeBreakpoint & "," & InternalExternalPurchase & "," & ExchangeCalcBasis & ", " &
        " " & InterCompanyTradeTransferred & "," & ShippingOrderNo & "," & SupplierShippingDay & "," & InterCompanyProcessNo & "," & WithdrawalDate & ", " &
        " " & StoreReturnMethod & "," & AllowanceDate & "," & AllowanceOriginalInvoice & "," & ReceiptReservationNo & "," & Source & "," & SourceDocNo2 & ", " &
        " " & SourceDocNo3 & "," & SourceType & "," & ApplicationExecutionOrganizationObject & "," & NoUse & "," & IntegrateSource & "," & RecoiledMaterialPickingNo & ", " &
        " " & WayToOpenDiscountOfProof & "," & DataOwner & "," & DataOwnerDept & "," & DataCreatedBy & "," & DataCreatedByDept & ", " &
        " " & DataCreatedDate & "," & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmedBy & "," & DataConfirmedDate & ", " &
        " " & DataPostedBy & "," & DataPostedDate & " " &
        " FROM " & PMDS.tblPRreceiptHeader & "  " &
        " where " & wStandard & " And  " & DocumentDate & " BETWEEN TO_DATE (@Sdate, 'yyyy/mm/dd') AND TO_DATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetPR_Receipt_DucumentDateBETWEEN_Header(Sdate As String, Edate As String) As DataTable
        Dim Sql As String = strWH_PR_Receipt_DucumentDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDS", "GetPR_Receipt_DucumentDateBETWEEN_Header", "Sql = strWH_PR_Receipt_DucumentDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetPR_Receipt_DucumentDateBETWEEN_Header_DataSet(Sdate As String, Edate As String) As DataSet
        Dim Sql As String = strWH_PR_Receipt_DucumentDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & Sdate & "'")
        Sql = Sql.Replace("@Edate", "'" & Edate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDS", "GetPR_Receipt_DucumentDateBETWEEN_Header_DataSet", "Sql = strWH_PR_Receipt_DucumentDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
