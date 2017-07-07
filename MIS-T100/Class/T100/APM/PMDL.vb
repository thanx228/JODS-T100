Imports System
Imports System.Data
Imports System.Data.OracleClient
Public Class PMDL
    ''' <remarks>
    '''# Module T100 : APM 
    ''' # Table : pmdlt
    '''# apmt500 : Purchaes Order PO : Header 
    ''' </remarks>
    Private Shared APM As String = "APM"
    '''<reamrks>## Table PO : Header  ##############</reamrks>
    Public Shared tblPOHeader As String = "pmdl_t"
    '''<reamrks> # Field </reamrks>
    Public Shared ent As String = "pmdlent"
    Public Shared Site As String = "pmdlsite"
    Public Shared Status As String = "pmdlstus"
    Public Shared ApplicationOrg As String = "pmdlunit"
    Public Shared PONo As String = "pmdldocno"
    Public Shared PurchaseDate As String = "pmdldocdt"
    Public Shared Version As String = "pmdl001"
    Public Shared Purchaser As String = "pmdl002"
    Public Shared PurchaseDepartment As String = "pmdl003"
    Public Shared VendorNo As String = "pmdl004"
    Public Shared PurchaseProperty As String = "pmdl005"
    Public Shared MultilateralProperties As String = "pmdl006"
    Public Shared DataSourceType As String = "pmdl007"
    Public Shared SourceDocNo As String = "pmdl008"
    Public Shared PaymentTerm As String = "pmdl009"
    Public Shared TradeTerms As String = "pmdl010"
    Public Shared TaxCode As String = "pmdl011"
    Public Shared TaxRate As String = "pmdl012"
    Public Shared TaxIncludedInUP As String = "pmdl013"
    Public Shared Currency As String = "pmdl015"
    Public Shared ExchRate As String = " pmdl016"
    Public Shared PricingMethod As String = "pmdl017"
    Public Shared PaymentDiscountConditions As String = "pmdl018"
    Public Shared IncludeMPSMRPcalculation As String = "pmdl019"
    Public Shared DeliveryMethod As String = "pmdl020"
    Public Shared PaymentVendor As String = "pmdl021"
    Public Shared DeliveryVendor As String = "pmdl022"
    Public Shared PurchaseClassification1 As String = "pmdl023"
    Public Shared PurchaseClassification2 As String = "pmdl024"
    Public Shared ShippingToAddress As String = "pmdl025"
    Public Shared BillAddress As String = "pmdl026"
    Public Shared SupplierContactPerson As String = "pmdl027"
    Public Shared OneTimeTradingPartnerID As String = "pmdl028"
    Public Shared ReceiptDepartment As String = "pmdl029"
    Public Shared MultilateralTradeSwitched As String = "pmdl030"
    Public Shared IntercompanyTradingSN As String = "pmdl031"
    Public Shared FinalCustomer As String = "pmdl032"
    Public Shared InvoiceType As String = "pmdl033"
    Public Shared OverallUntaxedPurchaseAmount As String = "pmdl040"
    Public Shared OverallTaxedPurchaseAmount As String = "pmdl041"
    Public Shared OverallPurchaseTax As String = "pmdl042"
    Public Shared HoldReason As String = "pmdl043"
    Public Shared Memo As String = "pmdl044"
    Public Shared PrePaymentInvoiceCreationMethod As String = "pmdl046"
    Public Shared LogisticsSettlement As String = "pmdl047"
    Public Shared AccountFlowSettlement As String = "pmdl048"
    Public Shared CashSettlement As String = "pmdl049"
    Public Shared MultilateralFinalDestination As String = "pmdl050"
    Public Shared IntercompanyTradingProcessID As String = "pmdl051"
    Public Shared EndVendor As String = "pmdl052"
    Public Shared TwoTargetLocations As String = "pmdl053"
    Public Shared InternalExternalPurchase As String = "pmdl054"
    Public Shared ExchangeRateCalculationBasis As String = "pmdl055"
    Public Shared PurchaseCenter As String = "pmdl200"
    Public Shared ContactTelephone As String = "pmdl201"
    Public Shared Fax As String = "pmdl202"
    Public Shared PurchaseMethod As String = "pmdl203"
    Public Shared DistributionCenter As String = "pmdl204"
    Public Shared RetainFieldStr As String = "pmdl900"
    Public Shared FinalPurchaseValidDate As String = "pmdl205"
    Public Shared LongValidPeriodOrderYN As String = "pmdl206"
    Public Shared ViewCategory As String = "pmdl207"
    Public Shared ElectronicProcurementNo As String = "pmdl208"
    '''<reamrks> # Adjustment information </reamrks>
    Public Shared DataOwner As String = "pmdlownid"
    Public Shared DepartmentOfData As String = "pmdlowndp"
    Public Shared DataCreatedBy As String = "pmdlcrtid"
    Public Shared DataCreatedByDept As String = "pmdlcrtdp"
    Public Shared DataCreatedDate As String = "pmdlcrtdt"
    Public Shared ModifiedBy As String = "pmdlmodid"
    Public Shared LastModifiedDate As String = "pmdlmoddt"
    Public Shared DataConfirmationPersonnel As String = "pmdlcnfid"
    Public Shared DataConfirmationDate As String = "pmdlcnfdt"
    Public Shared DataPoster As String = "pmdlpstid"
    Public Shared DataPostedDate As String = "pmdlpstdt"

    '#### For TOP #################
    Public Shared Approved As String = Status & "='Y'"

    ''' <remarks> Condition  </remarks>
    Public Shared wStandard As String = Site & " ='JINPAO' And " & ent & "='3'"

    '####### where  PO By DocNo = ?  Purchaes PO Receipt : Header  ##############################################################
    Private Shared strWHPODocNo As String = "Select " & PONo & " ," & Status & "," & ApplicationOrg & ", " & PurchaseDate & "," & Version & "," &
        " " & Purchaser & ", " & PurchaseDepartment & "," & VendorNo & "," & PurchaseProperty & "," & MultilateralProperties & "," & DataSourceType & ", " &
        " " & SourceDocNo & ", " & PaymentTerm & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & ", " &
        " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PaymentDiscountConditions & "," & IncludeMPSMRPcalculation & "," & DeliveryMethod & ", " &
        " " & PaymentVendor & ", " & DeliveryVendor & "," & PurchaseClassification1 & "," & PurchaseClassification2 & "," & ShippingToAddress & "," & BillAddress & "," &
        " " & SupplierContactPerson & "," & OneTimeTradingPartnerID & "," & ReceiptDepartment & "," & MultilateralTradeSwitched & "," & IntercompanyTradingSN & ", " &
        " " & FinalCustomer & "," & InvoiceType & "," & OverallUntaxedPurchaseAmount & "," & OverallTaxedPurchaseAmount & "," & OverallPurchaseTax & ", " &
        " " & HoldReason & ", " & Memo & "," & PrePaymentInvoiceCreationMethod & "," & LogisticsSettlement & "," & AccountFlowSettlement & ", " &
        " " & CashSettlement & "," & MultilateralFinalDestination & "," & IntercompanyTradingProcessID & "," & EndVendor & "," & TwoTargetLocations & ", " &
        " " & InternalExternalPurchase & "," & ExchangeRateCalculationBasis & "," & PurchaseCenter & "," & ContactTelephone & "," & Fax & "," & PurchaseMethod & ", " &
        " " & DistributionCenter & "," & RetainFieldStr & "," & FinalPurchaseValidDate & "," & LongValidPeriodOrderYN & "," & ViewCategory & "," & ElectronicProcurementNo & ", " &
        " " & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & "," & DataCreatedByDept & "," & DataCreatedDate & ", " &
        " " & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & "  " &
        " FROM " & PMDL.tblPOHeader & "  " &
        " where " & wStandard & " And  " & PONo & " = @sPONo  "
    Public Shared Function GetHeaderPOByDocNo(PstrPONo As String) As DataTable
        Dim Sql As String = strWHPODocNo
        Sql = Sql.Replace("@sPONo", "'" & PstrPONo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDL", "GetHeaderPOByDocNo", "Sql = strWHPODocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetHeaderPOByDocNoDataSet(PstrPONo As String) As DataSet
        Dim Sql As String = strWHPODocNo
        Sql = Sql.Replace("@sPONo", "'" & PstrPONo & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDL", "GetHeaderPOByDocNoDataSet", "Sql = strWHPODocNo", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function

    '####### where  PO By DocumentDate BETWEEN StartDate = ? and EndDate = ?  Purchaes PO Receipt : Header  ##############################################################
    Private Shared strWHPODocumentDateBETWEEN As String = "Select " & PONo & " ," & Status & "," & ApplicationOrg & ", " & PurchaseDate & "," & Version & "," &
        " " & Purchaser & ", " & PurchaseDepartment & "," & VendorNo & "," & PurchaseProperty & "," & MultilateralProperties & "," & DataSourceType & ", " &
        " " & SourceDocNo & ", " & PaymentTerm & "," & TradeTerms & "," & TaxCode & "," & TaxRate & "," & TaxIncludedInUP & ", " &
        " " & Currency & "," & ExchRate & "," & PricingMethod & "," & PaymentDiscountConditions & "," & IncludeMPSMRPcalculation & "," & DeliveryMethod & ", " &
        " " & PaymentVendor & ", " & DeliveryVendor & "," & PurchaseClassification1 & "," & PurchaseClassification2 & "," & ShippingToAddress & "," & BillAddress & "," &
        " " & SupplierContactPerson & "," & OneTimeTradingPartnerID & "," & ReceiptDepartment & "," & MultilateralTradeSwitched & "," & IntercompanyTradingSN & ", " &
        " " & FinalCustomer & "," & InvoiceType & "," & OverallUntaxedPurchaseAmount & "," & OverallTaxedPurchaseAmount & "," & OverallPurchaseTax & ", " &
        " " & HoldReason & ", " & Memo & "," & PrePaymentInvoiceCreationMethod & "," & LogisticsSettlement & "," & AccountFlowSettlement & ", " &
        " " & CashSettlement & "," & MultilateralFinalDestination & "," & IntercompanyTradingProcessID & "," & EndVendor & "," & TwoTargetLocations & ", " &
        " " & InternalExternalPurchase & "," & ExchangeRateCalculationBasis & "," & PurchaseCenter & "," & ContactTelephone & "," & Fax & "," & PurchaseMethod & ", " &
        " " & DistributionCenter & "," & RetainFieldStr & "," & FinalPurchaseValidDate & "," & LongValidPeriodOrderYN & "," & ViewCategory & "," & ElectronicProcurementNo & ", " &
        " " & DataOwner & "," & DepartmentOfData & "," & DataCreatedBy & "," & DataCreatedByDept & "," & DataCreatedDate & ", " &
        " " & ModifiedBy & "," & LastModifiedDate & "," & DataConfirmationPersonnel & "," & DataConfirmationDate & "," & DataPoster & "," & DataPostedDate & "  " &
        " FROM " & PMDL.tblPOHeader & "  " &
        " where " & wStandard & " And  " & PurchaseDate & " BETWEEN TODATE (@Sdate, 'yyyy/mm/dd') AND TODATE (@Edate, 'yyyy/mm/dd') "
    Public Shared Function GetHeaderPOByDocumentDateBETWEEN(sDate As String, eDate As String) As DataTable
        Dim Sql As String = strWHPODocumentDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & eDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim dt As New DataTable
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(dt)
            Return dt '*** Return DataTable ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDL", "GetHeaderPOByDocumentDateBETWEEN", "Sql = strWHPODocumentDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
    Public Shared Function GetHeaderPOByDocumentDateBETWEENDataSet(sDate As String, eDate As String) As DataSet
        Dim Sql As String = strWHPODocumentDateBETWEEN
        Sql = Sql.Replace("@Sdate", "'" & sDate & "'")
        Sql = Sql.Replace("@Edate", "'" & eDate & "'")
        Dim dtAdapter As OracleDataAdapter
        Dim ds As New DataSet
        Dim objConn = New OracleConnection(clsDBConnect.strT100ConnectionString)
        Try
            dtAdapter = New OracleDataAdapter(Sql, objConn)
            dtAdapter.Fill(ds)
            Return ds '*** Return DataSet ***
        Catch ex As Exception
            GetPageError.GetClassT100(APM, "PMDL", "GetHeaderPOByDocumentDateBETWEENDataSet", "Sql = strWHPODocumentDateBETWEEN", ex.Message)
            Return Nothing
        Finally
            objConn.Close()
            objConn = Nothing
        End Try
    End Function
End Class
